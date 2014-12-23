using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Qoollo.Logger.Common;
using System.Text;
using System.Diagnostics.Contracts;
using Qoollo.Logger.Exceptions;

namespace Qoollo.Logger.LoggingEventConverters
{
    /// <summary>
    /// Ключи для указания вида формата выводимой строки
    /// Все ключи указываются в фигурных скобках например: {Level}
    /// </summary>
    /// <summary>
    /// В ручную настраивомый вывод даты и времени
    /// Пример "{DateTime:yyyy-MM-dd hh:mm:ss}"
    /// Date.ToString(string format) - соответственно нужные ключи смотрим на MSDN
    /// выбор  громаден, 
    /// Пример использования самих ключей (с MSDN):
    /// This example displays the following output to the console:
    ///  d: 6/15/2008
    ///  D: Sunday, June 15, 2008
    ///  f: Sunday, June 15, 2008 9:15 PM
    ///  F: Sunday, June 15, 2008 9:15:07 PM
    ///  g: 6/15/2008 9:15 PM
    ///  G: 6/15/2008 9:15:07 PM
    ///  m: June 15
    ///  o: 2008-06-15T21:15:07.0000000
    ///  R: Sun, 15 Jun 2008 21:15:07 GMT
    ///  s: 2008-06-15T21:15:07
    ///  t: 9:15 PM
    ///  T: 9:15:07 PM
    ///  u: 2008-06-15 21:15:07Z
    ///  U: Monday, June 16, 2008 4:15:07 AM
    ///  y: June, 2008
    ///  
    ///  'h:mm:ss.ff t': 9:15:07.00 P
    ///  'd MMM yyyy': 15 Jun 2008
    ///  'HH:mm:ss.f': 21:15:07.0
    ///  'dd MMM HH:mm:ss': 15 Jun 21:15:07
    ///  '\Mon\t\h\: M': Month: 6
    ///  'HH:mm:ss.ffffzzz': 21:15:07.0000-07:00
    /// </summary>
    /// <summary>
    /// LEVEL, Level, level - вывод текущего уровня логирования
    /// </summary>
    /// <summary>
    /// STACKSOURCE, StackSource, stacksource - вывод цепочки имен модулей от которых происходит логирование
    /// Пример вывода - NetworkSystem.ReceptionModule
    /// </summary>
    /// <summary>
    /// CLASS, Class, class - вывод полного имени класса в котором вызывается мотод логирования
    /// </summary>
    /// <summary>
    /// METHOD, Method, method - вывод имени метода в котором вызывается мотод логирования
    /// </summary>
    /// <summary>
    /// MESSAGE, Message, message - вывод логирующего сообщения
    /// </summary>
    /// <summary>
    /// EXCEPTION, Exception, exception - вывод логирующего сообщения
    /// </summary>
    /// <summary>
    /// NAMESPACE, Namespace, namespace - вывод сообщения о пространстве имён
    /// </summary>
    /// <summary>
    /// ASSEMBLY, Assembly, assembly - вывод сообщения о сборке
    /// </summary>
    public static class TemplateParser
    {
        #region Substitutions for a template string

