using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Qoollo.Logger.Net
{
    /// <summary>
    /// Network logger server (WCF)
    /// </summary>
    public class LoggerServer : IDisposable
    {
        /// <summary>
        /// Create service from app.config
        /// </summary>
        /// <param name="singleton">Service handler</param>
        /// <returns>Created server</returns>
        public static LoggerServer Create(INetService singleton)
        {
            ServiceHost host = new ServiceHost(singleton);

            return new LoggerServer(host);
        }

        /// <summary>
        /// Creates WCF service on TCP
        /// </summary>
        /// <param name="singleton">Service handler singleton</param>
        /// <param name="port">TCP port</param>
        /// <param name="serviceName">Service name (WCF)</param>
        /// <returns>Created server</returns>
        public static LoggerServer CreateOnTcp(INetService singleton, int port, string serviceName = "LoggingService")
        {
            Uri baseAddr = new Uri(string.Format("net.tcp://0.0.0.0:{0}/{1}", port, serviceName));
            ServiceHost host = new ServiceHost(singleton, baseAddr);

            var binding = new NetTcpBinding(SecurityMode.None);
            host.AddServiceEndpoint(typeof(INetService), binding, baseAddr);

            var behavior = host.Description.Behaviors.Find<ServiceBehaviorAttribute>();
            behavior.InstanceContextMode = InstanceContextMode.Single;

            var debugBehavior = host.Description.Behaviors.Find<System.ServiceModel.Description.ServiceDebugBehavior>();

            if (debugBehavior != null)
                debugBehavior.IncludeExceptionDetailInFaults = true;

            return new LoggerServer(host);
        }

        /// <summary>
        /// Creates WCF service on Pipe
        /// </summary>
        /// <param name="singleton">Service handler singleton</param>
        /// <param name="pipeName">Pipe name</param>
        /// <returns>Created server</returns>
        public static LoggerServer CreateOnPipe(INetService singleton, string pipeName = "LoggingService")
        {
            Uri baseAddr = new Uri(string.Format("net.pipe://localhost/{0}", pipeName));
            ServiceHost host = new ServiceHost(singleton, baseAddr);

            var binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
            host.AddServiceEndpoint(typeof(INetService), binding, baseAddr);

            var behavior = host.Description.Behaviors.Find<ServiceBehaviorAttribute>();
            behavior.InstanceContextMode = InstanceContextMode.Single;

            var debugBehavior = host.Description.Behaviors.Find<System.ServiceModel.Description.ServiceDebugBehavior>();

            if (debugBehavior != null)
                debugBehavior.IncludeExceptionDetailInFaults = true;

            return new LoggerServer(host);
        }


        private ServiceHost _host;

        /// <summary>
        /// LoggerServer constructor
        /// </summary>
        /// <param name="host">Host</param>
        protected LoggerServer(ServiceHost host)
        {
            if (host == null)
                throw new ArgumentNullException("host");

            _host = host;
        }

        /// <summary>
        /// Open server (start listerning)
        /// </summary>
        public void Open()
        {
            _host.Open();
        }


       
        /// <summary>
        /// Clean-up server resource
        /// </summary>
        public void Dispose()
        {
            if (_host != null)
            {
                if (_host.State != CommunicationState.Closed)
                {
                    try
                    {
                        _host.Close();
                    }
                    catch
                    {
                        _host.Abort();
                    }
                }
                _host = null;
            }
        }
    }
}
