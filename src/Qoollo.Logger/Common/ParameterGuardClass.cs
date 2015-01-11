using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qoollo.Logger.Common
{
    /// <summary>
    /// Special class to separate main log parameters and additional
    /// </summary>
    public sealed class ParameterGuardClass
    {
        private static readonly ParameterGuardClass _value = new ParameterGuardClass();
        /// <summary>
        /// Instance of ParameterGuardClass
        /// </summary>
        public static ParameterGuardClass Value
        {
            get { return _value; }
        }

        private ParameterGuardClass() { }
    }
}
