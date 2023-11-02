namespace WsStorageCore.Models;

/// <summary>
/// DB field Identity model.
/// </summary>
[DebuggerDisplay("{ToString()}")]
public class WsSqlFieldIdentityModel
{
    #region Public and private fields, properties, constructor

     public virtual WsSqlEnumFieldIdentity Name { get; }
     public virtual Guid Uid { get; private set; }
     public virtual long Id { get; private set; }
     public virtual bool IsUid => Equals(Name, WsSqlEnumFieldIdentity.Uid);
     public virtual bool IsId => Equals(Name, WsSqlEnumFieldIdentity.Id);

    public WsSqlFieldIdentityModel()
    {
        Name = WsSqlEnumFieldIdentity.Empty;
        Uid = Guid.Empty;
        Id = 0;
    }

    public WsSqlFieldIdentityModel(WsSqlEnumFieldIdentity identityName) : this()
    {
        Name = identityName;
    }

    private WsSqlFieldIdentityModel(WsSqlEnumFieldIdentity identityName, long identityId, Guid identityUid) : this(identityName)
    {
        Uid = identityUid;
        Id = identityId;
    }

    public WsSqlFieldIdentityModel(WsSqlFieldIdentityModel item)
    {
        Name = item.Name;
        Uid = item.Uid;
        Id = item.Id;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        Name.Equals(WsSqlEnumFieldIdentity.Id) ? $"{Id}" : Name.Equals(WsSqlEnumFieldIdentity.Uid) ? $"{Uid}" : string.Empty;
    
    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlFieldIdentityModel)obj);
    }

    public override int GetHashCode() => Name switch
    {
        WsSqlEnumFieldIdentity.Id => Id.GetHashCode(),
        WsSqlEnumFieldIdentity.Uid => Uid.GetHashCode(),
        _ => default
    };

    public bool EqualsNew() => Equals(new());

    public bool EqualsDefault() =>
        Equals(Id, (long)0) &&
        Equals(Uid, Guid.Empty);

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlFieldIdentityModel item) =>
        ReferenceEquals(this, item) || Equals(Name, item.Name) && //-V3130
        Id.Equals(item.Id) &&
        Uid.Equals(item.Uid);

    public virtual void SetId(long value) => Id = value;

    public virtual void SetUid(Guid value) => Uid = value;

    public virtual bool IsNew => Name switch
    {
        WsSqlEnumFieldIdentity.Id => Equals(Id, default(long)),
        WsSqlEnumFieldIdentity.Uid => Equals(Uid, Guid.Empty),
        _ => default
    };

    public virtual bool IsExists => Name switch
    {
        WsSqlEnumFieldIdentity.Id => !Equals(Id, default(long)),
        WsSqlEnumFieldIdentity.Uid => !Equals(Uid, Guid.Empty),
        _ => default
    };

    #endregion
}