using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Qoollo.Logger.Net
{
    internal class InternalStableLoggerNetClient : StableLoggerNetClient
    {
        private static readonly Logger _thisClassSupportLogger = InnerSupportLogger.Instance.GetClassLogger(typeof(InternalStableLoggerNetClient));


        public static InternalStableLoggerNetClient CreateOnTcpInternal(string address, int port, string serviceName = "LoggingService")
        {
            EndpointAddress addr = new EndpointAddress(string.Format("net.tcp://{0}:{1}/{2}", address, port, serviceName));
            var binding = new NetTcpBinding(SecurityMode.None);

            return new InternalStableLoggerNetClient(binding, addr, 16000);
        }

        public static InternalStableLoggerNetClient CreateOnPipeInternal(string address, string pipeName = "LoggingService")
        {
            EndpointAddress addr = new EndpointAddress(string.Format("net.pipe://{0}/{1}", address, pipeName));
            var binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);

            return new InternalStableLoggerNetClient(binding, addr, 16000);
        }


        public InternalStableLoggerNetClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress, int connectionTestTimeMs)
            : base(binding, remoteAddress, connectionTestTimeMs)
        {
        }

        protected override void LogError(Exception ex, string message)
        {
            if (ex == null)
                _thisClassSupportLogger.Error(message);
            else
                _thisClassSupportLogger.Error(ex, message);
        }

        protected override void LogWarn(Exception ex, string message)
        {
            if (ex == null)
                _thisClassSupportLogger.Warn(message);
            else
                _thisClassSupportLogger.Warn(ex, message);
        }
    }
}
