using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Threading;

namespace Qoollo.Logger.Writers.Wrappers.Helpers.TemporaryStore
{
    internal class TemporaryStoreBase
    {
        private static readonly Logger _thisClassSupportLogger = InnerSupportLogger.Instance.GetClassLogger(typeof(TemporaryStoreBase));

        /// <summary>
        /// Состояние файла
        /// </summary>
        protected enum StoreFileState: byte
        {
            Active,
            Completed,
            Corrupted
        }

        /// <summary>
        /// Заголовок файла
        /// </summary>
        protected struct FileHeader
        {
            public const int SerializedSize = 9;

            public FileHeader(long activeIndex, StoreFileState fileState)
            {
                ActiveIndex = activeIndex;
                FileState = fileState;
            }

            public long ActiveIndex;
            public StoreFileState FileState;

            public bool IsValid
            {
                get { return ActiveIndex >= 0; }
            }

            public byte[] Serialize()
            {
                byte[] res = new byte[SerializedSize];
                res[0] = (byte)FileState;
                Array.Copy(BitConverter.GetBytes(ActiveIndex), 0, res, 1, 8);
                return res;
            }

            public static FileHeader Deserialize(byte[] array)
            {
                return new FileHeader(BitConverter.ToInt64(array, 1), (StoreFileState)array[0]);
            }
        }

        /// <summary>
        /// Заголовок отдельной записи
        /// </summary>
        protected struct RecordHeader
        {
            public const int SerializedSize = 4;

            public RecordHeader(long index, int size)
            {
                Index = index;
                Size = size;
            }

            public long Index;
            public int Size;

            public bool IsValid
            {
                get { return Size >= 0 && Index > 0; }
            }

            public bool IsWriteCompleted
            {
                get { return Size > 0; }
            }


            public static RecordHeader Invalid
            {
                get { return new RecordHeader(-1, -1); }
            }

            public static RecordHeader DuringWrite
            {
                get { return new RecordHeader(0, 0); }
            }

            public byte[] Serialize()
            {
                return BitConverter.GetBytes(Size);
            }

            public static RecordHeader Deserialize(long index, byte[] array)
            {
                return new RecordHeader(index, BitConverter.ToInt32(array, 0));
            }
        }


        /// <summary>
        /// Преобразование индекса файла в название
        /// </summary>
        protected static string StoreIndexToName(int index)
        {
            return "relLog_" + index.ToString();
        }

        /// <summary>
        /// Преобразование имени файла в индекс (-1, если некорректное название)
        /// </summary>
        protected static int StoreNameToIndex(string name)
        {
            if (!name.StartsWith("relLog_"))
                return -1;

            int result = -1;
            if (!int.TryParse(name.Substring(7), out result))
                return -1;

            return result;
        }


        // ============== FileHeader =================

        /// <summary>
        /// Захват блокировки на заголовок файла
        /// </summary>
        /// <param name="stream">Файл</param>
        private static void AcquireHeaderLock(FileStream stream)
        {
            Contract.Requires(stream != null);

            SpinWait sw = new SpinWait();
            while (true)
            {
                try
                {
                    stream.Lock(0, FileHeader.SerializedSize);
                    break;
                }
                catch (IOException) { }

                sw.SpinOnce();
            }
        }
        /// <summary>
        /// Освобождение блокировки на заголовке файла
        /// </summary>
        /// <param name="stream">Файл</param>
        private static void ReleaseHeaderLock(FileStream stream)
        {
            Contract.Requires(stream != null);

            stream.Unlock(0, FileHeader.SerializedSize);
        }

        /// <summary>
        /// Прочитать заголовок файла (позиция в файле сохраняется)
        /// </summary>
        protected static FileHeader AtomicReadFileHeader(FileStream stream)
        {
            Contract.Requires(stream != null);

            long initPos = stream.Position;
            byte[] record = new byte[FileHeader.SerializedSize];

            FileHeader result = new FileHeader(-1, StoreFileState.Active);

            try
            {
                AcquireHeaderLock(stream);

                stream.Seek(0, SeekOrigin.Begin);
                stream.Flush();
                if (stream.Read(record, 0, FileHeader.SerializedSize) == FileHeader.SerializedSize)
                    result = FileHeader.Deserialize(record);
            }
            finally
            {
                ReleaseHeaderLock(stream);
            }

            stream.Seek(initPos, SeekOrigin.Begin);

            return result;
        }


        /// <summary>
        /// Изменить состояние файла. Позиция в файле сохраняется
        /// </summary>
        protected static void AtomicChangeStoreState(FileStream stream, StoreFileState newState)
        {
            Contract.Requires(stream != null);

            long initPos = stream.Position;
            byte[] data = new byte[1];
            data[0] = (byte)newState;

            try
            {
                AcquireHeaderLock(stream);

                stream.Seek(0, SeekOrigin.Begin);
                stream.Write(data, 0, data.Length);
                stream.Flush(true);
            }
            finally
            {
                ReleaseHeaderLock(stream);
            }

            stream.Seek(initPos, SeekOrigin.Begin);
        }

