using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Configuration;
using Qoollo.Logger.Configuration.LoggerConfigurationV2;


namespace Qoollo.Logger.Configuration
{

    /* **** Sample section group *****
    
    internal class SampleSectionGroup: ConfigurationSectionGroup
    {
    	public LoggerConfigurationSectionConfigClass LoggerConfigurationSectionSection
    	{
    		get
    		{
    			return this.Sections["LoggerConfigurationSectionSection"] as LoggerConfigurationSectionConfigClass;
    		}
    	}
    
    	public ILoggerConfigurationSection LoadLoggerConfigurationSectionSection()
    	{
    		return this.LoggerConfigurationSectionSection.ExtractConfigData();
    	}
    }
    
    */
    
    
    
    		
    // ============================

    internal class LoggerConfigurationSectionImplement : ILoggerConfigurationSection
    {
        public LoggerConfigurationSectionImplement()
        {
            this._logger = new RootLoggerConfigurationImplement();
        }

        private IRootLoggerConfiguration _logger;
        public IRootLoggerConfiguration GetLoggerVal()
        {
            return _logger;
        }
        public void SetLoggerVal(IRootLoggerConfiguration value)
        {
            _logger = value;
        }
        IRootLoggerConfiguration ILoggerConfigurationSection.Logger
        {
            get { return _logger; }
        }


        public LoggerConfigurationSectionImplement Copy()
        {
        	var res = new LoggerConfigurationSectionImplement();
        
            res._logger = RootLoggerConfigurationImplement.CopyInh(this._logger);

            return res;
        }

        public static ILoggerConfigurationSection CopyInh(ILoggerConfigurationSection src)
        {
        	if (src == null)
        		return null;
        
        	if (src.GetType() == typeof(LoggerConfigurationSectionImplement))
        		return ((LoggerConfigurationSectionImplement)src).Copy();
        	if (src.GetType() == typeof(LoggerConfigurationSectionImplement))
        		return ((LoggerConfigurationSectionImplement)src).Copy();
        
        	throw new Exception("Unknown type: " + src.GetType().ToString());
        }
    }

    internal class RootLoggerConfigurationImplement : IRootLoggerConfiguration
    {
        public RootLoggerConfigurationImplement()
        {
            this._enableStackTraceExtraction = false;
            this._isEnabled = true;
            this._writerWrapper = new LoggerWriterWrapperConfigurationImplement();
        }

        private Boolean _enableStackTraceExtraction;
        public Boolean GetEnableStackTraceExtractionVal()
        {
            return _enableStackTraceExtraction;
        }
        public void SetEnableStackTraceExtractionVal(Boolean value)
        {
            _enableStackTraceExtraction = value;
        }
        Boolean IRootLoggerConfiguration.EnableStackTraceExtraction
        {
            get { return _enableStackTraceExtraction; }
        }

        private CfgLogLevel _logLevel;
        public CfgLogLevel GetLogLevelVal()
        {
            return _logLevel;
        }
        public void SetLogLevelVal(CfgLogLevel value)
        {
            _logLevel = value;
        }
        CfgLogLevel IRootLoggerConfiguration.LogLevel
        {
            get { return _logLevel; }
        }

        private Boolean _isEnabled;
        public Boolean GetIsEnabledVal()
        {
            return _isEnabled;
        }
        public void SetIsEnabledVal(Boolean value)
        {
            _isEnabled = value;
        }
        Boolean IRootLoggerConfiguration.IsEnabled
        {
            get { return _isEnabled; }
        }

        private ILoggerWriterWrapperConfiguration _writerWrapper;
        public ILoggerWriterWrapperConfiguration GetWriterWrapperVal()
        {
            return _writerWrapper;
        }
        public void SetWriterWrapperVal(ILoggerWriterWrapperConfiguration value)
        {
            _writerWrapper = value;
        }
        ILoggerWriterWrapperConfiguration IRootLoggerConfiguration.WriterWrapper
        {
            get { return _writerWrapper; }
        }


        public RootLoggerConfigurationImplement Copy()
        {
        	var res = new RootLoggerConfigurationImplement();
        
            res._enableStackTraceExtraction = this._enableStackTraceExtraction;
            res._logLevel = this._logLevel;
            res._isEnabled = this._isEnabled;
            res._writerWrapper = LoggerWriterWrapperConfigurationImplement.CopyInh(this._writerWrapper);

            return res;
        }

        public static IRootLoggerConfiguration CopyInh(IRootLoggerConfiguration src)
        {
        	if (src == null)
        		return null;
        
        	if (src.GetType() == typeof(RootLoggerConfigurationImplement))
        		return ((RootLoggerConfigurationImplement)src).Copy();
        	if (src.GetType() == typeof(RootLoggerConfigurationImplement))
        		return ((RootLoggerConfigurationImplement)src).Copy();
        
        	throw new Exception("Unknown type: " + src.GetType().ToString());
        }
    }

    internal class LoggerWriterWrapperConfigurationImplement : ILoggerWriterWrapperConfiguration
    {
        public LoggerWriterWrapperConfigurationImplement()
        {
        }


        public LoggerWriterWrapperConfigurationImplement Copy()
        {
        	var res = new LoggerWriterWrapperConfigurationImplement();
        

            return res;
        }

        public static ILoggerWriterWrapperConfiguration CopyInh(ILoggerWriterWrapperConfiguration src)
        {
        	if (src == null)
        		return null;
        
        	if (src.GetType() == typeof(LoggerWriterWrapperConfigurationImplement))
        		return ((LoggerWriterWrapperConfigurationImplement)src).Copy();
        	if (src.GetType() == typeof(GroupWrapperImplement))
        		return ((GroupWrapperImplement)src).Copy();
        	if (src.GetType() == typeof(AsyncReliableQueueWrapperImplement))
        		return ((AsyncReliableQueueWrapperImplement)src).Copy();
        	if (src.GetType() == typeof(ReliableWrapperImplement))
        		return ((ReliableWrapperImplement)src).Copy();
        	if (src.GetType() == typeof(NetworkWriterImplement))
        		return ((NetworkWriterImplement)src).Copy();
        	if (src.GetType() == typeof(AsyncQueueWrapperImplement))
        		return ((AsyncQueueWrapperImplement)src).Copy();
        	if (src.GetType() == typeof(PatternMatchingWrapperImplement))
        		return ((PatternMatchingWrapperImplement)src).Copy();
        	if (src.GetType() == typeof(EmptyWriterImplement))
        		return ((EmptyWriterImplement)src).Copy();
        	if (src.GetType() == typeof(ConsoleWriterImplement))
        		return ((ConsoleWriterImplement)src).Copy();
        	if (src.GetType() == typeof(FileWriterImplement))
        		return ((FileWriterImplement)src).Copy();
        	if (src.GetType() == typeof(DatabaseWriterImplement))
        		return ((DatabaseWriterImplement)src).Copy();
        	if (src.GetType() == typeof(PipeWriterImplement))
        		return ((PipeWriterImplement)src).Copy();
        	if (src.GetType() == typeof(RoutingWrapperImplement))
        		return ((RoutingWrapperImplement)src).Copy();
        
        	throw new Exception("Unknown type: " + src.GetType().ToString());
        }
    }

    internal class GroupWrapperImplement : IGroupWrapper
    {
        public GroupWrapperImplement()
        {
            this._loggers = new List<ILoggerWriterWrapperConfiguration>();
        }

        private List<ILoggerWriterWrapperConfiguration> _loggers;
        public List<ILoggerWriterWrapperConfiguration> GetLoggersVal()
        {
            return _loggers;
        }
        public void SetLoggersVal(List<ILoggerWriterWrapperConfiguration> value)
        {
            _loggers = value;
        }
        List<ILoggerWriterWrapperConfiguration> IGroupWrapper.Loggers
        {
            get { return _loggers; }
        }


        public GroupWrapperImplement Copy()
        {
        	var res = new GroupWrapperImplement();
        
            if (this._loggers == null)
                res._loggers = null;
            else
                res._loggers = _loggers.Select(o => LoggerWriterWrapperConfigurationImplement.CopyInh(o)).ToList();


            return res;
        }

        public static IGroupWrapper CopyInh(IGroupWrapper src)
        {
        	if (src == null)
        		return null;
        
        	if (src.GetType() == typeof(GroupWrapperImplement))
        		return ((GroupWrapperImplement)src).Copy();
        	if (src.GetType() == typeof(GroupWrapperImplement))
        		return ((GroupWrapperImplement)src).Copy();
        
        	throw new Exception("Unknown type: " + src.GetType().ToString());
        }
    }

    internal class AsyncReliableQueueWrapperImplement : IAsyncReliableQueueWrapper
    {
        public AsyncReliableQueueWrapperImplement()
        {
            this._maxQueueSize = 4096;
            this._isDiscardExcess = true;
            this._folderForTemporaryStore = AsyncReliableQueueWrapperConfiguration.DefaultFolderName;
            this._maxFileSize = 67108864;
            this._logger = new LoggerWriterWrapperConfigurationImplement();
        }

        private Int32 _maxQueueSize;
        public Int32 GetMaxQueueSizeVal()
        {
            return _maxQueueSize;
        }
        public void SetMaxQueueSizeVal(Int32 value)
        {
            _maxQueueSize = value;
        }
        Int32 IAsyncReliableQueueWrapper.MaxQueueSize
        {
            get { return _maxQueueSize; }
        }

        private Boolean _isDiscardExcess;
        public Boolean GetIsDiscardExcessVal()
        {
            return _isDiscardExcess;
        }
        public void SetIsDiscardExcessVal(Boolean value)
        {
            _isDiscardExcess = value;
        }
        Boolean IAsyncReliableQueueWrapper.IsDiscardExcess
        {
            get { return _isDiscardExcess; }
        }

        private String _folderForTemporaryStore;
        public String GetFolderForTemporaryStoreVal()
        {
            return _folderForTemporaryStore;
        }
        public void SetFolderForTemporaryStoreVal(String value)
        {
            _folderForTemporaryStore = value;
        }
        String IAsyncReliableQueueWrapper.FolderForTemporaryStore
        {
            get { return _folderForTemporaryStore; }
        }

        private Int64 _maxFileSize;
        public Int64 GetMaxFileSizeVal()
        {
            return _maxFileSize;
        }
        public void SetMaxFileSizeVal(Int64 value)
        {
            _maxFileSize = value;
        }
        Int64 IAsyncReliableQueueWrapper.MaxFileSize
        {
            get { return _maxFileSize; }
        }

        private ILoggerWriterWrapperConfiguration _logger;
        public ILoggerWriterWrapperConfiguration GetLoggerVal()
        {
            return _logger;
        }
        public void SetLoggerVal(ILoggerWriterWrapperConfiguration value)
        {
            _logger = value;
        }
        ILoggerWriterWrapperConfiguration IAsyncReliableQueueWrapper.Logger
        {
            get { return _logger; }
        }


        public AsyncReliableQueueWrapperImplement Copy()
        {
        	var res = new AsyncReliableQueueWrapperImplement();
        
            res._maxQueueSize = this._maxQueueSize;
            res._isDiscardExcess = this._isDiscardExcess;
            res._folderForTemporaryStore = this._folderForTemporaryStore;
            res._maxFileSize = this._maxFileSize;
            res._logger = LoggerWriterWrapperConfigurationImplement.CopyInh(this._logger);

            return res;
        }

        public static IAsyncReliableQueueWrapper CopyInh(IAsyncReliableQueueWrapper src)
        {
        	if (src == null)
        		return null;
        
        	if (src.GetType() == typeof(AsyncReliableQueueWrapperImplement))
        		return ((AsyncReliableQueueWrapperImplement)src).Copy();
        	if (src.GetType() == typeof(AsyncReliableQueueWrapperImplement))
        		return ((AsyncReliableQueueWrapperImplement)src).Copy();
        
        	throw new Exception("Unknown type: " + src.GetType().ToString());
        }
    }

