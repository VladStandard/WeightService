namespace WsStorageCore.Tables.TableScaleFkModels.PrintersResourcesFks;

[DebuggerDisplay("{ToString()}")]
public class WsSqlPrinterResourceFkModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    public virtual WsSqlPrinterModel Printer { get; set; }
    public virtual WsSqlTemplateResourceModel TemplateResource { get; set; }
    
    public WsSqlPrinterResourceFkModel() : base(WsSqlEnumFieldIdentity.Id)
    {
        Printer = new();
        TemplateResource = new();
    }
    
    public WsSqlPrinterResourceFkModel(WsSqlPrinterResourceFkModel item) : base(item)
    {
        Printer = new(item.Printer);
        TemplateResource = new(item.TemplateResource);
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsMarked()} | " +
        $"{nameof(Printer)}: {Printer}. " +
        $"{nameof(TemplateResource)}: {TemplateResource}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlPrinterResourceFkModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Printer.EqualsDefault() &&
        TemplateResource.EqualsDefault();

    public override void FillProperties()
    {
        base.FillProperties();
        Printer.FillProperties();
        TemplateResource.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlPrinterResourceFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Printer.Equals(item.Printer) &&
        TemplateResource.Equals(item.TemplateResource);

    #endregion
}