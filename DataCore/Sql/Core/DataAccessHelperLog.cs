// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;

namespace DataCore.Sql.Core;

public partial class DataAccessHelper
{
	#region Public and private fields, properties, constructor

	private static AppVersionHelper AppVersion { get; } = AppVersionHelper.Instance;
	private static AppModel App { get; set; } = new();
	private static DeviceModel Device { get; set; } = new();

	#endregion

	#region Public and private methods

	public void SetupLog(string deviceName, string appName)
	{
		DeviceModel device = GetItemDeviceNotNull(deviceName);

		if (device.IdentityIsNotNew)
			Device = device;

		AppModel app = GetItemAppOrCreateNew(appName);
		if (app.IdentityIsNotNew)
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

	public void LogError(Exception ex, string? deviceName = null, 
		string? appName = null, [CallerFilePath] string filePath = "", 
		[CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
	{
		LogCore(ex.Message, LogTypeEnum.Error, deviceName, appName, filePath, lineNumber, memberName);
		if (ex.InnerException is not null)
			LogCore(ex.InnerException.Message, LogTypeEnum.Error, deviceName, appName, filePath, lineNumber, memberName);
	}

	public void LogError(string message, string? deviceName = null, string? appName = null,
		[CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
	{
		LogCore(message, LogTypeEnum.Error, deviceName, appName, filePath, lineNumber, memberName);
	}

	public void LogStop(string message, string? deviceName = null, string? appName = null,
		[CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
		LogCore(message, LogTypeEnum.Stop, deviceName, appName, filePath, lineNumber, memberName);

	public void LogInformation(string message, string? deviceName = null, string? appName = null,
		[CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
		LogCore(message, LogTypeEnum.Information, deviceName, appName, filePath, lineNumber, memberName);

	public void LogWarning(string message, string? deviceName = null, string? appName = null,
		[CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
		LogCore(message, LogTypeEnum.Warning, deviceName, appName, filePath, lineNumber, memberName);

	private void LogCore(string message, LogTypeEnum logType, string deviceName, string appName, string filePath, int lineNumber, string memberName)
	{
		StringUtils.SetStringValueTrim(ref filePath, 32, true);
		StringUtils.SetStringValueTrim(ref memberName, 32);
		StringUtils.SetStringValueTrim(ref message, 1024);
        LogTypeModel? logTypeItem = GetItemLogType(logType);

        DeviceModel device = Device;
		AppModel app = App;

		if (!string.IsNullOrEmpty(deviceName))
			device = GetItemDeviceNotNull(deviceName);
		if (!string.IsNullOrEmpty(appName))
			app = GetItemAppOrCreateNew(appName);

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
		Save(log);
	}

	public void LogQuestion(string message, string? deviceName, string? appName,
		[CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
	{
		LogCore(message, LogTypeEnum.Question, deviceName, appName, filePath, lineNumber, memberName);
	}

	public Guid SaveApp(string name)
	{
		StringUtils.SetStringValueTrim(ref name, 32);
		AppModel app = new() { Name = name };
		Save(app);
		return app.IdentityValueUid;
	}

	#endregion
}
