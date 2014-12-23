using Qoollo.Logger.Common;
using Qoollo.Logger.Configuration;
using Qoollo.Logger.Exceptions;
using Qoollo.Logger.Helpers;
using Qoollo.Logger.LoggingEventConverters;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.IO;

namespace Qoollo.Logger.Writers
{
    /// <summary>
    /// DBWriter. Ресурс для записи сообщений в БД.
    /// В базе предварительно нужно создать таблицу для логов и хранимую процедуру для инсерта (лежит в папке с проектом)
    /// </summary>
    internal class DBWriter : Writer
    {
        private static readonly Logger _thisClassSupportLogger = InnerSupportLogger.Instance.GetClassLogger(typeof(DBWriter));

        private readonly string _storeProcedureName;    // Имя хранимой процедуры
        private readonly string _connectionString;
        private SqlConnection _conn;
        private readonly object _lockWrite = new object();
        private readonly LogLevel _logLevel;

        private LoggingEventConverterBase _exceptionConverterBase;
        private LoggingEventConverterBase _sourcesConverterBase;

        private ErrorTimeTracker _errorTracker = new ErrorTimeTracker(TimeSpan.FromMinutes(5));

        private volatile bool _isDisposed = false;

        public DBWriter(DatabaseWriterConfiguration config)
            : base(config.Level)
        {
            Contract.Requires(config != null);

            _logLevel = config.Level;
            _storeProcedureName = config.StoredProcedureName;
            _connectionString = config.ConnectionString;
            
            SetConverterFactory(ConverterFactory.Default);
        }


        private bool Connect()
        {
            try
            {
                Contract.Assume(_conn == null || _conn.State == ConnectionState.Closed);
                _conn = new SqlConnection(_connectionString);
                _conn.Open();
                return true;
            }
            catch (SqlException ex)
            {
                if (_errorTracker.CanWriteErrorGetAndUpdate())
                    _thisClassSupportLogger.ErrorFormat(ex, "Ошибка при инициализаци Базы данных \"{0}\"", _conn.Database);
                return false;
            }
        }

        private bool ConnectCheck()
        {
            if (_conn == null)
            {
                if (!Connect())
                    return false;
            }
            else if (_conn.State != ConnectionState.Open)
            {
                if (_conn.State == ConnectionState.Broken)
                    _conn.Close();

                if (!Connect())
                    return false;
            }

            return true;
        }

        public override bool Write(LoggingEvent data)
        {
            if (_isDisposed)
            {
                if (_errorTracker.CanWriteErrorGetAndUpdate())
                    _thisClassSupportLogger.Error("Попытка записи логирующего сообщения при освобожденных ресурсах");

                return false;
            }

            if (data.Level < _logLevel)
                return true;


            bool result = false;

            try
            {
                lock (_lockWrite)
                {
                    if (_isDisposed)
                        return false;

                    if (!ConnectCheck())
                    {
                        if (_errorTracker.CanWriteErrorGetAndUpdate())
                            _thisClassSupportLogger.Error("Не удалось подключиться к Базе данных.");
                        return false;
                    }

                    var comm = CreateCommand(data);

                    comm.ExecuteNonQuery();
                    result = true;
                }
            }
            catch (SqlException ex)
            {
                if (_errorTracker.CanWriteErrorGetAndUpdate())
                    _thisClassSupportLogger.Error(ex, "Ошибка при записи логирующего сообщения в Базу данных");

                result = false;
            }
            catch (IOException ex)
            {
                if (_errorTracker.CanWriteErrorGetAndUpdate())
                    _thisClassSupportLogger.Error(ex, "Ошибка при записи логирующего сообщения в Базу данных");

                result = false;
            }
            catch (Exception ex)
            {
                _thisClassSupportLogger.Error(ex, "Непоправимая ошибка при записи логирующего сообщения в Базу данных");
                throw new LoggerDBWriteException("Непоправимая ошибка при записи логирующего сообщения в Базу данных", ex);
            }

            return result;
        }


        private SqlCommand CreateCommand(LoggingEvent data)
        {
            var comm = new SqlCommand(_storeProcedureName, _conn) {CommandType = CommandType.StoredProcedure};

            comm.Parameters.Add("@Time", SqlDbType.DateTime);
            comm.Parameters.Add("@Level", SqlDbType.TinyInt);
            comm.Parameters.Add("@Class", SqlDbType.VarChar, 100);
            comm.Parameters.Add("@Method", SqlDbType.VarChar, 50);
            comm.Parameters.Add("@Message", SqlDbType.VarChar);
            comm.Parameters.Add("@Exception", SqlDbType.VarChar);
            comm.Parameters.Add("@Stacksources", SqlDbType.VarChar);
            comm.Parameters.Add("@Context", SqlDbType.VarChar);
            comm.Parameters.Add("@FilePath", SqlDbType.VarChar);
            comm.Parameters.Add("@LineNumber", SqlDbType.Int);

            comm.Parameters["@Class"].IsNullable = true;
            comm.Parameters["@Method"].IsNullable = true;
            comm.Parameters["@Exception"].IsNullable = true;
            comm.Parameters["@Stacksources"].IsNullable = true;
            comm.Parameters["@Context"].IsNullable = true;
            comm.Parameters["@FilePath"].IsNullable = true;
            comm.Parameters["@LineNumber"].IsNullable = true;

            comm.Parameters["@Time"].Value = data.Date;
            comm.Parameters["@Level"].Value = (byte) data.Level.Level;
            comm.Parameters["@Message"].Value = data.Message;

            if (data.Clazz == null)
            {
                comm.Parameters["@Class"].Value = DBNull.Value;
            }
            else
            {
                comm.Parameters["@Class"].Value = data.Clazz;
            }

            if (data.Method == null)
            {
                comm.Parameters["@Method"].Value = DBNull.Value;
            }
            else
            {
                comm.Parameters["@Method"].Value = data.Method;
            }

            if (data.Exception == null)
            {
                comm.Parameters["@Exception"].Value = DBNull.Value;
            }
            else
            {
                comm.Parameters["@Exception"].Value = _exceptionConverterBase.Convert(data);
            }

            if (data.StackSources == null)
            {
                comm.Parameters["@Stacksources"].Value = DBNull.Value;
            }
            else
            {
                comm.Parameters["@Stacksources"].Value = _sourcesConverterBase.Convert(data);
            }

            if (data.Context == null)
            {
                comm.Parameters["@Context"].Value = DBNull.Value;
            }
            else
            {
                comm.Parameters["@Context"].Value = data.Context;
            }

            if (data.FilePath == null)
            {
                comm.Parameters["@FilePath"].Value = DBNull.Value;
            }
            else
            {
                comm.Parameters["@FilePath"].Value = data.FilePath;
            }

            comm.Parameters["@LineNumber"].Value = data.LineNumber;

            return comm;
        }

        public override void SetConverterFactory(ConverterFactory factory)
        {
            base.SetConverterFactory(factory);

            _exceptionConverterBase = factory.CreateExceptionConverter();
            _sourcesConverterBase = factory.CreateStackSourceConverter();
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
                        try
                        {
                            _conn.Close();
                            _conn = null;
                        }
                        catch (SqlException)
                        {
                        }
                    }
                }
            }
        }
    }
}