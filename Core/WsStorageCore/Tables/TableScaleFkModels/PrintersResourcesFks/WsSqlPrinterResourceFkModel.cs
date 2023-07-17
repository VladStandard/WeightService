// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleFkModels.PrintersResourcesFks;

/// <summary>
/// Table "ZebraPrinterResourceRef".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlPrinterResourceFkModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual WsSqlPrinterModel Printer { get; set; }
    [XmlElement] public virtual WsSqlTemplateResourceModel TemplateResource { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlPrinterResourceFkModel() : base(WsSqlEnumFieldIdentity.Id)
    {
        Printer = new();
        TemplateResource = new();
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlPrinterResourceFkModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Printer = (WsSqlPrinterModel)info.GetValue(nameof(Printer), typeof(WsSqlPrinterModel));
        TemplateResource = (WsSqlTemplateResourceModel)info.GetValue(nameof(TemplateResource), typeof(WsSqlTemplateResourceModel));
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

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Printer), Printer);
        info.AddValue(nameof(TemplateResource), TemplateResource);
    }

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