        /// <summary>
        /// Изменить индекс активной записи в файле
        /// </summary>
        /// <param name="stream">Файл</param>
        /// <param name="recordIndex">Индекс записи</param>
        /// <param name="doFlush">Делать ли сброс на диск</param>
        protected static void AtomicChangeActiveRecord(FileStream stream, long recordIndex, bool doFlush = false)
        {
            Contract.Requires(stream != null);

            long initPos = stream.Position;
            byte[] data = BitConverter.GetBytes(recordIndex);

            try
            {
                AcquireHeaderLock(stream);

                stream.Seek(1, SeekOrigin.Begin);
                stream.Write(data, 0, data.Length);
                stream.Flush(doFlush);
            }
            finally
            {
                ReleaseHeaderLock(stream);
            }

            stream.Seek(initPos, SeekOrigin.Begin);
        }


        // ===================================

        /// <summary>
        /// Прочитать заголовок записи (RecordHeader.Invalid в случае ошибки)
        /// </summary>
        protected static RecordHeader ReadRecordHeader(FileStream stream)
        {
            Contract.Requires(stream != null);

            long initPos = stream.Position;
            byte[] array = new byte[RecordHeader.SerializedSize];
            if (stream.Read(array, 0, RecordHeader.SerializedSize) < RecordHeader.SerializedSize)
                return RecordHeader.Invalid;

            return RecordHeader.Deserialize(initPos, array);
        }

        /// <summary>
        /// Записать заголовок записи
        /// </summary>
        protected static void WriteRecordHeader(FileStream stream, RecordHeader header)
        {
            Contract.Requires(stream != null);

            byte[] array = header.Serialize();
            stream.Write(array, 0, array.Length);
        }

        /// <summary>
        /// Перейти к активной записи в файле
        /// </summary>
        protected static long SeekToLastActiveRecord(FileStream stream)
        {
            Contract.Requires(stream != null);

            stream.Position = 0;

            long recordIndex = AtomicReadFileHeader(stream).ActiveIndex;
            if (recordIndex < 0)
            {
                stream.Seek(FileHeader.SerializedSize, SeekOrigin.Begin);
                return stream.Position;
            }
            if (!SeekToRecord(stream, recordIndex))
                return -1;

            return recordIndex;
        }


        /// <summary>
        /// Перейти к указанной записи
        /// </summary>
        protected static bool SeekToRecord(FileStream stream, long recordIndex)
        {
            Contract.Requires(stream != null);

            return stream.Seek(recordIndex, SeekOrigin.Begin) == recordIndex;
        }

        /// <summary>
        /// Перейти к следующей записи за указанной
        /// </summary>
        protected static bool SeekToNextRecord(FileStream stream, long currentIndex)
        {
            Contract.Requires(stream != null);

            stream.Seek(currentIndex, SeekOrigin.Begin);
            var header = ReadRecordHeader(stream);

            if (!header.IsValid)
                return false;

            stream.Seek(header.Size, SeekOrigin.Current);
            return true;
        }


        // ============================

        /// <summary>
        /// Проверка валидности хранилища
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        protected static bool CheckTemporaryStore(FileStream stream)
        {
            Contract.Requires(stream != null);

            long initPos = stream.Position;

            try
            {
                stream.Position = 0;

                var fileHeader = AtomicReadFileHeader(stream);
                if (!fileHeader.IsValid || fileHeader.FileState == StoreFileState.Corrupted)
                    return false;

                stream.Position = FileHeader.SerializedSize;

                while (stream.Position < fileHeader.ActiveIndex && stream.Position < stream.Length)
                {
                    var header = ReadRecordHeader(stream);
                    if (!header.IsWriteCompleted)
                        return false;

                    long startPos = stream.Position;
                    stream.Seek(header.Size, SeekOrigin.Current);
                    if (startPos + header.Size != stream.Position)
                        return false;
                }

                if (stream.Position != fileHeader.ActiveIndex)
                    return false;

                while (stream.Position < stream.Length)
                {
                    var header = ReadRecordHeader(stream);
                    if (!header.IsValid)
                        return false;

                    if (!header.IsWriteCompleted)
                        return fileHeader.FileState == StoreFileState.Active;

                    long startPos = stream.Position;
                    stream.Seek(header.Size, SeekOrigin.Current);
                    if (startPos + header.Size != stream.Position)
                        return false;
                }
            }
            catch (IOException ex)
            {
                _thisClassSupportLogger.ErrorFormat(ex, "Reliable log storage file check failed for \'{0}\'. This file will be deleted (some logs could be lost).", stream.Name);
            }
            finally
            {
                stream.Position = initPos;
            }

            return true;
        }


        /// <summary>
        /// Удалить хранилище
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Успешность</returns>
        protected static bool DeleteStore(string fileName)
        {
            try
            {
                File.Delete(fileName);
                return true;
            }
            catch (IOException)
            {
            }

            return false;
        }



        /// <summary>
        /// Создать новое пустое хранилище
        /// </summary>
        /// <param name="folder">Директория</param>
        /// <param name="index">Индекс</param>
        /// <returns>Файл созданного хранилища</returns>
        protected static FileStream CreateStorage(string folder, int index)
        {
            string tmpFile = Path.Combine(folder, "new_" + StoreIndexToName(index) + ".tmp");
            string actFile = Path.Combine(folder, StoreIndexToName(index) + ".tmp");

            if (File.Exists(actFile))
                throw new ArgumentException("File is already existed: " + actFile);

            var stream = new FileStream(tmpFile, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.None);
            byte[] header = new FileHeader(FileHeader.SerializedSize, StoreFileState.Active).Serialize();
            stream.Write(header, 0, header.Length);
            stream.Close();

            File.Move(tmpFile, actFile);
            var res = new FileStream(actFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            res.Seek(FileHeader.SerializedSize, SeekOrigin.Begin);
            return res;
        }
    }
}
