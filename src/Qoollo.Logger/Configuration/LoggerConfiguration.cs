using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics.Contracts;
using System;

namespace Qoollo.Logger.Configuration
{
    /// <summary>
    /// Root configuration for logger
    /// </summary>
    public class LoggerConfiguration
    {
        /// <summary>
        /// LoggerConfiguration constructor
        /// </summary>
        /// <param name="level">Global log level</param>
        /// <param name="isEnabled">Should logger be enabled</param>
        /// <param name="isStackTraceEnabled">Can logger perform StackTrace extraction (more information, but slow)</param>
        /// <param name="writer">Real writer/wrapper configuration</param>
        public LoggerConfiguration(LogLevel level, bool isEnabled, bool isStackTraceEnabled, LogWriterWrapperConfiguration writer)
        {
            Contract.Requires<ArgumentNullException>(level != null);

            Level = level;
            IsEnabled = isEnabled;
            IsStackTraceEnabled = isStackTraceEnabled;
            Writer = writer;
        }
        /// <summary>
        /// LoggerConfiguration constructor
        /// </summary>
        /// <param name="level">Global log level</param>
        /// <param name="isEnabled">Should logger be enabled</param>
        /// <param name="isStackTraceEnabled">Can logger perform StackTrace extraction (more information, but slow)</param>
        public LoggerConfiguration(LogLevel level, bool isEnabled, bool isStackTraceEnabled)
            : this(level, isEnabled, isStackTraceEnabled, null)
        {
        }
        /// <summary>
        /// LoggerConfiguration constructor
        /// </summary>
        /// <param name="level">Global log level</param>
        public LoggerConfiguration(LogLevel level)
            : this(level, true, false, null)
        {

        }
        /// <summary>
        /// LoggerConfiguration constructor
        /// </summary>
        public LoggerConfiguration()
            : this(LogLevel.Trace, true, false, null)
        {
        }

        /// <summary>
        /// Global log level
        /// </summary>
        public LogLevel Level { get; private set; }
        /// <summary>
        /// Should logger be enabled
        /// </summary>
        public bool IsEnabled { get; private set; }
        /// <summary>
        /// Can logger perform StackTrace extraction (more information, but slow)
        /// </summary>
        public bool IsStackTraceEnabled { get; set; }
        /// <summary>
        /// Real writer/wrapper configuration
        /// </summary>
        public LogWriterWrapperConfiguration Writer { get; set; }
    }



    /// <summary>
    /// Base class for wraper and writer configuration objects
    /// </summary>
    public abstract class LogWriterWrapperConfiguration
    {
        /// <summary>
        /// LogWriterWrapperConfiguration constructor
        /// </summary>
        /// <param name="writerType">Writer/wrapper type</param>
        protected LogWriterWrapperConfiguration(WriterTypeEnum writerType)
        {
            WriterType = writerType;
        }

        /// <summary>
        /// Writer/wrapper type
        /// </summary>
        public WriterTypeEnum WriterType { get; private set; }
    }

    #region Wrappers

    /// <summary>
    /// Configuration for asynchronous wrapper with queue
    /// </summary>
    public class AsyncQueueWrapperConfiguration : LogWriterWrapperConfiguration
    {
        /// <summary>
        /// AsyncQueueWrapperConfiguration constructor
        /// </summary>
        /// <param name="maxQueueSize">Bounded capacity of the queue ('-1' - capacity not limited)</param>
        /// <param name="isDiscardExcess">Can wrapper discard messages on queue overflow ('false' - block the caller)</param>
        /// <param name="innerWriter">Inner writer/wrapper configuration</param>
        public AsyncQueueWrapperConfiguration(int maxQueueSize, bool isDiscardExcess, LogWriterWrapperConfiguration innerWriter)
            : base(WriterTypeEnum.AsyncQueueWrapper)
        {
            MaxQueueSize = maxQueueSize;
            IsDiscardExcess = isDiscardExcess;
            InnerWriter = innerWriter;
        }
        /// <summary>
        /// AsyncQueueWrapperConfiguration constructor
        /// </summary>
        /// <param name="maxQueueSize">Bounded capacity of the queue ('-1' - capacity not limited)</param>
        /// <param name="isDiscardExcess">Can wrapper discard messages on queue overflow ('false' - block the caller)</param>
        public AsyncQueueWrapperConfiguration(int maxQueueSize, bool isDiscardExcess)
            : this(maxQueueSize, isDiscardExcess, null)
        {
        }
        /// <summary>
        /// AsyncQueueWrapperConfiguration constructor
        /// </summary>
        public AsyncQueueWrapperConfiguration()
            : this(4 * 1024, true, null)
        {
        }

