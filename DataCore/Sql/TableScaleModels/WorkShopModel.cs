// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "WorkShop".
/// </summary>
[Serializable]
public class WorkShopModel : SqlTableBase, ICloneable, ISqlDbBase, ISerializable
{
	#region Public and private fields, properties, constructor

	[XmlElement] public virtual ProductionFacilityModel ProductionFacility { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
    public WorkShopModel() : base(SqlFieldIdentityEnum.Id)
	{
		ProductionFacility = new();
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
	private WorkShopModel(SerializationInfo info, StreamingContext context) : base(info, context)
	{
		ProductionFacility = (ProductionFacilityModel)info.GetValue(nameof(ProductionFacility), typeof(ProductionFacilityModel));
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

	public override bool EqualsDefault() =>
		base.EqualsDefault() &&
		ProductionFacility.EqualsDefault();

	public override object Clone()
    {
        WorkShopModel item = new();
        item.ProductionFacility = ProductionFacility.CloneCast();
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
	}

	public override void FillProperties()
	{
		base.FillProperties();
		ProductionFacility.FillProperties();
	}

	#endregion

	#region Public and private methods

	public virtual bool Equals(WorkShopModel item) =>
		ReferenceEquals(this, item) || base.Equals(item) &&
		ProductionFacility.Equals(item.ProductionFacility);

	public new virtual WorkShopModel CloneCast() => (WorkShopModel)Clone();

	#endregion
}