    internal class ReliableWrapperImplement : IReliableWrapper
    {
        public ReliableWrapperImplement()
        {
            this._maxFileSize = 67108864;
            this._logger = new LoggerWriterWrapperConfigurationImplement();
        }

        private String _folderForTemporaryStore;
        public String GetFolderForTemporaryStoreVal()
        {
            return _folderForTemporaryStore;
        }
        public void SetFolderForTemporaryStoreVal(String value)
        {
            _folderForTemporaryStore = value;
        }
        String IReliableWrapper.FolderForTemporaryStore
        {
            get { return _folderForTemporaryStore; }
        }

        private Int64 _maxFileSize;
        public Int64 GetMaxFileSizeVal()
        {
            return _maxFileSize;
        }
        public void SetMaxFileSizeVal(Int64 value)
        {
            _maxFileSize = value;
        }
        Int64 IReliableWrapper.MaxFileSize
        {
            get { return _maxFileSize; }
        }

        private ILoggerWriterWrapperConfiguration _logger;
        public ILoggerWriterWrapperConfiguration GetLoggerVal()
        {
            return _logger;
        }
        public void SetLoggerVal(ILoggerWriterWrapperConfiguration value)
        {
            _logger = value;
        }
        ILoggerWriterWrapperConfiguration IReliableWrapper.Logger
        {
            get { return _logger; }
        }


        public ReliableWrapperImplement Copy()
        {
        	var res = new ReliableWrapperImplement();
        
            res._folderForTemporaryStore = this._folderForTemporaryStore;
            res._maxFileSize = this._maxFileSize;
            res._logger = LoggerWriterWrapperConfigurationImplement.CopyInh(this._logger);

            return res;
        }

        public static IReliableWrapper CopyInh(IReliableWrapper src)
        {
        	if (src == null)
        		return null;
        
        	if (src.GetType() == typeof(ReliableWrapperImplement))
        		return ((ReliableWrapperImplement)src).Copy();
        	if (src.GetType() == typeof(ReliableWrapperImplement))
        		return ((ReliableWrapperImplement)src).Copy();
        
        	throw new Exception("Unknown type: " + src.GetType().ToString());
        }
    }

    internal class NetworkWriterImplement : INetworkWriter
    {
        public NetworkWriterImplement()
        {
            this._port = NetWriterConfiguration.DefaultPort;
            this._logLevel = CfgLogLevel.TRACE;
        }

        private String _serverAddress;
        public String GetServerAddressVal()
        {
            return _serverAddress;
        }
        public void SetServerAddressVal(String value)
        {
            _serverAddress = value;
        }
        String INetworkWriter.ServerAddress
        {
            get { return _serverAddress; }
        }

        private Int32 _port;
        public Int32 GetPortVal()
        {
            return _port;
        }
        public void SetPortVal(Int32 value)
        {
            _port = value;
        }
        Int32 INetworkWriter.Port
        {
            get { return _port; }
        }

        private CfgLogLevel _logLevel;
        public CfgLogLevel GetLogLevelVal()
        {
            return _logLevel;
        }
        public void SetLogLevelVal(CfgLogLevel value)
        {
            _logLevel = value;
        }
        CfgLogLevel ILoggerWriterConfiguration.LogLevel
        {
            get { return _logLevel; }
        }


        public NetworkWriterImplement Copy()
        {
        	var res = new NetworkWriterImplement();
        
            res._serverAddress = this._serverAddress;
            res._port = this._port;
            res._logLevel = this._logLevel;

            return res;
        }

        public static INetworkWriter CopyInh(INetworkWriter src)
        {
        	if (src == null)
        		return null;
        
        	if (src.GetType() == typeof(NetworkWriterImplement))
        		return ((NetworkWriterImplement)src).Copy();
        	if (src.GetType() == typeof(NetworkWriterImplement))
        		return ((NetworkWriterImplement)src).Copy();
        
        	throw new Exception("Unknown type: " + src.GetType().ToString());
        }
    }

    internal class AsyncQueueWrapperImplement : IAsyncQueueWrapper
    {
        public AsyncQueueWrapperImplement()
        {
            this._maxQueueSize = 4096;
            this._isDiscardExcess = true;
            this._logger = new LoggerWriterWrapperConfigurationImplement();
        }

        private Int32 _maxQueueSize;
        public Int32 GetMaxQueueSizeVal()
        {
            return _maxQueueSize;
        }
        public void SetMaxQueueSizeVal(Int32 value)
        {
            _maxQueueSize = value;
        }
        Int32 IAsyncQueueWrapper.MaxQueueSize
        {
            get { return _maxQueueSize; }
        }

        private Boolean _isDiscardExcess;
        public Boolean GetIsDiscardExcessVal()
        {
            return _isDiscardExcess;
        }
        public void SetIsDiscardExcessVal(Boolean value)
        {
            _isDiscardExcess = value;
        }
        Boolean IAsyncQueueWrapper.IsDiscardExcess
        {
            get { return _isDiscardExcess; }
        }

        private ILoggerWriterWrapperConfiguration _logger;
        public ILoggerWriterWrapperConfiguration GetLoggerVal()
        {
            return _logger;
        }
        public void SetLoggerVal(ILoggerWriterWrapperConfiguration value)
        {
            _logger = value;
        }
        ILoggerWriterWrapperConfiguration IAsyncQueueWrapper.Logger
        {
            get { return _logger; }
        }


        public AsyncQueueWrapperImplement Copy()
        {
        	var res = new AsyncQueueWrapperImplement();
        
            res._maxQueueSize = this._maxQueueSize;
            res._isDiscardExcess = this._isDiscardExcess;
            res._logger = LoggerWriterWrapperConfigurationImplement.CopyInh(this._logger);

            return res;
        }

        public static IAsyncQueueWrapper CopyInh(IAsyncQueueWrapper src)
        {
        	if (src == null)
        		return null;
        
        	if (src.GetType() == typeof(AsyncQueueWrapperImplement))
        		return ((AsyncQueueWrapperImplement)src).Copy();
        	if (src.GetType() == typeof(AsyncQueueWrapperImplement))
        		return ((AsyncQueueWrapperImplement)src).Copy();
        
        	throw new Exception("Unknown type: " + src.GetType().ToString());
        }
    }

    internal class PatternMatchingWrapperImplement : IPatternMatchingWrapper
    {
        public PatternMatchingWrapperImplement()
        {
            this._writers = new List<IPatternMatchingElement>();
        }

        private String _pattern;
        public String GetPatternVal()
        {
            return _pattern;
        }
        public void SetPatternVal(String value)
        {
            _pattern = value;
        }
        String IPatternMatchingWrapper.Pattern
        {
            get { return _pattern; }
        }

        private List<IPatternMatchingElement> _writers;
        public List<IPatternMatchingElement> GetWritersVal()
        {
            return _writers;
        }
        public void SetWritersVal(List<IPatternMatchingElement> value)
        {
            _writers = value;
        }
        List<IPatternMatchingElement> IPatternMatchingWrapper.Writers
        {
            get { return _writers; }
        }


        public PatternMatchingWrapperImplement Copy()
        {
        	var res = new PatternMatchingWrapperImplement();
        
            res._pattern = this._pattern;
            if (this._writers == null)
                res._writers = null;
            else
                res._writers = _writers.Select(o => PatternMatchingElementImplement.CopyInh(o)).ToList();


            return res;
        }

        public static IPatternMatchingWrapper CopyInh(IPatternMatchingWrapper src)
        {
        	if (src == null)
        		return null;
        
        	if (src.GetType() == typeof(PatternMatchingWrapperImplement))
        		return ((PatternMatchingWrapperImplement)src).Copy();
        	if (src.GetType() == typeof(PatternMatchingWrapperImplement))
        		return ((PatternMatchingWrapperImplement)src).Copy();
        
        	throw new Exception("Unknown type: " + src.GetType().ToString());
        }
    }

    internal class PatternMatchingElementImplement : IPatternMatchingElement
    {
        public PatternMatchingElementImplement()
        {
            this._writer = new LoggerWriterWrapperConfigurationImplement();
        }

        private ILoggerWriterWrapperConfiguration _writer;
        public ILoggerWriterWrapperConfiguration GetWriterVal()
        {
            return _writer;
        }
        public void SetWriterVal(ILoggerWriterWrapperConfiguration value)
        {
            _writer = value;
        }
        ILoggerWriterWrapperConfiguration IPatternMatchingElement.Writer
        {
            get { return _writer; }
        }


        public PatternMatchingElementImplement Copy()
        {
        	var res = new PatternMatchingElementImplement();
        
            res._writer = LoggerWriterWrapperConfigurationImplement.CopyInh(this._writer);

            return res;
        }

        public static IPatternMatchingElement CopyInh(IPatternMatchingElement src)
        {
        	if (src == null)
        		return null;
        
        	if (src.GetType() == typeof(PatternMatchingElementImplement))
        		return ((PatternMatchingElementImplement)src).Copy();
        	if (src.GetType() == typeof(PatternMatchingMatchImplement))
        		return ((PatternMatchingMatchImplement)src).Copy();
        	if (src.GetType() == typeof(PatternMatchingDefaultImplement))
        		return ((PatternMatchingDefaultImplement)src).Copy();
        
        	throw new Exception("Unknown type: " + src.GetType().ToString());
        }
    }

    internal class EmptyWriterImplement : IEmptyWriter
    {
        public EmptyWriterImplement()
        {
            this._logLevel = CfgLogLevel.TRACE;
        }

        private CfgLogLevel _logLevel;
        public CfgLogLevel GetLogLevelVal()
        {
            return _logLevel;
        }
        public void SetLogLevelVal(CfgLogLevel value)
        {
            _logLevel = value;
        }
        CfgLogLevel ILoggerWriterConfiguration.LogLevel
        {
            get { return _logLevel; }
        }


        public EmptyWriterImplement Copy()
        {
        	var res = new EmptyWriterImplement();
        
            res._logLevel = this._logLevel;

            return res;
        }

        public static IEmptyWriter CopyInh(IEmptyWriter src)
        {
        	if (src == null)
        		return null;
        
        	if (src.GetType() == typeof(EmptyWriterImplement))
        		return ((EmptyWriterImplement)src).Copy();
        	if (src.GetType() == typeof(EmptyWriterImplement))
        		return ((EmptyWriterImplement)src).Copy();
        
        	throw new Exception("Unknown type: " + src.GetType().ToString());
        }
    }

    internal class ConsoleWriterImplement : IConsoleWriter
    {
        public ConsoleWriterImplement()
        {
            this._template = ConsoleWriterConfiguration.DefaultTemplateFormat;
            this._logLevel = CfgLogLevel.TRACE;
        }

        private String _template;
        public String GetTemplateVal()
        {
            return _template;
        }
        public void SetTemplateVal(String value)
        {
            _template = value;
        }
        String IConsoleWriter.Template
        {
            get { return _template; }
        }

        private CfgLogLevel _logLevel;
        public CfgLogLevel GetLogLevelVal()
        {
            return _logLevel;
        }
        public void SetLogLevelVal(CfgLogLevel value)
        {
            _logLevel = value;
        }
        CfgLogLevel ILoggerWriterConfiguration.LogLevel
        {
            get { return _logLevel; }
        }


        public ConsoleWriterImplement Copy()
        {
        	var res = new ConsoleWriterImplement();
        
            res._template = this._template;
            res._logLevel = this._logLevel;

            return res;
        }

        public static IConsoleWriter CopyInh(IConsoleWriter src)
        {
        	if (src == null)
        		return null;
        
        	if (src.GetType() == typeof(ConsoleWriterImplement))
        		return ((ConsoleWriterImplement)src).Copy();
        	if (src.GetType() == typeof(ConsoleWriterImplement))
        		return ((ConsoleWriterImplement)src).Copy();
        
        	throw new Exception("Unknown type: " + src.GetType().ToString());
        }
    }

    internal class FileWriterImplement : IFileWriter
    {
        public FileWriterImplement()
        {
            this._template = FileWriterConfiguration.DefaultTemplateFormat;
            this._fileNameTemplate = FileWriterConfiguration.DefaultFileNameTemplateFormat;
            this._needFileRotation = true;
            this._encoding = "UTF-8";
            this._logLevel = CfgLogLevel.TRACE;
        }

