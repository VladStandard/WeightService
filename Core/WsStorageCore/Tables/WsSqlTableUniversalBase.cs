// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables;

/// <summary>
/// DB table universal model.
/// </summary>
[DebuggerDisplay("{nameof(WsSqlTableUniversalBase)} | {ToString()}")]
public class WsSqlTableUniversalBase : SerializeBase
{
    #region Public and private fields, properties, constructor

    [XmlIgnore] public virtual WsSqlFieldIdentityModel Identity { get; set; }
    [XmlElement] public virtual long IdentityValueId { get => Identity.Id; set => Identity.SetId(value); }
    [XmlElement] public virtual Guid IdentityValueUid { get => Identity.Uid; set => Identity.SetUid(value); }
    [XmlIgnore] public virtual bool IsExists => Identity.IsExists;
    [XmlIgnore] public virtual bool IsNotExists => Identity.IsNotExists;
    [XmlIgnore] public virtual bool IsNew => IsNotExists;
    [XmlIgnore] public virtual bool IsNotNew => IsExists;
    [XmlIgnore] public virtual bool IsIdentityId => Equals(Identity.Name, WsSqlFieldIdentity.Id);
    [XmlIgnore] public virtual bool IsIdentityUid => Equals(Identity.Name, WsSqlFieldIdentity.Uid);

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlTableUniversalBase()
    {
        Identity = new(WsSqlFieldIdentity.Empty);
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlTableUniversalBase(WsSqlFieldIdentity identityName) : this()
    {
        Identity = new(identityName);
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlTableUniversalBase(WsSqlFieldIdentityModel identity) : this()
    {
        Identity = (WsSqlFieldIdentityModel)identity.Clone();
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    protected WsSqlTableUniversalBase(SerializationInfo info, StreamingContext context)
    {
        Identity = (WsSqlFieldIdentityModel)info.GetValue(nameof(Identity), typeof(WsSqlFieldIdentityModel));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => 
        IsIdentityUid ? IdentityValueUid.ToString() : IdentityValueId.ToString();

    public virtual bool Equals(WsSqlTableUniversalBase item) =>
        ReferenceEquals(this, item) || Identity.Equals(item.Identity);

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlTableUniversalBase)obj);
    }

    public override int GetHashCode() => Identity.GetHashCode();

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Identity), Identity);
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool EqualsNew() => Equals(new WsSqlTableUniversalBase());

    public virtual bool EqualsDefault() =>
        Identity.EqualsDefault() &&
        IsIdentityUid ? Equals(IdentityValueUid, Guid.Empty) : Equals(IdentityValueId, default(long));

    #endregion
}