        /// <summary>
        /// Bounded capacity of the queue ('-1' - capacity not limited)
        /// </summary>
        public int MaxQueueSize { get; private set; }

        /// <summary>
        /// Can wrapper discard messages on queue overflow ('false' - block the caller)
        /// </summary>
        public bool IsDiscardExcess { get; private set; }

        /// <summary>
        /// Inner writer/wrapper configuration
        /// </summary>
        public LogWriterWrapperConfiguration InnerWriter { get; set; }
    }

    /// <summary>
    /// Configuration for asynchronous wrapper with reliable queue. 
    /// If writer currently can't write a message, this wrapper save log message to the file on the disk. 
    /// Later it read logs and try to send them to writer again.
    /// </summary>
    public class AsyncReliableQueueWrapperConfiguration : LogWriterWrapperConfiguration
    {
        /// <summary>
        /// Default forlder to store files with logs
        /// </summary>
        public const string DefaultFolderName = "reliable_log_q";

        /// <summary>
        /// AsyncReliableQueueWrapperConfiguration constructor
        /// </summary>
        /// <param name="maxQueueSize">Bounded capacity of the queue ('-1' - capacity not limited)</param>
        /// <param name="isDiscardExcess">Can wrapper discard messages on queue overflow ('false' - block the caller)</param>
        /// <param name="folderName">Forlder name to store files with logs</param>
        /// <param name="maxSingleFileSize">Maximum size of single file</param>
        /// <param name="innerWriter">Inner writer/wrapper configuration</param>
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
        /// AsyncReliableQueueWrapperConfiguration constructor
        /// </summary>
        /// <param name="maxQueueSize">Bounded capacity of the queue ('-1' - capacity not limited)</param>
        /// <param name="isDiscardExcess">Can wrapper discard messages on queue overflow ('false' - block the caller)</param>
        /// <param name="folderName">Forlder name to store files with logs</param>
        /// <param name="maxSingleFileSize">Maximum size of single file</param>
        public AsyncReliableQueueWrapperConfiguration(int maxQueueSize, bool isDiscardExcess, string folderName, long maxSingleFileSize)
            : this(maxQueueSize, isDiscardExcess, folderName, maxSingleFileSize, null)
        {
        }
        /// <summary>
        /// AsyncReliableQueueWrapperConfiguration constructor
        /// </summary>
        /// <param name="folderName">Forlder name to store files with logs</param>
        public AsyncReliableQueueWrapperConfiguration(string folderName)
            : this(4 * 1024, true, folderName, 64 * 1024 * 1024, null)
        {
        }
        /// <summary>
        /// AsyncReliableQueueWrapperConfiguration constructor
        /// </summary>
        public AsyncReliableQueueWrapperConfiguration()
            : this(4 * 1024, true, DefaultFolderName, 64 * 1024 * 1024, null)
        {
        }

        /// <summary>
        /// Bounded capacity of the queue ('-1' - capacity not limited)
        /// </summary>
        public int MaxQueueSize { get; private set; }

        /// <summary>
        /// Can wrapper discard messages on queue overflow ('false' - block the caller)
        /// </summary>
        public bool IsDiscardExcess { get; private set; }

        /// <summary>
        /// Forlder name to store files with logs
        /// </summary>
        public string FolderForTemporaryStore { get; private set; }

        /// <summary>
        /// Maximum size of single file
        /// </summary>
        public long MaxFileSize { get; private set; }

