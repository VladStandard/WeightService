// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.OrdersWeighings;

/// <summary>
/// Table "ORDERS_WEIGHINGS".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlOrderWeighingModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual WsSqlOrderModel Order { get; set; }
    [XmlElement] public virtual WsSqlPluWeighingModel PluWeighing { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlOrderWeighingModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        Order = new();
        PluWeighing = new();
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlOrderWeighingModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Order = (WsSqlOrderModel)info.GetValue(nameof(Order), typeof(WsSqlOrderModel));
        PluWeighing = (WsSqlPluWeighingModel)info.GetValue(nameof(PluWeighing), typeof(WsSqlPluWeighingModel));
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

    public object Clone()
    {
        WsSqlOrderWeighingModel item = new();
        item.Order = Order.CloneCast();
        item.PluWeighing = PluWeighing.CloneCast();
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

    public new virtual WsSqlOrderWeighingModel CloneCast() => (WsSqlOrderWeighingModel)Clone();

    #endregion
}
