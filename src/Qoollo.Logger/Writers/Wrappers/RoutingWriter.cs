using Qoollo.Logger.Common;
using Qoollo.Logger.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Qoollo.Logger.Writers
{
    /// <summary>
    /// RoutingWriter. Ресурс для сложной обработки логирующийся сообщений - распределение по нескольким ресурсам или их маршрутизация
    /// </summary>
    internal class RoutingWriter : ILoggingEventWriter
    {
        private static readonly Logger _thisClassSupportLogger = InnerSupportLogger.Instance.GetClassLogger(typeof(RoutingWriter));

        private List<ILoggingEventWriter> _all;
        private List<ILoggingEventWriter> _others;
        private Dictionary<string, List<ILoggingEventWriter>> _routingLoggers;

        private volatile bool _isDisposed = false;


        public RoutingWriter(RoutingWrapperConfiguration config, 
            Dictionary<string, List<ILoggingEventWriter>> routingWriters,
            IEnumerable<ILoggingEventWriter> others, IEnumerable<ILoggingEventWriter> all)
        {
            Contract.Requires(config != null);
            Contract.Requires(routingWriters != null);

            _routingLoggers = routingWriters.ToDictionary(o => o.Key, o => new List<ILoggingEventWriter>(o.Value));
            
            if (others != null)
                _others = new List<ILoggingEventWriter>(others);
            else
                _others = new List<ILoggingEventWriter>();

            if (all != null)
                _all = new List<ILoggingEventWriter>(all);
            else
                _all = new List<ILoggingEventWriter>();
        }

        public RoutingWriter(RoutingWrapperConfiguration config)
        {
            Contract.Requires(config != null);

            _routingLoggers = config.RoutingWriters.ToDictionary(pair => pair.Key, pair => pair.Value.Select(LoggerFactory.CreateWriter).ToList());
            _all = config.FromAll.Select(LoggerFactory.CreateWriter).ToList();
            _others = config.FromOthers.Select(LoggerFactory.CreateWriter).ToList();
        }


        public bool Write(LoggingEvent data)
        {
            if (_isDisposed)
            {
                _thisClassSupportLogger.Error("Attempt to write LoggingEvent in Disposed state");
                return false;
            }

            bool result = PrintAll(_all, data);

            var routingLoggers = _routingLoggers;
            
            if (routingLoggers != null)
            {
                List<ILoggingEventWriter> loggers;

                if (routingLoggers.TryGetValue(data.StackSources.Last(), out loggers))
                    return result && PrintAll(loggers, data);
                else
                    return result && PrintAll(_others, data);
            }

            return result;
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

        public void SetConverterFactory(LoggingEventConverters.ConverterFactory factory)
        {
            for (int i = 0; i < _all.Count; i++)
                _all[i].SetConverterFactory(factory);

            for (int i = 0; i < _others.Count; i++)
                _others[i].SetConverterFactory(factory);

            foreach (var elem in _routingLoggers)
            {
                foreach (var item in elem.Value)
                    item.SetConverterFactory(factory);
            }
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

                    for (int i = 0; i < _others.Count; i++)
                        _others[i].Dispose();

                    foreach (var elem in _routingLoggers)
                    {
                        foreach (var item in elem.Value)
                            item.Dispose();
                    }
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