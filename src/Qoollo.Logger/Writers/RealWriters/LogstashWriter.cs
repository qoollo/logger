using System;
using System.Collections.Generic;
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
using Qoollo.Logger.Net;
using Qoollo.Logger.RealWriters.Helpers;

namespace Qoollo.Logger.Writers
{

    internal class LogstashWriter : Writer, IDisposable
    {
        private static readonly Logger _thisClassSupportLogger = InnerSupportLogger.Instance.GetClassLogger(typeof(LogstashWriter));
        private readonly TcpHelper _writer;

        private readonly LogLevel _logLevel;

        private ErrorTimeTracker _errorTracker = new ErrorTimeTracker(TimeSpan.FromMinutes(5));
        private volatile bool _isDisposed = false;
        private readonly object _lockWrite = new object();

        private const int connectionTestTimeoutMaxMs = 16000;

        public LogstashWriter(LogstashWriterConfiguration config)
            : base(config.Level)
        {
            if (config.ServerAddress == null)
                throw new ArgumentNullException("ServerAddress");

            _writer = new TcpHelper(config.ServerAddress, config.Port, connectionTestTimeoutMaxMs);

            _writer.Start();
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

        protected virtual string ConvertToString(LoggingEvent log)
        {
            var sb = new StringBuilder(64);
            sb = sb.Append("{")
                .Append("timestamp", log.Date.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"))
                .Append("@version", 1)
                .Append("level", log.Level.ToString())
                .Append("@message", log.Message)
                .Append("logger", log.ProcessName)
                .Append("machinename", log.MachineName)
                .Append("ip", log.MachineIpAddress);

            if (log.Exception != null)
                sb = sb.Append("exception", log.Exception.ToString());

            var result = sb.ToString().TrimEnd(',') + "}\n";
            return result;
        }

    }

    internal static class Extentions
    {
        public static StringBuilder Append(this StringBuilder sb, string key, int value)
        {
            key = key.Trim('\'').Trim('"');
            return AppendDict(sb, String.Format("\"{0}\"", key), String.Format("\"{0}\"", value));
        }

        public static StringBuilder Append(this StringBuilder sb, string key, string value)
        {
            key = key.Trim('\'').Trim('"');
            value = value.Trim('\'').Trim('"');
            return AppendDict(sb, String.Format("\"{0}\"", key), String.Format("\"{0}\"", value));
        }

        private static StringBuilder AppendDict(StringBuilder sb, string key, string value)
        {
            return sb.Append(key).Append(":").Append(value).Append(",");
        }
    }


}
