using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qoollo.Logger.Writers.RealWriters.Helpers
{
    /// <summary>
    /// Simple JSON writer for Logstash writer
    /// </summary>
    internal class JsonSimpleWriter
    {
        private StringBuilder _sb;
        public JsonSimpleWriter()
        {
            _sb = new StringBuilder();
        }
        public JsonSimpleWriter(int capacity)
        {
            _sb = new StringBuilder(capacity);
        }
        public JsonSimpleWriter(StringBuilder sb)
        {
            Contract.Requires(sb != null);
            _sb = sb;
        }

        public StringBuilder GetStringBuilder()
        {
            return _sb;
        }

        public override string ToString()
        {
            return _sb.ToString();
        }


        private void AppendJsonText(string text, bool screen = true)
        {
            Contract.Requires(text != null);

            int screenStartPosition = _sb.Length;
            _sb.Append(text);
            if (screen)
            {
                _sb.Replace(Environment.NewLine, @"\n", screenStartPosition, _sb.Length - screenStartPosition);
                _sb.Replace("\n", @"\n", screenStartPosition, _sb.Length - screenStartPosition);
                _sb.Replace("\r", @"", screenStartPosition, _sb.Length - screenStartPosition);
                _sb.Replace("\t", @"\t", screenStartPosition, _sb.Length - screenStartPosition);
                _sb.Replace("\\", @"\\", screenStartPosition, _sb.Length - screenStartPosition);
                _sb.Replace("\'", @"\'", screenStartPosition, _sb.Length - screenStartPosition);
                _sb.Replace("\"", "\\\"", screenStartPosition, _sb.Length - screenStartPosition);
            }
        }
        private void RemoveEndingComma()
        {
            if (_sb.Length == 0)
                return;

            if (_sb[_sb.Length - 1] == ',')
                _sb.Remove(_sb.Length - 1, 1);
        }

        public JsonSimpleWriter AppendJsonKey(string key)
        {
            Contract.Requires(key != null);

            _sb.Append("\"").Append(key).Append("\"").Append(":");
            return this;
        }
        public JsonSimpleWriter AppendJsonValue(string value, bool screen = true, bool withComma = true)
        {
            Contract.Requires(value != null);

            _sb.Append("\"");
            this.AppendJsonText(value, screen);
            _sb.Append("\"");

            if (withComma)
                _sb.Append(",");

            return this;
        }

        public JsonSimpleWriter AppendJsonBeginObject()
        {
            _sb.Append("{");
            return this;
        }
        public JsonSimpleWriter AppendJsonEndObject(bool withComma = true, bool removePrevComma = true)
        {
            if (removePrevComma)
                this.RemoveEndingComma();
            _sb.Append("}");
            if (withComma)
                _sb.Append(",");
            return this;
        }

        public JsonSimpleWriter AppendJsonBeginList()
        {
            _sb.Append("[");
            return this;
        }
        public JsonSimpleWriter AppendJsonEndList(bool withComma = true, bool removePrevComma = true)
        {
            if (removePrevComma)
                this.RemoveEndingComma();
            _sb.Append("]");
            if (withComma)
                _sb.Append(",");
            return this;
        }



        public JsonSimpleWriter AppendJsonParamConditional(string key, string value, bool screen = true, bool withComma = true)
        {
            Contract.Requires(key != null);

            if (value == null)
                return this;

            this.AppendJsonKey(key);
            this.AppendJsonValue(value, screen, withComma);

            return this;
        }
        public JsonSimpleWriter AppendJsonParamConditional(string key, List<string> values, bool screen = true, bool withComma = true)
        {
            Contract.Requires(key != null);

            if (values == null)
                return this;

            this.AppendJsonKey(key);
            this.AppendJsonBeginList();
            for (int i = 0; i < values.Count; i++)
                if (values[i] != null)
                    this.AppendJsonValue(values[i], screen, withComma: true);
            this.AppendJsonEndList(withComma: withComma);

            return this;
        }
    }
}
