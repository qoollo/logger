using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qoollo.Logger.Configuration
{
    public enum CfgLogLevel
    {
        TRACE,
        DEBUG,
        INFO,
        WARN,
        ERROR,
        FATAL
    }

    public interface ILoggerConfigurationSection
    {
        IRootLoggerConfiguration Logger { get; }
    }



    public interface IRootLoggerConfiguration
    {
        bool EnableStackTraceExtraction { get; }
        CfgLogLevel LogLevel { get; }
        bool IsEnabled { get; }
        ILoggerWriterWrapperConfiguration WriterWrapper { get; }
    }


    public interface ILoggerWriterWrapperConfiguration
    {
    }


    public interface IAsyncQueueWrapper : ILoggerWriterWrapperConfiguration
    {
        int MaxQueueSize { get; }
        bool IsDiscardExcess { get; }
        ILoggerWriterWrapperConfiguration Logger { get; }
    }

    public interface IAsyncReliableQueueWrapper : ILoggerWriterWrapperConfiguration
    {
        int MaxQueueSize { get; }
        bool IsDiscardExcess { get; }
        string FolderForTemporaryStore { get; }
        long MaxFileSize { get; }
        ILoggerWriterWrapperConfiguration Logger { get; }
    }

    public interface IReliableWrapper : ILoggerWriterWrapperConfiguration
    {
        string FolderForTemporaryStore { get; }
        long MaxFileSize { get; }
        ILoggerWriterWrapperConfiguration Logger { get; }
    }


    public interface IGroupWrapper : ILoggerWriterWrapperConfiguration
    {
        List<ILoggerWriterWrapperConfiguration> Loggers { get; }
    }

    public interface IRoutingWrapper : ILoggerWriterWrapperConfiguration
    {
        Dictionary<string, ILoggerWriterWrapperConfiguration> RoutingBySystem { get; }
        List<ILoggerWriterWrapperConfiguration> FromAll { get; }
        List<ILoggerWriterWrapperConfiguration> FromOthers { get; }
    }

    public interface IPatternMatchingWrapper : ILoggerWriterWrapperConfiguration
    {
        string Pattern { get; }
        List<IPatternMatchingElement> Writers { get; }
    }

    public interface IPatternMatchingElement
    {
        ILoggerWriterWrapperConfiguration Writer { get; }
    }

    public interface IPatternMatchingDefault : IPatternMatchingElement
    {

    }

    public interface IPatternMatchingMatch : IPatternMatchingElement
    {
        string Value { get; }
    }


    public interface ILoggerWriterConfiguration : ILoggerWriterWrapperConfiguration
    {
        CfgLogLevel LogLevel { get; }
    }

    public interface IEmptyWriter : ILoggerWriterConfiguration
    {
    }


    public interface IConsoleWriter : ILoggerWriterConfiguration
    {
        string Template { get; }
    }

    public interface IFileWriter : ILoggerWriterConfiguration
    {
        string Template { get; }
        string FileNameTemplate { get; }
        bool NeedFileRotation { get; }
        string Encoding { get; }
    }

    public interface IPipeWriter : ILoggerWriterConfiguration
    {
        string ServerName { get; }
        string PipeName { get; }
    }

    public interface INetworkWriter : ILoggerWriterConfiguration
    {
        string ServerAddress { get; }
        int Port { get; }
    }

    public interface IDatabaseWriter : ILoggerWriterConfiguration
    {
        string ConnectionString { get; }
        string StoredProcedureName { get; }
    }


    public interface ICustomWriter : ILoggerWriterConfiguration
    {
        string Type { get; }
        Dictionary<string, string> Parameters { get; }
    }
}