        /// <summary>
        /// Inner writer/wrapper configuration
        /// </summary>
        public LogWriterWrapperConfiguration InnerWriter { get; set; }
    }



    /// <summary>
    /// Configuration for reliable wrapper (stores messages on the disk if they temporary can't be written by inner writer)
    /// </summary>
    public class ReliableWrapperConfiguration : LogWriterWrapperConfiguration
    {
        /// <summary>
        /// Default forlder to store files with logs
        /// </summary>
        public const string DefaultFolderName = "reliable_log_w";

        /// <summary>
        /// ReliableWrapperConfiguration constructor
        /// </summary>
        /// <param name="folderName">Forlder name to store files with logs</param>
        /// <param name="maxSingleFileSize">Maximum size of single file</param>
        /// <param name="innerWriter">Inner writer/wrapper configuration</param>
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
        /// ReliableWrapperConfiguration constructor
        /// </summary>
        /// <param name="folderName">Forlder name to store files with logs</param>
        /// <param name="maxSingleFileSize">Maximum size of single file</param>
        public ReliableWrapperConfiguration(string folderName, long maxSingleFileSize)
            : this(folderName, maxSingleFileSize, null)
        {
        }
        /// <summary>
        /// ReliableWrapperConfiguration constructor
        /// </summary>
        /// <param name="folderName">Forlder name to store files with logs</param>
        public ReliableWrapperConfiguration(string folderName)
            : this(folderName, 64 * 1024 * 1024, null)
        {
        }
        /// <summary>
        /// ReliableWrapperConfiguration constructor
        /// </summary>
        public ReliableWrapperConfiguration()
            : this(DefaultFolderName, 64 * 1024 * 1024, null)
        {
        }

        /// <summary>
        /// Forlder name to store files with logs
        /// </summary>
        public string FolderForTemporaryStore { get; private set; }

        /// <summary>
        /// Maximum size of single file
        /// </summary>
        public long MaxFileSize { get; private set; }

        /// <summary>
        /// Inner writer/wrapper configuration
        /// </summary>
        public LogWriterWrapperConfiguration InnerWriter { get; set; }
    }


    /// <summary>
    /// Configuration for Group Wrapper (wrapper that aggregate several other writers/wrappers)
    /// </summary>
    public class GroupWrapperConfiguration: LogWriterWrapperConfiguration
    {
        /// <summary>
        /// GroupWrapperConfiguration constructor
        /// </summary>
        /// <param name="writers">Inner writer/wrapper configurations</param>
        public GroupWrapperConfiguration(IEnumerable<LogWriterWrapperConfiguration> writers)
            : base(WriterTypeEnum.GroupWrapper)
        {
            if (writers != null)
                InnerWriters = new List<LogWriterWrapperConfiguration>(writers);
            else
                InnerWriters = new List<LogWriterWrapperConfiguration>();
        }
        /// <summary>
        /// GroupWrapperConfiguration constructor
        /// </summary>
        public GroupWrapperConfiguration()
            : this(null)
        {
        }

        /// <summary>
        /// Inner writer/wrapper configurations
        /// </summary>
        public List<LogWriterWrapperConfiguration> InnerWriters { get; private set; }
    }


    /// <summary>
    /// Configuration for wrapper that routes messages in per Module manner
    /// </summary>
    public class RoutingWrapperConfiguration: LogWriterWrapperConfiguration
    {
        /// <summary>
        /// RoutingWrapperConfiguration constructor
        /// </summary>
        /// <param name="routing">Writer/wrapper configurations to process messages from separate Module (dictionary key - module name)</param>
        /// <param name="fromAll">Writer/wrapper configurations to process all messages</param>
        /// <param name="fromOthers">Writer/wrapper configurations to process messages that have no special routing config</param>
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
        /// RoutingWrapperConfiguration constructor
        /// </summary>
        /// <param name="routing">Writer/wrapper configurations to process messages from separate Module</param>
        public RoutingWrapperConfiguration(Dictionary<string, List<LogWriterWrapperConfiguration>> routing)
            : this(routing, null, null)
        {

        }
        /// <summary>
        /// RoutingWrapperConfiguration constructor
        /// </summary>
        public RoutingWrapperConfiguration()
            : this(null, null, null)
        {

        }


