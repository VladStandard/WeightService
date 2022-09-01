// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.Xml;

/// <summary>
/// XML-класс штрих-кода.
/// </summary>
[Serializable]
public class XmlProductBarcodeModel : ISerializable, ITableModel
{
	#region Public and private fields, properties, constructor

	public string Type { get; set; }
	public string Barcode { get; set; }

	public XmlProductBarcodeModel()
	{
		Type = string.Empty;
		Barcode = string.Empty;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
	private XmlProductBarcodeModel(SerializationInfo info, StreamingContext context)
	{
		Type = info.GetString(nameof(Type));
		Barcode = info.GetString(nameof(Barcode));
	}

	#endregion

	#region Public and private methods

	public new virtual string ToString() =>
		$"{nameof(Type)}: {Type}. " +
		$"{nameof(Barcode)}: {Barcode}. ";

	public virtual bool Equals(XmlProductBarcodeModel item)
	{
		if (ReferenceEquals(this, item)) return true;
		return
			Equals(Type, item.Type) &&
			Equals(Barcode, item.Barcode);
	}

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
		XmlProductBarcodeModel item = new();
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

	#endregion
}