using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Qoollo.Logger.Common
{
    /// <summary>
    /// Контейнер, хранящий информацию о сообщении, которое хотим залогировать
    /// </summary>
    [DataContract]
    public class LoggingEvent
    {
        /// <summary>
        /// Констуктор для создания логгирующего сообщения (нужен для десериализации)
        /// </summary>
        protected internal LoggingEvent()
        {
        }

        /// <summary>
        /// Констуктор для создания логгирующего сообщения
        /// </summary>
        /// <param name="date">Дата</param>
        /// <param name="message">Сообщение</param>
        /// <param name="exception">Исключение</param>
        /// <param name="level">Уровень логирования</param>
        /// <param name="context">Контекст</param>
        /// <param name="stackSources">Источник</param>
        /// <param name="machineName">Имя машины</param>
        /// <param name="processName">Имя процесса</param>
        /// <param name="processId">Идентификатор процесса</param>
        /// <param name="assembly">Сборка</param>
        /// <param name="namespace">Пространство имён</param>
        /// <param name="class">Класс</param>
        /// <param name="method">Метод</param>
        /// <param name="filePath">Путь до файла</param>
        /// <param name="lineNumber">Номер строки</param>
        public LoggingEvent(DateTime date, string message, Exception exception, LogLevel level, string context,
                            List<string> stackSources, string machineName = null, string processName = null, int processId = 0, string assembly = null, string @namespace = null, string @class = null, string method = null,
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

            if (exception != null)
                Exception = new Error(exception);
        }

        /// <summary>
        /// Констуктор для создания логгирующего сообщения
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="exception">Исключение</param>
        /// <param name="level">Уровень логирования</param>
        /// <param name="context">Контекст</param>
        /// <param name="stackSources">Источник</param>
        /// <param name="machineName">Имя машины</param>
        /// <param name="processName">Имя процесса</param>
        /// <param name="processId">Идентификатор процесса</param>
        /// <param name="assembly">Сборка</param>
        /// <param name="namespace">Пространство имён</param>
        /// <param name="class">Класс</param>
        /// <param name="method">Метод</param>
        /// <param name="filePath">Путь до файла</param>
        /// <param name="lineNumber">Номер строки</param>
        public LoggingEvent(string message, Exception exception, LogLevel level, string context,
                            List<string> stackSources, string machineName = null, string processName = null, int processId = 0, string assembly = null, string @namespace = null, string @class = null, string method = null, string filePath = null,
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

            if (exception != null)
                Exception = new Error(exception);
        }


        /// <summary>
        /// Констуктор для создания логгирующего сообщения
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="exception">Исключение</param>
        /// <param name="level">Уровень логирования</param>
        /// <param name="stackSources">Источник</param>
        /// <param name="machineName">Имя машины</param>
        /// <param name="processName">Имя процесса</param>
        /// <param name="processId">Идентификатор процесса</param>
        /// <param name="assembly">Сборка</param>
        /// <param name="namespace">Пространство имён</param>
        /// <param name="class">Класс</param>
        /// <param name="method">Метод</param>
        /// <param name="filePath">Путь до файла</param>
        /// <param name="lineNumber">Номер строки</param>
        public LoggingEvent(string message, Exception exception, LogLevel level,
                            List<string> stackSources, string machineName = null, string processName = null, int processId = 0, string assembly = null, string @namespace = null, string @class = null, string method = null, string filePath = null,
                            int lineNumber = -1)
        {
            Date = DateTime.Now;
            Message = message;
            Level = level;
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

            if (exception != null)
                Exception = new Error(exception);
        }

        #region properties
        
        /// <summary>
        /// Дата и время создания логирующего сообщения
        /// </summary>
        [DataMember(Order = 1)]
        public DateTime Date { get; private set; }

        /// <summary>
        /// Уровень логирования
        /// </summary>
        [DataMember(Order = 2)]
        public LogLevel Level { get; private set; }

        /// <summary>
        /// Контекст логируемого сообщения
        /// для упрощения поиска и отслещивания лога от отдельных подсистем или для определенных процессов
        /// </summary>
        /// <summary>
        /// Пример использования - "id:12,name:NetSystem"
        /// "id:Arm13,task:24,cam:3"
        /// </summary>>
        [DataMember(Order = 3)]
        public string Context { get; private set; }

        /// <summary>
        /// Полное имя класса которое мы хотим вывести в логе, т.е. Namespace.Class
        /// Важное замечание: для чтения лога с помощью программы LogReader обязательно должна присутствовать
        /// и левая и правая часть имени через точку - иначе программа вылетает
        /// </summary>
        [DataMember(Order = 4)]
        public string Clazz { get; private set; }

        /// <summary>
        /// Имя метода из которого было вызвано логирование
        /// </summary>
        [DataMember(Order = 5)]
        public string Method { get; private set; }

        /// <summary>
        /// Полное имя файла исходного кода, в котором было вызвано логирование
        /// </summary>
        [DataMember(Order = 6)]
        public string FilePath { get; private set; }

        /// <summary>
        /// Номер строки в файле исходного кода, в которой было вызвано логирование
        /// </summary>
        [DataMember(Order = 7)]
        public int LineNumber { get; private set; }

        /// <summary>
        /// Логируемый текст, сообщение
        /// </summary>
        [DataMember(Order = 8)]
        public string Message { get; private set; }

        /// <summary>
        /// Иформация об исключительной информации
        /// </summary>
        [DataMember(Order = 9)]
        public Error Exception { get; private set; }

        /// <summary>
        /// Список имен модулей в порядке иерархии.
        /// Внутренний логгеры первый в списке, внешний - последний
        /// </summary>
        [DataMember(Order = 10)]
        public List<string> StackSources { get; private set; }

        /// <summary>
        /// Пространство имён
        /// </summary>
        [DataMember(Order = 11)]
        public string Namespace { get; private set; }

        /// <summary>
        /// Название сборки
        /// </summary>
        [DataMember(Order = 12)]
        public string Assembly { get; private set; }

        /// <summary>
        /// Имя компьютера
        /// </summary>
        [DataMember(Order = 13)]
        public string MachineName { get; private set; }

        /// <summary>
        /// Имя процесса
        /// </summary>
        [DataMember(Order = 14)]
        public string ProcessName { get; private set; }

        /// <summary>
        /// Идентификатор процесса
        /// </summary>
        [DataMember(Order = 15)]
        public int ProcessId { get; private set; }

        #endregion
    }
}