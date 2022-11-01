// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "DEVICES_TYPES_FK".
/// </summary>
[Serializable]
public class DeviceTypeFkModel : SqlTableBase, ICloneable, ISqlDbBase, ISerializable
{
	#region Public and private fields, properties, constructor

	[XmlElement] public virtual DeviceModel Device { get; set; }
	[XmlElement] public virtual DeviceTypeModel DeviceType { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public DeviceTypeFkModel() : base(SqlFieldIdentityEnum.Uid)
	{
		Device = new();
		DeviceType = new();
	}

	/// <summary>
	/// Constructor for serialization.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
	private DeviceTypeFkModel(SerializationInfo info, StreamingContext context) : base(info, context)
	{
		Device = (DeviceModel)info.GetValue(nameof(Device), typeof(DeviceModel));
		DeviceType = (DeviceTypeModel)info.GetValue(nameof(DeviceTypeModel), typeof(DeviceTypeModel));
	}

	#endregion

	#region Public and private methods - override

	/// <summary>
	/// To string.
	/// </summary>
	/// <returns></returns>
	public override string ToString() =>
		$"{nameof(IsMarked)}: {IsMarked}. " +
		$"{nameof(Device)}: {Device}. " +
		$"{nameof(DeviceType)}: {DeviceType}. ";

	public override bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
		return Equals((DeviceTypeFkModel)obj);
	}

	public override int GetHashCode() => base.GetHashCode();

	public override bool EqualsNew() => Equals(new());

	public override bool EqualsDefault() =>
		base.EqualsDefault() &&
		Device.EqualsDefault() &&
		DeviceType.EqualsDefault();

	public override object Clone()
	{
		DeviceTypeFkModel item = new();
		item.Device = Device.CloneCast();
		item.DeviceType = DeviceType.CloneCast();
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
		info.AddValue(nameof(Device), Device);
		info.AddValue(nameof(DeviceType), DeviceType);
	}

	public override void FillProperties()
	{
		base.FillProperties();
		Device.FillProperties();
		DeviceType.FillProperties();
	}

	#endregion

	#region Public and private methods - virtual

	public virtual bool Equals(DeviceTypeFkModel item) =>
		ReferenceEquals(this, item) || base.Equals(item) && //-V3130
		Device.Equals(item.Device) &&
		DeviceType.Equals(item.DeviceType);

	public new virtual DeviceTypeFkModel CloneCast() => (DeviceTypeFkModel)Clone();

	#endregion
}