        private String _template;
        public String GetTemplateVal()
        {
            return _template;
        }
        public void SetTemplateVal(String value)
        {
            _template = value;
        }
        String IFileWriter.Template
        {
            get { return _template; }
        }

        private String _fileNameTemplate;
        public String GetFileNameTemplateVal()
        {
            return _fileNameTemplate;
        }
        public void SetFileNameTemplateVal(String value)
        {
            _fileNameTemplate = value;
        }
        String IFileWriter.FileNameTemplate
        {
            get { return _fileNameTemplate; }
        }

        private Boolean _needFileRotation;
        public Boolean GetNeedFileRotationVal()
        {
            return _needFileRotation;
        }
        public void SetNeedFileRotationVal(Boolean value)
        {
            _needFileRotation = value;
        }
        Boolean IFileWriter.NeedFileRotation
        {
            get { return _needFileRotation; }
        }

        private String _encoding;
        public String GetEncodingVal()
        {
            return _encoding;
        }
        public void SetEncodingVal(String value)
        {
            _encoding = value;
        }
        String IFileWriter.Encoding
        {
            get { return _encoding; }
        }

        private CfgLogLevel _logLevel;
        public CfgLogLevel GetLogLevelVal()
        {
            return _logLevel;
        }
        public void SetLogLevelVal(CfgLogLevel value)
        {
            _logLevel = value;
        }
        CfgLogLevel ILoggerWriterConfiguration.LogLevel
        {
            get { return _logLevel; }
        }


        public FileWriterImplement Copy()
        {
        	var res = new FileWriterImplement();
        
            res._template = this._template;
            res._fileNameTemplate = this._fileNameTemplate;
            res._needFileRotation = this._needFileRotation;
            res._encoding = this._encoding;
            res._logLevel = this._logLevel;

            return res;
        }

        public static IFileWriter CopyInh(IFileWriter src)
        {
        	if (src == null)
        		return null;
        
        	if (src.GetType() == typeof(FileWriterImplement))
        		return ((FileWriterImplement)src).Copy();
        	if (src.GetType() == typeof(FileWriterImplement))
        		return ((FileWriterImplement)src).Copy();
        
        	throw new Exception("Unknown type: " + src.GetType().ToString());
        }
    }

    internal class DatabaseWriterImplement : IDatabaseWriter
    {
        public DatabaseWriterImplement()
        {
            this._storedProcedureName = DatabaseWriterConfiguration.DefaultStoredProcedureName;
            this._logLevel = CfgLogLevel.TRACE;
        }

        private String _connectionString;
        public String GetConnectionStringVal()
        {
            return _connectionString;
        }
        public void SetConnectionStringVal(String value)
        {
            _connectionString = value;
        }
        String IDatabaseWriter.ConnectionString
        {
            get { return _connectionString; }
        }

        private String _storedProcedureName;
        public String GetStoredProcedureNameVal()
        {
            return _storedProcedureName;
        }
        public void SetStoredProcedureNameVal(String value)
        {
            _storedProcedureName = value;
        }
        String IDatabaseWriter.StoredProcedureName
        {
            get { return _storedProcedureName; }
        }

        private CfgLogLevel _logLevel;
        public CfgLogLevel GetLogLevelVal()
        {
            return _logLevel;
        }
        public void SetLogLevelVal(CfgLogLevel value)
        {
            _logLevel = value;
        }
        CfgLogLevel ILoggerWriterConfiguration.LogLevel
        {
            get { return _logLevel; }
        }


        public DatabaseWriterImplement Copy()
        {
        	var res = new DatabaseWriterImplement();
        
            res._connectionString = this._connectionString;
            res._storedProcedureName = this._storedProcedureName;
            res._logLevel = this._logLevel;

            return res;
        }

        public static IDatabaseWriter CopyInh(IDatabaseWriter src)
        {
        	if (src == null)
        		return null;
        
        	if (src.GetType() == typeof(DatabaseWriterImplement))
        		return ((DatabaseWriterImplement)src).Copy();
        	if (src.GetType() == typeof(DatabaseWriterImplement))
        		return ((DatabaseWriterImplement)src).Copy();
        
        	throw new Exception("Unknown type: " + src.GetType().ToString());
        }
    }

    internal class PipeWriterImplement : IPipeWriter
    {
        public PipeWriterImplement()
        {
            this._serverName = PipeWriterConfiguration.DefaultServerName;
            this._pipeName = PipeWriterConfiguration.DefaultPipeName;
            this._logLevel = CfgLogLevel.TRACE;
        }

        private String _serverName;
        public String GetServerNameVal()
        {
            return _serverName;
        }
        public void SetServerNameVal(String value)
        {
            _serverName = value;
        }
        String IPipeWriter.ServerName
        {
            get { return _serverName; }
        }

        private String _pipeName;
        public String GetPipeNameVal()
        {
            return _pipeName;
        }
        public void SetPipeNameVal(String value)
        {
            _pipeName = value;
        }
        String IPipeWriter.PipeName
        {
            get { return _pipeName; }
        }

        private CfgLogLevel _logLevel;
        public CfgLogLevel GetLogLevelVal()
        {
            return _logLevel;
        }
        public void SetLogLevelVal(CfgLogLevel value)
        {
            _logLevel = value;
        }
        CfgLogLevel ILoggerWriterConfiguration.LogLevel
        {
            get { return _logLevel; }
        }


        public PipeWriterImplement Copy()
        {
        	var res = new PipeWriterImplement();
        
            res._serverName = this._serverName;
            res._pipeName = this._pipeName;
            res._logLevel = this._logLevel;

            return res;
        }

        public static IPipeWriter CopyInh(IPipeWriter src)
        {
        	if (src == null)
        		return null;
        
        	if (src.GetType() == typeof(PipeWriterImplement))
        		return ((PipeWriterImplement)src).Copy();
        	if (src.GetType() == typeof(PipeWriterImplement))
        		return ((PipeWriterImplement)src).Copy();
        
        	throw new Exception("Unknown type: " + src.GetType().ToString());
        }
    }

    internal class RoutingWrapperImplement : IRoutingWrapper
    {
        public RoutingWrapperImplement()
        {
            this._routingBySystem = new Dictionary<String, ILoggerWriterWrapperConfiguration>();
            this._fromAll = new List<ILoggerWriterWrapperConfiguration>();
            this._fromOthers = new List<ILoggerWriterWrapperConfiguration>();
        }

        private Dictionary<String, ILoggerWriterWrapperConfiguration> _routingBySystem;
        public Dictionary<String, ILoggerWriterWrapperConfiguration> GetRoutingBySystemVal()
        {
            return _routingBySystem;
        }
        public void SetRoutingBySystemVal(Dictionary<String, ILoggerWriterWrapperConfiguration> value)
        {
            _routingBySystem = value;
        }
        Dictionary<String, ILoggerWriterWrapperConfiguration> IRoutingWrapper.RoutingBySystem
        {
            get { return _routingBySystem; }
        }

        private List<ILoggerWriterWrapperConfiguration> _fromAll;
        public List<ILoggerWriterWrapperConfiguration> GetFromAllVal()
        {
            return _fromAll;
        }
        public void SetFromAllVal(List<ILoggerWriterWrapperConfiguration> value)
        {
            _fromAll = value;
        }
        List<ILoggerWriterWrapperConfiguration> IRoutingWrapper.FromAll
        {
            get { return _fromAll; }
        }

        private List<ILoggerWriterWrapperConfiguration> _fromOthers;
        public List<ILoggerWriterWrapperConfiguration> GetFromOthersVal()
        {
            return _fromOthers;
        }
        public void SetFromOthersVal(List<ILoggerWriterWrapperConfiguration> value)
        {
            _fromOthers = value;
        }
        List<ILoggerWriterWrapperConfiguration> IRoutingWrapper.FromOthers
        {
            get { return _fromOthers; }
        }


        public RoutingWrapperImplement Copy()
        {
        	var res = new RoutingWrapperImplement();
        
            if (this._routingBySystem == null)
                res._routingBySystem = null;
            else
                res._routingBySystem = _routingBySystem.ToDictionary(o => o.Key, o => LoggerWriterWrapperConfigurationImplement.CopyInh(o.Value));

            if (this._fromAll == null)
                res._fromAll = null;
            else
                res._fromAll = _fromAll.Select(o => LoggerWriterWrapperConfigurationImplement.CopyInh(o)).ToList();

            if (this._fromOthers == null)
                res._fromOthers = null;
            else
                res._fromOthers = _fromOthers.Select(o => LoggerWriterWrapperConfigurationImplement.CopyInh(o)).ToList();


            return res;
        }

        public static IRoutingWrapper CopyInh(IRoutingWrapper src)
        {
        	if (src == null)
        		return null;
        
        	if (src.GetType() == typeof(RoutingWrapperImplement))
        		return ((RoutingWrapperImplement)src).Copy();
        	if (src.GetType() == typeof(RoutingWrapperImplement))
        		return ((RoutingWrapperImplement)src).Copy();
        
        	throw new Exception("Unknown type: " + src.GetType().ToString());
        }
    }

    internal class PatternMatchingMatchImplement : IPatternMatchingMatch
    {
        public PatternMatchingMatchImplement()
        {
            this._writer = new LoggerWriterWrapperConfigurationImplement();
        }

        private String _value;
        public String GetValueVal()
        {
            return _value;
        }
        public void SetValueVal(String value)
        {
            _value = value;
        }
        String IPatternMatchingMatch.Value
        {
            get { return _value; }
        }

        private ILoggerWriterWrapperConfiguration _writer;
        public ILoggerWriterWrapperConfiguration GetWriterVal()
        {
            return _writer;
        }
        public void SetWriterVal(ILoggerWriterWrapperConfiguration value)
        {
            _writer = value;
        }
        ILoggerWriterWrapperConfiguration IPatternMatchingElement.Writer
        {
            get { return _writer; }
        }


        public PatternMatchingMatchImplement Copy()
        {
        	var res = new PatternMatchingMatchImplement();
        
            res._value = this._value;
            res._writer = LoggerWriterWrapperConfigurationImplement.CopyInh(this._writer);

            return res;
        }

        public static IPatternMatchingMatch CopyInh(IPatternMatchingMatch src)
        {
        	if (src == null)
        		return null;
        
        	if (src.GetType() == typeof(PatternMatchingMatchImplement))
        		return ((PatternMatchingMatchImplement)src).Copy();
        	if (src.GetType() == typeof(PatternMatchingMatchImplement))
        		return ((PatternMatchingMatchImplement)src).Copy();
        
        	throw new Exception("Unknown type: " + src.GetType().ToString());
        }
    }

    internal class PatternMatchingDefaultImplement : IPatternMatchingDefault
    {
        public PatternMatchingDefaultImplement()
        {
            this._writer = new LoggerWriterWrapperConfigurationImplement();
        }

        private ILoggerWriterWrapperConfiguration _writer;
        public ILoggerWriterWrapperConfiguration GetWriterVal()
        {
            return _writer;
        }
        public void SetWriterVal(ILoggerWriterWrapperConfiguration value)
        {
            _writer = value;
        }
        ILoggerWriterWrapperConfiguration IPatternMatchingElement.Writer
        {
            get { return _writer; }
        }


        public PatternMatchingDefaultImplement Copy()
        {
        	var res = new PatternMatchingDefaultImplement();
        
            res._writer = LoggerWriterWrapperConfigurationImplement.CopyInh(this._writer);

            return res;
        }

        public static IPatternMatchingDefault CopyInh(IPatternMatchingDefault src)
        {
        	if (src == null)
        		return null;
        
        	if (src.GetType() == typeof(PatternMatchingDefaultImplement))
        		return ((PatternMatchingDefaultImplement)src).Copy();
        	if (src.GetType() == typeof(PatternMatchingDefaultImplement))
        		return ((PatternMatchingDefaultImplement)src).Copy();
        
        	throw new Exception("Unknown type: " + src.GetType().ToString());
        }
    }


