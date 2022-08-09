// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com


// ReSharper disable MissingXmlDoc

namespace DataCore.Sql.Models;

/// <summary>
/// Enum column name.
/// </summary>
public enum ColumnName
{
    Default,
    Id,
    Uid,
}

[Serializable]
public class BaseEntity : BaseSerializeEntity, ICloneable, ISerializable
{
	#region Public and private fields, properties, constructor

	[XmlElement] public virtual long IdentityId { get; set; } = 0;
	[XmlElement] public virtual Guid IdentityUid { get; set; } = Guid.Empty;
	[XmlElement] public virtual DateTime CreateDt { get; set; } = DateTime.MinValue;
	[XmlElement] public virtual DateTime ChangeDt { get; set; } = DateTime.MinValue;
	[XmlElement] public virtual bool IsMarked { get; set; }
	[XmlIgnore] public virtual string IdentityUidStr
    {
        get => IdentityUid.ToString(); 
        set => IdentityUid = Guid.TryParse(value, out Guid uid) ? uid : Guid.Empty;
    }

	/// <summary>
	/// Constructor.
	/// </summary>
	private BaseEntity()
    {
        //
    }

	/// <summary>
	/// Constructor.
	/// </summary>
    public BaseEntity(long identityId) : this()
    {
        IdentityId = identityId;
    }

	/// <summary>
	/// Constructor.
	/// </summary>
    public BaseEntity(Guid identityUid) : this()
    {
        IdentityUid = identityUid;
    }

	/// <summary>
	/// Constructor.
	/// </summary>
    protected BaseEntity(SerializationInfo info, StreamingContext context)
    {
        IdentityId = info.GetInt64(nameof(IdentityId));
        IdentityUid = Guid.Parse(info.GetString(nameof(IdentityUid)));
        IdentityUidStr = info.GetString(nameof(IdentityUidStr));
        CreateDt = info.GetDateTime(nameof(CreateDt));
        ChangeDt = info.GetDateTime(nameof(ChangeDt));
        IsMarked = info.GetBoolean(nameof(IsMarked));
    }

    #endregion

    #region Public and private methods

    public override string ToString()
    {
        string strCreateDt = CreateDt != DateTime.MinValue ? $"{nameof(CreateDt)}: {CreateDt:yyyy-MM-dd}. " : string.Empty;
        string strChangeDt = ChangeDt != DateTime.MinValue ? $"{nameof(ChangeDt)}: {ChangeDt:yyyy-MM-dd}. " : string.Empty;
        string strIsMarked = IsMarked ? $"{nameof(IsMarked)}: {IsMarked}. " : string.Empty;
        return strCreateDt + strChangeDt + strIsMarked;
    }

	//public override int GetHashCode() => IdentityName switch
	//{
	//    ColumnName.Id => IdentityId.GetHashCode(),
	//    ColumnName.Uid => IdentityUid.GetHashCode(),
	//    _ => default,
	//};

	//public virtual bool EqualsEmpty()
	//{
	//    bool isIdentityEmpty = IdentityName switch
	//    {
	//        ColumnName.Id => Equals(IdentityId, 0),
	//        ColumnName.Uid => Equals(IdentityUid, Guid.Empty),
	//        _ => Equals(IdentityName, ColumnName.Default),
	//    };
	//    return isIdentityEmpty;
	//}

	public virtual bool Equals(BaseEntity item)
    {
        if (ReferenceEquals(this, item)) return true;
        return
            IdentityId.Equals(item.IdentityId) &&
            IdentityUid.Equals(item.IdentityUid) &&
            Equals(CreateDt, item.CreateDt) &&
            Equals(ChangeDt, item.ChangeDt) &&
            Equals(IsMarked, item.IsMarked);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((BaseEntity)obj);
    }

    public new virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(IdentityId), IdentityId);
        info.AddValue(nameof(IdentityUid), IdentityUid);
        info.AddValue(nameof(IdentityUidStr), IdentityUidStr);
        info.AddValue(nameof(ChangeDt), ChangeDt);
        info.AddValue(nameof(CreateDt), CreateDt);
        info.AddValue(nameof(IsMarked), IsMarked);
    }

    public virtual bool EqualsDefault()
    {
        return
            Equals(IdentityId, (long)0) &&
            Equals(IdentityUid, Guid.Empty) &&
            Equals(CreateDt, DateTime.MinValue) &&
            Equals(ChangeDt, DateTime.MinValue) &&
            Equals(IsMarked, false);
    }

    public virtual object Clone() => new BaseEntity()
    {
        IdentityId = IdentityId,
        IdentityUid = IdentityUid,
        CreateDt = CreateDt,
        ChangeDt = ChangeDt,
        IsMarked = IsMarked,
    };

    public virtual BaseEntity CloneCast() => (BaseEntity)Clone();

    public virtual void Setup(BaseEntity baseItem)
    {
        IdentityId = baseItem.IdentityId;
        IdentityUid = baseItem.IdentityUid;
        CreateDt = baseItem.CreateDt;
        ChangeDt = baseItem.ChangeDt;
        IsMarked = baseItem.IsMarked;
    }

    #endregion
}
