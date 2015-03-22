using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Qoollo.Logger.Configuration
{
    /// <summary>
    /// Performs configuration loading from App.config
    /// </summary>
    public static class Configurator
    {
        /// <summary>
        /// Register new converter from CustomAppConfigWriterConfiguration to concrete configuration object
        /// </summary>
        /// <param name="writerType">Writer type string</param>
        /// <param name="converter">Conversion function</param>
        public static void RegisterConverter(string writerType, Func<CustomAppConfigWriterConfiguration, CustomWriterConfiguration> converter)
        {
            CustomAppConfigWriterConfigurationConverter.RegisterConverter(writerType, converter);
        }
        /// <summary>
        /// Unregister converter
        /// </summary>
        /// <param name="writerType">Writer type string</param>
        /// <returns>true if the element is successfully found and removed</returns>
        public static bool UnregisterConverter(string writerType)
        {
            return CustomAppConfigWriterConfigurationConverter.UnregisterConverter(writerType);
        }



        /// <summary>
        /// Check whether configuration section exists
        /// </summary>
        /// <param name="sectionName">Configuration section name in App.config</param>
        /// <returns>Is section exists</returns>
        public static bool HasConfiguration(string sectionName)
        {
            Contract.Requires<ArgumentNullException>(sectionName != null);

            return Qoollo.Logger.Configuration.LoggerConfigurationV2.AppConfigLoader.HasConfiguration(sectionName);
        }
        /// <summary>
        /// Check whether configuration section exists
        /// </summary>
        /// <param name="sectionGroupName">Configuration section group name in App.config</param>
        /// <param name="sectionName">Configuration section name in App.config</param>
        /// <returns>Is section exists</returns>
        public static bool HasConfiguration(string sectionGroupName, string sectionName)
        {
            Contract.Requires<ArgumentNullException>(sectionGroupName != null);
            Contract.Requires<ArgumentNullException>(sectionName != null);

            return Qoollo.Logger.Configuration.LoggerConfigurationV2.AppConfigLoader.HasConfiguration(sectionGroupName, sectionName);
        }


        /// <summary>
        /// Loads logger configuration from App.config
        /// </summary>
        /// <param name="sectionName">Section name in App.config</param>
        /// <returns>Loaded configuration object</returns>
        public static LoggerConfiguration LoadConfiguration(string sectionName)
        {
            Contract.Requires<ArgumentNullException>(sectionName != null);

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
            Contract.Requires<ArgumentNullException>(sectionGroupName != null);
            Contract.Requires<ArgumentNullException>(sectionName != null);

            return Qoollo.Logger.Configuration.LoggerConfigurationV2.AppConfigLoader.GetConfiguration(sectionGroupName, sectionName);
        }
    }
}