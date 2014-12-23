using System;
using System.Diagnostics.Contracts;
using System.Runtime.Serialization;

namespace Qoollo.Logger
{
    /// <summary>
    /// Класс представляет собой уровень логирования и методы работы с ним
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
        /// Константа для уровня логгирования Trace
        /// </summary>
        public static readonly LogLevel Trace = new LogLevel(TraceValueConst, TraceLevelConst);

        /// <summary>
        /// Константа для уровня логгирования Debug
        /// </summary>
        public static readonly LogLevel Debug = new LogLevel(DebugValueConst, DebugLevelConst);

        /// <summary>
        /// Константа для уровня логгирования Info
        /// </summary>
        public static readonly LogLevel Info = new LogLevel(InfoValueConst, InfoLevelConst);

        /// <summary>
        /// Константа для уровня логгирования Warn
        /// </summary>
        public static readonly LogLevel Warn = new LogLevel(WarnValueConst, WarnLevelConst);

        /// <summary>
        /// Константа для уровня логгирования Error
        /// </summary>
        public static readonly LogLevel Error = new LogLevel(ErrorValueConst, ErrorLevelConst);

        /// <summary>
        /// Константа для уровня логгирования Fatal
        /// </summary>
        public static readonly LogLevel Fatal = new LogLevel(FatalValueConst, FatalLevelConst);


        /// <summary>
        /// Константа для логирования всего
        /// </summary>
        public static LogLevel FullLog { get { return Trace; } }

        #endregion

        #region constructors

        /// <summary>
        /// Конструктор по умолчанию нужен для десериализации
        /// </summary>
        internal LogLevel()
        {
            Level = TraceLevelConst;
        }

        private LogLevel(string value, int level)
        {
            Name = value;
            Level = level;
        }

        /// <summary>
        /// Преобразование строкового имени уровня логгирования в тип LogLevel
        /// </summary>
        /// <param name="value">Cтроковое имя уровня логгирования</param>
        /// <returns></returns>
        public static LogLevel Parse(string value)
        {
            Contract.Requires(value != null);

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
                    return null;
            }
        }

        /// <summary>
        /// Преобразование целочисленного значения уровня логгирования в тип LogLevel
        /// </summary>
        /// <param name="value">Число от 0 до 5</param>
        /// <returns></returns>
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
                    return null;
            }
        }

        #endregion

        #region properties

        /// <summary>
        /// Название уровня логирования (TRACE, DEBUG, ..., FATAL)
        /// </summary>
        public string Name { get; private set; }

        private int _level;

        /// <summary>
        /// Порядковый номер уровня логирования (0, 1, ..., 5)
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
                            "Не существует уровня логирования \"{0}\".", _level));
                }
            }
        }

        #region IsTraceEnabled,..., IsFatalEnabled

        /// <summary>
        /// Активен ли уровень логгирования Trace
        /// </summary>
        public bool IsTraceEnabled
        {
            get { return Level.CompareTo(TraceLevelConst) <= 0; }
        }

        /// <summary>
        /// Активен ли уровень логгирования Debug
        /// </summary>
        public bool IsDebugEnabled
        {
            get { return Level.CompareTo(DebugLevelConst) <= 0; }
        }

        /// <summary>
        /// Активен ли уровень логгирования Info
        /// </summary>
        public bool IsInfoEnabled
        {
            get { return Level.CompareTo(InfoLevelConst) <= 0; }
        }

        /// <summary>
        /// Активен ли уровень логгирования Warn
        /// </summary>
        public bool IsWarnEnabled
        {
            get { return Level.CompareTo(WarnLevelConst) <= 0; }
        }

        /// <summary>
        /// Активен ли уровень логгирования Error
        /// </summary>
        public bool IsErrorEnabled
        {
            get { return Level.CompareTo(ErrorLevelConst) <= 0; }
        }

        /// <summary>
        /// Активен ли уровень логгирования Fatal
        /// </summary>
        public bool IsFatalEnabled
        {
            get { return Level.CompareTo(FatalLevelConst) <= 0; }
        }

        /// <summary>
        /// Определяет активен ли уровень логгирования
        /// </summary>
        /// <param name="level">Уровень логгирования активность которого проверяется</param>
        /// <returns></returns>
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
        /// Реализация IComparable
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            var logLevel = obj as LogLevel;

            if (!ReferenceEquals(logLevel, null))
                return Level - logLevel.Level;
            else
                throw new ArgumentException("Object is not a LogLevel");
        }

        #endregion

        #region IComparable<LogLevel> Members

        /// <summary>
        /// Реализация IComparable<LogLevel/>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(LogLevel other)
        {
            if (ReferenceEquals(other, null))
                return 1;

            return Level - other.Level;
        }

        #endregion

        #region IEquatable<LogLevel> Members

        /// <summary>
        /// Реализация IEquatable<LogLevel/>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(LogLevel other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Level == other.Level;
        }

        #endregion

        #region Equality members

        /// <summary>
        /// Переобределние получения хеша
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Level;
        }

        /// <summary>
        /// Переопределение ==
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(LogLevel left, LogLevel right)
        {
            if (object.ReferenceEquals(left, right))
                return true;

            if (object.ReferenceEquals(left, null) || object.ReferenceEquals(right, null))
                return false;

            return left.Level == right.Level;
        }

        /// <summary>
        /// Переопределение !=
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(LogLevel left, LogLevel right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Переопределение
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator >(LogLevel left, LogLevel right)
        {
            Contract.Requires(left != null, "left is null");
            Contract.Requires(right != null, "right is null");

            return left.Level > right.Level;
        }

        /// <summary>
        /// Переопределение
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator >=(LogLevel left, LogLevel right)
        {
            Contract.Requires(left != null, "left is null");
            Contract.Requires(right != null, "right is null");

            return left.Level >= right.Level;
        }

        /// <summary>
        /// Переопределение
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator <(LogLevel left, LogLevel right)
        {
            Contract.Requires(left != null, "left is null");
            Contract.Requires(right != null, "right is null");

            return left.Level < right.Level;
        }

        /// <summary>
        /// Переопределение
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator <=(LogLevel left, LogLevel right)
        {
            Contract.Requires(left != null, "left is null");
            Contract.Requires(right != null, "right is null");

            return left.Level <= right.Level;
        }

        #endregion

        #endregion

        #region override: Equals(object obj), ToString()

        /// <summary>
        /// Переопределение Equals()
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var logLevel = obj as LogLevel;

            if (object.ReferenceEquals(logLevel, null))
                return false;

            return Level == logLevel.Level;
        }

        /// <summary>
        /// Переопределение ToString()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}