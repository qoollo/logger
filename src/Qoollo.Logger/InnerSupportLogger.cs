using Qoollo.Logger.Common;
using Qoollo.Logger.Configuration;
using Qoollo.Logger.Writers;

namespace Qoollo.Logger
{
    /// <summary>
    /// Inner logger
    /// </summary>
    internal static class InnerSupportLogger
    {
        public const string SupportLoggerName = "InnerSupportLogger";
        private static readonly object LockCreation = new object();
        private static volatile Logger _instance;

        public static Logger Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (LockCreation)
                    {
                        if (_instance == null)
                        {
                            string template = "{DateTime}|{Level}|{Msg}|{Class}|{Exception}";
                            string fileName = "support.log";

                            var queue = new AsyncQueue(new AsyncQueueWrapperConfiguration(), new SupportFileWriter(fileName, template));
#if (NET40CP)
                            _instance = new Logger11(config, SupportLoggerName, queue, true);
#else
                            _instance = new Logger(LogLevel.Info, SupportLoggerName, queue, false, true);
#endif

                        }
                    }
                }

                return _instance;
            }
        }
    }
}