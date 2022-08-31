// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "ORDERS_WEIGHINGS".
/// </summary>
[Serializable]
public class OrderWeighingModel : TableModel, ISerializable, ITableModel
{
	#region Public and private fields, properties, constructor

	[XmlElement] public virtual OrderModel Order { get; set; }
	[XmlElement] public virtual PluWeighingModel PluWeighing { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
    public OrderWeighingModel()
	{
		Order = new();
		PluWeighing = new();
	}

	#endregion

	#region Public and private methods

	public override string ToString()
    {
        return
			$"{nameof(IsMarked)}: {IsMarked}. " +
			$"{nameof(Order)}: {Order}. " + 
			$"{nameof(PluWeighing)}: {PluWeighing}. ";
    }

    public virtual bool Equals(OrderWeighingModel item)
    {
		if (!Order.Equals(item.Order))
			return false;
		if (!PluWeighing.Equals(item.PluWeighing))
			return false;
        if (ReferenceEquals(this, item)) return true;
        return base.Equals(item);
    }

    public override bool Equals(object obj)
    {
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((OrderWeighingModel)obj);
    }

	public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
		if (!Order.EqualsDefault())
			return false;
		if (!PluWeighing.EqualsDefault())
			return false;
        return base.EqualsDefault();
    }

    public new virtual object Clone()
    {
        OrderWeighingModel item = new();
        item.Order = Order.CloneCast();
        item.PluWeighing = PluWeighing.CloneCast();
        item.Setup(((TableModel)this).CloneCast());
        return item;
    }

    public new virtual OrderWeighingModel CloneCast() => (OrderWeighingModel)Clone();

    #endregion
}
