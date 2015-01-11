using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qoollo.Logger.Exceptions
{
    /// <summary>
    /// NetWriter exception
    /// </summary>
    [Serializable]
    public class LoggerNetWriteException : LoggerException
    {
        /// <summary>
        /// LoggerNetWriteException constructor
        /// </summary>
        /// <param name="message">A message that describes the error</param>
        public LoggerNetWriteException(string message) : base(message) { }
        /// <summary>
        /// LoggerNetWriteException constructor
        /// </summary>
        /// <param name="message">A message that describes the error</param>
        /// <param name="innerException">The exception that is the cause of the current exception</param>
        public LoggerNetWriteException(string message, Exception innerException) : base(message, innerException) { }
    }
}
