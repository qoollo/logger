using System;
using Qoollo.Logger.Common;
using Qoollo.Logger.LoggingEventConverters;
using System.Diagnostics.Contracts;

namespace Qoollo.Logger.Writers
{
    /// <summary>
    /// Base class for writers
    /// </summary>
    internal abstract class Writer : ILoggingEventWriter
    {
        protected const string CLOSING = "Application closed normally.";
        protected const string DISPOSE = "Dispose";

        public Writer(LogLevel level)
        {
            Contract.Requires(level != null);

            Level = level;
        }

        public ConverterFactory ConverterFactory { get; private set; }
        public LogLevel Level { get; private set; }


        /// <summary>
        /// Write the log message
        /// </summary>
        /// <param name="data">Log message</param>
        public abstract bool Write(LoggingEvent data);

        /// <summary>
        /// Set the factory to create converters from log message to string.
        /// Required for FileWriter and ConsoleWriter.
        /// </summary>
        /// <param name="factory">Factory to create particular converters</param>
        public virtual void SetConverterFactory(ConverterFactory factory)
        {
            ConverterFactory = factory;
        }


        protected virtual void Dispose(DisposeReason reason)
        {
        }

        /// <summary>
        /// Close writer. All pending messages should be processed
        /// </summary>
        public void Close()
        {
            Dispose(DisposeReason.Close);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Clean-up resources
        /// </summary>
        public void Dispose()
        {
            Dispose(DisposeReason.Dispose);
            GC.SuppressFinalize(this);
        }
    }
}