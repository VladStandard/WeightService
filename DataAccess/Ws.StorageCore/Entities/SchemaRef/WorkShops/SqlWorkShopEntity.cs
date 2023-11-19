namespace Ws.StorageCore.Entities.SchemaRef.WorkShops;

/// <summary>
/// Table "WorkShop".
/// </summary>
[DebuggerDisplay("{ToString()}")]
public class SqlWorkShopEntity : SqlEntityBase
{
    #region Public and private fields, properties, constructor

    public virtual SqlProductionSiteEntity ProductionSite { get; set; }

    public SqlWorkShopEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        ProductionSite = new();
    }

    public SqlWorkShopEntity(SqlWorkShopEntity item) : base(item)
    {
        ProductionSite = new(item.ProductionSite);
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(ProductionSite)}: {ProductionSite}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SqlWorkShopEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        ProductionSite.EqualsDefault();
    
    public override void FillProperties()
    {
        base.FillProperties();
        ProductionSite.FillProperties();
    }

    #endregion

    #region Public and private methods

    public virtual bool Equals(SqlWorkShopEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        ProductionSite.Equals(item.ProductionSite);

    #endregion
}
