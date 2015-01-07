using System;
using System.Diagnostics.Contracts;
using System.Runtime.Serialization;

namespace Qoollo.Logger
{
    /// <summary>
    /// Severity level of log message
    /// </summary>
    [DataContract]
    public sealed class LogLevel : IComparable, IComparable<LogLevel>, IEquatable<LogLevel>
    {
        #region constants

        private const string TraceValueConst = "TRACE";
        private const string DebugValueConst = "DEBUG";
        private const string InfoValueConst = "INFO";
        private const string WarnValueConst = "WARN";
        private const string ErrorValueConst = "ERROR";
        private const string FatalValueConst = "FATAL";

        private const int TraceLevelConst = 0;
        private const int DebugLevelConst = 1;
        private const int InfoLevelConst = 2;
        private const int WarnLevelConst = 3;
        private const int ErrorLevelConst = 4;
        private const int FatalLevelConst = 5;


        /// <summary>
        /// Trace level (0 - minimal severity)
        /// </summary>
        public static readonly LogLevel Trace = new LogLevel(TraceValueConst, TraceLevelConst);

        /// <summary>
        /// Debug level (1)
        /// </summary>
        public static readonly LogLevel Debug = new LogLevel(DebugValueConst, DebugLevelConst);

        /// <summary>
        /// Info level (2)
        /// </summary>
        public static readonly LogLevel Info = new LogLevel(InfoValueConst, InfoLevelConst);

        /// <summary>
        /// Warn level (3)
        /// </summary>
        public static readonly LogLevel Warn = new LogLevel(WarnValueConst, WarnLevelConst);

        /// <summary>
        /// Error level (4)
        /// </summary>
        public static readonly LogLevel Error = new LogLevel(ErrorValueConst, ErrorLevelConst);

        /// <summary>
        /// Fatal level (5 - maximal severity)
        /// </summary>
        public static readonly LogLevel Fatal = new LogLevel(FatalValueConst, FatalLevelConst);


        /// <summary>
        /// Constant for loggers to process all messages' levels (equal to Trace)
        /// </summary>
        public static LogLevel FullLog { get { return Trace; } }

        #endregion

        #region constructors

        /// <summary>
        /// Constructor without parameters
        /// </summary>
        internal LogLevel()
        {
            Level = TraceLevelConst;
        }
        /// <summary>
        /// Private constructor for concrete level
        /// </summary>
        /// <param name="value">Description</param>
        /// <param name="level">Level value</param>
        private LogLevel(string value, int level)
        {
            Name = value;
            Level = level;
        }

        /// <summary>
        /// Convert string representation of LogLevel to the object of LogLevel class
        /// </summary>
        /// <param name="value">String representation</param>
        /// <returns>LogLevel</returns>
        public static LogLevel Parse(string value)
        {
            Contract.Requires(value != null);

            if (value == null)
                throw new ArgumentNullException("value");

            switch (value.ToUpper())
            {
                case TraceValueConst:
                    return Trace;
                case DebugValueConst:
                    return Debug;
                case InfoValueConst:
                    return Info;
                case WarnValueConst:
                    return Warn;
                case ErrorValueConst:
                    return Error;
                case FatalValueConst:
                    return Fatal;
                default:
                    throw new ArgumentException("Unknown log level string representation: " + value.ToString());
            }
        }

        /// <summary>
        /// Convert integer number representation of LogLevel to object of LogLevel class
        /// </summary>
        /// <param name="value">Number (0 - 5)</param>
        /// <returns>LogLevel</returns>
        public static LogLevel Parse(int value)
        {
            switch (value)
            {
                case TraceLevelConst:
                    return Trace;
                case DebugLevelConst:
                    return Debug;
                case InfoLevelConst:
                    return Info;
                case WarnLevelConst:
                    return Warn;
                case ErrorLevelConst:
                    return Error;
                case FatalLevelConst:
                    return Fatal;
                default:
                    throw new ArgumentException("Unknown log level number representation: " + value.ToString());
            }
        }

        #endregion

        #region properties

        /// <summary>
        /// LogLevel name (TRACE, DEBUG, ..., FATAL)
        /// </summary>
        public string Name { get; private set; }

        private int _level;

        /// <summary>
        /// LogLevel number representation (0, 1, ..., 5)
        /// </summary>
        [DataMember(Order = 1)]
        public int Level
        {
            get { return _level; }
            private set
            {
                _level = value;
                switch (_level)
                {
                    case 0:
                        Name = TraceValueConst;
                        break;

                    case 1:
                        Name = DebugValueConst;
                        break;

                    case 2:
                        Name = InfoValueConst;
                        break;

                    case 3:
                        Name = WarnValueConst;
                        break;

                    case 4:
                        Name = ErrorValueConst;
                        break;

                    case 5:
                        Name = FatalValueConst;
                        break;

                    default:
                        throw new ArgumentException(string.Format(
                            "Unknown log level \"{0}\".", _level));
                }
            }
        }

        #region IsTraceEnabled,..., IsFatalEnabled

        /// <summary>
        /// Is messages with 'Trace' level will be process by logger configurated with this LogLevel
        /// </summary>
        public bool IsTraceEnabled
        {
            get { return Level.CompareTo(TraceLevelConst) <= 0; }
        }

        /// <summary>
        /// Is messages with 'Debug' level will be process by logger configurated with this LogLevel
        /// </summary>
        public bool IsDebugEnabled
        {
            get { return Level.CompareTo(DebugLevelConst) <= 0; }
        }

        /// <summary>
        /// Is messages with 'Info' level will be process by logger configurated with this LogLevel
        /// </summary>
        public bool IsInfoEnabled
        {
            get { return Level.CompareTo(InfoLevelConst) <= 0; }
        }

