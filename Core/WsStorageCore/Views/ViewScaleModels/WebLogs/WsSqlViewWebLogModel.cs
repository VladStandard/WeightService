namespace WsStorageCore.Views.ViewScaleModels.WebLogs;

[DebuggerDisplay("{ToString()}")]
public class WsSqlViewWebLogModel : WsSqlTableBase
{
	#region Public and private fields, properties, constructor

	public virtual string RequestUrl { get; set; }
	public virtual int RequestCountAll { get; set; }
    public virtual int ResponseCountSuccess { get; set; }
	public virtual int ResponseCountError { get; set; }
    public virtual string LogType { get; set; }
	public virtual string AppVersion { get; set; }


	public WsSqlViewWebLogModel() : base(WsSqlEnumFieldIdentity.Uid)
	{
		RequestUrl = string.Empty;
		RequestCountAll = 0;
        ResponseCountSuccess = 0;
		ResponseCountError = 0;
        LogType = string.Empty;
		AppVersion = string.Empty;
	}

	#endregion

	#region Public and private methods - override

	public override string ToString() =>
		$"{nameof(RequestUrl)}: {RequestUrl}. " +
		$"{nameof(RequestCountAll)}: {RequestCountAll}. " +
		$"{nameof(ResponseCountError)}: {ResponseCountError}. " +
        $"{nameof(LogType)}: {LogType}. " +
		$"{nameof(AppVersion)}: {AppVersion}. ";
    
	#endregion
    
}
