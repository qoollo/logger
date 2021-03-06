﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Qoollo.Logger.Common;
using System.Text;
using System.Diagnostics.Contracts;
using Qoollo.Logger.Exceptions;

namespace Qoollo.Logger.LoggingEventConverters
{
    /// <summary>
    /// Parser for log template strings. 
    /// Creates LoggingEventConvers chain from source template string.
    /// Template string can contain substitution tokens ({Level}, ...) and special symbols.
    /// </summary>
    /// <summary>
    /// DateTime - token for Date and Time substitution.
    /// Supports format string: {DateTime, format='yyyy-MM-dd hh:mm:ss'} 
    /// Format string parameters from MSDN:
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
    /// Level - substitution tokens for LogLevel
    /// </summary>
    /// <summary>
    /// Context - substitution tokens for Context
    /// </summary>
    /// <summary>
    /// StackSource, Sources  - substitution tokens for StackSource
    /// </summary>
    /// <summary>
    /// StackSourceHead, SourcesHead  - substitution tokens for the first element of StackSource
    /// </summary>
    /// <summary>
    /// StackSourceTail, SourcesTail  - substitution tokens for the last element of StackSource
    /// </summary>
    /// <summary>
    /// Class - substitution tokens for Class
    /// </summary>
    /// <summary>
    /// Method - substitution tokens for Method
    /// </summary>
    /// <summary>
    /// Message, Msg - substitution tokens for user Message
    /// </summary>
    /// <summary>
    /// Exception, Ex - substitution tokens for Exception
    /// </summary>
    /// <summary>
    /// Namespace - substitution tokens for Namespace
    /// </summary>
    /// <summary>
    /// Assembly - substitution tokens for Assembly
    /// </summary>
    /// <summary>
    /// MachineName, Machine - substitution tokens for MachineName
    /// </summary>
    /// <summary>
    /// MachineIpAddres, MachineIp - substitution tokens for MachineIpAddress
    /// </summary>
    /// <summary>
    /// ProcessName, Process - substitution tokens for ProcessName
    /// </summary>
    /// <summary>
    /// ProcessId - substitution tokens for ProcessId
    /// </summary>
    /// <summary>
    /// Assembly - substitution tokens for Assembly
    /// </summary>
    public static class TemplateParser
    {
        #region Substitutions for a template string

        private static readonly Dictionary<string, ConverterTypes> Substitutions = new Dictionary<string, ConverterTypes>
            {
                {"level",           ConverterTypes.LevelConverter},

                {"stacksource",     ConverterTypes.StackSourceConverter},
                {"sources",         ConverterTypes.StackSourceConverter},

                {"stacksourcehead", ConverterTypes.StackSourceHeadConverter},
                {"sourceshead",     ConverterTypes.StackSourceHeadConverter},

                {"stacksourcetail", ConverterTypes.StackSourceTailConverter},
                {"sourcestail",     ConverterTypes.StackSourceTailConverter},

                {"machinename",     ConverterTypes.MachineNameConverter},
                {"machine",         ConverterTypes.MachineNameConverter},
                
                {"machineipaddress",     ConverterTypes.MachineIpAddressConverter},
                {"machineip",            ConverterTypes.MachineIpAddressConverter},

                {"processname",     ConverterTypes.ProcessNameConverter},
                {"process",         ConverterTypes.ProcessNameConverter},

                {"processid",       ConverterTypes.ProcessIdConverter},

                {"assembly",        ConverterTypes.AssemblyConverter},

                {"namespace",       ConverterTypes.NamespaceConverter},

                {"class",           ConverterTypes.ClassConverter},

                {"method",          ConverterTypes.MethodConverter},

                {"message",         ConverterTypes.MessageConverter},
                {"msg",             ConverterTypes.MessageConverter},

                {"context",         ConverterTypes.ContextConverter},

                {"exception",       ConverterTypes.ExceptionConverter},
                {"ex",              ConverterTypes.ExceptionConverter},

                {"datetime",        ConverterTypes.DateConverter},
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
            StackSourceConverter,
            StackSourceHeadConverter,
            StackSourceTailConverter,
            MachineIpAddressConverter
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
        /// Parse supplied template string and creates the chain of converters using specified 'factory'
        /// </summary>
        /// <param name="template">Template string</param>
        /// <param name="factory">Factory to create converters</param>
        /// <returns>Final combined converter</returns>
        /// <exception cref="LoggerMessageTemplateParsingException">Exception for any parsing error</exception>
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
                throw new LoggerMessageTemplateParsingException("Unexpected end of string. Substitution token end was not found. String: '" + data + "', Position: " + curPos.ToString());


            string key = ReadTerm(data, ref curPos);
            SkipSpaces(data, ref curPos);

            if (curPos >= data.Length)
                throw new LoggerMessageTemplateParsingException("Unexpected end of string. Closing brace for substitution token not found ('}'). Token: '" + data.Substring(startPos) + "', String: '" + data + "', Postion: " + curPos.ToString());

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
                        throw new LoggerMessageTemplateParsingException("Incorrect symbol during parsing token parameters ('" + data[curPos].ToString() + "'). '}' or ',' expected. Token: '" + data.Substring(startPos) + "', String: '" + data + "', Position: " + curPos.ToString());
                }
                SkipSpaces(data, ref curPos);
            }

