using Qoollo.Logger.Common;


namespace Qoollo.Logger.Net
{
    /// <summary>
    /// Реализация интерфейса INetService.
    /// Принимает входящее сообщение, отправляет вызывающей стороне кол-во принятых байт
    /// </summary>
    public abstract class NetService : INetService
    {
        /// <summary>
        /// Приём данных
        /// </summary>
        /// <param name="data">Сообщение логирования</param>
        public void SendData(LoggingEvent data)
        {
            if (data == null)
                return;

            OnDataRecieved(data);
        }

        /// <summary>
        /// Вызов события у подписчиков
        /// </summary>
        protected abstract void OnDataRecieved(LoggingEvent data);
    }
}
