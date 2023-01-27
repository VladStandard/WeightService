// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Enums;
using DataCore.Sql.Tables;
using DataCore.Sql.TableScaleModels.Printers;
using DataCore.Sql.TableScaleModels.TemplatesResources;

namespace DataCore.Sql.TableScaleModels.PrintersResources;

/// <summary>
/// Table "ZebraPrinterResourceRef".
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(PrinterResourceModel)}")]
public class PrinterResourceModel : SqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual PrinterModel Printer { get; set; }
    [XmlElement] public virtual TemplateResourceModel TemplateResource { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public PrinterResourceModel() : base(SqlFieldIdentity.Id)
    {
        Printer = new();
        TemplateResource = new();
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected PrinterResourceModel(SerializationInfo info, StreamingContext context) : base(info, context)
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
        return Equals((PrinterResourceModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Printer.EqualsDefault() &&
        TemplateResource.EqualsDefault();

    public override object Clone()
    {
        PrinterResourceModel item = new();
        item.Printer = Printer.CloneCast();
        item.TemplateResource = TemplateResource.CloneCast();
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

    public virtual bool Equals(PrinterResourceModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Printer.Equals(item.Printer) &&
        TemplateResource.Equals(item.TemplateResource);

    public new virtual PrinterResourceModel CloneCast() => (PrinterResourceModel)Clone();

    #endregion
}
