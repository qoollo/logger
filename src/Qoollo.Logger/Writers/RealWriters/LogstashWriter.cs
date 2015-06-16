using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Qoollo.Logger.Common;
using Qoollo.Logger.Configuration;
using Qoollo.Logger.Helpers;
using Qoollo.Logger.LoggingEventConverters;
using Qoollo.Logger.Net;
using Qoollo.Logger.Writers.RealWriters.Helpers;

namespace Qoollo.Logger.Writers
{
    internal class LogstashWriter : Writer
    {
        private static readonly Logger _thisClassSupportLogger = InnerSupportLogger.Instance.GetClassLogger(typeof(LogstashWriter));
        private const int _connectionTestTimeoutMaxMs = 16000;

        private readonly LogLevel _logLevel;

        private readonly TcpHelper _writer;

        private LoggingEventConverterBase _exceptionConverter;
        private LoggingEventConverterBase _stackSourceConverter;

        private ErrorTimeTracker _errorTracker = new ErrorTimeTracker(TimeSpan.FromMinutes(5));
        private volatile bool _isDisposed = false;
        private readonly object _lockWrite = new object();


        public LogstashWriter(LogstashWriterConfiguration config)
            : base(config.Level)
        {
            Contract.Requires<ArgumentNullException>(config != null);

            _writer = new TcpHelper(config.ServerAddress, config.Port, _connectionTestTimeoutMaxMs);
            _logLevel = config.Level;
            _writer.Start();

            SetConverterFactory(ConverterFactory.Default);
        }

        public override void SetConverterFactory(ConverterFactory factory)
        {
            base.SetConverterFactory(factory);

            _exceptionConverter = factory.CreateExceptionConverter();
            _stackSourceConverter = factory.CreateStackSourceConverter();
        }


        public override bool Write(LoggingEvent data)
        {
            if (_isDisposed)
            {
                if (_errorTracker.CanWriteErrorGetAndUpdate())
                    _thisClassSupportLogger.Error("Attempt to write LoggingEvent in Disposed state");

                return false;
            }

            if (data.Level < _logLevel)
                return true;

            bool result = false;

            try
            {
                lock (_lockWrite)
                {
                    if (_isDisposed)
                        return false;

                    if (!_writer.IsStarted)
                        _writer.Start();

                    if (!_writer.HasConnection)
                    {
                        if (_errorTracker.CanWriteErrorGetAndUpdate())
                            _thisClassSupportLogger.Error("Connection to network logger server is not established: " + _writer.RemoteSideName);

                        return false;
                    }

                    _writer.Write(ConvertToString(data));
                    result = true;
                }
            }
            catch (SocketException ex)
            {
                if (_errorTracker.CanWriteErrorGetAndUpdate())
                    _thisClassSupportLogger.Error(ex, "Error while sending data to remote logstash tcp server: " + _writer.RemoteSideName);
            }
            catch (CommunicationException ex)
            {
                if (_errorTracker.CanWriteErrorGetAndUpdate())
                    _thisClassSupportLogger.Error(ex, "Error while sending data to remote logstash tcp server server: " + _writer.RemoteSideName);
            }
            catch (TimeoutException ex)
            {
                if (_errorTracker.CanWriteErrorGetAndUpdate())
                    _thisClassSupportLogger.Error(ex, "Error while sending data to remote logstash tcp server server: " + _writer.RemoteSideName);
            }
            catch (Exception ex)
            {
                _thisClassSupportLogger.Error(ex, "Fatal error while sending data to remote logstash tcp server server: " + _writer.RemoteSideName);
                throw new LoggerException("Fatal error while sending data to remote logstash tcp server server: " + _writer.RemoteSideName, ex);
            }

            return result;
        }


        /// <summary>
        /// Convert LoggingEvent to JSON string for LogStash
        /// </summary>
        /// <param name="log">Log event</param>
        /// <returns>JSON string</returns>
        protected virtual string ConvertToString(LoggingEvent log)
        {
            Contract.Requires(log != null);

            JsonSimpleWriter writer = new JsonSimpleWriter(1024);
            writer.AppendJsonBeginObject();
            {
                writer.AppendJsonParamConditional("@timestamp", log.Date.ToUniversalTime().ToString("o", System.Globalization.CultureInfo.InvariantCulture), screen: false);
                writer.AppendJsonParamConditional("@version", "1", screen: false);
                writer.AppendJsonParamConditional("level", log.Level.Name, screen: false);
                writer.AppendJsonParamConditional("message", log.Message ?? "", screen: true);
                writer.AppendJsonParamConditional("context", log.Context, screen: true);
                
                writer.AppendJsonKey("location").AppendJsonBeginObject();
                {
                    writer.AppendJsonParamConditional("file", log.FilePath, screen: true);
                    writer.AppendJsonParamConditional("line_number", log.LineNumber > 0 ? log.LineNumber.ToString() : null, screen: false);

                    writer.AppendJsonParamConditional("assembly", log.Assembly, screen: true);
                    writer.AppendJsonParamConditional("namespace", log.Namespace, screen: false);
                    writer.AppendJsonParamConditional("class", log.Clazz, screen: false);
                    writer.AppendJsonParamConditional("method", log.Method, screen: false);

                    writer.AppendJsonParamConditional("stack_source", log.StackSources, screen: true);

                    writer.AppendJsonEndObject();
                }

                writer.AppendJsonKey("source").AppendJsonBeginObject();
                {
                    writer.AppendJsonParamConditional("process_name", log.ProcessName, screen: true);
                    writer.AppendJsonParamConditional("pid", log.ProcessId > 0 ? log.ProcessId.ToString() : null, screen: false);
                    writer.AppendJsonParamConditional("machine_name", log.MachineName, screen: true);
                    writer.AppendJsonParamConditional("machine_ip", log.MachineIpAddress, screen: true);

                    writer.AppendJsonEndObject();
                }

                if (log.Exception != null)
                {
                    List<string> messages = new List<string>();
                    List<string> types = new List<string>();
                    Error currentError = log.Exception;
                    while (currentError != null)
                    {
                        messages.Add(currentError.Message);
                        types.Add(currentError.Type);
                        currentError = currentError.InnerError;
                    }

                    writer.AppendJsonParamConditional("exception", _exceptionConverter.Convert(log), screen: true);
                    writer.AppendJsonParamConditional("exception.message", messages, screen: true);
                    writer.AppendJsonParamConditional("exception.type", types, screen: true);
                }

                writer.AppendJsonEndObject(withComma: false);
            }
            return writer.GetStringBuilder().Append("\n").ToString();
        }

        protected override void Dispose(DisposeReason reason)
        {
            if (!_isDisposed)
            {
                _isDisposed = true;

                if (reason != DisposeReason.Finalize)
                {
                    lock (_lockWrite)
                    {
                        if (_writer != null)
                        {
                            _writer.Stop();
                            _writer.Dispose();
                        }
                    }
                }
                else
                {
                    if (_writer != null)
                        _writer.FinalizeFast();
                }
            }
        }
    }
}
