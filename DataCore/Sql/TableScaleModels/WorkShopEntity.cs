// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "WorkShop".
/// </summary>
[Serializable]
public class WorkShopEntity : BaseEntity
{
	#region Public and private fields, properties, constructor

	/// <summary>
	/// Identity name.
	/// </summary>
	[XmlElement] public static ColumnName IdentityName => ColumnName.Id;
	[XmlElement] public virtual ProductionFacilityEntity ProductionFacility { get; set; }
	[XmlElement] public virtual string Name { get; set; }
	[XmlElement] public virtual Guid IdRRef { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
    public WorkShopEntity() : this(0)
    {
        //
    }

	/// <summary>
	/// Constructor.
	/// </summary>
    public WorkShopEntity(long id) : base(id)
    {
        ProductionFacility = new();
        Name = string.Empty;
        IdRRef = Guid.Empty;
    }

    #endregion

    #region Public and private methods

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
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WorkShopEntity)obj);
    }

	public override int GetHashCode() => IdentityUid.GetHashCode();

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
        item.Setup(((BaseEntity)this).CloneCast());
        return item;
    }

    public new virtual WorkShopEntity CloneCast() => (WorkShopEntity)Clone();

    #endregion
}
