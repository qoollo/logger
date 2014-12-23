using Qoollo.Logger.Common;
using System.Diagnostics.Contracts;
using System.Text;

namespace Qoollo.Logger.LoggingEventConverters
{
    /// <summary>
    /// Интерфейс конвертера, используемого для преобразования части логируемых данных 
    /// (имени метода или времени) в строковое представление
    /// Может использоваться в последовательности конвертеров, которые строят строку для вывода в файл или консоль
    /// </summary>
    public abstract class LoggingEventConverterBase
    {
        /// <summary>
        ///  Преобразовать в строковое представление данные о событие
        ///  </summary><param name="data">Данные</param><returns></returns>
        public abstract string Convert(LoggingEvent data);
    }

    /// <summary>
    /// Расширение для конвертера с префиксами и суффиксами
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
        /// Конструктор LoggingEventConverterExtension
        /// </summary>
        /// <param name="innerConv">Внутренний конвертер</param>
        public LoggingEventConverterExtension(LoggingEventConverterBase innerConv)
        {
            Contract.Requires(innerConv != null);
            _innerConv = innerConv;
            _conversionWay = ConversionWay.NoPrefixSuffix;
        }

        /// <summary>
        /// Конструктор LoggingEventConverterExtension
        /// </summary>
        /// <param name="innerConv">Внутренний конвертер</param>
        /// <param name="valueOnNull">Значение, когда параметр null</param>
        public LoggingEventConverterExtension(LoggingEventConverterBase innerConv, string valueOnNull)
        {
            Contract.Requires(innerConv != null);
            _innerConv = innerConv;
            _valueOnNull = valueOnNull;
            _conversionWay = ConversionWay.NoPrefixSuffix;
        }

        /// <summary>
        /// Конструктор LoggingEventConverterExtension
        /// </summary>
        /// <param name="innerConv">Внутренний конвертер</param>
        /// <param name="prefix">Префикс</param>
        /// <param name="suffix">Суффикс</param>
        /// <param name="valueOnNull">Значение, когда параметр null</param>
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
        /// Префикс
        /// </summary>
        public string Prefix { get { return _prefix; } }
        /// <summary>
        /// Суффикс
        /// </summary>
        public string Suffix { get { return _suffix; } }

        /// <summary>
        /// Конвертация
        /// </summary>
        /// <param name="data">Сообщение лога</param>
        /// <returns>Строковое представление</returns>
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