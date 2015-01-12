using Qoollo.Logger.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qoollo.Logger.LoggingEventConverters
{
    /// <summary>
    /// Converter factory for Legacy format
    /// </summary>
    public class LegacyConverterFactory : ConverterFactory
    {
        /// <summary>
        /// Creates converter for 'Exception' property of LoggingEvent
        /// </summary>
        /// <returns>Created converter</returns>
        public override LoggingEventConverterBase CreateExceptionConverter()
        {
            return new LegacyExceptionConverter();
        }
        /// <summary>
        /// Creates converter for 'StackSources' property of LoggingEvent
        /// </summary>
        /// <returns>Created converter</returns>
        public override LoggingEventConverterBase CreateStackSourceConverter()
        {
            return new LegacyExceptionConverter();
        }
    }


    internal class LegacyExceptionConverter : LoggingEventConverterBase
    {
        public override string Convert(LoggingEvent data)
        {
            return Convert(data.Exception, data.Method);
        }

        /// <summary>
        /// Old converter
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private string ConvertOld(Error ex)
        {
            if (ex == null)
            {
                return null;
            }

            var builder = new StringBuilder();
            builder.Append(ex.GetType().Name).Append(": ").Append(ex.Message).AppendLine();

            Error cur = ex.InnerError;

            while (cur != null)
            {
                builder.Append("+ ").Append(cur.GetType().Name).Append(": ").Append(cur.Message).AppendLine();
                cur = cur.InnerError;
            }

            builder.Append("Source: ").Append(ex.Source).AppendLine();
            builder.Append("StackTrace: ").Append(ex.StackTrace).AppendLine();

            return builder.ToString();
        }

        /// <summary>
        /// New converter
        /// </summary>
        /// <param name="error">Error</param>
        /// <param name="methodName">Method name</param>
        /// <returns></returns>
        public string Convert(Error error, string methodName = null)
        {
            if (error == null)
            {
                return null;
            }

            Error innerError;

            var builder = new StringBuilder();

            // Types
            builder.Append("|").Append(error.Type);
            innerError = error.InnerError;

            while (innerError != null)
            {
                builder.Append(" ==> ").Append(innerError.Type);
                innerError = innerError.InnerError;
            }

            // Messages
            builder.Append("|").Append(error.Message);
            innerError = error.InnerError;

            while (innerError != null)
            {
                builder.Append(" ==> ").Append(innerError.Message);
                innerError = innerError.InnerError;
            }

            // StackSources
            builder.Append("|").Append(error.Source);
            builder.Append("|").Append(methodName);
            builder.Append("|").Append(error.StackTrace);
            innerError = error.InnerError;

            while (innerError != null)
            {
                builder.Append(" ==> ").Append(innerError.StackTrace);
                innerError = innerError.InnerError;
            }

            return builder.ToString();
        }
    }

    internal class LegacyStackSourceConverter : LoggingEventConverterBase
    {
        public override string Convert(LoggingEvent data)
        {
            return Convert(data.StackSources);
        }

        /// <summary>
        /// Convert list of strings to single string (sample: 'a1:a2:a3')
        /// </summary>
        /// <param name="arr">source list</param>
        /// <returns></returns>
        public static string Convert(List<string> arr)
        {
            if (arr == null)
            {
                return null;
            }

            var builder = new StringBuilder();

            foreach (string line in arr)
            {
                builder.Append(line);
                builder.Append(":");
            }

            if (builder.Length > 0)
                builder.Remove(builder.Length - 1, 1);

            return builder.ToString();
        }
    }
}