        /// <summary>
        /// Writer/wrapper configurations to process all messages
        /// </summary>
        public List<LogWriterWrapperConfiguration> FromAll { get; private set; }

        /// <summary>
        /// Writer/wrapper configurations to process messages that have no special routing config
        /// </summary>
        public List<LogWriterWrapperConfiguration> FromOthers { get; private set; }

        /// <summary>
        /// Writer/wrapper configurations to process messages from separate Module (dictionary key - module name)
        /// </summary>
        public Dictionary<string, List<LogWriterWrapperConfiguration>> RoutingWriters { get; private set; }
    }


    /// <summary>
    /// Configuration for simple pattern-matching messages router
    /// </summary>
    public class PatternMatchingWrapperConfiguration : LogWriterWrapperConfiguration
    {
        /// <summary>
        /// PatternMatchingWrapperConfiguration constructor
        /// </summary>
        /// <param name="pattern">Pattern to build the string for comparison</param>
        /// <param name="matchWriters">Writer/wrapper configurations for concrete matching</param>
        /// <param name="defaultWriter">Writer/wrapper configuration that will be used when matches not found</param>
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
        /// PatternMatchingWrapperConfiguration constructor
        /// </summary>
        /// <param name="pattern">Pattern to build the string for comparison</param>
        /// <param name="matchWriters">Writer/wrapper configurations for concrete matching</param>
        public PatternMatchingWrapperConfiguration(string pattern, Dictionary<string, LogWriterWrapperConfiguration> matchWriters)
            : this(pattern, matchWriters, null)
        {
        }
        /// <summary>
        /// PatternMatchingWrapperConfiguration constructor
        /// </summary>
        /// <param name="pattern">Pattern to build the string for comparison</param>
        public PatternMatchingWrapperConfiguration(string pattern)
            : this(pattern, null, null)
        {
        }


        /// <summary>
        /// Pattern to build the string for comparison
        /// </summary>
        public string Pattern {get; private set;}

        /// <summary>
        /// Writer/wrapper configurations for concrete matching
        /// </summary>
        public Dictionary<string, LogWriterWrapperConfiguration> MatchWriters { get; private set; }

        /// <summary>
        /// Writer/wrapper configuration that will be used when matches not found
        /// </summary>
        public LogWriterWrapperConfiguration DefaultWriter { get; private set; }
    }



    /// <summary>
    /// Base class for custom user wrapper configuration
    /// </summary>
    public abstract class CustomWrapperConfiguration: LogWriterWrapperConfiguration
    {
        /// <summary>
        /// CustomWrapperConfiguration constructor
        /// </summary>
        public CustomWrapperConfiguration()
            : base(WriterTypeEnum.CustomWrapper)
        {
        }
    }


    #endregion

    /// <summary>
    /// Base class for writer configurations
    /// </summary>
    public abstract class LogWriterConfiguration : LogWriterWrapperConfiguration
    {
        /// <summary>
        /// LogWriterConfiguration constructor
        /// </summary>
        /// <param name="level">Log level</param>
        /// <param name="writerType">Writer type</param>
        protected LogWriterConfiguration(LogLevel level, WriterTypeEnum writerType)
            : base(writerType)
        {
            Contract.Requires<ArgumentNullException>(level != null);

            Level = level;
        }

        /// <summary>
        /// Log level
        /// </summary>
        public LogLevel Level { get; private set; }
    }

    #region Writers

    /// <summary>
    /// Configuration for empty writer (sepcial writer that do not perform any actions)
    /// </summary>
    public class EmptyWriterConfiguration : LogWriterConfiguration
    {
        /// <summary>
        /// EmptyWriterConfiguration constructor
        /// </summary>
        public EmptyWriterConfiguration()
            : base(LogLevel.FullLog, WriterTypeEnum.EmptyWriter)
        {
        }
    }

