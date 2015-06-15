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

        private LoggingEventConverterBase _exceptionConverter;
        private LoggingEventConverterBase _sourcesConverter;

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
                    _thisClassSupportLogger.ErrorFormat(ex, "Can't open connection to SQL Server. Database: \"{0}\"", _conn.Database);
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
                    _thisClassSupportLogger.Error("Attempt to write LoggingEvent in Disposed state.");

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
                            _thisClassSupportLogger.Error("Can't open connection to MS SQL Server.");
                        return false;
                    }

                    using (var comm = CreateCommand(data))
                    {
                        comm.ExecuteNonQuery();
                        result = true;
                    }
                }
            }
            catch (SqlException ex)
            {
                if (_errorTracker.CanWriteErrorGetAndUpdate())
                    _thisClassSupportLogger.Error(ex, "Error during exection the command that insert log message to database");

                result = false;
            }
            catch (IOException ex)
            {
                if (_errorTracker.CanWriteErrorGetAndUpdate())
                    _thisClassSupportLogger.Error(ex, "Error during exection the command that insert log message to database");

                result = false;
            }
            catch (Exception ex)
            {
                _thisClassSupportLogger.Error(ex, "Fatal error while inserting log message to database");
                throw new LoggerDBWriteException("Fatal error while inserting log message to database", ex);
            }

            return result;
        }


        private static SqlParameter AddParameter(SqlCommand cmd, SqlDbType type, int size, bool isNullable, string name, object value)
        {
            if (value == null)
                value = DBNull.Value;

            return cmd.Parameters.Add(new SqlParameter(name, type, size) { IsNullable = isNullable, Value = value });
        }


        private SqlCommand CreateCommand(LoggingEvent data)
        {
            var cmd = new SqlCommand(_storeProcedureName, _conn) { CommandType = CommandType.StoredProcedure };

            AddParameter(cmd, SqlDbType.DateTime,  0,      false, "@Date",         data.Date);
            AddParameter(cmd, SqlDbType.TinyInt,   0,      false, "@Level",        (byte)data.Level.Level);
            AddParameter(cmd, SqlDbType.NVarChar,  0,      true , "@Context",      data.Context);
            AddParameter(cmd, SqlDbType.NVarChar,  255,    true , "@Class",        data.Clazz);
            AddParameter(cmd, SqlDbType.NVarChar,  255,    true , "@Method",       data.Method);
            AddParameter(cmd, SqlDbType.NVarChar,  0,      true , "@FilePath",     data.FilePath);
            AddParameter(cmd, SqlDbType.Int,       0,      true , "@LineNumber",   data.LineNumber < 0 ? (object)null : data.LineNumber);
            AddParameter(cmd, SqlDbType.NVarChar,  0,      false, "@Message",      data.Message);
            AddParameter(cmd, SqlDbType.NVarChar,  0,      true , "@Exception",    data.Exception != null ? _exceptionConverter.Convert(data) : null);
            AddParameter(cmd, SqlDbType.NVarChar,  0,      true , "@StackSources", data.StackSources != null ? _sourcesConverter.Convert(data) : null);
            AddParameter(cmd, SqlDbType.NVarChar,  0,      true , "@Namespace",    data.Namespace);
            AddParameter(cmd, SqlDbType.NVarChar,  0,      true , "@Assembly",     data.Assembly);
            AddParameter(cmd, SqlDbType.NVarChar,  255,    true , "@MachineName",  data.MachineName);
            AddParameter(cmd, SqlDbType.NVarChar,  64,     true , "@MachineIp",    data.MachineIpAddress);
            AddParameter(cmd, SqlDbType.NVarChar,  255,    true , "@ProcessName",  data.ProcessName);
            AddParameter(cmd, SqlDbType.Int,       0,      true , "@ProcessId",    data.ProcessId < 0 ? (object)null : data.ProcessId);

            return cmd;
        }

        public override void SetConverterFactory(ConverterFactory factory)
        {
            base.SetConverterFactory(factory);

            _exceptionConverter = factory.CreateExceptionConverter();
            _sourcesConverter = factory.CreateStackSourceConverter();
        }



        protected override void Dispose(DisposeReason reason)
        {
            if (!_isDisposed)
            {
                _isDisposed = true;

                if (reason != DisposeReason.Finalize)
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