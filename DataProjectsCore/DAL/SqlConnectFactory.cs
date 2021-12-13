// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace DataProjectsCore.DAL
{
    public class SqlConnectFactory
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static SqlConnectFactory _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public string ConnectionString { get; }

        private static readonly object _locker = new();

        protected SqlConnectFactory(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected SqlConnection GetSqlConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public static SqlConnection GetConnection(string connectionString)
        {
            lock (_locker)
            {
                if (_instance == null)
                    _instance = new SqlConnectFactory(connectionString);
            }

            return _instance.GetSqlConnection();
        }

        public static SqlConnection GetConnection()
        {
            if (_instance == null)
            {
                throw new Exception(@"Factory not initialized. Call this method with param _connectionString");
            }

            return _instance.GetSqlConnection();
        }

        [Obsolete(@"Deprecated method")]
        public static T? GetValue<T>(IDataReader reader, string fieldName)
        {
            //object value = reader[fieldName];
            //Type t = typeof(T);
            //t = Nullable.GetUnderlyingType(t) ?? t;
            //return (value == null || DBNull.Value.Equals(value)) ? default : (T)Convert.ChangeType(value, t);
            return GetValueAsNullable<T>(reader, fieldName);
        }

        public static T GetValueAsNotNullable<T>(IDataReader reader, string fieldName) where T : struct
        {
            object value = reader[fieldName];
            Type t = typeof(T);
            t = Nullable.GetUnderlyingType(t) ?? t;
            T? result = (value == null || DBNull.Value.Equals(value)) ? default : (T)Convert.ChangeType(value, t);
            return result == null ? default : (T)result;
        }

        public static T? GetValueAsNullable<T>(IDataReader reader, string fieldName)
        {
            object value = reader[fieldName];
            Type t = typeof(T);
            t = Nullable.GetUnderlyingType(t) ?? t;
            return (value == null || DBNull.Value.Equals(value)) ? default : (T)Convert.ChangeType(value, t);
        }

        #region Public and private methods - Wrappers execute

        public delegate void ExecuteReaderInside(SqlDataReader reader);

        public static void ExecuteReader(string query, SqlParameter[] parameters, ExecuteReaderInside methodInside)
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
                    //cmd.CommandType = CommandType.TableDirect;
                    using SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        methodInside?.Invoke(reader);
                    }
                    reader.Close();
                }
                con.Close();
            }
        }

        public delegate T? ExecuteReaderInside<T>(SqlDataReader reader);

        public static T? ExecuteReader<T>(string query, SqlParameter[] parameters, ExecuteReaderInside<T> methodInside)
        {
            lock (_locker)
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
                        result = methodInside(reader);
                    }
                    reader.Close();
                }
                con.Close();
                return result;
            }
        }

        public static T ExecuteReaderForEntity<T>(string query, SqlParameter[] parameters, ExecuteReaderInside<T> methodInside) where T : new()
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
                        result = methodInside(reader) ?? new T();
                    }
                    reader.Close();
                }
                con.Close();
                return result;
            }
        }

        public static void ExecuteNonQuery(string query, SqlParameter[] parameters)
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
    }
}
