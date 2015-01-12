using Qoollo.Logger.Common;


namespace Qoollo.Logger.Net
{
    /// <summary>
    /// Generic implementation of 'INetService'
    /// </summary>
    public abstract class NetService : INetService
    {
        /// <summary>
        /// Sends LoggingEvent to service
        /// </summary>
        /// <param name="data">Logging event</param>
        public void SendData(LoggingEvent data)
        {
            if (data == null)
                return;

            OnDataRecieved(data);
        }

        /// <summary>
        /// Perform real processing of received log event
        /// </summary>
        /// <param name="data">Logging event</param>
        protected abstract void OnDataRecieved(LoggingEvent data);
    }
}
