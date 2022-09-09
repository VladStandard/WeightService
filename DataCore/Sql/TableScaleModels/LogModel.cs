// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "LOGS".
/// </summary>
[Serializable]
public class LogModel : TableBase, ICloneable, ISqlDbBase, ISerializable
{
	#region Public and private fields, properties, constructor

	[XmlElement(IsNullable = true)] public virtual HostModel? Host { get; set; }
	[XmlElement(IsNullable = true)] public virtual AppModel? App { get; set; }
	[XmlElement(IsNullable = true)] public virtual LogTypeModel? LogType { get; set; }
	[XmlElement] public virtual string Version { get; set; }
	[XmlElement] public virtual string File { get; set; }
	[XmlElement] public virtual int Line { get; set; }
	[XmlElement] public virtual string Member { get; set; }
	[XmlElement] public virtual string Message { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public LogModel() : base(SqlFieldIdentityEnum.Uid)
	{
		Host = null;
		App = null;
		LogType = null;
		Version = string.Empty;
		File = string.Empty;
		Line = 0;
		Member = string.Empty;
		Message = string.Empty;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
	private LogModel(SerializationInfo info, StreamingContext context) : base(info, context)
	{
		Host = (HostModel?)info.GetValue(nameof(Host), typeof(HostModel));
		App = (AppModel?)info.GetValue(nameof(App), typeof(AppModel));
		LogType = (LogTypeModel?)info.GetValue(nameof(LogType), typeof(LogTypeModel));
		Version = info.GetString(nameof(Version));
		File = info.GetString(nameof(File));
		Line = info.GetInt32(nameof(Line));
		Member = info.GetString(nameof(Member));
		Message = info.GetString(nameof(Message));
	}

	#endregion

	#region Public and private methods - override

	public override string ToString() =>
		$"{nameof(IsMarked)}: {IsMarked}. " +
		$"{nameof(Host)}: {Host?.Name ?? "null"}. " +
		$"{nameof(App)}: {App?.Name ?? "null"}. " +
		$"{nameof(LogType)}: {LogType?.Icon ?? "null"}. " +
		$"{nameof(Version)}: {Version}. " +
		$"{nameof(File)}: {File}. " +
		$"{nameof(Line)}: {Line}. " +
		$"{nameof(Member)}: {Member}. " +
		$"{nameof(Message)}: {Message}. ";

	public override bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
		return Equals((LogModel)obj);
	}

	public override int GetHashCode() => base.GetHashCode();

	public override bool EqualsNew() => Equals(new());

	public override bool EqualsDefault()
	{
		if (Host != null && !Host.EqualsDefault())
			return false;
		if (App != null && !App.EqualsDefault())
			return false;
		if (LogType != null && !LogType.EqualsDefault())
			return false;
		return base.EqualsDefault() &&
			   Equals(Version, string.Empty) &&
			   Equals(File, string.Empty) &&
			   Equals(Line, 0) &&
			   Equals(Member, string.Empty) &&
			   Equals(Message, string.Empty);
	}

	public override object Clone()
	{
		LogModel item = new();
		item.Host = Host?.CloneCast();
		item.App = App?.CloneCast();
		item.LogType = LogType?.CloneCast();
		item.Version = Version;
		item.File = File;
		item.Line = Line;
		item.Member = Member;
		item.Message = Message;
		item.CloneSetup(base.CloneCast());
		return item;
	}

	/// <summary>
	/// Get object data for serialization info.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		base.GetObjectData(info, context);
		info.AddValue(nameof(Host), Host);
		info.AddValue(nameof(App), App);
		info.AddValue(nameof(LogType), LogType);
		info.AddValue(nameof(Version), Version);
		info.AddValue(nameof(File), File);
		info.AddValue(nameof(Line), Line);
		info.AddValue(nameof(Member), Member);
		info.AddValue(nameof(Message), Message);
	}

	#endregion

	#region Public and private methods - virtual

	public virtual bool Equals(LogModel item)
	{
		if (ReferenceEquals(this, item)) return true;
		if (Host != null && item.Host != null && !Host.Equals(item.Host))
			return false;
		if (App != null && item.App != null && !App.Equals(item.App))
			return false;
		if (LogType != null && item.LogType != null && !LogType.Equals(item.LogType))
			return false;
		return base.Equals(item) &&
			   Equals(Version, item.Version) &&
			   Equals(File, item.File) &&
			   Equals(Line, item.Line) &&
			   Equals(Member, item.Member) &&
			   Equals(Message, item.Message);
	}

	public new virtual LogModel CloneCast() => (LogModel)Clone();

	#endregion
}
