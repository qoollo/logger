using System;
using System.Diagnostics.Contracts;
using Qoollo.Logger.Common;
using Qoollo.Logger.Configuration;
using Qoollo.Logger.Writers;

namespace Qoollo.Logger
{
    /// <summary>
    /// Фабрика, с помощью которой можно создать полноценный логгер или внутренний логгер (writer)
    /// </summary>
    public static class LoggerFactory
    {
        /// <summary>
        /// Создаёт уже обернутый логгер для модуля с именем moduleName (уже готовый для логирования)
        /// </summary>
        /// <param name="moduleName">Имя модуля</param>
        /// <param name="sectionName">Имя конфигурационной секции в AppConfig </param>
        /// <returns>Логгер</returns>
        public static Logger CreateLoggerFromAppConfig(string moduleName, string sectionName)
        {
            Contract.Requires<ArgumentNullException>(moduleName != null, "moduleName");
            Contract.Requires<ArgumentNullException>(sectionName != null, "sectionName");

            LoggerConfiguration config = Configurator.LoadConfiguration(sectionName);

            Contract.Assume(config != null);
            if (config == null)
                throw new ArgumentNullException("Ошбика конфигурирования логгера");

            return CreateLogger(moduleName, config);
        }

        /// <summary>
        /// Создаёт уже обернутый логгер для модуля с именем moduleName (уже готовый для логирования)
        /// </summary>
        /// <param name="moduleName">Имя модуля</param>
        /// <param name="sectionGroupName">Имя группы секций</param>
        /// <param name="sectionName">Имя конфигурационной секции в AppConfig </param>
        /// <returns>Логгер</returns>
        public static Logger CreateLoggerFromAppConfig(string moduleName, string sectionGroupName, string sectionName)
        {
            Contract.Requires<ArgumentNullException>(moduleName != null);
            Contract.Requires<ArgumentNullException>(sectionGroupName != null);
            Contract.Requires<ArgumentNullException>(sectionName != null);

            LoggerConfiguration config = Configurator.LoadConfiguration(sectionGroupName, sectionName);

            Contract.Assume(config != null);
            if (config == null)
                throw new ArgumentNullException("Ошбика конфигурирования логгера");

            return CreateLogger(moduleName, config);
        }

        /// <summary>
        /// Создаёт уже обернутый логгер для модуля с именем moduleName (уже готовый для логирования)
        /// </summary>
        /// <param name="configuration">Конфигурация логгера</param>
        /// <param name="moduleName">Имя модуля </param>
        /// <returns>Логгер</returns>
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
        /// Создает определенный Writer
        /// </summary>
        /// <param name="config">Конфигурация логгера</param>
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
                    throw new NotImplementedException("Не известный тип ресурса для записи логов. Нет возможности произвести инстанцирование");
            }
        }
    }
}