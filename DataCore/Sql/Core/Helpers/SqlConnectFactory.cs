// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.Core.Helpers;

public class SqlConnectFactory
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static SqlConnectFactory _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static SqlConnectFactory Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private readonly object _locker = new();
    private DataAccessHelper DataAccess => DataAccessHelper.Instance;

    #endregion

    #region Public and private methods

    private SqlConnection GetSqlConnection()
    {
        return new(DataAccess.JsonSettings.Local.ConnectionString);
    }

    public SqlConnection GetConnection()
    {
        lock (_locker)
        {
            if (string.IsNullOrEmpty(DataAccess.JsonSettings.Local.ConnectionString))
            {
                throw new($"Factory not initialized. Call this method with param {nameof(DataAccess.JsonSettings.Local.ConnectionString)}");
            }
        }
        return _instance.GetSqlConnection();
    }

    public T GetValueAsNotNullable<T>(SqlDataReader reader, string fieldName) where T : struct
    {
        object value = reader[fieldName];
        Type t = typeof(T);
        t = Nullable.GetUnderlyingType(t) ?? t;
        T? result = value == null || DBNull.Value.Equals(value) ? default : (T)Convert.ChangeType(value, t);
        return result == null ? default : (T)result;
    }

    //public T? GetValueAsNullable<T>(SqlDataReader reader, string fieldName)
    //{
    //    object value = reader[fieldName];
    //    Type t = typeof(T);
    //    t = Nullable.GetUnderlyingType(t) ?? t;
    //    return value == null || DBNull.Value.Equals(value) ? default : (T)Convert.ChangeType(value, t);
    //}

    public string GetValueAsString(SqlDataReader reader, string fieldName)
    {
        object value = reader[fieldName];
        Type t = typeof(string);
        t = Nullable.GetUnderlyingType(t) ?? t;
        return value == null || DBNull.Value.Equals(value) ? string.Empty : (string)Convert.ChangeType(value, t);
    }

    #endregion

    #region Public and private methods - Wrappers execute

    public void ExecuteReader(string query, Action<SqlDataReader> action) =>
        ExecuteReader(query, new SqlParameter[] { }, action);

    public void ExecuteReader(string query, SqlParameter parameter, Action<SqlDataReader> action) =>
        ExecuteReader(query, new[] { parameter }, action);

    private void ExecuteReader(string query, SqlParameter[] parameters, Action<SqlDataReader> action)
    {
        using SqlConnection con = GetConnection();
        con.Open();
        using SqlCommand cmd = new(query);
        cmd.Connection = con;
        cmd.Parameters.Clear();
        if (parameters.Length > 0)
            cmd.Parameters.AddRange(parameters);
        //cmd.CommandType = CommandType.TableDirect;
        using SqlDataReader reader = cmd.ExecuteReader();
        if (reader.HasRows)
        {
            action(reader);
        }
        reader.Close();
        con.Close();
    }

    //public T? ExecuteReader<T>(string query, SqlParameter parameter, Func<SqlDataReader, T> func) =>
    //    ExecuteReader(query, new[] { parameter }, func);

    //private T? ExecuteReader<T>(string query, SqlParameter[] parameters, Func<SqlDataReader, T> func)
    //{
    //    T? result = default;
    //    using SqlConnection con = GetConnection();
    //    con.Open();
    //    using (SqlCommand cmd = new(query))
    //    {
    //        cmd.Connection = con;
    //        cmd.Parameters.Clear();
    //        if (parameters.Length > 0)
    //            cmd.Parameters.AddRange(parameters);
    //        //cmd.CommandType = CommandType.TableDirect;
    //        using SqlDataReader reader = cmd.ExecuteReader();
    //        if (reader.HasRows)
    //        {
    //            result = func.Invoke(reader);
    //        }
    //        reader.Close();
    //    }
    //    con.Close();
    //    return result;
    //}

    //public T ExecuteReaderForItem<T>(string query, SqlParameter parameter, Func<SqlDataReader, T> func) where T : new() =>
    //    ExecuteReaderForItem(query, new[] { parameter }, func);

    //private T ExecuteReaderForItem<T>(string query, SqlParameter[] parameters, Func<SqlDataReader, T> func) where T : new()
    //{
    //    lock (_locker)
    //    {
    //        T result = new();
    //        using SqlConnection con = GetConnection();
    //        con.Open();
    //        using (SqlCommand cmd = new(query))
    //        {
    //            cmd.Connection = con;
    //            cmd.Parameters.Clear();
    //            if (parameters.Length > 0)
    //                cmd.Parameters.AddRange(parameters);
    //            //cmd.CommandType = CommandType.TableDirect;
    //            using SqlDataReader reader = cmd.ExecuteReader();
    //            if (reader.HasRows)
    //            {
    //                result = func(reader) ?? new T();
    //            }
    //            reader.Close();
    //        }
    //        con.Close();
    //        return result;
    //    }
    //}

    //public void ExecuteNonQuery(string query, SqlParameter parameter)
    //{
    //    ExecuteNonQuery(query, new[] { parameter });
    //}

    //public int ExecuteNonQuery(string query, SqlParameter[] parameters)
    //{
    //    lock (_locker)
    //    {
    //        int result = 0;
    //        using SqlConnection con = GetConnection();
    //        con.Open();
    //        using (SqlCommand cmd = new(query))
    //        {
    //            cmd.Connection = con;
    //            cmd.Parameters.Clear();
    //            if (parameters.Length > 0)
    //                cmd.Parameters.AddRange(parameters);
    //            //cmd.CommandType = CommandType.StoredProcedure;
    //            result = cmd.ExecuteNonQuery();
    //        }
    //        con.Close();
    //        return result;
    //    }
    //}

    #endregion
}