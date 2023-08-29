using WsStorageCore.Tables.TableRefModels.ProductionSites;

namespace WsStorageCore.Tables.TableRefModels.WorkShops;

/// <summary>
/// Table "WorkShop".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlWorkShopModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual WsSqlProductionSiteModel ProductionSite { get; set; }

    public WsSqlWorkShopModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        ProductionSite = new();
    }
    
    protected WsSqlWorkShopModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        ProductionSite = (WsSqlProductionSiteModel)info.GetValue(nameof(ProductionSite), typeof(WsSqlProductionSiteModel));
    }

    public WsSqlWorkShopModel(WsSqlWorkShopModel item) : base(item)
    {
        ProductionSite = new(item.ProductionSite);
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsMarked()} | " +
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(ProductionSite)}: {ProductionSite}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlWorkShopModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        ProductionSite.EqualsDefault();

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(ProductionSite), ProductionSite);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        ProductionSite.FillProperties();
    }

    #endregion

    #region Public and private methods

    public virtual bool Equals(WsSqlWorkShopModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        ProductionSite.Equals(item.ProductionSite);

    #endregion
}
