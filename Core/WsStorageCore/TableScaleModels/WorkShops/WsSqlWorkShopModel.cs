// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.WorkShops;

/// <summary>
/// Table "WorkShop".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlWorkShopModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual WsSqlProductionFacilityModel ProductionFacility { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlWorkShopModel() : base(WsSqlFieldIdentity.Id)
    {
        ProductionFacility = new();
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlWorkShopModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        ProductionFacility = (WsSqlProductionFacilityModel)info.GetValue(nameof(ProductionFacility), typeof(WsSqlProductionFacilityModel));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsMarked()} | " +
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(ProductionFacility)}: {ProductionFacility}. ";

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
        ProductionFacility.EqualsDefault();

    public override object Clone()
    {
        WsSqlWorkShopModel item = new();
        item.CloneSetup(base.CloneCast());
        item.ProductionFacility = ProductionFacility.CloneCast();
        return item;
    }

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(ProductionFacility), ProductionFacility);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        ProductionFacility.FillProperties();
    }

    #endregion

    #region Public and private methods

    public virtual bool Equals(WsSqlWorkShopModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        ProductionFacility.Equals(item.ProductionFacility);

    public new virtual WsSqlWorkShopModel CloneCast() => (WsSqlWorkShopModel)Clone();

    #endregion
}
