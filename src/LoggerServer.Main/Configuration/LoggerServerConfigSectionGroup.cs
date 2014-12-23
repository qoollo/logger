using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerServer.Main.Configuration
{
    public class LoggerServerConfigSectionGroup 
    {
        public const string LoggerServerSectionGroupName = "LoggerServerConfig";


        public LoggerServer.Main.Configuration.LoggerServerConfigurationConfigClass LoggerServerMainConfigurationSection
        {
            get
            {
                return ConfigurationManager.GetSection(LoggerServerSectionGroupName + "/" + "LoggerServerConfigurationSection") as LoggerServer.Main.Configuration.LoggerServerConfigurationConfigClass;
            }
        }



        public LoggerServer.Main.Configuration.ILoggerServerConfiguration LoadLoggerServerConfigurationSection()
        {
            return this.LoggerServerMainConfigurationSection.ExtractConfigData();
        }
    }
}
