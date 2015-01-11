using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qoollo.Logger.Exceptions
{
    /// <summary>
    /// Database writer exception
    /// </summary>
    [Serializable]
    public class LoggerDBWriteException : LoggerException
    {
        /// <summary>
        /// LoggerDBWriteException constructor
        /// </summary>
        /// <param name="message">A message that describes the error</param>
        public LoggerDBWriteException(string message) : base(message) { }
        /// <summary>
        /// LoggerDBWriteException constructor
        /// </summary>
        /// <param name="message">A message that describes the error</param>
        /// <param name="innerException">The exception that is the cause of the current exception</param>
        public LoggerDBWriteException(string message, Exception innerException) : base(message, innerException) { }
    }
}
