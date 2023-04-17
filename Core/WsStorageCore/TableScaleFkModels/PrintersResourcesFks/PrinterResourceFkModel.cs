// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Enums;
using WsStorageCore.Tables;
using WsStorageCore.TableScaleModels.Printers;
using WsStorageCore.TableScaleModels.TemplatesResources;

namespace WsStorageCore.TableScaleFkModels.PrintersResourcesFks;

/// <summary>
/// Table "ZebraPrinterResourceRef".
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(PrinterResourceFkModel)} {Printer.Name}")]
public class PrinterResourceFkModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual PrinterModel Printer { get; set; }
    [XmlElement] public virtual TemplateResourceModel TemplateResource { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public PrinterResourceFkModel() : base(WsSqlFieldIdentity.Id)
    {
        Printer = new();
        TemplateResource = new();
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected PrinterResourceFkModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Printer = (PrinterModel)info.GetValue(nameof(Printer), typeof(PrinterModel));
        TemplateResource = (TemplateResourceModel)info.GetValue(nameof(TemplateResource), typeof(TemplateResourceModel));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Printer)}: {Printer}. " +
        $"{nameof(TemplateResource)}: {TemplateResource}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PrinterResourceFkModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Printer.EqualsDefault() &&
        TemplateResource.EqualsDefault();

    public override object Clone()
    {
        PrinterResourceFkModel item = new();
        item.CloneSetup(base.CloneCast());
        item.Printer = Printer.CloneCast();
        item.TemplateResource = TemplateResource.CloneCast();
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

    public virtual bool Equals(PrinterResourceFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Printer.Equals(item.Printer) &&
        TemplateResource.Equals(item.TemplateResource);

    public new virtual PrinterResourceFkModel CloneCast() => (PrinterResourceFkModel)Clone();

    #endregion
}
