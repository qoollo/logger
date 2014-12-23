using System;
using Qoollo.Logger.Common;
using Qoollo.Logger.LoggingEventConverters;
using System.Diagnostics.Contracts;

namespace Qoollo.Logger.Writers
{
    /// <summary>
    /// Базовый класс для всех писателей.
    /// От него наследуются классы отвечающие за отправку логов в консоль, в файл и в сеть...
    /// </summary>
    internal abstract class Writer : ILoggingEventWriter
    {
        protected const string CLOSING = "Завершение работы программы.";
        protected const string DISPOSE = "Dispose";

        public Writer(LogLevel level)
        {
            Contract.Requires(level != null);

            Level = level;
        }

        public ConverterFactory ConverterFactory { get; private set; }
        public LogLevel Level { get; private set; }

        public abstract bool Write(LoggingEvent data);


        /// <summary>
        /// Устанавливает фабрику для создания конвертеров,
        /// необходимых для преобразования логируемых данных в строки для вывода в файл или консоль
        /// </summary>
        /// <param name="factory"></param>
        public virtual void SetConverterFactory(ConverterFactory factory)
        {
            ConverterFactory = factory;
        }


        protected virtual void Dispose(bool disposing)
        {
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}