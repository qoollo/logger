using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics.Contracts;
using System;

namespace Qoollo.Logger.Configuration
{
    /// <summary>
    /// Корневая конфигурация логгера
    /// </summary>
    public class LoggerConfiguration
    {
        /// <summary>
        /// Конструктор LoggerConfiguration
        /// </summary>
        /// <param name="level">Уровень логирования</param>
        /// <param name="isEnabled">Флаг, показывающий включен логгер или нет</param>
        /// <param name="isStackTraceEnabled">Можно ли извлекать данные о вызове из стека</param>
        /// <param name="writer">Писатель логов или обёртка</param>
        public LoggerConfiguration(LogLevel level, bool isEnabled, bool isStackTraceEnabled, LogWriterWrapperConfiguration writer)
        {
            Contract.Requires<ArgumentNullException>(level != null);

            Level = level;
            IsEnabled = isEnabled;
            IsStackTraceEnabled = isStackTraceEnabled;
            Writer = writer;
        }
        /// <summary>
        /// Конструктор LoggerConfiguration
        /// </summary>
        /// <param name="level">Уровень логирования</param>
        /// <param name="isEnabled">Флаг, показывающий включен логгер или нет</param>
        /// <param name="isStackTraceEnabled">Можно ли извлекать данные о вызове из стека</param>
        public LoggerConfiguration(LogLevel level, bool isEnabled, bool isStackTraceEnabled)
            : this(level, isEnabled, isStackTraceEnabled, null)
        {
        }
        /// <summary>
        /// Конструктор LoggerConfiguration
        /// </summary>
        /// <param name="level">Уровень логирования</param>
        public LoggerConfiguration(LogLevel level)
            : this(level, true, false, null)
        {

        }
        /// <summary>
        /// Конструктор LoggerConfiguration
        /// </summary>
        public LoggerConfiguration()
            : this(LogLevel.Trace, true, false, null)
        {
        }

        /// <summary>
        /// Уровень логирования
        /// </summary>
        public LogLevel Level { get; private set; }
        /// <summary>
        /// Флаг, показывающий включен логгер или нет
        /// </summary>
        public bool IsEnabled { get; private set; }
        /// <summary>
        /// Можно ли извлекать данные о вызове из стека
        /// </summary>
        public bool IsStackTraceEnabled { get; set; }

        public LogWriterWrapperConfiguration Writer { get; set; }
    }



    /// <summary>
    /// Базовая конфигурация для писателя/обёртки
    /// </summary>
    public abstract class LogWriterWrapperConfiguration
    {
        /// <summary>
        /// Конструктор LogWriterWrapperConfiguration
        /// </summary>
        /// <param name="writerType">Тип писателя</param>
        protected LogWriterWrapperConfiguration(WriterTypeEnum writerType)
        {
            WriterType = writerType;
        }

        /// <summary>
        /// Тип писателя
        /// </summary>
        public WriterTypeEnum WriterType { get; private set; }
    }

    #region Wrappers

    /// <summary>
    /// Обёртка, обеспечивающая асинхронную обработку логов
    /// </summary>
    public class AsyncQueueWrapperConfiguration : LogWriterWrapperConfiguration
    {
        /// <summary>
        /// Конструктор AsyncQueueWrapperConfiguration
        /// </summary>
        /// <param name="maxQueueSize">Определяет максимальный размер очереди (-1 - для бесконечной очереди)</param>
        /// <param name="isDiscardExcess">Выбрасывать ли записи при переполнении очереди</param>
        /// <param name="innerWriter">Внутренний писатель</param>
        public AsyncQueueWrapperConfiguration(int maxQueueSize, bool isDiscardExcess, LogWriterWrapperConfiguration innerWriter)
            : base(WriterTypeEnum.AsyncQueueWrapper)
        {
            MaxQueueSize = maxQueueSize;
            IsDiscardExcess = isDiscardExcess;
            InnerWriter = innerWriter;
        }
        /// <summary>
        /// Конструктор AsyncQueueWrapperConfiguration
        /// </summary>
        /// <param name="maxQueueSize">Определяет максимальный размер очереди (-1 - для бесконечной очереди)</param>
        /// <param name="isDiscardExcess">Выбрасывать ли записи при переполнении очереди</param>
        public AsyncQueueWrapperConfiguration(int maxQueueSize, bool isDiscardExcess)
            : this(maxQueueSize, isDiscardExcess, null)
        {
        }
        /// <summary>
        /// Конструктор AsyncQueueWrapperConfiguration
        /// </summary>
        public AsyncQueueWrapperConfiguration()
            : this(4 * 1024, true, null)
        {
        }

