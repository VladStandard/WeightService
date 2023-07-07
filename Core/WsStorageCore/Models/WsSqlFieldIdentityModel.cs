// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Models;

/// <summary>
/// DB field Identity model.
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlFieldIdentityModel : WsSqlFieldBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual WsSqlEnumFieldIdentity Name { get; private set; }
    [XmlElement] public virtual Guid Uid { get; private set; }
    [XmlElement] public virtual long Id { get; private set; }
    [XmlIgnore] public virtual bool IsUid => Equals(Name, WsSqlEnumFieldIdentity.Uid);
    [XmlIgnore] public virtual bool IsId => Equals(Name, WsSqlEnumFieldIdentity.Id);

    public WsSqlFieldIdentityModel() : base()
    {
        FieldName = nameof(WsSqlFieldIdentityModel);
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

    protected WsSqlFieldIdentityModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Name = (WsSqlEnumFieldIdentity)info.GetValue(nameof(Name), typeof(WsSqlEnumFieldIdentity));
        Uid = Guid.Parse(info.GetString(nameof(Uid).ToUpper()));
        Id = info.GetInt64(nameof(Id));
    }

    public WsSqlFieldIdentityModel(WsSqlFieldIdentityModel item) : base(item)
    {
        FieldName = nameof(WsSqlFieldIdentityModel);
        Name = item.Name;
        Uid = item.Uid;
        Id = item.Id;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        Name.Equals(WsSqlEnumFieldIdentity.Id) ? $"{Id}" : Name.Equals(WsSqlEnumFieldIdentity.Uid) ? $"{Uid}" : string.Empty;

    public virtual object? GetValueAsObjectNullable() => Name switch
    {
        WsSqlEnumFieldIdentity.Id => Id,
        WsSqlEnumFieldIdentity.Uid => Uid,
        _ => null
    };

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

    public override bool EqualsNew() => Equals(new());

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Name), Name);
        info.AddValue(nameof(Id), Id);
        info.AddValue(nameof(Uid), Uid);
    }

    public override bool EqualsDefault() =>
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

    public virtual bool IsNotExists => Name switch
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