// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable InconsistentNaming

namespace WsStorageCore.Helpers;

/// <summary>
/// SQL-менеджер SQL-помощников.
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

    private WsSqlAccessCoreHelper AccessCore => WsSqlAccessCoreHelper.Instance;
    public WsSqlAccessManagerHelper AccessManager => WsSqlAccessManagerHelper.Instance;
    public WsSqlAccessItemHelper AccessItem => WsSqlAccessItemHelper.Instance;
    public WsSqlAccessListHelper AccessList => WsSqlAccessListHelper.Instance;
    private WsSqlContextCoreHelper ContextCore => WsSqlContextCoreHelper.Instance;
    public WsSqlContextItemHelper ContextItem => WsSqlContextItemHelper.Instance;
    public WsSqlContextListHelper ContextList => WsSqlContextListHelper.Instance;
    public WsSqlContextViewHelper ContextView => WsSqlContextViewHelper.Instance;
    public WsSqlContextBoxHelper ContextBox => WsSqlContextBoxHelper.Instance;
    public WsSqlContextBundleHelper ContextBundle => WsSqlContextBundleHelper.Instance;
    public WsSqlContextPluHelper ContextPlu => WsSqlContextPluHelper.Instance;
    public WsSqlContextPlu1cFkHelper ContextPlu1cFk => WsSqlContextPlu1cFkHelper.Instance;
    public WsSqlContextPluNestingHelper ContextPluNesting => WsSqlContextPluNestingHelper.Instance;
    public WsSqlContextPluStorageHelper ContextPluStorage => WsSqlContextPluStorageHelper.Instance;
    public WsJsonSettingsHelper JsonSettings => WsJsonSettingsHelper.Instance;
    private AppVersionHelper AppVersion => AppVersionHelper.Instance;
    private WsFileLoggerHelper FileLogger => WsFileLoggerHelper.Instance;
    public FluentNHibernate.Cfg.Db.MsSqlConfiguration? SqlConfiguration => AccessCore.SqlConfiguration;

    #endregion

    #region Public and private methods

    private bool SetupJsonSettingsCore(string dir, bool isRemote, string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
            throw new ArgumentException("Value must be fill!", nameof(fileName));

        string file = Path.Combine(dir, fileName);
        if (!File.Exists(file))
        {
            throw new(LocaleCore.System.JsonSettingsFileNotFound(file));
        }

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

    public void SetupJsonScales(string localDir, string appName)
    {
        try
        {
            FileLogger.Setup(localDir, appName);
            CheckJsonUpdates(localDir, JsonSettings.JsonFileName);

            if (!SetupJsonSettingsCore(localDir, false, JsonSettings.JsonFileName))
            {
                //if (isFileLog)
                //    FileLog.WriteMessage(LocaleCore.System.JsonSettingsLocalFileException);
                throw new(LocaleCore.System.JsonSettingsLocalFileException);
            }

            AccessCore.SetupSessionFactory(DebugHelper.Instance.IsDevelop);
            ContextItem.SetupLog(appName);
        }
        catch (Exception ex)
        {
            FileLogger.StoreException(ex);
        }
    }

    private void SetupJsonTestsCore(string localDir, string deviceName, string appName, string fileName, bool isShowSql)
    {
        CheckJsonUpdates(localDir, fileName);

        if (!SetupJsonSettingsCore(localDir, false, fileName))
            throw new(LocaleCore.System.JsonSettingsLocalFileException);

        AccessCore.SetupSessionFactory(isShowSql);
        ContextItem.SetupLog(deviceName, appName);
    }

    public void SetupJsonTestsDevelopAleksandrov(string localDir, string deviceName, string appName, bool isShowSql) =>
        SetupJsonTestsCore(localDir, deviceName, appName, JsonSettings.FileNameDevelopAleksandrov, isShowSql);

    public void SetupJsonTestsDevelopMorozov(string localDir, string deviceName, string appName, bool isShowSql) =>
        SetupJsonTestsCore(localDir, deviceName, appName, JsonSettings.FileNameDevelopMorozov, isShowSql);

    public void SetupJsonTestsDevelopVs(string localDir, string deviceName, string appName, bool isShowSql) =>
        SetupJsonTestsCore(localDir, deviceName, appName, JsonSettings.FileNameDevelopVs, isShowSql);

    public void SetupJsonTestsReleaseAleksandrov(string localDir, string deviceName, string appName, bool isShowSql) =>
        SetupJsonTestsCore(localDir, deviceName, appName, JsonSettings.FileNameReleaseAleksandrov, isShowSql);

    public void SetupJsonTestsReleaseMorozov(string localDir, string deviceName, string appName, bool isShowSql) =>
        SetupJsonTestsCore(localDir, deviceName, appName, JsonSettings.FileNameReleaseMorozov, isShowSql);

    public void SetupJsonTestsReleaseVs(string localDir, string deviceName, string appName, bool isShowSql) =>
        SetupJsonTestsCore(localDir, deviceName, appName, JsonSettings.FileNameReleaseVs, isShowSql);

    public void SetupJsonWebApp(string localDir, string appName)
    {
        try
        {
            AppVersion.Setup(Assembly.GetExecutingAssembly());
            FileLogger.Setup(localDir, appName);
            string subDir = Path.Combine(localDir, JsonSettings.BlazorSubDir);
            if (Directory.Exists(subDir))
            {
                // Local folder.
                FileLogger.Setup(subDir, appName);
                CheckJsonUpdates(subDir, JsonSettings.JsonFileName);
                if (!SetupJsonSettingsCore(subDir, false, JsonSettings.JsonFileName))
                    throw new(LocaleCore.System.JsonSettingsLocalFileException);
            }
            else
            {
                // IIS publish folder.
                CheckJsonUpdates(localDir, JsonSettings.JsonFileName);
                if (!SetupJsonSettingsCore(localDir, false, JsonSettings.JsonFileName))
                    throw new(LocaleCore.System.JsonSettingsLocalFileException);
            }

            AccessCore.SetupSessionFactory(DebugHelper.Instance.IsDevelop);
            ContextItem.SetupLog(appName);
            ContextItem.SaveLogInformation(LocaleCore.DeviceControl.WebAppIsStarted);
        }
        catch (Exception ex)
        {
            FileLogger.StoreException(ex);
        }
    }

    private void CheckJsonUpdates(string localDir, string fileName)
    {
        switch (DebugHelper.Instance.Config)
        {
            case WsConfiguration.DevelopAleksandrov:
            case WsConfiguration.DevelopMorozov:
            case WsConfiguration.ReleaseAleksandrov:
            case WsConfiguration.ReleaseMorozov:
                return;
            case WsConfiguration.DevelopVS:
            case WsConfiguration.ReleaseVS:
                break;
        }

        if (!Directory.Exists(JsonSettings.RemoteDir))
        {
            throw new(LocaleCore.System.JsonSettingsRemoteFolderNotFound);
        }
        string remoteFile = Path.Combine(JsonSettings.RemoteDir, fileName);
        if (!Directory.Exists(JsonSettings.RemoteDir))
        {
            throw new(LocaleCore.System.JsonSettingsRemoteFileNotFound);
        }

        SetupJsonSettingsCore(JsonSettings.RemoteDir, true, fileName);

        ushort version = GetJsonLocalVersion(localDir, fileName);
        if (version == 0 || version < JsonSettings.Remote.Version)
        {
            string localFile = Path.Combine(localDir, fileName);
            if (File.Exists(localFile))
                File.Delete(localFile);

            using StreamReader streamReader = File.OpenText(remoteFile);
            string content = streamReader.ReadToEnd();
            streamReader.Close();
            streamReader.Dispose();
            if (string.IsNullOrEmpty(content))
            {
                //if (isFileLog)
                //    FileLog.WriteMessage(LocaleCore.System.JsonSettingsFileIsEmpty(remoteFile));
                throw new(LocaleCore.System.JsonSettingsFileIsEmpty(remoteFile));
            }

            using StreamWriter streamWriter = File.CreateText(Path.Combine(localDir, fileName));
            streamWriter.Write(content);
            streamWriter.Close();
            streamWriter.Dispose();
        }
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

    public List<SqlDbFileSizeInfoModel> GetDbFileSizeInfos() => ContextCore.GetDbFileSizeInfos();
    
    public ushort GetDbFileSizeAll() => ContextCore.GetDbFileSizeAll();

    public List<WsSqlTableBase> GetTableModels() => ContextCore.GetTableModels();

    public List<Type> GetTableTypes() => ContextCore.GetTableTypes();

    public List<Type> GetTableMaps() => ContextCore.GetTableMaps();

    public List<Type> GetTableValidators() => ContextCore.GetTableValidators();

    #endregion
}