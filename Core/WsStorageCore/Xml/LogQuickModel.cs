// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Xml;

[Serializable]
public class LogQuickModel : WsSqlTableBase
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
	public LogQuickModel() : base(WsSqlFieldIdentity.Uid)
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
		$"{nameof(Message)}: {Message}. ";

	public override bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
		return Equals((LogQuickModel)obj);
	}

	public override int GetHashCode() => base.GetHashCode();

	public override bool EqualsNew() => Equals(new());

	public override bool EqualsDefault() =>
		base.EqualsDefault() &&
		Equals(Line, string.Empty) &&
		Equals(Host, string.Empty) &&
		Equals(App, string.Empty) &&
		Equals(Version, string.Empty) &&
		Equals(File, string.Empty) &&
		Equals(CodeLine, 0) &&
		Equals(Member, string.Empty) &&
		Equals(LogType, string.Empty) &&
		Equals(Message, string.Empty);

	public override object Clone()
	{
		LogQuickModel item = new();
		item.CloneSetup(base.CloneCast());
		item.Line = Line;
		item.Host = Host;
		item.App = App;
		item.Version = Version;
		item.File = File;
		item.CodeLine = CodeLine;
		item.Member = Member;
		item.LogType = LogType;
		item.Message = Message;
		return item;
	}

	#endregion

	#region Public and private methods - virtual

	public virtual bool Equals(LogQuickModel item) =>
		ReferenceEquals(this, item) || base.Equals(item) && //-V3130
		Equals(CreateDt, item.CreateDt) &&
		Equals(Line, item.Line) &&
		Equals(Host, item.Host) &&
		Equals(App, item.App) &&
		Equals(Version, item.Version) &&
		Equals(File, item.File) &&
		Equals(CodeLine, item.CodeLine) &&
		Equals(Member, item.Member) &&
		Equals(LogType, item.LogType) &&
		Equals(Message, item.Message);

	public new virtual LogQuickModel CloneCast() => (LogQuickModel)Clone();

	#endregion
}
