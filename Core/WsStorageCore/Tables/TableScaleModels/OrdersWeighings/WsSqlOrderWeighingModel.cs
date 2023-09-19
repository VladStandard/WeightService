namespace WsStorageCore.Tables.TableScaleModels.OrdersWeighings;

[DebuggerDisplay("{ToString()}")]
public class WsSqlOrderWeighingModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    public virtual WsSqlOrderModel Order { get; set; }
    public virtual WsSqlPluWeighingModel PluWeighing { get; set; }
    
    public WsSqlOrderWeighingModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        Order = new();
        PluWeighing = new();
    }

    public WsSqlOrderWeighingModel(WsSqlOrderWeighingModel item) : base(item)
    {
        Order = new(item.Order);
        PluWeighing = new(item.PluWeighing);
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsMarked()} | " +
        $"{nameof(Order)}: {Order}. " +
        $"{nameof(PluWeighing)}: {PluWeighing}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlOrderWeighingModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Order.EqualsDefault() &&
        PluWeighing.EqualsDefault();

    public override void FillProperties()
    {
        base.FillProperties();
        Order.FillProperties();
        PluWeighing.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlOrderWeighingModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Order.Equals(item.Order) &&
        PluWeighing.Equals(item.PluWeighing);

    #endregion
}