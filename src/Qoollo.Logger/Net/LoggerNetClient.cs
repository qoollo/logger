using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Qoollo.Logger.Net
{
    /// <summary>
    /// Клиент сетевого логгера
    /// </summary>
    public class LoggerNetClient: System.ServiceModel.ClientBase<INetService>
    {
        /// <summary>
        /// Конструктор LoggerNetClient
        /// </summary>
        /// <param name="endpointConfigurationName">Имя конфигурации Endpoint</param>
        public LoggerNetClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }
        /// <summary>
        /// Конструктор LoggerNetClient
        /// </summary>
        /// <param name="endpointConfigurationName">Имя конфигурации Endpoint</param>
        /// <param name="remoteAddress">Адрес</param>
        public LoggerNetClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }
        /// <summary>
        /// Конструктор LoggerNetClient
        /// </summary>
        /// <param name="endpointConfigurationName">Имя конфигурации Endpoint</param>
        /// <param name="remoteAddress">Адрес</param>
        public LoggerNetClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }
        /// <summary>
        /// Конструктор LoggerNetClient
        /// </summary>
        /// <param name="binding">binding</param>
        /// <param name="remoteAddress">Адрес</param>
        public LoggerNetClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        //public event EventHandler Faulted
        //{
        //    add { (this as ICommunicationObject).Faulted += value; }
        //    remove { (this as ICommunicationObject).Faulted -= value; }
        //}

        /// <summary>
        /// API удалённой стороны
        /// </summary>
        public INetService RemoteSide
        {
            get
            {
                return this.Channel;
            }
        }
    }
}
