using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Qoollo.Logger.Common;
using Qoollo.Logger.Configuration;
using Qoollo.Logger.LoggingEventConverters;
using Qoollo.Logger.Helpers;

namespace Qoollo.Logger
{
    /// <summary>
    /// LoggerBase class that contains core logging methods.
    /// Log - universal log method which also parametrized with LogLevel of message
    /// Trace, Debug, Info, Warn, Error, Fatal - separate logging methods for all log levels.
    /// </summary>
    public abstract partial class LoggerBase : ILogger
    {
        private const string Class = "Qoollo.Logger.LoggerBase";

        private readonly string _moduleName;
        private readonly Type _typeInfo;
        private List<string> _stackSources;
        private ILoggingEventWriter _logger;
        private readonly bool _isEnabled;

        private volatile bool _isDisposed;


        /// <summary>
        /// LoggerBase constructor
        /// </summary>
        /// <param name="configuration">Logger configuration object</param>
        /// <param name="moduleName">Name of the module for which this logger will be created</param>
        /// <param name="innerLogger">Logger or writer to be wrapped</param>
        public LoggerBase(LoggerConfiguration configuration, string moduleName, ILoggingEventWriter innerLogger)
        {
            Contract.Requires<ArgumentNullException>(configuration != null, "configuration");
            Contract.Requires<ArgumentNullException>(moduleName != null, "moduleName");
            Contract.Requires<ArgumentNullException>(innerLogger != null, "innerLogger");

            Contract.Assume(configuration.Level != null, "configuration.Level");

            _moduleName = moduleName;
            _typeInfo = null;
            _logger = innerLogger;
            Refresh();

            Level = configuration.Level;
            _isEnabled = configuration.IsEnabled;
            _isTraceEnabled = Level.IsTraceEnabled;
            _isDebugEnabled = Level.IsDebugEnabled;
            _isInfoEnabled = Level.IsInfoEnabled;
            _isWarnEnabled = Level.IsWarnEnabled;
            _isErrorEnabled = Level.IsErrorEnabled;
            _isFatalEnabled = Level.IsFatalEnabled;
            _allowStackTraceInfoExtraction = configuration.IsStackTraceEnabled;
        }

        /// <summary>
        /// LoggerBase constructor
        /// </summary>
        /// <param name="configuration">Logger configuration</param>
        /// <param name="moduleName">Name of the module for which this logger will be created</param>
        public LoggerBase(LoggerConfiguration configuration, string moduleName)
            : this(configuration, moduleName, LoggerFactory.CreateWriter(configuration.Writer))
        {
        }


        /// <summary>
        /// LoggerBase constructor
        /// </summary>
        /// <param name="logLevel">Log level</param>
        /// <param name="moduleName">Name of the module for which this logger will be created</param>
        /// <param name="typeInfo">Type to which this logger will be bound</param>
        /// <param name="innerLogger">Logger or writer to be wrapped</param>
        /// <param name="enableStackTraceExtraction">Will StackTrace extraction be enabled for this logger</param>
        /// <param name="isEnabled">Will logger be enabled after creation</param>
        public LoggerBase(LogLevel logLevel, string moduleName, Type typeInfo, ILoggingEventWriter innerLogger, bool enableStackTraceExtraction = false, bool isEnabled = true)
        {
            Contract.Requires<ArgumentNullException>(logLevel != null, "logLevel");
            Contract.Requires<ArgumentNullException>(moduleName != null, "moduleName");
            Contract.Requires<ArgumentNullException>(innerLogger != null, "innerLogger");

            _moduleName = moduleName;
            _typeInfo = typeInfo;
            _logger = innerLogger;
            Refresh();

            Level = logLevel;
            _isEnabled = isEnabled;
            _isTraceEnabled = Level.IsTraceEnabled;
            _isDebugEnabled = Level.IsDebugEnabled;
            _isInfoEnabled = Level.IsInfoEnabled;
            _isWarnEnabled = Level.IsWarnEnabled;
            _isErrorEnabled = Level.IsErrorEnabled;
            _isFatalEnabled = Level.IsFatalEnabled;

            _allowStackTraceInfoExtraction = enableStackTraceExtraction;
        }

