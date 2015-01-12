using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Qoollo.Logger.Net
{
    /// <summary>
    /// Network logger client (WCF)
    /// </summary>
    public class LoggerNetClient: System.ServiceModel.ClientBase<INetService>
    {
        /// <summary>
        /// LoggerNetClient constructor
        /// </summary>
        /// <param name="endpointConfigurationName">The name of the endpoint in the application configuration file</param>
        public LoggerNetClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }
        /// <summary>
        /// LoggerNetClient constructor
        /// </summary>
        /// <param name="endpointConfigurationName">The name of the endpoint in the application configuration file</param>
        /// <param name="remoteAddress">The address of the service endpoint</param>
        public LoggerNetClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }
        /// <summary>
        /// LoggerNetClient constructor
        /// </summary>
        /// <param name="endpointConfigurationName">The name of the endpoint in the application configuration file</param>
        /// <param name="remoteAddress">The address of the service endpoint</param>
        public LoggerNetClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }
        /// <summary>
        /// LoggerNetClient constructor
        /// </summary>
        /// <param name="binding">The binding with which to make calls to the service</param>
        /// <param name="remoteAddress">The address of the service endpoint</param>
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
        /// Remote side API
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
