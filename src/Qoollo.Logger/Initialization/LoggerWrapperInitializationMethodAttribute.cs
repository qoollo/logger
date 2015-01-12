using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qoollo.Logger
{
    /// <summary>
    /// Indicates logger initialization method
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class LoggerWrapperInitializationMethodAttribute: Attribute
    {
    }
}
