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
using Qoollo.Logger.RealWriters.Helpers;

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



        private static StringBuilder AppendScreened(StringBuilder sb, string value)
        {
            Contract.Requires(sb != null);
            Contract.Requires(value != null);

            int screenStartPosition = sb.Length;
            sb.Append(value);
            sb.Replace(Environment.NewLine, @"\n", screenStartPosition, sb.Length - screenStartPosition);
            sb.Replace("\n", @"\n", screenStartPosition, sb.Length - screenStartPosition);
            sb.Replace("\r", @"", screenStartPosition, sb.Length - screenStartPosition);
            sb.Replace("\t", @"\t", screenStartPosition, sb.Length - screenStartPosition);
            sb.Replace("\\", @"\\", screenStartPosition, sb.Length - screenStartPosition);
            sb.Replace("\'", @"\'", screenStartPosition, sb.Length - screenStartPosition);
            sb.Replace("\"", "\\\"", screenStartPosition, sb.Length - screenStartPosition);

            return sb;
        }

        private static bool AppendJsonParamConditional(StringBuilder sb, string key, string value, bool withComma = false, bool screen = false)
        {
            Contract.Requires(sb != null);
            Contract.Requires(key != null);

            if (value == null)
                return false;

            sb.Append("\"").Append(key).Append("\"").Append(":").Append("\"");
            if (screen)
                AppendScreened(sb, value);
            else
                sb.Append(value);
            sb.Append("\"");

            if (withComma)
                sb.Append(",");

            return true;
        }

        private static bool AppendJsonParamConditional(StringBuilder sb, string key, List<string> valueList, bool withComma = false, bool screen = false)
        {
            Contract.Requires(sb != null);
            Contract.Requires(key != null);

            if (valueList == null)
                return false;

            sb.Append("\"").Append(key).Append("\"").Append(":").Append("[");

            if (screen)
            {
                for (int i = 0; i < valueList.Count; i++)
                {
                    if (valueList[i] == null)
                        continue;

                    sb.Append("\"");
                    AppendScreened(sb, valueList[i]);
                    sb.Append("\",");
                }
            }
            else
            {
                for (int i = 0; i < valueList.Count; i++)
                {
                    if (valueList[i] == null)
                        continue;

                    sb.Append("\"");
                    sb.Append(valueList[i]);
                    sb.Append("\",");
                }
            }


            if (sb[sb.Length - 1] == ',')
                sb.Remove(sb.Length - 1, 1);

            sb.Append("]");

            if (withComma)
                sb.Append(",");

            return true;
        }

        /// <summary>
        /// Convert LoggingEvent to JSON string for LogStash
        /// </summary>
        /// <param name="log">Log event</param>
        /// <returns>JSON string</returns>
        protected virtual string ConvertToString(LoggingEvent log)
        {
            Contract.Requires(log != null);

            var sb = new StringBuilder(256);
            sb = sb.Append("{");
            AppendJsonParamConditional(sb, "timestamp", log.Date.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"), withComma: true, screen: false);
            AppendJsonParamConditional(sb, "@version", "1", withComma: true, screen: false);
            AppendJsonParamConditional(sb, "level", log.Level.Name, withComma: true, screen: false);
            AppendJsonParamConditional(sb, "@message", log.Message ?? "", withComma: true, screen: true);
            AppendJsonParamConditional(sb, "context", log.Context, withComma: true, screen: true);

            AppendJsonParamConditional(sb, "file", log.FilePath, withComma: true, screen: true);
            AppendJsonParamConditional(sb, "line_number", log.LineNumber > 0 ? log.LineNumber.ToString() : null, withComma: true, screen: false);

            AppendJsonParamConditional(sb, "assembly", log.Assembly, withComma: true, screen: true);
            AppendJsonParamConditional(sb, "namespace", log.Namespace, withComma: true, screen: false);
            AppendJsonParamConditional(sb, "class", log.Clazz, withComma: true, screen: false);
            AppendJsonParamConditional(sb, "method", log.Method, withComma: true, screen: false);

            AppendJsonParamConditional(sb, "process_name", log.ProcessName, withComma: true, screen: true);
            AppendJsonParamConditional(sb, "pid", log.ProcessId > 0 ? log.ProcessId.ToString() : null, withComma: true, screen: false);
            AppendJsonParamConditional(sb, "machine_name", log.MachineName, withComma: true, screen: true);
            AppendJsonParamConditional(sb, "machine_ip", log.MachineIpAddress, withComma: true, screen: true);

            if (log.StackSources != null && log.StackSources.Count > 0)
            {
                AppendJsonParamConditional(sb, "stack_source", log.StackSources, withComma: true, screen: true);
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

                AppendJsonParamConditional(sb, "exception_summary", _exceptionConverter.Convert(log), withComma: true, screen: true);
                AppendJsonParamConditional(sb, "exception_messages", messages, withComma: true, screen: true);
                AppendJsonParamConditional(sb, "exception_types", types, withComma: true, screen: true);
            }

            if (sb[sb.Length - 1] == ',')
                sb.Remove(sb.Length - 1, 1);

            string result = sb.Append("}\n").ToString();
            return result;
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
