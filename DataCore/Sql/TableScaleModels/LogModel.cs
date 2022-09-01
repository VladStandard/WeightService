// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "LOGS".
/// </summary>
[Serializable]
public class LogModel : TableModel, ISerializable, ITableModel
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
    public LogModel()
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

	#endregion

	#region Public and private methods

	public new virtual string ToString() =>
		$"{nameof(IsMarked)}: {IsMarked}. " +
		$"{nameof(Host)}: {Host?.Name ?? "null"}. " +
		$"{nameof(App)}: {App?.Name ?? "null"}. " +
		$"{nameof(LogType)}: {LogType?.Icon ?? "null"}. " +
		$"{nameof(Version)}: {Version}. " +
		$"{nameof(File)}: {File}. " +
		$"{nameof(Line)}: {Line}. " +
		$"{nameof(Member)}: {Member}. " +
		$"{nameof(Message)}: {Message}. ";

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

	public new virtual bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((LogModel)obj);
    }

	public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
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

    public new virtual int GetHashCode() => base.GetHashCode();

	public new virtual object Clone()
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

    public new virtual LogModel CloneCast() => (LogModel)Clone();

    #endregion
}
