using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Qoollo.Logger.Common;
using Qoollo.Logger.Configuration;
using Qoollo.Logger.LoggingEventConverters;

namespace Qoollo.Logger
{
    public abstract partial class LoggerBase : ILogger
    {
        private const string Class = "Qoollo.Logger.LoggerBase";

        private readonly string _moduleName;
        private readonly Type _typeInfo;
        private List<string> _stackSources;
        private ILoggingEventWriter _logger;
        private readonly bool _isEnabled;

        private volatile bool _isDisposed;


        /// <summary>
        /// Создание логгера
        /// </summary>
        /// <param name="configuration">Конфигурация логгера</param>
        /// <param name="moduleName">Имя модуля (подсистемы)</param>
        /// <param name="innerLogger">Внутренний логгер</param>
        public LoggerBase(LoggerConfiguration configuration, string moduleName, ILoggingEventWriter innerLogger)
        {
            Contract.Requires<ArgumentNullException>(configuration != null, "configuration");
            Contract.Requires<ArgumentNullException>(moduleName != null, "moduleName");
            Contract.Requires<ArgumentNullException>(innerLogger != null, "innerLogger");

            Contract.Assume(configuration.Level != null, "configuration.Level");

            _moduleName = moduleName;
            _typeInfo = null;
            _logger = innerLogger;
            Refresh();

            Level = configuration.Level;
            _isEnabled = configuration.IsEnabled;
            _isTraceEnabled = Level.IsTraceEnabled;
            _isDebugEnabled = Level.IsDebugEnabled;
            _isInfoEnabled = Level.IsInfoEnabled;
            _isWarnEnabled = Level.IsWarnEnabled;
            _isErrorEnabled = Level.IsErrorEnabled;
            _isFatalEnabled = Level.IsFatalEnabled;
            _allowStackTraceInfoExtraction = configuration.IsStackTraceEnabled;
        }

        /// <summary>
        /// Создание логгера
        /// </summary>
        /// <param name="configuration">Конфигурация логгера</param>
        /// <param name="moduleName">Имя модуля (подсистемы)</param>
        public LoggerBase(LoggerConfiguration configuration, string moduleName)
            : this(configuration, moduleName, LoggerFactory.CreateWriter(configuration.Writer))
        {
        }


        /// <summary>
        /// Создание логгера
        /// </summary>
        /// <param name="logLevel">Уровень логирования</param>
        /// <param name="moduleName">Имя модуля (подсистемы)</param>
        /// <param name="typeInfo">Тип, к которому привзяан логгер</param>
        /// <param name="innerLogger">Внутренний логгер</param>
        /// <param name="enableStackTraceExtraction">Разрешено ли получать данные из StackTrace</param>
        /// <param name="isEnabled">Включён ли логгер</param>
        public LoggerBase(LogLevel logLevel, string moduleName, Type typeInfo, ILoggingEventWriter innerLogger, bool enableStackTraceExtraction = false, bool isEnabled = true)
        {
            Contract.Requires<ArgumentNullException>(logLevel != null, "logLevel");
            Contract.Requires<ArgumentNullException>(moduleName != null, "moduleName");
            Contract.Requires<ArgumentNullException>(innerLogger != null, "innerLogger");

            _moduleName = moduleName;
            _typeInfo = typeInfo;
            _logger = innerLogger;
            Refresh();

            Level = logLevel;
            _isEnabled = isEnabled;
            _isTraceEnabled = Level.IsTraceEnabled;
            _isDebugEnabled = Level.IsDebugEnabled;
            _isInfoEnabled = Level.IsInfoEnabled;
            _isWarnEnabled = Level.IsWarnEnabled;
            _isErrorEnabled = Level.IsErrorEnabled;
            _isFatalEnabled = Level.IsFatalEnabled;

            _allowStackTraceInfoExtraction = enableStackTraceExtraction;
        }

        /// <summary>
        /// Создание логгера
        /// </summary>
        /// <param name="logLevel">Уровень логирования</param>
        /// <param name="moduleName">Имя модуля (подсистемы)</param>
        /// <param name="innerLogger">Внутренний логгер</param>
        /// <param name="enableStackTraceExtraction">Разрешено ли получать данные из StackTrace</param>
        /// <param name="isEnabled">Включён ли логгер</param>
        public LoggerBase(LogLevel logLevel, string moduleName, ILoggingEventWriter innerLogger, bool enableStackTraceExtraction = false, bool isEnabled = true)
            : this(logLevel, moduleName, null, innerLogger, enableStackTraceExtraction, isEnabled)
        {
        }



        #region Source Info Extraction

        private readonly bool _allowStackTraceInfoExtraction = false;

        /// <summary>
        /// Разрешено ли извлекать расширенную информацию об источнике логирования
        /// </summary>
        public bool AllowStackTraceInfoExtraction
        {
            get { return _allowStackTraceInfoExtraction; }
        }

