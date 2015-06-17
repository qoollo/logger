using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qoollo.Logger
{
    partial class LoggerStatic : LoggerImplementation.LoggerMarkerClass
    {
        /// <summary>
        /// LoggerStatic should not be created as instance. Use it as Static class.
        /// </summary>
        protected LoggerStatic()
        {
        }

        #region Log Levels

        /// <summary>
        /// Log level
        /// </summary>
        public static LogLevel Level
        {
            get { return Instance.Level; }
        }

        /// <summary>
        /// Is logger enabled
        /// </summary>
        public static bool IsLoggerEnabled
        {
            get { return Instance.IsLoggerEnabled; }
        }

        /// <summary>
        /// Will 'Trace' level messages be processed by this logger.
        /// </summary>
        public static bool IsTraceEnabled
        {
            get { return Instance.IsTraceEnabled; }
        }

        /// <summary>
        /// Will 'Debug' level messages be processed by this logger.
        /// </summary>
        public static bool IsDebugEnabled
        {
            get { return Instance.IsDebugEnabled; }
        }

        /// <summary>
        /// Will 'Info' level messages be processed by this logger.
        /// </summary>
        public static bool IsInfoEnabled
        {
            get { return Instance.IsInfoEnabled; }
        }

        /// <summary>
        /// Will 'Warn' level messages be processed by this logger.
        /// </summary>
        public static bool IsWarnEnabled
        {
            get { return Instance.IsWarnEnabled; }
        }

        /// <summary>
        /// Will 'Error' level messages be processed by this logger.
        /// </summary>
        public static bool IsErrorEnabled
        {
            get { return Instance.IsErrorEnabled; }
        }

        /// <summary>
        /// Will 'Fatal' level messages be processed by this logger.
        /// </summary>
        public static bool IsFatalEnabled
        {
            get { return Instance.IsFatalEnabled; }
        }


        /// <summary>
        /// Gets a value indicating whether logging is enabled for the specified level
        /// </summary>
        /// <param name="level">Log level to be checked</param>
        /// <returns>True if level enabled</returns>
        public static bool IsEnabled(LogLevel level)
        {
            return Instance.IsEnabled(level);
        }

        #endregion
    }
}
