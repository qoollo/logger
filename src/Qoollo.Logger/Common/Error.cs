using System;
using System.Diagnostics.Contracts;
using System.Runtime.Serialization;

namespace Qoollo.Logger.Common
{
    /// <summary>
    /// Exception data container (contains all information about the exception)
    /// </summary>
    [DataContract]
    public class Error
    {
        /// No parameter constructor
        protected internal Error()
        {
        }

        /// <summary>
        /// Error constructor
        /// </summary>
        /// <param name="type">Type of the Error</param>
        /// <param name="message">A message that describes the error</param>
        /// <param name="source">Error source</param>
        /// <param name="stackTrace">StackTrace</param>
        /// <param name="innerError">Inner error</param>
        public Error(string type, string message, string source, string stackTrace, Error innerError)
        {
            Type = type;
            Message = message;
            Source = source;
            StackTrace = stackTrace;
            InnerError = innerError;
        }

        /// <summary>
        /// Error constructor from Exception object
        /// </summary>
        /// <param name="exception">Exception object</param>
        public Error(Exception exception)
        {
            Contract.Requires(exception != null);

            Type = exception.GetType().FullName;
            Message = exception.Message;
            Source = exception.Source;
            StackTrace = exception.StackTrace;
            
            if (exception.InnerException != null)
                InnerError = new Error(exception.InnerException);
        }

        /// <summary>
        /// Type of the Error
        /// </summary>
        [DataMember(Order = 1)]
        public string Type { get; private set; }

        /// <summary>
        /// A message that describes the error
        /// </summary>
        [DataMember(Order = 2)]
        public string Message { get; private set; }

        /// <summary>
        /// Error source
        /// </summary>
        [DataMember(Order = 3)]
        public string Source { get; private set; }

        /// <summary>
        /// StackTrace from Exception
        /// </summary>
        [DataMember(Order = 4)]
        public string StackTrace { get; private set; }

        /// <summary>
        /// Inner error
        /// </summary>
        [DataMember(Order = 5)]
        public Error InnerError { get; private set; }
    }
}