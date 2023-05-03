// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Models;

[XmlRoot(WsWebConstants.Info, Namespace = "", IsNullable = false)]
public class WsServiceInfoModel : SerializeBase
{
    #region Public and private fields, properties, constructor

    public string Server { get; set; } = string.Empty;
    public string App { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string WinCurrentDate { get; set; } = string.Empty;
    public string SqlCurrentDate { get; set; } = string.Empty;
    public string ConnectionString { get; set; } = string.Empty;
    public int ConnectTimeout { get; set; }
    public string DataSource { get; set; } = string.Empty;
    public string SqlServerVersion { get; set; } = string.Empty;
    public string Database { get; set; } = string.Empty;
    public ulong PhysicalMegaBytes { get; set; }
    public ulong VirtualMegaBytes { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="app"></param>
    /// <param name="version"></param>
    /// <param name="winCurrentDate"></param>
    /// <param name="sqlCurrentDate"></param>
    /// <param name="connectionString"></param>
    /// <param name="connectTimeout"></param>
    /// <param name="dataSource"></param>
    /// <param name="serverVersion"></param>
    /// <param name="database"></param>
    /// <param name="physicalMegaBytes"></param>
    /// <param name="virtualMegaBytes"></param>
    public WsServiceInfoModel(string server, string app, string version, string winCurrentDate, string sqlCurrentDate, string connectionString,
        int connectTimeout, string dataSource, string serverVersion, string database, ulong physicalMegaBytes, ulong virtualMegaBytes)
    {
        Server = server;
        App = app;
        Version = version;
        WinCurrentDate = winCurrentDate;
        SqlCurrentDate = sqlCurrentDate;
        ConnectionString = connectionString;
        ConnectTimeout = connectTimeout;
        DataSource = dataSource;
        SqlServerVersion = serverVersion;
        Database = database;
        PhysicalMegaBytes = physicalMegaBytes;
        VirtualMegaBytes = virtualMegaBytes;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsServiceInfoModel()
    {
        //
    }

    #endregion

    #region Public and private methods

    public override string ToString()
    {
        return
            @$"{nameof(Server)}: {Server}. " + Environment.NewLine +
            @$"{nameof(App)}: {App}. " + Environment.NewLine +
            @$"{nameof(Version)}: {Version}. " + Environment.NewLine +
            @$"{nameof(WinCurrentDate)}: {WinCurrentDate}. " + Environment.NewLine +
            @$"{nameof(SqlCurrentDate)}: {SqlCurrentDate}. " + Environment.NewLine +
            @$"{nameof(ConnectionString)}: {ConnectionString}. " + Environment.NewLine +
            @$"{nameof(ConnectTimeout)}: {ConnectTimeout}. " + Environment.NewLine +
            @$"{nameof(DataSource)}: {DataSource}. " + Environment.NewLine +
            @$"{nameof(SqlServerVersion)}: {SqlServerVersion}. " + Environment.NewLine +
            @$"{nameof(Database)}: {Database}. " + Environment.NewLine +
            @$"{nameof(PhysicalMegaBytes)}: {PhysicalMegaBytes}. " + Environment.NewLine +
            @$"{nameof(VirtualMegaBytes)}: {VirtualMegaBytes}. ";
    }

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public new void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Server), Server);
        info.AddValue(nameof(App), App);
        info.AddValue(nameof(Version), Version);
        info.AddValue(nameof(WinCurrentDate), WinCurrentDate);
        info.AddValue(nameof(SqlCurrentDate), SqlCurrentDate);
        info.AddValue(nameof(ConnectionString), ConnectionString);
        info.AddValue(nameof(ConnectTimeout), ConnectTimeout);
        info.AddValue(nameof(DataSource), DataSource);
        info.AddValue(nameof(SqlServerVersion), SqlServerVersion);
        info.AddValue(nameof(Database), Database);
        info.AddValue(nameof(PhysicalMegaBytes), PhysicalMegaBytes);
        info.AddValue(nameof(VirtualMegaBytes), VirtualMegaBytes);
    }

    #endregion
}