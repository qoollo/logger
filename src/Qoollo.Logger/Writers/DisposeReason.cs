using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qoollo.Logger.Writers
{
    /// <summary>
    /// The reason why common dispose method was called
    /// </summary>
    internal enum DisposeReason
    {
        /// <summary>
        /// Manual dispose
        /// </summary>
        Dispose,
        /// <summary>
        /// Manual close
        /// </summary>
        Close,
        /// <summary>
        /// Finalizer
        /// </summary>
        Finalize
    }
}
