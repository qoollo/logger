 
using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using Libs.Logger.Common;
using Libs.Logger.Implementation;

namespace Libs.Logger
{
    /// <summary>
    /// Логгер. Содержит методы для логгирования.
    /// Флаг isUseTraceParser, определяет будет ли использоваться
    /// механизм извлечения имени класса, метода, имени файла, номера строки из StackTrace
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
        private readonly bool _isUseTraceParser;
        
        #region Constructors

        /// <summary>
        /// Создание логгера
        /// </summary>
        /// <param name="configuration">Конфигурация логгера</param>
        /// <param name="moduleName">Имя модуля (подсистемы)</param>
        /// <param name="innerLogger">Внутренний логгер</param>
        /// <param name="factory">Конвертор для преобразования данных в строковый тип</param>
        /// <param name="isUseTraceParser">Флаг, определяющий использование механизма извлечения 
        /// имени класса, метода, имени файла, номера строки из StackTrace</param>
        public LoggerBase(Common.LoggerConfiguration configuration, string moduleName, IInnerLogger innerLogger,
            bool isUseTraceParser, ConverterFactory factory = null):
            this(configuration, moduleName, innerLogger, factory)
        {
            _isUseTraceParser = isUseTraceParser;
        }

        #endregion

		#region Методы для логирования
	
		#region Log methods

