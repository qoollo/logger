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
    /// Простой интерфейс для передачи данных по сети
    /// </summary>
    [ServiceContract]
    public interface INetService
    {
        /// <summary>
        /// Передаем данные на сервер
        /// </summary>
        /// <param name="data">Событие лога</param>
        [OperationContract(IsOneWay = true)]
        void SendData(LoggingEvent data);
    }
}
