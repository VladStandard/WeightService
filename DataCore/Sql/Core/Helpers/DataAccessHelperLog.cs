// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Protocols;
using DataCore.Sql.TableDiagModels.Logs;
using DataCore.Sql.TableDiagModels.LogsTypes;

namespace DataCore.Sql.Core.Helpers;

public partial class DataAccessHelper
{
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

    public void SetupLog(string appName) => SetupLog("", appName);

    private void LogCore(string message, LogType logType, string filePath, int lineNumber, string memberName)
    {
        StrUtils.SetStringValueTrim(ref filePath, 32, true);
        StrUtils.SetStringValueTrim(ref memberName, 32);
        StrUtils.SetStringValueTrim(ref message, 1024);
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
            Message = message
        };
        SaveAsync(log).ConfigureAwait(false);
    }

    public void LogErrorWithInfo(Exception ex, string filePath, int lineNumber, string memberName)
	{
		LogCore(ex.Message, LogType.Error, filePath, lineNumber, memberName);
		if (ex.InnerException is not null)
			LogCore(ex.InnerException.Message, LogType.Error, filePath, lineNumber, memberName);
	}

    public void LogError(Exception ex, 
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        LogErrorWithInfo(ex, filePath, lineNumber, memberName);

    public void LogError(string message, 
		[CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        LogCore(message, LogType.Error, filePath, lineNumber, memberName);

    public void LogStop(string message, 
		[CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
		LogCore(message, LogType.Stop, filePath, lineNumber, memberName);

	public void LogInformation(string message, 
		[CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
		LogCore(message, LogType.Information, filePath, lineNumber, memberName);

	public void LogWarning(string message, 
		[CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
		LogCore(message, LogType.Warning, filePath, lineNumber, memberName);

	public void LogQuestion(string message, 
		[CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
		LogCore(message, LogType.Question, filePath, lineNumber, memberName);

	#endregion
}
