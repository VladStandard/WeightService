// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading;

namespace DataCore.DAL
{
    public class SqlConnectFactory
    {
        #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static SqlConnectFactory _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static SqlConnectFactory Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public and private fields and properties

        public string ConnectionString { get; }
        private readonly object _locker = new();
        public delegate void ExecuteReaderCallback(SqlDataReader reader);
        public delegate T? ExecuteReaderCallback<T>(SqlDataReader reader);

        #endregion

        #region Constructor and destructor

        protected SqlConnectFactory(string connectionString)
        {
            ConnectionString = connectionString;
        }

        #endregion

        #region Public and private methods

        protected SqlConnection GetSqlConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public SqlConnection GetConnection(string connectionString)
        {
            lock (_locker)
            {
                if (_instance == null)
                    _instance = new SqlConnectFactory(connectionString);
            }

            return _instance.GetSqlConnection();
        }

        public SqlConnection GetConnection()
        {
            lock (_locker)
            {
                if (_instance == null)
                {
                    throw new Exception(@"Factory not initialized. Call this method with param _connectionString");
                }
            }

            return _instance.GetSqlConnection();
        }

        public T GetValueAsNotNullable<T>(IDataReader reader, string fieldName) where T : struct
        {
            object value = reader[fieldName];
            Type t = typeof(T);
            t = Nullable.GetUnderlyingType(t) ?? t;
            T? result = value == null || DBNull.Value.Equals(value) ? default : (T)Convert.ChangeType(value, t);
            return result == null ? default : (T)result;
        }

        public T? GetValueAsNullable<T>(IDataReader reader, string fieldName)
        {
            object value = reader[fieldName];
            Type t = typeof(T);
            t = Nullable.GetUnderlyingType(t) ?? t;
            return value == null || DBNull.Value.Equals(value) ? default : (T)Convert.ChangeType(value, t);
        }

        public string GetValueAsString(IDataReader reader, string fieldName)
        {
            object value = reader[fieldName];
            Type t = typeof(string);
            t = Nullable.GetUnderlyingType(t) ?? t;
            return value == null || DBNull.Value.Equals(value) ? string.Empty : (string)Convert.ChangeType(value, t);
        }

        #region Public and private methods - Wrappers execute

        public void ExecuteReader(string query, SqlParameter[] parameters, ExecuteReaderCallback callback)
        {
            using SqlConnection con = GetConnection();
            con.Open();
            using (SqlCommand cmd = new(query))
            {
                cmd.Connection = con;
                cmd.Parameters.Clear();
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);
                //cmd.CommandType = CommandType.TableDirect;
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    callback?.Invoke(reader);
                }
                reader.Close();
            }
            con.Close();
        }

        public T? ExecuteReader<T>(string query, SqlParameter[] parameters, ExecuteReaderCallback<T> callback)
        {
            T? result = default;
            using SqlConnection con = GetConnection();
            con.Open();
            using (SqlCommand cmd = new(query))
            {
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.Parameters.AddRange(parameters);
                //cmd.CommandType = CommandType.TableDirect;
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    result = callback(reader);
                }
                reader.Close();
            }
            con.Close();
            return result;
        }

        public T ExecuteReaderForEntity<T>(string query, SqlParameter[] parameters, ExecuteReaderCallback<T> callback) where T : new()
        {
            lock (_locker)
            {
                T result = new();
                using SqlConnection con = GetConnection();
                con.Open();
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddRange(parameters);
                    //cmd.CommandType = CommandType.TableDirect;
                    using SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        result = callback(reader) ?? new T();
                    }
                    reader.Close();
                }
                con.Close();
                return result;
            }
        }

        public void ExecuteNonQuery(string query, SqlParameter[] parameters)
        {
            lock (_locker)
            {
                using SqlConnection con = GetConnection();
                con.Open();
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddRange(parameters);
                    //cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        #endregion

        #endregion
    }
}
