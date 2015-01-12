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
        class FileData
        {
            public FileData(FileStream file)
            {
                File = file;
                IsActive = true;
                LastAccess = DateTime.Now;
            }

            public DateTime LastAccess;
            public bool IsActive;
            public FileStream File;
        }

        private Dictionary<string, FileData> _files;
        private DateTime _lastScan;
        private TimeSpan _trimPeriod;
        private System.Threading.Timer _trimTimer;

        public FileWriterFilePool(TimeSpan trimPeriod)
        {
            _files = new Dictionary<string, FileData>();
            _trimPeriod = trimPeriod;
            _lastScan = DateTime.Now;

            _trimTimer = new System.Threading.Timer(TimerScanForOld, null, TimeSpan.Zero, trimPeriod);
        }

        /// <summary>
        /// Request FileStream from pool
        /// </summary>
        /// <param name="name">Путь до файла</param>
        /// <param name="releaseName">Имя файла для освобождения</param>
        /// <param name="streamToRelease">Старый файл для освобождения</param>
        /// <returns>Новый файл</returns>
        public FileStream RequestFile(string name, FileStream streamToRelease, string releaseName)
        {
            //name = Path.GetFileName(name);
            lock (_files)
            {
                FileData data = null;

                if (streamToRelease != null)
                {
                    releaseName = releaseName ?? streamToRelease.Name;
                    Contract.Assert(_files.ContainsKey(releaseName));

                    data = _files[releaseName];
                    Contract.Assert(object.ReferenceEquals(streamToRelease, data.File));

                    data.LastAccess = DateTime.Now;
                    data.IsActive = false;

                    if (!streamToRelease.CanWrite)
                    {
                        _files.Remove(releaseName);
                        streamToRelease.Close();
                    }
                }

                if (!_files.TryGetValue(name, out data))
                {
                    var newFile = CreateFile(name);
                    data = new FileData(newFile);
                    _files.Add(name, data);
                }

                data.LastAccess = DateTime.Now;
                data.IsActive = true;

                Contract.Assert(data.File.CanWrite);
                return data.File;
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
                elem.Value.File.Close();
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
                    elem.Value.File.Close();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
