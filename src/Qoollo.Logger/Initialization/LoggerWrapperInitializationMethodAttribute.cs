using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qoollo.Logger
{
    /// <summary>
    /// Атрибут для указания метода инициализации логгера
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class LoggerWrapperInitializationMethodAttribute: Attribute
    {
    }
}
