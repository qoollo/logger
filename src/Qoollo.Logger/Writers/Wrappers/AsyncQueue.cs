using Qoollo.Logger.Common;
using Qoollo.Logger.Configuration;
using Qoollo.Logger.Writers.Wrappers.Helpers.QueueAsyncProcessing;
using System;
using System.Diagnostics.Contracts;
using System.Threading;

namespace Qoollo.Logger.Writers
{
    /// <summary>
    /// AsyncQueue. В эту обертку оборачивается ресурс (Writer) если нужна поддержка ассинхронности
    /// </summary>
    internal class AsyncQueue : QueueAsyncProcessor<LoggingEvent>, ILoggingEventWriter
    {
        private static readonly Logger _thisClassSupportLogger = InnerSupportLogger.Instance.GetClassLogger(typeof(AsyncQueue));

        private readonly ILoggingEventWriter _logger;

        // Переполнена ли очередь
        private int _isOverflowed;

        // Граница переполнения
        private readonly int _borderOverflow;

        // Константы для замены bool на int для поодержки Interlock опереаций
        private const int IS_OVERFLOWED = 1;
        private const int IS_NOT_OVERFLOWED = 0;

        // Выбрасывать ли из очереди не влезающие сообщения
        private readonly bool _isDiscardExcess;

        private volatile bool _isDisposed = false;

        public AsyncQueue(AsyncQueueWrapperConfiguration config, ILoggingEventWriter logger)
            : base(1, config.MaxQueueSize, "AsyncQueue for logger", true)
        {
            Contract.Requires(config != null);
            Contract.Requires(logger != null);

            _logger = logger;
            
            // конфиги для обработки переполнения очереди
            _isOverflowed = IS_NOT_OVERFLOWED;
            _borderOverflow = Convert.ToInt32(config.MaxQueueSize * 0.5);
            _isDiscardExcess = config.IsDiscardExcess;

            Start();
        }

        public AsyncQueue(AsyncQueueWrapperConfiguration config)
            : this(config, LoggerFactory.CreateWriter(config.InnerWriter))
        {
        }




        public void SetConverterFactory(LoggingEventConverters.ConverterFactory factory)
        {
            _logger.SetConverterFactory(factory);
        }


        public bool Write(LoggingEvent data)
        {
            if (_isDisposed)
            {
                _thisClassSupportLogger.Error("Attempt to write LoggingEvent in Disposed state");
                return false;
            }

            if (data.Level.Level.CompareTo(LogLevel.Error.Level) >= 0)
            {
                _logger.Write(data);
            }
            else
            {
                if (TryAdd(data))
                {
                    if (ElementCount < _borderOverflow
                        && Interlocked.CompareExchange(ref _isOverflowed, IS_NOT_OVERFLOWED, IS_OVERFLOWED) == IS_OVERFLOWED)
                    {
                        _thisClassSupportLogger.Info("Async queue now is not overflowed");
                    }
                }
                else
                {
                    if (Interlocked.CompareExchange(ref _isOverflowed, IS_OVERFLOWED, IS_NOT_OVERFLOWED) == IS_NOT_OVERFLOWED)
                    {
                        _thisClassSupportLogger.Info("Async queue overflow detected");
                    }

                    if (_isDiscardExcess)
                    {
                        return false;
                    }

                    Add(data);
                }
            }

            return true;
        }


        protected override void Process(LoggingEvent element, object state, CancellationToken token)
        {
            _logger.Write(element);
            //Console.WriteLine(this.ElementCount);
        }


        protected void Dispose(DisposeReason reason)
        {
            if (!_isDisposed)
            {
                _isDisposed = true;

                if (reason == DisposeReason.Dispose)
                    _logger.Dispose();
                else if (reason == DisposeReason.Close)
                    _logger.Close();
            }
        }

        public void Close()
        {
            this.Stop(true, true, false);
            Dispose(DisposeReason.Close);
        }

        /// <summary>
        /// Main clean-up code
        /// </summary>
        /// <param name="isUserCall">Is called by user. False - from finalizer</param>
        protected override void Dispose(bool isUserCall)
        {
            base.Dispose(isUserCall);
            if (isUserCall)
                this.Dispose(DisposeReason.Dispose);
            else
                this.Dispose(DisposeReason.Finalize);
        }
    }
}