        /// <summary>
        /// LoggerBase constructor
        /// </summary>
        /// <param name="logLevel">Log level</param>
        /// <param name="moduleName">Name of the module for which this logger will be created</param>
        /// <param name="innerLogger">Logger or writer to be wrapped</param>
        /// <param name="enableStackTraceExtraction">Will StackTrace extraction be enabled for this logger</param>
        /// <param name="isEnabled">Will logger be enabled after creation</param>
        public LoggerBase(LogLevel logLevel, string moduleName, ILoggingEventWriter innerLogger, bool enableStackTraceExtraction = false, bool isEnabled = true)
            : this(logLevel, moduleName, null, innerLogger, enableStackTraceExtraction, isEnabled)
        {
        }


        #region Source Info Extraction

        private readonly bool _allowStackTraceInfoExtraction = false;

        /// <summary>
        /// Is StackTrace extraction enabled for this logger.
        /// (Adds information about class and assembly to log message; Slow)
        /// </summary>
        public bool AllowStackTraceInfoExtraction
        {
            get { return _allowStackTraceInfoExtraction; }
        }

        /// <summary>
        /// Extract full information about caller from StackTrace
        /// </summary>
        /// <param name="assembly">Extracted assembly name</param>
        /// <param name="namespace">Extracted namespace name</param>
        /// <param name="class">Extracted class name</param>
        /// <param name="method">Extracted method name</param>
        /// <param name="filePath">Extracted source code file name</param>
        /// <param name="lineNumber">Extracted line number in source code file</param>
        protected void ExtractCallerInfoFromStackTrace(out string assembly, out string @namespace, out string @class, out string method, out string filePath, out int lineNumber)
        {
            assembly = null;
            @namespace = null;
            @class = null;
            method = null;
            filePath = string.Empty;
            lineNumber = 0;

            var stack = new System.Diagnostics.StackTrace(true);
            for (int i = 0; i < stack.FrameCount; i++)
            {
                var frame = stack.GetFrame(i);
                var curMInf = frame.GetMethod();
                if (curMInf != null && 
                    curMInf.DeclaringType != typeof(LoggerBase) && !curMInf.DeclaringType.IsSubclassOf(typeof(LoggerBase)) &&
                    curMInf.DeclaringType != typeof(LoggerStatic) && !curMInf.DeclaringType.IsSubclassOf(typeof(LoggerStatic)))
                {
                    assembly = curMInf.DeclaringType.Assembly.FullName;
                    @namespace = curMInf.DeclaringType.Namespace;
                    @class = curMInf.DeclaringType.FullName;
                    method = curMInf.Name;
                    filePath = frame.GetFileName();
                    lineNumber = frame.GetFileLineNumber();
                    break;
                }
            }
        }

        /// <summary>
        /// Extract information about caller from StackTrace
        /// </summary>
        /// <param name="assembly">Extracted assembly name</param>
        /// <param name="namespace">Extracted namespace name</param>
        /// <param name="class">Extracted class name</param>
        /// <param name="method">Extracted method name</param>
        protected void ExtractCallerInfoFromStackTrace(out string assembly, out string @namespace, out string @class, out string method)
        {
            assembly = null;
            @namespace = null;
            @class = null;
            method = null;

            var stack = new System.Diagnostics.StackTrace(false);
            for (int i = 0; i < stack.FrameCount; i++)
            {
                var frame = stack.GetFrame(i);
                var curMInf = frame.GetMethod();
                if (curMInf != null && 
                    curMInf.DeclaringType != typeof(LoggerBase) && !curMInf.DeclaringType.IsSubclassOf(typeof(LoggerBase)) &&
                    curMInf.DeclaringType != typeof(LoggerStatic) && !curMInf.DeclaringType.IsSubclassOf(typeof(LoggerStatic)))
                {
                    assembly = curMInf.DeclaringType.Assembly.FullName;
                    @namespace = curMInf.DeclaringType.Namespace;
                    @class = curMInf.DeclaringType.FullName;
                    method = curMInf.Name;
                    break;
                }
            }
        }

