using Qoollo.Logger.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Qoollo.Logger.Net
{
    /// <summary>
    /// Logger network API
    /// </summary>
    [ServiceContract]
    public interface INetService
    {
        /// <summary>
        /// Sends LoggingEvent to service
        /// </summary>
        /// <param name="data">Logging event</param>
        [OperationContract(IsOneWay = true)]
        void SendData(LoggingEvent data);
    }
}
