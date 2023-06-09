// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Common;

namespace WsStorageCore.Models;

/// <summary>
/// DB field Identity model.
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlFieldIdentityModel : WsSqlFieldBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual WsSqlFieldIdentity Name { get; private set; }
    [XmlElement] public virtual Guid Uid { get; private set; }
    [XmlElement] public virtual long Id { get; private set; }
    [XmlIgnore] public virtual bool IsUid => Equals(Name, WsSqlFieldIdentity.Uid);
    [XmlIgnore] public virtual bool IsId => Equals(Name, WsSqlFieldIdentity.Id);

    public WsSqlFieldIdentityModel()
    {
        FieldName = nameof(WsSqlFieldIdentityModel);
        Name = WsSqlFieldIdentity.Empty;
        Id = 0;
        Uid = Guid.Empty;
    }

    public WsSqlFieldIdentityModel(WsSqlFieldIdentity identityName) : this()
    {
        Name = identityName;
    }

    private WsSqlFieldIdentityModel(WsSqlFieldIdentity identityName, long identityId, Guid identityUid) : this(identityName)
    {
        Uid = identityUid;
        Id = identityId;
    }

    protected WsSqlFieldIdentityModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Name = (WsSqlFieldIdentity)info.GetValue(nameof(Name), typeof(WsSqlFieldIdentity));
        Uid = Guid.Parse(info.GetString(nameof(Uid).ToUpper()));
        Id = info.GetInt64(nameof(Id));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        Name.Equals(WsSqlFieldIdentity.Id) ? $"{Id}" : Name.Equals(WsSqlFieldIdentity.Uid) ? $"{Uid}" : string.Empty;

    public virtual object? GetValueAsObjectNullable() => Name switch
    {
        WsSqlFieldIdentity.Id => Id,
        WsSqlFieldIdentity.Uid => Uid,
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
        WsSqlFieldIdentity.Id => Id.GetHashCode(),
        WsSqlFieldIdentity.Uid => Uid.GetHashCode(),
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

    public override object Clone() => new WsSqlFieldIdentityModel(Name, Id, Uid);

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlFieldIdentityModel item) =>
        ReferenceEquals(this, item) || Equals(Name, item.Name) && //-V3130
        Id.Equals(item.Id) &&
        Uid.Equals(item.Uid);

    public new virtual WsSqlFieldIdentityModel CloneCast() => (WsSqlFieldIdentityModel)Clone();

    public virtual void SetId(long value) => Id = value;

    public virtual void SetUid(Guid value) => Uid = value;

    public virtual bool IsNotExists => Name switch
    {
        WsSqlFieldIdentity.Id => Equals(Id, default(long)),
        WsSqlFieldIdentity.Uid => Equals(Uid, Guid.Empty),
        _ => default
    };

    public virtual bool IsExists => Name switch
    {
        WsSqlFieldIdentity.Id => !Equals(Id, default(long)),
        WsSqlFieldIdentity.Uid => !Equals(Uid, Guid.Empty),
        _ => default
    };

    [Obsolete(@"Use IsNotExists")]
    public virtual bool IsNew => IsNotExists;

    [Obsolete(@"Use IsExists")]
    public virtual bool IsNotNew => IsExists;

    #endregion
}