        /// <summary>
        /// Определяет максимальный размер очереди (-1 - для бесконечной очереди)
        /// </summary>
        public int MaxQueueSize { get; private set; }

        /// <summary>
        /// Флаг задает поведение в случае переполнения очереди событий - выбрасывать лишнии 
        /// или ожидать возможности добавления (блокировать поток на добавлении)
        /// </summary>
        public bool IsDiscardExcess { get; private set; }

        /// <summary>
        /// Конфигурация логгера, в который будут отправляться логгирующие сообщения
        /// </summary>
        public LogWriterWrapperConfiguration InnerWriter { get; set; }
    }

    /// <summary>
    /// Обертка для обеспечения асинхронности с гарантией записи в лог (ведёт локальное хранилище)
    /// </summary>
    public class AsyncReliableQueueWrapperConfiguration : LogWriterWrapperConfiguration
    {
        public const string DefaultFolderName = "reliable_log_q";

        /// <summary>
        /// Конструктор AsyncReliableQueueWrapperConfiguration
        /// </summary>
        /// <param name="maxQueueSize">Определяет максимальный размер очереди (-1 - для бесконечной очереди)</param>
        /// <param name="isDiscardExcess">Выбрасывать ли записи при переполнении очереди</param>
        /// <param name="folderName">Имя директории для хранения файлов</param>
        /// <param name="maxSingleFileSize">Максимальный размер одного файла</param>
        /// <param name="innerWriter">Внутренний писатель</param>
        public AsyncReliableQueueWrapperConfiguration(int maxQueueSize, bool isDiscardExcess, string folderName, long maxSingleFileSize, LogWriterWrapperConfiguration innerWriter)
            : base(WriterTypeEnum.AsyncQueueWithReliableSendingWrapper)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(folderName));
            Contract.Requires<ArgumentException>(maxSingleFileSize >= 4 * 1024);

