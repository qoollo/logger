using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qoollo.Logger.Exceptions
{
    /// <summary>
    /// Ошибка конфигации логгера.
    /// </summary>
    [Serializable]
    public class LoggerConfigurationException : LoggerException
    {
        /// <summary>
        /// Конструктор LoggerConfigurationException
        /// </summary>
        /// <param name="message">Сообщение</param>
        public LoggerConfigurationException(string message) : base(message) { }
        /// <summary>
        /// Конструктор LoggerConfigurationException
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="innerException">Внутреннее исключение</param>
        public LoggerConfigurationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
