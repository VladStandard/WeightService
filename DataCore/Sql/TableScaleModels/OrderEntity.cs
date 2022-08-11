// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "Orders".
/// </summary>
[Serializable]
public class OrderEntity : BaseEntity, ISerializable, IBaseEntity
{
	#region Public and private fields, properties, constructor

	/// <summary>
	/// Identity name.
	/// </summary>
	[XmlElement] public static ColumnName IdentityName => ColumnName.Id;
	[XmlElement] public virtual OrderTypeEntity OrderTypes { get; set; }
	[XmlElement] public virtual DateTime ProductDate { get; set; }
	[XmlElement] public virtual int? PlaneBoxCount { get; set; }
	[XmlElement] public virtual int? PlanePalletCount { get; set; }
	[XmlElement] public virtual DateTime PlanePackingOperationBeginDate { get; set; }
	[XmlElement] public virtual DateTime PlanePackingOperationEndDate { get; set; }
	[XmlElement] public virtual ScaleEntity Scales { get; set; }
	[XmlElement] public virtual PluEntity Plu { get; set; }
	[XmlElement] public virtual Guid IdRRef { get; set; }
	[XmlElement] public virtual TemplateEntity Template { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
    public OrderEntity() : base(0, false)
	{
		Init();
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="identityId"></param>
	/// <param name="isSetupDates"></param>
	public OrderEntity(long identityId, bool isSetupDates) : base(identityId, isSetupDates)
	{
		Init();
	}

    #endregion

    public new virtual void Init()
    {
	    base.Init();
        OrderTypes = new();
        ProductDate = DateTime.MinValue;
        PlaneBoxCount = default;
        PlanePalletCount = default;
        PlanePackingOperationBeginDate = DateTime.MinValue;
        PlanePackingOperationEndDate = DateTime.MinValue;
        Scales = new();
        Plu = new();
        IdRRef = Guid.Empty;
        Template = new();
    }

    #region Public and private methods

    public override string ToString()
    {
        string strOrderTypes = OrderTypes != null ? OrderTypes.IdentityId.ToString() : "null";
        string strScales = Scales != null ? Scales.IdentityId.ToString() : "null";
        string strPlu = Plu != null ? Plu.IdentityId.ToString() : "null";
        string strTemplates = Template != null ? Template.IdentityId.ToString() : "null";
        return
			$"{nameof(IdentityId)}: {IdentityId}. " + 
			$"{nameof(IsMarked)}: {IsMarked}. " +
			$"{nameof(OrderTypes)}: {strOrderTypes}. " +
			$"{nameof(ProductDate)}: {ProductDate}. " +
			$"{nameof(PlaneBoxCount)}: {PlaneBoxCount}. " +
			$"{nameof(PlanePalletCount)}: {PlanePalletCount}. " +
			$"{nameof(PlanePackingOperationBeginDate)}: {PlanePackingOperationBeginDate}. " +
			$"{nameof(PlanePackingOperationEndDate)}: {PlanePackingOperationEndDate}. " +
			$"{nameof(Scales)}: {strScales}. " +
			$"{nameof(Plu)}: {strPlu}." +
			$"{nameof(IdRRef)}: {IdRRef}." +
			$"{nameof(Template)}: {strTemplates}.";
    }

    public virtual bool Equals(OrderEntity item)
    {
        if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        if (OrderTypes != null && item.OrderTypes != null && !OrderTypes.Equals(item.OrderTypes))
            return false;
        if (Scales != null && item.Scales != null && !Scales.Equals(item.Scales))
            return false;
        if (Plu != null && item.Plu != null && !Plu.Equals(item.Plu))
            return false;
        if (Template != null && item.Template != null && !Template.Equals(item.Template))
            return false;
        return base.Equals(item) &&
               Equals(ProductDate, item.ProductDate) &&
               Equals(PlaneBoxCount, item.PlaneBoxCount) &&
               Equals(PlanePalletCount, item.PlanePalletCount) &&
               Equals(PlanePackingOperationBeginDate, item.PlanePackingOperationBeginDate) &&
               Equals(PlanePackingOperationEndDate, item.PlanePackingOperationEndDate) &&
               Equals(IdRRef, item.IdRRef);
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((OrderEntity)obj);
    }

	public override int GetHashCode() => IdentityId.GetHashCode();

	public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        if (OrderTypes != null && !OrderTypes.EqualsDefault())
            return false;
        if (Plu != null && !Plu.EqualsDefault())
            return false;
        if (Scales != null && !Scales.EqualsDefault())
            return false;
        if (Template != null && !Template.EqualsDefault())
            return false;
        return base.EqualsDefault() &&
               Equals(ProductDate, DateTime.MinValue) &&
               Equals(PlaneBoxCount, null) &&
               Equals(PlanePalletCount, null) &&
               Equals(PlanePackingOperationBeginDate, DateTime.MinValue) &&
               Equals(PlanePackingOperationEndDate, DateTime.MinValue) &&
               Equals(IdRRef, Guid.Empty);
    }

    public new virtual object Clone()
    {
        OrderEntity item = new();
        item.OrderTypes = OrderTypes.CloneCast();
        item.ProductDate = ProductDate;
        item.PlaneBoxCount = PlaneBoxCount;
        item.PlanePalletCount = PlanePalletCount;
        item.PlanePackingOperationBeginDate = PlanePackingOperationBeginDate;
        item.PlanePackingOperationEndDate = PlanePackingOperationEndDate;
        item.Scales = Scales.CloneCast();
        item.Plu = Plu.CloneCast();
        item.IdRRef = IdRRef;
        item.Template = Template.CloneCast();
        item.Setup(((BaseEntity)this).CloneCast());
        return item;
    }

    public new virtual OrderEntity CloneCast() => (OrderEntity)Clone();

    #endregion
}
