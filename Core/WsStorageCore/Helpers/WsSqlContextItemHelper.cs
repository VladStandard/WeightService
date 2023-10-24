namespace WsStorageCore.Helpers;

/// <summary>
/// SQL-контроллер таблицы записей.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlContextItemHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlContextItemHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlContextItemHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;
    public WsSqlAppEntity App { get; private set; } = new();
    public WsSqlDeviceEntity Device { get; private set; } = new();

    #endregion

    #region Public and private methods - Logs

    public void SetupLog(string deviceName, string appName)
    {
        WsSqlDeviceRepository deviceRepository = new();
        WsSqlAppRepository appRepository = new(); 
        if (Device.IsNew)
        {
            if (string.IsNullOrEmpty(deviceName))
                deviceName = MdNetUtils.GetLocalDeviceName(false);
            Device = deviceRepository.GetItemByName(deviceName);
        }

        if (App.IsNew)
        {
            if (string.IsNullOrEmpty(appName))
                appName = nameof(WsDataCore);
            App = appRepository.GetItemByNameOrCreate(appName);
        }
    }

    private void SaveLogCore(StringBuilder message, WsEnumLogType logType, string filePath, int lineNumber,
        string memberName) =>
        SaveLogCore(message.ToString(), logType, filePath, lineNumber, memberName);

    private void SaveLogCore(string message, WsEnumLogType logType, string filePath, int lineNumber, string memberName)
    {
        WsStrUtils.SetStringValueTrim(ref filePath, 32, true);
        WsStrUtils.SetStringValueTrim(ref memberName, 32);
        WsStrUtils.SetStringValueTrim(ref message, 1024);
        WsSqlLogTypeEntity logTypeItem = new WsSqlLogTypeRepository().GetItemByEnumType(logType);

        WsSqlLogEntity log = new()
        {
            CreateDt = DateTime.Now,
            ChangeDt = DateTime.Now,
            IsMarked = false,
            Device = Device,
            App = App,
            LogType = logTypeItem,
            Version = WsAppVersionHelper.Instance.Version,
            File = filePath,
            Line = lineNumber,
            Member = memberName,
            Message = message
        };
        SqlCore.Save(log, WsSqlEnumSessionType.IsolatedAsync);
    }

    public void SaveLogErrorWithInfo(Exception ex, string filePath, int lineNumber, string memberName)
    {
        string message = ex.Message;
        if (ex.InnerException is not null) message += " | " + ex.InnerException.Message;
        SaveLogCore(message, WsEnumLogType.Error, filePath, lineNumber, memberName);
    }

    public void SaveLogErrorWithInfo(Exception ex, string description, string filePath, int lineNumber, string memberName)
    {
        string message = ex.Message;
        if (ex.InnerException is not null) message += " | " + ex.InnerException.Message;
        SaveLogCore($"{description} | {message}", WsEnumLogType.Error, filePath, lineNumber, memberName);
    }

    public void SaveLogErrorWithInfo(string message, string filePath, int lineNumber, string memberName) =>
        SaveLogCore(message, WsEnumLogType.Error, filePath, lineNumber, memberName);

    public void SaveLogError(Exception ex,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        SaveLogErrorWithInfo(ex, filePath, lineNumber, memberName);

    public void SaveLogErrorWithDescription(Exception ex, string description,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        SaveLogErrorWithInfo(ex, description, filePath, lineNumber, memberName);

    public void SaveLogError(string message,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        SaveLogCore(message, WsEnumLogType.Error, filePath, lineNumber, memberName);

    public void SaveLogStop(string message,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        SaveLogCore(message, WsEnumLogType.Stop, filePath, lineNumber, memberName);
    
    public void SaveLogInformation(string message,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        SaveLogCore(message, WsEnumLogType.Information, filePath, lineNumber, memberName);
    
    public void SaveLogInformation(StringBuilder message,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        SaveLogCore(message, WsEnumLogType.Information, filePath, lineNumber, memberName);

    public void SaveLogInformationWithDescription(string message, string description,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        SaveLogCore($"{description} | {message}", WsEnumLogType.Information, filePath, lineNumber, memberName);

    public void SaveLogWarning(string message,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        SaveLogCore(message, WsEnumLogType.Warning, filePath, lineNumber, memberName);

    public void SaveLogQuestion(string message,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        SaveLogCore(message, WsEnumLogType.Question, filePath, lineNumber, memberName);

    #endregion

    #region Public and private methods - LogMemory

    public void SetupLog(string appName) => SetupLog("", appName);

    #endregion
    
}