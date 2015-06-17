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
using Qoollo.Logger.Helpers;
using Qoollo.Logger.Net;

namespace Qoollo.Logger.Writers.RealWriters.Helpers
{
    internal class TcpHelper: IDisposable
    {
        private static readonly Logger _thisClassSupportLogger = InnerSupportLogger.Instance.GetClassLogger(typeof(TcpHelper));

        private readonly string _address;
        private readonly int _port;

        private TcpClient _curClient;
        private Thread _connectionThread;

        private CancellationTokenSource _procStopTokenSource = new CancellationTokenSource();
        private object _syncObj = new object();

        private readonly int _connectionTestTimeMsMin;
        private readonly int _connectionTestTimeMsMax;

        private volatile bool _wasReconnected = false;

        public TcpHelper(string address, int port, int timeoutMs)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(address));
            Contract.Requires<ArgumentOutOfRangeException>(port > 0 && port < 65535);
            Contract.Requires<ArgumentOutOfRangeException>(timeoutMs > 0);


            _address = address;
            _port = port;

            _connectionTestTimeMsMin = Math.Max(500, timeoutMs / 32);
            _connectionTestTimeMsMax = timeoutMs;
      
        }


        /// <summary>
        /// Remoteside full address
        /// </summary>
        public string RemoteSideName
        {
            get
            {
                return _address + ":" + _port.ToString();
            }
        }

        /// <summary>
        /// Is there an active connectino right now
        /// </summary>
        public bool HasConnection
        {
            get
            {
                var localClient = Volatile.Read(ref _curClient);
                return localClient != null && localClient.Connected;
            }
        }

        /// <summary>
        /// Запущен ли клиент
        /// </summary>
        public bool IsStarted
        {
            get
            {
                return _connectionThread != null;
            }
        }

        /// <summary>
        /// Indicates, if reconnection was done
        /// </summary>
        public bool WasReconnected { get { return _wasReconnected; } }

        /// <summary>
        /// Mark, that reconnection was done by external code
        /// </summary>
        public void MarkReconnectedWasProcessed()
        {
            _wasReconnected = false;
        }


        /// <summary>
        /// Start connection lifecycle
        /// </summary>
        public void Start()
        {
            if (_connectionThread != null)
                throw new InvalidOperationException("Network client is already started");

            _procStopTokenSource = new CancellationTokenSource();

            _connectionThread = new Thread(ConnectionWorker);
            _connectionThread.IsBackground = true;
            _connectionThread.Name = "Logger TcpHelper connection thread: " + RemoteSideName;

            _connectionThread.Start();
        }

        /// <summary>
        /// Stop
        /// </summary>
        public void Stop()
        {
            if (_connectionThread == null)
                return;

            _procStopTokenSource.Cancel();
            _connectionThread.Join();

            lock (_syncObj)
            {
                var oldClient = Interlocked.Exchange(ref _curClient, null);
                if (oldClient != null)
                    DestroyClient(oldClient);
            }
        }



        private void DestroyClient(TcpClient client)
        {
            if (client == null)
                return;

            try { client.Close(); }
            catch { }
        }


        private void ConnectionWorker()
        {
            var token = _procStopTokenSource.Token;

            bool isConnected = false;
            int currentConnectionTestTimeMs = _connectionTestTimeMsMin;

            try
            {
                while (!token.IsCancellationRequested)
                {
                    if (_curClient == null || !_curClient.Connected)
                    {
                        lock (_syncObj)
                        {
                            if (isConnected)
                            {
                                currentConnectionTestTimeMs = _connectionTestTimeMsMin;
                                isConnected = false;
                            }


                            var newClient = new TcpClient();
                            try
                            {
                                newClient.Connect(_address, _port);
                                isConnected = true;
                            }
                            catch (SocketException ex)
                            {
                                _thisClassSupportLogger.Error(ex, "Logger tcpHelper threw socket exception");
                            }

                            if (isConnected)
                            {
                                var oldClient = Interlocked.Exchange(ref _curClient, newClient);
                                if (oldClient != null)
                                    DestroyClient(oldClient);
                                _wasReconnected = true;
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
            catch (OperationCanceledException)
            {
                if (!token.IsCancellationRequested)
                    throw;
            }
        }

    
        public bool Write(string data)
        {         
            if (data == null)
                throw new ArgumentNullException("data");

            var localClient = Volatile.Read(ref _curClient);
            if (localClient == null || !localClient.Connected)
                throw new CommunicationException("Connection is not established. Can't perform Write for tcp Server: " + RemoteSideName);

            lock (_syncObj)
            {
                localClient = Volatile.Read(ref _curClient);
                if (localClient == null || !localClient.Connected)
                    throw new CommunicationException("Connection is not established. Can't perform Write for tcp Server: " + RemoteSideName);

                try
                {
                    WriteToStream(localClient.GetStream(), data);
                }
                catch (SocketException sExc)
                {
                    throw new CommunicationException("Network error while sending data to remote TCP Server: " + RemoteSideName, sExc);
                }
                catch (IOException ioExc)
                {
                    throw new CommunicationException("Network error while sending data to remote TCP Server: " + RemoteSideName, ioExc);
                }

                return true;
            }
        }


        private void WriteToStream(NetworkStream stream, string data)
        {
            var writer = new StreamWriter(stream);
            writer.Write(data);

            writer.Flush();
        }


        /// <summary>
        /// Close connection and clean-up all resources
        /// </summary>
        /// <param name="isUserCall">Is called by user</param>
        protected void Dispose(bool isUserCall)
        {
            if (isUserCall)
            {
                this.Stop();
            }
            else
            {
                if (_procStopTokenSource != null && !_procStopTokenSource.IsCancellationRequested)
                    _procStopTokenSource.Cancel();
            }
        }

        /// <summary>
        /// Special method to finalize object from owner
        /// </summary>
        internal void FinalizeFast()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Close connection and clean-up all resources
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
