using Qoollo.Logger.Common;
using System.Diagnostics.Contracts;
using System.Text;

namespace Qoollo.Logger.LoggingEventConverters
{
    /// <summary>
    /// Base class for LoggingEvent converters. 
    /// Perform convertion of some part of LoggingEvent to string.
    /// </summary>
    public abstract class LoggingEventConverterBase
    {
        /// <summary>
        /// Perform convertion to string representation
        /// </summary>
        /// <param name="data">Source LoggingEvent</param>
        /// <returns>String after conversion</returns>
        public abstract string Convert(LoggingEvent data);
    }

    /// <summary>
    /// Base converter extension that adds 'Prefix', 'Suffix' and 'ValueOnNull' support
    /// </summary>
    public class LoggingEventConverterExtension : LoggingEventConverterBase
    {
        private enum ConversionWay
        {
            NoPrefixSuffix,
            PrefixOnly,
            SuffixOnly,
            PrefixAndSuffix
        }

        private readonly string _prefix;
        private readonly string _suffix;
        private readonly string _valueOnNull;
        private readonly LoggingEventConverterBase _innerConv;
        private readonly ConversionWay _conversionWay;

        /// <summary>
        /// LoggingEventConverterExtension constructor
        /// </summary>
        /// <param name="innerConv">Converter to be wrapped</param>
        public LoggingEventConverterExtension(LoggingEventConverterBase innerConv)
        {
            Contract.Requires(innerConv != null);
            _innerConv = innerConv;
            _conversionWay = ConversionWay.NoPrefixSuffix;
        }

        /// <summary>
        /// LoggingEventConverterExtension constructor
        /// </summary>
        /// <param name="innerConv">Converter to be wrapped</param>
        /// <param name="valueOnNull">Fallback value for 'null'</param>
        public LoggingEventConverterExtension(LoggingEventConverterBase innerConv, string valueOnNull)
        {
            Contract.Requires(innerConv != null);
            _innerConv = innerConv;
            _valueOnNull = valueOnNull;
            _conversionWay = ConversionWay.NoPrefixSuffix;
        }

        /// <summary>
        /// LoggingEventConverterExtension constructor
        /// </summary>
        /// <param name="innerConv">Converter to be wrapped</param>
        /// <param name="prefix">Prefix string</param>
        /// <param name="suffix">Suffix string</param>
        /// <param name="valueOnNull">Fallback value for 'null'</param>
        public LoggingEventConverterExtension(LoggingEventConverterBase innerConv, string prefix, string suffix, string valueOnNull)
        {
            Contract.Requires(innerConv != null);
            _innerConv = innerConv;
            _prefix = prefix;
            _suffix = suffix;
            _valueOnNull = valueOnNull;

            if (prefix == null && suffix == null)
                _conversionWay = ConversionWay.NoPrefixSuffix;
            else if (prefix != null && suffix == null)
                _conversionWay = ConversionWay.PrefixOnly;
            else if (prefix == null && suffix != null)
                _conversionWay = ConversionWay.SuffixOnly;
            else
                _conversionWay = ConversionWay.PrefixAndSuffix;

        }
        
        /// <summary>
        /// Prefix string
        /// </summary>
        public string Prefix { get { return _prefix; } }
        /// <summary>
        /// Suffix string
        /// </summary>
        public string Suffix { get { return _suffix; } }

        /// <summary>
        /// Perform convertion to string representation
        /// </summary>
        /// <param name="data">Source LoggingEvent</param>
        /// <returns>String after conversion</returns>
        public override string Convert(LoggingEvent data)
        {
            var convRes = _innerConv.Convert(data) ?? _valueOnNull;

            if (convRes == null)
                return null;

            switch (_conversionWay)
            {
                case ConversionWay.NoPrefixSuffix:
                    return convRes;
                case ConversionWay.PrefixOnly:
                    return string.Concat(_prefix, convRes);
                case ConversionWay.SuffixOnly:
                    return string.Concat(convRes, _suffix);
                case ConversionWay.PrefixAndSuffix:
                    return string.Concat(_prefix, convRes, _suffix);
            }

            return null;
        }
    }
}