 
using System;
using Qoollo.Logger.Common;
using Qoollo.Logger.Helpers;
using System.Runtime.CompilerServices;

namespace Qoollo.Logger
{
    /// <summary>
    /// Логгер. Содержит методы для логгирования.
    /// Log - универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
    /// Trace, Debug, Info, Warn, Error, Fatal - 5 методов логгирования с четко указанным уровнем.
    /// Выходной лог для записи файл, определяется темплейтом в app.config файле 
    /// (при сетевом логгировании этот параметр будет содержаться в конфигах самого сервера, принимающего логи).
    /// Так же в лог можно включать информацию, которая не передается в явном виде: имя метода, имя файла,
    /// номер строки в вызающем коде.
    /// Отдельно следует описать поле контекст. Под ним подразумевается строки в формате "id=3, pocessId=4, imageId=5"
    /// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД 
    /// и делать четкую выборку по интересующим полям
    /// </summary>
    partial class LoggerBase
    {
    

        #region Методы для логирования
    
        #region Log methods

        #region Log(..., string message, ...)

        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="level">Уровень логгирования.</param>
        /// <param name="message">Сообщение для логгирования.</param>
        /// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void Log(LogLevel level, string message,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && Level.IsEnabled(level))
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var data = new LoggingEvent(message, null, level, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }

        #endregion
        #region Log(..., string message, string context, ...)

        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="level">Уровень логгирования.</param>
        /// <param name="message">Сообщение для логгирования.</param>
        /// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void Log(LogLevel level, string message, string context,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && Level.IsEnabled(level))
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var data = new LoggingEvent(message, null, level, context, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }

        #endregion
        #region Log(..., Exception exception, string message, ...)

        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="level">Уровень логгирования.</param>
        /// <param name="message">Сообщение для логгирования.</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void Log(LogLevel level, Exception exception, string message,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && Level.IsEnabled(level))
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var data = new LoggingEvent(message, exception, level, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }

        #endregion
        #region Log(..., Exception exception, string message, string context, ...)

        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="level">Уровень логгирования.</param>
        /// <param name="message">Сообщение для логгирования.</param>
        /// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void Log(LogLevel level, Exception exception, string message, string context,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && Level.IsEnabled(level))
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var data = new LoggingEvent(message, exception, level, context, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }

        #endregion


        #region LogFormat(string template, ...)

        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="level">Уровень логгирования.</param>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void LogFormat(LogLevel level, string template,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && Level.IsEnabled(level))
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template);

                var data = new LoggingEvent(message, null, level, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region LogFormat(string template, object arg0, ...)

        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="level">Уровень логгирования.</param>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="arg0">Первый аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void LogFormat(LogLevel level, string template, object arg0,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && Level.IsEnabled(level))
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0);

                var data = new LoggingEvent(message, null, level, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region LogFormat(string template, object arg0, object arg1, ...)

        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="level">Уровень логгирования.</param>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void LogFormat(LogLevel level, string template, object arg0, object arg1,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && Level.IsEnabled(level))
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1);

                var data = new LoggingEvent(message, null, level, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region LogFormat(string template, object arg0, object arg1, object arg2, ...)

        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="level">Уровень логгирования.</param>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void LogFormat(LogLevel level, string template, object arg0, object arg1, object arg2,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && Level.IsEnabled(level))
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1, arg2);

                var data = new LoggingEvent(message, null, level, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region LogFormat(string template, object arg0, object arg1, object arg2, object arg3, ...)

        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="level">Уровень логгирования.</param>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void LogFormat(LogLevel level, string template, object arg0, object arg1, object arg2, object arg3,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && Level.IsEnabled(level))
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1, arg2, arg3);

                var data = new LoggingEvent(message, null, level, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region LogFormat(Exception exception, string template, ...)

        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="level">Уровень логгирования.</param>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void LogFormat(LogLevel level, Exception exception, string template,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && Level.IsEnabled(level))
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template);

                var data = new LoggingEvent(message, exception, level, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region LogFormat(Exception exception, string template, object arg0, ...)

        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="level">Уровень логгирования.</param>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void LogFormat(LogLevel level, Exception exception, string template, object arg0,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && Level.IsEnabled(level))
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0);

