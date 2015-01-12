using Qoollo.Logger.Common;
using Qoollo.Logger.LoggingEventConverters;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Qoollo.Logger
{
    /// <summary>
    /// Interface for log writer/wrapper
    /// </summary>
    [ContractClass(typeof(ILoggingEventWriterContract))]
    public interface ILoggingEventWriter : IDisposable
    {
        /// <summary>
        /// Set the factory to create converters from log message to string.
        /// Required for FileWriter and ConsoleWriter.
        /// </summary>
        /// <param name="factory">Factory to create particular converters</param>
        void SetConverterFactory(ConverterFactory factory);

        /// <summary>
        /// Write the log message
        /// </summary>
        /// <param name="data">Log message</param>
        bool Write(LoggingEvent data);
    }



    /// <summary>
    /// Common logger interface
    /// </summary>
    public interface ILogger : ILoggingEventWriter
    {
        /// <summary>
        /// Log level
        /// </summary>
        LogLevel Level { get; }

        /// <summary>
        /// Is stack trace extraction enabled
        /// </summary>
        bool AllowStackTraceInfoExtraction { get; }


        /// <summary>
        /// Returns the chain of modules through which this logger was initialized (logger stacking order)
        /// </summary>
        /// <returns>Chain of modules names. First element - inner module, last - outer.</returns>
        List<string> GetStackSources();

        /// <summary>
        /// Refresh StackSources chain
        /// </summary>
        void Refresh();
    }





    [ContractClassFor(typeof(ILoggingEventWriter))]
    internal abstract class ILoggingEventWriterContract : ILoggingEventWriter
    {
        void ILoggingEventWriter.SetConverterFactory(ConverterFactory factory)
        {
            Contract.Requires(factory != null);
        }

        bool ILoggingEventWriter.Write(LoggingEvent data)
        {
            Contract.Requires(data != null);
            throw new NotImplementedException();
        }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }
    }     
}