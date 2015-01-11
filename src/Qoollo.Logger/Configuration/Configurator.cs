using System.Collections.Generic;
using System.Linq;

namespace Qoollo.Logger.Configuration
{
    /// <summary>
    /// Performs configuration loading from App.config
    /// </summary>
    public static class Configurator
    {
        /// <summary>
        /// Loads logger configuration from App.config
        /// </summary>
        /// <param name="sectionName">Section name in App.config</param>
        /// <returns>Loaded configuration object</returns>
        public static LoggerConfiguration LoadConfiguration(string sectionName)
        {
            return Qoollo.Logger.Configuration.LoggerConfigurationV2.AppConfigLoader.GetConfiguration(sectionName);
        }
        /// <summary>
        /// Loads logger configuration from App.config
        /// </summary>
        /// <param name="sectionGroupName">Section group name in App.config</param>
        /// <param name="sectionName">Section name in App.config</param>
        /// <returns>Loaded configuration object</returns>
        public static LoggerConfiguration LoadConfiguration(string sectionGroupName, string sectionName)
        {
            return Qoollo.Logger.Configuration.LoggerConfigurationV2.AppConfigLoader.GetConfiguration(sectionGroupName, sectionName);
        }
    }
}