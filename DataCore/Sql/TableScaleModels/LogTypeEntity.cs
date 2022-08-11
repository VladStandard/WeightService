// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "LOG_TYPES".
/// </summary>
[Serializable]
public class LogTypeEntity : BaseEntity, ISerializable, IBaseEntity
{
	#region Public and private fields, properties, constructor

	/// <summary>
	/// Identity name.
	/// </summary>
	[XmlElement] public static ColumnName IdentityName => ColumnName.Uid;
	[XmlElement] public virtual byte Number { get; set; }
	[XmlElement] public virtual string Icon { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
    public LogTypeEntity() : base(Guid.Empty, false)
	{
		Init();
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="identityUid"></param>
	/// <param name="isSetupDates"></param>
	public LogTypeEntity(Guid identityUid, bool isSetupDates) : base(identityUid, isSetupDates)
    {
		Init();
	}

    #endregion

    #region Public and private methods

    public new virtual void Init()
    {
	    base.Init();
        Number = 0x00;
        Icon = string.Empty;
    }

    public override string ToString() =>
	    $"{nameof(IdentityUid)}: {IdentityUid}. " +
	    $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Number)}: {Number}. " +
        $"{nameof(Icon)}: {Icon}. ";

    public virtual bool Equals(LogTypeEntity item)
    {
        if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        return base.Equals(item) &&
               Equals(Number, item.Number) &&
               Equals(Icon, item.Icon);
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((LogTypeEntity)obj);
    }

	public override int GetHashCode() => IdentityUid.GetHashCode();

	public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        return base.EqualsDefault() &&
               Equals(Number, (byte)0x00) &&
               Equals(Icon, string.Empty);
    }

    public new virtual object Clone()
    {
        LogTypeEntity item = new();
        item.Number = Number;
        item.Icon = Icon;
        item.Setup(((BaseEntity)this).CloneCast());
        return item;
    }

    public new virtual LogTypeEntity CloneCast() => (LogTypeEntity)Clone();

    #endregion
}