		#region Log(..., string message, ...)

		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="level">Уровень логгирования.</param>
		/// <param name="message">Сообщение для логгирования.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Log(LogLevel level, string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && Level.IsEnabled(level))
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, level, null, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}

		#endregion
		#region Log(..., string context, string message, ...)

		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="level">Уровень логгирования.</param>
		/// <param name="message">Сообщение для логгирования.</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Log(LogLevel level, string context, string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && Level.IsEnabled(level))
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, level, context, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}

		#endregion
		#region Log(..., string context, string @class, string message, ...)

		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="level">Уровень логгирования.</param>
		/// <param name="message">Сообщение для логгирования.</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Log(LogLevel level, string context, string @class, string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && Level.IsEnabled(level))
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, level, context, _stackSources, @class, method, filePath, lineNumber);
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
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Log(LogLevel level, Exception exception, string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && Level.IsEnabled(level))
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, level, null, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}

		#endregion
		#region Log(..., string context, Exception exception, string message, ...)

		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="level">Уровень логгирования.</param>
		/// <param name="message">Сообщение для логгирования.</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Log(LogLevel level, string context, Exception exception, string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && Level.IsEnabled(level))
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, level, context, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}

		#endregion
		#region Log(..., string context, string @class, Exception exception, string message, ...)

		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="level">Уровень логгирования.</param>
		/// <param name="message">Сообщение для логгирования.</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Log(LogLevel level, string context, string @class, Exception exception, string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && Level.IsEnabled(level))
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, level, context, _stackSources, @class, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}

		#endregion


		#region LogFormat(..., string template, ...)

		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="level">Уровень логгирования.</param>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void LogFormat(LogLevel level, string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && Level.IsEnabled(level))
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, level, null, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
		#endregion
		#region LogFormat(..., string context, string template, ...)

		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="level">Уровень логгирования.</param>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void LogFormat(LogLevel level, string context, string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && Level.IsEnabled(level))
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, level, context, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
		#endregion
		#region LogFormat(..., string context, string @class, string template, ...)

		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="level">Уровень логгирования.</param>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void LogFormat(LogLevel level, string context, string @class, string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && Level.IsEnabled(level))
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, level, context, _stackSources, @class, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
		#endregion
		#region LogFormat(..., Exception exception, string template, ...)

		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="level">Уровень логгирования.</param>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void LogFormat(LogLevel level, Exception exception, string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && Level.IsEnabled(level))
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, level, null, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
		#endregion
		#region LogFormat(..., string context, Exception exception, string template, ...)

		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="level">Уровень логгирования.</param>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void LogFormat(LogLevel level, string context, Exception exception, string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && Level.IsEnabled(level))
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, level, context, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
		#endregion
		#region LogFormat(..., string context, string @class, Exception exception, string template, ...)

		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="level">Уровень логгирования.</param>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void LogFormat(LogLevel level, string context, string @class, Exception exception, string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && Level.IsEnabled(level))
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, level, context, _stackSources, @class, method, filePath, lineNumber);
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
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Trace(string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isTraceEnabled)
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, LogLevel.Trace, null, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
		#endregion
        #region Trace(..., string context, string message, ...)
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="message">Сообщение для логгирования.</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Trace(string context, string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isTraceEnabled)
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, LogLevel.Trace, context, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
		#endregion
        #region Trace(..., string context, string @class, string message, ...)
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="message">Сообщение для логгирования.</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Trace(string context, string @class, string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isTraceEnabled)
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, LogLevel.Trace, context, _stackSources, @class, method, filePath, lineNumber);
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
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Trace(Exception exception, string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isTraceEnabled)
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, LogLevel.Trace, null, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
		#endregion
        #region Trace(..., string context, Exception exception, string message, ...)
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="message">Сообщение для логгирования.</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Trace(string context, Exception exception, string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isTraceEnabled)
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, LogLevel.Trace, context, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
		#endregion
        #region Trace(..., string context, string @class, Exception exception, string message, ...)
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="message">Сообщение для логгирования.</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Trace(string context, string @class, Exception exception, string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isTraceEnabled)
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, LogLevel.Trace, context, _stackSources, @class, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
		#endregion

	    #endregion
		#region TraceFormat

		#region TraceFormat(..., string template, ...)
	
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void TraceFormat(string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isTraceEnabled)
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, LogLevel.Trace, null, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
	    #endregion
		#region TraceFormat(..., string context, string template, ...)
	
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void TraceFormat(string context, string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isTraceEnabled)
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, LogLevel.Trace, context, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
	    #endregion
		#region TraceFormat(..., string context, string @class, string template, ...)
	
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void TraceFormat(string context, string @class, string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isTraceEnabled)
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, LogLevel.Trace, context, _stackSources, @class, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
	    #endregion
		#region TraceFormat(..., Exception exception, string template, ...)
	
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void TraceFormat(Exception exception, string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isTraceEnabled)
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, LogLevel.Trace, null, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
	    #endregion
		#region TraceFormat(..., string context, Exception exception, string template, ...)
	
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void TraceFormat(string context, Exception exception, string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isTraceEnabled)
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, LogLevel.Trace, context, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
	    #endregion
		#region TraceFormat(..., string context, string @class, Exception exception, string template, ...)
	
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void TraceFormat(string context, string @class, Exception exception, string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isTraceEnabled)
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, LogLevel.Trace, context, _stackSources, @class, method, filePath, lineNumber);
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
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Debug(string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isDebugEnabled)
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, LogLevel.Debug, null, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
		#endregion
        #region Debug(..., string context, string message, ...)
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="message">Сообщение для логгирования.</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Debug(string context, string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isDebugEnabled)
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, LogLevel.Debug, context, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
		#endregion
        #region Debug(..., string context, string @class, string message, ...)
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="message">Сообщение для логгирования.</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Debug(string context, string @class, string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isDebugEnabled)
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, LogLevel.Debug, context, _stackSources, @class, method, filePath, lineNumber);
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
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Debug(Exception exception, string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isDebugEnabled)
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, LogLevel.Debug, null, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
		#endregion
        #region Debug(..., string context, Exception exception, string message, ...)
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="message">Сообщение для логгирования.</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Debug(string context, Exception exception, string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isDebugEnabled)
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, LogLevel.Debug, context, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
		#endregion
        #region Debug(..., string context, string @class, Exception exception, string message, ...)
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="message">Сообщение для логгирования.</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Debug(string context, string @class, Exception exception, string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isDebugEnabled)
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, LogLevel.Debug, context, _stackSources, @class, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
		#endregion

	    #endregion
		#region DebugFormat

		#region DebugFormat(..., string template, ...)
	
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void DebugFormat(string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isDebugEnabled)
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, LogLevel.Debug, null, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
	    #endregion
		#region DebugFormat(..., string context, string template, ...)
	
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void DebugFormat(string context, string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isDebugEnabled)
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, LogLevel.Debug, context, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
	    #endregion
		#region DebugFormat(..., string context, string @class, string template, ...)
	
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void DebugFormat(string context, string @class, string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isDebugEnabled)
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, LogLevel.Debug, context, _stackSources, @class, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
	    #endregion
		#region DebugFormat(..., Exception exception, string template, ...)
	
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void DebugFormat(Exception exception, string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isDebugEnabled)
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, LogLevel.Debug, null, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
	    #endregion
		#region DebugFormat(..., string context, Exception exception, string template, ...)
	
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void DebugFormat(string context, Exception exception, string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isDebugEnabled)
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, LogLevel.Debug, context, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
	    #endregion
		#region DebugFormat(..., string context, string @class, Exception exception, string template, ...)
	
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void DebugFormat(string context, string @class, Exception exception, string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isDebugEnabled)
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, LogLevel.Debug, context, _stackSources, @class, method, filePath, lineNumber);
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
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Info(string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isInfoEnabled)
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, LogLevel.Info, null, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
		#endregion
        #region Info(..., string context, string message, ...)
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="message">Сообщение для логгирования.</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Info(string context, string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isInfoEnabled)
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, LogLevel.Info, context, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
		#endregion
        #region Info(..., string context, string @class, string message, ...)
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="message">Сообщение для логгирования.</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Info(string context, string @class, string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isInfoEnabled)
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, LogLevel.Info, context, _stackSources, @class, method, filePath, lineNumber);
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
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Info(Exception exception, string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isInfoEnabled)
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, LogLevel.Info, null, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
		#endregion
        #region Info(..., string context, Exception exception, string message, ...)
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="message">Сообщение для логгирования.</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Info(string context, Exception exception, string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isInfoEnabled)
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, LogLevel.Info, context, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
		#endregion
        #region Info(..., string context, string @class, Exception exception, string message, ...)
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="message">Сообщение для логгирования.</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Info(string context, string @class, Exception exception, string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isInfoEnabled)
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, LogLevel.Info, context, _stackSources, @class, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
		#endregion

	    #endregion
		#region InfoFormat

		#region InfoFormat(..., string template, ...)
	
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void InfoFormat(string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isInfoEnabled)
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, LogLevel.Info, null, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
	    #endregion
		#region InfoFormat(..., string context, string template, ...)
	
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void InfoFormat(string context, string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isInfoEnabled)
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, LogLevel.Info, context, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
	    #endregion
		#region InfoFormat(..., string context, string @class, string template, ...)
	
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void InfoFormat(string context, string @class, string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isInfoEnabled)
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, LogLevel.Info, context, _stackSources, @class, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
	    #endregion
		#region InfoFormat(..., Exception exception, string template, ...)
	
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void InfoFormat(Exception exception, string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isInfoEnabled)
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, LogLevel.Info, null, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
	    #endregion
		#region InfoFormat(..., string context, Exception exception, string template, ...)
	
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void InfoFormat(string context, Exception exception, string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isInfoEnabled)
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, LogLevel.Info, context, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
	    #endregion
		#region InfoFormat(..., string context, string @class, Exception exception, string template, ...)
	
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void InfoFormat(string context, string @class, Exception exception, string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isInfoEnabled)
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, LogLevel.Info, context, _stackSources, @class, method, filePath, lineNumber);
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
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Warn(string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isWarnEnabled)
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, LogLevel.Warn, null, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
		#endregion
        #region Warn(..., string context, string message, ...)
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="message">Сообщение для логгирования.</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Warn(string context, string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isWarnEnabled)
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, LogLevel.Warn, context, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
		#endregion
        #region Warn(..., string context, string @class, string message, ...)
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="message">Сообщение для логгирования.</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Warn(string context, string @class, string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isWarnEnabled)
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, LogLevel.Warn, context, _stackSources, @class, method, filePath, lineNumber);
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
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Warn(Exception exception, string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isWarnEnabled)
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, LogLevel.Warn, null, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
		#endregion
        #region Warn(..., string context, Exception exception, string message, ...)
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="message">Сообщение для логгирования.</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Warn(string context, Exception exception, string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isWarnEnabled)
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, LogLevel.Warn, context, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
		#endregion
        #region Warn(..., string context, string @class, Exception exception, string message, ...)
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="message">Сообщение для логгирования.</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Warn(string context, string @class, Exception exception, string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isWarnEnabled)
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, LogLevel.Warn, context, _stackSources, @class, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
		#endregion

	    #endregion
		#region WarnFormat

		#region WarnFormat(..., string template, ...)
	
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void WarnFormat(string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isWarnEnabled)
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, LogLevel.Warn, null, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
	    #endregion
		#region WarnFormat(..., string context, string template, ...)
	
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void WarnFormat(string context, string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isWarnEnabled)
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, LogLevel.Warn, context, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
	    #endregion
		#region WarnFormat(..., string context, string @class, string template, ...)
	
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void WarnFormat(string context, string @class, string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isWarnEnabled)
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, LogLevel.Warn, context, _stackSources, @class, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
	    #endregion
		#region WarnFormat(..., Exception exception, string template, ...)
	
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void WarnFormat(Exception exception, string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isWarnEnabled)
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, LogLevel.Warn, null, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
	    #endregion
		#region WarnFormat(..., string context, Exception exception, string template, ...)
	
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void WarnFormat(string context, Exception exception, string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isWarnEnabled)
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, LogLevel.Warn, context, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
	    #endregion
		#region WarnFormat(..., string context, string @class, Exception exception, string template, ...)
	
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void WarnFormat(string context, string @class, Exception exception, string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isWarnEnabled)
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, LogLevel.Warn, context, _stackSources, @class, method, filePath, lineNumber);
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
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Error(string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isErrorEnabled)
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, LogLevel.Error, null, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
		#endregion
        #region Error(..., string context, string message, ...)
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="message">Сообщение для логгирования.</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Error(string context, string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isErrorEnabled)
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, LogLevel.Error, context, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
		#endregion
        #region Error(..., string context, string @class, string message, ...)
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="message">Сообщение для логгирования.</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Error(string context, string @class, string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isErrorEnabled)
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, LogLevel.Error, context, _stackSources, @class, method, filePath, lineNumber);
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
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Error(Exception exception, string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isErrorEnabled)
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, LogLevel.Error, null, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
		#endregion
        #region Error(..., string context, Exception exception, string message, ...)
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="message">Сообщение для логгирования.</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Error(string context, Exception exception, string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isErrorEnabled)
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, LogLevel.Error, context, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
		#endregion
        #region Error(..., string context, string @class, Exception exception, string message, ...)
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="message">Сообщение для логгирования.</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Error(string context, string @class, Exception exception, string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isErrorEnabled)
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, LogLevel.Error, context, _stackSources, @class, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
		#endregion

	    #endregion
		#region ErrorFormat

		#region ErrorFormat(..., string template, ...)
	
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void ErrorFormat(string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isErrorEnabled)
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, LogLevel.Error, null, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
	    #endregion
		#region ErrorFormat(..., string context, string template, ...)
	
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void ErrorFormat(string context, string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isErrorEnabled)
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, LogLevel.Error, context, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
	    #endregion
		#region ErrorFormat(..., string context, string @class, string template, ...)
	
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void ErrorFormat(string context, string @class, string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isErrorEnabled)
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, LogLevel.Error, context, _stackSources, @class, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
	    #endregion
		#region ErrorFormat(..., Exception exception, string template, ...)
	
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void ErrorFormat(Exception exception, string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isErrorEnabled)
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, LogLevel.Error, null, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
	    #endregion
		#region ErrorFormat(..., string context, Exception exception, string template, ...)
	
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void ErrorFormat(string context, Exception exception, string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isErrorEnabled)
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, LogLevel.Error, context, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
	    #endregion
		#region ErrorFormat(..., string context, string @class, Exception exception, string template, ...)
	
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void ErrorFormat(string context, string @class, Exception exception, string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isErrorEnabled)
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, LogLevel.Error, context, _stackSources, @class, method, filePath, lineNumber);
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
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Fatal(string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isFatalEnabled)
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, LogLevel.Fatal, null, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
		#endregion
        #region Fatal(..., string context, string message, ...)
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="message">Сообщение для логгирования.</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Fatal(string context, string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isFatalEnabled)
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, LogLevel.Fatal, context, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
		#endregion
        #region Fatal(..., string context, string @class, string message, ...)
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="message">Сообщение для логгирования.</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Fatal(string context, string @class, string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isFatalEnabled)
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, LogLevel.Fatal, context, _stackSources, @class, method, filePath, lineNumber);
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
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Fatal(Exception exception, string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isFatalEnabled)
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, LogLevel.Fatal, null, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
		#endregion
        #region Fatal(..., string context, Exception exception, string message, ...)
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="message">Сообщение для логгирования.</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Fatal(string context, Exception exception, string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isFatalEnabled)
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, LogLevel.Fatal, context, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
		#endregion
        #region Fatal(..., string context, string @class, Exception exception, string message, ...)
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="message">Сообщение для логгирования.</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void Fatal(string context, string @class, Exception exception, string message,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isFatalEnabled)
			{
				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, LogLevel.Fatal, context, _stackSources, @class, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
		#endregion

	    #endregion
		#region FatalFormat

		#region FatalFormat(..., string template, ...)
	
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void FatalFormat(string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isFatalEnabled)
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, LogLevel.Fatal, null, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
	    #endregion
		#region FatalFormat(..., string context, string template, ...)
	
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void FatalFormat(string context, string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isFatalEnabled)
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, LogLevel.Fatal, context, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
	    #endregion
		#region FatalFormat(..., string context, string @class, string template, ...)
	
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void FatalFormat(string context, string @class, string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isFatalEnabled)
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, null, LogLevel.Fatal, context, _stackSources, @class, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
	    #endregion
		#region FatalFormat(..., Exception exception, string template, ...)
	
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void FatalFormat(Exception exception, string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isFatalEnabled)
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, LogLevel.Fatal, null, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
	    #endregion
		#region FatalFormat(..., string context, Exception exception, string template, ...)
	
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void FatalFormat(string context, Exception exception, string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isFatalEnabled)
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, LogLevel.Fatal, context, _stackSources, null, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
	    #endregion
		#region FatalFormat(..., string context, string @class, Exception exception, string template, ...)
	
		/// <summary>
		/// Универсальный метод логгирования в который помимо прочих аргументов передается уровень логгирования.
		/// </summary>
		/// <param name="template">Шаблон сообщения (как в string.Format)</param>
		/// <param name="arg0">Первый аргумент</param>
		/// <param name="arg1">Второй аргумент</param>
		/// <param name="arg2">Третий аргумент</param>
		/// <param name="arg3">Четвертый аргумент</param>
		/// <param name="context">Контекст соолбщения. Строки в формате "id=3, pocessId=4, imageId=5"
		/// Идея этого поля в том чтобы записывать значения из контекста в отдельный столбец(ы) в БД
		/// и делать четкую выборку по интересующим полям.</param>
		/// <param name="exception">Возникшее исключение.</param>
		/// <param name="class">Имя класса из которого происходит логгирование.</param>
		/// <param name="lineNumber">Автоподставляемый параметр! Номер строки в файле исходного кода,
		/// на которой произошел вызов метода логгирования.</param>
		/// <param name="filePath">Автоподставляемый параметр! Имя файла исходного кода,
		/// из которого произошел вызов метода логгирования.</param>
		/// <param name="method">Автоподставляемый параметр! Имя метода,
		/// из которого произошел вызов метода логгирования.</param>
		public void FatalFormat(string context, string @class, Exception exception, string template, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null,
					int lineNumber = 0, string filePath = null, string method = null)
		{
			if (_isEnabled && _isFatalEnabled)
			{
				var message = string.Format(template, arg0, arg1, arg2, arg3);

				if (_isUseTraceParser)
			{
				var st = StackTraceParser.GetStackTrace();

				method = method ?? st.Method;
				filePath = filePath ?? st.FilePath;
				lineNumber = lineNumber == 0 ? st.LineNumber : lineNumber;
			}

				var data = new LoggingEvent(message, exception, LogLevel.Fatal, context, _stackSources, @class, method, filePath, lineNumber);
                _logger.Write(data);
			}
		}
	    #endregion

	    #endregion

		#endregion

    }
}