// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable MissingXmlDoc

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "ProductionFacility".
/// </summary>
[Serializable]
public class ProductionFacilityEntity : TableModel, ISerializable, ITableModel
{
	#region Public and private fields, properties, constructor

	[XmlElement] public virtual string Name { get; set; }
	[XmlElement] public virtual string Address { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public ProductionFacilityEntity() : base(ColumnName.Id, 0, false)
	{
		Init();
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="identityId"></param>
	/// <param name="isSetupDates"></param>
	public ProductionFacilityEntity(long identityId, bool isSetupDates) : base(ColumnName.Id, identityId, isSetupDates)
	{
		Init();
	}

    #endregion

    #region Public and private methods

    public new virtual void Init()
    {
	    base.Init();
		Name = string.Empty;
		Address = string.Empty;
	}

	public override string ToString() =>
	    $"{nameof(IdentityId)}: {IdentityId}. " +
	    $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(Address)}: {Address}. ";

    public virtual bool Equals(ProductionFacilityEntity item)
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
        return Equals((ProductionFacilityEntity)obj);
    }

	public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        return base.EqualsDefault() &&
               Equals(Name, string.Empty) &&
               Equals(Address, string.Empty);
    }

    public new virtual object Clone()
    {
        ProductionFacilityEntity item = new();
        item.Name = Name;
        item.Address = Address;
        item.Setup(((TableModel)this).CloneCast());
        return item;
    }

    public new virtual ProductionFacilityEntity CloneCast() => (ProductionFacilityEntity)Clone();

    #endregion
}
