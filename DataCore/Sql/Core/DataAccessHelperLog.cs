// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;

namespace DataCore.Sql.Core;

public static class DataAccessHelperLog
{
	#region Public and private fields, properties, constructor

	private static AppVersionHelper AppVersion { get; } = AppVersionHelper.Instance;
	private static AppModel? App { get; set; }
	private static DeviceModel Device { get; set; }

	#endregion

	#region Public and private methods

	public static void SetupLog(this DataAccessHelper dataAccess, string deviceName, string appName)
	{
		DeviceModel? device = dataAccess.GetItemDevice(deviceName);

		if (device?.IdentityIsNotNew == true)
			Device = device;

		AppModel? app = dataAccess.GetOrCreateNewApp(appName);
		if (app is not null && !app.EqualsDefault())
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

	public static void LogError(this DataAccessHelper dataAccess, Exception ex, string? hostName = null, 
		string? appName = null, [CallerFilePath] string filePath = "", 
		[CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
	{
		LogCore(dataAccess, ex.Message, LogTypeEnum.Error, hostName, appName, filePath, lineNumber, memberName);
		if (ex.InnerException is not null)
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

	private static void LogCore(this DataAccessHelper dataAccess, string message, LogTypeEnum logType, string deviceName, string appName, string filePath, int lineNumber, string memberName)
	{
		StringUtils.SetStringValueTrim(ref filePath, 32, true);
		StringUtils.SetStringValueTrim(ref memberName, 32);
		StringUtils.SetStringValueTrim(ref message, 1024);
        LogTypeModel? logTypeItem = dataAccess.GetItemLogType(logType);

        DeviceModel device = Device;
		AppModel? app = App;

		if (!string.IsNullOrEmpty(deviceName))
			device = dataAccess.GetItemDeviceNotNull(deviceName);
		if (!string.IsNullOrEmpty(appName))
			app = dataAccess.GetOrCreateNewApp(appName);

		LogModel log = new()
		{
			CreateDt = DateTime.Now,
			ChangeDt = DateTime.Now,
			IsMarked = false,
			Device = device,
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
		return app.IdentityValueUid;
	}

	#endregion
}
