using WsStorageCore.Tables.TableRefModels.ProductionSites;

namespace WsStorageCore.Helpers;

/// <summary>
/// SQL-менеджер доступа к данным БД (используется клиентами).
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlContextManagerHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlContextManagerHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlContextManagerHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private WsAppVersionHelper AppVersion => WsAppVersionHelper.Instance;
    private WsFileLoggerHelper FileLogger => WsFileLoggerHelper.Instance;
    public WsJsonSettingsHelper JsonSettings => WsJsonSettingsHelper.Instance;
    public WsSqlContextItemHelper ContextItem => WsSqlContextItemHelper.Instance;
    public WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;
    public MsSqlConfiguration? SqlConfiguration => SqlCore.SqlConfiguration;
    public WsSqlProductionSiteRepository ProductionSiteRepository { get; } = new();
    public WsSqlDeviceRepository DeviceRepository { get; } = new();
    public WsSqlDeviceSettingsFkRepository DeviceSettingsFksRepository { get; } = new();
    public WsSqlLineRepository LineRepository { get; } = new();
    public WsSqlLogMemoryRepository LogMemoryRepository { get; } = new();
    public WsSqlPlu1CRepository Plu1CRepository { get; } = new();
    public WsSqlPluClipFkRepository PlusClipFkRepository { get; } = new();
    public WsSqlPluLabelRepository PluLabelRepository { get; } = new();
    public WsSqlPluLineRepository PluLineRepository { get; } = new();
    public WsSqlPluNestingFkRepository PluNestingFkRepository { get; } = new();
    public WsSqlPluStorageMethodFkRepository SqlPluStorageMethodFkRepository { get; } = new();
    public WsSqlPluWeighingRepository PluWeighingRepository { get; } = new();

    #endregion

    #region Public and private methods

    private bool SetupConfigsCore(string dir, bool isRemote, string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
            throw new ArgumentException("Value must be fill!", nameof(fileName));

        string file = Path.Combine(dir, fileName);
        if (!File.Exists(file))
            throw new(WsLocaleCore.System.ConfigFileNotFound(file));

        using StreamReader streamReader = File.OpenText(file);
        JsonSerializer serializer = new();
        object? jsonObject = (WsJsonSettingsModel?)serializer.Deserialize(streamReader, typeof(WsJsonSettingsModel));
        if (jsonObject is WsJsonSettingsModel jsonSettings)
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new()
            {
                ["Data Source"] = jsonSettings.Sql.DataSource,
                ["Initial Catalog"] = jsonSettings.Sql.InitialCatalog,
                ["Persist Security Info"] = jsonSettings.Sql.PersistSecurityInfo,
                ["User ID"] = jsonSettings.Sql.UserId,
                ["Password"] = jsonSettings.Sql.Password,
                //sqlConnectionStringBuilder["Encrypt"] = jsonSettings.Sql.Encrypt;
                ["Connect Timeout"] = jsonSettings.Sql.ConnectTimeout,
                ["TrustServerCertificate"] = jsonSettings.Sql.TrustServerCertificate
            };

            switch (isRemote)
            {
                case false:
                    JsonSettings.Local = jsonSettings;
                    JsonSettings.Local.ConnectionString = sqlConnectionStringBuilder.ConnectionString;
                    break;
                default:
                    JsonSettings.Remote = jsonSettings;
                    JsonSettings.Remote.ConnectionString = sqlConnectionStringBuilder.ConnectionString;
                    break;
            }
            JsonSettings.IsRemote = isRemote;
            //DataAccessHelper.Instance.SetupSessionFactory();
        }
        return jsonObject is not null;
    }

    public void SetupJsonConsole(string localDir, string appName)
    {
        try
        {
            CheckConfigsUpdates(localDir, JsonSettings.JsonFileName);
            if (!SetupConfigsCore(localDir, false, JsonSettings.JsonFileName))
                throw new(WsLocaleCore.System.ConfigLocalFileException);
            SqlCore.SetSessionFactory(false);
            ContextItem.SetupLog(appName);
        }
        catch (Exception ex)
        {
            FileLogger.StoreException(ex);
        }
    }

    public void SetupJsonScales(string localDir, string appName)
    {
        try
        {
            // #WS-T-1105: Fix jitDebugging error for Debug configs.
            CheckMachineConfigUpdates(localDir);
            CheckConfigsUpdates(localDir, JsonSettings.JsonFileName);
            if (!SetupConfigsCore(localDir, false, JsonSettings.JsonFileName))
                throw new(WsLocaleCore.System.ConfigLocalFileException);
            SqlCore.SetSessionFactory(WsDebugHelper.Instance.IsDevelop);
            ContextItem.SetupLog(appName);
        }
        catch (Exception ex)
        {
            FileLogger.StoreException(ex);
        }
    }

    private void SetupJsonTestsCore(string localDir, string deviceName, string appName, string fileName, bool isShowSql)
    {
        CheckConfigsUpdates(localDir, fileName);

        if (!SetupConfigsCore(localDir, false, fileName))
            throw new(WsLocaleCore.System.ConfigLocalFileException);

        SqlCore.SetSessionFactory(isShowSql);
        ContextItem.SetupLog(deviceName, appName);
    }

    public void SetupJsonTestsDevelopAleksandrov(string localDir, string deviceName, string appName, bool isShowSql) =>
        SetupJsonTestsCore(localDir, deviceName, appName, JsonSettings.FileNameDevelopAleksandrov, isShowSql);

    public void SetupJsonTestsDevelopMorozov(string localDir, string deviceName, string appName, bool isShowSql) =>
        SetupJsonTestsCore(localDir, deviceName, appName, JsonSettings.FileNameDevelopMorozov, isShowSql);

    public void SetupJsonTestsDevelopVs(string localDir, string deviceName, string appName, bool isShowSql) =>
        SetupJsonTestsCore(localDir, deviceName, appName, JsonSettings.FileNameDevelopVs, isShowSql);
    
    public void SetupJsonTestsReleaseVs(string localDir, string deviceName, string appName, bool isShowSql) =>
        SetupJsonTestsCore(localDir, deviceName, appName, JsonSettings.FileNameReleaseVs, isShowSql);

    public void SetupJsonWebApp(string localDir, string? appName, bool isShowSql)
    {
        try
        {
            AppVersion.Setup(Assembly.GetExecutingAssembly());
            if (appName != null)
                FileLogger.Setup(localDir, appName);
            string subDir = Path.Combine(localDir, JsonSettings.BinNetSubDir);
            if (Directory.Exists(subDir))
            {
                // Local folder.
                CheckConfigsUpdates(subDir, JsonSettings.JsonFileName);
                if (!SetupConfigsCore(subDir, false, JsonSettings.JsonFileName))
                    throw new(WsLocaleCore.System.ConfigLocalFileException);
            }
            else
            {
                // IIS publish folder.
                CheckConfigsUpdates(localDir, JsonSettings.JsonFileName);
                if (!SetupConfigsCore(localDir, false, JsonSettings.JsonFileName))
                    throw new(WsLocaleCore.System.ConfigLocalFileException);
            }

            SqlCore.SetSessionFactory(isShowSql);
            ContextItem.SetupLog(appName ?? string.Empty);
            ContextItem.SaveLogInformation(WsLocaleCore.DeviceControl.WebAppIsStarted);
        }
        catch (Exception ex)
        {
            FileLogger.StoreException(ex);
        }
    }

    /// <summary>
    /// #WS-T-1105: Fix jitDebugging error for Debug configs.
    /// </summary>
    private void CheckMachineConfigUpdates(string localDir)
    {
        string fileName = "machine.config";
        string remoteFile = Path.Combine(JsonSettings.RemoteDir, fileName);
        CheckDirAndFile(remoteFile);
        if (!WsDebugHelper.Instance.Config.Equals(WsEnumConfiguration.DevelopVS)) return;

        string localFile = Path.Combine(localDir, fileName);
        if (File.Exists(localFile)) File.Delete(localFile);

        using StreamReader streamReader = File.OpenText(remoteFile);
        string content = streamReader.ReadToEnd();
        streamReader.Close();
        streamReader.Dispose();
        if (string.IsNullOrEmpty(content))
            throw new(WsLocaleCore.System.ConfigFileIsEmpty(remoteFile));

        using StreamWriter streamWriter = File.CreateText(Path.Combine(localDir, fileName));
        streamWriter.Write(content);
        streamWriter.Close();
        streamWriter.Dispose();
    }

    private void CheckConfigsUpdates(string localDir, string fileName)
    {
        string remoteFile = Path.Combine(JsonSettings.RemoteDir, fileName);
        CheckDirAndFile(remoteFile);
        switch (WsDebugHelper.Instance.Config)
        {
            case WsEnumConfiguration.DevelopAleksandrov:
            case WsEnumConfiguration.DevelopMorozov:
                return;
        }
        SetupConfigsCore(JsonSettings.RemoteDir, true, fileName);

        ushort version = GetJsonLocalVersion(localDir, fileName);
        if (version == 0 || version < JsonSettings.Remote.Version)
        {
            string localFile = Path.Combine(localDir, fileName);
            if (File.Exists(localFile)) File.Delete(localFile);

            using StreamReader streamReader = File.OpenText(remoteFile);
            string content = streamReader.ReadToEnd();
            streamReader.Close();
            streamReader.Dispose();
            if (string.IsNullOrEmpty(content))
                throw new(WsLocaleCore.System.ConfigFileIsEmpty(remoteFile));

            using StreamWriter streamWriter = File.CreateText(Path.Combine(localDir, fileName));
            streamWriter.Write(content);
            streamWriter.Close();
            streamWriter.Dispose();
        }
    }

    private void CheckDirAndFile(string fileName)
    {
        if (!Directory.Exists(JsonSettings.RemoteDir))
            throw new(WsLocaleCore.System.ConfigRemoteFolderNotFound);
        if (!File.Exists(fileName))
            throw new(WsLocaleCore.System.ConfigRemoteFileNotFound);
    }

    private ushort GetJsonLocalVersion(string localDir, string fileName)
    {
        string localFile = Path.Combine(localDir, fileName);
        if (!File.Exists(localFile))
            return 0;

        using StreamReader streamReader = File.OpenText(localFile);
        string content = streamReader.ReadToEnd();
        streamReader.Close();
        streamReader.Dispose();
        if (string.IsNullOrEmpty(content))
        {
            //throw new Exception(LocaleCore.System.JsonSettingsFileIsEmpty(localFile));
            return 0;
        }

        if (content.Contains(nameof(WsJsonSettingsModel.Version)))
        {
            string[] lines = content.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (string line in lines)
            {
                if (line.Contains($"\"{nameof(WsJsonSettingsModel.Version)}\""))
                {
                    int posStart = line.IndexOf(": ") + 2;
                    int posEnd = line.IndexOf(",");
                    int length = posEnd - posStart;
                    string strVersion = line.Substring(posStart, length);
                    if (ushort.TryParse(strVersion, out ushort result))
                        return result;
                    //throw new Exception(LocaleCore.System.JsonSettingsParseVersionException(localFile));
                }
            }
        }
        return 0;
    }
    

    #endregion
}