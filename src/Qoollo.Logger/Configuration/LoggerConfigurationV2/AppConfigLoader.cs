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
        /// Возвращает конфигурацию логгера из конфигурационного файла
        /// </summary>
        /// <param name="sectionName">Имя конфигурационной секции в AppConfig </param>
        /// <returns>Логгер</returns>
        internal static ILoggerConfigurationSection LoadSection(string sectionName)
        {
            var cfgSec = (Qoollo.Logger.Configuration.LoggerConfigurationSectionConfigClass)ConfigurationManager.GetSection(sectionName);

            return cfgSec.ExtractConfigData();
        }

        /// <summary>
        /// Возвращает конфигурацию логгера из конфигурационного файла
        /// </summary>
        /// <param name="sectionGroup">Имя группы секций в AppConfig </param>
        /// <param name="sectionName">Имя конфигурационной секции в AppConfig </param>
        /// <returns>Логгер</returns>
        internal static ILoggerConfigurationSection LoadSection(string sectionGroup, string sectionName)
        {
            var cfgSec = (Qoollo.Logger.Configuration.LoggerConfigurationSectionConfigClass)ConfigurationManager.GetSection(string.Format("{0}/{1}", sectionGroup, sectionName));

            return cfgSec.ExtractConfigData();
        }

        /// <summary>
        /// Возвращает конфигурацию логгера из конфигурационного файла
        /// </summary>
        /// <param name="sectionName">Имя конфигурационной секции в AppConfig </param>
        /// <returns>Логгер</returns>
        public static LoggerConfiguration GetConfiguration(string sectionName)
        {
            Contract.Requires(sectionName != null, "sectionName");

            var section = LoadSection(sectionName);

            Contract.Assume(section != null, "section");

            var configuration = ConfigurationFormatConverter.Convert(section);

            if (configuration == null)
                throw new Qoollo.Logger.Exceptions.LoggerConfigurationException("Ошбка конфигурирования логгера");

            return configuration;
        }


        public static LoggerConfiguration GetConfiguration(string sectionGroupName, string sectionName)
        {
            return GetConfiguration(string.Format("{0}/{1}", sectionGroupName, sectionName));
        }
    }
}
