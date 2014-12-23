using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qoollo.Logger.Exceptions
{
    /// <summary>
    /// Ошибка записи лога в файл.
    /// </summary>
    [Serializable]
    public class LoggerFileWriteException : LoggerException
    {
        /// <summary>
        /// Конструктор LoggerFileWriteException
        /// </summary>
        /// <param name="message">Сообщение</param>
        public LoggerFileWriteException(string message) : base(message) { }
        /// <summary>
        /// Конструктор LoggerFileWriteException
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="innerException">Внутреннее исключение</param>
        public LoggerFileWriteException(string message, Exception innerException) : base(message, innerException) { }
    }
}
