namespace WsStorageCore.Common;

[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlTable1CBase : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    public virtual Guid Uid1C { get; set; }

    public WsSqlTable1CBase() : base()
    {
        Uid1C = Guid.Empty;
    }

    public WsSqlTable1CBase(WsSqlEnumFieldIdentity identityName) : base(identityName)
    {
        Uid1C = Guid.Empty;
    }
    
    public WsSqlTable1CBase(WsSqlTable1CBase item) : base(item)
    {
        Uid1C = item.Uid1C;
    }

    #endregion

    #region Public and private methods - override

    public virtual bool Equals(WsSqlTable1CBase item) =>
        ReferenceEquals(this, item) || base.Equals(item) && Equals(Uid1C, item.Uid1C);

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlTable1CBase)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    #endregion

    #region Public and private methods - virtual

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() && Equals(Uid1C, Guid.Empty);

    #endregion
}