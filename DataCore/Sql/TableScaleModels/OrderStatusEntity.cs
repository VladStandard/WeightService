// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "OrderStatus".
/// </summary>
[Serializable]
public class OrderStatusEntity : BaseEntity, ISerializable, IBaseEntity
{
	#region Public and private fields, properties, constructor

	/// <summary>
	/// Identity name.
	/// </summary>
	[XmlElement] public static ColumnName IdentityName => ColumnName.Id;
	[XmlElement] public virtual string OrderId { get; set; }
	[XmlElement] public virtual DateTime CurrentDate { get; set; }
	[XmlElement] public virtual byte CurrentStatus { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
    public OrderStatusEntity() : base(0, false)
	{
		Init();
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="identityId"></param>
	/// <param name="isSetupDates"></param>
	public OrderStatusEntity(long identityId, bool isSetupDates) : base(identityId, isSetupDates)
    {
		Init();
	}

    #endregion

    public new virtual void Init()
    {
	    base.Init();
        OrderId = string.Empty;
        CurrentDate = DateTime.MinValue;
        CurrentStatus = 0x00;
    }

    #region Public and private methods

    public override string ToString() =>
	    $"{nameof(IdentityId)}: {IdentityId}. " +
	    $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(OrderId)}: {OrderId}. " +
        $"{nameof(CurrentDate)}: {CurrentDate}. " +
        $"{nameof(CurrentStatus)}: {CurrentStatus}. ";

    public virtual bool Equals(OrderStatusEntity item)
    {
        if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        return base.Equals(item) &&
               Equals(OrderId, item.OrderId) &&
               Equals(CurrentDate, item.CurrentDate) &&
               Equals(CurrentStatus, item.CurrentStatus);
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((OrderStatusEntity)obj);
    }

	public override int GetHashCode() => IdentityId.GetHashCode();

	public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        return base.EqualsDefault() &&
               Equals(OrderId, string.Empty) &&
               Equals(CurrentDate, DateTime.MinValue) &&
               Equals(CurrentStatus, (byte)0x00);
    }

    public new virtual object Clone()
    {
        OrderStatusEntity item = new();
        item.OrderId = OrderId;
        item.CurrentDate = CurrentDate;
        item.CurrentStatus = CurrentStatus;
        item.Setup(((BaseEntity)this).CloneCast());
        return item;
    }

    public new virtual OrderStatusEntity CloneCast() => (OrderStatusEntity)Clone();

    #endregion
}
