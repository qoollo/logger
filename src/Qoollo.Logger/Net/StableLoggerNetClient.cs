using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Qoollo.Logger.Net
{
    /// <summary>
    /// Клиент сетевого логгера
    /// </summary>
    public class StableLoggerNetClient: INetService, IDisposable
    {
        /// <summary>
        /// Создать клиент на TCP
        /// </summary>
        /// <param name="address">Адрес</param>
        /// <param name="port">Порт</param>
        /// <param name="serviceName">Имя сервиса WCF</param>
        /// <returns>Созданный клиент</returns>
        public static StableLoggerNetClient CreateOnTcp(string address, int port, string serviceName = "LoggingService")
        {
            EndpointAddress addr = new EndpointAddress(string.Format("net.tcp://{0}:{1}/{2}", address, port, serviceName));
            var binding = new NetTcpBinding(SecurityMode.None);

            return new StableLoggerNetClient(binding, addr, 16000);
        }

        /// <summary>
        /// Создать клиент логгера на Pipe
        /// </summary>
        /// <param name="address">Адрес</param>
        /// <param name="pipeName">Имя пайпа</param>
        /// <returns>Созданный клиент</returns>
        public static StableLoggerNetClient CreateOnPipe(string address, string pipeName = "LoggingService")
        {
            EndpointAddress addr = new EndpointAddress(string.Format("net.pipe://{0}/{1}", address, pipeName));
            var binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);

            return new StableLoggerNetClient(binding, addr, 16000);
        }


        // =======================

        private string _crEnpointConfigName;
        private System.ServiceModel.EndpointAddress _crRemoteAddr;
        private System.ServiceModel.Channels.Binding _crBinding;

        private LoggerNetClient _curClient;

        private Thread _connectToLogServerThread;
        private CancellationTokenSource _procStopTokenSource = new CancellationTokenSource();
        private object _syncObj = new object();

        private readonly int _connectionTestTimeMsMin;
        private readonly int _connectionTestTimeMsMax;

        /// <summary>
        /// Конструктор StableLoggerNetClient
        /// </summary>
        /// <param name="endpointConfigurationName">Имя конфигурации Endpoint</param>
        /// <param name="connectionTestTimeMs">Период проверки наличия соединения</param>
        public StableLoggerNetClient(string endpointConfigurationName, int connectionTestTimeMs)
        {
            _crEnpointConfigName = endpointConfigurationName;

            _connectionTestTimeMsMin = Math.Max(500, connectionTestTimeMs / 32);
            _connectionTestTimeMsMax = connectionTestTimeMs;
        }
        /// <summary>
        /// Конструктор StableLoggerNetClient
        /// </summary>
        /// <param name="endpointConfigurationName">Имя конфигурации Endpoint</param>
        /// <param name="remoteAddress">Адрес</param>
        /// <param name="connectionTestTimeMs">Период проверки наличия соединения</param>
        public StableLoggerNetClient(string endpointConfigurationName, string remoteAddress, int connectionTestTimeMs) 
        {
            _crEnpointConfigName = endpointConfigurationName;
            _crRemoteAddr = new System.ServiceModel.EndpointAddress(remoteAddress);

            _connectionTestTimeMsMin = Math.Max(500, connectionTestTimeMs / 32);
            _connectionTestTimeMsMax = connectionTestTimeMs;
        }
        /// <summary>
        /// Конструктор StableLoggerNetClient
        /// </summary>
        /// <param name="endpointConfigurationName">Имя конфигурации Endpoint</param>
        /// <param name="remoteAddress">Адрес</param>
        /// <param name="connectionTestTimeMs">Период проверки наличия соединения</param>
        public StableLoggerNetClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress, int connectionTestTimeMs)
        {
            _crEnpointConfigName = endpointConfigurationName;
            _crRemoteAddr = remoteAddress;

            _connectionTestTimeMsMin = Math.Max(500, connectionTestTimeMs / 32);
            _connectionTestTimeMsMax = connectionTestTimeMs;
        }
        /// <summary>
        /// Конструктор StableLoggerNetClient
        /// </summary>
        /// <param name="binding">binding</param>
        /// <param name="remoteAddress">Адрес</param>
        /// <param name="connectionTestTimeMs">Период проверки наличия соединения</param>
        public StableLoggerNetClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress, int connectionTestTimeMs)
        {
            _crBinding = binding;
            _crRemoteAddr = remoteAddress;

            _connectionTestTimeMsMin = Math.Max(500, connectionTestTimeMs / 32);
            _connectionTestTimeMsMax = connectionTestTimeMs;
        }

        /// <summary>
        /// Имя RemoteSide
        /// </summary>
        public string RemoteSideName
        {
            get
            {
                if (_crRemoteAddr != null)
                    return _crRemoteAddr.Uri.ToString();

                return _crEnpointConfigName;
            }
        }

        /// <summary>
        /// Установлено ли соединение в данный момент
        /// </summary>
        public bool HasConnection
        {
            get {
                var localClient = _curClient;
                return 
                    localClient != null && 
                    localClient.State == System.ServiceModel.CommunicationState.Opened;
            }
        }
        
        /// <summary>
        /// Запущен ли
        /// </summary>
        public bool IsStarted
        {
            get
            {
                return _connectToLogServerThread != null;
            }
        }

        /// <summary>
        /// Залогировать ошибку
        /// </summary>
        /// <param name="ex">Исключение (если есть)</param>
        /// <param name="message">Сообщение</param>
        protected virtual void LogError(Exception ex, string message)
        {

        }
        /// <summary>
        /// Залогировать предупреждение
        /// </summary>
        /// <param name="ex">Исключение (если есть)</param>
        /// <param name="message">Сообщение</param>
        protected virtual void LogWarn(Exception ex, string message)
        {

        }

        /// <summary>
        /// Отправить пакет данных
        /// </summary>
        /// <param name="data">Пакет логирования</param>
        public void SendData(Qoollo.Logger.Common.LoggingEvent data)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            var localClient = Volatile.Read(ref _curClient);
            if (localClient == null || localClient.State != System.ServiceModel.CommunicationState.Opened)
                throw new CommunicationException("Connection is not established. Can't send data to Log Server: " + RemoteSideName);

            lock (_syncObj)
            {
                localClient = Volatile.Read(ref _curClient);
                if (localClient == null || localClient.State != System.ServiceModel.CommunicationState.Opened)
                    throw new CommunicationException("Connection is not established. Can't send data to Log Server: " + RemoteSideName);

                _curClient.RemoteSide.SendData(data);
            }
        }


        /// <summary>
        /// Запустить
        /// </summary>
        public void Start()
        {
            if (_connectToLogServerThread != null)
                throw new InvalidOperationException("Network logging client is already started");

            _procStopTokenSource = new CancellationTokenSource();

            _connectToLogServerThread = new Thread(ConnectingToLogServerThreadFunc);
            _connectToLogServerThread.IsBackground = true;
            _connectToLogServerThread.Name = "Network logger connection thread: " + RemoteSideName;

            _connectToLogServerThread.Start();
        }

        /// <summary>
        /// Остановить
        /// </summary>
        public void Stop()
        {
            if (_connectToLogServerThread == null)
                return;


            _procStopTokenSource.Cancel();
            _connectToLogServerThread.Join();

            lock (_syncObj)
            {
                var oldClient = Interlocked.Exchange(ref _curClient, null);
                if (oldClient != null)
                    DestroyClient(oldClient);
            }
        }






        private LoggerNetClient CreateNewClient()
        {
            if (_crBinding != null)
                return new LoggerNetClient(_crBinding, _crRemoteAddr);

            if (_crRemoteAddr != null)
                return new LoggerNetClient(_crEnpointConfigName, _crRemoteAddr);

            return new LoggerNetClient(_crEnpointConfigName);
        }

        private void DestroyClient(LoggerNetClient client)
        {
            if (client == null || client.State == CommunicationState.Closed)
                return;

            try { client.Close(); }
            catch { client.Abort(); }
        }



        private void ConnectingToLogServerThreadFunc()
        {
            var token = _procStopTokenSource.Token;

            bool wasErrorPrinted = false;
            bool isConnected = false;
            int currentConnectionTestTimeMs = _connectionTestTimeMsMin;

            try
            {
                while (!token.IsCancellationRequested)
                {
                    if (_curClient == null || _curClient.State != System.ServiceModel.CommunicationState.Opened)
                    {
                        lock (_syncObj)
                        {
                            if (isConnected)
                            {
                                currentConnectionTestTimeMs = _connectionTestTimeMsMin;
                                isConnected = false;
                            }

                            var newClient = CreateNewClient();
                            try
                            {
                                newClient.Open();
                                wasErrorPrinted = false;
                                isConnected = true;
                            }
                            catch (TimeoutException tmExc)
                            {
                                if (!wasErrorPrinted)
                                {
                                    LogError(tmExc, "Can't connect to log server: " + RemoteSideName);
                                    wasErrorPrinted = true;
                                }
                            }
                            catch (CommunicationException cmExc)
                            {
                                if (!wasErrorPrinted)
                                {
                                    LogError(cmExc, "Can't connect to log server: " + RemoteSideName);
                                    wasErrorPrinted = true;
                                }
                            }

                            if (isConnected)
                            {
                                var oldClient = Interlocked.Exchange(ref _curClient, newClient);
                                if (oldClient != null)
                                    DestroyClient(oldClient);
                            }
                            else
                            {
                                DestroyClient(newClient);
                            }

                            if (isConnected)
                            {
                                currentConnectionTestTimeMs = (_connectionTestTimeMsMax + _connectionTestTimeMsMin) / 2;
                            }
                            else
                            {
                                currentConnectionTestTimeMs *= 2;
                                if (currentConnectionTestTimeMs > _connectionTestTimeMsMax)
                                    currentConnectionTestTimeMs = _connectionTestTimeMsMax;
                            }
                        }
                    }

                    token.WaitHandle.WaitOne(currentConnectionTestTimeMs);
                }
            }
            catch (OperationCanceledException cex)
            {
                if (!token.IsCancellationRequested)
                {
                    LogError(cex, "Unknown error inside connecting to log server: " + RemoteSideName);
                    throw;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Unknown error inside connecting to log server: " + RemoteSideName);
                throw;
            }
        }

        /// <summary>
        /// Освободить ресурсы
        /// </summary>
        public void Dispose()
        {
            Stop();
        }
    }
}
