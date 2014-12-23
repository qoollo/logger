using Qoollo.Logger.Common;
using Qoollo.Logger.Configuration;
using Qoollo.Logger.LoggingEventConverters;
using System;
using System.Diagnostics.Contracts;

namespace Qoollo.Logger.Writers
{
    /// <summary>
    /// ConsoleWriter. Ресурс для вывода сообщений на консоль.
    /// </summary>
    internal class ConsoleWriter : Writer
    {
        private static readonly Logger _thisClassSupportLogger = InnerSupportLogger.Instance.GetClassLogger(typeof(ConsoleWriter));

        private LoggingEventConverterBase _templateConverter;
        private readonly string _rawTemplate;
        private readonly ConsoleColor _defaultColor;
        private readonly object _syncObj = new object();
        private readonly LogLevel _logLevel;


        public ConsoleWriter(ConsoleWriterConfiguration config)
            : base(config.Level)
        {
            Contract.Requires(config != null);

            _logLevel = config.Level;
            _rawTemplate = config.Template;
            
            _defaultColor = Console.ForegroundColor;
            SetConverterFactory(ConverterFactory.Default);
        }


        private ConsoleColor GetColorByLogLevel(LogLevel level)
        {
            ConsoleColor result = _defaultColor;
            if (level == LogLevel.Warn)
                result = ConsoleColor.Yellow;
            else if (level == LogLevel.Error)
                result = ConsoleColor.Red;
            else if (level == LogLevel.Fatal)
                result = ConsoleColor.Magenta;
            return result;
        }

        public override bool Write(LoggingEvent data)
        {
            if (data.Level < _logLevel)
                return true;
            
            string line = _templateConverter.Convert(data) ?? "";
            ConsoleColor requestedColor = GetColorByLogLevel(data.Level);

            lock (_syncObj)
            {
                if (requestedColor == _defaultColor)
                {
                    Console.Out.WriteLine(line);
                    Console.Out.Flush();
                }
                else
                {
                    Console.ForegroundColor = requestedColor;
                    Console.Out.WriteLine(line);
                    Console.Out.Flush();
                    Console.ForegroundColor = _defaultColor;
                }
            }
            
            return true;
        }

        /// <summary>
        /// Устанавливает фабрику для создания конвертеров,
        /// необходимых для преобразования логируемых данных в строки для вывода в файл или консоль
        /// </summary>
        /// <param name="factory"></param>
        public override void SetConverterFactory(ConverterFactory factory)
        {
            base.SetConverterFactory(factory);
            _templateConverter = TemplateParser.Parse(_rawTemplate, factory);
        }
    }
}