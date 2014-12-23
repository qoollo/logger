using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerServer.Main
{
    public class LoggerServerController: IDisposable
    {
        private volatile bool _isWork = false;
        private volatile bool _isDisposed = false;

        private Qoollo.Logger.LoggerBase _mainLogger;
        private Qoollo.Logger.LoggerBase _supportLogger;

        private LoggerHandler _handler;

        private Qoollo.Logger.Net.LoggerServer _tcpServer;
        private Qoollo.Logger.Net.LoggerServer _pipeServer;

        public LoggerServerController()
        {
        }


        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            _supportLogger.Fatal(e.ExceptionObject as Exception, "Unhandled exception");
        }

        public void Start()
        {
            if (_isDisposed)
                throw new ObjectDisposedException(this.GetType().Name);
            if (_isWork)
                throw new InvalidOperationException("LoggerServerController is already started");
            _isWork = true;

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            _supportLogger = Qoollo.Logger.LoggerFactory.CreateLoggerFromAppConfig("LoggerServer", "LoggerServerConfig", "SupportLoggerConfigurationSection");
            //_supportLogger.SetConverterFactory(new Qoollo.Logger.LoggingEventConverters.ConverterFactory());
            LogNs.Logger.Init(_supportLogger);

            _mainLogger = Qoollo.Logger.LoggerFactory.CreateLoggerFromAppConfig("LoggerServer", "LoggerServerConfig", "MainLoggerConfigurationSection");
            //_mainLogger.SetConverterFactory(new Qoollo.Logger.LoggingEventConverters.ConverterFactory());

            _handler = new LoggerHandler(_mainLogger);

            var configLoader = new LoggerServer.Main.Configuration.LoggerServerConfigSectionGroup();
            var config = configLoader.LoadLoggerServerConfigurationSection();

            if (config.TcpServerConfig.IsEnabled)
                _tcpServer = Qoollo.Logger.Net.LoggerServer.CreateOnTcp(_handler, config.TcpServerConfig.Port);

            if (config.PipeServerConfig.IsEnabled)
                _pipeServer = Qoollo.Logger.Net.LoggerServer.CreateOnPipe(_handler, config.PipeServerConfig.PipeName);


            _tcpServer.Open();
            _pipeServer.Open();

            _supportLogger.Debug("Logger server controller started");
        }


        protected void Dispose(bool isUserCall)
        {
            if (_isDisposed)
                return;

            _isDisposed = true;

            if (isUserCall)
            {
                if (_tcpServer != null)
                    _tcpServer.Dispose();

                if (_pipeServer != null)
                    _pipeServer.Dispose();

                _mainLogger.Dispose();

                _supportLogger.Debug("Logger Server Controller disposed");
                AppDomain.CurrentDomain.UnhandledException -= CurrentDomain_UnhandledException;
                _supportLogger.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
