// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Data.Common;
using DataCore.Sql.Fields;

namespace DataCore.Sql;

/// <summary>
/// SQL helper.
/// </summary>
public class SqlHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static SqlHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static SqlHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    public DataAccessHelper DataAccess { get; private set; } = DataAccessHelper.Instance;
    public DbConnection? Connection { get; set; }
    public DbProviderFactory? ProviderFactory { get; private set; }
    public short ConnectTimeout { get; private set; }
    public short PacketSize { get; private set; }
    //public SqlAuthentication Authentication { get; private set; }
    public string ApplicationName { get; private set; }
    public string ConnectionString { get; private set; }
    public string Status { get; set; }
    public string WorkstationId { get; private set; }

    #endregion

    #region Constructor and destructor


    public SqlHelper()
    {
        ApplicationName = string.Empty;
        //Authentication = null;
        Connection = null;
        ConnectionString = string.Empty;
        ConnectTimeout = 600;
        PacketSize = 0;
        ProviderFactory = null;
        Status = string.Empty;
        WorkstationId = string.Empty;

        Open();
    }

    #endregion

    #region Public methods

    public void Open(string? applicationName = null, string? workstationId = null, short connectTimeout = 15, short packetSize = 8192)
    {
        //DataAccess.Setup(Directory.GetCurrentDirectory());
        //Authentication = new SqlAuthentication(server, database, integratedSecurity, userId, password, encrypt, false);
        ApplicationName = applicationName is string appName ? appName : "ScalesUI";
        ConnectTimeout = connectTimeout;
        if (ConnectTimeout < 15)
            ConnectTimeout = 15;
        PacketSize = packetSize;
        if (PacketSize < 512)
            PacketSize = 512;
        WorkstationId = workstationId is string wkId ? wkId : string.Empty;
        SetConnectionString();
    }

    public void SetConnectionString()
    {
        string workstation = !string.IsNullOrEmpty(WorkstationId) ? $@"Workstation ID={WorkstationId}" : $@"Workstation ID={System.Net.Dns.GetHostName()}";
        ConnectionString = $@"Application Name={ApplicationName}; Connect Timeout={ConnectTimeout}; Packet Size={PacketSize}; {workstation};";
        Console.WriteLine($@"ConnectionString=""{ConnectionString}""");
    }

    /// <summary>
    /// Set the sql parameters.
    /// </summary>
    /// <param name="cmd"></param>
    /// <param name="parameters"></param>
    private void SetParameters(DbCommand cmd, Collection<SqlParameter> parameters)
    {
        foreach (SqlParameter parameter in parameters)
        {
            cmd.Parameters.Add(new SqlParameter(parameter.ParameterName, parameter.Value));
        }
    }

    public Collection<Collection<object>> SelectData(string query, Collection<string> fieldNames, Collection<SqlParameter>? parameters = null)
    {
        Collection<Collection<object>> result = new();
        if (ProviderFactory == null || string.IsNullOrEmpty(query) || Connection == null || Connection.State != ConnectionState.Open)
            return result;

        using DbCommand cmd = ProviderFactory.CreateCommand();
        if (cmd != null)
        {
            cmd.CommandText = query;
            if (parameters?.Count > 0)
                SetParameters(cmd, parameters);
            cmd.Connection = Connection;
            //cmd.CommandTimeout = 180;
            if (cmd.Connection.State == ConnectionState.Open)
            {
                using DbDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Collection<object> record = new();
                        foreach (string field in fieldNames)
                        {
                            record.Add(reader.GetFieldValue<object>(reader.GetOrdinal(field)));
                        }
                        result.Add(record);
                    }
                }
                reader.Close();
            }
            else
            {
                Console.WriteLine(@"Connection is not open.");
            }
        }

        return result;
    }

    // Set db provider factory for Miscosoft.Data.SqlClient.
    public void SetProviderFactory()
    {
        if (ProviderFactory == null)
            ProviderFactory = SqlClientFactory.Instance;
        //ProviderFactory = DbProviderFactories.GetFactory("System.Data.SqlClient");
        //ProviderFactory = DbProviderFactories.GetFactory("Microsoft.Data.SqlClient");
    }


    /// <summary>
    /// Open the sql connection.
    /// </summary>
    /// <param name="language"></param>
    public void OpenConnection()
    {
        try
        {
            SetProviderFactory();
            if (ProviderFactory != null && !string.IsNullOrEmpty(ConnectionString))
            {
                Connection = ProviderFactory.CreateConnection();
                if (Connection != null)
                {
                    Connection.ConnectionString = ConnectionString;
                    Connection.Open();
                }
            }
            Status = LocaleCore.Sql.StatusConnected;
        }
        catch (Exception ex)
        {
            Status = LocaleCore.Sql.StatusExceptionConnect(ex);
            throw;
        }
    }

    /// <summary>
    /// Close the sql connection.
    /// </summary>
    /// <param name="language"></param>
    public void CloseConnection()
    {
        Status = LocaleCore.Sql.StatusClosed;
        Connection?.Close();
    }

    /// <summary>
    /// Get the sql connection state.
    /// </summary>
    /// <returns></returns>
    public ConnectionState GetConnection()
    {
        if (Connection != null)
        {
            return Connection.State;
        }
        return ConnectionState.Closed;
    }

    public SqlTableField<T>? GetValueField<T>(SqlTableField<T> field, SqlDataReader reader) where T : IConvertible
    {
        T? value = default;
        int ordinal = -1;
        try
        {
            if (field != null && reader != null)
            {
                ordinal = reader.GetOrdinal(field.Name);
                if (!reader.IsDBNull(ordinal))
                {
                    value = reader.GetFieldValue<T>(ordinal);
                }
            }
        }
        catch (Exception ex)
        {
            Status = LocaleCore.Sql.StatusExceptionFieldValue(ex, typeof(T).Name, reader.GetFieldType(ordinal).Name);
            throw;
        }

        if (field != null)
            return new SqlTableField<T>(field.Name, value, field.DefaultValue);
        //return new SqlTableField<T>(null, value, default);
        return null;
    }

    public T? GetValue<T>(string name, SqlDataReader reader, string? description = null) where T : IConvertible
    {
        T? value = default;
        int ordinal = -1;
        try
        {
            if (reader != null)
            {
                if (reader.IsFieldExists(name))
                    ordinal = reader.GetOrdinal(name);
                else
                    Status = $@"Поле [{name}] не найдено. {description}";
                if (ordinal >= 0)
                {
                    if (!reader.IsDBNull(ordinal))
                    {
                        value = reader.GetFieldValue<T>(ordinal);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Status = LocaleCore.Sql.StatusExceptionFieldValue(ex, typeof(T).Name, reader.GetFieldType(ordinal).Name);
            throw;
        }
        return value;
    }

    #endregion
}
