using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qoollo.Logger.Common
{
    /// <summary>
    /// Класс для экранирования вспомогательных параметров при логировании
    /// </summary>
    public sealed class ParameterGuardClass
    {
        private static readonly ParameterGuardClass _value = new ParameterGuardClass();
        /// <summary>
        /// Единственный инстанс для экранирования
        /// </summary>
        public static ParameterGuardClass Value
        {
            get { return _value; }
        }

        private ParameterGuardClass() { }
    }
}
