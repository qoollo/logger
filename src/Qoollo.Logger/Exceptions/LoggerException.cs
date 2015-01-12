using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Qoollo.Logger
{
    /// <summary>
    /// Base class for logger exceptions
    /// </summary>
    [Serializable]
    public class LoggerException : ApplicationException
    {
        /// <summary>
        /// LoggerException constructor
        /// </summary>
        public LoggerException() { }
        /// <summary>
        /// LoggerException constructor
        /// </summary>
        /// <param name="message">A message that describes the error</param>
        public LoggerException(string message) : base(message) { }
        /// <summary>
        /// LoggerException constructor
        /// </summary>
        /// <param name="message">A message that describes the error</param>
        /// <param name="innerException">The exception that is the cause of the current exception</param>
        public LoggerException(string message, Exception innerException) : base(message, innerException) { }
        /// <summary>
        /// LoggerException constructor
        /// </summary>
        /// <param name="info">info</param>
        /// <param name="context">context</param>
        protected LoggerException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
