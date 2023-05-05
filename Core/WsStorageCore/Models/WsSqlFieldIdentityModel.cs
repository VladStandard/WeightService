// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Models;

/// <summary>
/// DB field Identity model.
/// </summary>
[Serializable]
[DebuggerDisplay("{GetValue()}")]
public class WsSqlFieldIdentityModel : WsSqlFieldBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual WsSqlFieldIdentity Name { get; private set; }
    [XmlElement] public virtual long Id { get; private set; }
    [XmlElement] public virtual Guid Uid { get; private set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlFieldIdentityModel()
    {
        FieldName = nameof(WsSqlFieldIdentityModel);
        Name = WsSqlFieldIdentity.Empty;
        Id = 0;
        Uid = Guid.Empty;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="identityName"></param>
    public WsSqlFieldIdentityModel(WsSqlFieldIdentity identityName) : this()
    {
        Name = identityName;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    private WsSqlFieldIdentityModel(WsSqlFieldIdentity identityName, long identityId, Guid identityUid) : this(identityName)
    {
        Id = identityId;
        Uid = identityUid;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlFieldIdentityModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Name = (WsSqlFieldIdentity)info.GetValue(nameof(Name), typeof(WsSqlFieldIdentity));
        Id = info.GetInt64(nameof(Id));
        Uid = Guid.Parse(info.GetString(nameof(Uid).ToUpper()));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        Name switch
        {
            WsSqlFieldIdentity.Id => $"{nameof(Id)}: {Id}. ",
            WsSqlFieldIdentity.Uid => $"{nameof(Uid)}: {Uid}. ",
            _ => string.Empty
        };

    public string GetValue() =>
        Name switch
        {
            WsSqlFieldIdentity.Id => $"{Id}",
            WsSqlFieldIdentity.Uid => $"{Uid}",
            _ => string.Empty
        };

    public virtual string GetValueAsString() => Name switch
    {
        WsSqlFieldIdentity.Id => Id.ToString(),
        WsSqlFieldIdentity.Uid => Uid.ToString(),
        _ => string.Empty
    };

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