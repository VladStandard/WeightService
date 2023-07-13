// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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
    public WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;
    private WsSqlContextCoreHelper ContextCore => WsSqlContextCoreHelper.Instance;
    public WsJsonSettingsHelper JsonSettings => WsJsonSettingsHelper.Instance;
    public WsSqlAccessRepository ContextAccess => WsSqlAccessRepository.Instance;
    public WsSqlAreaRepository ContextAreas => WsSqlAreaRepository.Instance;
    public WsSqlBoxRepository ContextBoxes => WsSqlBoxRepository.Instance;
    public WsSqlBrandRepository ContextBrands => WsSqlBrandRepository.Instance;
    public WsSqlBundleRepository ContextBundles => WsSqlBundleRepository.Instance;
    public WsSqlClipRepository ContextClips => WsSqlClipRepository.Instance;
    public WsSqlContextItemHelper ContextItem => WsSqlContextItemHelper.Instance;
    public WsSqlContextListHelper ContextList => WsSqlContextListHelper.Instance;
    public WsSqlContextViewHelper ContextView => WsSqlContextViewHelper.Instance;
    public WsSqlDeviceLineFkRepository ContextDevicesLines => WsSqlDeviceLineFkRepository.Instance;
    public WsSqlLineRepository ContextLines => WsSqlLineRepository.Instance;
    public WsSqlPlu1CRepository ContextPlu1CFk => WsSqlPlu1CRepository.Instance;
    public WsSqlPluFkRepository ContextPlusFk => WsSqlPluFkRepository.Instance;
    public WsSqlPluLabelRepository ContextPlusLabels => WsSqlPluLabelRepository.Instance;
    public WsSqlPluBrandFkRepository ContextPluBrandsFk => WsSqlPluBrandFkRepository.Instance;
    public WsSqlPluBundleFkRepository ContextPluBundlesFk => WsSqlPluBundleFkRepository.Instance;
    public WsSqlPluClipFkRepository ContextPlusClipsFk => WsSqlPluClipFkRepository.Instance;
    public WsSqlPluCharacteristicRepository ContextPluCharacteristics => WsSqlPluCharacteristicRepository.Instance;
    public WsSqlPluCharacteristicsFkRepository ContextPluCharacteristicsFk => WsSqlPluCharacteristicsFkRepository.Instance;
    public WsSqlPluRepository ContextPlus => WsSqlPluRepository.Instance;
    public WsSqlPluLineRepository ContextPlusLines => WsSqlPluLineRepository.Instance;
    public WsSqlPluNestingFkRepository ContextPlusNesting => WsSqlPluNestingFkRepository.Instance;
    public WsSqlPluStorageMethodFkRepository ContextPlusStorages => WsSqlPluStorageMethodFkRepository.Instance;
    public WsSqlPluWeighingRepository ContextPlusWeighing => WsSqlPluWeighingRepository.Instance;
    public WsSqlTemplateRepository ContextTemplates => WsSqlTemplateRepository.Instance;
    public FluentNHibernate.Cfg.Db.MsSqlConfiguration? SqlConfiguration => SqlCore.SqlConfiguration;

    #endregion

    #region Public and private methods

    private bool SetupJsonSettingsCore(string dir, bool isRemote, string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
            throw new ArgumentException("Value must be fill!", nameof(fileName));

        string file = Path.Combine(dir, fileName);
        if (!File.Exists(file))
        {
            throw new(WsLocaleCore.System.JsonSettingsFileNotFound(file));
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
            //FileLogger.Setup(localDir, appName);
            CheckJsonUpdates(localDir, JsonSettings.JsonFileName);

            if (!SetupJsonSettingsCore(localDir, false, JsonSettings.JsonFileName))
            {
                throw new(WsLocaleCore.System.JsonSettingsLocalFileException);
            }

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
        CheckJsonUpdates(localDir, fileName);

        if (!SetupJsonSettingsCore(localDir, false, fileName))
            throw new(WsLocaleCore.System.JsonSettingsLocalFileException);

        SqlCore.SetSessionFactory(isShowSql);
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
                CheckJsonUpdates(subDir, JsonSettings.JsonFileName);
                if (!SetupJsonSettingsCore(subDir, false, JsonSettings.JsonFileName))
                    throw new(WsLocaleCore.System.JsonSettingsLocalFileException);
            }
            else
            {
                // IIS publish folder.
                CheckJsonUpdates(localDir, JsonSettings.JsonFileName);
                if (!SetupJsonSettingsCore(localDir, false, JsonSettings.JsonFileName))
                    throw new(WsLocaleCore.System.JsonSettingsLocalFileException);
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

    private void CheckJsonUpdates(string localDir, string fileName)
    {
        switch (WsDebugHelper.Instance.Config)
        {
            case WsEnumConfiguration.DevelopAleksandrov:
            case WsEnumConfiguration.DevelopMorozov:
            case WsEnumConfiguration.ReleaseAleksandrov:
            case WsEnumConfiguration.ReleaseMorozov:
                return;
            case WsEnumConfiguration.DevelopVS:
            case WsEnumConfiguration.ReleaseVS:
                break;
        }

        if (!Directory.Exists(JsonSettings.RemoteDir))
        {
            throw new(WsLocaleCore.System.JsonSettingsRemoteFolderNotFound);
        }
        string remoteFile = Path.Combine(JsonSettings.RemoteDir, fileName);
        if (!Directory.Exists(JsonSettings.RemoteDir))
        {
            throw new(WsLocaleCore.System.JsonSettingsRemoteFileNotFound);
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
                throw new(WsLocaleCore.System.JsonSettingsFileIsEmpty(remoteFile));
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

    public List<WsSqlDbFileSizeInfoModel> GetDbFileSizeInfos() => ContextCore.GetDbFileSizeInfos();
    
    public ushort GetDbFileSizeAll() => ContextCore.GetDbFileSizeAll();

    public List<WsSqlTableBase> GetTableModels() => ContextCore.GetTableModels();

    public List<Type> GetTableTypes() => ContextCore.GetTableTypes();

    public List<Type> GetTableMaps() => ContextCore.GetTableMaps();

    public List<Type> GetTableValidators() => ContextCore.GetTableValidators();

    #endregion
}