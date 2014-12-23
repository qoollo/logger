using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qoollo.Logger.Writers.Wrappers.Helpers.TemporaryStore
{
    internal class TemporaryStoreLockFile
    {
        public const string LockFileName = ".lock";
        private const string SubDirectoryPrefix = "sub_";
        private const uint HRFileLocked = 0x80070020;
        private const uint HRPortionOfFileLocked = 0x80070021;

        private static readonly Logger _thisClassSupportLogger = InnerSupportLogger.Instance.GetClassLogger(typeof(TemporaryStoreWriter));

        /// <summary>
        /// Связано ли исключение с наличием блокировки на файле
        /// </summary>
        /// <param name="e">Исключение</param>
        /// <returns>Заблокирован ли файл</returns>
        private static bool IsExceptionForFileLocked(IOException e)
        {
            var errorCode = (uint)System.Runtime.InteropServices.Marshal.GetHRForException(e);
            return errorCode == HRFileLocked || errorCode == HRPortionOfFileLocked;
        }

        /// <summary>
        /// Заблокирована ли директория
        /// </summary>
        /// <param name="path">Путь до директории</param>
        /// <returns>Заблокирована ли</returns>
        public static bool IsDirectoryLocked(string path)
        {
            if (!Directory.Exists(path))
                return false;

            string fileName = Path.Combine(path, LockFileName);

            if (!File.Exists(fileName))
                return false;

            try
            {
                using (File.Open(fileName, FileMode.Open)) { }
                return false;
            }
            catch (IOException e)
            {
                if (!IsExceptionForFileLocked(e))
                    throw;

                return true;
            }
        }

        /// <summary>
        /// Попытаться захватить блокировку на директории
        /// </summary>
        /// <param name="path">Путь до директории</param>
        /// <param name="dirLockObj">Файловый поток, удерживающий блокировку</param>
        /// <returns>Удалось ли заблокировать</returns>
        public static bool TryAcquireDirectoryLock(string path, out Stream dirLockObj)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string fileName = Path.Combine(path, LockFileName);

            try
            {
                dirLockObj = File.Open(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                return true;
            }
            catch (IOException e)
            {
                if (!IsExceptionForFileLocked(e))
                    throw;

                dirLockObj = null;
                return false;
            }
        }


        /// <summary>
        /// Выполить поиск незаблокированной директории. 
        /// Сначала используется корневая, затем создаются поддиректории.
        /// </summary>
        /// <param name="path">Путь до директории</param>
        /// <param name="dirLockObj">Файловый поток, удерживающий блокировку</param>
        /// <returns>Путь до заблокированной директории</returns>
        public static string FindNotLockedDirectory(string path, out Stream dirLockObj)
        {
            if (TryAcquireDirectoryLock(path, out dirLockObj))
                return path;

            _thisClassSupportLogger.WarnFormat("Не получается заблокировать директорию для временного хранения логов: {0}. Вместо неё будет использована поддиректория.", path);

            DirectoryInfo rootDirInfo = new DirectoryInfo(path);
            int maxIndex = 0;
            int curIndex = 0;

            foreach (var subDir in rootDirInfo.GetDirectories(SubDirectoryPrefix + "*"))
            {
                if (!int.TryParse(subDir.Name.Substring(SubDirectoryPrefix.Length), out curIndex))
                    continue;

                maxIndex = Math.Max(maxIndex, curIndex);

                if (TryAcquireDirectoryLock(subDir.FullName, out dirLockObj))
                    return subDir.FullName;
            }

            string finalDir = Path.Combine(rootDirInfo.FullName, SubDirectoryPrefix + (maxIndex + 1).ToString());
            if (!TryAcquireDirectoryLock(finalDir, out dirLockObj))
            {
                _thisClassSupportLogger.Fatal("Can't acquire direcotry lock. Probably due to concurrect access. Directory: " + finalDir);
                throw new IOException("Can't acquire direcotry lock. Probably due to concurrect access. Directory: " + finalDir);
            }

            return finalDir;
        }
    }
}