        private static readonly Dictionary<string, ConverterTypes> Substitutions = new Dictionary<string, ConverterTypes>
            {
                {"Level",       ConverterTypes.LevelConverter},
                {"level",       ConverterTypes.LevelConverter},
                {"LEVEL",       ConverterTypes.LevelConverter},

                {"StackSource", ConverterTypes.StackSourceConverter},
                {"stacksource", ConverterTypes.StackSourceConverter},
                {"STACKSOURCE", ConverterTypes.StackSourceConverter},
                {"SOURCES",     ConverterTypes.StackSourceConverter},
                {"Sources",     ConverterTypes.StackSourceConverter},
                {"sources",     ConverterTypes.StackSourceConverter},

                {"MACHINENAME", ConverterTypes.MachineNameConverter},
                {"MachineName", ConverterTypes.MachineNameConverter},
                {"Machinename", ConverterTypes.MachineNameConverter},
                {"machinename", ConverterTypes.MachineNameConverter},
                {"MACHINE",     ConverterTypes.MachineNameConverter},
                {"Machine",     ConverterTypes.MachineNameConverter},
                {"machine",     ConverterTypes.MachineNameConverter},

                {"PROCESSNAME", ConverterTypes.ProcessNameConverter},
                {"ProcessName", ConverterTypes.ProcessNameConverter},
                {"Processname", ConverterTypes.ProcessNameConverter},
                {"processname", ConverterTypes.ProcessNameConverter},
                {"PROCESS",     ConverterTypes.ProcessNameConverter},
                {"Process",     ConverterTypes.ProcessNameConverter},
                {"process",     ConverterTypes.ProcessNameConverter},

                {"PROCESSID",   ConverterTypes.ProcessIdConverter},
                {"ProcessId",   ConverterTypes.ProcessIdConverter},
                {"Processid",   ConverterTypes.ProcessIdConverter},
                {"processid",   ConverterTypes.ProcessIdConverter},

                {"ASSEMBLY",    ConverterTypes.AssemblyConverter},
                {"Assembly",    ConverterTypes.AssemblyConverter},
                {"assembly",    ConverterTypes.AssemblyConverter},

                {"NAMESPACE",   ConverterTypes.NamespaceConverter},
                {"Namespace",   ConverterTypes.NamespaceConverter},
                {"namespace",   ConverterTypes.NamespaceConverter},

                {"Class",       ConverterTypes.ClassConverter},
                {"class",       ConverterTypes.ClassConverter},
                {"CLASS",       ConverterTypes.ClassConverter},

                {"Method",      ConverterTypes.MethodConverter},
                {"method",      ConverterTypes.MethodConverter},
                {"METHOD",      ConverterTypes.MethodConverter},

                {"Message",     ConverterTypes.MessageConverter},
                {"message",     ConverterTypes.MessageConverter},
                {"MESSAGE",     ConverterTypes.MessageConverter},
                {"Msg",         ConverterTypes.MessageConverter},
                {"msg",         ConverterTypes.MessageConverter},

                {"Context",     ConverterTypes.ContextConverter},
                {"context",     ConverterTypes.ContextConverter},
                {"CONTEXT",     ConverterTypes.ContextConverter},

                {"Exception",   ConverterTypes.ExceptionConverter},
                {"exception",   ConverterTypes.ExceptionConverter},
                {"EXCEPTION",   ConverterTypes.ExceptionConverter},

                {"Ex",          ConverterTypes.ExceptionConverter},
                {"ex",          ConverterTypes.ExceptionConverter},
                {"EX",          ConverterTypes.ExceptionConverter},

                {"DateTime",    ConverterTypes.DateConverter},
                {"datetime",    ConverterTypes.DateConverter},
                {"DATETIME",    ConverterTypes.DateConverter},
            };

        enum ConverterTypes
        {
            LevelConverter,
            MachineNameConverter,
            ProcessNameConverter,
            ProcessIdConverter,
            AssemblyConverter,
            NamespaceConverter,
            ClassConverter,
            MethodConverter,
            MessageConverter,
            ContextConverter,
            DateConverter,
            ExceptionConverter,
            StackSourceConverter
        }

        #endregion


        private class ParsedConverterParams
        {
            public string Format;
            public string ValueOnNull;
            public string Prefix;
            public string Suffix;
        }


        /// <summary>
        /// Преобразование строки в конвертер для получения строки из логируемых данных 
        /// </summary>
        /// <param name="template">шаблон строки</param>
        /// <param name="factory">фабрика</param>
        /// <returns></returns>
        /// <exception cref="LoggerMessageTemplateParsingException"></exception>
        public static LoggingEventConverterBase Parse(string template, ConverterFactory factory)
        {
            Contract.Requires(template != null);
            Contract.Requires(factory != null);
            Contract.Ensures(Contract.Result<LoggingEventConverterBase>() != null);

            if (template == "")
                return factory.CreateConstConverter("");


            char[] stopSymbols = { '{' };
            var converters = new List<LoggingEventConverterBase>();

            int curPos = 0;
            while (curPos < template.Length)
            {
                if (template[curPos] == '{')
                {
                    curPos++;
                    var curConv = ParseConverter(template, ref curPos, factory);
                    converters.Add(curConv);
                }
                else
                {
                    string str = ReadString(template, ref curPos, stopSymbols);
                    converters.Add(factory.CreateConstConverter(str));
                }
            }


            if (converters.Count == 1)
                return converters[0];

            return new LoggingEventConverter(converters);
        }


