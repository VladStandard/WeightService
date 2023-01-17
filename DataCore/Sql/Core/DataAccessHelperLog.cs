// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;
using DataCore.Protocols;
using DataCore.Sql.TableScaleModels.Apps;
using DataCore.Sql.TableScaleModels.Devices;
using DataCore.Sql.TableScaleModels.Logs;
using DataCore.Sql.TableScaleModels.LogsTypes;

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
		if (Device.IsNew)
		{
			if (string.IsNullOrEmpty(deviceName))
				deviceName = NetUtils.GetLocalDeviceName(false);
			Device = GetItemDeviceOrCreateNew(deviceName);
		}

		if (App.IsNew)
		{
			if (string.IsNullOrEmpty(appName))
				appName = nameof(DataCore);
			App = GetItemAppOrCreateNew(appName);
		}
    }

    private void LogCore(string message, LogTypeEnum logType, string deviceName, string appName, string filePath, int lineNumber, string memberName)
    {
        SetupLog(deviceName, appName);
        LogCoreFast(message, logType, filePath, lineNumber, memberName);
    }

    private void LogCoreFast(string message, LogTypeEnum logType, string filePath, int lineNumber, string memberName)
    {
        StringUtils.SetStringValueTrim(ref filePath, 32, true);
        StringUtils.SetStringValueTrim(ref memberName, 32);
        StringUtils.SetStringValueTrim(ref message, 1024);
        LogTypeModel? logTypeItem = GetItemLogTypeNullable(logType);

        LogModel log = new()
        {
            CreateDt = DateTime.Now,
            ChangeDt = DateTime.Now,
            IsMarked = false,
            Device = Device,
            App = App,
            LogType = logTypeItem,
            Version = AppVersion.Version,
            File = filePath,
            Line = lineNumber,
            Member = memberName,
            Message = message,
        };
        SaveAsync(log).ConfigureAwait(false);
    }

	public void LogErrorFast(Exception ex, [CallerFilePath] string filePath = "",
		[CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    {
        LogCoreFast(ex.Message, LogTypeEnum.Error, filePath, lineNumber, memberName);
        if (ex.InnerException is not null)
            LogCoreFast(ex.InnerException.Message, LogTypeEnum.Error, filePath, lineNumber, memberName);
    }

	public void LogErrorFast(string message, string filePath, int lineNumber, string memberName)
    {
        LogCoreFast(message, LogTypeEnum.Error, filePath, lineNumber, memberName);
    }

    public void LogError(Exception ex, string deviceName, string appName, [CallerFilePath] string filePath = "", 
		[CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
	{
		LogCore(ex.Message, LogTypeEnum.Error, deviceName, appName, filePath, lineNumber, memberName);
		if (ex.InnerException is not null)
			LogCore(ex.InnerException.Message, LogTypeEnum.Error, deviceName, appName, filePath, lineNumber, memberName);
	}

	public void LogError(string message, string deviceName, string appName,
		[CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
	{
		LogCore(message, LogTypeEnum.Error, deviceName, appName, filePath, lineNumber, memberName);
	}

	public void LogStop(string message, string deviceName, string appName,
		[CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
		LogCore(message, LogTypeEnum.Stop, deviceName, appName, filePath, lineNumber, memberName);

	public void LogStopFast(string message, 
		[CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
		LogCoreFast(message, LogTypeEnum.Stop, filePath, lineNumber, memberName);

	public void LogInformation(string message, string deviceName, string appName,
		[CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
		LogCore(message, LogTypeEnum.Information, deviceName, appName, filePath, lineNumber, memberName);

	public void LogInformationFast(string message, 
		[CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
		LogCoreFast(message, LogTypeEnum.Information, filePath, lineNumber, memberName);

	public void LogWarning(string message, string deviceName, string appName,
		[CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
		LogCore(message, LogTypeEnum.Warning, deviceName, appName, filePath, lineNumber, memberName);

	public void LogWarningFast(string message, 
		[CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
		LogCoreFast(message, LogTypeEnum.Warning, filePath, lineNumber, memberName);

	public void LogQuestion(string message, string deviceName, string appName,
		[CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
		LogCore(message, LogTypeEnum.Question, deviceName, appName, filePath, lineNumber, memberName);

	public void LogQuestionFast(string message, 
		[CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
		LogCoreFast(message, LogTypeEnum.Question, filePath, lineNumber, memberName);

	#endregion
}