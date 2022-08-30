// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "WorkShop".
/// </summary>
[Serializable]
public class WorkShopEntity : TableModel, ISerializable, ITableModel
{
	#region Public and private fields, properties, constructor

	[XmlElement] public virtual ProductionFacilityEntity ProductionFacility { get; set; }
	[XmlElement] public virtual string Name { get; set; }
	[XmlElement] public virtual Guid IdRRef { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
    public WorkShopEntity() : base(ColumnName.Id, 0, false)
	{
		Init();
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="identityId"></param>
	/// <param name="isSetupDates"></param>
	public WorkShopEntity(long identityId, bool isSetupDates) : base(ColumnName.Id, identityId, isSetupDates)
    {
	    Init();
    }

    #endregion

    #region Public and private methods

    public new virtual void Init()
    {
		base.Init();
	    ProductionFacility = new();
	    Name = string.Empty;
	    IdRRef = Guid.Empty;
    }

	public override string ToString()
    {
        string strProductionFacility = ProductionFacility != null ? ProductionFacility.IdentityId.ToString() : "null";
        return
			$"{nameof(IdentityId)}: {IdentityId}. " + 
			$"{nameof(IsMarked)}: {IsMarked}. " +
			$"{nameof(ProductionFacility)}: {strProductionFacility}. " +
			$"{nameof(Name)}: {Name}. " +
			$"{nameof(IdRRef)}: {IdRRef}. ";
    }

    public virtual bool Equals(WorkShopEntity item)
    {
        if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        if (ProductionFacility != null && item.ProductionFacility != null && !ProductionFacility.Equals(item.ProductionFacility))
            return false;
        return base.Equals(item) &&
               Equals(Name, item.Name) &&
               Equals(IdRRef, item.IdRRef);
    }

    public override bool Equals(object obj)
    {
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((WorkShopEntity)obj);
    }

	public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        if (ProductionFacility != null && !ProductionFacility.EqualsDefault())
            return false;
        return base.EqualsDefault() &&
               Equals(Name, string.Empty) &&
               Equals(IdRRef, Guid.Empty);
    }

    public new virtual object Clone()
    {
        WorkShopEntity item = new();
        item.ProductionFacility = ProductionFacility.CloneCast();
        item.Name = Name;
        item.IdRRef = IdRRef;
        item.Setup(((TableModel)this).CloneCast());
        return item;
    }

    public new virtual WorkShopEntity CloneCast() => (WorkShopEntity)Clone();

    #endregion
}
