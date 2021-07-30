// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Data;
using System.Data.SqlClient;

namespace EntitiesLib
{
    public class SqlConnectFactory
    {

        private static SqlConnectFactory _instance;

        public string ConnectionString { get; }

        private static readonly object Locker = new object();

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
            lock (Locker)
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

        public static T GetValue<T>(IDataReader dr, string fieldName)
        {
            object value = dr[fieldName];

            Type t = typeof(T);
            t = Nullable.GetUnderlyingType(t) ?? t;

            return (value == null || DBNull.Value.Equals(value)) ?
                default(T) : (T)Convert.ChangeType(value, t);
        }
    }
}