            MaxQueueSize = maxQueueSize;
            IsDiscardExcess = isDiscardExcess;
            FolderForTemporaryStore = folderName;
            MaxFileSize = maxSingleFileSize;
            InnerWriter = innerWriter;
        }
        /// <summary>
        /// Конструктор AsyncReliableQueueWrapperConfiguration
        /// </summary>
        /// <param name="maxQueueSize">Определяет максимальный размер очереди (-1 - для бесконечной очереди)</param>
        /// <param name="isDiscardExcess">Выбрасывать ли записи при переполнении очереди</param>
        /// <param name="folderName">Имя директории для хранения файлов</param>
        /// <param name="maxSingleFileSize">Максимальный размер одного файла</param>
        public AsyncReliableQueueWrapperConfiguration(int maxQueueSize, bool isDiscardExcess, string folderName, long maxSingleFileSize)
            : this(maxQueueSize, isDiscardExcess, folderName, maxSingleFileSize, null)
        {
        }
        /// <summary>
        /// Конструктор AsyncReliableQueueWrapperConfiguration
        /// </summary>
        /// <param name="folderName">Имя директории для хранения файлов</param>
        public AsyncReliableQueueWrapperConfiguration(string folderName)
            : this(4 * 1024, true, folderName, 64 * 1024 * 1024, null)
        {
        }
        /// <summary>
        /// Конструктор AsyncReliableQueueWrapperConfiguration
        /// </summary>
        public AsyncReliableQueueWrapperConfiguration()
            : this(4 * 1024, true, DefaultFolderName, 64 * 1024 * 1024, null)
        {
        }

        /// <summary>
        /// Определяет максимальный размер очереди (-1 - для бесконечной очереди)
        /// </summary>
        public int MaxQueueSize { get; private set; }

        /// <summary>
        /// Флаг задает поведение в случае переполнения очереди событий - выбрасывать лишнии 
        /// или ожидать возможности добавления (блокировать поток на добавлении)
        /// </summary>
        public bool IsDiscardExcess { get; private set; }

        /// <summary>
        /// Имя папки, в которую будут складываться неотправленные сообщения 
        /// для отложенной отправки
        /// </summary>
        public string FolderForTemporaryStore { get; private set; }

        /// <summary>
        /// Максимальный размер файла в байтах для хранения неотправленных логов
        /// </summary>
        public long MaxFileSize { get; private set; }

        /// <summary>
        /// Конфигурация логгера, в который будут отправляться логгирующие сообщения
        /// </summary>
        public LogWriterWrapperConfiguration InnerWriter { get; set; }
    }



    /// <summary>
    /// Обертка для предотвращения потерь записи (ведёт локальное хранилище)
    /// </summary>
    public class ReliableWrapperConfiguration : LogWriterWrapperConfiguration
    {
        public const string DefaultFolderName = "reliable_log_w";

        /// <summary>
        /// Конструктор ReliableWrapperConfiguration
        /// </summary>
        /// <param name="folderName">Имя директории для хранения файлов</param>
        /// <param name="maxSingleFileSize">Максимальный размер одного файла</param>
        /// <param name="innerWriter">Внутренний писатель</param>
        public ReliableWrapperConfiguration(string folderName, long maxSingleFileSize, LogWriterWrapperConfiguration innerWriter)
            : base(WriterTypeEnum.ReliableWrapper)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(folderName));
            Contract.Requires<ArgumentException>(maxSingleFileSize >= 4 * 1024);

            FolderForTemporaryStore = folderName;
            MaxFileSize = maxSingleFileSize;
            InnerWriter = innerWriter;
        }
        /// <summary>
        /// Конструктор ReliableWrapperConfiguration
        /// </summary>
        /// <param name="folderName">Имя директории для хранения файлов</param>
        /// <param name="maxSingleFileSize">Максимальный размер одного файла</param>
        public ReliableWrapperConfiguration(string folderName, long maxSingleFileSize)
            : this(folderName, maxSingleFileSize, null)
        {
        }
        /// <summary>
        /// Конструктор ReliableWrapperConfiguration
        /// </summary>
        /// <param name="folderName">Имя директории для хранения файлов</param>
        public ReliableWrapperConfiguration(string folderName)
            : this(folderName, 64 * 1024 * 1024, null)
        {
        }
        /// <summary>
        /// Конструктор ReliableWrapperConfiguration
        /// </summary>
        public ReliableWrapperConfiguration()
            : this(DefaultFolderName, 64 * 1024 * 1024, null)
        {
        }

        /// <summary>
        /// Имя папки, в которую будут складываться неотправленные сообщения 
        /// для отложенной отправки
        /// </summary>
        public string FolderForTemporaryStore { get; private set; }

        /// <summary>
        /// Максимальный размер файла в байтах для хранения неотправленных логов
        /// </summary>
        public long MaxFileSize { get; private set; }

        /// <summary>
        /// Конфигурация логгера, в который будут отправляться логгирующие сообщения
        /// </summary>
        public LogWriterWrapperConfiguration InnerWriter { get; set; }
    }


    /// <summary>
    /// Групповой враппер
    /// </summary>
    public class GroupWrapperConfiguration: LogWriterWrapperConfiguration
    {
        /// <summary>
        /// Конструктор GroupWrapperConfiguration
        /// </summary>
        /// <param name="writers">Внутренние писатели</param>
        public GroupWrapperConfiguration(IEnumerable<LogWriterWrapperConfiguration> writers)
            : base(WriterTypeEnum.GroupWrapper)
        {
            if (writers != null)
                InnerWriters = new List<LogWriterWrapperConfiguration>(writers);
            else
                InnerWriters = new List<LogWriterWrapperConfiguration>();
        }
        /// <summary>
        /// Конструктор GroupWrapperConfiguration
        /// </summary>
        public GroupWrapperConfiguration()
            : this(null)
        {
        }

        /// <summary>
        /// Внутренние писатели
        /// </summary>
        public List<LogWriterWrapperConfiguration> InnerWriters { get; private set; }
    }


    /// <summary>
    /// Маршрутизирующий враппер
    /// </summary>
    public class RoutingWrapperConfiguration: LogWriterWrapperConfiguration
    {
        /// <summary>
        /// Конструктор RoutingWrapperConfiguration
        /// </summary>
        /// <param name="routing">Маршрутизация по имени подсистем</param>
        /// <param name="fromAll">Доставка всем</param>
        /// <param name="fromOthers">Доставка оставшимя</param>
        public RoutingWrapperConfiguration(Dictionary<string, List<LogWriterWrapperConfiguration>> routing,
            IEnumerable<LogWriterWrapperConfiguration> fromAll, IEnumerable<LogWriterWrapperConfiguration> fromOthers)
            : base(WriterTypeEnum.RoutingWrapper)
        {
            if (fromAll != null)
                FromAll = new List<LogWriterWrapperConfiguration>(fromAll);
            else
                FromAll = new List<LogWriterWrapperConfiguration>();

            if (fromOthers != null)
                FromOthers = new List<LogWriterWrapperConfiguration>(fromOthers);
            else
                FromOthers = new List<LogWriterWrapperConfiguration>();

            if (routing != null)
                RoutingWriters = routing.ToDictionary(o => o.Key, o => new List<LogWriterWrapperConfiguration>(o.Value));
            else
                RoutingWriters = new Dictionary<string, List<LogWriterWrapperConfiguration>>();
        }
        /// <summary>
        /// Конструктор RoutingWrapperConfiguration
        /// </summary>
        /// <param name="routing">Маршрутизация по имени подсистем</param>
        public RoutingWrapperConfiguration(Dictionary<string, List<LogWriterWrapperConfiguration>> routing)
            : this(routing, null, null)
        {

        }
        /// <summary>
        /// Конструктор RoutingWrapperConfiguration
        /// </summary>
        public RoutingWrapperConfiguration()
            : this(null, null, null)
        {

        }


        /// <summary>
        /// Список конфигураций логгеров для логирования со всех модулей
        /// </summary>
        public List<LogWriterWrapperConfiguration> FromAll { get; private set; }

        /// <summary>
        /// Список конфигураций логгеров для логирования с модулей которые явно не прописаны в маршрутизации
        /// </summary>
        public List<LogWriterWrapperConfiguration> FromOthers { get; private set; }

        /// <summary>
        /// Список конфигураций логгеров для логирования с привязкой для каждого имени модуля
        /// </summary>
        public Dictionary<string, List<LogWriterWrapperConfiguration>> RoutingWriters { get; private set; }
    }


    /// <summary>
    /// Маршрутизирующий по паттерну враппер
    /// </summary>
    public class PatternMatchingWrapperConfiguration : LogWriterWrapperConfiguration
    {
        /// <summary>
        /// Конструктор PatternMatchingWrapperConfiguration
        /// </summary>
        /// <param name="pattern">Паттерн для получения строки</param>
        /// <param name="matchWriters">Словарь для выявляения совпадающих по паттерну писателей</param>
        /// <param name="defaultWriter">Писатель, в который пишет я при отстутвии совпадений</param>
        public PatternMatchingWrapperConfiguration(string pattern,
                    Dictionary<string, LogWriterWrapperConfiguration> matchWriters, 
                    LogWriterWrapperConfiguration defaultWriter)
            : base(WriterTypeEnum.PatternMatchingWrapper)
        {
            Contract.Requires<ArgumentNullException>(pattern != null);

            Pattern = pattern;

            if (matchWriters != null)
                MatchWriters = new Dictionary<string,LogWriterWrapperConfiguration>(matchWriters);
            else
                MatchWriters = new Dictionary<string,LogWriterWrapperConfiguration>();

            DefaultWriter = defaultWriter;
        }
        /// <summary>
        /// Конструктор PatternMatchingWrapperConfiguration
        /// </summary>
        /// <param name="pattern">Паттерн для получения строки</param>
        /// <param name="matchWriters">Словарь для выявляения совпадающих по паттерну писателей</param>
        public PatternMatchingWrapperConfiguration(string pattern, Dictionary<string, LogWriterWrapperConfiguration> matchWriters)
            : this(pattern, matchWriters, null)
        {
        }
        /// <summary>
        /// Конструктор PatternMatchingWrapperConfiguration
        /// </summary>
        /// <param name="pattern">Паттерн для получения строки</param>
        public PatternMatchingWrapperConfiguration(string pattern)
            : this(pattern, null, null)
        {
        }


        /// <summary>
        /// Паттерн для формирования строки по сообщению, которая потом сравнивается
        /// </summary>
        public string Pattern {get; private set;}

        /// <summary>
        /// Список конфигураций логгеров для логирования с привязкой по паттерну
        /// </summary>
        public Dictionary<string, LogWriterWrapperConfiguration> MatchWriters { get; private set; }

        /// <summary>
        /// Конфигурация писателя, в который пишется при отсутствии совпадений
        /// </summary>
        public LogWriterWrapperConfiguration DefaultWriter { get; private set; }
    }



    #endregion

    /// <summary>
    /// Базовая конфигурация для писателей 
    /// </summary>
    public abstract class LogWriterConfiguration : LogWriterWrapperConfiguration
    {
        /// <summary>
        /// Конструктор LogWriterConfiguration
        /// </summary>
        /// <param name="level">Уровень логирования</param>
        /// <param name="writerType">Тип писателя</param>
        protected LogWriterConfiguration(LogLevel level, WriterTypeEnum writerType)
            : base(writerType)
        {
            Contract.Requires<ArgumentNullException>(level != null);

            Level = level;
        }

        /// <summary>
        /// Уровень логирования
        /// </summary>
        public LogLevel Level { get; private set; }
    }

    #region Writers

    /// <summary>
    /// Пустой писатель
    /// </summary>
    public class EmptyWriterConfiguration : LogWriterConfiguration
    {
        /// <summary>
        /// Конструктор EmptyWriterConfiguration
        /// </summary>
        public EmptyWriterConfiguration()
            : base(LogLevel.FullLog, WriterTypeEnum.EmptyWriter)
        {
        }
    }

    /// <summary>
    /// Писатель логов в консоль
    /// </summary>
    public class ConsoleWriterConfiguration: LogWriterConfiguration
    {
        public const string DefaultTemplateFormat = "{DateTime}. {Level}. \\n At {StackSource}.{Class}::{Method}.\\n Message: {Message}. {Exception, prefix = '\\n Exception: ', valueOnNull=''}\\n\\n";

        /// <summary>
        /// Конструктор ConsoleWriterConfiguration
        /// </summary>
        /// <param name="level">Уровень логирования</param>
        /// <param name="template">Шаблон</param>
        public ConsoleWriterConfiguration(LogLevel level, string template)
            : base(level, WriterTypeEnum.ConsoleWriter)
        {
            Contract.Requires<ArgumentNullException>(level != null);
            Contract.Requires<ArgumentNullException>(template != null);

            Template = template;
        }
        /// <summary>
        /// Конструктор ConsoleWriterConfiguration
        /// </summary>
        /// <param name="template">Шаблон</param>
        public ConsoleWriterConfiguration(string template)
            : this(LogLevel.FullLog, template)
        {
        }
        /// <summary>
        /// Конструктор ConsoleWriterConfiguration
        /// </summary>
        public ConsoleWriterConfiguration()
            : this(LogLevel.FullLog, DefaultTemplateFormat)
        {
        }

        /// <summary>
        /// Шаблон для записи форматируемой строки
        /// </summary>
        public string Template { get; private set; }
    }

    /// <summary>
    /// Писатель в файл
    /// </summary>
    public class FileWriterConfiguration: LogWriterConfiguration
    {
        public const string DefaultTemplateFormat = "{DateTime}. {Level}. \\n At {StackSource}.{Class}::{Method}.\\n Message: {Message}. {Exception, prefix = '\\n Exception: ', valueOnNull=''}\\n\\n";
        public const string DefaultFileNameTemplateFormat = "logs/app.log";

        /// <summary>
        /// Конструктор FileWriterConfiguration
        /// </summary>
        /// <param name="level">Уровень логирования</param>
        /// <param name="template">Шаблон записи</param>
        /// <param name="fileNameTemplate">Шаблон имени файла с директорией</param>
        /// <param name="isNeedFileRotation">Нужна ли ротация файлов</param>
        /// <param name="encoding">Кодировка</param>
        public FileWriterConfiguration(LogLevel level, string template, string fileNameTemplate, bool isNeedFileRotation, Encoding encoding)
            : base(level, WriterTypeEnum.FileWriter)
        {
            Contract.Requires<ArgumentNullException>(level != null);
            Contract.Requires<ArgumentNullException>(template != null);
            Contract.Requires<ArgumentNullException>(fileNameTemplate != null);

            Template = template;
            FileNameTemplate = fileNameTemplate;
            IsNeedFileRotate = isNeedFileRotation;
            Encoding = encoding ?? Encoding.UTF8;
        }
        /// <summary>
        /// Конструктор FileWriterConfiguration
        /// </summary>
        /// <param name="level">Уровень логирования</param>
        /// <param name="template">Шаблон записи</param>
        /// <param name="fileNameTemplate">Шаблон имени файла с директорией</param>
        /// <param name="encoding">Кодировка</param>
        public FileWriterConfiguration(LogLevel level, string template, string fileNameTemplate, Encoding encoding)
            : this(level, template, fileNameTemplate, fileNameTemplate.Contains('{') && fileNameTemplate.Contains('}'), encoding)
        {       
        }
        /// <summary>
        /// Конструктор FileWriterConfiguration
        /// </summary>
        /// <param name="level">Уровень логирования</param>
        /// <param name="template">Шаблон записи</param>
        /// <param name="fileNameTemplate">Шаблон имени файла с директорией</param>
        public FileWriterConfiguration(LogLevel level, string template, string fileNameTemplate)
            : this(level, template, fileNameTemplate, Encoding.UTF8)
        {
        }
        /// <summary>
        /// Конструктор FileWriterConfiguration
        /// </summary>
        /// <param name="template">Шаблон записи</param>
        /// <param name="fileNameTemplate">Шаблон имени файла с директорией</param>
        public FileWriterConfiguration(string template, string fileNameTemplate)
            : this(LogLevel.FullLog, template, fileNameTemplate)
        {
        }
        /// <summary>
        /// Конструктор FileWriterConfiguration
        /// </summary>
        public FileWriterConfiguration()
            : this(LogLevel.FullLog, DefaultTemplateFormat, DefaultFileNameTemplateFormat)
        {
        }

        /// <summary>
        /// Шаблон для записи форматируемой строки для некоторых логгеров - ConsoleWriter, FileWriter
        /// </summary>
        public string Template { get; private set; }

        /// <summary>
        /// Имя файла для записи логов
        /// Может содержать ключи для подстановки Data, Time, Modul, Namespace, Class, Level, 
        /// и ключи объявленные в контексте экземляра логгера
        /// Благордаря ключам поддерживается ротация файлов - по дате или по имени системы от куда пишем
        /// </summary>
        public string FileNameTemplate { get; private set; }

        /// <summary>
        /// Показывает нужна ли ротация файлов
        /// </summary>
        public bool IsNeedFileRotate { get; private set; }

        /// <summary>
        /// Кодировка, используемая для записи в файл
        /// </summary>
        public Encoding Encoding { get; private set; }
    }


    /// <summary>
    /// Конфиг писателя в Пайп
    /// </summary>
    public class PipeWriterConfiguration: LogWriterConfiguration
    {
        public const string DefaultServerName = ".";
        public const string DefaultPipeName = "LoggingService";

        /// <summary>
        /// Конструктор PipeWriterConfiguration
        /// </summary>
        /// <param name="level">Уровень логирования</param>
        /// <param name="serverName">Имя сервера</param>
        /// <param name="pipeName">Имя пайпа</param>
        public PipeWriterConfiguration(LogLevel level, string serverName, string pipeName)
            : base(level, WriterTypeEnum.PipeWriter)
        {
            Contract.Requires<ArgumentNullException>(level != null);
            Contract.Requires<ArgumentNullException>(serverName != null);
            Contract.Requires<ArgumentNullException>(pipeName != null);

            ServerName = serverName;
            PipeName = pipeName;
        }
        /// <summary>
        /// Конструктор PipeWriterConfiguration
        /// </summary>
        /// <param name="level">Уровень логирования</param>
        /// <param name="pipeName">Имя пайпа</param>
        public PipeWriterConfiguration(LogLevel level, string pipeName)
            : this(level, DefaultServerName, pipeName)
        {

        }
        /// <summary>
        /// Конструктор PipeWriterConfiguration
        /// </summary>
        /// <param name="pipeName">Имя пайпа</param>
        public PipeWriterConfiguration(string pipeName)
            : this(LogLevel.FullLog, pipeName)
        {

        }
        /// <summary>
        /// Конструктор PipeWriterConfiguration
        /// </summary>
        public PipeWriterConfiguration()
            : this(LogLevel.FullLog, DefaultServerName, DefaultPipeName)
        {

        }

        /// <summary>
        /// Имя сервера
        /// </summary>
        public string ServerName { get; private set; }

        /// <summary>
        /// Имя пайпа
        /// </summary>
        public string PipeName { get; private set; }
    }


    /// <summary>
    /// Конфиг писателя по сети
    /// </summary>
    public class NetWriterConfiguration: LogWriterConfiguration
    {
        public const int DefaultPort = 26113;
        public const string DefaultServerName = "127.0.0.1";

        /// <summary>
        /// Конструктор NetWriterConfiguration
        /// </summary>
        /// <param name="level">Уровень логирования</param>
        /// <param name="serverAddress">Адрес сервера</param>
        /// <param name="port">Порт</param>
        public NetWriterConfiguration(LogLevel level, string serverAddress, int port)
            : base(level, WriterTypeEnum.NetWriter)
        {
            Contract.Requires<ArgumentNullException>(level != null);
            Contract.Requires<ArgumentNullException>(serverAddress != null);
            Contract.Requires<ArgumentException>(port > 0 && port < 65536);

            ServerAddress = serverAddress;
            Port = port;
        }
        /// <summary>
        /// Конструктор NetWriterConfiguration
        /// </summary>
        /// <param name="level">Уровень логирования</param>
        /// <param name="serverAddress">Адрес сервера</param>
        public NetWriterConfiguration(LogLevel level, string serverAddress)
            : this(level, serverAddress, DefaultPort)
        {

        }
        /// <summary>
        /// Конструктор NetWriterConfiguration
        /// </summary>
        /// <param name="serverAddress">Адрес сервера</param>
        public NetWriterConfiguration(string serverAddress)
            : this(LogLevel.FullLog, serverAddress, DefaultPort)
        {

        }
        /// <summary>
        /// Конструктор NetWriterConfiguration
        /// </summary>
        public NetWriterConfiguration()
            : this(LogLevel.FullLog, DefaultServerName, DefaultPort)
        {

        }

        /// <summary>
        /// Имя или адресс сервера
        /// </summary>
        public string ServerAddress { get; private set; }

        /// <summary>
        /// Порт для подключения
        /// </summary>
        public int Port { get; private set; }
    }

    /// <summary>
    /// Конфиг писателя в базу данных
    /// </summary>
    public class DatabaseWriterConfiguration: LogWriterConfiguration
    {
        public const string DefaultStoredProcedureName = "[dbo].[LogInsert]";
        public const string DefaultLogDatabaseName = "LogDatabase";
        public const string DefaultConnectionString = "Data Source = (local); Database = LogDatabase; Integrated Security = SSPI;";

        /// <summary>
        /// Конструктор DatabaseWriterConfiguration
        /// </summary>
        /// <param name="level">Уровень логирования</param>
        /// <param name="connectionString">Строка соединения</param>
        /// <param name="storedProcedureName">Имя хранимой процедуры</param>
        public DatabaseWriterConfiguration(LogLevel level, string connectionString, string storedProcedureName)
            : base(level, WriterTypeEnum.DBWriter)
        {
            Contract.Requires<ArgumentNullException>(level != null);
            Contract.Requires<ArgumentNullException>(connectionString != null);
            Contract.Requires<ArgumentNullException>(storedProcedureName != null);

            ConnectionString = connectionString;
            StoredProcedureName = storedProcedureName;
        }
        /// <summary>
        /// Конструктор DatabaseWriterConfiguration
        /// </summary>
        /// <param name="level">Уровень логирования</param>
        /// <param name="connectionString">Строка соединения</param>
        public DatabaseWriterConfiguration(LogLevel level, string connectionString)
            : this(level, connectionString, DefaultStoredProcedureName)
        {
        }
        /// <summary>
        /// Конструктор DatabaseWriterConfiguration
        /// </summary>
        /// <param name="connectionString">Строка соединения</param>
        public DatabaseWriterConfiguration(string connectionString)
            : this(LogLevel.FullLog, connectionString, DefaultStoredProcedureName)
        {
        }
        /// <summary>
        /// Конструктор DatabaseWriterConfiguration
        /// </summary>
        public DatabaseWriterConfiguration()
            : this(DefaultConnectionString)
        {
        }

        /// <summary>
        /// Строка подключения к БД
        /// </summary>
        public string ConnectionString { get; private set; }

        /// <summary>
        /// Имя хранимой процедуры для вставки в БД
        /// </summary>
        public string StoredProcedureName { get; private set; }
    }

    #endregion
}