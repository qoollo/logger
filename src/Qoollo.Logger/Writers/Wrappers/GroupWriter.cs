using Qoollo.Logger.Common;
using Qoollo.Logger.Configuration;
using Qoollo.Logger.LoggingEventConverters;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Qoollo.Logger.Writers
{
    /// <summary>
    /// GroupWriter. Ресурс для отправки логирующийся сообщений в несколько целей
    /// </summary>
    internal class GroupWriter : ILoggingEventWriter
    {
        private static readonly Logger _thisClassSupportLogger = InnerSupportLogger.Instance.GetClassLogger(typeof(GroupWriter));

        private List<ILoggingEventWriter> _all;
        private volatile bool _isDisposed = false;

        public GroupWriter(GroupWrapperConfiguration config, IEnumerable<ILoggingEventWriter> loggers)
        {
            Contract.Requires(config != null);
            Contract.Requires(loggers != null);

            _all = new List<ILoggingEventWriter>(loggers);
        }

        public GroupWriter(GroupWrapperConfiguration config)
            : this(config, config.InnerWriters.Select(LoggerFactory.CreateWriter))
        {
        }


        public bool Write(LoggingEvent data)
        {
            if (_isDisposed)
            {
                _thisClassSupportLogger.Error("Попытка записи логирующего сообщения при освобожденных ресурсах");
                return false;
            }

            return PrintAll(_all, data);
        }

        private static bool PrintAll(List<ILoggingEventWriter> loggers, LoggingEvent data)
        {
            if (loggers == null)
                return false;

            bool result = true;

            for (int i = 0; i < loggers.Count; i++)
                result = loggers[i].Write(data) && result;

            return result;
        }

        public void SetConverterFactory(ConverterFactory factory)
        {
            for (int i = 0; i < _all.Count; i++)
                _all[i].SetConverterFactory(factory);
        }



        protected virtual void Dispose(bool isUserCall)
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
                if (isUserCall)
                {
                    for (int i = 0; i < _all.Count; i++)
                        _all[i].Dispose();
                }
            }
        }


        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}