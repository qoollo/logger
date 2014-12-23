using Qoollo.Logger.Common;
using Qoollo.Logger.Exceptions;
using Qoollo.Logger.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;

namespace Qoollo.Logger.Writers.Wrappers.Helpers.TemporaryStore
{
    /// <summary>
    /// Читатель из временного хранилища
    /// </summary>
    internal class TemporaryStoreReader : TemporaryStoreBase, IDisposable
    {
        private static readonly Logger _thisClassSupportLogger = InnerSupportLogger.Instance.GetClassLogger(typeof(TemporaryStoreReader));

        private struct RecordInfo
        {
            public RecordInfo(RecordHeader header, LoggingEvent data)
            {
                Header = header;
                Data = data;
            }

            public RecordHeader Header;
            public LoggingEvent Data;

            public bool IsValid
            {
                get { return Data != null && Header.IsValid; }
            }

            public static RecordInfo Invalid
            {
                get { return new RecordInfo(RecordHeader.Invalid, null); }
            }
        }


        private const int CompletedFlushPeriod = 150;

        private readonly string _folderName;

        private int _curStoreIndex;
        private FileStream _storeStream;

        private RecordInfo _cachedRecord;

        private int _completedCount = 0;

        public TemporaryStoreReader(string folderNameOfTemporaryStore)
        {
            Contract.Requires<ArgumentNullException>(folderNameOfTemporaryStore != null);

            _folderName = folderNameOfTemporaryStore;
            _curStoreIndex = -1;
            _cachedRecord = new RecordInfo(RecordHeader.Invalid, null);
        }

        /// <summary>
        /// Считать очередную запись из хранилища
        /// </summary>
        /// <returns></returns>
        public LoggingEvent GetRecord()
        {
            if (_cachedRecord.IsValid)
                return _cachedRecord.Data;

            try
            {
                FileStream curStream = GetStoreStream();

                if (curStream == null)
                    return null;

                bool isCorrupted = false;
                _cachedRecord = ReadRecord(curStream, out isCorrupted);
                if (isCorrupted)
                    PorcessCorruptedStore(curStream);

                return _cachedRecord.Data;
            }
            catch (IOException ex)
            {
                _thisClassSupportLogger.Error(ex, "Ошибка при считывании данных из временного хранилища логов");
            }

            return null;
        }

        /// <summary>
        /// Пометить, что текущая запись успешно обработана и можно перейтик следующей
        /// </summary>
        public void RecordCompleted()
        {
            Contract.Assert(_cachedRecord.IsValid);

            long newIndex = _cachedRecord.Header.Index + RecordHeader.SerializedSize + _cachedRecord.Header.Size;
            Contract.Assert(_storeStream.Position == newIndex);
            if (_storeStream.Position != newIndex)
                _storeStream.Position = newIndex;

            _cachedRecord = RecordInfo.Invalid;

            _completedCount++;

            try
            {
                AtomicChangeActiveRecord(_storeStream, newIndex, _completedCount % CompletedFlushPeriod == 0);
            }
            catch (IOException ex)
            {
                _thisClassSupportLogger.Error(ex, "Ошибка при переходе на следующую запись во временном хранилище логов");
            }
        }


        private void PorcessCorruptedStore(FileStream stream)
        {
            Contract.Assert(_storeStream == stream);

            _storeStream = null;
            ProcessCorruptedStorage(stream);
        }

        private static RecordInfo ReadRecord(FileStream stream, out bool isCorrupted)
        {
            isCorrupted = false;
            if (stream.Position == stream.Length)
                return RecordInfo.Invalid;

            long initPos = stream.Position;

            try
            {
                var recordHeader = ReadRecordHeader(stream);
                if (!recordHeader.IsValid)
                {
                    isCorrupted = true;
                    return RecordInfo.Invalid;
                }

                if (!recordHeader.IsWriteCompleted)
                {
                    stream.Seek(initPos, SeekOrigin.Begin);
                    return RecordInfo.Invalid;
                }

                var record = new byte[recordHeader.Size];
                if (stream.Read(record, 0, record.Length) != record.Length)
                {
                    isCorrupted = true;
                    return RecordInfo.Invalid;
                }

                return new RecordInfo(recordHeader, Serializer.Deserialize(record));
            }
            catch (LoggerSerializationException ex)
            {
                _thisClassSupportLogger.ErrorFormat(ex,
                    "Ошибка при считывании данных из временного хранилища \'{0}\'. Хранилище будет удалено.", stream.Name);
            }
            catch (IOException ex)
            {
                _thisClassSupportLogger.ErrorFormat(ex,
                    "Ошибка при считывании данных из временного хранилища \'{0}\'. Хранилище будет удалено.", stream.Name);
            }

            isCorrupted = true;
            return RecordInfo.Invalid;
        }


        private FileStream GetStoreStream()
        {
            if (_storeStream != null)
            {
                if (_storeStream.Position < _storeStream.Length)
                    return _storeStream;

                var header = AtomicReadFileHeader(_storeStream);
                if (header.FileState == StoreFileState.Active)
                    return _storeStream;

                var name = _storeStream.Name;
                _storeStream.Close();
                _storeStream = null;
                DeleteStore(name);
            }


            _storeStream = OpenNextStorage(_folderName, _curStoreIndex);

            if (_storeStream != null)
            {
                SeekToLastActiveRecord(_storeStream);

                int storeId = StoreNameToIndex(Path.GetFileNameWithoutExtension(_storeStream.Name));
                Contract.Assert(storeId >= 0);
                _curStoreIndex = storeId;
            }


            return _storeStream;
        }



        private static FileStream OpenNextStorage(string folderName, int curStoreIndex)
        {
            List<FileInfo> nextStorages = null;
            FileStream stream = null;

            if (Directory.Exists(folderName))
            {
                var directory = new DirectoryInfo(folderName);
                var files = directory.GetFiles();

                if (files.Length != 0)
                {
                    nextStorages = files.Where(o => StoreNameToIndex(Path.GetFileNameWithoutExtension(o.Name)) > curStoreIndex).ToList();
                    if (nextStorages.Count == 0)
                        return null;

                    nextStorages.Sort((a, b) => StoreNameToIndex(Path.GetFileNameWithoutExtension(a.Name)).CompareTo(StoreNameToIndex(Path.GetFileNameWithoutExtension(b.Name))));

                    for (int i = 0; i < nextStorages.Count; i++)
                    {
                        stream = new FileStream(nextStorages[i].FullName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                        stream.Seek(FileHeader.SerializedSize, SeekOrigin.Begin);
                        if (CheckTemporaryStore(stream))
                            break;

                        ProcessCorruptedStorage(stream);
                        stream = null;
                    }
                }
            }

            return stream;
        }


        private static void ProcessCorruptedStorage(FileStream stream)
        {
            AtomicChangeStoreState(stream, StoreFileState.Corrupted);
            string name = stream.Name;
            stream.Close();
            DeleteStore(name);
        }


        protected virtual void Dispose(bool isUserCall)
        {
            if (isUserCall)
            {
                if (_storeStream != null)
                {
                    _storeStream.Close();
                    _storeStream = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
