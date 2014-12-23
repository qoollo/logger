using Qoollo.Logger;

namespace LoggerServer.Main.LogNs
{
    public class Logger : Qoollo.Logger.Logger
    {
        private static Logger _instance = new Logger(LogLevel.FullLog, GetEmptyLogger());

        public static Logger Instance
        {
            get { return _instance; }
        }

        private Logger(LogLevel logLevel, ILogger innerLoggerWrapper)
            : base(logLevel, "Main", innerLoggerWrapper)
        {
        }

        [LoggerWrapperInitializationMethod]
        public static void Init(ILogger innerLogger)
        {
            _instance = new Logger(innerLogger.Level, innerLogger);
        }
    }
}