        /// <summary>
        /// Extract information about caller by bound type or from StackTrace (if enabled)
        /// </summary>
        /// <param name="assembly">Extracted assembly name</param>
        /// <param name="namespace">Extracted namespace name</param>
        /// <param name="class">Extracted class name</param>
        /// <param name="method">Extracted method name</param>
        /// <param name="filePath">Extracted source code file name</param>
        /// <param name="lineNumber">Extracted line number in source code file</param>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        protected void ExtractCallerInfo(ref string assembly, ref string @namespace, ref string @class, ref string method, ref string filePath, ref int lineNumber)
        {
            if (_allowStackTraceInfoExtraction)
            {
                if (filePath != null && lineNumber > 0)
                    ExtractCallerInfoFromStackTrace(out assembly, out @namespace, out @class, out method);
                else
                    ExtractCallerInfoFromStackTrace(out assembly, out @namespace, out @class, out method, out filePath, out lineNumber);
            }
            else
            {
                if (@class == null && _typeInfo != null)
                {
                    @class = _typeInfo.FullName;
                    assembly = _typeInfo.Assembly.FullName;
                    @namespace = _typeInfo.Namespace;
                }
            }
        }

        #endregion

        #region Log Levels

        private readonly bool _isTraceEnabled;
        private readonly bool _isDebugEnabled;
        private readonly bool _isInfoEnabled;
        private readonly bool _isWarnEnabled;
        private readonly bool _isErrorEnabled;
        private readonly bool _isFatalEnabled;

        /// <summary>
        /// Is logger enabled
        /// </summary>
        public bool IsLoggerEnabled
        {
            get { return _isEnabled; }
        }

        /// <summary>
        /// Will 'Trace' level messages be processed by this logger.
        /// </summary>
        public bool IsTraceEnabled
        {
            get { return _isTraceEnabled; }
        }

        /// <summary>
        /// Will 'Debug' level messages be processed by this logger.
        /// </summary>
        public bool IsDebugEnabled
        {
            get { return _isDebugEnabled; }
        }

        /// <summary>
        /// Will 'Info' level messages be processed by this logger.
        /// </summary>
        public bool IsInfoEnabled
        {
            get { return _isInfoEnabled; }
        }

        /// <summary>
        /// Will 'Warn' level messages be processed by this logger.
        /// </summary>
        public bool IsWarnEnabled
        {
            get { return _isWarnEnabled; }
        }

        /// <summary>
        /// Will 'Error' level messages be processed by this logger.
        /// </summary>
        public bool IsErrorEnabled
        {
            get { return _isErrorEnabled; }
        }

        /// <summary>
        /// Will 'Fatal' level messages be processed by this logger.
        /// </summary>
        public bool IsFatalEnabled
        {
            get { return _isFatalEnabled; }
        }


        /// <summary>
        /// Gets a value indicating whether logging is enabled for the specified level
        /// </summary>
        /// <param name="level">Log level to be checked</param>
        /// <returns>True if level enabled</returns>
        public bool IsEnabled(LogLevel level)
        {
            return Level.IsEnabled(level);
        }

        #endregion

        #region Implementation of ILogger

        /// <summary>
        /// Name of module to which this logger is bound
        /// </summary>
        public string ModuleName { get { return _moduleName; } }

        /// <summary>
        /// Type to which this logger is bound
        /// </summary>
        public Type TypeInfo { get { return _typeInfo; } }

        /// <summary>
        /// Log level
        /// </summary>
        public LogLevel Level { get; private set; }


