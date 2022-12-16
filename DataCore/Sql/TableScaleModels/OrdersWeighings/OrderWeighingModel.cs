// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using DataCore.Sql.TableScaleModels.Orders;
using DataCore.Sql.TableScaleModels.PlusWeighings;

namespace DataCore.Sql.TableScaleModels.OrdersWeighings;

/// <summary>
/// Table "ORDERS_WEIGHINGS".
/// </summary>
[Serializable]
[DebuggerDisplay("Type = {nameof(OrderWeighingModel)}")]
public class OrderWeighingModel : SqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual OrderModel Order { get; set; }
    [XmlElement] public virtual PluWeighingModel PluWeighing { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public OrderWeighingModel() : base(SqlFieldIdentityEnum.Uid)
    {
        Order = new();
        PluWeighing = new();
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected OrderWeighingModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Order = (OrderModel)info.GetValue(nameof(Order), typeof(OrderModel));
        PluWeighing = (PluWeighingModel)info.GetValue(nameof(PluWeighing), typeof(PluWeighingModel));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Order)}: {Order}. " +
        $"{nameof(PluWeighing)}: {PluWeighing}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((OrderWeighingModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Order.EqualsDefault() &&
        PluWeighing.EqualsDefault();

    public override object Clone()
    {
        OrderWeighingModel item = new();
        item.Order = Order.CloneCast();
        item.PluWeighing = PluWeighing.CloneCast();
        item.CloneSetup(base.CloneCast());
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

    public virtual bool Equals(OrderWeighingModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Order.Equals(item.Order) &&
        PluWeighing.Equals(item.PluWeighing);

    public new virtual OrderWeighingModel CloneCast() => (OrderWeighingModel)Clone();

    #endregion
}
