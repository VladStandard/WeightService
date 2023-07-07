// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Common;

/// <summary>
/// Базовый класс SQL-таблицы.
/// </summary>
[DebuggerDisplay("{ToString()}")]
public class WsSqlTableUniversalBase : SerializeBase
{
    #region Public and private fields, properties, constructor

    [XmlIgnore] public virtual WsSqlFieldIdentityModel Identity { get; init; }
    [XmlElement] public virtual long IdentityValueId { get => Identity.Id; set => Identity.SetId(value); }
    [XmlElement] public virtual Guid IdentityValueUid { get => Identity.Uid; set => Identity.SetUid(value); }
    
    [XmlIgnore] protected virtual bool IsExists => Identity.IsExists;
    [XmlIgnore] protected virtual bool IsNotExists => Identity.IsNotExists;
    [XmlIgnore] protected virtual bool IsIdentityUid => Equals(Identity.Name, WsSqlEnumFieldIdentity.Uid);

    public WsSqlTableUniversalBase()
    {
        Identity = new(WsSqlEnumFieldIdentity.Empty);
    }

    public WsSqlTableUniversalBase(WsSqlEnumFieldIdentity identityName) : this()
    {
        Identity = new(identityName);
    }

    public WsSqlTableUniversalBase(WsSqlFieldIdentityModel identity) : this()
    {
        Identity = new(identity);
    }

    protected WsSqlTableUniversalBase(SerializationInfo info, StreamingContext context)
    {
        Identity = (WsSqlFieldIdentityModel)info.GetValue(nameof(Identity), typeof(WsSqlFieldIdentityModel));
    }

    public WsSqlTableUniversalBase(WsSqlTableUniversalBase item) : base(item)
    {
        Identity = new(item.Identity);
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        IsIdentityUid ? IdentityValueUid.ToString() : IdentityValueId.ToString();

    protected virtual bool Equals(WsSqlTableUniversalBase item) =>
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

    public virtual bool EqualsNew() => Equals(new());

    public virtual bool EqualsDefault() =>
        Identity.EqualsDefault() &&
        IsIdentityUid ? Equals(IdentityValueUid, Guid.Empty) : Equals(IdentityValueId, default(long));

    #endregion
}