using WsStorageCore.Entities.SchemaRef.Hosts;
using WsStorageCore.Enums;

namespace WsStorageCore.Helpers;

/// <summary>
/// SQL-контроллер таблицы записей.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class SqlContextItemHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static SqlContextItemHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static SqlContextItemHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private SqlCoreHelper SqlCore => SqlCoreHelper.Instance;
    private SqlAppEntity App { get; set; } = new();
    private SqlHostEntity Host { get; set; } = new();

    #endregion

    #region Public and private methods - Logs


    #region Private

    public void SetupLog(string deviceName, string appName)
    {
        SqlHostRepository hostRepository = new();
        SqlAppRepository appRepository = new(); 
        if (Host.IsNew)
        {
            if (string.IsNullOrEmpty(deviceName))
                deviceName = MdNetUtils.GetLocalDeviceName(false);
            Host = hostRepository.GetItemByName(deviceName);
        }

        if (App.IsNew)
        {
            if (string.IsNullOrEmpty(appName))
                appName = nameof(WsDataCore);
            App = appRepository.GetItemByNameOrCreate(appName);
        }
    }
    
    public void SetupLog(string appName) => SetupLog("", appName);
    
    private void SaveLogCore(string message, LogTypeEnum logType, string filePath, int lineNumber, string memberName)
    {
        WsStrUtils.SetStringValueTrim(ref filePath, 32, true);
        WsStrUtils.SetStringValueTrim(ref memberName, 32);
        WsStrUtils.SetStringValueTrim(ref message, 1024);

        SqlLogEntity log = new()
        {
            CreateDt = DateTime.Now,
            ChangeDt = DateTime.Now,
            IsMarked = false,
            Device = Host,
            App = App,
            Type = logType,
            Version = WsAppVersionHelper.Instance.Version,
            File = filePath,
            Line = lineNumber,
            Member = memberName,
            Message = message
        };
        SqlCore.Save(log, SqlEnumSessionType.IsolatedAsync);
    }

    #endregion
    
    public void SaveLogErrorWithInfo(Exception ex, string filePath, int lineNumber, string memberName)
    {
        string message = ex.Message;
        if (ex.InnerException is not null) message += " | " + ex.InnerException.Message;
        SaveLogCore(message, LogTypeEnum.Error, filePath, lineNumber, memberName);
    }
    
    public void SaveLogError(Exception ex, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        SaveLogErrorWithInfo(ex, filePath, lineNumber, memberName);

    public void SaveLogErrorWithInfo(string message, string filePath, int lineNumber, string memberName) =>
        SaveLogCore(message, LogTypeEnum.Error, filePath, lineNumber, memberName);

    public void SaveLogErrorWithDescription(Exception ex, string description, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    {
        string message = ex.Message;
        if (ex.InnerException is not null) message += " | " + ex.InnerException.Message;
        SaveLogCore($"{description} | {message}", LogTypeEnum.Error, filePath, lineNumber, memberName);
    }

    public void SaveLogError(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        SaveLogCore(message, LogTypeEnum.Error, filePath, lineNumber, memberName);
    
    public void SaveLogInformation(string message, string description = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    {
        if (description != string.Empty) message = $"{description} | {message}";
        SaveLogCore(message, LogTypeEnum.Info, filePath, lineNumber, memberName);
    }

    public void SaveLogWarning(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        SaveLogCore(message, LogTypeEnum.Warning, filePath, lineNumber, memberName);

    #endregion
}