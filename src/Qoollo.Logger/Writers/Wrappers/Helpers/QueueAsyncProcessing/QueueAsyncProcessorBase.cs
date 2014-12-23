using System;

namespace Qoollo.Logger.Writers.Wrappers.Helpers.QueueAsyncProcessing
{
    /// <summary>
    /// Базовый класс для асинхронных обработчиков
    /// </summary>
    /// <typeparam name="T">Тип обрабатываемого элемента</typeparam>
    internal abstract class QueueAsyncProcessorBase<T> : IDisposable
    {
        /// <summary>
        /// Попытаться добавить элемент на обработку
        /// </summary>
        /// <param name="element">Элемент</param>
        /// <returns>Удалось ли добавить</returns>
        public abstract bool TryAdd(T element);
        /// <summary>
        /// Добавление элемента на обработку
        /// </summary>
        /// <param name="element">Элемент</param>
        public abstract void Add(T element);

        /// <summary>
        /// Основной код освобождения ресурсов
        /// </summary>
        /// <param name="isUserCall">Вызвано ли освобождение пользователем. False - деструктор</param>
        protected virtual void Dispose(bool isUserCall)
        { 
        }

        /// <summary>
        /// Освобождение ресурсов
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
