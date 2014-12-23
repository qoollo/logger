using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qoollo.Logger.Writers.Wrappers.Helpers.QueueAsyncProcessing
{
    /// <summary>
    /// Состояние асинхронного обработчика
    /// </summary>
    internal enum QueueAsyncProcessorState : int
    {
        /// <summary>
        /// Создан
        /// </summary>
        Created = 0,
        /// <summary>
        /// В процессе запуска
        /// </summary>
        StartPending = 1,
        /// <summary>
        /// Работает
        /// </summary>
        InWork = 2,
        /// <summary>
        /// В процессе остановки
        /// </summary>
        StopPending = 3,
        /// <summary>
        /// Остановлен
        /// </summary>
        Stopped = 4,
        /// <summary>
        /// Уничтожен
        /// </summary>
        Disposed = 5
    }
}
