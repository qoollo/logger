using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qoollo.Logger.Writers.RealWriters.Helpers
{
    /// <summary>
    /// Пул открытых файлов для FileWriter
    /// </summary>
    internal class FileWriterFilePool : IDisposable
    {
        public struct FileStreamAndWriter
        {
            private readonly FileStream _file;
            private readonly StreamWriter _writer;

            public FileStreamAndWriter(FileStream file, StreamWriter writer)
            {
                Contract.Requires(file != null);
                Contract.Requires(writer != null);

                _file = file;
                _writer = writer;
            }

            public FileStream File { get { return _file; } }
            public StreamWriter Writer { get { return _writer; } }

            public bool IsInitialized { get { return _file != null; } }

            public void WriteLine(string line)
            {
                _writer.WriteLine(line);
            }

            public void Flush(bool onDisk)
            {
                _writer.Flush();
                if (onDisk)
                    _file.Flush(onDisk);
            }
        }

        private class FileData: IDisposable
        {
            public FileData(FileStream file, Encoding encoding)
            {
                Contract.Requires(file != null);
                Contract.Requires(encoding != null);

                File = file;
                Writer = new StreamWriter(file, encoding);
                IsActive = true;
                LastAccess = DateTime.Now;
            }

            public DateTime LastAccess;
            public bool IsActive;
            public FileStream File;
            public StreamWriter Writer;

            public FileStreamAndWriter GetFileStreamAndWriter()
            {
                return new FileStreamAndWriter(this.File, this.Writer);
            }

            /// <summary>
            /// Flush and close file
            /// </summary>
            public void Dispose()
            {
                Writer.Flush();
                File.Close();
            }
        }


        // =============


        private Dictionary<string, FileData> _files;
        private Encoding _encoding;
        private DateTime _lastScan;
        private TimeSpan _trimPeriod;
        private System.Threading.Timer _trimTimer;

        public FileWriterFilePool(TimeSpan trimPeriod, Encoding encoding)
        {
            Contract.Requires(trimPeriod > TimeSpan.Zero);
            Contract.Requires(encoding != null);

            _files = new Dictionary<string, FileData>();
            _encoding = encoding;
            _trimPeriod = trimPeriod;
            _lastScan = DateTime.Now;

            _trimTimer = new System.Threading.Timer(TimerScanForOld, null, TimeSpan.Zero, trimPeriod);
        }

        /// <summary>
        /// Request FileStream and StreamWriter from pool
        /// </summary>
        /// <param name="name">Path to the file</param>
        /// <param name="streamToRelease">Previous stream that should be released</param>
        /// <param name="releaseName">Name of previously requested stream</param>
        /// <returns>Requested stream with writer</returns>
        public FileStreamAndWriter RequestFile(string name, FileStreamAndWriter streamToRelease, string releaseName)
        {
            //name = Path.GetFileName(name);
            lock (_files)
            {
                FileData data = null;

                if (streamToRelease.IsInitialized)
                {
                    releaseName = releaseName ?? streamToRelease.File.Name;
                    Contract.Assert(_files.ContainsKey(releaseName));

                    data = _files[releaseName];
                    Contract.Assert(object.ReferenceEquals(streamToRelease.File, data.File));
                    Contract.Assert(object.ReferenceEquals(streamToRelease.Writer, data.Writer));

                    data.LastAccess = DateTime.Now;
                    data.IsActive = false;

                    if (!streamToRelease.File.CanWrite)
                    {
                        _files.Remove(releaseName);
                        streamToRelease.Flush(false);
                        streamToRelease.File.Close();
                    }
                }

                if (!_files.TryGetValue(name, out data))
                {
                    var newFile = CreateFile(name);
                    data = new FileData(newFile, _encoding);
                    _files.Add(name, data);
                }

                data.LastAccess = DateTime.Now;
                data.IsActive = true;

                Contract.Assert(data.File.CanWrite);
                return data.GetFileStreamAndWriter();
            }
        }

        private FileStream CreateFile(string name)
        {
            if (!File.Exists(name))
            {
                var folder = Path.GetDirectoryName(name);

                if (!string.IsNullOrEmpty(folder))
                    Directory.CreateDirectory(folder);
            }

            return new FileStream(name, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
        }


        private void TimerScanForOld(object state)
        {
            if (DateTime.Now - _lastScan > _trimPeriod)
            {
                _lastScan = DateTime.Now;
                ScanForOld();
            }
        }

        private void ScanForOld()
        {
            List<KeyValuePair<string, FileData>> forDetele = new List<KeyValuePair<string, FileData>>(_files.Count);

            lock (_files)
            {
                DateTime curDt = DateTime.Now;
                foreach (var elem in _files)
                {
                    if ((!elem.Value.IsActive && (elem.Value.LastAccess - curDt > _trimPeriod)) || !elem.Value.File.CanWrite)
                        forDetele.Add(elem);
                }

                foreach (var elem in forDetele)
                    _files.Remove(elem.Key);
            }

            foreach (var elem in forDetele)
                elem.Value.Dispose();
        }


        protected void Dispose(bool isUserCall)
        {
            if (_trimTimer != null)
                _trimTimer.Dispose();

            List<KeyValuePair<string, FileData>> forDetele = null;

            lock (_files)
            {
                forDetele = new List<KeyValuePair<string, FileData>>(_files);
                _files.Clear();
            }

            if (isUserCall)
            {
                foreach (var elem in forDetele)
                    elem.Value.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
