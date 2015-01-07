using System;
using System.Diagnostics.Contracts;
using Qoollo.Logger.Common;
using Qoollo.Logger.Configuration;
using Qoollo.Logger.Writers;

namespace Qoollo.Logger
{
    /// <summary>
    /// Factory to create instance of logger by configuration
    /// </summary>
    public static class LoggerFactory
    {
        /// <summary>
        /// Create logger by configuration in AppConfig for module with name 'moduleName'
        /// </summary>
        /// <param name="moduleName">Name of the module</param>
        /// <param name="sectionName">Configuration section name in AppConfig (default: 'LoggerConfigurationSection')</param>
        /// <returns>Created logger</returns>
        public static Logger CreateLoggerFromAppConfig(string moduleName, string sectionName)
        {
            Contract.Requires<ArgumentNullException>(moduleName != null, "moduleName");
            Contract.Requires<ArgumentNullException>(sectionName != null, "sectionName");

            LoggerConfiguration config = Configurator.LoadConfiguration(sectionName);

            Contract.Assume(config != null);
            if (config == null)
                throw new ArgumentNullException("Logger configuration error");

            return CreateLogger(moduleName, config);
        }

        /// <summary>
        /// Create logger by configuration in AppConfig for module with name 'moduleName'
        /// </summary>
        /// <param name="moduleName">Name of the module</param>
        /// <param name="sectionGroupName">SectionGroup name in AppConfig</param>
        /// <param name="sectionName">Configuration section name in AppConfig (default: 'LoggerConfigurationSection')</param>
        /// <returns>Created logger</returns>
        public static Logger CreateLoggerFromAppConfig(string moduleName, string sectionGroupName, string sectionName)
        {
            Contract.Requires<ArgumentNullException>(moduleName != null);
            Contract.Requires<ArgumentNullException>(sectionGroupName != null);
            Contract.Requires<ArgumentNullException>(sectionName != null);

            LoggerConfiguration config = Configurator.LoadConfiguration(sectionGroupName, sectionName);

            Contract.Assume(config != null);
            if (config == null)
                throw new ArgumentNullException("Logger configuration error");

            return CreateLogger(moduleName, config);
        }

        /// <summary>
        /// Create logger by passed configuration for module with name 'moduleName'
        /// </summary>
        /// <param name="configuration">Logger configuration object</param>
        /// <param name="moduleName">Module name</param>
        /// <returns>Created logger</returns>
        public static Logger CreateLogger(string moduleName, LoggerConfiguration configuration)
        {
            Contract.Requires<ArgumentNullException>(moduleName != null, "moduleName");
            Contract.Requires<ArgumentNullException>(configuration != null, "configuration");

            ILoggingEventWriter writer = CreateWriter(configuration.Writer);
            Contract.Assume(writer != null, "writer");

            var wrappedLogger = new Logger(configuration.Level, moduleName, writer, configuration.IsStackTraceEnabled, configuration.IsEnabled);
            Contract.Assume(wrappedLogger != null, "wrappedLogger");

            return wrappedLogger;
        }


        /// <summary>
        /// Create log Writer or Wrapper by its configuration
        /// </summary>
        /// <param name="config">Writer or Wrapper configuration</param>
        internal static ILoggingEventWriter CreateWriter(LogWriterWrapperConfiguration config)
        {
            Contract.Requires<ArgumentNullException>(config != null, "configuration");

            switch (config.WriterType)
            {
                case WriterTypeEnum.EmptyWriter:
                    return EmptyWriter.Instance;

                case WriterTypeEnum.AsyncQueueWrapper:
                    return new AsyncQueue((AsyncQueueWrapperConfiguration)config);

                case WriterTypeEnum.AsyncQueueWithReliableSendingWrapper:
                    return new AsyncQueueWithReliableSending((AsyncReliableQueueWrapperConfiguration)config);

                case WriterTypeEnum.ConsoleWriter:
                    return new ConsoleWriter((ConsoleWriterConfiguration)config);

                case WriterTypeEnum.FileWriter:
                    return new FileWriter((FileWriterConfiguration)config);

                case WriterTypeEnum.DBWriter:
                    return new DBWriter((DatabaseWriterConfiguration)config);

                case WriterTypeEnum.PipeWriter:
                    return new PipeWriter((PipeWriterConfiguration)config);

                case WriterTypeEnum.NetWriter:
                    return new NetWriter((NetWriterConfiguration)config);

                case WriterTypeEnum.GroupWrapper:
                    return new GroupWriter((GroupWrapperConfiguration)config);

                case WriterTypeEnum.RoutingWrapper:
                    return new RoutingWriter((RoutingWrapperConfiguration)config);

                case WriterTypeEnum.PatternMatchingWrapper:
                    return new PatternMatchingWriter((PatternMatchingWrapperConfiguration)config);

                case WriterTypeEnum.ReliableWrapper:
                    return new ReliableWrapper((ReliableWrapperConfiguration)config);

                default:
                    throw new ArgumentException("Unknown type of logger Writer/Wrapper configuration: " + config.WriterType.ToString());
            }
        }
    }
}