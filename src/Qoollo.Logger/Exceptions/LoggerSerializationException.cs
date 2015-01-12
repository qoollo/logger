using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qoollo.Logger.Exceptions
{
    /// <summary>
    /// Message serialization/deserialization error
    /// </summary>
    [Serializable]
    public class LoggerSerializationException : LoggerException
    {
        /// <summary>
        /// LoggerSerializationException constructor
        /// </summary>
        /// <param name="message">A message that describes the error</param>
        public LoggerSerializationException(string message) : base(message) { }
        /// <summary>
        /// LoggerSerializationException constructor
        /// </summary>
        /// <param name="message">A message that describes the error</param>
        /// <param name="innerException">The exception that is the cause of the current exception</param>
        public LoggerSerializationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
