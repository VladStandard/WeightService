// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "ORDERS_WEIGHINGS".
/// </summary>
[Serializable]
public class OrderWeighingEntity : TableModel, ISerializable, ITableModel
{
	#region Public and private fields, properties, constructor

	/// <summary>
	/// Identity name.
	/// </summary>
	[XmlElement] public static ColumnName IdentityName => ColumnName.Uid;
	[XmlElement] public virtual OrderEntity Order { get; set; }
	[XmlElement] public virtual PluWeighingEntity PluWeighing { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
    public OrderWeighingEntity() : base(0, false)
	{
		Init();
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="identityUid"></param>
	/// <param name="isSetupDates"></param>
	public OrderWeighingEntity(Guid identityUid, bool isSetupDates) : base(identityUid, isSetupDates)
	{
		Init();
	}

    #endregion

    public new virtual void Init()
    {
	    base.Init();
	    Order = new();
	    PluWeighing = new();
    }

    #region Public and private methods

    public override string ToString()
    {
        return
			$"{nameof(IdentityUid)}: {IdentityUid}. " + 
			$"{nameof(IsMarked)}: {IsMarked}. " +
			$"{nameof(Order)}: {Order}. " + 
			$"{nameof(PluWeighing)}: {PluWeighing}. ";
    }

    public virtual bool Equals(OrderWeighingEntity item)
    {
		//if (item is null) return false;
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
        return Equals((OrderWeighingEntity)obj);
    }

	public override int GetHashCode() => IdentityUid.GetHashCode();

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
        OrderWeighingEntity item = new();
        item.Order = Order.CloneCast();
        item.PluWeighing = PluWeighing.CloneCast();
        item.Setup(((TableModel)this).CloneCast());
        return item;
    }

    public new virtual OrderWeighingEntity CloneCast() => (OrderWeighingEntity)Clone();

    #endregion
}
