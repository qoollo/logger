using System;
using System.Collections.Generic;

namespace Qoollo.Logger.Writers.Wrappers.Helpers.QueueAsyncProcessing
{
    /// <summary>
    /// Исключение при обработке элемента в QueueAsyncProcessor
    /// </summary>
    [Serializable]
    internal class QueueAsyncProcessorException : Exception
    {
        /// <summary>
        /// Конструктор QueueAsyncProcessorException без параметров
        /// </summary>
        public QueueAsyncProcessorException() { }
        /// <summary>
        /// Конструктор QueueAsyncProcessorException с сообщением об ошибке
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        public QueueAsyncProcessorException(string message) : base(message) { }
        /// <summary>
        /// Конструктор QueueAsyncProcessorException с сообщением об ошибке и внутренним исключением
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="innerExc">Внутреннее исключение</param>
        public QueueAsyncProcessorException(string message, Exception innerExc) : base(message, innerExc) { }
    }
}
