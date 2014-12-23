using Qoollo.Logger.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qoollo.Logger
{
    /// <summary>
    /// Набор стандартных логгеров
    /// </summary>
    public static class LoggerDefault
    {
        private static Logger _emptyInstance;
        private static Logger _consoleInstance;
        private static Logger _defaultInstance;

        private static readonly object _lockCreation = new object();

        /// <summary>
        /// Пустой логгер (в никуда)
        /// </summary>
        public static Logger EmptyLogger
        {
            get
            {
                if (_emptyInstance == null)
                {
                    lock (_lockCreation)
                    {
                        if (_emptyInstance == null)
                        {
                            var config = new EmptyWriterConfiguration();
                            _emptyInstance = new Logger(LogLevel.Info, "EmptyLogger", LoggerFactory.CreateWriter(config), false, true);
                        }
                    }
                }

                return _emptyInstance;
            }
        }

        /// <summary>
        /// Простой консольный логгер
        /// </summary>
        public static Logger ConsoleLogger
        {
            get
            {
                if (_consoleInstance == null)
                {
                    lock (_lockCreation)
                    {
                        if (_consoleInstance == null)
                        {
                            var config = new ConsoleWriterConfiguration();
                            _consoleInstance = new Logger(LogLevel.FullLog, "ConsoleLogger", LoggerFactory.CreateWriter(config), false, true);
                        }
                    }
                }

                return _consoleInstance;
            }
        }

        /// <summary>
        /// Логгер по умолчанию
        /// </summary>
        public static Logger Instance
        {
            get
            {
                if (_defaultInstance == null)
                    System.Threading.Interlocked.CompareExchange(ref _defaultInstance, ConsoleLogger, null);
                return _defaultInstance;
            }
        }

        /// <summary>
        /// Задать логгер по умолчанию
        /// </summary>
        /// <param name="newDefault">Новый логгер</param>
        public static void SetInstance(Logger newDefault)
        {
            if (newDefault == null)
                newDefault = ConsoleLogger;

            var oldLogger = System.Threading.Interlocked.Exchange(ref _defaultInstance, newDefault);
            if (oldLogger != null && oldLogger != ConsoleLogger && oldLogger != EmptyLogger)
                oldLogger.Dispose();
        }
        /// <summary>
        /// Сбросить логгер по умолчанию в стандартный
        /// </summary>
        public static void ResetInstance()
        {
            SetInstance(null);
        }
        /// <summary>
        /// Загрузить логгер по умолчанию из файла конфигурации
        /// </summary>
        /// <param name="sectionName">Имя секции для загрузки</param>
        public static void LoadInstanceFromAppConfig(string sectionName = "LoggerConfigurationSection")
        {
            Contract.Requires<ArgumentNullException>(sectionName != null);

            var logger = LoggerFactory.CreateLoggerFromAppConfig("Default", sectionName);
            SetInstance(logger);
        }
    }
}
