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
    /// Main class for logger.
    /// It is a good practice to create own logger singleton in every assembly 
    /// which should be inherited from this class.
    /// </summary>
    public class Logger : LoggerBase
    {
        /// <summary>
        /// Returns the instance of Empty logger
        /// </summary>
        /// <returns>Logger instance</returns>
        protected static Logger GetEmptyLogger()
        {
            return LoggerDefault.EmptyLogger;
        }

        /// <summary>
        /// Method for initialization logger singleton in dependant assembly (with the help of Reflection)
        /// </summary>
        /// <param name="wrapper">Parent logger instance that will be wrapped by assembly logger singleton</param>
        /// <param name="assembly">Assembly where the loggers will be initialized</param>
        protected static void InitializeLoggerInAssembly(ILogger wrapper, System.Reflection.Assembly assembly)
        {
            Contract.Requires<ArgumentNullException>(wrapper != null);
            Contract.Requires<ArgumentNullException>(assembly != null);

            Initializer.InitializeLoggerInAssembly(wrapper, assembly);
        }

        /// <summary>
        /// Method for initialization logger singleton in dependant assemblies (with the help of Reflection)
        /// </summary>
        /// <param name="wrapper">Parent logger instance that will be wrapped by assembly logger singleton</param>
        /// <param name="assembly">Array of dependant assemblies</param>
        protected static void InitializeLoggerInAssembly(ILogger wrapper, params System.Reflection.Assembly[] assembly)
        {
            Contract.Requires<ArgumentNullException>(wrapper != null);
            Contract.Requires<ArgumentNullException>(assembly != null);

            Initializer.InitializeLoggerInAssembly(wrapper, assembly);
        }

        /// <summary>
        /// Method for initialization logger singleton in dependant assembly (with the help of Reflection)
        /// </summary>
        /// <param name="wrapper">Parent logger instance that will be wrapped by assembly logger singleton</param>
        /// <param name="type">Any type in dependant assembly (will be used to extract assembly as 'type.Assembly')</param>
        protected static void InitializeLoggerInAssembly(ILogger wrapper, Type type)
        {
            Contract.Requires<ArgumentNullException>(wrapper != null);
            Contract.Requires<ArgumentNullException>(type != null);

            Initializer.InitializeLoggerInAssembly(wrapper, type.Assembly);
        }

        /// <summary>
        /// Method for initialization logger singleton in dependant assemblies (with the help of Reflection)
        /// </summary>
        /// <param name="wrapper">Parent logger instance that will be wrapped by assembly logger singleton</param>
        /// <param name="types">Array of types from dependant assemblies (will be used to extract assembly as 'type.Assembly')</param>
        protected static void InitializeLoggerInAssembly(ILogger wrapper, params Type[] types)
        {
            Contract.Requires<ArgumentNullException>(wrapper != null);
            Contract.Requires<ArgumentNullException>(types != null);

            Initializer.InitializeLoggerInAssembly(wrapper, types.Select(o => o.Assembly));
        }



        /// <summary>
        /// Detect if logger enabled (for well-known loggers only)
        /// </summary>
        /// <param name="logger">logger instance as interface of ILogger</param>
        /// <returns>Is logger enabled</returns>
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
        /// Logger constructor
        /// </summary>
        /// <param name="logLevel">Log level</param>
        /// <param name="moduleName">Name of the module for which this logger will be created</param>
        /// <param name="innerLogger">Parent logger or writer</param>
        /// <param name="enableStackTraceExtraction">Will StackTrace extraction be enabled for this logger</param>
        /// <param name="isEnabled">Will logger be enabled after creation</param>
        internal Logger(LogLevel logLevel, string moduleName, ILoggingEventWriter innerLogger, bool enableStackTraceExtraction, bool isEnabled)
            : base(logLevel, moduleName, innerLogger, enableStackTraceExtraction, isEnabled)
        {
        }

        /// <summary>
        /// Logger constructor
        /// </summary>
        /// <param name="logLevel">Log level</param>
        /// <param name="moduleName">Name of the module for which this logger will be created</param>
        /// <param name="typeInfo">Type to which this logger will be bound</param>
        /// <param name="innerLogger">Parent logger or writer</param>
        /// <param name="enableStackTraceExtraction">Will StackTrace extraction be enabled for this logger</param>
        /// <param name="isEnabled">Will logger be enabled after creation</param>
        internal Logger(LogLevel logLevel, string moduleName, Type typeInfo, ILoggingEventWriter innerLogger, bool enableStackTraceExtraction, bool isEnabled)
            : base(logLevel, moduleName, typeInfo, innerLogger, enableStackTraceExtraction, isEnabled)
        {
        }


        /// <summary>
        /// Logger constructor
        /// </summary>
        /// <param name="moduleName">Name of the module for which this logger will be created</param>
        /// <param name="innerLogger">Parent logger to be wrapped by this logger instance</param>
        protected Logger(string moduleName, ILogger innerLogger)
            : base(innerLogger.Level, moduleName, innerLogger, innerLogger.AllowStackTraceInfoExtraction, DetectIsEnabled(innerLogger))
        {
        }

        /// <summary>
        /// Logger constructor
        /// </summary>
        /// <param name="logLevel">Log level</param>
        /// <param name="moduleName">Name of the module for which this logger will be created</param>
        /// <param name="innerLogger">Parent logger to be wrapped by this logger instance</param>
        protected Logger(LogLevel logLevel, string moduleName, ILogger innerLogger)
            : base(logLevel, moduleName, innerLogger, innerLogger.AllowStackTraceInfoExtraction, DetectIsEnabled(innerLogger))
        {
        }




        /// <summary>
        /// Creates the logger which bound to the concrete type.
        /// (add type information to log message without slow StackTrace extraction)
        /// </summary>
        /// <param name="typeInfo">Type</param>
        /// <returns>Instance of logger for passed type</returns>
        public Logger GetClassLogger(Type typeInfo)
        {
            if (typeInfo == null)
                throw new ArgumentNullException("typeInfo");

            return new Logger(this.Level, this.ModuleName, typeInfo, this, this.AllowStackTraceInfoExtraction, this.IsLoggerEnabled);
        }

        /// <summary>
        /// Creates the logger which bound to the type from which this method is calling
        /// </summary>
        /// <returns>Instance of logger for current class</returns>
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