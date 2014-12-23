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
                _thisClassSupportLogger.Error("Попытка записи логирующего сообщения при освобожденных ресурсах");
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
                        _thisClassSupportLogger.Info("Освобождение очереди для поддержания ассинхронности");
                    }
                }
                else
                {
                    if (Interlocked.CompareExchange(ref _isOverflowed, IS_OVERFLOWED, IS_NOT_OVERFLOWED) == IS_NOT_OVERFLOWED)
                    {
                        _thisClassSupportLogger.Info("Переполнение очереди для поддержания ассинхронности");
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


        /// <summary>
        /// Основной код освобождения ресурсов
        /// </summary>
        /// <param name="isUserCall">Вызвано ли освобождение пользователем. False - деструктор</param>
        protected override void Dispose(bool isUserCall)
        {
            if (!_isDisposed)
            {
                _isDisposed = true;

                if (isUserCall)
                    _logger.Dispose();
            }

            base.Dispose(isUserCall);
        }
    }
}
