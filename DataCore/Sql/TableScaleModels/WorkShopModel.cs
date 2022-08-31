// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "WorkShop".
/// </summary>
[Serializable]
public class WorkShopModel : TableModel, ISerializable, ITableModel
{
	#region Public and private fields, properties, constructor

	[XmlElement] public virtual ProductionFacilityModel ProductionFacility { get; set; }
	[XmlElement] public virtual string Name { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
    public WorkShopModel() : base(ColumnName.Id)
	{
		ProductionFacility = new();
		Name = string.Empty;
	}

    #endregion

    #region Public and private methods

	public override string ToString()
    {
        return
			$"{nameof(IsMarked)}: {IsMarked}. " +
			$"{nameof(Name)}: {Name}. " +
			$"{nameof(ProductionFacility)}: {ProductionFacility}. ";
    }

    public virtual bool Equals(WorkShopModel item)
    {
        if (ReferenceEquals(this, item)) return true;
        if (!ProductionFacility.Equals(item.ProductionFacility))
            return false;
        return 
	        base.Equals(item) &&
            Equals(Name, item.Name);
    }

    public override bool Equals(object obj)
    {
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((WorkShopModel)obj);
    }

	public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        if (!ProductionFacility.EqualsDefault())
            return false;
        return 
	        base.EqualsDefault() &&
            Equals(Name, string.Empty);
    }

    public new virtual object Clone()
    {
        WorkShopModel item = new();
        item.ProductionFacility = ProductionFacility.CloneCast();
        item.Name = Name;
        item.Setup(((TableModel)this).CloneCast());
        return item;
    }

    public new virtual WorkShopModel CloneCast() => (WorkShopModel)Clone();

    #endregion
}