        private static LoggingEventConverterBase ParseConverter(string data, ref int curPos, ConverterFactory factory)
        {
            int startPos = curPos;
            if (startPos > data.Length)
                startPos = data.Length;

            SkipSpaces(data, ref curPos);
            if (curPos >= data.Length)
                throw new LoggerMessageTemplateParsingException("Невозможно прочесть информацию о конвертере, т.к. строка закончилась. Строка: '" + data + "', позиция: " + curPos.ToString());


            string key = ReadTerm(data, ref curPos);
            SkipSpaces(data, ref curPos);

            if (curPos >= data.Length)
                throw new LoggerMessageTemplateParsingException("Не обнаружен конец конвертера ('}'). Конвертер: '" + data.Substring(startPos) + "', строка: '" + data + "', позиция: " + curPos.ToString());

            ParsedConverterParams parsedParams = new ParsedConverterParams();

            while (curPos < data.Length)
            {
                switch (data[curPos])
                {
                    case '}':
                        curPos++;
                        return CreateConverter(key, parsedParams, factory);
                    case ',':
                        curPos++;
                        var keyValue = ParseKeyValue(data, ref curPos);
                        FillParsedConverterParams(parsedParams, keyValue, data.Substring(startPos));
                        break;
                    default:
                        throw new LoggerMessageTemplateParsingException("Обнаружен некорректный символ при разборе конвертера ('" + data[curPos].ToString() + "'). Ожидалось '}' или ','. Конвертер: '" + data.Substring(startPos) + "', строка: '" + data + "', позиция: " + curPos.ToString());
                }
                SkipSpaces(data, ref curPos);
            }

            throw new LoggerMessageTemplateParsingException("Не обнаружен конец конвертера ('}'). Конвертер: '" + data.Substring(startPos) + "', строка: '" + data + "', позиция: " + curPos.ToString());
        }


        private static LoggingEventConverterBase CreateConverter(string key, ParsedConverterParams parsedParams, ConverterFactory factory)
        {
            Contract.Requires(key != null);
            Contract.Requires(parsedParams != null);
            Contract.Requires(factory != null);

            ConverterTypes type;

            if (!Substitutions.TryGetValue(key, out type))
                throw new LoggerMessageTemplateParsingException("Неизвестное наименование конвертера: " + key);
            
            switch (type)
            {
                case ConverterTypes.LevelConverter:
                    return factory.CreateLevelConverter();
                    
                case ConverterTypes.MachineNameConverter:
                    return WrapByParsedParams(factory.CreateMachineNameConverter(), parsedParams, "??");

                case ConverterTypes.ProcessNameConverter:
                    return WrapByParsedParams(factory.CreateProcessNameConverter(), parsedParams, "??");

                case ConverterTypes.ProcessIdConverter:
                    return WrapByParsedParams(factory.CreateProcessIdConverter(), parsedParams, "??");

                case ConverterTypes.AssemblyConverter:
                    return WrapByParsedParams(factory.CreateAssemblyConverter(), parsedParams, "<no asm>");

                case ConverterTypes.NamespaceConverter:
                    return WrapByParsedParams(factory.CreateNamespaceConverter(), parsedParams, "<no ns>");

                case ConverterTypes.ClassConverter:
                    return WrapByParsedParams(factory.CreateClassConverter(), parsedParams, "<no class>");

                case ConverterTypes.MethodConverter:
                    return WrapByParsedParams(factory.CreateMethodConverter(), parsedParams, "<no method>");

                case ConverterTypes.MessageConverter:
                    return WrapByParsedParams(factory.CreateMessageConverter(), parsedParams, "<no msg>");

                case ConverterTypes.ContextConverter:
                    return WrapByParsedParams(factory.CreateContextConverter(), parsedParams, null);

                case ConverterTypes.ExceptionConverter:
                    return WrapByParsedParams(factory.CreateExceptionConverter(), parsedParams, "<no exception>");

                case ConverterTypes.StackSourceConverter:
                    return WrapByParsedParams(factory.CreateStackSourceConverter(), parsedParams, "??");

                case ConverterTypes.DateConverter:
                    return factory.CreateDateConverter(parsedParams.Format);

                default:
                    throw new ArgumentException(string.Format("Неизвестный тип конвертера \"{0}\"", type.ToString()));
            }
        }


        private static LoggingEventConverterBase WrapByParsedParams(LoggingEventConverterBase src, ParsedConverterParams parsedParams, string defValOnNull)
        {
            Contract.Requires(src != null);
            Contract.Requires(parsedParams != null);

            string prefix = parsedParams.Prefix;
            if (prefix == "")
                prefix = null;

            string suffix = parsedParams.Suffix;
            if (suffix == "")
                suffix = null;

            string valueOnNull = parsedParams.ValueOnNull;
            if (valueOnNull == "")
                valueOnNull = null;
            else if (valueOnNull == null)
                valueOnNull = defValOnNull;

            if (prefix == null && suffix == null && valueOnNull == null)
                return src;

            return new LoggingEventConverterExtension(src, prefix, suffix, valueOnNull);
        }




