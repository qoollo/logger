using Qoollo.Logger.Configuration;
using Qoollo.Logger.Writers.Wrappers.Helpers.TemporaryStore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Qoollo.Logger.Writers
{
    internal class ReliableWrapper : ILoggingEventWriter
    {
        private static readonly Logger _thisClassSupportLogger = InnerSupportLogger.Instance.GetClassLogger(typeof(ReliableWrapper));

        private readonly ILoggingEventWriter _logger;

        private CancellationTokenSource _tokenSource;
        private readonly Thread _readerThread;
        private readonly System.IO.Stream _tempStoreLock;
        private readonly TemporaryStoreReader _reader;
        private readonly TimeSpan _sleepTime = TimeSpan.FromSeconds(10);
        private readonly TemporaryStoreWriter _writer;

        private volatile bool _isDisposed = false;


        internal ReliableWrapper(ReliableWrapperConfiguration config, ILoggingEventWriter logger)
        {
            Contract.Requires(config != null);
            Contract.Requires(logger != null);

            _logger = logger;

            string newFolderForTemporaryStore = TemporaryStoreLockFile.FindNotLockedDirectory(config.FolderForTemporaryStore, out _tempStoreLock);
            _reader = new TemporaryStoreReader(newFolderForTemporaryStore);
            _writer = new TemporaryStoreWriter(newFolderForTemporaryStore, config.MaxFileSize);
            _tokenSource = new CancellationTokenSource();

            _readerThread = new Thread(new ParameterizedThreadStart(TemporaryReaderLoop));
            _readerThread.IsBackground = true;
            _readerThread.Name = "AsyncQueueWithReliableSending (reader loop) for logger";

            _readerThread.Start(_tokenSource.Token);
        }

        public ReliableWrapper(ReliableWrapperConfiguration config)
            : this(config, LoggerFactory.CreateWriter(config.InnerWriter))
        {
        }


        public void SetConverterFactory(LoggingEventConverters.ConverterFactory factory)
        {
            _logger.SetConverterFactory(factory);
        }

        public bool Write(Common.LoggingEvent data)
        {
            if (_isDisposed)
            {
                _thisClassSupportLogger.Error("Attempt to write LoggingEvent in Disposed state");
                return false;
            }

            if (!_logger.Write(data))
            {
                lock (_writer)
                {
                    _writer.Write(data);
                }
            }
            return true;
        }


        private void TemporaryReaderLoop(object state)
        {
            var token = (CancellationToken)state;

            while (!token.IsCancellationRequested)
            {
                try
                {
                    var data = _reader.GetRecord();

                    if (data != null && _logger.Write(data))
                    {
                        _reader.RecordCompleted();
                    }
                    else
                    {
                        token.WaitHandle.WaitOne(_sleepTime);
                    }
                }
                catch (Exception ex)
                {
                    _thisClassSupportLogger.Error(ex, "Error while working with temporary reliable storage of logs");
                    throw;
                }
            }
        }


        /// <summary>
        /// Main clean-up code
        /// </summary>
        /// <param name="reason">Disposing reason</param>
        protected virtual void Dispose(DisposeReason reason)
        {
            if (!_isDisposed)
            {
                _isDisposed = true;

                _tokenSource.Cancel();

                if (_readerThread != null)
                {
                    if (reason != DisposeReason.Finalize)
                        _readerThread.Join();
                }

                if (reason == DisposeReason.Dispose)
                    _logger.Dispose();
                else if (reason == DisposeReason.Close)
                    _logger.Dispose();

                _tempStoreLock.Dispose();
            }
        }

        public void Close()
        {
            Dispose(DisposeReason.Close);
            GC.SuppressFinalize(this);
        }
        public void Dispose()
        {
            Dispose(DisposeReason.Dispose);
            GC.SuppressFinalize(this);
        }
    }
}
