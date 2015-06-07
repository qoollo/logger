using System;
using System.Collections.Generic;
using Qoollo.Logger.Common;

namespace Qoollo.Logger.LoggingEventConverters
{
    /// <summary>
    /// Factory that creates concrete converters for different parameters of LoggingEvent. 
    /// These converters transform parameters to its string representations.
    /// </summary>
    public class ConverterFactory
    {
        private static readonly ConverterFactory _default = new ConverterFactory();

        /// <summary>
        /// Default converter factory
        /// </summary>
        public static ConverterFactory Default { get { return _default; } }

        /// <summary>
        /// Creates aggreagated converter (combine sequence converters into one)
        /// </summary>
        /// <param name="converters">Converter sequence</param>
        /// <returns>Created converter</returns>
        public virtual LoggingEventConverterBase CreateLoggingEventConverter(List<LoggingEventConverterBase> converters)
        {
            return new LoggingEventConverter(converters);
        }

        /// <summary>
        /// Creates converter for constant string (by default it should return this string without changes)
        /// </summary>
        /// <param name="constString">String</param>
        /// <returns>Created converter</returns>
        public virtual LoggingEventConverterBase CreateConstConverter(string constString)
        {
            return new ConstConverter(constString);
        }

        /// <summary>
        /// Creates converter for 'Context' property of LoggingEvent
        /// </summary>
        /// <returns>Created converter</returns>
        public virtual LoggingEventConverterBase CreateContextConverter()
        {
            return new ContextConverter();
        }

        /// <summary>
        /// Creates converter for 'Date' property of LoggingEvent
        /// </summary>
        /// <param name="format">DateTime format string</param>
        /// <returns>Created converter</returns>
        public virtual LoggingEventConverterBase CreateDateConverter(string format)
        {
            return new DateConverter(format);
        }

        /// <summary>
        /// Creates converter for 'Level' property of LoggingEvent
        /// </summary>
        /// <returns></returns>
        public virtual LoggingEventConverterBase CreateLevelConverter()
        {
            return new LevelConverter();
        }

        /// <summary>
        /// Creates converter for 'MachineName' property of LoggingEvent
        /// </summary>
        /// <returns>Created converter</returns>
        public virtual LoggingEventConverterBase CreateMachineNameConverter()
        {
            return new MachineNameConverter();
        }

        /// <summary>
        /// Creates converter for 'IPv4' property of LoggingEvent
        /// </summary>
        /// <returns>Created converter</returns>
        public virtual LoggingEventConverterBase CreateIPv4Converter()
        {
            return new IPv4Converter();
        }

        /// <summary>
        /// Creates converter for 'ProcessName' property of LoggingEvent
        /// </summary>
        /// <returns>Created converter</returns>
        public virtual LoggingEventConverterBase CreateProcessNameConverter()
        {
            return new ProcessNameConverter();
        }

        /// <summary>
        /// Creates converter for 'ProcessId' property of LoggingEvent
        /// </summary>
        /// <returns>Created converter</returns>
        public virtual LoggingEventConverterBase CreateProcessIdConverter()
        {
            return new ProcessIdConverter();
        }

        /// <summary>
        /// Creates converter for 'Assembly' property of LoggingEvent
        /// </summary>
        /// <returns>Created converter</returns>
        public virtual LoggingEventConverterBase CreateAssemblyConverter()
        {
            return new AssemblyConverter();
        }

        /// <summary>
        /// Creates converter for 'Namespace' property of LoggingEvent
        /// </summary>
        /// <returns>Created converter</returns>
        public virtual LoggingEventConverterBase CreateNamespaceConverter()
        {
            return new NamespaceConverter();
        }

        /// <summary>
        /// Creates converter for 'Clazz' property of LoggingEvent
        /// </summary>
        /// <returns>Created converter</returns>
        public virtual LoggingEventConverterBase CreateClassConverter()
        {
            return new ClassConverter();
        }

        /// <summary>
        /// Creates converter for 'Method' property of LoggingEvent
        /// </summary>
        /// <returns>Created converter</returns>
        public virtual LoggingEventConverterBase CreateMethodConverter()
        {
            return new MethodConverter();
        }

        /// <summary>
        /// Creates converter for 'Message' property of LoggingEvent
        /// </summary>
        /// <returns>Created converter</returns>
        public virtual LoggingEventConverterBase CreateMessageConverter()
        {
            return new MessageConverter();
        }

        /// <summary>
        /// Creates converter for 'Exception' property of LoggingEvent
        /// </summary>
        /// <returns>Created converter</returns>
        public virtual LoggingEventConverterBase CreateExceptionConverter()
        {
            return new ExceptionConverter();
        }

        /// <summary>
        /// Creates converter for 'StackSources' property of LoggingEvent
        /// </summary>
        /// <returns>Created converter</returns>
        public virtual LoggingEventConverterBase CreateStackSourceConverter()
        {
            return new StackSourceConverter();
        }

        /// <summary>
        /// Creates converter for 'StackSources[0]' property of LoggingEvent
        /// </summary>
        /// <returns>Created converter</returns>
        public virtual LoggingEventConverterBase CreateStackSourceHeadConverter()
        {
            return new StackSourceHeadConverter();
        }

        /// <summary>
        /// Creates converter for 'StackSources[end]' property of LoggingEvent
        /// </summary>
        /// <returns>Created converter</returns>
        public virtual LoggingEventConverterBase CreateStackSourceTailConverter()
        {
            return new StackSourceTailConverter();
        }
    }
}