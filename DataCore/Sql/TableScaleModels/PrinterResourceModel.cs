// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "ZebraPrinterResourceRef".
/// </summary>
[Serializable]
public class PrinterResourceModel : SqlTableBase, ICloneable, ISqlDbBase, ISerializable
{
	#region Public and private fields, properties, constructor

	[XmlElement] public virtual PrinterModel Printer { get; set; }
	[XmlElement] public virtual TemplateResourceModel Resource { get; set; }
	
	/// <summary>
	/// Constructor.
	/// </summary>
    public PrinterResourceModel() : base(SqlFieldIdentityEnum.Id)
	{
		Printer = new();
		Resource = new();
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
	private PrinterResourceModel(SerializationInfo info, StreamingContext context) : base(info, context)
	{
		Printer = (PrinterModel)info.GetValue(nameof(Printer), typeof(PrinterModel));
		Resource = (TemplateResourceModel)info.GetValue(nameof(Resource), typeof(TemplateResourceModel));
	}

	#endregion

	#region Public and private methods - override

	public override string ToString() =>
		$"{nameof(IsMarked)}: {IsMarked}. " +
		$"{nameof(Printer)}: {Printer}. " +
		$"{nameof(Resource)}: {Resource}. ";

	public override bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((PrinterResourceModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

	public override bool EqualsNew() => Equals(new());

	public override bool EqualsDefault()
    {
        if (!Printer.EqualsDefault())
            return false;
        if (!Resource.EqualsDefault())
            return false;
        return base.EqualsDefault();
    }

	public override object Clone()
    {
        PrinterResourceModel item = new();
        item.Printer = Printer.CloneCast();
        item.Resource = Resource.CloneCast();
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
		info.AddValue(nameof(Printer), Printer);
		info.AddValue(nameof(Resource), Resource);
	}

	#endregion

	#region Public and private methods - virtual

	public virtual bool Equals(PrinterResourceModel item)
	{
		if (ReferenceEquals(this, item)) return true;
		if (!Printer.Equals(item.Printer))
			return false;
		if (!Resource.Equals(item.Resource))
			return false;
		return base.Equals(item);
	}

	public new virtual PrinterResourceModel CloneCast() => (PrinterResourceModel)Clone();

	#endregion
}
