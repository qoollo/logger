using System.Linq;
using Qoollo.Logger.Common;
using Qoollo.Logger.Configuration;
using Qoollo.Logger.Initialization;
using Qoollo.Logger.LoggingEventConverters;
using Qoollo.Logger.Writers;
using System;
using System.Diagnostics.Contracts;

namespace Qoollo.Logger
{
    /// <summary>
    /// Логгер для использования в подсистемах
    /// Желательно оборачивать в свой собственный сингелтон в подсистеме, 
    /// который обязательно наследовать от данного класса.
    /// </summary>
    public class Logger : LoggerBase
    {
        /// <summary>
        /// Получить пустой логгер
        /// </summary>
        /// <returns>Логгер</returns>
        protected static Logger GetEmptyLogger()
        {
            return LoggerDefault.EmptyLogger;
        }

        /// <summary>
        /// Инициализировать обёртку логгера
        /// </summary>
        /// <param name="assembly">Сборка для поиска</param>
        /// <param name="wrapper">Обёртка</param>
        /// <returns>Количество инициализированных логгеров</returns>
        protected static void InitializeLoggerInAssembly(ILogger wrapper, System.Reflection.Assembly assembly)
        {
            Contract.Requires<ArgumentNullException>(wrapper != null);
            Contract.Requires<ArgumentNullException>(assembly != null);

            Initializer.InitializeLoggerInAssembly(wrapper, assembly);
        }

        /// <summary>
        /// Инициализировать обёртку логгера
        /// </summary>
        /// <param name="wrapper">Обёртка</param>
        /// <param name="assembly">Сборки для поиска</param>
        /// <returns>Количество инициализированных логгеров</returns>
        protected static void InitializeLoggerInAssembly(ILogger wrapper, params System.Reflection.Assembly[] assembly)
        {
            Contract.Requires<ArgumentNullException>(wrapper != null);
            Contract.Requires<ArgumentNullException>(assembly != null);

            Initializer.InitializeLoggerInAssembly(wrapper, assembly);
        }

        /// <summary>
        /// Инициализировать логгеры в других сборках как дочерние
        /// </summary>
        /// <param name="wrapper">Обёртка</param>
        /// <param name="type">Произвольные тип из сборок с дочерним логгером</param>
        /// <returns>Количество инициализированных логгеров</returns>
        protected static void InitializeLoggerInAssembly(ILogger wrapper, Type type)
        {
            Contract.Requires<ArgumentNullException>(wrapper != null);
            Contract.Requires<ArgumentNullException>(type != null);

            Initializer.InitializeLoggerInAssembly(wrapper, type.Assembly);
        }

        /// <summary>
        /// Инициализировать логгеры в других сборках как дочерние
        /// </summary>
        /// <param name="wrapper">Обёртка</param>
        /// <param name="types">Произвольные типы из сборок с дочерними логгерами</param>
        /// <returns>Количество инициализированных логгеров</returns>
        protected static void InitializeLoggerInAssembly(ILogger wrapper, params Type[] types)
        {
            Contract.Requires<ArgumentNullException>(wrapper != null);
            Contract.Requires<ArgumentNullException>(types != null);

            Initializer.InitializeLoggerInAssembly(wrapper, types.Select(o => o.Assembly));
        }



        /// <summary>
        /// Определяет, включён ли логгер (для well-known логгеров)
        /// </summary>
        /// <param name="logger">Интерфейс логгера</param>
        /// <returns>Включён ли</returns>
        private static bool DetectIsEnabled(ILogger logger)
        {
            if (logger == null)
                return false;

            var loggerBase = logger as LoggerBase;
            if (loggerBase == null)
                return true;

            return loggerBase.IsLoggerEnabled;
        }


        /// <summary>
        /// Создание логгера
        /// </summary>
        /// <param name="logLevel">Уровень логирования</param>
        /// <param name="moduleName">Имя модуля (подсистемы)</param>
        /// <param name="innerLogger">Внутренний логгер</param>
        /// <param name="enableStackTraceExtraction">Разрешено ли получать данные из StackTrace</param>
        /// <param name="isEnabled">Вклюён ли логгер</param>
        internal Logger(LogLevel logLevel, string moduleName, ILoggingEventWriter innerLogger, bool enableStackTraceExtraction, bool isEnabled)
            : base(logLevel, moduleName, innerLogger, enableStackTraceExtraction, isEnabled)
        {
        }

        /// <summary>
        /// Создание логгера
        /// </summary>
        /// <param name="logLevel">Уровень логирования</param>
        /// <param name="moduleName">Имя модуля (подсистемы)</param>
        /// <param name="typeInfo">Тип, к которому привзяан логгер</param>
        /// <param name="innerLogger">Внутренний логгер</param>
        /// <param name="enableStackTraceExtraction">Разрешено ли получать данные из StackTrace</param>
        /// <param name="isEnabled">Вклюён ли логгер</param>
        internal Logger(LogLevel logLevel, string moduleName, Type typeInfo, ILoggingEventWriter innerLogger, bool enableStackTraceExtraction, bool isEnabled)
            : base(logLevel, moduleName, typeInfo, innerLogger, enableStackTraceExtraction, isEnabled)
        {
        }


        /// <summary>
        /// Создание логгера
        /// </summary>
        /// <param name="moduleName">Имя модуля (подсистемы)</param>
        /// <param name="innerLogger">Внутренний логгер</param>
        protected Logger(string moduleName, ILogger innerLogger)
            : base(innerLogger.Level, moduleName, innerLogger, innerLogger.AllowStackTraceInfoExtraction, DetectIsEnabled(innerLogger))
        {
        }

        /// <summary>
        /// Создание логгера
        /// </summary>
        /// <param name="logLevel">Уровень логирования</param>
        /// <param name="moduleName">Имя модуля (подсистемы)</param>
        /// <param name="innerLogger">Внутренний логгер</param>
        protected Logger(LogLevel logLevel, string moduleName, ILogger innerLogger)
            : base(logLevel, moduleName, innerLogger, innerLogger.AllowStackTraceInfoExtraction, DetectIsEnabled(innerLogger))
        {
        }




        /// <summary>
        /// Получение логгера для указанного типа
        /// </summary>
        /// <param name="typeInfo">Тип</param>
        /// <returns>Логгер для типа</returns>
        public Logger GetClassLogger(Type typeInfo)
        {
            if (typeInfo == null)
                throw new ArgumentNullException("typeInfo");

            return new Logger(this.Level, this.ModuleName, typeInfo, this, this.AllowStackTraceInfoExtraction, this.IsLoggerEnabled);
        }

        /// <summary>
        /// Получение логгера для текущего класса
        /// </summary>
        /// <returns>Логгер для типа</returns>
        public Logger GetThisClassLogger()
        {
            var stack = new System.Diagnostics.StackTrace(false);
            for (int i = 0; i < stack.FrameCount; i++)
            {
                var frame = stack.GetFrame(i);
                var curMInf = frame.GetMethod();
                if (curMInf != null && curMInf.DeclaringType != typeof(LoggerBase) && !curMInf.DeclaringType.IsSubclassOf(typeof(LoggerBase)))
                {
                    return GetClassLogger(curMInf.DeclaringType);
                }
            }

            return this;
        }
    }
}