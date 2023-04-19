// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Helpers;

public class WsSqlConnectFactory
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlConnectFactory _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlConnectFactory Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private readonly object _locker = new();

    #endregion

    #region Public and private methods

    public SqlConnection GetConnection()
    {
        lock (_locker)
        {
            if (string.IsNullOrEmpty(WsSqlAccessCoreHelper.JsonSettings.Local.ConnectionString))
            {
                throw new($"Factory not initialized. Call this method with param {nameof(WsSqlAccessCoreHelper.JsonSettings.Local.ConnectionString)}");
            }
        }
        return new(WsSqlAccessCoreHelper.JsonSettings.Local.ConnectionString);
    }

    public T GetValueAsNotNullable<T>(SqlDataReader reader, string fieldName) where T : struct
    {
        object value = reader[fieldName];
        Type t = typeof(T);
        t = Nullable.GetUnderlyingType(t) ?? t;
        T? result = value == null || DBNull.Value.Equals(value) ? default : (T)Convert.ChangeType(value, t);
        return result == null ? default : (T)result;
    }

    public string GetValueAsString(SqlDataReader reader, string fieldName)
    {
        object value = reader[fieldName];
        Type t = typeof(string);
        t = Nullable.GetUnderlyingType(t) ?? t;
        return value == null || DBNull.Value.Equals(value) ? string.Empty : (string)Convert.ChangeType(value, t);
    }

    #endregion

    #region Public and private methods - Wrappers execute

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

    #endregion
}