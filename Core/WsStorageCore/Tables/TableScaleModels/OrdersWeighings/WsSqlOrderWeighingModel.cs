namespace WsStorageCore.Tables.TableScaleModels.OrdersWeighings;

[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlOrderWeighingModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual WsSqlOrderModel Order { get; set; }
    [XmlElement] public virtual WsSqlPluWeighingModel PluWeighing { get; set; }
    
    public WsSqlOrderWeighingModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        Order = new();
        PluWeighing = new();
    }
    
    protected WsSqlOrderWeighingModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Order = (WsSqlOrderModel)info.GetValue(nameof(Order), typeof(WsSqlOrderModel));
        PluWeighing = (WsSqlPluWeighingModel)info.GetValue(nameof(PluWeighing), typeof(WsSqlPluWeighingModel));
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

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Order), Order);
        info.AddValue(nameof(PluWeighing), PluWeighing);
    }

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