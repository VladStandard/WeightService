// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable VirtualMemberCallInConstructor

using DataCore.Sql.TableScaleModels.PlusScales;

namespace DataCore.Sql.Core.Models;

/// <summary>
/// PLU label context.
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(PluLabelContextModel)}")]
public class PluLabelContextModel : SerializeBase
{
    #region Public and private fields, properties, constructor


    [XmlIgnore] private PluScaleModel PluScale { get; set; }
    [XmlElement] public virtual DateTime ProductDt { get; set; }
    [XmlElement]
    public virtual string ProductDtFormat
    {
        get => $"{ProductDt:dd.MM.yyyy}";
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string LotNumberFormat
    {
        get => $"{ProductDt:yyMM}";
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string ProductDateBarCodeFormat
    {
        get => $"{ProductDt:yyMMdd}";
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string ProductTimeBarCodeFormat
    {
        get => $"{ProductDt:HHmmss}";
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual DateTime ExpirationDt
    {
        get => PluScale.IsNew ? DateTime.MinValue : ProductDt.AddDays(PluScale.Plu.ShelfLifeDays);
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string ExpirationDtFormat
    {
        get => $"{ExpirationDt:dd.MM.yyyy}";
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string Nesting
    {
        get
        {
            //PluNestingFkModel pluNestingFk = UserSess
            //PluScale.Plu.Net
            //return $"{NettoWeight:#0.000} {LocaleCore.Scales.WeightUnitKg}".Replace('.', ',');
            return $"{LocaleCore.Table.Nesting}: {LocaleCore.Table.NestingMeasurement}";
        }
        // This code need for print labels.
        set => _ = value;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public PluLabelContextModel(PluScaleModel pluScale)
    {
        PluScale = pluScale;
        ProductDt = DateTime.MinValue;
        ExpirationDt = DateTime.MinValue;
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected PluLabelContextModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        PluScale = (PluScaleModel)info.GetValue(nameof(PluScale), typeof(PluScaleModel));
        ProductDt = info.GetDateTime(nameof(ProductDt));
        ExpirationDt = info.GetDateTime(nameof(ExpirationDt));
    }

    #endregion

    #region Public and private methods - override

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(PluScale), PluScale);
        info.AddValue(nameof(ProductDt), ProductDt);
        info.AddValue(nameof(ExpirationDt), ExpirationDt);
    }

    #endregion
}