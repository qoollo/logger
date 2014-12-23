using System;
using System.Runtime.Serialization;

namespace Qoollo.Logger.Common
{
    /// <summary>
    /// Класс для хранения и передачи информации об ошибке.
    /// Мы меньше храним и у нас не возникает проблем с сериализацией/десериализацией
    /// </summary>
    [DataContract]
    public class Error
    {
        /// Констуктор по умолчанию нужен для десериализации
        protected internal Error()
        {
        }

        /// <summary>
        /// Конструктор для создания внутреннего типа для хранения информации об ошибке
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        /// <param name="source"></param>
        /// <param name="stackTrace"></param>
        /// <param name="innerError"></param>
        public Error(string type,string message,string source, string stackTrace, Error innerError)
        {
            Type = type;
            Message = message;
            Source = source;
            StackTrace = stackTrace;
            InnerError = innerError;
        }

        /// <summary>
        /// Конструктор для создания внутреннего типа для хранения информации об ошибке
        /// </summary>
        /// <param name="exception"></param>
        public Error(Exception exception)
        {
            Type = exception.GetType().FullName;
            Message = exception.Message;
            Source = exception.Source;
            StackTrace = exception.StackTrace;
            
            if (exception.InnerException != null)
                InnerError = new Error(exception.InnerException);
        }

        /// <summary>
        /// Имя типа исключения
        /// </summary>
        [DataMember(Order = 1)]
        public string Type { get; private set; }

        /// <summary>
        /// Сообщение исключения
        /// </summary>
        [DataMember(Order = 2)]
        public string Message { get; private set; }

        /// <summary>
        /// Источник исключения
        /// </summary>
        [DataMember(Order = 3)]
        public string Source { get; private set; }

        /// <summary>
        /// Стек вызовов
        /// </summary>
        [DataMember(Order = 4)]
        public string StackTrace { get; private set; }

        /// <summary>
        /// Внутренняя ошибка
        /// </summary>
        [DataMember(Order = 5)]
        public Error InnerError { get; private set; }
    }
}