    /// <summary>
    /// Configuration for Console writer (writes logs to Console)
    /// </summary>
    public class ConsoleWriterConfiguration: LogWriterConfiguration
    {
        /// <summary>
        /// Default log message format string
        /// </summary>
        public const string DefaultTemplateFormat = "{DateTime}. {Level}. \\n At {StackSource}.{Class}::{Method}.\\n Message: {Message}. {Exception, prefix = '\\n Exception: ', valueOnNull=''}\\n\\n";

        /// <summary>
        /// ConsoleWriterConfiguration constructor
        /// </summary>
        /// <param name="level">Log level</param>
        /// <param name="template">Log message format string</param>
        public ConsoleWriterConfiguration(LogLevel level, string template)
            : base(level, WriterTypeEnum.ConsoleWriter)
        {
            Contract.Requires<ArgumentNullException>(level != null);
            Contract.Requires<ArgumentNullException>(template != null);

            Template = template;
        }
        /// <summary>
        /// ConsoleWriterConfiguration constructor
        /// </summary>
        /// <param name="template">Log message format string</param>
        public ConsoleWriterConfiguration(string template)
            : this(LogLevel.FullLog, template)
        {
        }
        /// <summary>
        /// ConsoleWriterConfiguration constructor
        /// </summary>
        public ConsoleWriterConfiguration()
            : this(LogLevel.FullLog, DefaultTemplateFormat)
        {
        }

        /// <summary>
        /// Log message format string
        /// </summary>
        public string Template { get; private set; }
    }

    /// <summary>
    /// Configuration for File Writer (writes logs to text file)
    /// </summary>
    public class FileWriterConfiguration: LogWriterConfiguration
    {
        /// <summary>
        /// Default log message format string
        /// </summary>
        public const string DefaultTemplateFormat = "{DateTime}. {Level}. \\n At {StackSource}.{Class}::{Method}.\\n Message: {Message}. {Exception, prefix = '\\n Exception: ', valueOnNull=''}\\n\\n";
        /// <summary>
        /// Default file name format string
        /// </summary>
        public const string DefaultFileNameTemplateFormat = "logs/app.log";

        /// <summary>
        /// FileWriterConfiguration constructor
        /// </summary>
        /// <param name="level">Log level</param>
        /// <param name="template">Log message format string</param>
        /// <param name="fileNameTemplate">File name format string</param>
        /// <param name="isNeedFileRotation">Enable file rotation (required if 'fileNameTemplate' contains sustitution tokens)</param>
        /// <param name="encoding">Text file encoding (default: UTF-8)</param>
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
        /// FileWriterConfiguration constructor
        /// </summary>
        /// <param name="level">Log level</param>
        /// <param name="template">Log message format string</param>
        /// <param name="fileNameTemplate">File name format string</param>
        /// <param name="encoding">Text file encoding (default: UTF-8)</param>
        public FileWriterConfiguration(LogLevel level, string template, string fileNameTemplate, Encoding encoding)
            : this(level, template, fileNameTemplate, fileNameTemplate.Contains('{') && fileNameTemplate.Contains('}'), encoding)
        {       
        }
        /// <summary>
        /// FileWriterConfiguration constructor
        /// </summary>
        /// <param name="level">Log level</param>
        /// <param name="template">Log message format string</param>
        /// <param name="fileNameTemplate">File name format string</param>
        public FileWriterConfiguration(LogLevel level, string template, string fileNameTemplate)
            : this(level, template, fileNameTemplate, Encoding.UTF8)
        {
        }
        /// <summary>
        /// FileWriterConfiguration constructor
        /// </summary>
        /// <param name="template">Log message format string</param>
        /// <param name="fileNameTemplate">File name format string</param>
        public FileWriterConfiguration(string template, string fileNameTemplate)
            : this(LogLevel.FullLog, template, fileNameTemplate)
        {
        }
        /// <summary>
        /// FileWriterConfiguration constructor
        /// </summary>
        public FileWriterConfiguration()
            : this(LogLevel.FullLog, DefaultTemplateFormat, DefaultFileNameTemplateFormat)
        {
        }

        /// <summary>
        /// Log message format string (can contains substitution tokens)
        /// </summary>
        public string Template { get; private set; }

