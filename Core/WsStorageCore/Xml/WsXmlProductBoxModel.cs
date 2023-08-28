namespace WsStorageCore.Xml;

/// <summary>
/// XML-класс коробки.
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsXmlProductBoxModel : ISerializable, IWsSqlObjectBase
{
	#region Public and private fields, properties, constructor

	public string Description { get; set; }
	/// <summary>
	/// Вес.
	/// </summary>
	public decimal Heft { get; set; }
	/// <summary>
	/// .
	/// </summary>
	public decimal Capacity { get; set; }
	/// <summary>
	/// .
	/// </summary>
	public decimal Rate { get; set; }
	public int Threshold { get; set; }
	public string Okei { get; set; }
	public string Unit { get; set; }

	public WsXmlProductBoxModel()
	{
		Description = string.Empty;
		Heft = 0;
		Capacity = 0;
		Rate = 0;
		Threshold = 0;
		Okei = string.Empty;
		Unit = string.Empty;
	}
    
	private WsXmlProductBoxModel(SerializationInfo info, StreamingContext context)
	{
		Description = info.GetString(nameof(Description));
		Heft = info.GetDecimal(nameof(Heft));
		Capacity = info.GetDecimal(nameof(Capacity));
		Rate = info.GetDecimal(nameof(Rate));
		Threshold = info.GetInt32(nameof(Threshold));
		Okei = info.GetString(nameof(Okei));
		Unit = info.GetString(nameof(Unit));
	}

    public WsXmlProductBoxModel(WsXmlProductBoxModel item)
    {
        Description = item.Description;
        Heft = item.Heft;
        Capacity = item.Capacity;
        Rate = item.Rate;
        Threshold = item.Threshold;
        Okei = item.Okei;
        Unit = item.Unit;
    }

    #endregion

    #region Public and private methods

    public override string ToString() =>
		$"{nameof(Description)}: {Description}. " +
		$"{nameof(Heft)}: {Heft}. " +
		$"{nameof(Capacity)}: {Capacity}. " +
		$"{nameof(Rate)}: {Rate}. " +
		$"{nameof(Threshold)}: {Threshold}. " +
		$"{nameof(Okei)}: {Okei}. " +
		$"{nameof(Unit)}: {Unit}. ";

	public virtual bool Equals(WsXmlProductBoxModel item) =>
		ReferenceEquals(this, item) || Equals(Description, item.Description) && //-V3130
		Equals(Heft, item.Heft) &&
		Equals(Capacity, item.Capacity) &&
		Equals(Rate, item.Rate) &&
		Equals(Threshold, item.Threshold) &&
		Equals(Okei, item.Okei) &&
		Equals(Unit, item.Unit);

	public virtual bool EqualsNew()
	{
		return Equals(new());
	}

	/// <summary>
	/// Get object data for serialization info.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
	public void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue(nameof(Description), Description);
		info.AddValue(nameof(Heft), Heft);
		info.AddValue(nameof(Capacity), Capacity);
		info.AddValue(nameof(Rate), Rate);
		info.AddValue(nameof(Threshold), Threshold);
		info.AddValue(nameof(Okei), Okei);
		info.AddValue(nameof(Unit), Unit);
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