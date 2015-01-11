using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qoollo.Logger.Helpers
{
    /// <summary>
    /// Time tracker to log errors
    /// </summary>
    internal class ErrorTimeTracker
    {
        private long _initTimeTicks;
        private readonly long _ticksInterval = TimeSpan.FromMinutes(5).Ticks;

        public ErrorTimeTracker()
            : this(TimeSpan.FromMinutes(5))
        {
        }
        public ErrorTimeTracker(TimeSpan interval)
        {
            _ticksInterval = interval.Ticks;
            _initTimeTicks = DateTime.Now.Ticks - _ticksInterval - 1;
        }


        public bool CanWriteErrorGetAndUpdate()
        {
            var now = DateTime.Now;

            if (now.Ticks - _initTimeTicks > _ticksInterval)
            {
                System.Threading.Interlocked.Exchange(ref _initTimeTicks, now.Ticks);
                return true;
            }

            return false;
        }

        public void ResetError()
        {
            System.Threading.Interlocked.Exchange(ref _initTimeTicks, DateTime.Now.Ticks - _ticksInterval - 1);
        }
    }
}
