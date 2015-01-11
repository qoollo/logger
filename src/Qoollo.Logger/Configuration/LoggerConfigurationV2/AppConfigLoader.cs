using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qoollo.Logger.Configuration.LoggerConfigurationV2
{
    internal static class AppConfigLoader
    {
        /// <summary>
        /// Returns the logger configuration from App.config
        /// </summary>
        /// <param name="sectionName">Configuration section name in App.config</param>
        /// <returns>Configuration</returns>
        internal static ILoggerConfigurationSection LoadSection(string sectionName)
        {
            var cfgSec = (Qoollo.Logger.Configuration.LoggerConfigurationSectionConfigClass)ConfigurationManager.GetSection(sectionName);

            return cfgSec.ExtractConfigData();
        }

        /// <summary>
        /// Returns the logger configuration from App.config
        /// </summary>
        /// <param name="sectionGroup">Configuration section group name in App.config</param>
        /// <param name="sectionName">Configuration section name in App.config</param>
        /// <returns>Configuration</returns>
        internal static ILoggerConfigurationSection LoadSection(string sectionGroup, string sectionName)
        {
            var cfgSec = (Qoollo.Logger.Configuration.LoggerConfigurationSectionConfigClass)ConfigurationManager.GetSection(string.Format("{0}/{1}", sectionGroup, sectionName));

            return cfgSec.ExtractConfigData();
        }



        /// <summary>
        /// Read configuration from App.config and converts it to main configuration format
        /// </summary>
        /// <param name="sectionName">Configuration section name in App.config</param>
        /// <returns>Configuration</returns>
        public static LoggerConfiguration GetConfiguration(string sectionName)
        {
            Contract.Requires(sectionName != null, "sectionName");

            var section = LoadSection(sectionName);

            Contract.Assume(section != null, "section");

            var configuration = ConfigurationFormatConverter.Convert(section);

            if (configuration == null)
                throw new Qoollo.Logger.Exceptions.LoggerConfigurationException("Logger configuration error");

            return configuration;
        }

        /// <summary>
        /// Read configuration from App.config and converts it to main configuration format
        /// </summary>
        /// <param name="sectionGroupName">Configuration section group name in App.config</param>
        /// <param name="sectionName">Configuration section name in App.config</param>
        /// <returns>Configuration</returns>
        public static LoggerConfiguration GetConfiguration(string sectionGroupName, string sectionName)
        {
            return GetConfiguration(string.Format("{0}/{1}", sectionGroupName, sectionName));
        }
    }
}
