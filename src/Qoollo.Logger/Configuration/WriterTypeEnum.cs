using System.Collections.Generic;

namespace Qoollo.Logger.Configuration
{
    /// <summary>
    /// Тип писателя
    /// </summary>
    public enum WriterTypeEnum
    {
        /// <summary>
        /// Логгер для сложной настройки роутинга собщений между внутренними логгерами
        /// </summary>
        EmptyWriter,


        /// <summary>
        /// Очередь для поддержания ассинхронности
        /// </summary>
        AsyncQueueWrapper,

        /// <summary>
        /// Очередь для поддержания ассинхронности и надежной доставки логов.
        /// Использует дополнительное хранилище на диске для записи логов, которые не удается отправить в данный момент
        /// </summary>
        AsyncQueueWithReliableSendingWrapper,

        /// <summary>
        /// Консольный логгер
        /// </summary>
        ConsoleWriter,

        /// <summary>
        /// Логгер для записи в файл
        /// </summary>
        FileWriter,

        /// <summary>
        /// Логгер для БД
        /// </summary>
        DBWriter,

        /// <summary>
        /// Логгер для локального сервера - подключение по пайпу
        /// </summary>
        PipeWriter,

        /// <summary>
        /// Логгер для ссетевого сервера - подключение по tcp
        /// </summary>
        NetWriter,

        /// <summary>
        /// Логгер для передачи сообщений группе внутренних логгеров
        /// </summary>
        GroupWrapper,

        /// <summary>
        /// Логгер для сложной настройки роутинга собщений между внутренними логгерами
        /// </summary>
        RoutingWrapper,

        /// <summary>
        /// Враппер с маршрутизацией на основе сравнения паттернов
        /// </summary>
        PatternMatchingWrapper,

        /// <summary>
        /// Враппер для предотвращения потерь логов (ведёт локальное хранилище)
        /// </summary>
        ReliableWrapper,
    }
}