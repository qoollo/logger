using Qoollo.Logger.Common;
using Qoollo.Logger.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qoollo.Logger.Configuration.LoggerConfigurationV2
{
    /// <summary>
    /// Преобразователь форматов конфигураций
    /// </summary>
    internal class ConfigurationFormatConverter
    {
        internal static LoggerConfiguration Convert(ILoggerConfigurationSection section)
        {
            return CreateLoggerConfigRoot(section.Logger);
        }

        // =================


        private static LogLevel ConvertLogLevel(CfgLogLevel src)
        {
            switch (src)
            {
                case CfgLogLevel.TRACE:
                    return LogLevel.Trace;
                case CfgLogLevel.DEBUG:
                    return LogLevel.Debug;
                case CfgLogLevel.INFO:
                    return LogLevel.Info;
                case CfgLogLevel.WARN:
                    return LogLevel.Warn;
                case CfgLogLevel.ERROR:
                    return LogLevel.Error;
                case CfgLogLevel.FATAL:
                    return LogLevel.Fatal;
                default:
                    throw new ArgumentException("src");
            }
        }

        private static AsyncQueueWrapperConfiguration CreateLoggerConfigurationInstance(IAsyncQueueWrapper config)
        {
            var logger = CreateLoggerConfigurationInstanceCommon(config.Logger);
            return new AsyncQueueWrapperConfiguration(config.MaxQueueSize, config.IsDiscardExcess, logger);
        }

        private static AsyncReliableQueueWrapperConfiguration CreateLoggerConfigurationInstance(IAsyncReliableQueueWrapper config)
        {
            var logger = CreateLoggerConfigurationInstanceCommon(config.Logger);
            return new AsyncReliableQueueWrapperConfiguration(config.MaxQueueSize, config.IsDiscardExcess, config.FolderForTemporaryStore, config.MaxFileSize, logger);
        }

        private static ReliableWrapperConfiguration CreateLoggerConfigurationInstance(IReliableWrapper config)
        {
            var logger = CreateLoggerConfigurationInstanceCommon(config.Logger);
            return new ReliableWrapperConfiguration(config.FolderForTemporaryStore, config.MaxFileSize, logger);
        }

        private static ConsoleWriterConfiguration CreateLoggerConfigurationInstance(IConsoleWriter config)
        {
            return new ConsoleWriterConfiguration(ConvertLogLevel(config.LogLevel), config.Template);
        }

        private static FileWriterConfiguration CreateLoggerConfigurationInstance(IFileWriter config)
        {
            bool isNeedFileRotation = config.NeedFileRotation;
            if (isNeedFileRotation)
                isNeedFileRotation = config.FileNameTemplate.Contains('{') || config.FileNameTemplate.Contains('}');

            return new FileWriterConfiguration(ConvertLogLevel(config.LogLevel), config.Template, config.FileNameTemplate, isNeedFileRotation, Encoding.GetEncoding(config.Encoding));
        }

        private static DatabaseWriterConfiguration CreateLoggerConfigurationInstance(IDatabaseWriter config)
        {
            return new DatabaseWriterConfiguration(ConvertLogLevel(config.LogLevel), config.ConnectionString, config.StoredProcedureName);
        }

        private static PipeWriterConfiguration CreateLoggerConfigurationInstance(IPipeWriter config)
        {
            return new PipeWriterConfiguration(ConvertLogLevel(config.LogLevel), config.ServerName, config.PipeName);
        }

        private static NetWriterConfiguration CreateLoggerConfigurationInstance(INetworkWriter config)
        {
            return new NetWriterConfiguration(ConvertLogLevel(config.LogLevel), config.ServerAddress, config.Port);
        }

        private static EmptyWriterConfiguration CreateLoggerConfigurationInstance(IEmptyWriter config)
        {
            return new EmptyWriterConfiguration();
        }

        private static RoutingWrapperConfiguration CreateLoggerConfigurationInstance(IRoutingWrapper config)
        {
            var routingLoggers =
                config.RoutingBySystem.ToDictionary(
                    o => o.Key,
                    o => (new List<LogWriterWrapperConfiguration> { CreateLoggerConfigurationInstanceCommon(o.Value) }));

            List<LogWriterWrapperConfiguration> fromAll = config.FromAll.Select(CreateLoggerConfigurationInstanceCommon).ToList();
            List<LogWriterWrapperConfiguration> fromOthers = config.FromOthers.Select(CreateLoggerConfigurationInstanceCommon).ToList();

            return new RoutingWrapperConfiguration(routingLoggers, fromAll, fromOthers);
        }

        private static GroupWrapperConfiguration CreateLoggerConfigurationInstance(IGroupWrapper config)
        {
            IEnumerable<LogWriterWrapperConfiguration> loggers = config.Loggers.Select(CreateLoggerConfigurationInstanceCommon).ToList();

            return new GroupWrapperConfiguration(loggers);
        }

        private static PatternMatchingWrapperConfiguration CreateLoggerConfigurationInstance(IPatternMatchingWrapper config)
        {
            Dictionary<string, LogWriterWrapperConfiguration> matchCfg = new Dictionary<string, LogWriterWrapperConfiguration>();
            LogWriterWrapperConfiguration defaultCfg = null;

            foreach (var elem in config.Writers)
            {
                if (elem is IPatternMatchingMatch)
                {
                    if (matchCfg.ContainsKey((elem as IPatternMatchingMatch).Value))
                        throw new LoggerConfigurationException("Повторяющиеся ключи в PatternMatchingWrapper: " + (elem as IPatternMatchingMatch).Value);
                    matchCfg.Add((elem as IPatternMatchingMatch).Value, CreateLoggerConfigurationInstanceCommon((elem as IPatternMatchingMatch).Writer));
                }
                else if (elem is IPatternMatchingDefault)
                {
                    if (defaultCfg != null)
                        throw new LoggerConfigurationException("Элемент 'default' встетился более одного раза в PatternMatchingWrapper.");
                    defaultCfg = CreateLoggerConfigurationInstanceCommon((elem as IPatternMatchingDefault).Writer);
                }
            }

            return new PatternMatchingWrapperConfiguration(config.Pattern, matchCfg, defaultCfg);
        }


        private static LogWriterWrapperConfiguration CreateLoggerConfigurationInstanceCommon(ILoggerWriterWrapperConfiguration baseConfig)
        {
            if (baseConfig is IReliableWrapper)
                return CreateLoggerConfigurationInstance(baseConfig as IReliableWrapper);

            if (baseConfig is IAsyncReliableQueueWrapper)
                return CreateLoggerConfigurationInstance(baseConfig as IAsyncReliableQueueWrapper);

            if (baseConfig is IAsyncQueueWrapper)
                return CreateLoggerConfigurationInstance(baseConfig as IAsyncQueueWrapper);

            if (baseConfig is IConsoleWriter)
                return CreateLoggerConfigurationInstance(baseConfig as IConsoleWriter);

            if (baseConfig is IFileWriter)
                return CreateLoggerConfigurationInstance(baseConfig as IFileWriter);

            if (baseConfig is IDatabaseWriter)
                return CreateLoggerConfigurationInstance(baseConfig as IDatabaseWriter);

            if (baseConfig is IPipeWriter)
                return CreateLoggerConfigurationInstance(baseConfig as IPipeWriter);

            if (baseConfig is INetworkWriter)
                return CreateLoggerConfigurationInstance(baseConfig as INetworkWriter);

            if (baseConfig is IGroupWrapper)
                return CreateLoggerConfigurationInstance(baseConfig as IGroupWrapper);

            if (baseConfig is IRoutingWrapper)
                return CreateLoggerConfigurationInstance(baseConfig as IRoutingWrapper);

            if (baseConfig is IPatternMatchingWrapper)
                return CreateLoggerConfigurationInstance(baseConfig as IPatternMatchingWrapper);

            if (baseConfig is IEmptyWriter)
                return CreateLoggerConfigurationInstance(baseConfig as IEmptyWriter);

            throw new ArgumentException("Неизвестный тип конфигурации");
        }



        private static LoggerConfiguration CreateLoggerConfigRoot(IRootLoggerConfiguration config)
        {
            var innerLogger = CreateLoggerConfigurationInstanceCommon(config.WriterWrapper);
            return new LoggerConfiguration(ConvertLogLevel(config.LogLevel), config.IsEnabled, config.EnableStackTraceExtraction, innerLogger);
        }
    }
}
