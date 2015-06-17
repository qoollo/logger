 
using System;
using Qoollo.Logger.Common;
using Qoollo.Logger.Helpers;
using System.Runtime.CompilerServices;

namespace Qoollo.Logger
{
    partial class LoggerStatic
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
		public static void Log(LogLevel level, string message, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.Log(level, message, guard, @class, method, filePath, lineNumber);
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
		public static void Log(LogLevel level, string message, string context, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.Log(level, message, context, guard, @class, method, filePath, lineNumber);
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
		public static void Log(LogLevel level, Exception exception, string message, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.Log(level, exception, message, guard, @class, method, filePath, lineNumber);
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
		public static void Log(LogLevel level, Exception exception, string message, string context, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.Log(level, exception, message, context, guard, @class, method, filePath, lineNumber);
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
		public static void LogFormat(LogLevel level, string template, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.LogFormat(level, template, guard, @class, method, filePath, lineNumber);
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
		public static void LogFormat<TArg1>(LogLevel level, string template, TArg1 arg1, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.LogFormat(level, template, arg1, guard, @class, method, filePath, lineNumber);
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
		public static void LogFormat<TArg1, TArg2>(LogLevel level, string template, TArg1 arg1, TArg2 arg2, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.LogFormat(level, template, arg1, arg2, guard, @class, method, filePath, lineNumber);
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
		public static void LogFormat<TArg1, TArg2, TArg3>(LogLevel level, string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.LogFormat(level, template, arg1, arg2, arg3, guard, @class, method, filePath, lineNumber);
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
		public static void LogFormat<TArg1, TArg2, TArg3, TArg4>(LogLevel level, string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.LogFormat(level, template, arg1, arg2, arg3, arg4, guard, @class, method, filePath, lineNumber);
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
		public static void LogFormat(LogLevel level, Exception exception, string template, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.LogFormat(level, exception, template, guard, @class, method, filePath, lineNumber);
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
		public static void LogFormat<TArg1>(LogLevel level, Exception exception, string template, TArg1 arg1, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.LogFormat(level, exception, template, arg1, guard, @class, method, filePath, lineNumber);
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
		public static void LogFormat<TArg1, TArg2>(LogLevel level, Exception exception, string template, TArg1 arg1, TArg2 arg2, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.LogFormat(level, exception, template, arg1, arg2, guard, @class, method, filePath, lineNumber);
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
		public static void LogFormat<TArg1, TArg2, TArg3>(LogLevel level, Exception exception, string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.LogFormat(level, exception, template, arg1, arg2, arg3, guard, @class, method, filePath, lineNumber);
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
		public static void LogFormat<TArg1, TArg2, TArg3, TArg4>(LogLevel level, Exception exception, string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.LogFormat(level, exception, template, arg1, arg2, arg3, arg4, guard, @class, method, filePath, lineNumber);
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
		public static void Trace(string message, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.Trace(message, guard, @class, method, filePath, lineNumber);
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
		public static void Trace(string message, string context, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.Trace(message, context, guard, @class, method, filePath, lineNumber);
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
		public static void Trace(Exception exception, string message, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.Trace(exception, message, guard, @class, method, filePath, lineNumber);
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
		public static void Trace(Exception exception, string message, string context, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.Trace(exception, message, context, guard, @class, method, filePath, lineNumber);
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
		public static void TraceFormat(string template, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.TraceFormat(template, guard, @class, method, filePath, lineNumber);
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
		public static void TraceFormat<TArg1>(string template, TArg1 arg1, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.TraceFormat(template, arg1, guard, @class, method, filePath, lineNumber);
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
		public static void TraceFormat<TArg1, TArg2>(string template, TArg1 arg1, TArg2 arg2, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.TraceFormat(template, arg1, arg2, guard, @class, method, filePath, lineNumber);
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
		public static void TraceFormat<TArg1, TArg2, TArg3>(string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.TraceFormat(template, arg1, arg2, arg3, guard, @class, method, filePath, lineNumber);
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
		public static void TraceFormat<TArg1, TArg2, TArg3, TArg4>(string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.TraceFormat(template, arg1, arg2, arg3, arg4, guard, @class, method, filePath, lineNumber);
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
		public static void TraceFormat(Exception exception, string template, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.TraceFormat(exception, template, guard, @class, method, filePath, lineNumber);
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
		public static void TraceFormat<TArg1>(Exception exception, string template, TArg1 arg1, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.TraceFormat(exception, template, arg1, guard, @class, method, filePath, lineNumber);
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
		public static void TraceFormat<TArg1, TArg2>(Exception exception, string template, TArg1 arg1, TArg2 arg2, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.TraceFormat(exception, template, arg1, arg2, guard, @class, method, filePath, lineNumber);
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
		public static void TraceFormat<TArg1, TArg2, TArg3>(Exception exception, string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.TraceFormat(exception, template, arg1, arg2, arg3, guard, @class, method, filePath, lineNumber);
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
		public static void TraceFormat<TArg1, TArg2, TArg3, TArg4>(Exception exception, string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.TraceFormat(exception, template, arg1, arg2, arg3, arg4, guard, @class, method, filePath, lineNumber);
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
		public static void Debug(string message, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.Debug(message, guard, @class, method, filePath, lineNumber);
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
		public static void Debug(string message, string context, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.Debug(message, context, guard, @class, method, filePath, lineNumber);
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
		public static void Debug(Exception exception, string message, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.Debug(exception, message, guard, @class, method, filePath, lineNumber);
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
		public static void Debug(Exception exception, string message, string context, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.Debug(exception, message, context, guard, @class, method, filePath, lineNumber);
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
		public static void DebugFormat(string template, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.DebugFormat(template, guard, @class, method, filePath, lineNumber);
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
		public static void DebugFormat<TArg1>(string template, TArg1 arg1, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.DebugFormat(template, arg1, guard, @class, method, filePath, lineNumber);
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
		public static void DebugFormat<TArg1, TArg2>(string template, TArg1 arg1, TArg2 arg2, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.DebugFormat(template, arg1, arg2, guard, @class, method, filePath, lineNumber);
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
		public static void DebugFormat<TArg1, TArg2, TArg3>(string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.DebugFormat(template, arg1, arg2, arg3, guard, @class, method, filePath, lineNumber);
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
		public static void DebugFormat<TArg1, TArg2, TArg3, TArg4>(string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.DebugFormat(template, arg1, arg2, arg3, arg4, guard, @class, method, filePath, lineNumber);
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
		public static void DebugFormat(Exception exception, string template, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.DebugFormat(exception, template, guard, @class, method, filePath, lineNumber);
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
		public static void DebugFormat<TArg1>(Exception exception, string template, TArg1 arg1, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.DebugFormat(exception, template, arg1, guard, @class, method, filePath, lineNumber);
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
		public static void DebugFormat<TArg1, TArg2>(Exception exception, string template, TArg1 arg1, TArg2 arg2, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.DebugFormat(exception, template, arg1, arg2, guard, @class, method, filePath, lineNumber);
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
		public static void DebugFormat<TArg1, TArg2, TArg3>(Exception exception, string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.DebugFormat(exception, template, arg1, arg2, arg3, guard, @class, method, filePath, lineNumber);
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
		public static void DebugFormat<TArg1, TArg2, TArg3, TArg4>(Exception exception, string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.DebugFormat(exception, template, arg1, arg2, arg3, arg4, guard, @class, method, filePath, lineNumber);
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
		public static void Info(string message, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.Info(message, guard, @class, method, filePath, lineNumber);
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
		public static void Info(string message, string context, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.Info(message, context, guard, @class, method, filePath, lineNumber);
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
		public static void Info(Exception exception, string message, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.Info(exception, message, guard, @class, method, filePath, lineNumber);
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
		public static void Info(Exception exception, string message, string context, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.Info(exception, message, context, guard, @class, method, filePath, lineNumber);
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
		public static void InfoFormat(string template, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.InfoFormat(template, guard, @class, method, filePath, lineNumber);
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
		public static void InfoFormat<TArg1>(string template, TArg1 arg1, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.InfoFormat(template, arg1, guard, @class, method, filePath, lineNumber);
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
		public static void InfoFormat<TArg1, TArg2>(string template, TArg1 arg1, TArg2 arg2, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.InfoFormat(template, arg1, arg2, guard, @class, method, filePath, lineNumber);
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
		public static void InfoFormat<TArg1, TArg2, TArg3>(string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.InfoFormat(template, arg1, arg2, arg3, guard, @class, method, filePath, lineNumber);
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
		public static void InfoFormat<TArg1, TArg2, TArg3, TArg4>(string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.InfoFormat(template, arg1, arg2, arg3, arg4, guard, @class, method, filePath, lineNumber);
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
		public static void InfoFormat(Exception exception, string template, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.InfoFormat(exception, template, guard, @class, method, filePath, lineNumber);
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
		public static void InfoFormat<TArg1>(Exception exception, string template, TArg1 arg1, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.InfoFormat(exception, template, arg1, guard, @class, method, filePath, lineNumber);
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
		public static void InfoFormat<TArg1, TArg2>(Exception exception, string template, TArg1 arg1, TArg2 arg2, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.InfoFormat(exception, template, arg1, arg2, guard, @class, method, filePath, lineNumber);
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
		public static void InfoFormat<TArg1, TArg2, TArg3>(Exception exception, string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.InfoFormat(exception, template, arg1, arg2, arg3, guard, @class, method, filePath, lineNumber);
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
		public static void InfoFormat<TArg1, TArg2, TArg3, TArg4>(Exception exception, string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.InfoFormat(exception, template, arg1, arg2, arg3, arg4, guard, @class, method, filePath, lineNumber);
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
		public static void Warn(string message, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.Warn(message, guard, @class, method, filePath, lineNumber);
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
		public static void Warn(string message, string context, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.Warn(message, context, guard, @class, method, filePath, lineNumber);
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
		public static void Warn(Exception exception, string message, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.Warn(exception, message, guard, @class, method, filePath, lineNumber);
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
		public static void Warn(Exception exception, string message, string context, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.Warn(exception, message, context, guard, @class, method, filePath, lineNumber);
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
		public static void WarnFormat(string template, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.WarnFormat(template, guard, @class, method, filePath, lineNumber);
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
		public static void WarnFormat<TArg1>(string template, TArg1 arg1, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.WarnFormat(template, arg1, guard, @class, method, filePath, lineNumber);
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
		public static void WarnFormat<TArg1, TArg2>(string template, TArg1 arg1, TArg2 arg2, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.WarnFormat(template, arg1, arg2, guard, @class, method, filePath, lineNumber);
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
		public static void WarnFormat<TArg1, TArg2, TArg3>(string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.WarnFormat(template, arg1, arg2, arg3, guard, @class, method, filePath, lineNumber);
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
		public static void WarnFormat<TArg1, TArg2, TArg3, TArg4>(string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.WarnFormat(template, arg1, arg2, arg3, arg4, guard, @class, method, filePath, lineNumber);
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
		public static void WarnFormat(Exception exception, string template, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.WarnFormat(exception, template, guard, @class, method, filePath, lineNumber);
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
		public static void WarnFormat<TArg1>(Exception exception, string template, TArg1 arg1, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.WarnFormat(exception, template, arg1, guard, @class, method, filePath, lineNumber);
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
		public static void WarnFormat<TArg1, TArg2>(Exception exception, string template, TArg1 arg1, TArg2 arg2, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.WarnFormat(exception, template, arg1, arg2, guard, @class, method, filePath, lineNumber);
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
		public static void WarnFormat<TArg1, TArg2, TArg3>(Exception exception, string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.WarnFormat(exception, template, arg1, arg2, arg3, guard, @class, method, filePath, lineNumber);
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
		public static void WarnFormat<TArg1, TArg2, TArg3, TArg4>(Exception exception, string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.WarnFormat(exception, template, arg1, arg2, arg3, arg4, guard, @class, method, filePath, lineNumber);
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
		public static void Error(string message, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.Error(message, guard, @class, method, filePath, lineNumber);
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
		public static void Error(string message, string context, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.Error(message, context, guard, @class, method, filePath, lineNumber);
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
		public static void Error(Exception exception, string message, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.Error(exception, message, guard, @class, method, filePath, lineNumber);
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
		public static void Error(Exception exception, string message, string context, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.Error(exception, message, context, guard, @class, method, filePath, lineNumber);
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
		public static void ErrorFormat(string template, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.ErrorFormat(template, guard, @class, method, filePath, lineNumber);
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
		public static void ErrorFormat<TArg1>(string template, TArg1 arg1, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.ErrorFormat(template, arg1, guard, @class, method, filePath, lineNumber);
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
		public static void ErrorFormat<TArg1, TArg2>(string template, TArg1 arg1, TArg2 arg2, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.ErrorFormat(template, arg1, arg2, guard, @class, method, filePath, lineNumber);
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
		public static void ErrorFormat<TArg1, TArg2, TArg3>(string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.ErrorFormat(template, arg1, arg2, arg3, guard, @class, method, filePath, lineNumber);
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
		public static void ErrorFormat<TArg1, TArg2, TArg3, TArg4>(string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.ErrorFormat(template, arg1, arg2, arg3, arg4, guard, @class, method, filePath, lineNumber);
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
		public static void ErrorFormat(Exception exception, string template, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.ErrorFormat(exception, template, guard, @class, method, filePath, lineNumber);
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
		public static void ErrorFormat<TArg1>(Exception exception, string template, TArg1 arg1, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.ErrorFormat(exception, template, arg1, guard, @class, method, filePath, lineNumber);
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
		public static void ErrorFormat<TArg1, TArg2>(Exception exception, string template, TArg1 arg1, TArg2 arg2, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.ErrorFormat(exception, template, arg1, arg2, guard, @class, method, filePath, lineNumber);
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
		public static void ErrorFormat<TArg1, TArg2, TArg3>(Exception exception, string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.ErrorFormat(exception, template, arg1, arg2, arg3, guard, @class, method, filePath, lineNumber);
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
		public static void ErrorFormat<TArg1, TArg2, TArg3, TArg4>(Exception exception, string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.ErrorFormat(exception, template, arg1, arg2, arg3, arg4, guard, @class, method, filePath, lineNumber);
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
		public static void Fatal(string message, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.Fatal(message, guard, @class, method, filePath, lineNumber);
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
		public static void Fatal(string message, string context, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.Fatal(message, context, guard, @class, method, filePath, lineNumber);
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
		public static void Fatal(Exception exception, string message, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.Fatal(exception, message, guard, @class, method, filePath, lineNumber);
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
		public static void Fatal(Exception exception, string message, string context, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.Fatal(exception, message, context, guard, @class, method, filePath, lineNumber);
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
		public static void FatalFormat(string template, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.FatalFormat(template, guard, @class, method, filePath, lineNumber);
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
		public static void FatalFormat<TArg1>(string template, TArg1 arg1, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.FatalFormat(template, arg1, guard, @class, method, filePath, lineNumber);
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
		public static void FatalFormat<TArg1, TArg2>(string template, TArg1 arg1, TArg2 arg2, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.FatalFormat(template, arg1, arg2, guard, @class, method, filePath, lineNumber);
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
		public static void FatalFormat<TArg1, TArg2, TArg3>(string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.FatalFormat(template, arg1, arg2, arg3, guard, @class, method, filePath, lineNumber);
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
		public static void FatalFormat<TArg1, TArg2, TArg3, TArg4>(string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.FatalFormat(template, arg1, arg2, arg3, arg4, guard, @class, method, filePath, lineNumber);
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
		public static void FatalFormat(Exception exception, string template, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.FatalFormat(exception, template, guard, @class, method, filePath, lineNumber);
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
		public static void FatalFormat<TArg1>(Exception exception, string template, TArg1 arg1, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.FatalFormat(exception, template, arg1, guard, @class, method, filePath, lineNumber);
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
		public static void FatalFormat<TArg1, TArg2>(Exception exception, string template, TArg1 arg1, TArg2 arg2, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.FatalFormat(exception, template, arg1, arg2, guard, @class, method, filePath, lineNumber);
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
		public static void FatalFormat<TArg1, TArg2, TArg3>(Exception exception, string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.FatalFormat(exception, template, arg1, arg2, arg3, guard, @class, method, filePath, lineNumber);
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
		public static void FatalFormat<TArg1, TArg2, TArg3, TArg4>(Exception exception, string template, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, ParameterGuardClass guard = null, string @class = null, [CallerMemberName] string method = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
		{
			Instance.FatalFormat(exception, template, arg1, arg2, arg3, arg4, guard, @class, method, filePath, lineNumber);
		}

	
		#endregion

		#endregion
    }
}