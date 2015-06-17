using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qoollo.Logger.Configuration.LoggerConfigurationV2
{
    enum CfgLogLevel
    {
        TRACE,
        DEBUG,
        INFO,
        WARN,
        ERROR,
        FATAL
    }

    interface ILoggerConfigurationSection
    {
        IRootLoggerConfiguration Logger { get; }
    }



    interface IRootLoggerConfiguration
    {
        bool EnableStackTraceExtraction { get; }
        CfgLogLevel LogLevel { get; }
        bool IsEnabled { get; }
        ILoggerWriterWrapperConfiguration WriterWrapper { get; }
    }


    interface ILoggerWriterWrapperConfiguration
    {
    }


    interface IAsyncQueueWrapper : ILoggerWriterWrapperConfiguration
    {
        int MaxQueueSize { get; }
        bool IsDiscardExcess { get; }
        ILoggerWriterWrapperConfiguration Logger { get; }
    }

    interface IAsyncReliableQueueWrapper : ILoggerWriterWrapperConfiguration
    {
        int MaxQueueSize { get; }
        bool IsDiscardExcess { get; }
        string FolderForTemporaryStore { get; }
        long MaxFileSize { get; }
        ILoggerWriterWrapperConfiguration Logger { get; }
    }

    interface IReliableWrapper: ILoggerWriterWrapperConfiguration
    {
        string FolderForTemporaryStore { get; }
        long MaxFileSize { get; }
        ILoggerWriterWrapperConfiguration Logger { get; }
    }


    interface IGroupWrapper : ILoggerWriterWrapperConfiguration
    {
        List<ILoggerWriterWrapperConfiguration> Loggers { get; }
    }

    interface IRoutingWrapper : ILoggerWriterWrapperConfiguration
    {
        Dictionary<string, ILoggerWriterWrapperConfiguration> RoutingBySystem { get; }
        List<ILoggerWriterWrapperConfiguration> FromAll { get; }
        List<ILoggerWriterWrapperConfiguration> FromOthers { get; }
    }


    interface IPatternMatchingWrapper : ILoggerWriterWrapperConfiguration
    {
        string Pattern { get; }
        List<IPatternMatchingElement> Writers { get; }
    }

    interface IPatternMatchingElement
    {
        ILoggerWriterWrapperConfiguration Writer { get; }
    }

    interface IPatternMatchingDefault: IPatternMatchingElement
    {

    }

    interface IPatternMatchingMatch : IPatternMatchingElement
    {
        string Value { get; }
    }



    interface ILoggerWriterConfiguration : ILoggerWriterWrapperConfiguration
    {
        CfgLogLevel LogLevel { get; }
    }

    interface IEmptyWriter : ILoggerWriterConfiguration
    {
    }


    interface IConsoleWriter : ILoggerWriterConfiguration
    {
        string Template { get; }
    }

    interface IFileWriter : ILoggerWriterConfiguration
    {
        string Template { get; }
        string FileNameTemplate { get; }
        bool NeedFileRotation { get; }
        string Encoding { get; }
    }

    interface IPipeWriter : ILoggerWriterConfiguration
    {
        string ServerName { get; }
        string PipeName { get; }
    }

    interface INetworkWriter : ILoggerWriterConfiguration
    {
        string ServerAddress { get; }
        int Port { get; }        
    }

    interface ILogstashWriter : ILoggerWriterConfiguration
    {
        string ServerAddress { get; }
        int Port { get; }
    }

    interface IDatabaseWriter : ILoggerWriterConfiguration
    {
        string ConnectionString { get; }
        string StoredProcedureName { get; }
    }

    interface ICustomWriter: ILoggerWriterConfiguration
    {
        string Type { get; }
        Dictionary<string, string> Parameters { get; }
    }
}
