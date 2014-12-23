using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qoollo.Logger.Exceptions
{
    /// <summary>
    /// Исключение разбора шаблона сообщения логирования
    /// </summary>
    public class LoggerMessageTemplateParsingException : LoggerConfigurationException
    {
        /// <summary>
        /// Конструктор LoggerMessageTemplateParsingException
        /// </summary>
        /// <param name="message">Сообщение</param>
        public LoggerMessageTemplateParsingException(string message) : base(message) { }
        /// <summary>
        /// Конструктор LoggerMessageTemplateParsingException
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="innerException">Внутреннее исключение</param>
        public LoggerMessageTemplateParsingException(string message, Exception innerException) : base(message, innerException) { }
    }
}
