// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "APPS".
/// </summary>
[Serializable]
public class AppEntity : BaseEntity, ISerializable, IBaseEntity
{
	#region Public and private fields, properties, constructor

	/// <summary>
	/// Identity name.
	/// </summary>
	[XmlElement] public static ColumnName IdentityName => ColumnName.Uid;
	[XmlElement] public virtual string Name { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public AppEntity() : base(Guid.Empty, false)
	{
		Init();
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="identityUid"></param>
	/// <param name="isSetupDates"></param>
	public AppEntity(Guid identityUid, bool isSetupDates) : base(identityUid, isSetupDates)
	{
		Init();
	}

	/// <summary>
	/// Constructor for serialization.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
    protected AppEntity(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Name = info.GetString(nameof(Name));
    }

	#endregion

	#region Public and private methods

	public new virtual void Init()
	{
		base.Init();
		Name = string.Empty;
	}

	public override string ToString() =>
	    $"{nameof(IdentityUid)}: {IdentityUid}. " +
	    $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Name)}: {Name}. ";

    public virtual bool Equals(AppEntity item)
    {
        if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        return base.Equals(item) &&
               Equals(Name, item.Name);
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((AppEntity)obj);
    }

    public override int GetHashCode() => IdentityUid.GetHashCode();

    public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        return base.EqualsDefault() &&
            Equals(Name, string.Empty);
    }

    public new virtual object Clone()
    {
        AppEntity item = new();
        item.Name = Name;
        item.Setup(((BaseEntity)this).CloneCast());
        return item;
    }

    public new virtual AppEntity CloneCast() => (AppEntity)Clone();

    public new virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Name), Name);
    }

    #endregion
}
