using Qoollo.Logger.Common;
using Qoollo.Logger.Configuration;
using Qoollo.Logger.Exceptions;
using Qoollo.Logger.Helpers;
using Qoollo.Logger.Net;
using System;
using System.Diagnostics.Contracts;
using System.Net.Sockets;
using System.ServiceModel;

namespace Qoollo.Logger.Writers
{
    /// <summary>
    /// NetWriter. Ресурс для отправки сообщения по тсп на сетевой сервер сбора логов
    /// </summary>
    internal class NetWriter : Writer
    {
        private static readonly Logger _thisClassSupportLogger = InnerSupportLogger.Instance.GetClassLogger(typeof(NetWriter));

        private readonly InernalStableLoggerNetClient _writer;

        private readonly string _serverName;
        private readonly int _port;
        private readonly object _lockWrite = new object();
        private readonly LogLevel _logLevel;

        private ErrorTimeTracker _errorTracker = new ErrorTimeTracker(TimeSpan.FromMinutes(5));
        private volatile bool _isDisposed = false;


        public NetWriter(NetWriterConfiguration config)
            : base(config.Level)
        {
            Contract.Requires(config != null);

            _logLevel = config.Level;
            _serverName = config.ServerAddress;
            _port = config.Port;

            _writer = InernalStableLoggerNetClient.CreateOnTcpInternal(_serverName, _port);
            _writer.Start();
        }


        /// <summary>
        /// Отправка события логгера по сети
        /// </summary>
        /// <param name="data"></param>
        public override bool Write(LoggingEvent data)
        {
            if (_isDisposed)
            {
                if (_errorTracker.CanWriteErrorGetAndUpdate())
                    _thisClassSupportLogger.Error("Попытка записи логирующего сообщения при освобожденных ресурсах");

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
                            _thisClassSupportLogger.Error("В данный момент нет подключения к серверу логирования: " + _writer.RemoteSideName);

                        return false;
                    }

                    _writer.SendData(data);
                    result = true;
                }
            }
            catch (SocketException ex)
            {
                if (_errorTracker.CanWriteErrorGetAndUpdate())
                    _thisClassSupportLogger.Error(ex, "Ошибка отправки сообщения на сервер логирования: " + _writer.RemoteSideName);
            }
            catch (CommunicationException ex)
            {
                if (_errorTracker.CanWriteErrorGetAndUpdate())
                    _thisClassSupportLogger.Error(ex, "Ошибка отправки сообщения на сервер логирования: " + _writer.RemoteSideName);
            }
            catch (TimeoutException ex)
            {
                if (_errorTracker.CanWriteErrorGetAndUpdate())
                    _thisClassSupportLogger.Error(ex, "Ошибка отправки сообщения на сервер логирования: " + _writer.RemoteSideName);
            }
            catch (Exception ex)
            {
                _thisClassSupportLogger.Error(ex, "Непоправимая ошибка отправки сообщения на сервер логирования: " + _writer.RemoteSideName);
                throw new LoggerNetWriteException("Непоправимая ошибка отправки сообщения на сервер логирования: " + _writer.RemoteSideName, ex);
            }

            return result;
        }


        protected override void Dispose(bool isUserCall)
        {
            if (!_isDisposed)
            {
                _isDisposed = true;

                lock (_lockWrite)
                {
                    if (_writer != null)
                    {
                        _writer.Dispose();
                    }
                }
            }
        }
    }
}