using Qoollo.Logger.Common;
using Qoollo.Logger.Configuration;
using Qoollo.Logger.Exceptions;
using Qoollo.Logger.Helpers;
using Qoollo.Logger.Net;
using System;
using System.Diagnostics.Contracts;
using System.ServiceModel;

namespace Qoollo.Logger.Writers
{
    /// <summary>
    /// PipeWriter. Ресурс для отправки сообщения по пайпу на локальный сервер сбора логов
    /// </summary>
    internal class PipeWriter : Writer
    {
        private static readonly Logger _thisClassSupportLogger = InnerSupportLogger.Instance.GetClassLogger(typeof(PipeWriter));

        private readonly InternalStableLoggerNetClient _writer;

        private readonly object _lockWrite = new object();
        private readonly LogLevel _logLevel;

        private ErrorTimeTracker _errorTracker = new ErrorTimeTracker(TimeSpan.FromMinutes(5));
        private volatile bool _isDisposed = false;


        public PipeWriter(PipeWriterConfiguration config)
            : base(config.Level)
        {
            Contract.Requires(config != null);

            _logLevel = config.Level;
            _writer = InternalStableLoggerNetClient.CreateOnPipeInternal(config.ServerName, config.PipeName);
            _writer.Start();
        }

        /// <summary>
        /// Отправка события логгера по сети
        /// </summary>
        public override bool Write(LoggingEvent data)
        {
            if (_isDisposed)
            {
                if (_errorTracker.CanWriteErrorGetAndUpdate())
                    _thisClassSupportLogger.Error("Attempt to write LoggingEvent in Disposed state");
                return false;
            }

            if (data.Level < _logLevel)
                return true;

            bool result = false;

            try
            {
                lock (_lockWrite)
                {
                    if (!_writer.IsStarted)
                        _writer.Start();

                    if (!_writer.HasConnection)
                    {
                        if (_errorTracker.CanWriteErrorGetAndUpdate())
                            _thisClassSupportLogger.Error("Connection to Pipe logger server is not established: " + _writer.RemoteSideName);

                        return false;
                    }

                    _writer.SendData(data);
                    result = true;
                }
            }
            catch (CommunicationException ex)
            {
                if (_errorTracker.CanWriteErrorGetAndUpdate())
                    _thisClassSupportLogger.Error(ex, "Error while sending data to Pipe logger server: " + _writer.RemoteSideName);
            }
            catch (TimeoutException ex)
            {
                if (_errorTracker.CanWriteErrorGetAndUpdate())
                    _thisClassSupportLogger.Error(ex, "Error while sending data to Pipe logger server: " + _writer.RemoteSideName);
            }
            catch (Exception ex)
            {
                _thisClassSupportLogger.Error(ex, "Fatal error while sending data to Pipe logger server: " + _writer.RemoteSideName);
                throw new LoggerNetWriteException("Fatal error while sending data to Pipe logger server: " + _writer.RemoteSideName, ex);
            }

            return result;
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
    }
}