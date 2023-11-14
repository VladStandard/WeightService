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
    private WsJsonSettingsHelper JsonSettings => WsJsonSettingsHelper.Instance;
    private WsSqlContextItemHelper ContextItem => WsSqlContextItemHelper.Instance;
    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;
    
    #endregion

    #region Public and private methods
    
    public void SetupJsonScales(string localDir, string appName)
    {
        try
        {
            // #WS-T-1105: Fix jitDebugging error for Debug configs.
            CheckMachineConfigUpdates(localDir);
            SqlCore.SetSessionFactory(WsDebugHelper.Instance.IsDevelop);
            ContextItem.SetupLog(appName);
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
        if (!WsDebugHelper.Instance.Config.Equals(WsEnumConfiguration.DevelopVs)) return;

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
    
    private void CheckDirAndFile(string fileName)
    {
        if (!Directory.Exists(JsonSettings.RemoteDir))
            throw new(WsLocaleCore.System.ConfigRemoteFolderNotFound);
        if (!File.Exists(fileName))
            throw new(WsLocaleCore.System.ConfigRemoteFileNotFound);
    }
    

    #endregion
}