            throw new LoggerMessageTemplateParsingException("Unexpected end of string. Closing brace for substitution token not found ('}'). Token: '" + data.Substring(startPos) + "', String: '" + data + "', Postion: " + curPos.ToString());
        }


        private static LoggingEventConverterBase CreateConverter(string key, ParsedConverterParams parsedParams, ConverterFactory factory)
        {
            Contract.Requires(key != null);
            Contract.Requires(parsedParams != null);
            Contract.Requires(factory != null);

            ConverterTypes type;

            if (!Substitutions.TryGetValue(key.ToLowerInvariant(), out type))
                throw new LoggerMessageTemplateParsingException("Incorrect or unsupported token name: " + key);
            
            switch (type)
            {
                case ConverterTypes.LevelConverter:
                    return factory.CreateLevelConverter();
                    
                case ConverterTypes.MachineNameConverter:
                    return WrapByParsedParams(factory.CreateMachineNameConverter(), parsedParams, "??");

                case ConverterTypes.MachineIpAddressConverter:
                    return WrapByParsedParams(factory.CreateMachineIpAddressConverter(), parsedParams, "??");

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

                case ConverterTypes.StackSourceHeadConverter:
                    return WrapByParsedParams(factory.CreateStackSourceHeadConverter(), parsedParams, null);

                case ConverterTypes.StackSourceTailConverter:
                    return WrapByParsedParams(factory.CreateStackSourceTailConverter(), parsedParams, null);

                case ConverterTypes.DateConverter:
                    return factory.CreateDateConverter(parsedParams.Format);

                default:
                    throw new ArgumentException(string.Format("Unknown ConverterTypes value: \"{0}\"", type.ToString()));
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
                    throw new LoggerMessageTemplateParsingException("Incorrect or unsupported token parameter ('" + val.Key + "'). Possible values: 'Format', 'ValueOnNull', 'Prefix' or 'Suffix'. Context: " + context);
            }
        }

        private static KeyValuePair<string, string> ParseKeyValue(string data, ref int curPos)
        {
            int startPos = curPos;
            if (startPos > data.Length)
                startPos = data.Length;

            SkipSpaces(data, ref curPos);
            if (curPos >= data.Length)
                throw new LoggerMessageTemplateParsingException("Key name of token parameter not found. Parameter starts here: '" + data.Substring(startPos) + "', Full string: '" + data + "', Position: " + curPos.ToString());

            string key = ReadTerm(data, ref curPos);
            string value = null;

            SkipSpaces(data, ref curPos);
            if (curPos >= data.Length || data[curPos] != '=')
                throw new LoggerMessageTemplateParsingException("Value of token parameter not found ('=' expected). Parameter starts here: '" + data.Substring(startPos) + "', Full string: '" + data + "', Position: " + curPos.ToString());

            curPos++;

            SkipSpaces(data, ref curPos);
            if (curPos >= data.Length)
                throw new LoggerMessageTemplateParsingException("Value of token parameter not found. Parameter starts here: '" + data.Substring(startPos) + "', Full string: '" + data + "', Position: " + curPos.ToString());

            if (data[curPos] == '\'')
            {
                curPos++;
                value = ReadString(data, ref curPos, new char[] { '\'' });
                if (curPos >= data.Length || data[curPos] != '\'')
                    throw new LoggerMessageTemplateParsingException("End of the token parameter not found (''' expected). Parameter starts here: '" + data.Substring(startPos) + "', Full string: '" + data + "', Position: " + curPos.ToString());
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
                throw new LoggerMessageTemplateParsingException("Unexpected end of string. Term can't be read. String: " + data + ", Position: " + curPos.ToString());

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
                throw new LoggerMessageTemplateParsingException("Incorrect escape sequence. String: " + data + ", Position: " + curPos.ToString());

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