 
using System;
using Qoollo.Logger.Common;
using Qoollo.Logger.Helpers;
using System.Runtime.CompilerServices;

namespace Qoollo.Logger
{
    partial class LoggerBase
    {
    

		#region All logging methods


        #region Log



		/// <summary>
		/// Writes log message at the specified level with specified parameters.
		/// </summary>
		/// <param name="level">Log level for message</param>
		/// <param name="message">Log message</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Log(LogLevel level, string message, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && Level.IsEnabled(level))
			{
				this.WriteLog(level, null, message, null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at the specified level with specified parameters.
		/// </summary>
		/// <param name="level">Log level for message</param>
		/// <param name="message">Log message</param>
		/// <param name="context">Log message context.
		/// Additional parameter that can be used to filter log messages.</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Log(LogLevel level, string message, string context, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && Level.IsEnabled(level))
			{
				this.WriteLog(level, null, message, context, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at the specified level with specified parameters.
		/// </summary>
		/// <param name="level">Log level for message</param>
		/// <param name="message">Log message</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Log(LogLevel level, Exception exception, string message, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && Level.IsEnabled(level))
			{
				this.WriteLog(level, exception, message, null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at the specified level with specified parameters.
		/// </summary>
		/// <param name="level">Log level for message</param>
		/// <param name="message">Log message</param>
		/// <param name="context">Log message context.
		/// Additional parameter that can be used to filter log messages.</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Log(LogLevel level, Exception exception, string message, string context, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && Level.IsEnabled(level))
			{
				this.WriteLog(level, exception, message, context, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at the specified level with specified parameters.
		/// </summary>
		/// <param name="level">Log level for message</param>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void LogFormat(LogLevel level, string template, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && Level.IsEnabled(level))
			{
				this.WriteLog(level, null, template, null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at the specified level with specified parameters.
		/// </summary>
		/// <param name="level">Log level for message</param>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void LogFormat<TArg1>(LogLevel level, string template, TArg1 arg1, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && Level.IsEnabled(level))
			{
				this.WriteLog(level, null, string.Format(template, arg1), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at the specified level with specified parameters.
		/// </summary>
		/// <param name="level">Log level for message</param>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void LogFormat<TArg1, TArg2>(LogLevel level, string template, TArg1 arg1, TArg2 arg2, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && Level.IsEnabled(level))
			{
				this.WriteLog(level, null, string.Format(template, arg1, arg2), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at the specified level with specified parameters.
		/// </summary>
		/// <param name="level">Log level for message</param>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <typeparam name="TArg3">Third argument type</typeparam>
		/// <param name="arg3">Third argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void LogFormat<TArg1, TArg2, TArg3>(LogLevel level, string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && Level.IsEnabled(level))
			{
				this.WriteLog(level, null, string.Format(template, arg1, arg2, arg3), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at the specified level with specified parameters.
		/// </summary>
		/// <param name="level">Log level for message</param>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <typeparam name="TArg3">Third argument type</typeparam>
		/// <param name="arg3">Third argument</param>
		/// <typeparam name="TArg4">Fourth argument type</typeparam>
		/// <param name="arg4">Fourth argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void LogFormat<TArg1, TArg2, TArg3, TArg4>(LogLevel level, string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && Level.IsEnabled(level))
			{
				this.WriteLog(level, null, string.Format(template, arg1, arg2, arg3, arg4), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at the specified level with specified parameters.
		/// </summary>
		/// <param name="level">Log level for message</param>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void LogFormat(LogLevel level, Exception exception, string template, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && Level.IsEnabled(level))
			{
				this.WriteLog(level, exception, template, null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at the specified level with specified parameters.
		/// </summary>
		/// <param name="level">Log level for message</param>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void LogFormat<TArg1>(LogLevel level, Exception exception, string template, TArg1 arg1, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && Level.IsEnabled(level))
			{
				this.WriteLog(level, exception, string.Format(template, arg1), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at the specified level with specified parameters.
		/// </summary>
		/// <param name="level">Log level for message</param>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void LogFormat<TArg1, TArg2>(LogLevel level, Exception exception, string template, TArg1 arg1, TArg2 arg2, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && Level.IsEnabled(level))
			{
				this.WriteLog(level, exception, string.Format(template, arg1, arg2), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at the specified level with specified parameters.
		/// </summary>
		/// <param name="level">Log level for message</param>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <typeparam name="TArg3">Third argument type</typeparam>
		/// <param name="arg3">Third argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void LogFormat<TArg1, TArg2, TArg3>(LogLevel level, Exception exception, string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && Level.IsEnabled(level))
			{
				this.WriteLog(level, exception, string.Format(template, arg1, arg2, arg3), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at the specified level with specified parameters.
		/// </summary>
		/// <param name="level">Log level for message</param>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <typeparam name="TArg3">Third argument type</typeparam>
		/// <param name="arg3">Third argument</param>
		/// <typeparam name="TArg4">Fourth argument type</typeparam>
		/// <param name="arg4">Fourth argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void LogFormat<TArg1, TArg2, TArg3, TArg4>(LogLevel level, Exception exception, string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && Level.IsEnabled(level))
			{
				this.WriteLog(level, exception, string.Format(template, arg1, arg2, arg3, arg4), null, @class, method, filePath, lineNumber);
			}
		}

	
		#endregion


        #region Trace



		/// <summary>
		/// Writes log message at Trace level with specified parameters.
		/// </summary>
		/// <param name="message">Log message</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Trace(string message, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isTraceEnabled)
			{
				this.WriteLog(LogLevel.Trace, null, message, null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Trace level with specified parameters.
		/// </summary>
		/// <param name="message">Log message</param>
		/// <param name="context">Log message context.
		/// Additional parameter that can be used to filter log messages.</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Trace(string message, string context, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isTraceEnabled)
			{
				this.WriteLog(LogLevel.Trace, null, message, context, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Trace level with specified parameters.
		/// </summary>
		/// <param name="message">Log message</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Trace(Exception exception, string message, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isTraceEnabled)
			{
				this.WriteLog(LogLevel.Trace, exception, message, null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Trace level with specified parameters.
		/// </summary>
		/// <param name="message">Log message</param>
		/// <param name="context">Log message context.
		/// Additional parameter that can be used to filter log messages.</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Trace(Exception exception, string message, string context, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isTraceEnabled)
			{
				this.WriteLog(LogLevel.Trace, exception, message, context, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Trace level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void TraceFormat(string template, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isTraceEnabled)
			{
				this.WriteLog(LogLevel.Trace, null, template, null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Trace level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void TraceFormat<TArg1>(string template, TArg1 arg1, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isTraceEnabled)
			{
				this.WriteLog(LogLevel.Trace, null, string.Format(template, arg1), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Trace level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void TraceFormat<TArg1, TArg2>(string template, TArg1 arg1, TArg2 arg2, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isTraceEnabled)
			{
				this.WriteLog(LogLevel.Trace, null, string.Format(template, arg1, arg2), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Trace level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <typeparam name="TArg3">Third argument type</typeparam>
		/// <param name="arg3">Third argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void TraceFormat<TArg1, TArg2, TArg3>(string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isTraceEnabled)
			{
				this.WriteLog(LogLevel.Trace, null, string.Format(template, arg1, arg2, arg3), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Trace level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <typeparam name="TArg3">Third argument type</typeparam>
		/// <param name="arg3">Third argument</param>
		/// <typeparam name="TArg4">Fourth argument type</typeparam>
		/// <param name="arg4">Fourth argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void TraceFormat<TArg1, TArg2, TArg3, TArg4>(string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isTraceEnabled)
			{
				this.WriteLog(LogLevel.Trace, null, string.Format(template, arg1, arg2, arg3, arg4), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Trace level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void TraceFormat(Exception exception, string template, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isTraceEnabled)
			{
				this.WriteLog(LogLevel.Trace, exception, template, null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Trace level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void TraceFormat<TArg1>(Exception exception, string template, TArg1 arg1, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isTraceEnabled)
			{
				this.WriteLog(LogLevel.Trace, exception, string.Format(template, arg1), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Trace level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void TraceFormat<TArg1, TArg2>(Exception exception, string template, TArg1 arg1, TArg2 arg2, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isTraceEnabled)
			{
				this.WriteLog(LogLevel.Trace, exception, string.Format(template, arg1, arg2), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Trace level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <typeparam name="TArg3">Third argument type</typeparam>
		/// <param name="arg3">Third argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void TraceFormat<TArg1, TArg2, TArg3>(Exception exception, string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isTraceEnabled)
			{
				this.WriteLog(LogLevel.Trace, exception, string.Format(template, arg1, arg2, arg3), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Trace level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <typeparam name="TArg3">Third argument type</typeparam>
		/// <param name="arg3">Third argument</param>
		/// <typeparam name="TArg4">Fourth argument type</typeparam>
		/// <param name="arg4">Fourth argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void TraceFormat<TArg1, TArg2, TArg3, TArg4>(Exception exception, string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isTraceEnabled)
			{
				this.WriteLog(LogLevel.Trace, exception, string.Format(template, arg1, arg2, arg3, arg4), null, @class, method, filePath, lineNumber);
			}
		}

	
		#endregion


        #region Debug



		/// <summary>
		/// Writes log message at Debug level with specified parameters.
		/// </summary>
		/// <param name="message">Log message</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Debug(string message, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isDebugEnabled)
			{
				this.WriteLog(LogLevel.Debug, null, message, null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Debug level with specified parameters.
		/// </summary>
		/// <param name="message">Log message</param>
		/// <param name="context">Log message context.
		/// Additional parameter that can be used to filter log messages.</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Debug(string message, string context, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isDebugEnabled)
			{
				this.WriteLog(LogLevel.Debug, null, message, context, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Debug level with specified parameters.
		/// </summary>
		/// <param name="message">Log message</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Debug(Exception exception, string message, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isDebugEnabled)
			{
				this.WriteLog(LogLevel.Debug, exception, message, null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Debug level with specified parameters.
		/// </summary>
		/// <param name="message">Log message</param>
		/// <param name="context">Log message context.
		/// Additional parameter that can be used to filter log messages.</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Debug(Exception exception, string message, string context, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isDebugEnabled)
			{
				this.WriteLog(LogLevel.Debug, exception, message, context, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Debug level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void DebugFormat(string template, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isDebugEnabled)
			{
				this.WriteLog(LogLevel.Debug, null, template, null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Debug level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void DebugFormat<TArg1>(string template, TArg1 arg1, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isDebugEnabled)
			{
				this.WriteLog(LogLevel.Debug, null, string.Format(template, arg1), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Debug level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void DebugFormat<TArg1, TArg2>(string template, TArg1 arg1, TArg2 arg2, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isDebugEnabled)
			{
				this.WriteLog(LogLevel.Debug, null, string.Format(template, arg1, arg2), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Debug level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <typeparam name="TArg3">Third argument type</typeparam>
		/// <param name="arg3">Third argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void DebugFormat<TArg1, TArg2, TArg3>(string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isDebugEnabled)
			{
				this.WriteLog(LogLevel.Debug, null, string.Format(template, arg1, arg2, arg3), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Debug level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <typeparam name="TArg3">Third argument type</typeparam>
		/// <param name="arg3">Third argument</param>
		/// <typeparam name="TArg4">Fourth argument type</typeparam>
		/// <param name="arg4">Fourth argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void DebugFormat<TArg1, TArg2, TArg3, TArg4>(string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isDebugEnabled)
			{
				this.WriteLog(LogLevel.Debug, null, string.Format(template, arg1, arg2, arg3, arg4), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Debug level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void DebugFormat(Exception exception, string template, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isDebugEnabled)
			{
				this.WriteLog(LogLevel.Debug, exception, template, null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Debug level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void DebugFormat<TArg1>(Exception exception, string template, TArg1 arg1, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isDebugEnabled)
			{
				this.WriteLog(LogLevel.Debug, exception, string.Format(template, arg1), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Debug level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void DebugFormat<TArg1, TArg2>(Exception exception, string template, TArg1 arg1, TArg2 arg2, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isDebugEnabled)
			{
				this.WriteLog(LogLevel.Debug, exception, string.Format(template, arg1, arg2), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Debug level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <typeparam name="TArg3">Third argument type</typeparam>
		/// <param name="arg3">Third argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void DebugFormat<TArg1, TArg2, TArg3>(Exception exception, string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isDebugEnabled)
			{
				this.WriteLog(LogLevel.Debug, exception, string.Format(template, arg1, arg2, arg3), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Debug level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <typeparam name="TArg3">Third argument type</typeparam>
		/// <param name="arg3">Third argument</param>
		/// <typeparam name="TArg4">Fourth argument type</typeparam>
		/// <param name="arg4">Fourth argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void DebugFormat<TArg1, TArg2, TArg3, TArg4>(Exception exception, string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isDebugEnabled)
			{
				this.WriteLog(LogLevel.Debug, exception, string.Format(template, arg1, arg2, arg3, arg4), null, @class, method, filePath, lineNumber);
			}
		}

	
		#endregion


        #region Info



		/// <summary>
		/// Writes log message at Info level with specified parameters.
		/// </summary>
		/// <param name="message">Log message</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Info(string message, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isInfoEnabled)
			{
				this.WriteLog(LogLevel.Info, null, message, null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Info level with specified parameters.
		/// </summary>
		/// <param name="message">Log message</param>
		/// <param name="context">Log message context.
		/// Additional parameter that can be used to filter log messages.</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Info(string message, string context, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isInfoEnabled)
			{
				this.WriteLog(LogLevel.Info, null, message, context, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Info level with specified parameters.
		/// </summary>
		/// <param name="message">Log message</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Info(Exception exception, string message, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isInfoEnabled)
			{
				this.WriteLog(LogLevel.Info, exception, message, null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Info level with specified parameters.
		/// </summary>
		/// <param name="message">Log message</param>
		/// <param name="context">Log message context.
		/// Additional parameter that can be used to filter log messages.</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Info(Exception exception, string message, string context, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isInfoEnabled)
			{
				this.WriteLog(LogLevel.Info, exception, message, context, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Info level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void InfoFormat(string template, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isInfoEnabled)
			{
				this.WriteLog(LogLevel.Info, null, template, null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Info level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void InfoFormat<TArg1>(string template, TArg1 arg1, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isInfoEnabled)
			{
				this.WriteLog(LogLevel.Info, null, string.Format(template, arg1), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Info level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void InfoFormat<TArg1, TArg2>(string template, TArg1 arg1, TArg2 arg2, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isInfoEnabled)
			{
				this.WriteLog(LogLevel.Info, null, string.Format(template, arg1, arg2), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Info level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <typeparam name="TArg3">Third argument type</typeparam>
		/// <param name="arg3">Third argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void InfoFormat<TArg1, TArg2, TArg3>(string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isInfoEnabled)
			{
				this.WriteLog(LogLevel.Info, null, string.Format(template, arg1, arg2, arg3), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Info level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <typeparam name="TArg3">Third argument type</typeparam>
		/// <param name="arg3">Third argument</param>
		/// <typeparam name="TArg4">Fourth argument type</typeparam>
		/// <param name="arg4">Fourth argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void InfoFormat<TArg1, TArg2, TArg3, TArg4>(string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isInfoEnabled)
			{
				this.WriteLog(LogLevel.Info, null, string.Format(template, arg1, arg2, arg3, arg4), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Info level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void InfoFormat(Exception exception, string template, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isInfoEnabled)
			{
				this.WriteLog(LogLevel.Info, exception, template, null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Info level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void InfoFormat<TArg1>(Exception exception, string template, TArg1 arg1, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isInfoEnabled)
			{
				this.WriteLog(LogLevel.Info, exception, string.Format(template, arg1), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Info level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void InfoFormat<TArg1, TArg2>(Exception exception, string template, TArg1 arg1, TArg2 arg2, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isInfoEnabled)
			{
				this.WriteLog(LogLevel.Info, exception, string.Format(template, arg1, arg2), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Info level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <typeparam name="TArg3">Third argument type</typeparam>
		/// <param name="arg3">Third argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void InfoFormat<TArg1, TArg2, TArg3>(Exception exception, string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isInfoEnabled)
			{
				this.WriteLog(LogLevel.Info, exception, string.Format(template, arg1, arg2, arg3), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Info level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <typeparam name="TArg3">Third argument type</typeparam>
		/// <param name="arg3">Third argument</param>
		/// <typeparam name="TArg4">Fourth argument type</typeparam>
		/// <param name="arg4">Fourth argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void InfoFormat<TArg1, TArg2, TArg3, TArg4>(Exception exception, string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isInfoEnabled)
			{
				this.WriteLog(LogLevel.Info, exception, string.Format(template, arg1, arg2, arg3, arg4), null, @class, method, filePath, lineNumber);
			}
		}

	
		#endregion


        #region Warn



		/// <summary>
		/// Writes log message at Warn level with specified parameters.
		/// </summary>
		/// <param name="message">Log message</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Warn(string message, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isWarnEnabled)
			{
				this.WriteLog(LogLevel.Warn, null, message, null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Warn level with specified parameters.
		/// </summary>
		/// <param name="message">Log message</param>
		/// <param name="context">Log message context.
		/// Additional parameter that can be used to filter log messages.</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Warn(string message, string context, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isWarnEnabled)
			{
				this.WriteLog(LogLevel.Warn, null, message, context, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Warn level with specified parameters.
		/// </summary>
		/// <param name="message">Log message</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Warn(Exception exception, string message, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isWarnEnabled)
			{
				this.WriteLog(LogLevel.Warn, exception, message, null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Warn level with specified parameters.
		/// </summary>
		/// <param name="message">Log message</param>
		/// <param name="context">Log message context.
		/// Additional parameter that can be used to filter log messages.</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Warn(Exception exception, string message, string context, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isWarnEnabled)
			{
				this.WriteLog(LogLevel.Warn, exception, message, context, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Warn level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void WarnFormat(string template, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isWarnEnabled)
			{
				this.WriteLog(LogLevel.Warn, null, template, null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Warn level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void WarnFormat<TArg1>(string template, TArg1 arg1, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isWarnEnabled)
			{
				this.WriteLog(LogLevel.Warn, null, string.Format(template, arg1), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Warn level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void WarnFormat<TArg1, TArg2>(string template, TArg1 arg1, TArg2 arg2, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isWarnEnabled)
			{
				this.WriteLog(LogLevel.Warn, null, string.Format(template, arg1, arg2), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Warn level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <typeparam name="TArg3">Third argument type</typeparam>
		/// <param name="arg3">Third argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void WarnFormat<TArg1, TArg2, TArg3>(string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isWarnEnabled)
			{
				this.WriteLog(LogLevel.Warn, null, string.Format(template, arg1, arg2, arg3), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Warn level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <typeparam name="TArg3">Third argument type</typeparam>
		/// <param name="arg3">Third argument</param>
		/// <typeparam name="TArg4">Fourth argument type</typeparam>
		/// <param name="arg4">Fourth argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void WarnFormat<TArg1, TArg2, TArg3, TArg4>(string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isWarnEnabled)
			{
				this.WriteLog(LogLevel.Warn, null, string.Format(template, arg1, arg2, arg3, arg4), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Warn level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void WarnFormat(Exception exception, string template, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isWarnEnabled)
			{
				this.WriteLog(LogLevel.Warn, exception, template, null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Warn level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void WarnFormat<TArg1>(Exception exception, string template, TArg1 arg1, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isWarnEnabled)
			{
				this.WriteLog(LogLevel.Warn, exception, string.Format(template, arg1), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Warn level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void WarnFormat<TArg1, TArg2>(Exception exception, string template, TArg1 arg1, TArg2 arg2, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isWarnEnabled)
			{
				this.WriteLog(LogLevel.Warn, exception, string.Format(template, arg1, arg2), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Warn level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <typeparam name="TArg3">Third argument type</typeparam>
		/// <param name="arg3">Third argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void WarnFormat<TArg1, TArg2, TArg3>(Exception exception, string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isWarnEnabled)
			{
				this.WriteLog(LogLevel.Warn, exception, string.Format(template, arg1, arg2, arg3), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Warn level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <typeparam name="TArg3">Third argument type</typeparam>
		/// <param name="arg3">Third argument</param>
		/// <typeparam name="TArg4">Fourth argument type</typeparam>
		/// <param name="arg4">Fourth argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void WarnFormat<TArg1, TArg2, TArg3, TArg4>(Exception exception, string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isWarnEnabled)
			{
				this.WriteLog(LogLevel.Warn, exception, string.Format(template, arg1, arg2, arg3, arg4), null, @class, method, filePath, lineNumber);
			}
		}

	
		#endregion


        #region Error



		/// <summary>
		/// Writes log message at Error level with specified parameters.
		/// </summary>
		/// <param name="message">Log message</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Error(string message, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isErrorEnabled)
			{
				this.WriteLog(LogLevel.Error, null, message, null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Error level with specified parameters.
		/// </summary>
		/// <param name="message">Log message</param>
		/// <param name="context">Log message context.
		/// Additional parameter that can be used to filter log messages.</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Error(string message, string context, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isErrorEnabled)
			{
				this.WriteLog(LogLevel.Error, null, message, context, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Error level with specified parameters.
		/// </summary>
		/// <param name="message">Log message</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Error(Exception exception, string message, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isErrorEnabled)
			{
				this.WriteLog(LogLevel.Error, exception, message, null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Error level with specified parameters.
		/// </summary>
		/// <param name="message">Log message</param>
		/// <param name="context">Log message context.
		/// Additional parameter that can be used to filter log messages.</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Error(Exception exception, string message, string context, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isErrorEnabled)
			{
				this.WriteLog(LogLevel.Error, exception, message, context, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Error level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ErrorFormat(string template, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isErrorEnabled)
			{
				this.WriteLog(LogLevel.Error, null, template, null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Error level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ErrorFormat<TArg1>(string template, TArg1 arg1, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isErrorEnabled)
			{
				this.WriteLog(LogLevel.Error, null, string.Format(template, arg1), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Error level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ErrorFormat<TArg1, TArg2>(string template, TArg1 arg1, TArg2 arg2, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isErrorEnabled)
			{
				this.WriteLog(LogLevel.Error, null, string.Format(template, arg1, arg2), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Error level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <typeparam name="TArg3">Third argument type</typeparam>
		/// <param name="arg3">Third argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ErrorFormat<TArg1, TArg2, TArg3>(string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isErrorEnabled)
			{
				this.WriteLog(LogLevel.Error, null, string.Format(template, arg1, arg2, arg3), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Error level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <typeparam name="TArg3">Third argument type</typeparam>
		/// <param name="arg3">Third argument</param>
		/// <typeparam name="TArg4">Fourth argument type</typeparam>
		/// <param name="arg4">Fourth argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ErrorFormat<TArg1, TArg2, TArg3, TArg4>(string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isErrorEnabled)
			{
				this.WriteLog(LogLevel.Error, null, string.Format(template, arg1, arg2, arg3, arg4), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Error level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ErrorFormat(Exception exception, string template, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isErrorEnabled)
			{
				this.WriteLog(LogLevel.Error, exception, template, null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Error level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ErrorFormat<TArg1>(Exception exception, string template, TArg1 arg1, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isErrorEnabled)
			{
				this.WriteLog(LogLevel.Error, exception, string.Format(template, arg1), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Error level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ErrorFormat<TArg1, TArg2>(Exception exception, string template, TArg1 arg1, TArg2 arg2, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isErrorEnabled)
			{
				this.WriteLog(LogLevel.Error, exception, string.Format(template, arg1, arg2), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Error level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <typeparam name="TArg3">Third argument type</typeparam>
		/// <param name="arg3">Third argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ErrorFormat<TArg1, TArg2, TArg3>(Exception exception, string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isErrorEnabled)
			{
				this.WriteLog(LogLevel.Error, exception, string.Format(template, arg1, arg2, arg3), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Error level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <typeparam name="TArg3">Third argument type</typeparam>
		/// <param name="arg3">Third argument</param>
		/// <typeparam name="TArg4">Fourth argument type</typeparam>
		/// <param name="arg4">Fourth argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ErrorFormat<TArg1, TArg2, TArg3, TArg4>(Exception exception, string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isErrorEnabled)
			{
				this.WriteLog(LogLevel.Error, exception, string.Format(template, arg1, arg2, arg3, arg4), null, @class, method, filePath, lineNumber);
			}
		}

	
		#endregion


        #region Fatal



		/// <summary>
		/// Writes log message at Fatal level with specified parameters.
		/// </summary>
		/// <param name="message">Log message</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Fatal(string message, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isFatalEnabled)
			{
				this.WriteLog(LogLevel.Fatal, null, message, null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Fatal level with specified parameters.
		/// </summary>
		/// <param name="message">Log message</param>
		/// <param name="context">Log message context.
		/// Additional parameter that can be used to filter log messages.</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Fatal(string message, string context, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isFatalEnabled)
			{
				this.WriteLog(LogLevel.Fatal, null, message, context, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Fatal level with specified parameters.
		/// </summary>
		/// <param name="message">Log message</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Fatal(Exception exception, string message, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isFatalEnabled)
			{
				this.WriteLog(LogLevel.Fatal, exception, message, null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Fatal level with specified parameters.
		/// </summary>
		/// <param name="message">Log message</param>
		/// <param name="context">Log message context.
		/// Additional parameter that can be used to filter log messages.</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Fatal(Exception exception, string message, string context, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isFatalEnabled)
			{
				this.WriteLog(LogLevel.Fatal, exception, message, context, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Fatal level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void FatalFormat(string template, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isFatalEnabled)
			{
				this.WriteLog(LogLevel.Fatal, null, template, null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Fatal level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void FatalFormat<TArg1>(string template, TArg1 arg1, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isFatalEnabled)
			{
				this.WriteLog(LogLevel.Fatal, null, string.Format(template, arg1), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Fatal level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void FatalFormat<TArg1, TArg2>(string template, TArg1 arg1, TArg2 arg2, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isFatalEnabled)
			{
				this.WriteLog(LogLevel.Fatal, null, string.Format(template, arg1, arg2), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Fatal level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <typeparam name="TArg3">Third argument type</typeparam>
		/// <param name="arg3">Third argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void FatalFormat<TArg1, TArg2, TArg3>(string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isFatalEnabled)
			{
				this.WriteLog(LogLevel.Fatal, null, string.Format(template, arg1, arg2, arg3), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Fatal level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <typeparam name="TArg3">Third argument type</typeparam>
		/// <param name="arg3">Third argument</param>
		/// <typeparam name="TArg4">Fourth argument type</typeparam>
		/// <param name="arg4">Fourth argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void FatalFormat<TArg1, TArg2, TArg3, TArg4>(string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isFatalEnabled)
			{
				this.WriteLog(LogLevel.Fatal, null, string.Format(template, arg1, arg2, arg3, arg4), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Fatal level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void FatalFormat(Exception exception, string template, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isFatalEnabled)
			{
				this.WriteLog(LogLevel.Fatal, exception, template, null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Fatal level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void FatalFormat<TArg1>(Exception exception, string template, TArg1 arg1, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isFatalEnabled)
			{
				this.WriteLog(LogLevel.Fatal, exception, string.Format(template, arg1), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Fatal level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void FatalFormat<TArg1, TArg2>(Exception exception, string template, TArg1 arg1, TArg2 arg2, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isFatalEnabled)
			{
				this.WriteLog(LogLevel.Fatal, exception, string.Format(template, arg1, arg2), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Fatal level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <typeparam name="TArg3">Third argument type</typeparam>
		/// <param name="arg3">Third argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void FatalFormat<TArg1, TArg2, TArg3>(Exception exception, string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isFatalEnabled)
			{
				this.WriteLog(LogLevel.Fatal, exception, string.Format(template, arg1, arg2, arg3), null, @class, method, filePath, lineNumber);
			}
		}

	

		/// <summary>
		/// Writes log message at Fatal level with specified parameters.
		/// </summary>
		/// <param name="template">Message template (similar to string.Format)</param>
		/// <param name="exception">Exception object to be logged</param>
		/// <typeparam name="TArg1">First argument type</typeparam>
		/// <param name="arg1">First argument</param>
		/// <typeparam name="TArg2">Second argument type</typeparam>
		/// <param name="arg2">Second argument</param>
		/// <typeparam name="TArg3">Third argument type</typeparam>
		/// <param name="arg3">Third argument</param>
		/// <typeparam name="TArg4">Fourth argument type</typeparam>
		/// <param name="arg4">Fourth argument</param>
		/// <param name="guard">Special guard parameter</param>
		/// <param name="class">Name of the class from which logging is performed.</param>
		/// <param name="lineNumber">Auto-completed parameter! Line number in source code file 
		/// at which the logging performed.</param>
		/// <param name="filePath">Auto-completed parameter! Source code file name 
		/// from which logging performed</param>
		/// <param name="method">Auto-completed parameter! Method name 
		/// from which logging performed.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void FatalFormat<TArg1, TArg2, TArg3, TArg4>(Exception exception, string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			if (_isEnabled && _isFatalEnabled)
			{
				this.WriteLog(LogLevel.Fatal, exception, string.Format(template, arg1, arg2, arg3, arg4), null, @class, method, filePath, lineNumber);
			}
		}

	
		#endregion

		#endregion
    }
}