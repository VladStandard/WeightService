// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;

namespace DataCore.Sql.Core;

public static class DataAccessHelperLog
{
	#region Public and private fields, properties, constructor

	private static AppVersionHelper AppVersion { get; } = AppVersionHelper.Instance;
	private static AppModel? App { get; set; }
	private static HostModel? Host { get; set; }

	#endregion

	#region Public and private methods

	public static void SetupLog(this DataAccessHelper dataAccess, string? hostName, string? appName)
	{
		HostModel? host = dataAccess.GetItemHost(hostName);

		if (host != null && !host.EqualsDefault())
			Host = host;

		AppModel? app = dataAccess.GetOrCreateNewApp(appName);
		if (app != null && !app.EqualsDefault())
			App = app;
	}

	public static void LogToFile(this DataAccessHelper dataAccess, string localFileLog, string message,
		[CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
	{
		StreamWriter streamWriter = File.Exists(localFileLog) ? File.AppendText(localFileLog) : File.CreateText(localFileLog);
		streamWriter.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {nameof(filePath)}: {filePath}. {nameof(lineNumber)}: {lineNumber}. {nameof(memberName)}: {memberName}");
		streamWriter.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {nameof(message)}: {message}");
		streamWriter.Close();
		streamWriter.Dispose();
	}

	public static void LogError(this DataAccessHelper dataAccess, Exception ex, string? hostName = null, string? appName = null,
		[CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
	{
		LogCore(dataAccess, ex.Message, LogTypeEnum.Error, hostName, appName, filePath, lineNumber, memberName);
		if (ex.InnerException != null)
			LogCore(dataAccess, ex.InnerException.Message, LogTypeEnum.Error, hostName, appName, filePath, lineNumber, memberName);
	}

	public static void LogError(this DataAccessHelper dataAccess, string message, string? hostName = null, string? appName = null,
		[CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
	{
		LogCore(dataAccess, message, LogTypeEnum.Error, hostName, appName, filePath, lineNumber, memberName);
	}

	public static void LogStop(this DataAccessHelper dataAccess, string message, string? hostName = null, string? appName = null,
		[CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
		LogCore(dataAccess, message, LogTypeEnum.Stop, hostName, appName, filePath, lineNumber, memberName);

	public static void LogInformation(this DataAccessHelper dataAccess, string message, string? hostName = null, string? appName = null,
		[CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
		LogCore(dataAccess, message, LogTypeEnum.Information, hostName, appName, filePath, lineNumber, memberName);

	public static void LogWarning(this DataAccessHelper dataAccess, string message, string? hostName = null, string? appName = null,
		[CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
		LogCore(dataAccess, message, LogTypeEnum.Warning, hostName, appName, filePath, lineNumber, memberName);

	private static void LogCore(this DataAccessHelper dataAccess, string message, LogTypeEnum logType, string? hostName, string? appName, string filePath, int lineNumber, string memberName)
	{
		StringUtils.SetStringValueTrim(ref filePath, 32, true);
		StringUtils.SetStringValueTrim(ref memberName, 32);
		byte logNumber = (byte)logType;
		StringUtils.SetStringValueTrim(ref message, 1024);
		SqlCrudConfigModel sqlCrudConfig = new(new() { new(SqlFieldEnum.Number, SqlFieldComparerEnum.Equal, logNumber) }, null, 0);
		LogTypeModel? logTypeItem = dataAccess.GetItem<LogTypeModel>(sqlCrudConfig);

		HostModel? host = Host;
		AppModel? app = App;

		if (!string.IsNullOrEmpty(hostName))
			host = dataAccess.GetItemHost(hostName);
		if (!string.IsNullOrEmpty(appName))
			app = dataAccess.GetOrCreateNewApp(appName);

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
		dataAccess.Save(log);
	}

	public static void LogQuestion(this DataAccessHelper dataAccess, string message, string? hostName, string? appName,
		[CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
	{
		LogCore(dataAccess, message, LogTypeEnum.Question, hostName, appName, filePath, lineNumber, memberName);
	}

	public static Guid SaveApp(this DataAccessHelper dataAccess, string name)
	{
		StringUtils.SetStringValueTrim(ref name, 32);
		AppModel app = new() { Name = name };
		dataAccess.Save(app);
		return app.Identity.Uid;
	}

	public static long? GetHostId(this DataAccessHelper dataAccess, string name)
	{
		StringUtils.SetStringValueTrim(ref name, 150);
		SqlCrudConfigModel sqlCrudConfig = new(new() { new(SqlFieldEnum.Name, SqlFieldComparerEnum.Equal, name) }, null, 0);
		HostModel? host = dataAccess.GetItem<HostModel>(sqlCrudConfig);
		return host?.Identity.Id;
	}

	#endregion
}
