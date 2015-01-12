using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qoollo.Logger.Configuration
{
    /// <summary>
    /// Container for custom log Writer parameters from App.config
    /// </summary>
    public class CustomAppConfigWriterConfiguration: CustomWriterConfiguration
    {
        /// <summary>
        /// CustomAppConfigWriterConfiguration constructor
        /// </summary>
        /// <param name="level">Log level</param>
        /// <param name="writerType">Writer type</param>
        /// <param name="parameters">Dictionary with all passed paramters</param>
        public CustomAppConfigWriterConfiguration(LogLevel level, string writerType, Dictionary<string, string> parameters)
            : base(level)
        {
            Contract.Requires<ArgumentNullException>(writerType != null);
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(writerType));

            WriterTypeString = writerType;
            Parameters = parameters ?? new Dictionary<string, string>();
        }
        /// <summary>
        /// CustomAppConfigWriterConfiguration constructor
        /// </summary>
        /// <param name="level">Log level</param>
        /// <param name="writerType">Writer type</param>
        public CustomAppConfigWriterConfiguration(LogLevel level, string writerType)
            : this(level, writerType, null)
        {
        }

        /// <summary>
        /// Writer type
        /// </summary>
        public string WriterTypeString { get; private set; }
        /// <summary>
        /// Dictionary with all passed paramters
        /// </summary>
        public Dictionary<string, string> Parameters { get; private set; }
    }




    /// <summary>
    /// Converter from CustomAppConfigWriterConfiguration to concrete configuration type
    /// </summary>
    internal static class CustomAppConfigWriterConfigurationConverter
    {
        private static readonly Dictionary<string, Func<CustomAppConfigWriterConfiguration, CustomWriterConfiguration>> _userConverters = new Dictionary<string, Func<CustomAppConfigWriterConfiguration, CustomWriterConfiguration>>();


        /// <summary>
        /// Register new converter
        /// </summary>
        /// <param name="writerType">Writer type string</param>
        /// <param name="converter">Conversion function</param>
        public static void RegisterConverter(string writerType, Func<CustomAppConfigWriterConfiguration, CustomWriterConfiguration> converter)
        {
            Contract.Requires<ArgumentNullException>(writerType != null);
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(writerType));
            Contract.Requires<ArgumentNullException>(converter != null);

            lock (_userConverters)
            {
                _userConverters.Add(writerType, converter);
            }
        }

        /// <summary>
        /// Unregister converter
        /// </summary>
        /// <param name="writerType">Writer type string</param>
        /// <returns>true if the element is successfully found and removed</returns>
        public static bool UnregisterConverter(string writerType)
        {
            Contract.Requires<ArgumentNullException>(writerType != null);

            lock (_userConverters)
            {
                return _userConverters.Remove(writerType);
            }
        }


        /// <summary>
        /// Perform conversion (if conversion is not possible then returns 'source' value without any change)
        /// </summary>
        /// <param name="source">Source configuration</param>
        /// <returns>Converted value or original</returns>
        public static CustomWriterConfiguration Convert(CustomAppConfigWriterConfiguration source)
        {
            Contract.Requires<ArgumentNullException>(source != null);

            lock (_userConverters)
            {
                Func<CustomAppConfigWriterConfiguration, CustomWriterConfiguration> converter = null;
                if (_userConverters.TryGetValue(source.WriterTypeString, out converter))
                    return converter(source);
            }

            return source;
        }
    }
}