    // ============================

    internal class LoggerConfigurationSectionConfigClass : System.Configuration.ConfigurationSection
    {
    	private ILoggerConfigurationSection _configData = new LoggerConfigurationSectionImplement();
    	public ILoggerConfigurationSection ConfigData { get {return _configData; } }
     
    
    	public ILoggerConfigurationSection ExtractConfigData()
    	{
    		return LoggerConfigurationSectionImplement.CopyInh(_configData);
    	}
    
    	protected override void InitializeDefault()
        {
    		base.InitializeDefault();
            _configData = new LoggerConfigurationSectionImplement();
        }
    
        protected override void DeserializeSection(System.Xml.XmlReader reader)
        {
    		if (reader.NodeType == System.Xml.XmlNodeType.None)
    			reader.Read();
    		_configData = DeserializeILoggerConfigurationSectionElem(reader);
        }
    
        public override bool IsReadOnly()
        {
    		return true;
        }
    	
    
    	private T Parse<T>(string value)
        {
    		return (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(value);
        }
    
    
        private List<T> DeserializeList<T>(System.Xml.XmlReader reader, Func<System.Xml.XmlReader, T> readFnc, string expectedName)
        {
    	    if (reader.NodeType != System.Xml.XmlNodeType.Element)
                throw new System.Configuration.ConfigurationErrorsException("Expected Element node type", reader);
    
            List<T> res = new List<T>();
        
            if (reader.IsEmptyElement)
            {
                reader.Skip();
                return res;
            }
        
            string initialName = reader.Name;
        
            reader.ReadStartElement();
        
            do
            {
                if (reader.NodeType == System.Xml.XmlNodeType.Element)
                {
                    if (expectedName != null && reader.Name != expectedName)
                        throw new System.Configuration.ConfigurationErrorsException("Unexpected element name inside list: " + reader.Name, reader);
    
                    T elem = readFnc(reader);
                    res.Add(elem);
                }
                else
                {
                    reader.Skip();
                }
            }
            while (reader.NodeType != System.Xml.XmlNodeType.EndElement || reader.Name != initialName);
        
        	reader.ReadEndElement();
        
            return res;
        }
    
    
        private Dictionary<TKey, TValue> DeserializeDictionary<TKey, TValue>(System.Xml.XmlReader reader, Func<System.Xml.XmlReader, TValue> readFnc, string expectedName)
        {
    	    if (reader.NodeType != System.Xml.XmlNodeType.Element)
                throw new System.Configuration.ConfigurationErrorsException("Expected Element node type", reader);
    
            Dictionary<TKey, TValue> res = new Dictionary<TKey, TValue>();
        
            if (reader.IsEmptyElement)
            {
                reader.Skip();
                return res;
            }
        
            string initialName = reader.Name;
        
            reader.ReadStartElement();
        
            do
            {
                if (reader.NodeType == System.Xml.XmlNodeType.Element)
                {
                    if (expectedName != null && reader.Name != expectedName)
                        throw new System.Configuration.ConfigurationErrorsException("Unexpected element name inside list: " + reader.Name, reader);
    
                    string strKey = reader.GetAttribute("key");
                    if (strKey == null)
                        throw new System.Configuration.ConfigurationErrorsException("Key not found for dictionary: " + reader.Name, reader);
        
                    TKey key = Parse<TKey>(strKey);
                    TValue val = readFnc(reader);
        
                    res.Add(key, val);
                }
                else
                {
                    reader.Skip();
                }
            }
            while (reader.NodeType != System.Xml.XmlNodeType.EndElement || reader.Name != initialName);
        
        	reader.ReadEndElement();
        
            return res;
        }
        
        
        private T DeserializeSimpleValueElement<T>(System.Xml.XmlReader reader)
        {
    		if (reader.NodeType != System.Xml.XmlNodeType.Element)
                throw new System.Configuration.ConfigurationErrorsException("Expected Element node type", reader);
    
            string ElemName = reader.Name;
        
            string addValue = reader.GetAttribute("value");
            if (addValue == null)
                throw new System.Configuration.ConfigurationErrorsException("Value not found for SimpleValueElement '" + ElemName + "' inside element", reader);
        
            T res = Parse<T>(addValue);
        
            if (reader.IsEmptyElement)
            {
                reader.Read();
            }
            else
            {
                reader.Read();
                if (reader.MoveToContent() != System.Xml.XmlNodeType.EndElement)
                    throw new System.Configuration.ConfigurationErrorsException("SimpleValueElement '" + ElemName + "' can't contains any other elements", reader);
                reader.ReadEndElement();
            }
        
            return res;
        }
    
    
        private T DeserializeSimpleValueElement<T>(System.Xml.XmlReader reader, string expectedName)
        {
    	    if (reader.NodeType != System.Xml.XmlNodeType.Element)
                throw new System.Configuration.ConfigurationErrorsException("Expected Element node type", reader);
    
            if (expectedName != null && reader.Name != expectedName)
                throw new System.Configuration.ConfigurationErrorsException("Unexpected element name inside list: " + reader.Name, reader);
    
            string ElemName = reader.Name;
    
            string addValue = reader.GetAttribute("value");
            if (addValue == null)
                throw new System.Configuration.ConfigurationErrorsException("Value not found for SimpleValueElement '" + ElemName + "' inside element", reader);
    
            T res = Parse<T>(addValue);
    
            if (reader.IsEmptyElement)
            {
                reader.Read();
            }
            else
            {
                reader.Read();
                if (reader.MoveToContent() != System.Xml.XmlNodeType.EndElement)
                    throw new System.Configuration.ConfigurationErrorsException("SimpleValueElement '" + ElemName + "' can't contains any other elements", reader);
                reader.ReadEndElement();
            }
    
            return res;
        }
    
    
    
        private ILoggerConfigurationSection DeserializeILoggerConfigurationSectionElem(System.Xml.XmlReader reader)
        {
        	var res = new LoggerConfigurationSectionImplement();
        
        	HashSet<string> parsedElements = new HashSet<string>();
        

            if (reader.IsEmptyElement)
            {
            	reader.Skip();
            }
            else
            {
            	string initialName = reader.Name;
                reader.ReadStartElement();
            	do
                {
            		if (reader.NodeType != System.Xml.XmlNodeType.Element)
                    {
            			reader.Skip();
            		}
            		else
            		{			
                        switch (reader.Name)
                        {
                            case "add":
                                string addKey = reader.GetAttribute("key");
                                if (addKey == null)
                                	throw new System.Configuration.ConfigurationErrorsException("Key not found for 'add' inside element 'ILoggerConfigurationSection'", reader);	
                                
                                switch (addKey)
                                {
                                	default:
                                		throw new System.Configuration.ConfigurationErrorsException("Unknown key " + addKey + " inside element 'ILoggerConfigurationSection'", reader);
                                }
                            case "logger":
                                res.SetLoggerVal(DeserializeIRootLoggerConfigurationElem(reader));
                                parsedElements.Add("logger");
                                break;
                            default:
                                throw new System.Configuration.ConfigurationErrorsException("Unknown element inside 'ILoggerConfigurationSection': " + reader.Name, reader);
                        }
            		}
            	}
            	while (reader.NodeType != System.Xml.XmlNodeType.EndElement || reader.Name != initialName);
            
                reader.ReadEndElement();
            }
            
            HashSet<string> restElems = new HashSet<string>();
            restElems.Add("logger");
            restElems.RemoveWhere(o => parsedElements.Contains(o));
            if (restElems.Count > 0)
                throw new System.Configuration.ConfigurationErrorsException("Not all required properties readed: " + string.Join(", ",restElems));
            return res;
        }

        private ILoggerConfigurationSection DeserializeILoggerConfigurationSectionElem(System.Xml.XmlReader reader, string expectedName)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Expected Element node type", reader);
        
        	if (expectedName != null && reader.Name != expectedName)
        		throw new System.Configuration.ConfigurationErrorsException("Unexpected element name for type 'ILoggerConfigurationSection': " + reader.Name, reader);
            
        	return DeserializeILoggerConfigurationSectionElem(reader);
        }

        private ILoggerConfigurationSection DeserializeILoggerConfigurationSectionElemWithInh(System.Xml.XmlReader reader)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Not an Element node type", reader);
        
        	switch (reader.Name)
        	{
                case "loggerConfigurationSection":
                    return DeserializeILoggerConfigurationSectionElem(reader);
                default:
                    throw new System.Configuration.ConfigurationErrorsException("Unknown child type name: '" + reader.Name + "' for base type 'ILoggerConfigurationSection'", reader);
            }
        }


        private IRootLoggerConfiguration DeserializeIRootLoggerConfigurationElem(System.Xml.XmlReader reader)
        {
        	var res = new RootLoggerConfigurationImplement();
        
        	HashSet<string> parsedElements = new HashSet<string>();
        
            string attribGenTempVal = null;
            attribGenTempVal = reader.GetAttribute("enableStackTraceExtraction");
            if (attribGenTempVal != null)
                res.SetEnableStackTraceExtractionVal(Parse<Boolean>(attribGenTempVal));

            attribGenTempVal = reader.GetAttribute("logLevel");
            if (attribGenTempVal != null)
                res.SetLogLevelVal(Parse<CfgLogLevel>(attribGenTempVal));
            else
                throw new System.Configuration.ConfigurationErrorsException("Attribute 'logLevel for element 'IRootLoggerConfiguration' not defined", reader);

            attribGenTempVal = reader.GetAttribute("isEnabled");
            if (attribGenTempVal != null)
                res.SetIsEnabledVal(Parse<Boolean>(attribGenTempVal));


            if (reader.IsEmptyElement)
            {
            	reader.Skip();
            }
            else
            {
            	string initialName = reader.Name;
            	do
                {
            		if (reader.NodeType != System.Xml.XmlNodeType.Element)
                    {
            			reader.Skip();
            		}
            		else
            		{			
                        reader.Read();
                        if (reader.MoveToContent() != System.Xml.XmlNodeType.Element)
                            throw new System.Configuration.ConfigurationErrorsException("Injected element not found for property 'writerWrapper' inside 'IRootLoggerConfiguration", reader);
                        res.SetWriterWrapperVal(DeserializeILoggerWriterWrapperConfigurationElemWithInh(reader));
                        if (reader.MoveToContent() != System.Xml.XmlNodeType.EndElement)
                            throw new System.Configuration.ConfigurationErrorsException("Injected element 'writerWrapper' inside 'IRootLoggerConfiguration can't contain several subelements", reader);
                        reader.ReadEndElement();
                        parsedElements.Add("writerWrapper");
                        break;
            		}
            	}
            	while (reader.NodeType != System.Xml.XmlNodeType.EndElement || reader.Name != initialName);
            
            }
            
            HashSet<string> restElems = new HashSet<string>();
            restElems.Add("writerWrapper");
            restElems.RemoveWhere(o => parsedElements.Contains(o));
            if (restElems.Count > 0)
                throw new System.Configuration.ConfigurationErrorsException("Not all required properties readed: " + string.Join(", ",restElems));
            return res;
        }

        private IRootLoggerConfiguration DeserializeIRootLoggerConfigurationElem(System.Xml.XmlReader reader, string expectedName)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Expected Element node type", reader);
        
        	if (expectedName != null && reader.Name != expectedName)
        		throw new System.Configuration.ConfigurationErrorsException("Unexpected element name for type 'IRootLoggerConfiguration': " + reader.Name, reader);
            
