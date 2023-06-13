// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Xml;

/// <summary>
/// XML-класс штрих-кода.
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsXmlProductBarcodeModel : ISerializable, IWsSqlDbBase
{
	#region Public and private fields, properties, constructor

	public string Type { get; set; }
	public string Barcode { get; set; }

	public WsXmlProductBarcodeModel()
	{
		Type = string.Empty;
		Barcode = string.Empty;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
	private WsXmlProductBarcodeModel(SerializationInfo info, StreamingContext context)
	{
		Type = info.GetString(nameof(Type));
		Barcode = info.GetString(nameof(Barcode));
	}

	#endregion

	#region Public and private methods

	public override string ToString() =>
		$"{nameof(Type)}: {Type}. " +
		$"{nameof(Barcode)}: {Barcode}. ";

	public virtual bool Equals(WsXmlProductBarcodeModel item) =>
		ReferenceEquals(this, item) || Equals(Type, item.Type) && //-V3130
		Equals(Barcode, item.Barcode);

	public virtual bool EqualsNew()
	{
		return Equals(new());
	}

	/// <summary>
	/// Clone class as object.
	/// </summary>
	/// <returns></returns>
	public object Clone()
	{
		WsXmlProductBarcodeModel item = new();
		item.Type = Type;
		item.Barcode = Barcode;
		return item;
	}

	/// <summary>
	/// Get object data for serialization info.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
	public void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue(nameof(Type), Type);
		info.AddValue(nameof(Barcode), Barcode);
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