        /// <summary>
        /// File name format string (can contains substitution tokens)
        /// </summary>
        public string FileNameTemplate { get; private set; }

        /// <summary>
        /// Is file rotation enabled (required if 'FileNameTemplate' contains sustitution tokens)
        /// </summary>
        public bool IsNeedFileRotate { get; private set; }

        /// <summary>
        /// Text file encoding
        /// </summary>
        public Encoding Encoding { get; private set; }
    }


    /// <summary>
    /// Configuration for Pipe Writer (sends logs to server through Pipe)
    /// </summary>
    public class PipeWriterConfiguration: LogWriterConfiguration
    {
        /// <summary>
        /// Default server name (local)
        /// </summary>
        public const string DefaultServerName = ".";
        /// <summary>
        /// Default Pipe name
        /// </summary>
        public const string DefaultPipeName = "LoggingService";

        /// <summary>
        /// PipeWriterConfiguration constructor
        /// </summary>
        /// <param name="level">Log level</param>
        /// <param name="serverName">Server name</param>
        /// <param name="pipeName">Pipe name</param>
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
        /// PipeWriterConfiguration constructor
        /// </summary>
        /// <param name="level">Log level</param>
        /// <param name="pipeName">Pipe name</param>
        public PipeWriterConfiguration(LogLevel level, string pipeName)
            : this(level, DefaultServerName, pipeName)
        {

        }
        /// <summary>
        /// PipeWriterConfiguration constructor
        /// </summary>
        /// <param name="pipeName">Pipe name</param>
        public PipeWriterConfiguration(string pipeName)
            : this(LogLevel.FullLog, pipeName)
        {

        }
        /// <summary>
        /// PipeWriterConfiguration constructor
        /// </summary>
        public PipeWriterConfiguration()
            : this(LogLevel.FullLog, DefaultServerName, DefaultPipeName)
        {

        }

        /// <summary>
        /// Server name
        /// </summary>
        public string ServerName { get; private set; }

        /// <summary>
        /// Pipe name
        /// </summary>
        public string PipeName { get; private set; }
    }


    /// <summary>
    /// Configuration for Network Writer (sends logs to server through Network)
    /// </summary>
    public class NetWriterConfiguration: LogWriterConfiguration
    {
        /// <summary>
        /// Default server TCP port
        /// </summary>
        public const int DefaultPort = 26113;
        /// <summary>
        /// Default server address/host
        /// </summary>
        public const string DefaultServerName = "127.0.0.1";

        /// <summary>
        /// NetWriterConfiguration constructor
        /// </summary>
        /// <param name="level">Log level</param>
        /// <param name="serverAddress">Server address/host</param>
        /// <param name="port">Server TCP port</param>
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
        /// NetWriterConfiguration constructor
        /// </summary>
        /// <param name="level">Log level</param>
        /// <param name="serverAddress">Server address/host</param>
        public NetWriterConfiguration(LogLevel level, string serverAddress)
            : this(level, serverAddress, DefaultPort)
        {

        }
        /// <summary>
        /// NetWriterConfiguration constructor
        /// </summary>
        /// <param name="serverAddress">Server address/host</param>
        public NetWriterConfiguration(string serverAddress)
            : this(LogLevel.FullLog, serverAddress, DefaultPort)
        {

        }
        /// <summary>
        /// NetWriterConfiguration constructor
        /// </summary>
        public NetWriterConfiguration()
            : this(LogLevel.FullLog, DefaultServerName, DefaultPort)
        {

        }

        /// <summary>
        /// Server address/host
        /// </summary>
        public string ServerAddress { get; private set; }

        /// <summary>
        /// Server TCP port
        /// </summary>
        public int Port { get; private set; }
    }

    /// <summary>
    /// Configuration for Network Writer (sends logs to server through Network)
    /// </summary>
    public class TcpWriterConfiguration : LogWriterConfiguration
    {
        /// <summary>
        /// Default server TCP port
        /// </summary>
        public const int DefaultPort = 5001;
        /// <summary>
        /// Default server address/host
        /// </summary>
        public const string DefaultServerName = "127.0.0.1";

