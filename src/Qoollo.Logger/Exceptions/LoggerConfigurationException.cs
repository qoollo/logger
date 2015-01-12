using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qoollo.Logger.Exceptions
{
    /// <summary>
    /// Error in logger configuration
    /// </summary>
    [Serializable]
    public class LoggerConfigurationException : LoggerException
    {
        /// <summary>
        /// LoggerConfigurationException constructor
        /// </summary>
        /// <param name="message">A message that describes the error</param>
        public LoggerConfigurationException(string message) : base(message) { }
        /// <summary>
        /// LoggerConfigurationException constructor
        /// </summary>
        /// <param name="message">A message that describes the error</param>
        /// <param name="innerException">The exception that is the cause of the current exception</param>
        public LoggerConfigurationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
