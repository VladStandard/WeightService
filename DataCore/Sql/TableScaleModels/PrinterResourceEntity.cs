// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "ZebraPrinterResourceRef".
/// </summary>
[Serializable]
public class PrinterResourceEntity : BaseEntity, ISerializable, IBaseEntity
{
	#region Public and private fields, properties, constructor

	/// <summary>
	/// Identity name.
	/// </summary>
	[XmlElement] public static ColumnName IdentityName => ColumnName.Id;
	[XmlElement] public virtual PrinterEntity Printer { get; set; }
	[XmlElement] public virtual TemplateResourceEntity Resource { get; set; }
	[XmlElement] public virtual string Description { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
    public PrinterResourceEntity() : base(0, false)
	{
		Init();
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="identityId"></param>
	/// <param name="isSetupDates"></param>
	public PrinterResourceEntity(long identityId, bool isSetupDates) : base(identityId, isSetupDates)
	{
		Init();
	}

	#endregion

	#region Public and private methods

	public new virtual void Init()
	{
		base.Init();
		Printer = new();
		Resource = new();
		Description = string.Empty;
	}

	public override string ToString()
    {
        string strPrinter = Printer != null ? Printer.IdentityId.ToString() : "null";
        string strResource = Resource != null ? Resource.IdentityId.ToString() : "null";
        return
			$"{nameof(IdentityId)}: {IdentityId}. " + 
            $"{nameof(IsMarked)}: {IsMarked}. " +
            $"{nameof(Printer)}: {strPrinter}. " +
            $"{nameof(Resource)}: {strResource}. " +
            $"{nameof(Description)}: {Description}. ";
    }

    public virtual bool Equals(PrinterResourceEntity item)
    {
        if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        if (Printer != null && item.Printer != null && !Printer.Equals(item.Printer))
            return false;
        if (Resource != null && item.Resource != null && !Resource.Equals(item.Resource))
            return false;
        return base.Equals(item) &&
               Equals(Description, item.Description);
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PrinterResourceEntity)obj);
    }

	public override int GetHashCode() => IdentityId.GetHashCode();

	public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        if (Printer != null && !Printer.EqualsDefault())
            return false;
        if (Resource != null && !Resource.EqualsDefault())
            return false;
        return base.EqualsDefault() &&
               Equals(Description, string.Empty);
    }

    public new virtual object Clone()
    {
        PrinterResourceEntity item = new();
        item.Printer = Printer.CloneCast();
        item.Resource = Resource.CloneCast();
        item.Description = Description;
        item.Setup(((BaseEntity)this).CloneCast());
        return item;
    }

    public new virtual PrinterResourceEntity CloneCast() => (PrinterResourceEntity)Clone();

    #endregion
}
