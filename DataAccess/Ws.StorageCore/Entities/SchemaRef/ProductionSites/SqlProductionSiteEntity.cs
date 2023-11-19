namespace Ws.StorageCore.Entities.SchemaRef.ProductionSites;

/// <summary>
/// Table "ProductionSite".
/// </summary>
[DebuggerDisplay("{ToString()}")]
public class SqlProductionSiteEntity : SqlEntityBase
{
    #region Public and private fields, properties, constructor

    public virtual string Address { get; set; }
    
    public SqlProductionSiteEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        Address = string.Empty;
    }

    public SqlProductionSiteEntity(SqlProductionSiteEntity item) : base(item)
    {
        Address = item.Address;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{Address}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SqlProductionSiteEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(Address, string.Empty);

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(SqlProductionSiteEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(Address, item.Address);

    #endregion
}