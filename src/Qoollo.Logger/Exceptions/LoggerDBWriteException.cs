using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qoollo.Logger.Exceptions
{
    /// <summary>
    /// Ошибка записи лога в базу данных.
    /// </summary>
    [Serializable]
    public class LoggerDBWriteException : LoggerException
    {
        /// <summary>
        /// Конструктор LoggerDBWriteException
        /// </summary>
        /// <param name="message">Сообщение</param>
        public LoggerDBWriteException(string message) : base(message) { }
        /// <summary>
        /// Конструктор LoggerDBWriteException
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="innerException">Внутреннее исключение</param>
        public LoggerDBWriteException(string message, Exception innerException) : base(message, innerException) { }
    }
}
