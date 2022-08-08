// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "LOGS".
/// </summary>
[Serializable]
public class LogEntity : BaseEntity
{
	#region Public and private fields, properties, constructor

	/// <summary>
	/// Identity name.
	/// </summary>
	[XmlElement] public static ColumnName IdentityName => ColumnName.Uid;
	[XmlElement(IsNullable = true)] public virtual HostEntity? Host { get; set; }
	[XmlElement(IsNullable = true)] public virtual AppEntity? App { get; set; }
	[XmlElement(IsNullable = true)] public virtual LogTypeEntity? LogType { get; set; }
	[XmlElement] public virtual string Version { get; set; } = string.Empty;
	[XmlElement] public virtual string File { get; set; } = string.Empty;
	[XmlElement] public virtual int Line { get; set; }
	[XmlElement] public virtual string Member { get; set; } = string.Empty;
	[XmlElement] public virtual string Message { get; set; } = string.Empty;

	/// <summary>
	/// Constructor.
	/// </summary>
    public LogEntity() : this(Guid.Empty)
    {
        //
    }

	/// <summary>
	/// Constructor.
	/// </summary>
    public LogEntity(Guid uid) : base(uid)
    {
        //
    }

    #endregion

    #region Public and private methods

    public override string ToString()
    {
        string strHost = Host != null ? Host.Name.ToString() : "null";
        string strApp = App != null ? App.Name.ToString() : "null";
        string strLogType = LogType != null ? LogType.Icon.ToString() : "null";
        return
			$"{nameof(IdentityUid)}: {IdentityUid}. " + 
			$"{nameof(IsMarked)}: {IsMarked}. " +
			$"{nameof(Host)}: {strHost}. " +
			$"{nameof(App)}: {strApp}. " +
			$"{nameof(LogType)}: {strLogType}. " +
			$"{nameof(Version)}: {Version}. " +
			$"{nameof(File)}: {File}. " +
			$"{nameof(Line)}: {Line}. " +
			$"{nameof(Member)}: {Member}. " +
			$"{nameof(Message)}: {Message}. ";
    }

    public virtual bool Equals(LogEntity item)
    {
        if (item is null) return false;
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

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((LogEntity)obj);
    }

	public override int GetHashCode() => IdentityUid.GetHashCode();

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

    public new virtual object Clone()
    {
        LogEntity item = new();
        item.Host = Host?.CloneCast();
        item.App = App?.CloneCast();
        item.LogType = LogType?.CloneCast();
        item.Version = Version;
        item.File = File;
        item.Line = Line;
        item.Member = Member;
        item.Message = Message;
        item.Setup(((BaseEntity)this).CloneCast());
        return item;
    }

    public new virtual LogEntity CloneCast() => (LogEntity)Clone();

    #endregion
}