        private static void FillParsedConverterParams(ParsedConverterParams parsedParams, KeyValuePair<string, string> val, string context)
        {
            Contract.Requires(parsedParams != null);

            switch (val.Key.Trim().ToLower())
            {
                case "format":
                    parsedParams.Format = val.Value;
                    break;
                case "valueonnull":
                    parsedParams.ValueOnNull = val.Value;
                    break;
                case "prefix":
                    parsedParams.Prefix = val.Value;
                    break;
                case "suffix":
                    parsedParams.Suffix = val.Value;
                    break;
                default:
                    throw new LoggerMessageTemplateParsingException("Обнаружен некорректный параметр конвертера ('" + val.Key + "'). Ожидалось 'Format', 'ValueOnNull', 'Prefix' или 'Suffix'. Контекст: " + context);
            }
        }

        private static KeyValuePair<string, string> ParseKeyValue(string data, ref int curPos)
        {
            int startPos = curPos;
            if (startPos > data.Length)
                startPos = data.Length;

            SkipSpaces(data, ref curPos);
            if (curPos >= data.Length)
                throw new LoggerMessageTemplateParsingException("Не обнаружено наименование параметра. Начало: '" + data.Substring(startPos) + "', строка: '" + data + "', позиция: " + curPos.ToString());

            string key = ReadTerm(data, ref curPos);
            string value = null;

            SkipSpaces(data, ref curPos);
            if (curPos >= data.Length || data[curPos] != '=')
                throw new LoggerMessageTemplateParsingException("Не обнаружено значение параметра (ожидалось '='). Начало: '" + data.Substring(startPos) + "', строка: '" + data + "', позиция: " + curPos.ToString());

            curPos++;

            SkipSpaces(data, ref curPos);
            if (curPos >= data.Length)
                throw new LoggerMessageTemplateParsingException("Не обнаружено значение параметра. Начало: '" + data.Substring(startPos) + "', строка: '" + data + "', позиция: " + curPos.ToString());

            if (data[curPos] == '\'')
            {
                curPos++;
                value = ReadString(data, ref curPos, new char[] { '\'' });
                if (curPos >= data.Length || data[curPos] != '\'')
                    throw new LoggerMessageTemplateParsingException("Не обнаружен конец значения параметра (ожидалось '''). Начало: '" + data.Substring(startPos) + "', строка: '" + data + "', позиция: " + curPos.ToString());
                curPos++;
            }
            else
            {
                value = ReadString(data, ref curPos, new char[] { '}', ',' }).Trim();
            }


            return new KeyValuePair<string, string>(key, value);
        }


        private static void SkipSpaces(string data, ref int curPos)
        {
            while (curPos < data.Length)
            {
                if (!char.IsWhiteSpace(data[curPos]))
                    break;
                curPos++;
            }
        }

        private static string ReadTerm(string data, ref int curPos)
        {
            if (curPos >= data.Length)
                throw new LoggerMessageTemplateParsingException("Невозможно прочесть терм за границей строки. Строка: " + data + ", позиция: " + curPos.ToString());

            int startPos = curPos;
            while (curPos < data.Length)
            {
                if (!(char.IsLetterOrDigit(data[curPos]) || data[curPos] == '_'))
                    break;
                curPos++;
            }

            return data.Substring(startPos, curPos - startPos);
        }


        private static string ReadString(string data, ref int curPos, char[] stopSymbols)
        {
            if (curPos >= data.Length)
                return "";

            StringBuilder bld = new StringBuilder(data.Length - curPos);

            while (curPos < data.Length)
            {
                if (Array.IndexOf(stopSymbols, data[curPos]) >= 0)
                    break;

                if (data[curPos] == '\\')
                {
                    curPos++;
                    bld.Append(ReadScreenedSymbol(data, ref curPos));
                }
                else
                {
                    bld.Append(data[curPos]);
                    curPos++;
                }
            }

            return bld.ToString();
        }

        private static string ReadScreenedSymbol(string data, ref int curPos)
        {
            if (curPos >= data.Length)
                throw new LoggerMessageTemplateParsingException("Неверное положение экранируемого символа. Строка: " + data + ", позиция: " + curPos.ToString());

            switch (data[curPos])
            {
                case 'n':
                    curPos++;
                    return Environment.NewLine;
                default:
                    curPos++;
                    return data[curPos - 1].ToString();
            }
        }
    }
}