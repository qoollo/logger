using Qoollo.Logger.Common;
using Qoollo.Logger.Configuration;
using Qoollo.Logger.LoggingEventConverters;
using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Security;
using System.Text;

namespace Qoollo.Logger.Writers
{
    /// <summary>
    /// SupportFileWriter. Вспомогательный ресурс для записи сообщений в файл
    /// </summary>
    internal class SupportFileWriter : Writer
    {
        private readonly Encoding _encoding;
        private readonly string _fileName;
        private readonly string _rawTemplate;
        private LoggingEventConverterBase _templateConverter;
        private FileStream _fileStream;
        private StreamWriter _writer;
        private readonly object _lockWrite = new object();
        private volatile bool _isDisposed = false;

        private SupportFileWriter(FileWriterConfiguration config)
            : base(LogLevel.Trace)
        {
            Contract.Requires(config != null);

            _encoding = config.Encoding;
            _rawTemplate = config.Template;
            _fileName = config.FileNameTemplate;

            SetConverterFactory(ConverterFactory.Default);
        }

        public SupportFileWriter(string fileName, string template = null)
            : base(LogLevel.Trace)
        {
            Contract.Requires(fileName != null);

            _encoding = Encoding.UTF8;
            _rawTemplate = template ?? "{DateTime, format = 'HH:mm:ss dd:MM:YYYY'}|{Level}|{Msg}|{Class}|{Exception}";
            _fileName = fileName;

            SetConverterFactory(ConverterFactory.Default);
        }



        public override bool Write(LoggingEvent data)
        {
            if (_isDisposed)
                return false;

            lock (_lockWrite)
            {
                if (_isDisposed)
                    return false;

                if (!RefreshWriter(data))
                    return false;

                var line = _templateConverter.Convert(data);

                try
                {
                    _writer.WriteLine(line);
                    _writer.Flush();
                    _fileStream.Flush(true);

                    return true;
                }
                catch (Exception)
                {
                }
            }

            return false;
        }


        private bool RefreshWriter(LoggingEvent data)
        {
            if (_fileStream != null)
                return true;

            try
            {
                _fileStream = GetFileStream(_fileName);
                _writer = new StreamWriter(_fileStream, _encoding);

                return true;
            }
            catch (UnauthorizedAccessException)
            {
            }
            catch (SecurityException)
            {
            }
            catch (PathTooLongException)
            {
            }
            catch (IOException)
            {
            }

            return false;
        }

        private static FileStream GetFileStream(string filename)
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
        }


        protected override void Dispose(bool isUserCall)
        {
            if (!_isDisposed)
            {
                lock (_lockWrite)
                {
                    if (!_isDisposed)
                    {
                        _isDisposed = true;

                        if (isUserCall)
                        {
                            if (_writer != null)
                            {
                                _writer.Close();
                                _writer = null;
                            }

                            if (_fileStream != null)
                            {
                                _fileStream.Dispose();
                                _fileStream = null;
                            }
                        }
                    }
                }
            }
        }
    }
}