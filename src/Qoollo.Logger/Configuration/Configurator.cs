using System.Collections.Generic;
using System.Linq;

namespace Qoollo.Logger.Configuration
{
    /// <summary>
    /// Загрузка конфигурации логгера
    /// </summary>
    public static class Configurator
    {
        /// <summary>
        /// Функция для загрузки конфигурации логгера
        /// </summary>
        public static LoggerConfiguration LoadConfiguration(string sectionName)
        {
            return Qoollo.Logger.Configuration.LoggerConfigurationV2.AppConfigLoader.GetConfiguration(sectionName);
        }
        /// <summary>
        /// Функция для загрузки конфигурации логгера
        /// </summary>
        public static LoggerConfiguration LoadConfiguration(string sectionGroupName, string sectionName)
        {
            return Qoollo.Logger.Configuration.LoggerConfigurationV2.AppConfigLoader.GetConfiguration(sectionGroupName, sectionName);
        }
    }
}