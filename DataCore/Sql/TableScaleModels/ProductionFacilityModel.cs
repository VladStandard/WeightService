// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable MissingXmlDoc

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "ProductionFacility".
/// </summary>
[Serializable]
public class ProductionFacilityModel : TableModel, ISerializable, ITableModel
{
	#region Public and private fields, properties, constructor

	[XmlElement] public virtual string Name { get; set; }
	[XmlElement] public virtual string Address { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public ProductionFacilityModel() : base(ColumnName.Id)
	{
		Name = string.Empty;
		Address = string.Empty;
	}

	#endregion

	#region Public and private methods

	public override string ToString() =>
	    $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(Address)}: {Address}. ";

    public virtual bool Equals(ProductionFacilityModel item)
    {
        //if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        return base.Equals(item) &&
               Equals(Name, item.Name) &&
               Equals(Address, item.Address);
    }

    public override bool Equals(object obj)
    {
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((ProductionFacilityModel)obj);
    }

	public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        return 
	        base.EqualsDefault() &&
            Equals(Name, string.Empty) &&
            Equals(Address, string.Empty);
    }

    public new virtual object Clone()
    {
        ProductionFacilityModel item = new();
        item.Name = Name;
        item.Address = Address;
        item.Setup(((TableModel)this).CloneCast());
        return item;
    }

    public new virtual ProductionFacilityModel CloneCast() => (ProductionFacilityModel)Clone();

    #endregion
}
