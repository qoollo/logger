using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerServer.Main
{
    public class LoggerHandler: Qoollo.Logger.Net.NetService
    {
        private const int LogPeriod = 2000;

        private Qoollo.Logger.ILogger _mainLogger;
        private long _receivedMsgCount = 0;

        public LoggerHandler(Qoollo.Logger.ILogger mainLogger)
        {
            if (mainLogger == null)
                throw new ArgumentNullException("mainLogger");

            _mainLogger = mainLogger;
        }

        protected override void OnDataRecieved(Qoollo.Logger.Common.LoggingEvent data)
        {
            _mainLogger.Write(data);

            if (System.Threading.Interlocked.Increment(ref _receivedMsgCount) % LogPeriod == 0)
                LogNs.Logger.Instance.Info("Processed messages count: " + System.Threading.Interlocked.Read(ref _receivedMsgCount).ToString());
        }
    }
}
