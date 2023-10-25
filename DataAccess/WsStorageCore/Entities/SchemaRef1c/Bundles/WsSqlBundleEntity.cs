namespace WsStorageCore.Entities.SchemaRef1c.Bundles;

[DebuggerDisplay("{ToString()}")]
public class WsSqlBundleEntity : WsSqlTable1CBase
{
    #region Public and private fields, properties, constructor

    public virtual decimal Weight { get; set; }

    public WsSqlBundleEntity() : base(WsSqlEnumFieldIdentity.Uid)
    {
       Weight = 0;
    }

    public WsSqlBundleEntity(WsSqlBundleEntity item) : base(item)
    {
        Weight = item.Weight;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{Name} | {Weight} | {Uid1C}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlBundleEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public new virtual bool EqualsDefault() =>
        base.EqualsDefault() && Equals(Weight, (decimal)0);

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlBundleEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(Weight, item.Weight);
    #endregion
}