        /// <summary>
        /// Hook to process stack source on refresh
        /// </summary>
        /// <param name="stackSource">Current chain (can be changed)</param>
        protected virtual void OnRefreshStackSource(List<string> stackSource)
        {
        }

        /// <summary>
        /// Refresh StackSources chain
        /// </summary>
        public void Refresh()
        {
            if (_logger is ILogger)
                _stackSources = (_logger as ILogger).GetStackSources();
            else
                _stackSources = new List<string>();

            if (_stackSources.Count == 0 || _stackSources[_stackSources.Count - 1] != _moduleName || _typeInfo == null)
                _stackSources.Add(_moduleName);

            OnRefreshStackSource(_stackSources);
        }

        /// <summary>
        /// Returns the chain of modules through which this logger was initialized (logger stacking order)
        /// </summary>
        /// <returns>Chain of modules names. First element - inner module, last - outer.</returns>
        List<string> ILogger.GetStackSources()
        {
            return new List<string>(_stackSources);
        }

        /// <summary>
        /// Set the factory to create converters from log message to string.
        /// Required for FileWriter and ConsoleWriter.
        /// </summary>
        /// <param name="factory">Factory to create particular converters</param>
        public void SetConverterFactory(ConverterFactory factory)
        {
            _logger.SetConverterFactory(factory);
        }

        /// <summary>
        /// Write the log message
        /// </summary>
        /// <param name="data">Log message</param>
        bool ILoggingEventWriter.Write(LoggingEvent data)
        {
            return _logger.Write(data);
        }

        #endregion

        #region Helper Logging Methods

        /// <summary>
        /// Helping method to write log
        /// </summary>
        /// <param name="level">Log Level</param>
        /// <param name="exception">Exception (can be null)</param>
        /// <param name="message">Log message</param>
        /// <param name="context">Log message context</param>
        /// <param name="class">Class name from which the logging performed</param>
        /// <param name="method">Method name from which the logging performed</param>
        /// <param name="filePath">Source code file name</param>
        /// <param name="lineNumber">Line number in source code file</param>
        private void WriteLog(LogLevel level, Exception exception, string message, string context,
                              string @class, string method, string filePath, int lineNumber)
        {
            Contract.Requires(level != null);

            string assembly = null;
            string @namespace = null;
            ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

            var data = new LoggingEvent(message, exception, level, context, _stackSources, LocalMachineInfo.MachineName, LocalMachineInfo.MachineAddress, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
            _logger.Write(data);
        }

        #endregion

        #region Disposing

        /// <summary>
        /// Main clean-up code
        /// </summary>
        /// <param name="isUserCall">Is called by user</param>
        /// <param name="isClose">Is called from Close method</param>
        private void Dispose(bool isUserCall, bool isClose)
        {
            if (!_isDisposed)
            {
                _isDisposed = true;

                if (isUserCall)
                {
                    if (_logger != null)
                    {
                        if (isClose)
                            _logger.Close();
                        else
                            _logger.Dispose();
                    }
                }
            }
        }


        /// <summary>
        /// Wirte passed message at Info level and then dispose the logger.
        /// Helps to distinct cases of expected closing of application and unexpected (by unhandled exception)
        /// </summary>
        /// <param name="msg">Message to write</param>
        public void Close(string msg)
        {
            if (!_isDisposed)
            {
                var thisType = this.GetType();

                var data = new LoggingEvent(msg, null, LogLevel.Info, null, _stackSources,
                    LocalMachineInfo.MachineName, LocalMachineInfo.MachineAddress, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId,
                    thisType.Assembly.FullName, thisType.Namespace, thisType.Name, "Close", null, -1);

                (this as ILogger).Write(data);
                this.Close();
            }
        }

        /// <summary>
        /// Close logger and clean-up all resources.
        /// It guarantee that all pending messages will be processed.
        /// </summary>
        public void Close()
        {
            Dispose(true, isClose: true);
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Clean up logger resources (see also 'void Close()')
        /// </summary>
        public void Dispose()
        {
            Dispose(true, isClose: false);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
