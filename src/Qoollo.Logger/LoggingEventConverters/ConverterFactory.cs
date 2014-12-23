using System;
using System.Collections.Generic;
using Qoollo.Logger.Common;

namespace Qoollo.Logger.LoggingEventConverters
{
    /// <summary>
    /// Фабрика для создания конвертеров,
    /// необходимых для преобразования логируемых данных в строки для вывода в файл или консоль
    /// </summary>
    public class ConverterFactory
    {
        private static readonly ConverterFactory _default = new ConverterFactory();

        /// <summary>
        /// Инстанс фабрики по-умолчанию
        /// </summary>
        public static ConverterFactory Default { get { return _default; } }

        /// <summary>
        /// Создание результирующего конвертора,
        /// содержащего последовательность конверторов, 
        /// с помощью которых он получает результирующую строку
        /// </summary>
        /// <param name="converters">Пооследовательность конвертеров</param>
        /// <returns></returns>
        public virtual LoggingEventConverterBase CreateLoggingEventConverter(List<LoggingEventConverterBase> converters)
        {
            return new LoggingEventConverter(converters);
        }

        /// <summary>
        /// Создание конвертора для статической части шаблона
        /// Пример
        /// Для шаблона "-- {Msg} " - Потребуется два статичных конвертора
        /// 1) Возвращает строку "-- "
        /// 2) Возвращает " "
        /// </summary>
        /// <param name="constString">строка</param>
        /// <returns></returns>
        public virtual LoggingEventConverterBase CreateConstConverter(string constString)
        {
            return new ConstConverter(constString);
        }

        /// <summary>
        /// Создание конвертора для контекста
        /// </summary>
        /// <returns></returns>
        public virtual LoggingEventConverterBase CreateContextConverter()
        {
            return new ContextConverter();
        }

        /// <summary>
        /// Создание ковертора для преобразования даты в строковый формат
        /// </summary>
        /// <param name="format">Формат даты и времени</param>
        /// <returns></returns>
        public virtual LoggingEventConverterBase CreateDateConverter(string format)
        {
            return new DateConverter(format);
        }

        /// <summary>
        /// Создание ковертора для получения строки из уровня логирования
        /// </summary>
        /// <returns></returns>
        public virtual LoggingEventConverterBase CreateLevelConverter()
        {
            return new LevelConverter();
        }

        /// <summary>
        /// Создание конвертера для получения имени машины
        /// </summary>
        /// <returns></returns>
        public virtual LoggingEventConverterBase CreateMachineNameConverter()
        {
            return new MachineNameConverter();
        }

        /// <summary>
        /// Создание конвертера для получения имени процесса
        /// </summary>
        /// <returns></returns>
        public virtual LoggingEventConverterBase CreateProcessNameConverter()
        {
            return new ProcessNameConverter();
        }

        /// <summary>
        /// Создание конвертера для получения идентификатора процесса
        /// </summary>
        /// <returns></returns>
        public virtual LoggingEventConverterBase CreateProcessIdConverter()
        {
            return new ProcessIdConverter();
        }

        /// <summary>
        /// Создание конвертера для получения имени сборки
        /// </summary>
        /// <returns></returns>
        public virtual LoggingEventConverterBase CreateAssemblyConverter()
        {
            return new AssemblyConverter();
        }

        /// <summary>
        /// Создание конвертера для получения пространства имён
        /// </summary>
        /// <returns></returns>
        public virtual LoggingEventConverterBase CreateNamespaceConverter()
        {
            return new NamespaceConverter();
        }

        /// <summary>
        /// Создание ковертора для получения имени класса
        /// </summary>
        /// <returns></returns>
        public virtual LoggingEventConverterBase CreateClassConverter()
        {
            return new ClassConverter();
        }

        /// <summary>
        /// Создание ковертора для получения имени метода
        /// </summary>
        /// <returns></returns>
        public virtual LoggingEventConverterBase CreateMethodConverter()
        {
            return new MethodConverter();
        }

        /// <summary>
        /// Создание ковертора для получения строки сообщения
        /// </summary>
        /// <returns></returns>
        public virtual LoggingEventConverterBase CreateMessageConverter()
        {
            return new MessageConverter();
        }

        /// <summary>
        /// Создание ковертора для получения строкого представления исключения
        /// </summary>
        /// <returns></returns>
        public virtual LoggingEventConverterBase CreateExceptionConverter()
        {
            return new ExceptionConverter();
        }

        /// <summary>
        /// Создание ковертора для получения строки из списка StackSource
        /// </summary>
        /// <returns></returns>
        public virtual LoggingEventConverterBase CreateStackSourceConverter()
        {
            return new StackSourceConverter();
        }
    }
}