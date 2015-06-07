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
   
    internal class TcpWriter: Writer, IDisposable
    {
        private static readonly Logger _thisClassSupportLogger = InnerSupportLogger.Instance.GetClassLogger(typeof(TcpWriter));
        private readonly TcpHelper _writer;

        private readonly LogLevel _logLevel;

        private ErrorTimeTracker _errorTracker = new ErrorTimeTracker(TimeSpan.FromMinutes(5));
        private volatile bool _isDisposed = false;

        
        public TcpWriter(TcpWriterConfiguration config)
            : base(config.Level)
        {
            if (config.ServerAddress == null)
                throw new ArgumentNullException("ServerAddress");

            _writer = new TcpHelper(config.ServerAddress, config.Port, config.ConnectionTestTimeMs);
            
            _writer.Start();
        }


        protected override void Dispose(DisposeReason reason)
        {
            if (!_isDisposed)
            {
                _isDisposed = true;

                if (reason != DisposeReason.Finalize)
                {
                    if (_writer != null)
                    {
                        _writer.Stop();
                        _writer.Dispose(reason);
                    }
                }
                else
                {
                     //if (_writer != null)
                      //  _writer.FinalizeFast();
                }
            }
        }

        public override bool Write(LoggingEvent data)
        {         
            if (data == null)
            {
                throw new ArgumentNullException("LoggingEvent data");
            }

            return _writer.Write(ConvertToString(data));
        }

        protected virtual string ConvertToString(LoggingEvent log)
        {
            int unixTimestamp = (Int32)(log.Date.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            var sb = new StringBuilder(64);
            sb = sb.Append("{")
                .Append("timestamp", log.Date.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"))
                .Append("@version", 1)
                .Append("level", log.Level.ToString())
                .Append("message", log.Message)
                .Append("logger", log.ProcessName)
                .Append("machinename", log.MachineName);
                //.Append("host", log.MachineName);

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
