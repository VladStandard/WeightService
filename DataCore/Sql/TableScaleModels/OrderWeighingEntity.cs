// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "ORDERS_WEIGHINGS".
/// </summary>
[Serializable]
public class OrderWeighingEntity : BaseEntity, ISerializable, IBaseEntity
{
	#region Public and private fields, properties, constructor

	/// <summary>
	/// Identity name.
	/// </summary>
	[XmlElement] public static ColumnName IdentityName => ColumnName.Uid;
	[XmlElement] public virtual OrderEntity Order { get; set; }
	[XmlElement] public virtual WeithingFactEntity Fact { get; set; }

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
	    Fact = new();
    }

    #region Public and private methods

    public override string ToString()
    {
        return
			$"{nameof(IdentityUid)}: {IdentityUid}. " + 
			$"{nameof(IsMarked)}: {IsMarked}. " +
			$"{nameof(Order)}: {Order.IdentityUid}. " + 
			$"{nameof(Fact)}: {Fact.IdentityId}. ";
    }

    public virtual bool Equals(OrderWeighingEntity item)
    {
		//if (item is null) return false;
		if (!Order.Equals(item.Order))
			return false;
		if (!Fact.Equals(item.Fact))
			return false;
        if (ReferenceEquals(this, item)) return true;
        return base.Equals(item);
    }

    public override bool Equals(object obj)
    {
        //if (obj is null) return false;
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
		if (!Fact.EqualsDefault())
			return false;
        return base.EqualsDefault();
    }

    public new virtual object Clone()
    {
        OrderWeighingEntity item = new();
        item.Order = Order.CloneCast();
        item.Fact = Fact.CloneCast();
        item.Setup(((BaseEntity)this).CloneCast());
        return item;
    }

    public new virtual OrderWeighingEntity CloneCast() => (OrderWeighingEntity)Clone();

    #endregion
}
