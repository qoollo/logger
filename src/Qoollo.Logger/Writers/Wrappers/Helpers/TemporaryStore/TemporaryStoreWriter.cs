using Qoollo.Logger.Common;
using Qoollo.Logger.Helpers;
using System;
using System.Diagnostics.Contracts;
using System.IO;

namespace Qoollo.Logger.Writers.Wrappers.Helpers.TemporaryStore
{
    /// <summary>
    /// Писатель во временное хранилище
    /// </summary>
    internal class TemporaryStoreWriter : TemporaryStoreBase, IDisposable
    {
        private static readonly Logger _thisClassSupportLogger = InnerSupportLogger.Instance.GetClassLogger(typeof(TemporaryStoreWriter));

        private const int CountToFlush = 150;

        private readonly string _folderName;
        private readonly long _maxFileSize;

        private int _curStoreIndex;
        private FileStream _storeStream;

        private long _wirtedRecord;

        public TemporaryStoreWriter(string folderNameOfTemporaryStore, long maxFileSize)
        {
            Contract.Requires<ArgumentNullException>(folderNameOfTemporaryStore != null);

            _wirtedRecord = 0;
            _maxFileSize = maxFileSize;
            _folderName = folderNameOfTemporaryStore;
            _curStoreIndex = InitialProcessStorageFolderAndReturnLastIndex(folderNameOfTemporaryStore);
        }


        /// <summary>
        /// Записать событие лога в хранилище
        /// </summary>
        /// <param name="data">Данные</param>
        /// <returns>Успешность</returns>
        public bool Write(LoggingEvent data)
        {
            Contract.Requires(data != null);

            var curStream = GetStoreStream();

            if (curStream == null)
                return false;


            var bytes = Serializer.Serialize(data);
            var header = new RecordHeader(curStream.Position, bytes.Length);

            try
            {
                long pos = curStream.Position;
                WriteRecordHeader(curStream, RecordHeader.DuringWrite);
                curStream.Write(bytes, 0, bytes.Length);
                curStream.Flush();
                curStream.Seek(pos, SeekOrigin.Begin);
                WriteRecordHeader(curStream, header);
                curStream.Seek(0, SeekOrigin.End);
                curStream.Flush();

                _wirtedRecord++;
                if (_wirtedRecord % CountToFlush == 0 || data.Level >= LogLevel.Error)
                    curStream.Flush(true);

                return true;
            }
            catch (IOException ex)
            {
                _thisClassSupportLogger.Error(ex, "Ошибка записи во временное хранилище логов: " + _folderName);
            }
            catch (Exception ex)
            {
                _thisClassSupportLogger.Error(ex, "Непоправимая ошибка записи во временное хранилище логов: " + _folderName);
                throw;
            }

            return false;
        }


        private FileStream GetStoreStream()
        {
            try
            {
                if (_storeStream != null)
                {
                    if (_storeStream.Length < _maxFileSize)
                        return _storeStream;

                    var header = AtomicReadFileHeader(_storeStream);
                    if (header.FileState == StoreFileState.Active)
                        AtomicChangeStoreState(_storeStream, StoreFileState.Completed);

                    _storeStream.Close();
                    _storeStream = null;
                }


                _curStoreIndex++;
                _storeStream = CreateStorage(_folderName, _curStoreIndex);
            }
            catch (IOException ex)
            {
                _thisClassSupportLogger.Error(ex, "Ошибка записи во временное хранилище логов");
            }

            return _storeStream;
        }



        private static int InitialProcessStorageFolderAndReturnLastIndex(string folderName)
        {
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
                return 0;
            }

            var directory = new DirectoryInfo(folderName);
            var files = directory.GetFiles();

            int index = -1;

            if (files.Length != 0)
            {
                for (int i = 0; i < files.Length; i++)
                {
                    int curIndex = StoreNameToIndex(Path.GetFileNameWithoutExtension(files[i].Name));
                    if (curIndex < 0)
                        continue;

                    if (curIndex > index)
                        index = curIndex;

                    var stream = new FileStream(files[i].FullName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                    var header = AtomicReadFileHeader(stream);
                    if (header.FileState == StoreFileState.Active)
                        AtomicChangeStoreState(stream, StoreFileState.Completed);

                    stream.Close();
                }
            }

            return index;
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
