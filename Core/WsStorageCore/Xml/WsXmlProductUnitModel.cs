// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Xml;

/// <summary>
/// XML-класс юнита продукта.
/// </summary>
[Serializable]
public class WsXmlProductUnitModel : ISerializable, IWsSqlDbBase
{
	#region Public and private fields, properties, constructor

	public decimal Heft { get; set; }
	public decimal Capacity { get; set; }
	public decimal Rate { get; set; }
	public int Threshold { get; set; }
	public string Okei { get; set; }
	public string Description { get; set; }

	public WsXmlProductUnitModel()
	{
		Heft = 0; Capacity = 0; Rate = 0; Threshold = 0; 
		Okei = string.Empty; Description = string.Empty;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
	private WsXmlProductUnitModel(SerializationInfo info, StreamingContext context)
	{
		Heft = info.GetDecimal(nameof(Heft));
		Capacity = info.GetDecimal(nameof(Capacity));
		Rate = info.GetDecimal(nameof(Rate));
		Threshold = info.GetInt32(nameof(Threshold));
		Okei = info.GetString(nameof(Okei));
		Description = info.GetString(nameof(Description));
	}

	#endregion

	#region Public and private methods

	public override string ToString() =>
		$"{nameof(Heft)}: {Heft}. " +
		$"{nameof(Capacity)}: {Capacity}. " +
		$"{nameof(Rate)}: {Rate}. " +
		$"{nameof(Threshold)}: {Threshold}. " +
		$"{nameof(Okei)}: {Okei}. " +
		$"{nameof(Description)}: {Description}. ";

	public bool Equals(WsXmlProductUnitModel item) =>
		ReferenceEquals(this, item) || Equals(Heft, item.Heft) && //-V3130
		Equals(Capacity, item.Capacity) &&
		Equals(Rate, item.Rate) &&
		Equals(Threshold, item.Threshold) &&
		Equals(Okei, item.Okei) &&
		Equals(Description, item.Description);

	public bool EqualsNew()
	{
		return Equals(new());
	}

	/// <summary>
	/// Clone class as object.
	/// </summary>
	/// <returns></returns>
	public object Clone()
	{
		WsXmlProductUnitModel item = new();
		item.Heft = Heft;
		item.Capacity = Capacity;
		item.Rate = Rate;
		item.Threshold = Threshold;
		item.Okei = Okei;
		item.Description = Description;
		return item;
	}

	/// <summary>
	/// Get object data for serialization info.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
	public void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue(nameof(Heft), Heft);
		info.AddValue(nameof(Capacity), Capacity);
		info.AddValue(nameof(Rate), Rate);
		info.AddValue(nameof(Threshold), Threshold);
		info.AddValue(nameof(Okei), Okei);
		info.AddValue(nameof(Description), Description);
	}

	public void ClearNullProperties()
	{
		throw new NotImplementedException();
	}

	public virtual void FillProperties()
	{
		throw new NotImplementedException();
	}

	#endregion
}