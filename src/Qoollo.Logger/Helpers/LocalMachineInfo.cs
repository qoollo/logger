using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qoollo.Logger.Helpers
{
    /// <summary>
    /// Local machine info
    /// </summary>
    internal static class LocalMachineInfo
    {
        private static string _machineName;
        private static string _machineAddress;
        private static string _combinedMachineName;
        private static string _processName;
        private static int _processId;

        /// <summary>
        /// Machine name
        /// </summary>
        public static string MachineName
        {
            get
            {
                if (_machineName == null)
                    _machineName = Environment.MachineName;
                return _machineName;
            }
        }


        /// <summary>
        /// Init MachineAddress
        /// </summary>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private static void InitMachineAddress()
        {
            if (_machineAddress == null)
            {
                if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                {
                    var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
                    var ipAddr = host.AddressList.FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
                    if (ipAddr != null)
                        _machineAddress = ipAddr.ToString();
                }

                if (_machineAddress == null)
                    _machineAddress = "<no network>";

                System.Threading.Thread.MemoryBarrier();
            }
        }

        /// <summary>
        /// Machine address (ip)
        /// </summary>
        public static string MachineAddress
        {
            get
            {
                if (_machineAddress == null)
                    InitMachineAddress();
                return _machineAddress;
            }
        }

        /// <summary>
        /// Machine name and Machine address
        /// </summary>
        public static string CombinedMachineName
        {
            get
            {
                if (_combinedMachineName == null)
                    _combinedMachineName = MachineName + "(" + MachineAddress + ")";

                return _combinedMachineName;
            }
        }


        /// <summary>
        /// Process name
        /// </summary>
        public static string ProcessName
        {
            get
            {
                if (_processName == null)
                {
                    var process = System.Diagnostics.Process.GetCurrentProcess();
                    _processName = process.ProcessName;
                }

                return _processName;
            }
        }


        /// <summary>
        /// Process ID
        /// </summary>
        public static int ProcessId
        {
            get
            {
                if (_processId == 0)
                {
                    var process = System.Diagnostics.Process.GetCurrentProcess();
                    _processId = process.Id;
                }

                return _processId;
            }
        }
    }
}
