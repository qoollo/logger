using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Qoollo.Logger.Net
{
    /// <summary>
    /// Сервер сетевого логгера
    /// </summary>
    public class LoggerServer : IDisposable
    {
        /// <summary>
        /// Создать сервис на основе конифга в app.config
        /// </summary>
        /// <param name="singleton">Синглтон хэндлера</param>
        /// <returns>Созданный сервер</returns>
        public static LoggerServer Create(INetService singleton)
        {
            ServiceHost host = new ServiceHost(singleton);

            return new LoggerServer(host);
        }

        /// <summary>
        /// Создать сервис на TCP
        /// </summary>
        /// <param name="singleton">Синглтон хэндлера</param>
        /// <param name="port">Порт</param>
        /// <param name="serviceName">Имя сервиса WCF</param>
        /// <returns>Созданный сервер</returns>
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
        /// Создать сервис на Pipe
        /// </summary>
        /// <param name="singleton">Синглтон хэндлера</param>
        /// <param name="pipeName">Имя пайпа</param>
        /// <returns>Созданный сервер</returns>
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
        /// Конструктор LoggerServer
        /// </summary>
        /// <param name="host">Хост</param>
        protected LoggerServer(ServiceHost host)
        {
            if (host == null)
                throw new ArgumentNullException("host");

            _host = host;
        }

        /// <summary>
        /// Открыть сервис
        /// </summary>
        public void Open()
        {
            _host.Open();
        }


       
        /// <summary>
        /// Освободить ресурсы
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
