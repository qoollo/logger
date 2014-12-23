using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qoollo.Logger.Common;
using System;

namespace Qoollo.Logger.LoggingEventConverters
{
    internal class LoggingEventConverter : LoggingEventConverterBase
    {
        private readonly List<LoggingEventConverterBase> _converters;

        public LoggingEventConverter(List<LoggingEventConverterBase> converters)
        {
            _converters = converters;
        }

        public override string Convert(LoggingEvent data)
        {
            var builder = new StringBuilder();

            foreach (var converter in _converters)
            {
                builder.Append(converter.Convert(data));
            }

            return builder.ToString();
        }
    }

    internal class ConstConverter : LoggingEventConverterBase
    {
        private readonly string _constString;

        public ConstConverter(string constString)
        {
            _constString = constString;
        }

        public override string Convert(LoggingEvent data)
        {
            return _constString;
        }
    }

    internal class DateConverter : LoggingEventConverterBase
    {
        private readonly string _format;

        public DateConverter(string format)
        {
            if (string.IsNullOrWhiteSpace(format))
                _format = null;
            else
                _format = format;
        }

        public override string Convert(LoggingEvent data)
        {
            if (_format != null)
                return data.Date.ToString(_format);

            return data.Date.ToString();
        }
    }

    internal class LevelConverter : LoggingEventConverterBase
    {
        public override string Convert(LoggingEvent data)
        {
            return data.Level.Name;
        }
    }

    internal class MachineNameConverter : LoggingEventConverterBase
    {
        public override string Convert(LoggingEvent data)
        {
            return data.MachineName;
        }
    }

    internal class ProcessNameConverter : LoggingEventConverterBase
    {
        public override string Convert(LoggingEvent data)
        {
            return data.ProcessName;
        }
    }

    internal class ProcessIdConverter : LoggingEventConverterBase
    {
        public override string Convert(LoggingEvent data)
        {
            return data.ProcessId.ToString();
        }
    }

    internal class AssemblyConverter : LoggingEventConverterBase
    {
        public override string Convert(LoggingEvent data)
        {
            return data.Assembly;
        }
    }

    internal class NamespaceConverter : LoggingEventConverterBase
    {
        public override string Convert(LoggingEvent data)
        {
            return data.Namespace;
        }
    }

    internal class ClassConverter : LoggingEventConverterBase
    {
        public override string Convert(LoggingEvent data)
        {
            return data.Clazz;
        }
    }

    internal class MethodConverter : LoggingEventConverterBase
    {
        public override string Convert(LoggingEvent data)
        {
            return data.Method;
        }
    }

    internal class MessageConverter : LoggingEventConverterBase
    {
        public override string Convert(LoggingEvent data)
        {
            return data.Message;
        }
    }

    internal class ContextConverter : LoggingEventConverterBase
    {
        public override string Convert(LoggingEvent data)
        {
            return data.Context;
        }
    }



    internal class ExceptionConverter : LoggingEventConverterBase
    {
        public override string Convert(LoggingEvent data)
        {
            return Convert(data.Exception, data.Method);
        }

        /// <summary>
        /// Преобразование информации об исключительной ситуации в строковый вид
        /// С поддежки LogReader
        /// </summary>
        /// <param name="error">Информация об ошибке</param>
        /// <param name="methodName">Имя метода, где возникла ошибка</param>
        /// <returns></returns>
        public string Convert(Error error, string methodName = null)
        {
            if (error == null)
            {
                return null;
            }

            var builder = new StringBuilder();

            builder.Append(error.Type).Append(": ").Append(error.Message).AppendLine();
            if (error.Source != null)
                builder.Append("  Source: ").Append(error.Source).AppendLine();
            if (error.StackTrace != null)
                builder.Append("  StackTrace: ").Append(error.StackTrace.Replace(Environment.NewLine, "   " + Environment.NewLine)).AppendLine();

            Error innerError = error.InnerError;
            while (innerError != null)
            {
                builder.Append(" ==> ").Append(innerError.Type).Append(": ").Append(innerError.Message).AppendLine();
                if (innerError.Source != null)
                    builder.Append("    Source: ").Append(innerError.Source).AppendLine();
                if (innerError.StackTrace != null)
                    builder.Append("    StackTrace: ").Append(innerError.StackTrace.Replace(Environment.NewLine, "     " + Environment.NewLine)).AppendLine();
                innerError = innerError.InnerError;
            }

            return builder.ToString();
        }
    }


    internal class StackSourceConverter : LoggingEventConverterBase
    {
        public override string Convert(LoggingEvent data)
        {
            return Convert(data.StackSources);
        }

        /// <summary>
        /// Преобразование списка в строковый вид
        /// </summary>
        /// <param name="arr">Массив строк</param>
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
                if (line.Contains("."))
                {
                    builder.Append("[").Append(line).Append("]");
                }
                else
                {
                    builder.Append(line);
                }
                builder.Append(".");
            }

            if (builder.Length > 0)
                builder.Remove(builder.Length - 1, 1);

            return builder.ToString();
        }
    }
}