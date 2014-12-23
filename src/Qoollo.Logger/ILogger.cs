using Qoollo.Logger.Common;
using Qoollo.Logger.LoggingEventConverters;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Qoollo.Logger
{
    /// <summary>
    /// Интерфейс для писателя сообщений лога
    /// </summary>
    [ContractClass(typeof(ILoggingEventWriterContract))]
    public interface ILoggingEventWriter : IDisposable
    {
        /// <summary>
        /// Устанавливает фабрику для создания конвертеров,
        /// необходимых для преобразования логируемых данных в строки для вывода в файл или консоль
        /// </summary>
        /// <param name="factory"></param>
        void SetConverterFactory(ConverterFactory factory);

        /// <summary>
        /// Логирование
        /// </summary>
        /// <param name="data"></param>
        bool Write(LoggingEvent data);
    }



    /// <summary>
    /// Общий интерфейс логгера
    /// </summary>
    public interface ILogger : ILoggingEventWriter
    {
        /// <summary>
        /// Возвращает уровень логирования
        /// </summary>
        LogLevel Level { get; }

        /// <summary>
        /// Разрешено ли извлекать расширенную информацию об источнике логирования
        /// </summary>
        bool AllowStackTraceInfoExtraction { get; }


        /// <summary>
        /// Возвращает цепочку вложенности программных модулей
        /// </summary>
        /// <returns>Список модулей. Первый элемент - внутренний модуль, последний - внешний.</returns>
        List<string> GetStackSources();

        /// <summary>
        /// Обновление цепочки вложенности программных модулей (StackSources) 
        /// </summary>
        void Refresh();
    }





    [ContractClassFor(typeof(ILoggingEventWriter))]
    internal abstract class ILoggingEventWriterContract : ILoggingEventWriter
    {
        void ILoggingEventWriter.SetConverterFactory(ConverterFactory factory)
        {
            Contract.Requires(factory != null);
        }

        bool ILoggingEventWriter.Write(LoggingEvent data)
        {
            Contract.Requires(data != null);
            throw new NotImplementedException();
        }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }
    }     
}