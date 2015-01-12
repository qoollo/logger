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
    /// Stable network logger client (WCF).
    /// Automatically reopen connection on fault
    /// </summary>
    public class StableLoggerNetClient: INetService, IDisposable
    {
        /// <summary>
        /// Create TCP client
        /// </summary>
        /// <param name="address">Server address</param>
        /// <param name="port">Server TCP port</param>
        /// <param name="serviceName">WCF Service name</param>
        /// <returns>Created client</returns>
        public static StableLoggerNetClient CreateOnTcp(string address, int port, string serviceName = "LoggingService")
        {
            EndpointAddress addr = new EndpointAddress(string.Format("net.tcp://{0}:{1}/{2}", address, port, serviceName));
            var binding = new NetTcpBinding(SecurityMode.None);

            return new StableLoggerNetClient(binding, addr, 16000);
        }

        /// <summary>
        /// Create Pipe client
        /// </summary>
        /// <param name="address">Server address</param>
        /// <param name="pipeName">Pipe name</param>
        /// <returns>Created client</returns>
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
        /// StableLoggerNetClient constructor
        /// </summary>
        /// <param name="endpointConfigurationName">The name of the endpoint in the application configuration file</param>
        /// <param name="connectionTestTimeMs">Max reconnection period in milliseconds</param>
        public StableLoggerNetClient(string endpointConfigurationName, int connectionTestTimeMs)
        {
            _crEnpointConfigName = endpointConfigurationName;

            _connectionTestTimeMsMin = Math.Max(500, connectionTestTimeMs / 32);
            _connectionTestTimeMsMax = connectionTestTimeMs;
        }
        /// <summary>
        /// StableLoggerNetClient constructor
        /// </summary>
        /// <param name="endpointConfigurationName">The name of the endpoint in the application configuration file</param>
        /// <param name="remoteAddress">The address of the service endpoint</param>
        /// <param name="connectionTestTimeMs">Max reconnection period in milliseconds</param>
        public StableLoggerNetClient(string endpointConfigurationName, string remoteAddress, int connectionTestTimeMs) 
        {
            _crEnpointConfigName = endpointConfigurationName;
            _crRemoteAddr = new System.ServiceModel.EndpointAddress(remoteAddress);

            _connectionTestTimeMsMin = Math.Max(500, connectionTestTimeMs / 32);
            _connectionTestTimeMsMax = connectionTestTimeMs;
        }
        /// <summary>
        /// StableLoggerNetClient constructor
        /// </summary>
        /// <param name="endpointConfigurationName">The name of the endpoint in the application configuration file</param>
        /// <param name="remoteAddress">The address of the service endpoint</param>
        /// <param name="connectionTestTimeMs">Max reconnection period in milliseconds</param>
        public StableLoggerNetClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress, int connectionTestTimeMs)
        {
            _crEnpointConfigName = endpointConfigurationName;
            _crRemoteAddr = remoteAddress;

            _connectionTestTimeMsMin = Math.Max(500, connectionTestTimeMs / 32);
            _connectionTestTimeMsMax = connectionTestTimeMs;
        }
        /// <summary>
        /// StableLoggerNetClient constructor
        /// </summary>
        /// <param name="binding">The binding with which to make calls to the service</param>
        /// <param name="remoteAddress">The address of the service endpoint</param>
        /// <param name="connectionTestTimeMs">Max reconnection period in milliseconds</param>
        public StableLoggerNetClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress, int connectionTestTimeMs)
        {
            _crBinding = binding;
            _crRemoteAddr = remoteAddress;

            _connectionTestTimeMsMin = Math.Max(500, connectionTestTimeMs / 32);
            _connectionTestTimeMsMax = connectionTestTimeMs;
        }

        /// <summary>
        /// RemoteSide name
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
        /// Is connection in Opened state
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
        /// Is client started
        /// </summary>
        public bool IsStarted
        {
            get
            {
                return _connectToLogServerThread != null;
            }
        }

        /// <summary>
        /// Log error
        /// </summary>
        /// <param name="ex">Exception (can be null)</param>
        /// <param name="message">User message</param>
        protected virtual void LogError(Exception ex, string message)
        {

        }
        /// <summary>
        /// Log warning
        /// </summary>
        /// <param name="ex">Exception (can be null)</param>
        /// <param name="message">User message</param>
        protected virtual void LogWarn(Exception ex, string message)
        {

        }

        /// <summary>
        /// Sends LoggingEvent to service
        /// </summary>
        /// <param name="data">Logging event</param>
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
        /// Open connection
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
        /// Close connection
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
        /// Close connection and clean-up all resources
        /// </summary>
        public void Dispose()
        {
            Stop();
        }
    }
}