        /// <summary>
        /// Извлечение информации об источнике логирования по стек трейсу
        /// </summary>
        /// <param name="assembly">Сборка</param>
        /// <param name="namespace">Пространство имён</param>
        /// <param name="class">Класс</param>
        /// <param name="method">Метод</param>
        /// <param name="filePath">Файл</param>
        /// <param name="lineNumber">Строка</param>
        protected void ExtractCallerInfoFromStackTrace(out string assembly, out string @namespace, out string @class, out string method, out string filePath, out int lineNumber)
        {
            assembly = null;
            @namespace = null;
            @class = null;
            method = null;
            filePath = string.Empty;
            lineNumber = 0;

            var stack = new System.Diagnostics.StackTrace(true);
            for (int i = 0; i < stack.FrameCount; i++)
            {
                var frame = stack.GetFrame(i);
                var curMInf = frame.GetMethod();
                if (curMInf != null && curMInf.DeclaringType != typeof(LoggerBase) && !curMInf.DeclaringType.IsSubclassOf(typeof(LoggerBase)))
                {
                    assembly = curMInf.DeclaringType.Assembly.FullName;
                    @namespace = curMInf.DeclaringType.Namespace;
                    @class = curMInf.DeclaringType.FullName;
                    method = curMInf.Name;
                    filePath = frame.GetFileName();
                    lineNumber = frame.GetFileLineNumber();
                    break;
                }
            }
        }

        /// <summary>
        /// Извлечение информации об источнике логирования по стек трейсу
        /// </summary>
        /// <param name="assembly">Сборка</param>
        /// <param name="namespace">Пространство имён</param>
        /// <param name="class">Класс</param>
        /// <param name="method">Метод</param>
        protected void ExtractCallerInfoFromStackTrace(out string assembly, out string @namespace, out string @class, out string method)
        {
            assembly = null;
            @namespace = null;
            @class = null;
            method = null;

            var stack = new System.Diagnostics.StackTrace(false);
            for (int i = 0; i < stack.FrameCount; i++)
            {
                var frame = stack.GetFrame(i);
                var curMInf = frame.GetMethod();
                if (curMInf != null && curMInf.DeclaringType != typeof(LoggerBase) && !curMInf.DeclaringType.IsSubclassOf(typeof(LoggerBase)))
                {
                    assembly = curMInf.DeclaringType.Assembly.FullName;
                    @namespace = curMInf.DeclaringType.Namespace;
                    @class = curMInf.DeclaringType.FullName;
                    method = curMInf.Name;
                    break;
                }
            }
        }

        /// <summary>
        /// Извлечение информации о точке вызова
        /// </summary>
        /// <param name="assembly">Сборка</param>
        /// <param name="namespace">Пространство имён</param>
        /// <param name="class">Класс</param>
        /// <param name="method">Метод</param>
        /// <param name="filePath">Путь до файла</param>
        /// <param name="lineNumber">Строка</param>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        protected void ExtractCallerInfo(ref string assembly, ref string @namespace, ref string @class, ref string method, ref string filePath, ref int lineNumber)
        {
            if (_allowStackTraceInfoExtraction)
            {
                if (filePath != null && lineNumber > 0)
                    ExtractCallerInfoFromStackTrace(out assembly, out @namespace, out @class, out method);
                else
                    ExtractCallerInfoFromStackTrace(out assembly, out @namespace, out @class, out method, out filePath, out lineNumber);
            }
            else
            {
                if (@class == null && _typeInfo != null)
                {
                    @class = _typeInfo.FullName;
                    assembly = _typeInfo.Assembly.FullName;
                    @namespace = _typeInfo.Namespace;
                }
            }
        }

        #endregion

        #region Уровни логирования

        private readonly bool _isTraceEnabled;
        private readonly bool _isDebugEnabled;
        private readonly bool _isInfoEnabled;
        private readonly bool _isWarnEnabled;
        private readonly bool _isErrorEnabled;
        private readonly bool _isFatalEnabled;

        /// <summary>
        /// Включён ли сам логгер
        /// </summary>
        public bool IsLoggerEnabled
        {
            get { return _isEnabled; }
        }

        /// <summary>
        /// Включен ли уровень <c>Trace</c> логирования.
        /// </summary>
        /// <returns>Значение <see langword="true" /> если логирование включено для <c>Trace</c> уровня, иначе возвращается <see langword="false" />.</returns>
        public bool IsTraceEnabled
        {
            get { return _isTraceEnabled; }
        }

        /// <summary>
        /// Включен ли уровень <c>Debug</c> логирования.
        /// </summary>
        /// <returns>Значение <see langword="true" /> если логирование включено для <c>Debug</c> уровня, иначе возвращается <see langword="false" />.</returns>
        public bool IsDebugEnabled
        {
            get { return _isDebugEnabled; }
        }

        /// <summary>
        /// Включен ли уровень <c>Info</c> логирования.
        /// </summary>
        /// <returns>Значение <see langword="true" /> если логирование включено для <c>Info</c> уровня, иначе возвращается <see langword="false" />.</returns>
        public bool IsInfoEnabled
        {
            get { return _isInfoEnabled; }
        }

