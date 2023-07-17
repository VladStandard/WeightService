// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Views.ViewScaleModels.Logs;

[DebuggerDisplay("{ToString()}")]
public class WsSqlViewLogModel : WsSqlTableBase
{
	#region Public and private fields, properties, constructor

	public virtual string Line { get; set; }
	public virtual string Host { get; set; }
    public virtual string HostPrettyName { get; set; }
	public virtual string App { get; set; }
	public virtual string Version { get; set; }
	public virtual string File { get; set; }
	public virtual int CodeLine { get; set; }
	public virtual string Member { get; set; }
	public virtual string LogType { get; set; }
	public virtual string Message { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public WsSqlViewLogModel() : base(WsSqlEnumFieldIdentity.Uid)
	{
		Line = string.Empty;
		Host = string.Empty;
        HostPrettyName = string.Empty;
		App = string.Empty;
		Version = string.Empty;
		File = string.Empty;
        CodeLine = 0;
		Member = string.Empty;
        LogType = string.Empty;
		Message = string.Empty;
	}

	#endregion

	#region Public and private methods - override

	public override string ToString() =>
		$"{nameof(Line)}: {Line}. " +
		$"{nameof(Host)}: {Host}. " +
		$"{nameof(App)}: {App}. " +
		$"{nameof(Version)}: {Version}. " +
		$"{nameof(File)}: {File}. " +
		$"{nameof(CodeLine)}: {CodeLine}. " +
		$"{nameof(Member)}: {Member}. " +
		$"{nameof(LogType)}: {LogType}. " +
		$"{nameof(Message)}: {Message}.";

    #endregion
}
