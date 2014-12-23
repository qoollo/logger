using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qoollo.Logger.Exceptions
{
    /// <summary>
    /// Ошибка передачи лога по сети.
    /// </summary>
    [Serializable]
    public class LoggerNetWriteException : LoggerException
    {
        /// <summary>
        /// Конструктор LoggerNetWriteException
        /// </summary>
        /// <param name="message">Сообщение</param>
        public LoggerNetWriteException(string message) : base(message) { }
        /// <summary>
        /// Конструктор LoggerNetWriteException
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="innerException">Внутреннее исключение</param>
        public LoggerNetWriteException(string message, Exception innerException) : base(message, innerException) { }
    }
}
