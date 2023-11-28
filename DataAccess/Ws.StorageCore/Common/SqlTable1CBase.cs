namespace Ws.StorageCore.Common;

[DebuggerDisplay("{ToString()}")]
public class SqlTable1CBase : SqlEntityBase
{
    #region Public and private fields, properties, constructor

    public virtual Guid Uid1C { get; set; }

    public SqlTable1CBase() : base()
    {
        Uid1C = Guid.Empty;
    }

    public SqlTable1CBase(SqlEnumFieldIdentity identityName) : base(identityName)
    {
        Uid1C = Guid.Empty;
    }

    public SqlTable1CBase(SqlTable1CBase item) : base(item)
    {
        Uid1C = item.Uid1C;
    }

    #endregion

    #region Public and private methods - override

    public virtual bool Equals(SqlTable1CBase item) =>
        ReferenceEquals(this, item) || base.Equals(item) && Equals(Uid1C, item.Uid1C);

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SqlTable1CBase)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    #endregion
}