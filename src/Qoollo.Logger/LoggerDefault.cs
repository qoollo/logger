﻿using Qoollo.Logger.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Qoollo.Logger
{
    /// <summary>
    /// Contains several default loggers (Empty, Console, Global singleton)
    /// </summary>
    public static class LoggerDefault
    {
        private static Logger _emptyInstance;
        private static Logger _consoleInstance;
        private static Logger _defaultInstance;

        private static readonly object _lockCreation = new object();

        /// <summary>
        /// Init empty logger instance
        /// </summary>
        private static void CreateEmptyLogger()
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
        }

        /// <summary>
        /// Init console logger instance
        /// </summary>
        private static void CreateConsoleLogger()
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
        }

        /// <summary>
        /// Init default logger instance
        /// </summary>
        private static void CreateDefaultLogger()
        {
            if (_defaultInstance == null)
            {
                lock (_lockCreation)
                {
                    if (_defaultInstance == null)
                    {
                        if (Configurator.HasConfiguration("LoggerConfigurationSection"))
                            LoadInstanceFromAppConfig();
                        else
                            SetInstance(ConsoleLogger);
                    }
                }
            }
        }

        /// <summary>
        /// Automatic logger dispose on ProcessExit event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="args">Event args</param>
        private static void FreeDefaultLogger(object sender, EventArgs args)
        {
            if (_defaultInstance != null && _defaultInstance != _consoleInstance && _defaultInstance != _emptyInstance)
            {
                var oldLogger = System.Threading.Interlocked.Exchange(ref _defaultInstance, _consoleInstance);
                if (oldLogger != null && oldLogger != _consoleInstance && oldLogger != _emptyInstance)
                    oldLogger.Dispose();
            }
        }

        /// <summary>
        /// Empty logger (not writes any message)
        /// </summary>
        public static Logger EmptyLogger
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (_emptyInstance == null)
                    CreateEmptyLogger();

                return _emptyInstance;
            }
        }

        /// <summary>
        /// Simple logger to Console
        /// </summary>
        public static Logger ConsoleLogger
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (_consoleInstance == null)
                    CreateConsoleLogger();

                return _consoleInstance;
            }
        }

        /// <summary>
        /// Global logger singleton
        /// </summary>
        public static Logger Instance
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (_defaultInstance == null)
                    CreateDefaultLogger();

                return _defaultInstance;
            }
        }

        /// <summary>
        /// Set global logger singleton instance
        /// </summary>
        /// <param name="newDefault">New instance</param>
        public static void SetInstance(Logger newDefault)
        {
            if (newDefault == null)
                newDefault = EmptyLogger;

            var oldLogger = System.Threading.Interlocked.Exchange(ref _defaultInstance, newDefault);
            if (oldLogger != null && oldLogger != _consoleInstance && oldLogger != _emptyInstance)
                oldLogger.Close();

            AppDomain.CurrentDomain.ProcessExit += FreeDefaultLogger;
        }
        /// <summary>
        /// Reset global logger singleton
        /// </summary>
        public static void ResetInstance()
        {
            SetInstance(null);
        }
        /// <summary>
        /// Load global logger singleton from AppConfig
        /// </summary>
        /// <param name="sectionName">Section name in AppConfig</param>
        public static void LoadInstanceFromAppConfig(string sectionName = "LoggerConfigurationSection")
        {
            Contract.Requires<ArgumentNullException>(sectionName != null);

            var logger = LoggerFactory.CreateLoggerFromAppConfig("Default", sectionName);
            SetInstance(logger);
        }
    }
}