                var data = new LoggingEvent(message, exception, level, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region LogFormat(Exception exception, string template, object arg0, object arg1, ...)

        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="level">Уровень логгирования.</param>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void LogFormat(LogLevel level, Exception exception, string template, object arg0, object arg1,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && Level.IsEnabled(level))
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1);

                var data = new LoggingEvent(message, exception, level, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region LogFormat(Exception exception, string template, object arg0, object arg1, object arg2, ...)

        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="level">Уровень логгирования.</param>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void LogFormat(LogLevel level, Exception exception, string template, object arg0, object arg1, object arg2,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && Level.IsEnabled(level))
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1, arg2);

                var data = new LoggingEvent(message, exception, level, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region LogFormat(Exception exception, string template, object arg0, object arg1, object arg2, object arg3, ...)

        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="level">Уровень логгирования.</param>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void LogFormat(LogLevel level, Exception exception, string template, object arg0, object arg1, object arg2, object arg3,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && Level.IsEnabled(level))
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1, arg2, arg3);

                var data = new LoggingEvent(message, exception, level, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
    
        #endregion


        #region Trace

        #region Trace(..., string message, ...)
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="message">Сообщение для логгирования.</param>
        /// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void Trace(string message,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isTraceEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);
                
				var data = new LoggingEvent(message, null, LogLevel.Trace, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region Trace(..., string message, string context, ...)
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="message">Сообщение для логгирования.</param>
        /// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void Trace(string message, string context,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isTraceEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);
                
				var data = new LoggingEvent(message, null, LogLevel.Trace, context, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region Trace(..., Exception exception, string message, ...)
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="message">Сообщение для логгирования.</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void Trace(Exception exception, string message,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isTraceEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);
                
				var data = new LoggingEvent(message, exception, LogLevel.Trace, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region Trace(..., Exception exception, string message, string context, ...)
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="message">Сообщение для логгирования.</param>
        /// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void Trace(Exception exception, string message, string context,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isTraceEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);
                
				var data = new LoggingEvent(message, exception, LogLevel.Trace, context, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion

        #endregion
        #region TraceFormat

        #region TraceFormat(string template, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void TraceFormat(string template,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isTraceEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template);

                var data = new LoggingEvent(message, null, LogLevel.Trace, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region TraceFormat(string template, object arg0, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="arg0">Первый аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void TraceFormat(string template, object arg0,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isTraceEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0);

                var data = new LoggingEvent(message, null, LogLevel.Trace, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region TraceFormat(string template, object arg0, object arg1, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void TraceFormat(string template, object arg0, object arg1,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isTraceEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1);

                var data = new LoggingEvent(message, null, LogLevel.Trace, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region TraceFormat(string template, object arg0, object arg1, object arg2, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void TraceFormat(string template, object arg0, object arg1, object arg2,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isTraceEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1, arg2);

                var data = new LoggingEvent(message, null, LogLevel.Trace, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region TraceFormat(string template, object arg0, object arg1, object arg2, object arg3, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void TraceFormat(string template, object arg0, object arg1, object arg2, object arg3,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isTraceEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1, arg2, arg3);

                var data = new LoggingEvent(message, null, LogLevel.Trace, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region TraceFormat(Exception exception, string template, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void TraceFormat(Exception exception, string template,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isTraceEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template);

                var data = new LoggingEvent(message, exception, LogLevel.Trace, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region TraceFormat(Exception exception, string template, object arg0, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void TraceFormat(Exception exception, string template, object arg0,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isTraceEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0);

                var data = new LoggingEvent(message, exception, LogLevel.Trace, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region TraceFormat(Exception exception, string template, object arg0, object arg1, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void TraceFormat(Exception exception, string template, object arg0, object arg1,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isTraceEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1);

                var data = new LoggingEvent(message, exception, LogLevel.Trace, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region TraceFormat(Exception exception, string template, object arg0, object arg1, object arg2, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void TraceFormat(Exception exception, string template, object arg0, object arg1, object arg2,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isTraceEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1, arg2);

                var data = new LoggingEvent(message, exception, LogLevel.Trace, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region TraceFormat(Exception exception, string template, object arg0, object arg1, object arg2, object arg3, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void TraceFormat(Exception exception, string template, object arg0, object arg1, object arg2, object arg3,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isTraceEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1, arg2, arg3);

                var data = new LoggingEvent(message, exception, LogLevel.Trace, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion

        #endregion

        #region Debug

        #region Debug(..., string message, ...)
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="message">Сообщение для логгирования.</param>
        /// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void Debug(string message,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isDebugEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);
                
				var data = new LoggingEvent(message, null, LogLevel.Debug, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region Debug(..., string message, string context, ...)
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="message">Сообщение для логгирования.</param>
        /// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void Debug(string message, string context,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isDebugEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);
                
				var data = new LoggingEvent(message, null, LogLevel.Debug, context, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region Debug(..., Exception exception, string message, ...)
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="message">Сообщение для логгирования.</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void Debug(Exception exception, string message,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isDebugEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);
                
				var data = new LoggingEvent(message, exception, LogLevel.Debug, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region Debug(..., Exception exception, string message, string context, ...)
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="message">Сообщение для логгирования.</param>
        /// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void Debug(Exception exception, string message, string context,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isDebugEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);
                
				var data = new LoggingEvent(message, exception, LogLevel.Debug, context, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion

        #endregion
        #region DebugFormat

        #region DebugFormat(string template, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void DebugFormat(string template,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isDebugEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template);

                var data = new LoggingEvent(message, null, LogLevel.Debug, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region DebugFormat(string template, object arg0, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="arg0">Первый аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void DebugFormat(string template, object arg0,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isDebugEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0);

                var data = new LoggingEvent(message, null, LogLevel.Debug, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region DebugFormat(string template, object arg0, object arg1, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void DebugFormat(string template, object arg0, object arg1,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isDebugEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1);

                var data = new LoggingEvent(message, null, LogLevel.Debug, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region DebugFormat(string template, object arg0, object arg1, object arg2, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void DebugFormat(string template, object arg0, object arg1, object arg2,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isDebugEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1, arg2);

                var data = new LoggingEvent(message, null, LogLevel.Debug, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region DebugFormat(string template, object arg0, object arg1, object arg2, object arg3, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void DebugFormat(string template, object arg0, object arg1, object arg2, object arg3,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isDebugEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1, arg2, arg3);

                var data = new LoggingEvent(message, null, LogLevel.Debug, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region DebugFormat(Exception exception, string template, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void DebugFormat(Exception exception, string template,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isDebugEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template);

                var data = new LoggingEvent(message, exception, LogLevel.Debug, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region DebugFormat(Exception exception, string template, object arg0, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void DebugFormat(Exception exception, string template, object arg0,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isDebugEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0);

                var data = new LoggingEvent(message, exception, LogLevel.Debug, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region DebugFormat(Exception exception, string template, object arg0, object arg1, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void DebugFormat(Exception exception, string template, object arg0, object arg1,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isDebugEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1);

                var data = new LoggingEvent(message, exception, LogLevel.Debug, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region DebugFormat(Exception exception, string template, object arg0, object arg1, object arg2, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void DebugFormat(Exception exception, string template, object arg0, object arg1, object arg2,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isDebugEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1, arg2);

                var data = new LoggingEvent(message, exception, LogLevel.Debug, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region DebugFormat(Exception exception, string template, object arg0, object arg1, object arg2, object arg3, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void DebugFormat(Exception exception, string template, object arg0, object arg1, object arg2, object arg3,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isDebugEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1, arg2, arg3);

                var data = new LoggingEvent(message, exception, LogLevel.Debug, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion

        #endregion

        #region Info

        #region Info(..., string message, ...)
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="message">Сообщение для логгирования.</param>
        /// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void Info(string message,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isInfoEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);
                
				var data = new LoggingEvent(message, null, LogLevel.Info, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region Info(..., string message, string context, ...)
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="message">Сообщение для логгирования.</param>
        /// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void Info(string message, string context,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isInfoEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);
                
				var data = new LoggingEvent(message, null, LogLevel.Info, context, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region Info(..., Exception exception, string message, ...)
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="message">Сообщение для логгирования.</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void Info(Exception exception, string message,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isInfoEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);
                
				var data = new LoggingEvent(message, exception, LogLevel.Info, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region Info(..., Exception exception, string message, string context, ...)
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="message">Сообщение для логгирования.</param>
        /// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void Info(Exception exception, string message, string context,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isInfoEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);
                
				var data = new LoggingEvent(message, exception, LogLevel.Info, context, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion

        #endregion
        #region InfoFormat

        #region InfoFormat(string template, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void InfoFormat(string template,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isInfoEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template);

                var data = new LoggingEvent(message, null, LogLevel.Info, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region InfoFormat(string template, object arg0, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="arg0">Первый аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void InfoFormat(string template, object arg0,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isInfoEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0);

                var data = new LoggingEvent(message, null, LogLevel.Info, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region InfoFormat(string template, object arg0, object arg1, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void InfoFormat(string template, object arg0, object arg1,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isInfoEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1);

                var data = new LoggingEvent(message, null, LogLevel.Info, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region InfoFormat(string template, object arg0, object arg1, object arg2, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void InfoFormat(string template, object arg0, object arg1, object arg2,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isInfoEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1, arg2);

                var data = new LoggingEvent(message, null, LogLevel.Info, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region InfoFormat(string template, object arg0, object arg1, object arg2, object arg3, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void InfoFormat(string template, object arg0, object arg1, object arg2, object arg3,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isInfoEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1, arg2, arg3);

                var data = new LoggingEvent(message, null, LogLevel.Info, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region InfoFormat(Exception exception, string template, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void InfoFormat(Exception exception, string template,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isInfoEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template);

                var data = new LoggingEvent(message, exception, LogLevel.Info, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region InfoFormat(Exception exception, string template, object arg0, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void InfoFormat(Exception exception, string template, object arg0,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isInfoEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0);

                var data = new LoggingEvent(message, exception, LogLevel.Info, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region InfoFormat(Exception exception, string template, object arg0, object arg1, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void InfoFormat(Exception exception, string template, object arg0, object arg1,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isInfoEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1);

                var data = new LoggingEvent(message, exception, LogLevel.Info, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region InfoFormat(Exception exception, string template, object arg0, object arg1, object arg2, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void InfoFormat(Exception exception, string template, object arg0, object arg1, object arg2,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isInfoEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1, arg2);

                var data = new LoggingEvent(message, exception, LogLevel.Info, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region InfoFormat(Exception exception, string template, object arg0, object arg1, object arg2, object arg3, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void InfoFormat(Exception exception, string template, object arg0, object arg1, object arg2, object arg3,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isInfoEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1, arg2, arg3);

                var data = new LoggingEvent(message, exception, LogLevel.Info, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion

        #endregion

        #region Warn

        #region Warn(..., string message, ...)
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="message">Сообщение для логгирования.</param>
        /// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void Warn(string message,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isWarnEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);
                
				var data = new LoggingEvent(message, null, LogLevel.Warn, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region Warn(..., string message, string context, ...)
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="message">Сообщение для логгирования.</param>
        /// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void Warn(string message, string context,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isWarnEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);
                
				var data = new LoggingEvent(message, null, LogLevel.Warn, context, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region Warn(..., Exception exception, string message, ...)
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="message">Сообщение для логгирования.</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void Warn(Exception exception, string message,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isWarnEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);
                
				var data = new LoggingEvent(message, exception, LogLevel.Warn, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region Warn(..., Exception exception, string message, string context, ...)
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="message">Сообщение для логгирования.</param>
        /// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void Warn(Exception exception, string message, string context,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isWarnEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);
                
				var data = new LoggingEvent(message, exception, LogLevel.Warn, context, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion

        #endregion
        #region WarnFormat

        #region WarnFormat(string template, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void WarnFormat(string template,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isWarnEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template);

                var data = new LoggingEvent(message, null, LogLevel.Warn, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region WarnFormat(string template, object arg0, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="arg0">Первый аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void WarnFormat(string template, object arg0,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isWarnEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0);

                var data = new LoggingEvent(message, null, LogLevel.Warn, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region WarnFormat(string template, object arg0, object arg1, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void WarnFormat(string template, object arg0, object arg1,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isWarnEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1);

                var data = new LoggingEvent(message, null, LogLevel.Warn, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region WarnFormat(string template, object arg0, object arg1, object arg2, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void WarnFormat(string template, object arg0, object arg1, object arg2,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isWarnEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1, arg2);

                var data = new LoggingEvent(message, null, LogLevel.Warn, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region WarnFormat(string template, object arg0, object arg1, object arg2, object arg3, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void WarnFormat(string template, object arg0, object arg1, object arg2, object arg3,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isWarnEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1, arg2, arg3);

                var data = new LoggingEvent(message, null, LogLevel.Warn, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region WarnFormat(Exception exception, string template, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void WarnFormat(Exception exception, string template,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isWarnEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template);

                var data = new LoggingEvent(message, exception, LogLevel.Warn, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region WarnFormat(Exception exception, string template, object arg0, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void WarnFormat(Exception exception, string template, object arg0,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isWarnEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0);

                var data = new LoggingEvent(message, exception, LogLevel.Warn, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region WarnFormat(Exception exception, string template, object arg0, object arg1, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void WarnFormat(Exception exception, string template, object arg0, object arg1,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isWarnEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1);

                var data = new LoggingEvent(message, exception, LogLevel.Warn, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region WarnFormat(Exception exception, string template, object arg0, object arg1, object arg2, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void WarnFormat(Exception exception, string template, object arg0, object arg1, object arg2,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isWarnEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1, arg2);

                var data = new LoggingEvent(message, exception, LogLevel.Warn, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region WarnFormat(Exception exception, string template, object arg0, object arg1, object arg2, object arg3, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void WarnFormat(Exception exception, string template, object arg0, object arg1, object arg2, object arg3,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isWarnEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1, arg2, arg3);

                var data = new LoggingEvent(message, exception, LogLevel.Warn, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion

        #endregion

        #region Error

        #region Error(..., string message, ...)
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="message">Сообщение для логгирования.</param>
        /// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void Error(string message,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isErrorEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);
                
				var data = new LoggingEvent(message, null, LogLevel.Error, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region Error(..., string message, string context, ...)
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="message">Сообщение для логгирования.</param>
        /// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void Error(string message, string context,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isErrorEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);
                
				var data = new LoggingEvent(message, null, LogLevel.Error, context, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region Error(..., Exception exception, string message, ...)
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="message">Сообщение для логгирования.</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void Error(Exception exception, string message,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isErrorEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);
                
				var data = new LoggingEvent(message, exception, LogLevel.Error, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region Error(..., Exception exception, string message, string context, ...)
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="message">Сообщение для логгирования.</param>
        /// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void Error(Exception exception, string message, string context,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isErrorEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);
                
				var data = new LoggingEvent(message, exception, LogLevel.Error, context, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion

        #endregion
        #region ErrorFormat

        #region ErrorFormat(string template, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void ErrorFormat(string template,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isErrorEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template);

                var data = new LoggingEvent(message, null, LogLevel.Error, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region ErrorFormat(string template, object arg0, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="arg0">Первый аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void ErrorFormat(string template, object arg0,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isErrorEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0);

                var data = new LoggingEvent(message, null, LogLevel.Error, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region ErrorFormat(string template, object arg0, object arg1, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void ErrorFormat(string template, object arg0, object arg1,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isErrorEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1);

                var data = new LoggingEvent(message, null, LogLevel.Error, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region ErrorFormat(string template, object arg0, object arg1, object arg2, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void ErrorFormat(string template, object arg0, object arg1, object arg2,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isErrorEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1, arg2);

                var data = new LoggingEvent(message, null, LogLevel.Error, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region ErrorFormat(string template, object arg0, object arg1, object arg2, object arg3, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void ErrorFormat(string template, object arg0, object arg1, object arg2, object arg3,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isErrorEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1, arg2, arg3);

                var data = new LoggingEvent(message, null, LogLevel.Error, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region ErrorFormat(Exception exception, string template, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void ErrorFormat(Exception exception, string template,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isErrorEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template);

                var data = new LoggingEvent(message, exception, LogLevel.Error, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region ErrorFormat(Exception exception, string template, object arg0, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void ErrorFormat(Exception exception, string template, object arg0,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isErrorEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0);

                var data = new LoggingEvent(message, exception, LogLevel.Error, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region ErrorFormat(Exception exception, string template, object arg0, object arg1, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void ErrorFormat(Exception exception, string template, object arg0, object arg1,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isErrorEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1);

                var data = new LoggingEvent(message, exception, LogLevel.Error, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region ErrorFormat(Exception exception, string template, object arg0, object arg1, object arg2, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void ErrorFormat(Exception exception, string template, object arg0, object arg1, object arg2,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isErrorEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1, arg2);

                var data = new LoggingEvent(message, exception, LogLevel.Error, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region ErrorFormat(Exception exception, string template, object arg0, object arg1, object arg2, object arg3, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void ErrorFormat(Exception exception, string template, object arg0, object arg1, object arg2, object arg3,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isErrorEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1, arg2, arg3);

                var data = new LoggingEvent(message, exception, LogLevel.Error, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion

        #endregion

        #region Fatal

        #region Fatal(..., string message, ...)
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="message">Сообщение для логгирования.</param>
        /// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void Fatal(string message,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isFatalEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);
                
				var data = new LoggingEvent(message, null, LogLevel.Fatal, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region Fatal(..., string message, string context, ...)
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="message">Сообщение для логгирования.</param>
        /// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void Fatal(string message, string context,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isFatalEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);
                
				var data = new LoggingEvent(message, null, LogLevel.Fatal, context, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region Fatal(..., Exception exception, string message, ...)
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="message">Сообщение для логгирования.</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void Fatal(Exception exception, string message,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isFatalEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);
                
				var data = new LoggingEvent(message, exception, LogLevel.Fatal, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region Fatal(..., Exception exception, string message, string context, ...)
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="message">Сообщение для логгирования.</param>
        /// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void Fatal(Exception exception, string message, string context,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isFatalEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);
                
				var data = new LoggingEvent(message, exception, LogLevel.Fatal, context, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion

        #endregion
        #region FatalFormat

        #region FatalFormat(string template, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void FatalFormat(string template,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isFatalEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template);

                var data = new LoggingEvent(message, null, LogLevel.Fatal, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region FatalFormat(string template, object arg0, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="arg0">Первый аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void FatalFormat(string template, object arg0,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isFatalEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0);

                var data = new LoggingEvent(message, null, LogLevel.Fatal, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region FatalFormat(string template, object arg0, object arg1, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void FatalFormat(string template, object arg0, object arg1,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isFatalEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1);

                var data = new LoggingEvent(message, null, LogLevel.Fatal, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region FatalFormat(string template, object arg0, object arg1, object arg2, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void FatalFormat(string template, object arg0, object arg1, object arg2,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isFatalEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1, arg2);

                var data = new LoggingEvent(message, null, LogLevel.Fatal, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region FatalFormat(string template, object arg0, object arg1, object arg2, object arg3, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void FatalFormat(string template, object arg0, object arg1, object arg2, object arg3,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isFatalEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1, arg2, arg3);

                var data = new LoggingEvent(message, null, LogLevel.Fatal, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region FatalFormat(Exception exception, string template, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void FatalFormat(Exception exception, string template,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isFatalEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template);

                var data = new LoggingEvent(message, exception, LogLevel.Fatal, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region FatalFormat(Exception exception, string template, object arg0, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void FatalFormat(Exception exception, string template, object arg0,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isFatalEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0);

                var data = new LoggingEvent(message, exception, LogLevel.Fatal, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region FatalFormat(Exception exception, string template, object arg0, object arg1, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void FatalFormat(Exception exception, string template, object arg0, object arg1,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isFatalEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1);

                var data = new LoggingEvent(message, exception, LogLevel.Fatal, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region FatalFormat(Exception exception, string template, object arg0, object arg1, object arg2, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void FatalFormat(Exception exception, string template, object arg0, object arg1, object arg2,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isFatalEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1, arg2);

                var data = new LoggingEvent(message, exception, LogLevel.Fatal, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion
        #region FatalFormat(Exception exception, string template, object arg0, object arg1, object arg2, object arg3, ...)
    
        /// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
        /// <param name="template">Шаблон сообщения (как в string.Format)</param>
        /// <param name="exception">Возникшее исключение.</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="guard">Защитный параметр</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
        public void FatalFormat(Exception exception, string template, object arg0, object arg1, object arg2, object arg3,
                    ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (_isEnabled && _isFatalEnabled)
            {
				string assembly = null;
				string @namespace = null;
				ExtractCallerInfo(ref assembly, ref @namespace, ref @class, ref method, ref filePath, ref lineNumber);

                var message = string.Format(template, arg0, arg1, arg2, arg3);

                var data = new LoggingEvent(message, exception, LogLevel.Fatal, null, _stackSources, LocalMachineInfo.CombinedMachineName, LocalMachineInfo.ProcessName, LocalMachineInfo.ProcessId, assembly, @namespace, @class, method, filePath, lineNumber);
                _logger.Write(data);
            }
        }
        #endregion

        #endregion

        #endregion
    }
}