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

namespace Qoollo.Logger.Writers
{
   
    internal class TcpWriter: Writer, IDisposable
    {
        private static readonly Logger _thisClassSupportLogger = InnerSupportLogger.Instance.GetClassLogger(typeof(TcpWriter));
        private readonly InternalStableLoggerNetClient _writer;

        private readonly string _address;
        private readonly string _serverName;
        private readonly int _port;
        private readonly object _lockWrite = new object();
        private readonly LogLevel _logLevel;

        private ErrorTimeTracker _errorTracker = new ErrorTimeTracker(TimeSpan.FromMinutes(5));
        private volatile bool _isDisposed = false;


        private TcpClient _curClient;

        private Thread _connectToPerfCountersServerThread;
        private CancellationTokenSource _procStopTokenSource = new CancellationTokenSource();
        private object _syncObj = new object();

        private readonly int _connectionTestTimeMsMin;
        private readonly int _connectionTestTimeMsMax;

        private volatile bool _wasReconnected = false;

        public TcpWriter(TcpWriterConfiguration config)
            : base(config.Level)
        {
            if (config.ServerAddress == null)
                throw new ArgumentNullException("ServerAddress");
            
            _address = config.ServerAddress;
            _port = config.Port;

            _connectionTestTimeMsMin = Math.Max(500, config.ConnectionTestTimeMs / 32);
            _connectionTestTimeMsMax = config.ConnectionTestTimeMs;

            Start();
        }


        /// <summary>
        /// Имя удалённой стороны
        /// </summary>
        public string RemoteSideName
        {
            get
            {
                return _address + ":" + _port.ToString();
            }
        }

        /// <summary>
        /// Есть ли в данный момент соединение
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
                return _connectToPerfCountersServerThread != null;
            }
        }

        /// <summary>
        /// Было ли пересоздано подключение
        /// </summary>
        public bool WasReconnected { get { return _wasReconnected; } }
        /// <summary>
        /// Пометить, что переподключение было обработано внешним кодом
        /// </summary>
        public void MarkReconnectedWasProcessed()
        {
            _wasReconnected = false;
        }




        /// <summary>
        /// Запустить
        /// </summary>
        public void Start()
        {
            if (_connectToPerfCountersServerThread != null)
                throw new InvalidOperationException("Performance counters Graphite network client is already started");

            _procStopTokenSource = new CancellationTokenSource();

            _connectToPerfCountersServerThread = new Thread(ConnectionWorker);
            _connectToPerfCountersServerThread.IsBackground = true;
            _connectToPerfCountersServerThread.Name = "GraphiteCounters connection thread: " + RemoteSideName;

            _connectToPerfCountersServerThread.Start();
        }

        /// <summary>
        /// Остановить
        /// </summary>
        public void Stop()
        {
            if (_connectToPerfCountersServerThread == null)
                return;

            _procStopTokenSource.Cancel();
            _connectToPerfCountersServerThread.Join();

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
                                _thisClassSupportLogger.Error(ex, "TcpWriter threw socket exception");
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
                            _writer.Dispose();
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
            if (data == null)
            {
                throw new ArgumentNullException("LoggingEvent data");
            }

            var localClient = Volatile.Read(ref _curClient);
            if (localClient == null || !localClient.Connected)
            {
                _thisClassSupportLogger.Error("Connection is not established. Can't perform Write for tcp Server: " + RemoteSideName);
                return false;
            }
            lock (_syncObj)
            {
                localClient = Volatile.Read(ref _curClient);
                if (localClient == null || !localClient.Connected)
                {
                    _thisClassSupportLogger.Error("Connection is not established. Can't perform Write for tcp Server: " + RemoteSideName);
                    return false;
                }

                try
                {
                    WriteToStream(localClient.GetStream(), DateTime.Now, data);
                }
                catch (SocketException sExc)
                {
                    _thisClassSupportLogger.Error(sExc, "Connection is not established. Can't perform Write for tcp Server: " + RemoteSideName);
                    return false;
                }
                catch (IOException ioExc)
                {
                    _thisClassSupportLogger.Error(ioExc, "Network error. Can't perform Write for tcp Server: " + RemoteSideName);
                    return false;
                }

                return true;
            }
        }


        private void WriteToStream(NetworkStream stream, DateTime time, LoggingEvent log)
        {
            var writer = new StreamWriter(stream);
            writer.Write(ConvertToString(time, log));

            writer.Flush();
        }

        protected string ConvertToString(DateTime time, LoggingEvent log)
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