        /// <summary>
        /// Is messages with 'Warn' level will be process by logger configurated with this LogLevel
        /// </summary>
        public bool IsWarnEnabled
        {
            get { return Level.CompareTo(WarnLevelConst) <= 0; }
        }

        /// <summary>
        /// Is messages with 'Error' level will be process by logger configurated with this LogLevel
        /// </summary>
        public bool IsErrorEnabled
        {
            get { return Level.CompareTo(ErrorLevelConst) <= 0; }
        }

        /// <summary>
        /// Is messages with 'Fatal' level will be process by logger configurated with this LogLevel
        /// </summary>
        public bool IsFatalEnabled
        {
            get { return Level.CompareTo(FatalLevelConst) <= 0; }
        }

        /// <summary>
        /// Is messages with passed 'level' will be process by logger configurated with this LogLevel
        /// </summary>
        /// <param name="level">Checking log level</param>
        /// <returns>Is enabled</returns>
        public bool IsEnabled(LogLevel level)
        {
            Contract.Requires(level != null);

            return level.Level - Level >= 0;
        }

        #endregion

        #endregion

        #region implements interfaces

        #region IComparable Members

        /// <summary>
        /// Compares this LogLevel with log level passed as parameter
        /// </summary>
        /// <param name="obj">LogLevel object</param>
        /// <returns>Comparison result</returns>
        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            var logLevel = obj as LogLevel;
            if (object.ReferenceEquals(logLevel, null))
                throw new ArgumentException("Object is not a LogLevel");

            return this.Level - logLevel.Level;
        }

        #endregion

        #region IComparable<LogLevel> Members

        /// <summary>
        /// Compares this LogLevel with log level passed as parameter
        /// </summary>
        /// <param name="other">LogLevel object</param>
        /// <returns>Comparison result</returns>
        public int CompareTo(LogLevel other)
        {
            if (ReferenceEquals(other, null))
                return 1;

            return this.Level - other.Level;
        }

        #endregion

        #region IEquatable<LogLevel> Members

        /// <summary>
        /// Equality comparsion of LogLevels
        /// </summary>
        /// <param name="other">LogLevel to compare with</param>
        /// <returns>Is equals</returns>
        public bool Equals(LogLevel other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return this.Level == other.Level;
        }

        #endregion

        #region Equality members

        /// <summary>
        /// Returns the hash code of LogLevel object
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            return Level;
        }

        /// <summary>
        /// Equality comparison operator for LogLevel
        /// </summary>
        /// <param name="left">Left</param>
        /// <param name="right">Right</param>
        /// <returns>Is equals</returns>
        public static bool operator ==(LogLevel left, LogLevel right)
        {
            if (object.ReferenceEquals(left, right))
                return true;

            if (object.ReferenceEquals(left, null) || object.ReferenceEquals(right, null))
                return false;

            return left.Level == right.Level;
        }

        /// <summary>
        /// Inequality comparsion operator for LogLevel
        /// </summary>
        /// <param name="left">Left</param>
        /// <param name="right">Right</param>
        /// <returns>Is not equal</returns>
        public static bool operator !=(LogLevel left, LogLevel right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Greater operator for LogLevel
        /// </summary>
        /// <param name="left">Left</param>
        /// <param name="right">Right</param>
        /// <returns>Is Left greater than Right</returns>
        public static bool operator >(LogLevel left, LogLevel right)
        {
            Contract.Requires<ArgumentNullException>(left != null, "left is null");
            Contract.Requires<ArgumentNullException>(right != null, "right is null");

            return left.Level > right.Level;
        }

        /// <summary>
        /// Greater or Equal comparison operator for LogLevel
        /// </summary>
        /// <param name="left">Left</param>
        /// <param name="right">Right</param>
        /// <returns>Is Left greater than Right</returns>
        public static bool operator >=(LogLevel left, LogLevel right)
        {
            Contract.Requires<ArgumentNullException>(left != null, "left is null");
            Contract.Requires<ArgumentNullException>(right != null, "right is null");

            return left.Level >= right.Level;
        }

        /// <summary>
        /// Less comparison operator for LogLevel
        /// </summary>
        /// <param name="left">Left</param>
        /// <param name="right">Right</param>
        /// <returns>Is Left greater than Right</returns>
        public static bool operator <(LogLevel left, LogLevel right)
        {
            Contract.Requires<ArgumentNullException>(left != null, "left is null");
            Contract.Requires<ArgumentNullException>(right != null, "right is null");

            return left.Level < right.Level;
        }

        /// <summary>
        /// Less or Equal comparison operator for LogLevel
        /// </summary>
        /// <param name="left">Left</param>
        /// <param name="right">Right</param>
        /// <returns>Is Left greater than Right</returns>
        public static bool operator <=(LogLevel left, LogLevel right)
        {
            Contract.Requires<ArgumentNullException>(left != null, "left is null");
            Contract.Requires<ArgumentNullException>(right != null, "right is null");

            return left.Level <= right.Level;
        }

        #endregion

        #endregion

        #region override: Equals(object obj), ToString()

        /// <summary>
        /// Is this LogLevel equals to passed as parameter
        /// </summary>
        /// <param name="obj">Other log level</param>
        /// <returns>Is equals</returns>
        public override bool Equals(object obj)
        {
            var logLevel = obj as LogLevel;

            if (object.ReferenceEquals(logLevel, null))
                return false;

            return Level == logLevel.Level;
        }

        /// <summary>
        /// Returns the string representation of this LogLevel
        /// </summary>
        /// <returns>String representation</returns>
        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}