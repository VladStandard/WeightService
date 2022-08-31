// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "ZebraPrinterResourceRef".
/// </summary>
[Serializable]
public class PrinterResourceModel : TableModel, ISerializable, ITableModel
{
	#region Public and private fields, properties, constructor

	[XmlElement] public virtual PrinterModel Printer { get; set; }
	[XmlElement] public virtual TemplateResourceModel Resource { get; set; }
	[XmlElement] public virtual string Description { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
    public PrinterResourceModel() : base(ColumnName.Id)
	{
		Printer = new();
		Resource = new();
		Description = string.Empty;
	}

	#endregion

	#region Public and private methods

	public override string ToString()
    {
        return
            $"{nameof(IsMarked)}: {IsMarked}. " +
            $"{nameof(Printer)}: {Printer}. " +
            $"{nameof(Resource)}: {Resource}. " +
            $"{nameof(Description)}: {Description}. ";
    }

    public virtual bool Equals(PrinterResourceModel item)
    {
        if (ReferenceEquals(this, item)) return true;
        if (!Printer.Equals(item.Printer))
            return false;
        if (!Resource.Equals(item.Resource))
            return false;
        return base.Equals(item) &&
               Equals(Description, item.Description);
    }

    public override bool Equals(object obj)
    {
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((PrinterResourceModel)obj);
    }

	public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        if (!Printer.EqualsDefault())
            return false;
        if (!Resource.EqualsDefault())
            return false;
        return base.EqualsDefault() &&
               Equals(Description, string.Empty);
    }

    public new virtual object Clone()
    {
        PrinterResourceModel item = new();
        item.Printer = Printer.CloneCast();
        item.Resource = Resource.CloneCast();
        item.Description = Description;
        item.Setup(((TableModel)this).CloneCast());
        return item;
    }

    public new virtual PrinterResourceModel CloneCast() => (PrinterResourceModel)Clone();

    #endregion
}
