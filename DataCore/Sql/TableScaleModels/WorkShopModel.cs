// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core;
using DataCore.Sql.Tables;
using static DataCore.Sql.Core.SqlQueries.DbScales.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "WorkShop".
/// </summary>
[Serializable]
public class WorkShopModel : TableBaseModel, ICloneable, IDbBaseModel, ISerializable
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

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
	private WorkShopModel(SerializationInfo info, StreamingContext context) : base(info, context)
	{
		ProductionFacility = (ProductionFacilityModel)info.GetValue(nameof(ProductionFacility), typeof(ProductionFacilityModel));
		Name = info.GetString(nameof(Name));
	}

	#endregion

	#region Public and private methods - override

	public override string ToString() =>
		$"{nameof(IsMarked)}: {IsMarked}. " +
		$"{nameof(Name)}: {Name}. " +
		$"{nameof(ProductionFacility)}: {ProductionFacility}. ";

	public override bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((WorkShopModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

	public override bool EqualsNew() => Equals(new());

	public override bool EqualsDefault()
    {
        if (!ProductionFacility.EqualsDefault())
            return false;
        return 
	        base.EqualsDefault() &&
            Equals(Name, string.Empty);
    }

	public override object Clone()
    {
        WorkShopModel item = new();
        item.ProductionFacility = ProductionFacility.CloneCast();
        item.Name = Name;
		item.CloneSetup(base.CloneCast());
		return item;
    }

	/// <summary>
	/// Get object data for serialization info.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		base.GetObjectData(info, context);
		info.AddValue(nameof(ProductionFacility), ProductionFacility);
		info.AddValue(nameof(Name), Name);
	}

	#endregion

	#region Public and private methods

	public virtual bool Equals(WorkShopModel item)
	{
		if (ReferenceEquals(this, item)) return true;
		if (!ProductionFacility.Equals(item.ProductionFacility))
			return false;
		return
			base.Equals(item) &&
			Equals(Name, item.Name);
	}

	public new virtual WorkShopModel CloneCast() => (WorkShopModel)Clone();

	#endregion
}
