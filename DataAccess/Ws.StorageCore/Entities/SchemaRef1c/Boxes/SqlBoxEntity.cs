namespace Ws.StorageCore.Entities.SchemaRef1c.Boxes;

[DebuggerDisplay("{ToString()}")]
public class SqlBoxEntity : SqlTable1CBase
{
    #region Public and private fields, properties, constructor

    public virtual decimal Weight { get; set; }

    public SqlBoxEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        Weight = 0;
    }

    public SqlBoxEntity(SqlBoxEntity item) : base(item)
    {
        Weight = item.Weight;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{Uid1C} | {Name} | {Weight}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SqlBoxEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(SqlBoxEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(Weight, item.Weight);

    #endregion
}