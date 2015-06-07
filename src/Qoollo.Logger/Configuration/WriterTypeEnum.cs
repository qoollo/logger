using System.Collections.Generic;

namespace Qoollo.Logger.Configuration
{
    /// <summary>
    /// Supported writer/wrapper types
    /// </summary>
    public enum WriterTypeEnum
    {
        /// <summary>
        /// Empty writer (sepcial writer that do not perform any actions)
        /// </summary>
        EmptyWriter,


        /// <summary>
        /// Asynchronous wrapper with queue
        /// </summary>
        AsyncQueueWrapper,

        /// <summary>
        /// Asynchronous wrapper with reliable queue. 
        /// If writer currently can't write a message, this wrapper save log message to the file on the disk. 
        /// Later it read logs and try to send them to writer again.
        /// </summary>
        AsyncQueueWithReliableSendingWrapper,

        /// <summary>
        /// Writer for Console
        /// </summary>
        ConsoleWriter,

        /// <summary>
        /// Writer for text file
        /// </summary>
        FileWriter,

        /// <summary>
        /// Writer for Database (MS SQL Server)
        /// </summary>
        DBWriter,

        /// <summary>
        /// Writer that sends logs to server through Pipe (WCF)
        /// </summary>
        PipeWriter,

        /// <summary>
        /// Writer that sends logs to server through Network (WCF)
        /// </summary>
        NetWriter,

        /// <summary>
        /// Wrapper that aggregate several other writers/wrappers
        /// </summary>
        GroupWrapper,

        /// <summary>
        /// Wrapper to route messages in per Module manner
        /// </summary>
        RoutingWrapper,

        /// <summary>
        /// Simple pattern-matching router for log messages
        /// </summary>
        PatternMatchingWrapper,

        /// <summary>
        /// Reliable wrapper (stores messages on the disk if they temporary can't be written by inner writer)
        /// </summary>
        ReliableWrapper,



        /// <summary>
        /// Custom user wrapper configuration
        /// </summary>
        CustomWrapper,

        /// <summary>
        /// Custom user writer configuration
        /// </summary>
        CustomWriter,

        /// <summary>
        /// Custom tcp writer with user format
        /// </summary>
        LogstashWriter,
    }
}