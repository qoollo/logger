using Qoollo.Logger.Common;
using Qoollo.Logger.Configuration;
using Qoollo.Logger.LoggingEventConverters;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qoollo.Logger.Writers
{
    internal class PatternMatchingWriter : ILoggingEventWriter
    {
        private static readonly Logger _thisClassSupportLogger = InnerSupportLogger.Instance.GetClassLogger(typeof(PatternMatchingWriter));

        private readonly Dictionary<string, ILoggingEventWriter> _match;
        private readonly ILoggingEventWriter _default;
        private readonly string _matchTemplate;
        private LoggingEventConverterBase _templateConverter;

        private volatile bool _isDisposed = false;


        public PatternMatchingWriter(PatternMatchingWrapperConfiguration config, 
            Dictionary<string, ILoggingEventWriter> matchWriters, ILoggingEventWriter defaultWriter)
        {
            Contract.Requires(config != null);
            Contract.Requires(matchWriters != null);

            _match = new Dictionary<string,ILoggingEventWriter>(matchWriters);
            _default = defaultWriter;
            _matchTemplate = config.Pattern;
            _templateConverter = TemplateParser.Parse(_matchTemplate, ConverterFactory.Default);
        }

        public PatternMatchingWriter(PatternMatchingWrapperConfiguration config)
        {
            Contract.Requires(config != null);

            _match = config.MatchWriters.ToDictionary(o => o.Key, o => LoggerFactory.CreateWriter(o.Value));
            if (config.DefaultWriter != null)
                _default = LoggerFactory.CreateWriter(config.DefaultWriter);
            _matchTemplate = config.Pattern;
            _templateConverter = TemplateParser.Parse(_matchTemplate, ConverterFactory.Default);
        }


        public bool Write(LoggingEvent data)
        {
            if (_isDisposed)
            {
                _thisClassSupportLogger.Error("Attempt to write LoggingEvent in Disposed state");
                return false;
            }

            string pattern = _templateConverter.Convert(data);

            ILoggingEventWriter curWriter = null;
            if (pattern == null || !_match.TryGetValue(pattern, out curWriter))
                curWriter = _default;

            if (curWriter != null)
                return curWriter.Write(data);

            return true;
        }


        public void SetConverterFactory(LoggingEventConverters.ConverterFactory factory)
        {
            _templateConverter = TemplateParser.Parse(_matchTemplate, factory);

            foreach (var elem in _match)
                elem.Value.SetConverterFactory(factory);

            if (_default != null)
                _default.SetConverterFactory(factory);
        }



        protected virtual void Dispose(DisposeReason reason)
        {
            if (!_isDisposed)
            {
                _isDisposed = true;

                if (reason == DisposeReason.Dispose)
                {
                    foreach (var elem in _match)
                        elem.Value.Dispose();

                    if (_default != null)
                        _default.Dispose();
                }
                else if (reason == DisposeReason.Close)
                {
                    foreach (var elem in _match)
                        elem.Value.Close();

                    if (_default != null)
                        _default.Close();
                }
            }
        }


        public void Close()
        {
            Dispose(DisposeReason.Close);
            GC.SuppressFinalize(this);
        }
        public void Dispose()
        {
            Dispose(DisposeReason.Dispose);
            GC.SuppressFinalize(this);
        }
    }
}