        /// <summary>
        /// Включен ли уровень <c>Warn</c> логирования.
        /// </summary>
        /// <returns>Значение <see langword="true" /> если логирование включено для <c>Warn</c> уровня, иначе возвращается <see langword="false" />.</returns>
        public bool IsWarnEnabled
        {
            get { return _isWarnEnabled; }
        }

        /// <summary>
        /// Включен ли уровень <c>Error</c> логирования.
        /// </summary>
        /// <returns>Значение <see langword="true" /> если логирование включено для <c>Error</c> уровня, иначе возвращается <see langword="false" />.</returns>
        public bool IsErrorEnabled
        {
            get { return _isErrorEnabled; }
        }

        /// <summary>
        /// Включен ли уровень <c>Fatal</c> логирования.
        /// </summary>
        /// <returns>Значение <see langword="true" /> если логирование включено для <c>Fatal</c> уровня, иначе возвращается <see langword="false" />.</returns>
        public bool IsFatalEnabled
        {
            get { return _isFatalEnabled; }
        }


        /// <summary>
        /// Включен ли специфичный уровень логирования.
        /// </summary>
        /// <param name="level">Проверяемый уровень логирования.</param>
        /// <returns>Значение <see langword="true" /> если логирование включено для данного уровня, иначе возвращается <see langword="false" />.</returns>
        public bool IsEnabled(LogLevel level)
        {
            return Level.IsEnabled(level);
        }

        #endregion

        #region Implementation of ILogger

        /// <summary>
        /// Имя модуля, к которому привзяан логгер
        /// </summary>
        public string ModuleName { get { return _moduleName; } }

        /// <summary>
        /// Тип, к которому привязан логгер
        /// </summary>
        public Type TypeInfo { get { return _typeInfo; } }

        /// <summary>
        /// Возвращает уровень логирования
        /// </summary>
        public LogLevel Level { get; private set; }


        /// <summary>
        /// Вызывается при обновлении цепочки логгеров
        /// </summary>
        /// <param name="stackSource">Текущая цепочка (может быть изменена)</param>
        protected virtual void OnRefreshStackSource(List<string> stackSource)
        {
        }

        /// <summary>
        /// Обновление цепочки вложенности программных модулей (StackSources) 
        /// </summary>
        public void Refresh()
        {
            if (_logger is ILogger)
                _stackSources = (_logger as ILogger).GetStackSources();
            else
                _stackSources = new List<string>();

            if (_stackSources.Count == 0 || _stackSources[_stackSources.Count - 1] != _moduleName || _typeInfo == null)
                _stackSources.Add(_moduleName);

            OnRefreshStackSource(_stackSources);
        }

        /// <summary>
        /// Возвращает цепочку вложенности программных модулей
        /// </summary>
        /// <returns>Список модулей. Первый элемент - внутренний модуль, последний - внешний.</returns>
        List<string> ILogger.GetStackSources()
        {
            return new List<string>(_stackSources);
        }

        /// <summary>
        /// Устанавливает фабрику для создания конвертеров,
        /// необходимых для преобразования логируемых данных в строки для вывода в файл или консоль
        /// </summary>
        /// <param name="factory"></param>
        public void SetConverterFactory(ConverterFactory factory)
        {
            _logger.SetConverterFactory(factory);
        }

        bool ILoggingEventWriter.Write(LoggingEvent data)
        {
            return _logger.Write(data);
        }

        #endregion




        /// <summary>
        /// Реализация настраиваемого освобождения ресурсов
        /// </summary>
        /// <param name="isUserCall"></param>
        protected void Dispose(bool isUserCall)
        {
            if (!_isDisposed)
            {
                _isDisposed = true;

                if (isUserCall)
                {
                    if (_logger != null)
                        _logger.Dispose();
                }
            }
        }


        /// <summary>
        /// Метод закрыть должен применятся один раз и при закрытии программы (если хотите чтобы она закрылась
        /// с вашим сообщением, а не с логом об экстренном закрытии программы)
        /// не стоит удивляться что при закрытии логгера в одном потоке, он отвалится в другом 
        /// (например, если они пишут в один файл - то на самом деле это один и тот же логгер)
        /// </summary>
        public void Close(string msg)
        {
            if (!_isDisposed)
            {
                var data = new LoggingEvent(msg, null, LogLevel.Info, _stackSources, Class, "Close");
                (this as ILogger).Write(data);
                Dispose(true);
            }
        }


        /// <summary>
        /// Метод закрыть должен применятся один раз и при закрытии программы (если хотите чтобы она закрылась
        /// сообщением о корректном завершении, а не с логом об экстренном закрытии программы)
        /// не стоит удивляться что при закрытии логгера в одном потоке, он отвалится в другом 
        /// (например, если они пишут в один файл - то на самом деле это один и тот же логгер)
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
