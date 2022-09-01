// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core;
using static DataCore.ShareEnums;

namespace DataCore.Sql.Controllers;

public class LogController
{
    #region Public and private fields, properties, constructor

    private AppVersionHelper AppVersion { get; set; } = AppVersionHelper.Instance;
    private AppModel? App { get; set; }
    private DataAccessHelper DataAccess { get; set; } = DataAccessHelper.Instance;
    private HostModel? Host { get; set; }

    #endregion

    #region Public and private methods

    public void Setup(string? hostName, string? appName)
    {
        HostModel? host = DataAccess.Crud.GetItemHost(hostName);

        if (host != null && !host.EqualsDefault())
            Host = host;

        AppModel? app = DataAccess.Crud.GetOrCreateNewApp(appName);
        if (app != null && !app.EqualsDefault())
            App = app;
    }

    public void LogToFile(string localFileLog, string message,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    {
	    StreamWriter streamWriter = File.Exists(localFileLog) ? File.AppendText(localFileLog) : File.CreateText(localFileLog);
        streamWriter.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {nameof(filePath)}: {filePath}. {nameof(lineNumber)}: {lineNumber}. {nameof(memberName)}: {memberName}");
        streamWriter.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {nameof(message)}: {message}");
        streamWriter.Close();
        streamWriter.Dispose();
    }

    public void LogError(Exception ex, string? hostName = null, string? appName = null,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    {
        LogCore(ex.Message, LogType.Error, hostName, appName, filePath, lineNumber, memberName);
        if (ex.InnerException != null)
            LogCore(ex.InnerException.Message, LogType.Error, hostName, appName, filePath, lineNumber, memberName);
    }

    public void LogError(string message, string? hostName = null, string? appName = null,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    {
        LogCore(message, LogType.Error, hostName, appName, filePath, lineNumber, memberName);
    }

    public void LogStop(string message, string? hostName = null, string? appName = null,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        LogCore(message, LogType.Stop, hostName, appName, filePath, lineNumber, memberName);

    public void LogInformation(string message, string? hostName = null, string? appName = null,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        LogCore(message, LogType.Information, hostName, appName, filePath, lineNumber, memberName);

    public void LogWarning(string message, string? hostName = null, string? appName = null,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        LogCore(message, LogType.Warning, hostName, appName, filePath, lineNumber, memberName);

    private void LogCore(string message, LogType logType, string? hostName, string? appName, string filePath, int lineNumber, string memberName)
    {
        StringUtils.SetStringValueTrim(ref filePath, 32, true);
        StringUtils.SetStringValueTrim(ref memberName, 32);
        byte logNumber = (byte)logType;
        StringUtils.SetStringValueTrim(ref message, 1024);
        SqlCrudConfigModel sqlCrudConfig = new(new() { new(DbField.Number, DbComparer.Equal, logNumber) }, null, 0);
        LogTypeModel? logTypeItem = DataAccess.Crud.GetItem<LogTypeModel>(sqlCrudConfig);

        HostModel? host = Host;
        AppModel? app = App;

        if (!string.IsNullOrEmpty(hostName))
            host = DataAccess.Crud.GetItemHost(hostName);
        if (!string.IsNullOrEmpty(appName))
            app = DataAccess.Crud.GetOrCreateNewApp(appName);

        LogModel log = new()
        {
            CreateDt = DateTime.Now,
            ChangeDt = DateTime.Now,
            IsMarked = false,
            Host = host,
            App = app,
            LogType = logTypeItem,
            Version = AppVersion.Version,
            File = filePath,
            Line = lineNumber,
            Member = memberName,
            Message = message,
        };
        DataAccess.Crud.Save(log);
    }

    public void LogQuestion(string message, string? hostName, string? appName,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    {
        LogCore(message, LogType.Question, hostName, appName, filePath, lineNumber, memberName);
    }

    public Guid SaveApp(string name)
    {
        StringUtils.SetStringValueTrim(ref name, 32);
        AppModel app = new() { Name = name };
        DataAccess.Crud.Save(app);
        return app.Identity.Uid;
    }

    public long? GetHostId(string name)
    {
        StringUtils.SetStringValueTrim(ref name, 150);
        SqlCrudConfigModel sqlCrudConfig = new(new() { new(DbField.Name, DbComparer.Equal, name) }, null, 0);
		HostModel? host = DataAccess.Crud.GetItem<HostModel>(sqlCrudConfig);
        return host?.Identity.Id;
    }

    #endregion
}
