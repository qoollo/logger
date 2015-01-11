using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qoollo.Logger.Exceptions
{
    /// <summary>
    /// Error during parsing of message template string
    /// </summary>
    public class LoggerMessageTemplateParsingException : LoggerConfigurationException
    {
        /// <summary>
        /// LoggerMessageTemplateParsingException constructor
        /// </summary>
        /// <param name="message">A message that describes the error</param>
        public LoggerMessageTemplateParsingException(string message) : base(message) { }
        /// <summary>
        /// LoggerMessageTemplateParsingException constructor
        /// </summary>
        /// <param name="message">A message that describes the error</param>
        /// <param name="innerException">The exception that is the cause of the current exception</param>
        public LoggerMessageTemplateParsingException(string message, Exception innerException) : base(message, innerException) { }
    }
}
