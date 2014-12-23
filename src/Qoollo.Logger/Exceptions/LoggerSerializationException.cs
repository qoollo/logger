using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qoollo.Logger.Exceptions
{
    /// <summary>
    /// Ошибка конфигации логгера.
    /// </summary>
    [Serializable]
    public class LoggerSerializationException : LoggerException
    {
        /// <summary>
        /// Конструктор LoggerSerializationException
        /// </summary>
        /// <param name="message">Сообщение</param>
        public LoggerSerializationException(string message) : base(message) { }
        /// <summary>
        /// Конструктор LoggerSerializationException
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="innerException">Внутреннее исключение</param>
        public LoggerSerializationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
