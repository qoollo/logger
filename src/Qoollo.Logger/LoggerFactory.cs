using System;
using System.Diagnostics.Contracts;
using Qoollo.Logger.Common;
using Qoollo.Logger.Configuration;
using Qoollo.Logger.Writers;
using System.Collections.Generic;

namespace Qoollo.Logger
{
    /// <summary>
    /// Factory to create instance of logger by configuration
    /// </summary>
    public static class LoggerFactory
    {
        private static readonly Dictionary<Type, Func<LogWriterWrapperConfiguration, ILoggingEventWriter>> _userFactories = new Dictionary<Type, Func<LogWriterWrapperConfiguration, ILoggingEventWriter>>();

        /// <summary>
        /// Register custom user factory
        /// </summary>
        /// <typeparam name="TConfig">Type of configuration</typeparam>
        /// <param name="factory">Delegate that creates writer/wrapper by configuration</param>
        private static void RegisterCustomFactoryInner<TConfig>(Func<TConfig, ILoggingEventWriter> factory) where TConfig: LogWriterWrapperConfiguration
        {
            Contract.Requires<ArgumentNullException>(factory != null);

            lock (_userFactories)
            {
                _userFactories.Add(typeof(TConfig), (cfg) =>
                {
                    if (!(cfg is TConfig))
                        return null;

                    return factory((TConfig)cfg);
                });
            }
        }
        /// <summary>
        /// Removes registration of custom user factory
        /// </summary>
        /// <typeparam name="TConfig">Type of configuration</typeparam>
        /// <returns>true if the element is successfully found and removed</returns>
        private static bool UnregisterCustomFactoryInner<TConfig>() where TConfig : LogWriterWrapperConfiguration
        {
            lock (_userFactories)
            {
                return _userFactories.Remove(typeof(TConfig));
            }
        }

        /// <summary>
        /// Register custom user factory to create wrapper
        /// </summary>
        /// <typeparam name="TConfig">Type of wrapper configuration</typeparam>
        /// <param name="factory">Delegate that creates wrapper by configuration</param>
        public static void RegisterWrapperFactory<TConfig>(Func<TConfig, ILoggingEventWriter> factory) where TConfig: CustomWrapperConfiguration
        {
            RegisterCustomFactoryInner<TConfig>(factory);
        }
        /// <summary>
        /// Register custom user factory to create writer
        /// </summary>
        /// <typeparam name="TConfig">Type of writer configuration</typeparam>
        /// <param name="factory">Delegate that creates writer by configuration</param>
        public static void RegisterWriterFactory<TConfig>(Func<TConfig, ILoggingEventWriter> factory) where TConfig : CustomWriterConfiguration
        {
            RegisterCustomFactoryInner<TConfig>(factory);
        }

        /// <summary>
        /// Removes registration of custom user factory
        /// </summary>
        /// <typeparam name="TConfig">Type of wrapper configuration</typeparam>
        /// <returns>true if the element is successfully found and removed</returns>
        public static bool UnregisterWrapperFactory<TConfig>() where TConfig : CustomWrapperConfiguration
        {
            return UnregisterCustomFactoryInner<TConfig>();
        }
        /// <summary>
        /// Removes registration of custom user factory
        /// </summary>
        /// <typeparam name="TConfig">Type of writer configuration</typeparam>
        /// <returns>true if the element is successfully found and removed</returns>
        public static bool UnregisterWriterFactory<TConfig>() where TConfig : CustomWriterConfiguration
        {
            return UnregisterCustomFactoryInner<TConfig>();
        }



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
        /// Create custom writer/wrapper if possible
        /// </summary>
        /// <param name="config">Writer or Wrapper configuration</param>
        /// <returns>Created wrapper of writer or Null</returns>
        private static ILoggingEventWriter CreateCustomWriter(LogWriterWrapperConfiguration config)
        {
            Contract.Requires(config != null);

            lock (_userFactories)
            {
                Func<LogWriterWrapperConfiguration, ILoggingEventWriter> factoryByType = null;
                if (_userFactories.TryGetValue(config.GetType(), out factoryByType))
                    return factoryByType(config);

                foreach (var factory in _userFactories)
                {
                    var tmp = factory.Value(config);
                    if (tmp != null)
                        return tmp;
                }
            }

            return null;
        }

        /// <summary>
        /// Create log Writer or Wrapper by its configuration
        /// </summary>
        /// <param name="config">Writer or Wrapper configuration</param>
        /// <returns>Created wrapper of writer</returns>
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

                case WriterTypeEnum.CustomWrapper:
                case WriterTypeEnum.CustomWriter:
                    var result = CreateCustomWriter(config);
                    if (result == null)
                        throw new ArgumentException("Unknown type of custom logger Writer/Wrapper configuration: " + config.GetType().Name);
                    return result;

                default:
                    throw new ArgumentException("Unknown type of logger Writer/Wrapper configuration: " + config.WriterType.ToString());
            }
        }
    }
}