        /// <summary>
        /// TcpWriterConfiguration constructor
        /// </summary>
        /// <param name="level">Log level</param>
        /// <param name="serverAddress">Server address/host</param>
        /// <param name="port">Server TCP port</param>
        public TcpWriterConfiguration(LogLevel level, string serverAddress, int port, string format)
            : base(level, WriterTypeEnum.TcpWriter)
        {
            Contract.Requires<ArgumentNullException>(level != null);
            Contract.Requires<ArgumentNullException>(serverAddress != null);
            Contract.Requires<ArgumentException>(port > 0 && port < 65536);

            ServerAddress = serverAddress;
            Port = port;
        }
        /// <summary>
        /// TcpWriterConfiguration constructor
        /// </summary>
        /// <param name="level">Log level</param>
        /// <param name="serverAddress">Server address/host</param>
        public TcpWriterConfiguration(LogLevel level, string serverAddress)
            : this(level, serverAddress, DefaultPort, null)
        {

        }
        /// <summary>
        /// NetWriterConfiguration constructor
        /// </summary>
        /// <param name="serverAddress">Server address/host</param>
        public TcpWriterConfiguration(string serverAddress)
            : this(LogLevel.FullLog, serverAddress, DefaultPort, null)
        {

        }
        /// <summary>
        /// TcpWriterConfiguration constructor
        /// </summary>
        public TcpWriterConfiguration()
            : this(LogLevel.FullLog, DefaultServerName, DefaultPort, null)
        {

        }

        /// <summary>
        /// Server address/host
        /// </summary>
        public string ServerAddress { get;  set; }

        /// <summary>
        /// Server TCP port
        /// </summary>
        public int Port { get;  set; }

        public int ConnectionTestTimeMs { get; set; }
    }

    /// <summary>
    /// Configuration for Database writer (writes logs to MS SQL Server Database)
    /// </summary>
    public class DatabaseWriterConfiguration: LogWriterConfiguration
    {
        /// <summary>
        /// Default stored porcedure name to insert logs
        /// </summary>
        public const string DefaultStoredProcedureName = "[dbo].[LogInsert]";
        /// <summary>
        /// Default database name
        /// </summary>
        public const string DefaultLogDatabaseName = "LogDatabase";
        /// <summary>
        /// Default connection string
        /// </summary>
        public const string DefaultConnectionString = "Data Source = (local); Database = LogDatabase; Integrated Security = SSPI;";

        /// <summary>
        /// DatabaseWriterConfiguration constructor
        /// </summary>
        /// <param name="level">Log level</param>
        /// <param name="connectionString">Connection string</param>
        /// <param name="storedProcedureName">Stored procedure name to insert logs</param>
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
        /// DatabaseWriterConfiguration constructor
        /// </summary>
        /// <param name="level">Log level</param>
        /// <param name="connectionString">Connection string</param>
        public DatabaseWriterConfiguration(LogLevel level, string connectionString)
            : this(level, connectionString, DefaultStoredProcedureName)
        {
        }
        /// <summary>
        /// DatabaseWriterConfiguration constructor
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        public DatabaseWriterConfiguration(string connectionString)
            : this(LogLevel.FullLog, connectionString, DefaultStoredProcedureName)
        {
        }
        /// <summary>
        /// DatabaseWriterConfiguration constructor
        /// </summary>
        public DatabaseWriterConfiguration()
            : this(DefaultConnectionString)
        {
        }

        /// <summary>
        /// Connection string
        /// </summary>
        public string ConnectionString { get; private set; }

        /// <summary>
        /// Stored procedure name to insert logs
        /// </summary>
        public string StoredProcedureName { get; private set; }
    }


    /// <summary>
    /// Base class for user custom writer configuration
    /// </summary>
    public abstract class CustomWriterConfiguration: LogWriterConfiguration
    {
        /// <summary>
        /// CustomWriterConfiguration constructor
        /// </summary>
        /// <param name="level">Log level</param>
        public CustomWriterConfiguration(LogLevel level)
            : base(level, WriterTypeEnum.CustomWriter)
        {
        }
    }

    #endregion
}