        	return DeserializeIRootLoggerConfigurationElem(reader);
        }

        private IRootLoggerConfiguration DeserializeIRootLoggerConfigurationElemWithInh(System.Xml.XmlReader reader)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Not an Element node type", reader);
        
        	switch (reader.Name)
        	{
                case "rootLoggerConfiguration":
                    return DeserializeIRootLoggerConfigurationElem(reader);
                default:
                    throw new System.Configuration.ConfigurationErrorsException("Unknown child type name: '" + reader.Name + "' for base type 'IRootLoggerConfiguration'", reader);
            }
        }


        private ILoggerWriterWrapperConfiguration DeserializeILoggerWriterWrapperConfigurationElem(System.Xml.XmlReader reader)
        {
        	var res = new LoggerWriterWrapperConfigurationImplement();
        
        	HashSet<string> parsedElements = new HashSet<string>();
        

            if (reader.IsEmptyElement)
            {
            	reader.Skip();
            }
            else
            {
            	string initialName = reader.Name;
                reader.ReadStartElement();
            	do
                {
            		if (reader.NodeType != System.Xml.XmlNodeType.Element)
                    {
            			reader.Skip();
            		}
            		else
            		{			
                        switch (reader.Name)
                        {
                            case "add":
                                string addKey = reader.GetAttribute("key");
                                if (addKey == null)
                                	throw new System.Configuration.ConfigurationErrorsException("Key not found for 'add' inside element 'ILoggerWriterWrapperConfiguration'", reader);	
                                
                                switch (addKey)
                                {
                                	default:
                                		throw new System.Configuration.ConfigurationErrorsException("Unknown key " + addKey + " inside element 'ILoggerWriterWrapperConfiguration'", reader);
                                }
                            default:
                                throw new System.Configuration.ConfigurationErrorsException("Unknown element inside 'ILoggerWriterWrapperConfiguration': " + reader.Name, reader);
                        }
            		}
            	}
            	while (reader.NodeType != System.Xml.XmlNodeType.EndElement || reader.Name != initialName);
            
                reader.ReadEndElement();
            }
            
            HashSet<string> restElems = new HashSet<string>();
            restElems.RemoveWhere(o => parsedElements.Contains(o));
            if (restElems.Count > 0)
                throw new System.Configuration.ConfigurationErrorsException("Not all required properties readed: " + string.Join(", ",restElems));
            return res;
        }

        private ILoggerWriterWrapperConfiguration DeserializeILoggerWriterWrapperConfigurationElem(System.Xml.XmlReader reader, string expectedName)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Expected Element node type", reader);
        
        	if (expectedName != null && reader.Name != expectedName)
        		throw new System.Configuration.ConfigurationErrorsException("Unexpected element name for type 'ILoggerWriterWrapperConfiguration': " + reader.Name, reader);
            
        	return DeserializeILoggerWriterWrapperConfigurationElem(reader);
        }

        private ILoggerWriterWrapperConfiguration DeserializeILoggerWriterWrapperConfigurationElemWithInh(System.Xml.XmlReader reader)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Not an Element node type", reader);
        
        	switch (reader.Name)
        	{
                case "groupWrapper":
                    return DeserializeIGroupWrapperElem(reader);
                case "asyncReliableQueueWrapper":
                    return DeserializeIAsyncReliableQueueWrapperElem(reader);
                case "reliableWrapper":
                    return DeserializeIReliableWrapperElem(reader);
                case "networkWriter":
                    return DeserializeINetworkWriterElem(reader);
                case "asyncQueueWrapper":
                    return DeserializeIAsyncQueueWrapperElem(reader);
                case "patternMatchingWrapper":
                    return DeserializeIPatternMatchingWrapperElem(reader);
                case "emptyWriter":
                    return DeserializeIEmptyWriterElem(reader);
                case "consoleWriter":
                    return DeserializeIConsoleWriterElem(reader);
                case "fileWriter":
                    return DeserializeIFileWriterElem(reader);
                case "databaseWriter":
                    return DeserializeIDatabaseWriterElem(reader);
                case "pipeWriter":
                    return DeserializeIPipeWriterElem(reader);
                case "routingWrapper":
                    return DeserializeIRoutingWrapperElem(reader);
                default:
                    throw new System.Configuration.ConfigurationErrorsException("Unknown child type name: '" + reader.Name + "' for base type 'ILoggerWriterWrapperConfiguration'", reader);
            }
        }


        private IGroupWrapper DeserializeIGroupWrapperElem(System.Xml.XmlReader reader)
        {
        	var res = new GroupWrapperImplement();
        
        	HashSet<string> parsedElements = new HashSet<string>();
        

            if (reader.IsEmptyElement)
            {
            	reader.Skip();
            }
            else
            {
            	string initialName = reader.Name;
            	do
                {
            		if (reader.NodeType != System.Xml.XmlNodeType.Element)
                    {
            			reader.Skip();
            		}
            		else
            		{			
                        var tmp_Loggers = DeserializeList(reader, DeserializeILoggerWriterWrapperConfigurationElemWithInh, null);
                        res.SetLoggersVal(tmp_Loggers);
                        break;
            		}
            	}
            	while (reader.NodeType != System.Xml.XmlNodeType.EndElement || reader.Name != initialName);
            
            }
            
            HashSet<string> restElems = new HashSet<string>();
            restElems.RemoveWhere(o => parsedElements.Contains(o));
            if (restElems.Count > 0)
                throw new System.Configuration.ConfigurationErrorsException("Not all required properties readed: " + string.Join(", ",restElems));
            return res;
        }

        private IGroupWrapper DeserializeIGroupWrapperElem(System.Xml.XmlReader reader, string expectedName)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Expected Element node type", reader);
        
        	if (expectedName != null && reader.Name != expectedName)
        		throw new System.Configuration.ConfigurationErrorsException("Unexpected element name for type 'IGroupWrapper': " + reader.Name, reader);
            
        	return DeserializeIGroupWrapperElem(reader);
        }

        private IGroupWrapper DeserializeIGroupWrapperElemWithInh(System.Xml.XmlReader reader)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Not an Element node type", reader);
        
        	switch (reader.Name)
        	{
                case "groupWrapper":
                    return DeserializeIGroupWrapperElem(reader);
                default:
                    throw new System.Configuration.ConfigurationErrorsException("Unknown child type name: '" + reader.Name + "' for base type 'IGroupWrapper'", reader);
            }
        }


        private IAsyncReliableQueueWrapper DeserializeIAsyncReliableQueueWrapperElem(System.Xml.XmlReader reader)
        {
        	var res = new AsyncReliableQueueWrapperImplement();
        
        	HashSet<string> parsedElements = new HashSet<string>();
        
            string attribGenTempVal = null;
            attribGenTempVal = reader.GetAttribute("maxQueueSize");
            if (attribGenTempVal != null)
                res.SetMaxQueueSizeVal(Parse<Int32>(attribGenTempVal));

            attribGenTempVal = reader.GetAttribute("isDiscardExcess");
            if (attribGenTempVal != null)
                res.SetIsDiscardExcessVal(Parse<Boolean>(attribGenTempVal));

            attribGenTempVal = reader.GetAttribute("folderForTemporaryStore");
            if (attribGenTempVal != null)
                res.SetFolderForTemporaryStoreVal(Parse<String>(attribGenTempVal));

            attribGenTempVal = reader.GetAttribute("maxFileSize");
            if (attribGenTempVal != null)
                res.SetMaxFileSizeVal(Parse<Int64>(attribGenTempVal));


            if (reader.IsEmptyElement)
            {
            	reader.Skip();
            }
            else
            {
            	string initialName = reader.Name;
            	do
                {
            		if (reader.NodeType != System.Xml.XmlNodeType.Element)
                    {
            			reader.Skip();
            		}
            		else
            		{			
                        reader.Read();
                        if (reader.MoveToContent() != System.Xml.XmlNodeType.Element)
                            throw new System.Configuration.ConfigurationErrorsException("Injected element not found for property 'logger' inside 'IAsyncReliableQueueWrapper", reader);
                        res.SetLoggerVal(DeserializeILoggerWriterWrapperConfigurationElemWithInh(reader));
                        if (reader.MoveToContent() != System.Xml.XmlNodeType.EndElement)
                            throw new System.Configuration.ConfigurationErrorsException("Injected element 'logger' inside 'IAsyncReliableQueueWrapper can't contain several subelements", reader);
                        reader.ReadEndElement();
                        parsedElements.Add("logger");
                        break;
            		}
            	}
            	while (reader.NodeType != System.Xml.XmlNodeType.EndElement || reader.Name != initialName);
            
            }
            
            HashSet<string> restElems = new HashSet<string>();
            restElems.Add("logger");
            restElems.RemoveWhere(o => parsedElements.Contains(o));
            if (restElems.Count > 0)
                throw new System.Configuration.ConfigurationErrorsException("Not all required properties readed: " + string.Join(", ",restElems));
            return res;
        }

        private IAsyncReliableQueueWrapper DeserializeIAsyncReliableQueueWrapperElem(System.Xml.XmlReader reader, string expectedName)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Expected Element node type", reader);
        
        	if (expectedName != null && reader.Name != expectedName)
        		throw new System.Configuration.ConfigurationErrorsException("Unexpected element name for type 'IAsyncReliableQueueWrapper': " + reader.Name, reader);
            
        	return DeserializeIAsyncReliableQueueWrapperElem(reader);
        }

        private IAsyncReliableQueueWrapper DeserializeIAsyncReliableQueueWrapperElemWithInh(System.Xml.XmlReader reader)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Not an Element node type", reader);
        
        	switch (reader.Name)
        	{
                case "asyncReliableQueueWrapper":
                    return DeserializeIAsyncReliableQueueWrapperElem(reader);
                default:
                    throw new System.Configuration.ConfigurationErrorsException("Unknown child type name: '" + reader.Name + "' for base type 'IAsyncReliableQueueWrapper'", reader);
            }
        }


        private IReliableWrapper DeserializeIReliableWrapperElem(System.Xml.XmlReader reader)
        {
        	var res = new ReliableWrapperImplement();
        
        	HashSet<string> parsedElements = new HashSet<string>();
        
            string attribGenTempVal = null;
            attribGenTempVal = reader.GetAttribute("folderForTemporaryStore");
            if (attribGenTempVal != null)
                res.SetFolderForTemporaryStoreVal(Parse<String>(attribGenTempVal));
            else
                throw new System.Configuration.ConfigurationErrorsException("Attribute 'folderForTemporaryStore' for element 'IReliableWrapper' not defined", reader);

            attribGenTempVal = reader.GetAttribute("maxFileSize");
            if (attribGenTempVal != null)
                res.SetMaxFileSizeVal(Parse<Int64>(attribGenTempVal));


            if (reader.IsEmptyElement)
            {
            	reader.Skip();
            }
            else
            {
            	string initialName = reader.Name;
            	do
                {
            		if (reader.NodeType != System.Xml.XmlNodeType.Element)
                    {
            			reader.Skip();
            		}
            		else
            		{			
                        reader.Read();
                        if (reader.MoveToContent() != System.Xml.XmlNodeType.Element)
                            throw new System.Configuration.ConfigurationErrorsException("Injected element not found for property 'logger' inside 'IReliableWrapper", reader);
                        res.SetLoggerVal(DeserializeILoggerWriterWrapperConfigurationElemWithInh(reader));
                        if (reader.MoveToContent() != System.Xml.XmlNodeType.EndElement)
                            throw new System.Configuration.ConfigurationErrorsException("Injected element 'logger' inside 'IReliableWrapper can't contain several subelements", reader);
                        reader.ReadEndElement();
                        parsedElements.Add("logger");
                        break;
            		}
            	}
            	while (reader.NodeType != System.Xml.XmlNodeType.EndElement || reader.Name != initialName);
            
            }
            
            HashSet<string> restElems = new HashSet<string>();
            restElems.Add("logger");
            restElems.RemoveWhere(o => parsedElements.Contains(o));
            if (restElems.Count > 0)
                throw new System.Configuration.ConfigurationErrorsException("Not all required properties readed: " + string.Join(", ",restElems));
            return res;
        }

        private IReliableWrapper DeserializeIReliableWrapperElem(System.Xml.XmlReader reader, string expectedName)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Expected Element node type", reader);
        
        	if (expectedName != null && reader.Name != expectedName)
        		throw new System.Configuration.ConfigurationErrorsException("Unexpected element name for type 'IReliableWrapper': " + reader.Name, reader);
            
        	return DeserializeIReliableWrapperElem(reader);
        }

        private IReliableWrapper DeserializeIReliableWrapperElemWithInh(System.Xml.XmlReader reader)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Not an Element node type", reader);
        
        	switch (reader.Name)
        	{
                case "reliableWrapper":
                    return DeserializeIReliableWrapperElem(reader);
                default:
                    throw new System.Configuration.ConfigurationErrorsException("Unknown child type name: '" + reader.Name + "' for base type 'IReliableWrapper'", reader);
            }
        }


        private INetworkWriter DeserializeINetworkWriterElem(System.Xml.XmlReader reader)
        {
        	var res = new NetworkWriterImplement();
        
        	HashSet<string> parsedElements = new HashSet<string>();
        
            string attribGenTempVal = null;
            attribGenTempVal = reader.GetAttribute("serverAddress");
            if (attribGenTempVal != null)
                res.SetServerAddressVal(Parse<String>(attribGenTempVal));
            else
                throw new System.Configuration.ConfigurationErrorsException("Attribute 'serverAddress for element 'INetworkWriter' not defined", reader);

            attribGenTempVal = reader.GetAttribute("port");
            if (attribGenTempVal != null)
                res.SetPortVal(Parse<Int32>(attribGenTempVal));

            attribGenTempVal = reader.GetAttribute("logLevel");
            if (attribGenTempVal != null)
                res.SetLogLevelVal(Parse<CfgLogLevel>(attribGenTempVal));


            if (reader.IsEmptyElement)
            {
            	reader.Skip();
            }
            else
            {
            	string initialName = reader.Name;
                reader.ReadStartElement();
            	do
                {
            		if (reader.NodeType != System.Xml.XmlNodeType.Element)
                    {
            			reader.Skip();
            		}
            		else
            		{			
                        switch (reader.Name)
                        {
                            case "add":
                                string addKey = reader.GetAttribute("key");
                                if (addKey == null)
                                	throw new System.Configuration.ConfigurationErrorsException("Key not found for 'add' inside element 'INetworkWriter'", reader);	
                                
                                switch (addKey)
                                {
                                	default:
                                		throw new System.Configuration.ConfigurationErrorsException("Unknown key " + addKey + " inside element 'INetworkWriter'", reader);
                                }
                            default:
                                throw new System.Configuration.ConfigurationErrorsException("Unknown element inside 'INetworkWriter': " + reader.Name, reader);
                        }
            		}
            	}
            	while (reader.NodeType != System.Xml.XmlNodeType.EndElement || reader.Name != initialName);
            
                reader.ReadEndElement();
            }
            
            HashSet<string> restElems = new HashSet<string>();
            restElems.RemoveWhere(o => parsedElements.Contains(o));
            if (restElems.Count > 0)
                throw new System.Configuration.ConfigurationErrorsException("Not all required properties readed: " + string.Join(", ",restElems));
            return res;
        }

        private INetworkWriter DeserializeINetworkWriterElem(System.Xml.XmlReader reader, string expectedName)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Expected Element node type", reader);
        
        	if (expectedName != null && reader.Name != expectedName)
        		throw new System.Configuration.ConfigurationErrorsException("Unexpected element name for type 'INetworkWriter': " + reader.Name, reader);
            
        	return DeserializeINetworkWriterElem(reader);
        }

        private INetworkWriter DeserializeINetworkWriterElemWithInh(System.Xml.XmlReader reader)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Not an Element node type", reader);
        
        	switch (reader.Name)
        	{
                case "networkWriter":
                    return DeserializeINetworkWriterElem(reader);
                default:
                    throw new System.Configuration.ConfigurationErrorsException("Unknown child type name: '" + reader.Name + "' for base type 'INetworkWriter'", reader);
            }
        }


        private IAsyncQueueWrapper DeserializeIAsyncQueueWrapperElem(System.Xml.XmlReader reader)
        {
        	var res = new AsyncQueueWrapperImplement();
        
        	HashSet<string> parsedElements = new HashSet<string>();
        
            string attribGenTempVal = null;
            attribGenTempVal = reader.GetAttribute("maxQueueSize");
            if (attribGenTempVal != null)
                res.SetMaxQueueSizeVal(Parse<Int32>(attribGenTempVal));

            attribGenTempVal = reader.GetAttribute("isDiscardExcess");
            if (attribGenTempVal != null)
                res.SetIsDiscardExcessVal(Parse<Boolean>(attribGenTempVal));


            if (reader.IsEmptyElement)
            {
            	reader.Skip();
            }
            else
            {
            	string initialName = reader.Name;
            	do
                {
            		if (reader.NodeType != System.Xml.XmlNodeType.Element)
                    {
            			reader.Skip();
            		}
            		else
            		{			
                        reader.Read();
                        if (reader.MoveToContent() != System.Xml.XmlNodeType.Element)
                            throw new System.Configuration.ConfigurationErrorsException("Injected element not found for property 'logger' inside 'IAsyncQueueWrapper", reader);
                        res.SetLoggerVal(DeserializeILoggerWriterWrapperConfigurationElemWithInh(reader));
                        if (reader.MoveToContent() != System.Xml.XmlNodeType.EndElement)
                            throw new System.Configuration.ConfigurationErrorsException("Injected element 'logger' inside 'IAsyncQueueWrapper can't contain several subelements", reader);
                        reader.ReadEndElement();
                        parsedElements.Add("logger");
                        break;
            		}
            	}
            	while (reader.NodeType != System.Xml.XmlNodeType.EndElement || reader.Name != initialName);
            
            }
            
            HashSet<string> restElems = new HashSet<string>();
            restElems.Add("logger");
            restElems.RemoveWhere(o => parsedElements.Contains(o));
            if (restElems.Count > 0)
                throw new System.Configuration.ConfigurationErrorsException("Not all required properties readed: " + string.Join(", ",restElems));
            return res;
        }

        private IAsyncQueueWrapper DeserializeIAsyncQueueWrapperElem(System.Xml.XmlReader reader, string expectedName)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Expected Element node type", reader);
        
        	if (expectedName != null && reader.Name != expectedName)
        		throw new System.Configuration.ConfigurationErrorsException("Unexpected element name for type 'IAsyncQueueWrapper': " + reader.Name, reader);
            
        	return DeserializeIAsyncQueueWrapperElem(reader);
        }

        private IAsyncQueueWrapper DeserializeIAsyncQueueWrapperElemWithInh(System.Xml.XmlReader reader)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Not an Element node type", reader);
        
        	switch (reader.Name)
        	{
                case "asyncQueueWrapper":
                    return DeserializeIAsyncQueueWrapperElem(reader);
                default:
                    throw new System.Configuration.ConfigurationErrorsException("Unknown child type name: '" + reader.Name + "' for base type 'IAsyncQueueWrapper'", reader);
            }
        }


        private IPatternMatchingWrapper DeserializeIPatternMatchingWrapperElem(System.Xml.XmlReader reader)
        {
        	var res = new PatternMatchingWrapperImplement();
        
        	HashSet<string> parsedElements = new HashSet<string>();
        
            string attribGenTempVal = null;
            attribGenTempVal = reader.GetAttribute("pattern");
            if (attribGenTempVal != null)
                res.SetPatternVal(Parse<String>(attribGenTempVal));
            else
                throw new System.Configuration.ConfigurationErrorsException("Attribute 'pattern for element 'IPatternMatchingWrapper' not defined", reader);


            if (reader.IsEmptyElement)
            {
            	reader.Skip();
            }
            else
            {
            	string initialName = reader.Name;
            	do
                {
            		if (reader.NodeType != System.Xml.XmlNodeType.Element)
                    {
            			reader.Skip();
            		}
            		else
            		{			
                        var tmp_Writers = DeserializeList(reader, DeserializeIPatternMatchingElementElemWithInh, null);
                        res.SetWritersVal(tmp_Writers);
                        break;
            		}
            	}
            	while (reader.NodeType != System.Xml.XmlNodeType.EndElement || reader.Name != initialName);
            
            }
            
            HashSet<string> restElems = new HashSet<string>();
            restElems.RemoveWhere(o => parsedElements.Contains(o));
            if (restElems.Count > 0)
                throw new System.Configuration.ConfigurationErrorsException("Not all required properties readed: " + string.Join(", ",restElems));
            return res;
        }

        private IPatternMatchingWrapper DeserializeIPatternMatchingWrapperElem(System.Xml.XmlReader reader, string expectedName)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Expected Element node type", reader);
        
        	if (expectedName != null && reader.Name != expectedName)
        		throw new System.Configuration.ConfigurationErrorsException("Unexpected element name for type 'IPatternMatchingWrapper': " + reader.Name, reader);
            
        	return DeserializeIPatternMatchingWrapperElem(reader);
        }

        private IPatternMatchingWrapper DeserializeIPatternMatchingWrapperElemWithInh(System.Xml.XmlReader reader)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Not an Element node type", reader);
        
        	switch (reader.Name)
        	{
                case "patternMatchingWrapper":
                    return DeserializeIPatternMatchingWrapperElem(reader);
                default:
                    throw new System.Configuration.ConfigurationErrorsException("Unknown child type name: '" + reader.Name + "' for base type 'IPatternMatchingWrapper'", reader);
            }
        }


        private IPatternMatchingElement DeserializeIPatternMatchingElementElem(System.Xml.XmlReader reader)
        {
        	var res = new PatternMatchingElementImplement();
        
        	HashSet<string> parsedElements = new HashSet<string>();
        

            if (reader.IsEmptyElement)
            {
            	reader.Skip();
            }
            else
            {
            	string initialName = reader.Name;
            	do
                {
            		if (reader.NodeType != System.Xml.XmlNodeType.Element)
                    {
            			reader.Skip();
            		}
            		else
            		{			
                        reader.Read();
                        if (reader.MoveToContent() != System.Xml.XmlNodeType.Element)
                            throw new System.Configuration.ConfigurationErrorsException("Injected element not found for property 'writer' inside 'IPatternMatchingElement", reader);
                        res.SetWriterVal(DeserializeILoggerWriterWrapperConfigurationElemWithInh(reader));
                        if (reader.MoveToContent() != System.Xml.XmlNodeType.EndElement)
                            throw new System.Configuration.ConfigurationErrorsException("Injected element 'writer' inside 'IPatternMatchingElement can't contain several subelements", reader);
                        reader.ReadEndElement();
                        parsedElements.Add("writer");
                        break;
            		}
            	}
            	while (reader.NodeType != System.Xml.XmlNodeType.EndElement || reader.Name != initialName);
            
            }
            
            HashSet<string> restElems = new HashSet<string>();
            restElems.Add("writer");
            restElems.RemoveWhere(o => parsedElements.Contains(o));
            if (restElems.Count > 0)
                throw new System.Configuration.ConfigurationErrorsException("Not all required properties readed: " + string.Join(", ",restElems));
            return res;
        }

        private IPatternMatchingElement DeserializeIPatternMatchingElementElem(System.Xml.XmlReader reader, string expectedName)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Expected Element node type", reader);
        
        	if (expectedName != null && reader.Name != expectedName)
        		throw new System.Configuration.ConfigurationErrorsException("Unexpected element name for type 'IPatternMatchingElement': " + reader.Name, reader);
            
        	return DeserializeIPatternMatchingElementElem(reader);
        }

        private IPatternMatchingElement DeserializeIPatternMatchingElementElemWithInh(System.Xml.XmlReader reader)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Not an Element node type", reader);
        
        	switch (reader.Name)
        	{
                case "match":
                    return DeserializeIPatternMatchingMatchElem(reader);
                case "default":
                    return DeserializeIPatternMatchingDefaultElem(reader);
                default:
                    throw new System.Configuration.ConfigurationErrorsException("Unknown child type name: '" + reader.Name + "' for base type 'IPatternMatchingElement'", reader);
            }
        }


        private IEmptyWriter DeserializeIEmptyWriterElem(System.Xml.XmlReader reader)
        {
        	var res = new EmptyWriterImplement();
        
        	HashSet<string> parsedElements = new HashSet<string>();
        
            string attribGenTempVal = null;
            attribGenTempVal = reader.GetAttribute("logLevel");
            if (attribGenTempVal != null)
                res.SetLogLevelVal(Parse<CfgLogLevel>(attribGenTempVal));


            if (reader.IsEmptyElement)
            {
            	reader.Skip();
            }
            else
            {
            	string initialName = reader.Name;
                reader.ReadStartElement();
            	do
                {
            		if (reader.NodeType != System.Xml.XmlNodeType.Element)
                    {
            			reader.Skip();
            		}
            		else
            		{			
                        switch (reader.Name)
                        {
                            case "add":
                                string addKey = reader.GetAttribute("key");
                                if (addKey == null)
                                	throw new System.Configuration.ConfigurationErrorsException("Key not found for 'add' inside element 'IEmptyWriter'", reader);	
                                
                                switch (addKey)
                                {
                                	default:
                                		throw new System.Configuration.ConfigurationErrorsException("Unknown key " + addKey + " inside element 'IEmptyWriter'", reader);
                                }
                            default:
                                throw new System.Configuration.ConfigurationErrorsException("Unknown element inside 'IEmptyWriter': " + reader.Name, reader);
                        }
            		}
            	}
            	while (reader.NodeType != System.Xml.XmlNodeType.EndElement || reader.Name != initialName);
            
                reader.ReadEndElement();
            }
            
            HashSet<string> restElems = new HashSet<string>();
            restElems.RemoveWhere(o => parsedElements.Contains(o));
            if (restElems.Count > 0)
                throw new System.Configuration.ConfigurationErrorsException("Not all required properties readed: " + string.Join(", ",restElems));
            return res;
        }

        private IEmptyWriter DeserializeIEmptyWriterElem(System.Xml.XmlReader reader, string expectedName)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Expected Element node type", reader);
        
        	if (expectedName != null && reader.Name != expectedName)
        		throw new System.Configuration.ConfigurationErrorsException("Unexpected element name for type 'IEmptyWriter': " + reader.Name, reader);
            
        	return DeserializeIEmptyWriterElem(reader);
        }

        private IEmptyWriter DeserializeIEmptyWriterElemWithInh(System.Xml.XmlReader reader)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Not an Element node type", reader);
        
        	switch (reader.Name)
        	{
                case "emptyWriter":
                    return DeserializeIEmptyWriterElem(reader);
                default:
                    throw new System.Configuration.ConfigurationErrorsException("Unknown child type name: '" + reader.Name + "' for base type 'IEmptyWriter'", reader);
            }
        }


        private IConsoleWriter DeserializeIConsoleWriterElem(System.Xml.XmlReader reader)
        {
        	var res = new ConsoleWriterImplement();
        
        	HashSet<string> parsedElements = new HashSet<string>();
        
            string attribGenTempVal = null;
            attribGenTempVal = reader.GetAttribute("template");
            if (attribGenTempVal != null)
                res.SetTemplateVal(Parse<String>(attribGenTempVal));

            attribGenTempVal = reader.GetAttribute("logLevel");
            if (attribGenTempVal != null)
                res.SetLogLevelVal(Parse<CfgLogLevel>(attribGenTempVal));


            if (reader.IsEmptyElement)
            {
            	reader.Skip();
            }
            else
            {
            	string initialName = reader.Name;
                reader.ReadStartElement();
            	do
                {
            		if (reader.NodeType != System.Xml.XmlNodeType.Element)
                    {
            			reader.Skip();
            		}
            		else
            		{			
                        switch (reader.Name)
                        {
                            case "add":
                                string addKey = reader.GetAttribute("key");
                                if (addKey == null)
                                	throw new System.Configuration.ConfigurationErrorsException("Key not found for 'add' inside element 'IConsoleWriter'", reader);	
                                
                                switch (addKey)
                                {
                                	default:
                                		throw new System.Configuration.ConfigurationErrorsException("Unknown key " + addKey + " inside element 'IConsoleWriter'", reader);
                                }
                            default:
                                throw new System.Configuration.ConfigurationErrorsException("Unknown element inside 'IConsoleWriter': " + reader.Name, reader);
                        }
            		}
            	}
            	while (reader.NodeType != System.Xml.XmlNodeType.EndElement || reader.Name != initialName);
            
                reader.ReadEndElement();
            }
            
            HashSet<string> restElems = new HashSet<string>();
            restElems.RemoveWhere(o => parsedElements.Contains(o));
            if (restElems.Count > 0)
                throw new System.Configuration.ConfigurationErrorsException("Not all required properties readed: " + string.Join(", ",restElems));
            return res;
        }

        private IConsoleWriter DeserializeIConsoleWriterElem(System.Xml.XmlReader reader, string expectedName)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Expected Element node type", reader);
        
        	if (expectedName != null && reader.Name != expectedName)
        		throw new System.Configuration.ConfigurationErrorsException("Unexpected element name for type 'IConsoleWriter': " + reader.Name, reader);
            
        	return DeserializeIConsoleWriterElem(reader);
        }

        private IConsoleWriter DeserializeIConsoleWriterElemWithInh(System.Xml.XmlReader reader)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Not an Element node type", reader);
        
        	switch (reader.Name)
        	{
                case "consoleWriter":
                    return DeserializeIConsoleWriterElem(reader);
                default:
                    throw new System.Configuration.ConfigurationErrorsException("Unknown child type name: '" + reader.Name + "' for base type 'IConsoleWriter'", reader);
            }
        }


        private IFileWriter DeserializeIFileWriterElem(System.Xml.XmlReader reader)
        {
        	var res = new FileWriterImplement();
        
        	HashSet<string> parsedElements = new HashSet<string>();
        
            string attribGenTempVal = null;
            attribGenTempVal = reader.GetAttribute("template");
            if (attribGenTempVal != null)
                res.SetTemplateVal(Parse<String>(attribGenTempVal));

            attribGenTempVal = reader.GetAttribute("fileNameTemplate");
            if (attribGenTempVal != null)
                res.SetFileNameTemplateVal(Parse<String>(attribGenTempVal));

            attribGenTempVal = reader.GetAttribute("needFileRotation");
            if (attribGenTempVal != null)
                res.SetNeedFileRotationVal(Parse<Boolean>(attribGenTempVal));

            attribGenTempVal = reader.GetAttribute("encoding");
            if (attribGenTempVal != null)
                res.SetEncodingVal(Parse<String>(attribGenTempVal));

            attribGenTempVal = reader.GetAttribute("logLevel");
            if (attribGenTempVal != null)
                res.SetLogLevelVal(Parse<CfgLogLevel>(attribGenTempVal));


            if (reader.IsEmptyElement)
            {
            	reader.Skip();
            }
            else
            {
            	string initialName = reader.Name;
                reader.ReadStartElement();
            	do
                {
            		if (reader.NodeType != System.Xml.XmlNodeType.Element)
                    {
            			reader.Skip();
            		}
            		else
            		{			
                        switch (reader.Name)
                        {
                            case "add":
                                string addKey = reader.GetAttribute("key");
                                if (addKey == null)
                                	throw new System.Configuration.ConfigurationErrorsException("Key not found for 'add' inside element 'IFileWriter'", reader);	
                                
                                switch (addKey)
                                {
                                	default:
                                		throw new System.Configuration.ConfigurationErrorsException("Unknown key " + addKey + " inside element 'IFileWriter'", reader);
                                }
                            default:
                                throw new System.Configuration.ConfigurationErrorsException("Unknown element inside 'IFileWriter': " + reader.Name, reader);
                        }
            		}
            	}
            	while (reader.NodeType != System.Xml.XmlNodeType.EndElement || reader.Name != initialName);
            
                reader.ReadEndElement();
            }
            
            HashSet<string> restElems = new HashSet<string>();
            restElems.RemoveWhere(o => parsedElements.Contains(o));
            if (restElems.Count > 0)
                throw new System.Configuration.ConfigurationErrorsException("Not all required properties readed: " + string.Join(", ",restElems));
            return res;
        }

        private IFileWriter DeserializeIFileWriterElem(System.Xml.XmlReader reader, string expectedName)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Expected Element node type", reader);
        
        	if (expectedName != null && reader.Name != expectedName)
        		throw new System.Configuration.ConfigurationErrorsException("Unexpected element name for type 'IFileWriter': " + reader.Name, reader);
            
        	return DeserializeIFileWriterElem(reader);
        }

        private IFileWriter DeserializeIFileWriterElemWithInh(System.Xml.XmlReader reader)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Not an Element node type", reader);
        
        	switch (reader.Name)
        	{
                case "fileWriter":
                    return DeserializeIFileWriterElem(reader);
                default:
                    throw new System.Configuration.ConfigurationErrorsException("Unknown child type name: '" + reader.Name + "' for base type 'IFileWriter'", reader);
            }
        }


        private IDatabaseWriter DeserializeIDatabaseWriterElem(System.Xml.XmlReader reader)
        {
        	var res = new DatabaseWriterImplement();
        
        	HashSet<string> parsedElements = new HashSet<string>();
        
            string attribGenTempVal = null;
            attribGenTempVal = reader.GetAttribute("connectionString");
            if (attribGenTempVal != null)
                res.SetConnectionStringVal(Parse<String>(attribGenTempVal));
            else
                throw new System.Configuration.ConfigurationErrorsException("Attribute 'connectionString for element 'IDatabaseWriter' not defined", reader);

            attribGenTempVal = reader.GetAttribute("storedProcedureName");
            if (attribGenTempVal != null)
                res.SetStoredProcedureNameVal(Parse<String>(attribGenTempVal));

            attribGenTempVal = reader.GetAttribute("logLevel");
            if (attribGenTempVal != null)
                res.SetLogLevelVal(Parse<CfgLogLevel>(attribGenTempVal));


            if (reader.IsEmptyElement)
            {
            	reader.Skip();
            }
            else
            {
            	string initialName = reader.Name;
                reader.ReadStartElement();
            	do
                {
            		if (reader.NodeType != System.Xml.XmlNodeType.Element)
                    {
            			reader.Skip();
            		}
            		else
            		{			
                        switch (reader.Name)
                        {
                            case "add":
                                string addKey = reader.GetAttribute("key");
                                if (addKey == null)
                                	throw new System.Configuration.ConfigurationErrorsException("Key not found for 'add' inside element 'IDatabaseWriter'", reader);	
                                
                                switch (addKey)
                                {
                                	default:
                                		throw new System.Configuration.ConfigurationErrorsException("Unknown key " + addKey + " inside element 'IDatabaseWriter'", reader);
                                }
                            default:
                                throw new System.Configuration.ConfigurationErrorsException("Unknown element inside 'IDatabaseWriter': " + reader.Name, reader);
                        }
            		}
            	}
            	while (reader.NodeType != System.Xml.XmlNodeType.EndElement || reader.Name != initialName);
            
                reader.ReadEndElement();
            }
            
            HashSet<string> restElems = new HashSet<string>();
            restElems.RemoveWhere(o => parsedElements.Contains(o));
            if (restElems.Count > 0)
                throw new System.Configuration.ConfigurationErrorsException("Not all required properties readed: " + string.Join(", ",restElems));
            return res;
        }

        private IDatabaseWriter DeserializeIDatabaseWriterElem(System.Xml.XmlReader reader, string expectedName)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Expected Element node type", reader);
        
        	if (expectedName != null && reader.Name != expectedName)
        		throw new System.Configuration.ConfigurationErrorsException("Unexpected element name for type 'IDatabaseWriter': " + reader.Name, reader);
            
        	return DeserializeIDatabaseWriterElem(reader);
        }

        private IDatabaseWriter DeserializeIDatabaseWriterElemWithInh(System.Xml.XmlReader reader)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Not an Element node type", reader);
        
        	switch (reader.Name)
        	{
                case "databaseWriter":
                    return DeserializeIDatabaseWriterElem(reader);
                default:
                    throw new System.Configuration.ConfigurationErrorsException("Unknown child type name: '" + reader.Name + "' for base type 'IDatabaseWriter'", reader);
            }
        }


        private IPipeWriter DeserializeIPipeWriterElem(System.Xml.XmlReader reader)
        {
        	var res = new PipeWriterImplement();
        
        	HashSet<string> parsedElements = new HashSet<string>();
        
            string attribGenTempVal = null;
            attribGenTempVal = reader.GetAttribute("serverName");
            if (attribGenTempVal != null)
                res.SetServerNameVal(Parse<String>(attribGenTempVal));

            attribGenTempVal = reader.GetAttribute("pipeName");
            if (attribGenTempVal != null)
                res.SetPipeNameVal(Parse<String>(attribGenTempVal));

            attribGenTempVal = reader.GetAttribute("logLevel");
            if (attribGenTempVal != null)
                res.SetLogLevelVal(Parse<CfgLogLevel>(attribGenTempVal));


            if (reader.IsEmptyElement)
            {
            	reader.Skip();
            }
            else
            {
            	string initialName = reader.Name;
                reader.ReadStartElement();
            	do
                {
            		if (reader.NodeType != System.Xml.XmlNodeType.Element)
                    {
            			reader.Skip();
            		}
            		else
            		{			
                        switch (reader.Name)
                        {
                            case "add":
                                string addKey = reader.GetAttribute("key");
                                if (addKey == null)
                                	throw new System.Configuration.ConfigurationErrorsException("Key not found for 'add' inside element 'IPipeWriter'", reader);	
                                
                                switch (addKey)
                                {
                                	default:
                                		throw new System.Configuration.ConfigurationErrorsException("Unknown key " + addKey + " inside element 'IPipeWriter'", reader);
                                }
                            default:
                                throw new System.Configuration.ConfigurationErrorsException("Unknown element inside 'IPipeWriter': " + reader.Name, reader);
                        }
            		}
            	}
            	while (reader.NodeType != System.Xml.XmlNodeType.EndElement || reader.Name != initialName);
            
                reader.ReadEndElement();
            }
            
            HashSet<string> restElems = new HashSet<string>();
            restElems.RemoveWhere(o => parsedElements.Contains(o));
            if (restElems.Count > 0)
                throw new System.Configuration.ConfigurationErrorsException("Not all required properties readed: " + string.Join(", ",restElems));
            return res;
        }

        private IPipeWriter DeserializeIPipeWriterElem(System.Xml.XmlReader reader, string expectedName)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Expected Element node type", reader);
        
        	if (expectedName != null && reader.Name != expectedName)
        		throw new System.Configuration.ConfigurationErrorsException("Unexpected element name for type 'IPipeWriter': " + reader.Name, reader);
            
        	return DeserializeIPipeWriterElem(reader);
        }

        private IPipeWriter DeserializeIPipeWriterElemWithInh(System.Xml.XmlReader reader)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Not an Element node type", reader);
        
        	switch (reader.Name)
        	{
                case "pipeWriter":
                    return DeserializeIPipeWriterElem(reader);
                default:
                    throw new System.Configuration.ConfigurationErrorsException("Unknown child type name: '" + reader.Name + "' for base type 'IPipeWriter'", reader);
            }
        }


        private IRoutingWrapper DeserializeIRoutingWrapperElem(System.Xml.XmlReader reader)
        {
        	var res = new RoutingWrapperImplement();
        
        	HashSet<string> parsedElements = new HashSet<string>();
        

            if (reader.IsEmptyElement)
            {
            	reader.Skip();
            }
            else
            {
            	string initialName = reader.Name;
                reader.ReadStartElement();
            	do
                {
            		if (reader.NodeType != System.Xml.XmlNodeType.Element)
                    {
            			reader.Skip();
            		}
            		else
            		{			
                        switch (reader.Name)
                        {
                            case "add":
                                string addKey = reader.GetAttribute("key");
                                if (addKey == null)
                                	throw new System.Configuration.ConfigurationErrorsException("Key not found for 'add' inside element 'IRoutingWrapper'", reader);	
                                
                                switch (addKey)
                                {
                                	default:
                                		throw new System.Configuration.ConfigurationErrorsException("Unknown key " + addKey + " inside element 'IRoutingWrapper'", reader);
                                }
                            case "routingBySystem":
                                var tmp_RoutingBySystem = DeserializeDictionary<String, ILoggerWriterWrapperConfiguration>(reader, DeserializeILoggerWriterWrapperConfigurationElemWithInh, null);
                                res.SetRoutingBySystemVal(tmp_RoutingBySystem);
                                parsedElements.Add("routingBySystem");
                                break;
                            case "fromAll":
                                var tmp_FromAll = DeserializeList(reader, DeserializeILoggerWriterWrapperConfigurationElemWithInh, null);
                                res.SetFromAllVal(tmp_FromAll);
                                parsedElements.Add("fromAll");
                                break;
                            case "fromOthers":
                                var tmp_FromOthers = DeserializeList(reader, DeserializeILoggerWriterWrapperConfigurationElemWithInh, null);
                                res.SetFromOthersVal(tmp_FromOthers);
                                parsedElements.Add("fromOthers");
                                break;
                            default:
                                throw new System.Configuration.ConfigurationErrorsException("Unknown element inside 'IRoutingWrapper': " + reader.Name, reader);
                        }
            		}
            	}
            	while (reader.NodeType != System.Xml.XmlNodeType.EndElement || reader.Name != initialName);
            
                reader.ReadEndElement();
            }
            
            HashSet<string> restElems = new HashSet<string>();
            restElems.Add("routingBySystem");
            restElems.Add("fromAll");
            restElems.Add("fromOthers");
            restElems.RemoveWhere(o => parsedElements.Contains(o));
            if (restElems.Count > 0)
                throw new System.Configuration.ConfigurationErrorsException("Not all required properties readed: " + string.Join(", ",restElems));
            return res;
        }

        private IRoutingWrapper DeserializeIRoutingWrapperElem(System.Xml.XmlReader reader, string expectedName)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Expected Element node type", reader);
        
        	if (expectedName != null && reader.Name != expectedName)
        		throw new System.Configuration.ConfigurationErrorsException("Unexpected element name for type 'IRoutingWrapper': " + reader.Name, reader);
            
        	return DeserializeIRoutingWrapperElem(reader);
        }

        private IRoutingWrapper DeserializeIRoutingWrapperElemWithInh(System.Xml.XmlReader reader)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Not an Element node type", reader);
        
        	switch (reader.Name)
        	{
                case "routingWrapper":
                    return DeserializeIRoutingWrapperElem(reader);
                default:
                    throw new System.Configuration.ConfigurationErrorsException("Unknown child type name: '" + reader.Name + "' for base type 'IRoutingWrapper'", reader);
            }
        }


        private IPatternMatchingMatch DeserializeIPatternMatchingMatchElem(System.Xml.XmlReader reader)
        {
        	var res = new PatternMatchingMatchImplement();
        
        	HashSet<string> parsedElements = new HashSet<string>();
        
            string attribGenTempVal = null;
            attribGenTempVal = reader.GetAttribute("value");
            if (attribGenTempVal != null)
                res.SetValueVal(Parse<String>(attribGenTempVal));
            else
                throw new System.Configuration.ConfigurationErrorsException("Attribute 'value for element 'IPatternMatchingMatch' not defined", reader);


            if (reader.IsEmptyElement)
            {
            	reader.Skip();
            }
            else
            {
            	string initialName = reader.Name;
            	do
                {
            		if (reader.NodeType != System.Xml.XmlNodeType.Element)
                    {
            			reader.Skip();
            		}
            		else
            		{			
                        reader.Read();
                        if (reader.MoveToContent() != System.Xml.XmlNodeType.Element)
                            throw new System.Configuration.ConfigurationErrorsException("Injected element not found for property 'writer' inside 'IPatternMatchingMatch", reader);
                        res.SetWriterVal(DeserializeILoggerWriterWrapperConfigurationElemWithInh(reader));
                        if (reader.MoveToContent() != System.Xml.XmlNodeType.EndElement)
                            throw new System.Configuration.ConfigurationErrorsException("Injected element 'writer' inside 'IPatternMatchingMatch can't contain several subelements", reader);
                        reader.ReadEndElement();
                        parsedElements.Add("writer");
                        break;
            		}
            	}
            	while (reader.NodeType != System.Xml.XmlNodeType.EndElement || reader.Name != initialName);
            
            }
            
            HashSet<string> restElems = new HashSet<string>();
            restElems.Add("writer");
            restElems.RemoveWhere(o => parsedElements.Contains(o));
            if (restElems.Count > 0)
                throw new System.Configuration.ConfigurationErrorsException("Not all required properties readed: " + string.Join(", ",restElems));
            return res;
        }

        private IPatternMatchingMatch DeserializeIPatternMatchingMatchElem(System.Xml.XmlReader reader, string expectedName)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Expected Element node type", reader);
        
        	if (expectedName != null && reader.Name != expectedName)
        		throw new System.Configuration.ConfigurationErrorsException("Unexpected element name for type 'IPatternMatchingMatch': " + reader.Name, reader);
            
        	return DeserializeIPatternMatchingMatchElem(reader);
        }

        private IPatternMatchingMatch DeserializeIPatternMatchingMatchElemWithInh(System.Xml.XmlReader reader)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Not an Element node type", reader);
        
        	switch (reader.Name)
        	{
                case "patternMatchingMatch":
                    return DeserializeIPatternMatchingMatchElem(reader);
                default:
                    throw new System.Configuration.ConfigurationErrorsException("Unknown child type name: '" + reader.Name + "' for base type 'IPatternMatchingMatch'", reader);
            }
        }


        private IPatternMatchingDefault DeserializeIPatternMatchingDefaultElem(System.Xml.XmlReader reader)
        {
        	var res = new PatternMatchingDefaultImplement();
        
        	HashSet<string> parsedElements = new HashSet<string>();
        

            if (reader.IsEmptyElement)
            {
            	reader.Skip();
            }
            else
            {
            	string initialName = reader.Name;
            	do
                {
            		if (reader.NodeType != System.Xml.XmlNodeType.Element)
                    {
            			reader.Skip();
            		}
            		else
            		{			
                        reader.Read();
                        if (reader.MoveToContent() != System.Xml.XmlNodeType.Element)
                            throw new System.Configuration.ConfigurationErrorsException("Injected element not found for property 'writer' inside 'IPatternMatchingDefault", reader);
                        res.SetWriterVal(DeserializeILoggerWriterWrapperConfigurationElemWithInh(reader));
                        if (reader.MoveToContent() != System.Xml.XmlNodeType.EndElement)
                            throw new System.Configuration.ConfigurationErrorsException("Injected element 'writer' inside 'IPatternMatchingDefault can't contain several subelements", reader);
                        reader.ReadEndElement();
                        parsedElements.Add("writer");
                        break;
            		}
            	}
            	while (reader.NodeType != System.Xml.XmlNodeType.EndElement || reader.Name != initialName);
            
            }
            
            HashSet<string> restElems = new HashSet<string>();
            restElems.Add("writer");
            restElems.RemoveWhere(o => parsedElements.Contains(o));
            if (restElems.Count > 0)
                throw new System.Configuration.ConfigurationErrorsException("Not all required properties readed: " + string.Join(", ",restElems));
            return res;
        }

        private IPatternMatchingDefault DeserializeIPatternMatchingDefaultElem(System.Xml.XmlReader reader, string expectedName)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Expected Element node type", reader);
        
        	if (expectedName != null && reader.Name != expectedName)
        		throw new System.Configuration.ConfigurationErrorsException("Unexpected element name for type 'IPatternMatchingDefault': " + reader.Name, reader);
            
        	return DeserializeIPatternMatchingDefaultElem(reader);
        }

        private IPatternMatchingDefault DeserializeIPatternMatchingDefaultElemWithInh(System.Xml.XmlReader reader)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Not an Element node type", reader);
        
        	switch (reader.Name)
        	{
                case "patternMatchingDefault":
                    return DeserializeIPatternMatchingDefaultElem(reader);
                default:
                    throw new System.Configuration.ConfigurationErrorsException("Unknown child type name: '" + reader.Name + "' for base type 'IPatternMatchingDefault'", reader);
            }
        }


    }


}
