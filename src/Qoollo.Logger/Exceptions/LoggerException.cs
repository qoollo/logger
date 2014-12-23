using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Qoollo.Logger
{
    /// <summary>
    /// Базовый класс для исключений бросаемых логгером
    /// </summary>
    [Serializable]
    public class LoggerException : ApplicationException
    {
        /// <summary>
        /// Конструктор LoggerException
        /// </summary>
        public LoggerException() { }
        /// <summary>
        /// Конструктор LoggerException
        /// </summary>
        /// <param name="message">Сообщение</param>
        public LoggerException(string message) : base(message) { }
        /// <summary>
        /// Конструктор LoggerException
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="innerException">Внутреннее исключение</param>
        public LoggerException(string message, Exception innerException) : base(message, innerException) { }
        /// <summary>
        /// Конструктор LoggerException для десериализации
        /// </summary>
        /// <param name="info">info</param>
        /// <param name="context">context</param>
        protected LoggerException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
