// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "ACCESS".
/// </summary>
[Serializable]
public class AccessEntity : BaseEntity, ISerializable
{
	#region Public and private fields, properties, constructor

	/// <summary>
	/// Identity name.
	/// </summary>
	[XmlElement] public static ColumnName IdentityName => ColumnName.Uid;
	/// <summary>
	/// User name.
	/// </summary>
	[XmlElement] public virtual string User { get; set; } = string.Empty;
	/// <summary>
	/// User rights.
	/// </summary>
	[XmlElement] public virtual byte Rights { get; set; } = 0x00;

	/// <summary>
	/// Constructor.
	/// </summary>
	public AccessEntity() : this(Guid.Empty)
    {
        //
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="uid"></param>
    public AccessEntity(Guid uid) : base(uid)
    {
        // 
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
	/// <param name="context"></param>
	private AccessEntity(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        User = info.GetString(nameof(User));
        Rights = info.GetByte(nameof(Rights));
    }

    #endregion

    #region Public and private methods

    /// <summary>
    /// To string.
    /// </summary>
    /// <returns></returns>
    public override string ToString() =>
	    $"{nameof(IdentityUid)}: {IdentityUid}. " +
	    $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(User)}: {User}. " +
        $"{nameof(Rights)}: {Rights}. ";

    /// <summary>
    /// To short string.
    /// </summary>
    /// <returns></returns>
    public virtual string ToStringShort() =>
        $"{nameof(User)}: {User}. " +
        $"{nameof(Rights)}: {Rights}. ";

    public virtual bool Equals(AccessEntity item)
    {
        //if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        return base.Equals(item) &&
               Equals(User, item.User) &&
               Equals(Rights, item.Rights);
    }

    public override bool Equals(object obj)
    {
        //if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((AccessEntity)obj);
    }

    public override int GetHashCode() => IdentityUid.GetHashCode();

    public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        return base.EqualsDefault() &&
               Equals(User, string.Empty) &&
               Equals(Rights, (byte)0x00);
    }

    public new virtual object Clone()
    {
        AccessEntity item = new();
        item.User = User;
        item.Rights = Rights;
        item.Setup(((BaseEntity)this).CloneCast());
        return item;
    }

    public new virtual AccessEntity CloneCast() => (AccessEntity)Clone();

    public new virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(User), User);
        info.AddValue(nameof(Rights), Rights);
    }

    #endregion
}
