using Qoollo.Logger.Common;
using Qoollo.Logger.Configuration;
using Qoollo.Logger.Exceptions;
using Qoollo.Logger.Helpers;
using Qoollo.Logger.LoggingEventConverters;
using Qoollo.Logger.Writers.RealWriters.Helpers;
using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Security;
using System.Text;

namespace Qoollo.Logger.Writers
{
    /// <summary>
    /// FileWriter. Ресурс для записи сообщений в файл
    /// </summary>
    internal class FileWriter : Writer
    {
        private static readonly Logger _thisClassSupportLogger = InnerSupportLogger.Instance.GetClassLogger(typeof(FileWriter));

        private readonly Encoding _encoding;
        private readonly bool _isNeedFileRotate;

        private readonly string _rawFilename;
        private LoggingEventConverterBase _filenameConverter;

        private readonly string _rawTemplate;
        private LoggingEventConverterBase _templateConverter;

        private string _lastFilename;
        private FileWriterFilePool.FileStreamAndWriter _fileStreamAndWriter;
        private FileWriterFilePool _filePool;

        protected readonly object _lockWrite = new object();

        private readonly LogLevel _logLevel;

        private ErrorTimeTracker _errorTracker = new ErrorTimeTracker(TimeSpan.FromMinutes(5));

        private volatile bool _isDisposed = false;


        public FileWriter(FileWriterConfiguration config)
            : base(config.Level)
        {
            Contract.Requires(config != null);

            _logLevel = config.Level;
            _encoding = config.Encoding;
            _rawTemplate = config.Template;
            _isNeedFileRotate = config.IsNeedFileRotate;
            _rawFilename = config.FileNameTemplate;
          
            _filePool = new FileWriterFilePool(TimeSpan.FromMinutes(10), config.Encoding);
            SetConverterFactory(ConverterFactory.Default);
        }


        public override bool Write(LoggingEvent data)
        {
            if (_isDisposed)
            {
                if (_errorTracker.CanWriteErrorGetAndUpdate())
                    _thisClassSupportLogger.Error("Attempt to write LoggingEvent in Disposed state");

                return false;
            }

            if (data.Level < _logLevel)
                return true;

            var line = _templateConverter.Convert(data) ?? "";

            // lock именно здесь так как в зависимости от сообщения может генериться разное имя файла
            // это не страшно, но на это имя файла может открыться writer в другой файл - а это уже конец
            lock (_lockWrite)
            {
                if (_isDisposed)
                    return false;

                if (!RefreshWriter(data))
                    return false;

                try
                {
                    _fileStreamAndWriter.WriteLine(line);
                    if (data.Level >= LogLevel.Info)
                    {
                        bool flushToDisk = data.Level >= LogLevel.Fatal;
                        _fileStreamAndWriter.Flush(flushToDisk);
                    }

                    return true;
                }
                catch (IOException ex)
                {
                    if (_errorTracker.CanWriteErrorGetAndUpdate())
                        _thisClassSupportLogger.Error(ex, "Error writing LoggingEvent to file: " + _lastFilename);
                }
                catch (Exception ex)
                {
                    _thisClassSupportLogger.Error(ex, "Fatal error while writing LoggingEvent to file: " + _lastFilename);
                    throw new LoggerFileWriteException("Fatal error while writing LoggingEvent to file: " + _lastFilename, ex);
                }
            }

            return false;
        }

        protected bool RefreshWriter(LoggingEvent data)
        {
            var filename = _isNeedFileRotate ? _filenameConverter.Convert(data) : _rawFilename;


            if (filename != _lastFilename || !_fileStreamAndWriter.IsInitialized)
            {
                try
                {
                    _fileStreamAndWriter = _filePool.RequestFile(filename, _fileStreamAndWriter, _lastFilename);
                    _lastFilename = filename;

                    return true;
                }
                catch (UnauthorizedAccessException ex)
                {
                    if (_errorTracker.CanWriteErrorGetAndUpdate())
                        _thisClassSupportLogger.Error(ex, "File opening error: " + filename);
                }
                catch (SecurityException ex)
                {
                    if (_errorTracker.CanWriteErrorGetAndUpdate())
                        _thisClassSupportLogger.Error(ex, "File opening error: " + filename);
                }
                catch (PathTooLongException ex)
                {
                    _thisClassSupportLogger.Error(ex, "Fatal file opening error: " + filename);
                    throw new LoggerFileWriteException("Fatal file opening error: " + filename, ex);
                }
                catch (IOException ex)
                {
                    if (_errorTracker.CanWriteErrorGetAndUpdate())
                        _thisClassSupportLogger.Error(ex, "File opening error: " + filename);
                }

                return false;
            }

            return true;
        }

        private static FileStream GetWriter(string filename)
        {
            if (!File.Exists(filename))
            {
                var folder = Path.GetDirectoryName(filename);

                if (!string.IsNullOrEmpty(folder))
                    Directory.CreateDirectory(folder);
            }

            return new FileStream(filename, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
        }

        /// <summary>
        /// Устанавливает фабрику для создания конвертеров,
        /// необходимых для преобразования логируемых данных в строки для вывода в файл или консоль
        /// </summary>
        /// <param name="factory"></param>
        public override void SetConverterFactory(ConverterFactory factory)
        {
            base.SetConverterFactory(factory);

            _templateConverter = TemplateParser.Parse(_rawTemplate, factory);
            _filenameConverter = TemplateParser.Parse(_rawFilename, factory);
        }



        protected override void Dispose(bool isUserCall)
        {
            if (!_isDisposed)
            {
                _isDisposed = true;

                if (isUserCall)
                {
                    lock (_lockWrite)
                    {
                        if (_fileStreamAndWriter.IsInitialized)
                        {
                            _fileStreamAndWriter.Flush(false);
                            _fileStreamAndWriter = new FileWriterFilePool.FileStreamAndWriter();
                        }

                        _filePool.Dispose();
                    }
                }
            }
        }
    }
}