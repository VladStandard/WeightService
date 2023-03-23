// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable VirtualMemberCallInConstructor

using DataCore.Sql.TableScaleFkModels.PlusNestingFks;
using DataCore.Sql.TableScaleModels.PlusLabels;
using DataCore.Sql.TableScaleModels.PlusScales;
using DataCore.Sql.TableScaleModels.PlusWeighings;
using DataCore.Sql.TableScaleModels.ProductionFacilities;
using DataCore.Sql.Xml;

namespace DataCore.Sql.Core.Models;

/// <summary>
/// PLU label context.
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(PluLabelContextModel)}")]
public class PluLabelContextModel : SerializeBase
{
    #region Public and private fields, properties, constructor

    [XmlIgnore] private DataContextModel? DataContext { get; set; }
    [XmlIgnore] private PluLabelModel PluLabel { get; set; }
    [XmlIgnore] private PluNestingFkModel PluNestingFk { get; set; }
    [XmlIgnore] private PluScaleModel PluScale { get; set; }
    [XmlIgnore] private PluWeighingModel PluWeighing { get; set; }
    [XmlIgnore] private ProductionFacilityModel ProductionFacility { get; set; }
    [XmlElement]
    public virtual string ProductDt
    {
        get => $"{PluLabel.ProductDt:dd.MM.yyyy}";
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string LotNumberFormat
    {
        get => $"{PluLabel.ProductDt:yyMM}";
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string ProductDateBarCodeFormat
    {
        get => $"{PluLabel.ProductDt:yyMMdd}";
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string ProductTimeBarCodeFormat
    {
        get => $"{PluLabel.ProductDt:HHmmss}";
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string Nesting
    {
        get => $"{LocaleCore.Table.Nesting}: {DataContext?.GetPluNestingFkBundleCount(PluNestingFk) ?? 0}{LocaleCore.Table.NestingMeasurement}";
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string Address
    {
        get => ProductionFacility.Address;
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string PluDescription
    {
        get => PluScale.Plu.Description;
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string PluFullName
    {
        get => PluScale.Plu.FullName;
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string PluName
    {
        get => PluScale.Plu.Name;
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string PluNumber
    {
        get => $"{PluScale.Plu.Number:000}";
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string PluNestingWeightTare
    {
        get => $"{PluNestingFk.WeightTare:000}";
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string ExpirationDt
    {
        get => $"{PluLabel.ExpirationDt:dd.MM.yyyy}";
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string ScaleNumber
    {
        get => $"{PluScale.Scale.Number:00000}";
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string ScaleDescription
    {
        get => PluScale.Scale.Description;
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string ScaleCounter
    {
        get => $"{PluScale.Scale.Counter:00000000}";
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string PluWeighingKg2
    {
        get => $"{PluWeighing.NettoWeight:00.000}".Replace(',', '.').Split('.')[0];
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string PluWeighingKg3
    {
        get => $"{PluWeighing.NettoWeight:000.000}".Replace(',', '.').Split('.')[0];
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string PluWeighing1Dot3Eng
    {
        get => $"{PluWeighing.NettoWeight:0.000}".Replace(',', '.');
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string PluWeighing2Dot3Eng
    {
        get => $"{PluWeighing.NettoWeight:#0.000}".Replace(',', '.');
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string PluWeighing1Dot3Rus
    {
        get => $"{PluWeighing.NettoWeight:0.000}".Replace('.', ',');
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string PluWeighing2Dot3Rus
    {
        get => $"{PluWeighing.NettoWeight:#0.000}".Replace('.', ',');
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string PluWeighing2Dot3RusKg
    {
        get => $"{PluWeighing.NettoWeight:#0.000} {LocaleCore.Scales.WeightUnitKg}".Replace('.', ',');
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string PluWeighingGr2
    {
        get => $"{PluWeighing.NettoWeight:#.00}".Replace(',', '.').Split('.')[1];
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string PluWeighingGr3
    {
        get => $"{PluWeighing.NettoWeight:#.000}".Replace(',', '.').Split('.')[1];
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string PluWeighingKneading
    {
        get => $"{PluWeighing.Kneading:000}";
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string BarCodeEan13
    {
        get => PluScale.Plu.Ean13;
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string BarCodeGtin14
    {
        get => PluScale.Plu.Gtin.Length switch
        {
            13 => BarcodeHelper.Instance.GetGtinWithCheckDigit(PluScale.Plu.Gtin[..13]),
            14 => PluScale.Plu.Gtin,
            _ => "ERROR"
        };
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string BarCodeItf14
    {
        get => PluScale.Plu.Itf14;
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string BarCodeTop
    {
        /*
        Константа [3 симв]: 298
        Номер АРМ [5 симв]: ScaleNumber
        Счётчик [8 симв]:   ScaleCounter
        Дата [6 симв]:      ProductDateBarCodeFormat
        Время [6 симв]:     ProductTimeBarCodeFormat
        ПЛУ [3 симв]:       PluNumber
        Вес [5 симв]:       PluWeighingKg2 PluWeighingGr3
        Замес [3 симв]:     PluWeighingKneading
        */
        get => $"298{ScaleNumber}{ScaleCounter}{ProductDateBarCodeFormat}{ProductTimeBarCodeFormat}{PluNumber}{PluWeighingKg2}{PluWeighingGr3}{PluWeighingKneading}";
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string BarCodeRight
    {
        /*
        Константа [3 симв]: 299
        Номер АРМ [5 симв]: ScaleNumber
        Счётчик [8 симв]:   ScaleCounter
        */
        get => $"299{ScaleNumber}{ScaleCounter}";
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string BarCodeBottom
    {
        /*
        Константа [2 симв]:     01
        GTIN [14 симв]:         BarCodeGtin14
        Константа [4 симв]:     3103
        Вес [6 симв]:           PluWeighingKg3 PluWeighingGr3
        Константа [2 симв]:     11
        Дата [6 симв]:          ProductDateBarCodeFormat
        Константа [2 симв]:     10
        Номер партии [4 симв]:  LotNumberFormat
        */
        get => $"01{BarCodeGtin14}3103{PluWeighingKg3}{PluWeighingGr3}11{ProductDateBarCodeFormat}10{LotNumberFormat}";
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string BarCodeBottomString
    {
        /*
        Константа [4 симв]:     (01)
        GTIN [14 симв]:         BarCodeGtin14
        Константа [6 симв]:     (3103)
        Вес [6 симв]:           PluWeighingKg3 PluWeighingGr3
        Константа [4 симв]:     (11)
        Дата [6 симв]:          ProductDateBarCodeFormat
        Константа [4 симв]:     (10)
        Номер партии [4 симв]:  LotNumberFormat
        */
        get => $"(01){BarCodeGtin14}(3103){PluWeighingKg3}{PluWeighingGr3}(11){ProductDateBarCodeFormat}(10){LotNumberFormat}";
        // This code need for print labels.
        set => _ = value;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public PluLabelContextModel() : this(null, new(), new(), new(), new(), new())
    {
        //
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public PluLabelContextModel(DataContextModel? dataContext, PluLabelModel pluLabel, PluNestingFkModel pluNestingFk, 
        PluScaleModel pluScale, ProductionFacilityModel productionFacility, PluWeighingModel pluWeighing)
    {
        DataContext = dataContext;

        PluLabel = pluLabel;
        PluNestingFk = pluNestingFk;
        PluScale = pluScale;
        ProductionFacility = productionFacility;
        PluWeighing = pluWeighing;
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected PluLabelContextModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        PluLabel = (PluLabelModel)info.GetValue(nameof(PluLabel), typeof(PluLabelModel));
        PluNestingFk = (PluNestingFkModel)info.GetValue(nameof(PluNestingFk), typeof(PluNestingFkModel));
        PluScale = (PluScaleModel)info.GetValue(nameof(PluScale), typeof(PluScaleModel));
        ProductionFacility = (ProductionFacilityModel)info.GetValue(nameof(ProductionFacility), typeof(ProductionFacilityModel));
        PluWeighing = (PluWeighingModel)info.GetValue(nameof(PluWeighing), typeof(PluWeighingModel));

        ProductDt = info.GetString(nameof(ProductDt));
        LotNumberFormat = info.GetString(nameof(LotNumberFormat));
        ProductDateBarCodeFormat = info.GetString(nameof(ProductDateBarCodeFormat));
        ProductTimeBarCodeFormat = info.GetString(nameof(ProductTimeBarCodeFormat));
        Nesting = info.GetString(nameof(Nesting));
        Address = info.GetString(nameof(Address));
        PluDescription = info.GetString(nameof(PluDescription));
        PluFullName = info.GetString(nameof(PluFullName));
        PluName = info.GetString(nameof(PluName));
        PluNumber = info.GetString(nameof(PluNumber));
        PluNestingWeightTare = info.GetString(nameof(PluNestingWeightTare));
        BarCodeGtin14 = info.GetString(nameof(BarCodeGtin14));
        ExpirationDt = info.GetString(nameof(ExpirationDt));
    }

    #endregion

    #region Public and private methods - override

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);

        info.AddValue(nameof(PluLabel), PluLabel);
        info.AddValue(nameof(PluNestingFk), PluNestingFk);
        info.AddValue(nameof(PluScale), PluScale);
        info.AddValue(nameof(ProductionFacility), ProductionFacility);
        info.AddValue(nameof(PluWeighing), PluWeighing);

        info.AddValue(nameof(ProductDt), ProductDt);
        info.AddValue(nameof(LotNumberFormat), LotNumberFormat);
        info.AddValue(nameof(ProductDateBarCodeFormat), ProductDateBarCodeFormat);
        info.AddValue(nameof(ProductTimeBarCodeFormat), ProductTimeBarCodeFormat);
        info.AddValue(nameof(Nesting), Nesting);
        info.AddValue(nameof(Address), Address);
        info.AddValue(nameof(PluDescription), PluDescription);
        info.AddValue(nameof(PluFullName), PluFullName);
        info.AddValue(nameof(PluName), PluName);
        info.AddValue(nameof(PluNumber), PluNumber);
        info.AddValue(nameof(PluNestingWeightTare), PluNestingWeightTare);
        info.AddValue(nameof(BarCodeGtin14), BarCodeGtin14);
        info.AddValue(nameof(ExpirationDt), ExpirationDt);
    }

    #endregion
}