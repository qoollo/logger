using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qoollo.Logger.Exceptions
{
    /// <summary>
    /// File writer exception
    /// </summary>
    [Serializable]
    public class LoggerFileWriteException : LoggerException
    {
        /// <summary>
        /// LoggerFileWriteException constructor
        /// </summary>
        /// <param name="message">A message that describes the error</param>
        public LoggerFileWriteException(string message) : base(message) { }
        /// <summary>
        /// LoggerFileWriteException constructor
        /// </summary>
        /// <param name="message">A message that describes the error</param>
        /// <param name="innerException">The exception that is the cause of the current exception</param>
        public LoggerFileWriteException(string message, Exception innerException) : base(message, innerException) { }
    }
}
