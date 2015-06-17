using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Qoollo.Logger.Common
{
    /// <summary>
    /// Log message data container (contains all information about the message)
    /// </summary>
    [DataContract]
    public class LoggingEvent
    {
        /// <summary>
        /// Constructor without parameters
        /// </summary>
        protected internal LoggingEvent()
        {
        }

        /// <summary>
        /// LoggingEvent constructor
        /// </summary>
        /// <param name="date">Date and Time when log event created</param>
        /// <param name="message">User message</param>
        /// <param name="exception">Exception</param>
        /// <param name="level">Level of this message</param>
        /// <param name="context">Context</param>
        /// <param name="stackSources">Stack source</param>
        /// <param name="machineName">Source Machine name</param>
        /// <param name="processName">Source Process name</param>
        /// <param name="processId">Source Process Id</param>
        /// <param name="assembly">Source Assembly</param>
        /// <param name="namespace">Source namespace</param>
        /// <param name="class">Source Class name</param>
        /// <param name="method">Source Method name</param>
        /// <param name="filePath">Source code file name</param>
        /// <param name="lineNumber">Line number in source code</param>
        /// <param name="machineIpAddress">Ip4 address of the source machine</param>
        public LoggingEvent(DateTime date, string message, Exception exception, LogLevel level, string context,
                            List<string> stackSources, string machineName = null, string machineIpAddress = null, string processName = null, int processId = 0, string assembly = null, string @namespace = null, string @class = null, string method = null,
                            string filePath = null, int lineNumber = -1)
        {
            Date = date;
            Message = message;
            Level = level;
            Context = context;
            StackSources = stackSources;
            Clazz = @class;
            Method = method;
            FilePath = filePath;
            LineNumber = lineNumber;
            Assembly = assembly;
            Namespace = @namespace;
            MachineName = machineName;
            ProcessName = processName;
            ProcessId = processId;
            MachineIpAddress = machineIpAddress;

            if (exception != null)
                Exception = new Error(exception);
        }

        /// <summary>
        /// LoggingEvent constructor
        /// </summary>
        /// <param name="message">User message</param>
        /// <param name="exception">Exception</param>
        /// <param name="level">Level of this message</param>
        /// <param name="context">Context</param>
        /// <param name="stackSources">Stack source</param>
        /// <param name="machineName">Source Machine name</param>
        /// <param name="processName">Source Process name</param>
        /// <param name="processId">Source Process Id</param>
        /// <param name="assembly">Source Assembly</param>
        /// <param name="namespace">Source namespace</param>
        /// <param name="class">Source Class name</param>
        /// <param name="method">Source Method name</param>
        /// <param name="filePath">Source code file name</param>
        /// <param name="lineNumber">Line number in source code</param>
        /// <param name="machineIpAddress">Ip4 address of the source machine</param>
        public LoggingEvent(string message, Exception exception, LogLevel level, string context,
                            List<string> stackSources, string machineName = null, string machineIpAddress = null, string processName = null, int processId = 0, string assembly = null, string @namespace = null, string @class = null, string method = null, string filePath = null,
                            int lineNumber = -1)
        {
            Date = DateTime.Now;
            Message = message;
            Level = level;
            Context = context;
            StackSources = stackSources;
            Clazz = @class;
            Method = method;
            FilePath = filePath;
            LineNumber = lineNumber;
            Assembly = assembly;
            Namespace = @namespace;
            MachineName = machineName;
            ProcessName = processName;
            ProcessId = processId;
            MachineIpAddress = machineIpAddress;

            if (exception != null)
                Exception = new Error(exception);
        }


        #region properties
        
        /// <summary>
        /// Date and Time when log event created
        /// </summary>
        [DataMember(Order = 1)]
        public DateTime Date { get; private set; }

        /// <summary>
        /// Level of this message
        /// </summary>
        [DataMember(Order = 2)]
        public LogLevel Level { get; private set; }

        /// <summary>
        /// Log message context.
        /// Additional parameter that can be used to filter log messages.
        /// </summary>
        [DataMember(Order = 3)]
        public string Context { get; private set; }

        /// <summary>
        /// Source Class name
        /// </summary>
        [DataMember(Order = 4)]
        public string Clazz { get; private set; }

        /// <summary>
        /// Source Method name
        /// </summary>
        [DataMember(Order = 5)]
        public string Method { get; private set; }

        /// <summary>
        /// Source code file name
        /// </summary>
        [DataMember(Order = 6)]
        public string FilePath { get; private set; }

        /// <summary>
        /// Line number in source code
        /// </summary>
        [DataMember(Order = 7)]
        public int LineNumber { get; private set; }

        /// <summary>
        /// User message
        /// </summary>
        [DataMember(Order = 8)]
        public string Message { get; private set; }

        /// <summary>
        /// Exception
        /// </summary>
        [DataMember(Order = 9)]
        public Error Exception { get; private set; }

        /// <summary>
        /// StackSource: chain of modules names. First element - inner module, last - outer.
        /// </summary>
        [DataMember(Order = 10)]
        public List<string> StackSources { get; private set; }

        /// <summary>
        /// Source Namespace
        /// </summary>
        [DataMember(Order = 11)]
        public string Namespace { get; private set; }

        /// <summary>
        /// Source Assembly name
        /// </summary>
        [DataMember(Order = 12)]
        public string Assembly { get; private set; }

        /// <summary>
        /// Source Machine name
        /// </summary>
        [DataMember(Order = 13)]
        public string MachineName { get; private set; }

        /// <summary>
        /// First IP4 address of source machine
        /// </summary>
        [DataMember(Order = 16)]
        public string MachineIpAddress { get; private set; }

        /// <summary>
        /// Source Process name
        /// </summary>
        [DataMember(Order = 14)]
        public string ProcessName { get; private set; }

        /// <summary>
        /// Source Process Id
        /// </summary>
        [DataMember(Order = 15)]
        public int ProcessId { get; private set; }

        #endregion
    }
}