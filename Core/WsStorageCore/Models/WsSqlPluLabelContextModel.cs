// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable VirtualMemberCallInConstructor

namespace WsStorageCore.Models;

/// <summary>
/// PLU label context.
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlPluLabelContextModel : SerializeBase
{
    #region Public and private properties - References

    [XmlIgnore] private WsSqlPluLabelModel PluLabel { get; set; }
    [XmlIgnore] private WsSqlViewPluNestingModel ViewPluNesting { get; set; }
    [XmlIgnore] private WsSqlPluScaleModel PluScale { get; set; }
    [XmlIgnore] private WsSqlPluWeighingModel PluWeighing { get; set; }
    [XmlIgnore] private WsSqlProductionFacilityModel ProductionFacility { get; set; }

    #endregion

    #region Public and private properties - XSLT trasform for print labels

    [XmlElement] public virtual string ProductDt { get => $"{PluLabel.ProductDt:dd.MM.yyyy}"; set => _ = value; }
    [XmlElement] public virtual string ProductDtWithCaption { get => $"{LocaleCore.Scales.LabelContextProductDt}: {ProductDt}"; set => _ = value; }
    [XmlElement] public virtual string LotNumberFormat { get => $"{PluLabel.ProductDt:yyMM}"; set => _ = value; }
    [XmlElement] public virtual string ProductDateBarCodeFormat { get => $"{PluLabel.ProductDt:yyMMdd}"; set => _ = value; }
    [XmlElement] public virtual string ProductTimeBarCodeFormat { get => $"{PluLabel.ProductDt:HHmmss}"; set => _ = value; }
    //[XmlElement] public virtual string CurrentDateBarCode { get => $"{DateTime.Now:yyMMdd}"; set => _ = value; }
    //[XmlElement] public virtual string CurrentTimeBarCode { get => $"{DateTime.Now:HHmmss}"; set => _ = value; }
    [XmlElement] public virtual string Nesting { get => $"{LocaleCore.Scales.LabelContextNesting}: {ViewPluNesting.BundleCount}{LocaleCore.Table.NestingMeasurement}"; set => _ = value; }
    [XmlElement] public virtual string Address { get => ProductionFacility.Address; set => _ = value; }
    [XmlElement] public virtual string PluDescription { get => PluScale.Plu.Description; set => _ = value; }
    [XmlElement] public virtual string PluFullName { get => PluScale.Plu.FullName; set => _ = value; }
    [XmlElement] public virtual string PluName { get => PluScale.Plu.Name; set => _ = value; }
    [XmlElement] public virtual string PluNumber { get => $"{PluScale.Plu.Number:000}"; set => _ = value; }
    [XmlElement] public virtual string PluScaleNumber { get => $"{LocaleCore.Scales.LabelContextPlu}: {PluNumber} / {ScaleNumber}"; set => _ = value; }
    [XmlElement] public virtual string PluNestingWeightTare { get => $"{ViewPluNesting.TareWeight:000}"; set => _ = value; }
    [XmlElement] public virtual string ExpirationDt { get => $"{PluLabel.ExpirationDt:dd.MM.yyyy}"; set => _ = value; }
    [XmlElement] public virtual string ExpirationDtWithCaption { get => $"{LocaleCore.Scales.LabelContextExpirationDt}: {ExpirationDt}"; set => _ = value; }
    [XmlElement] public virtual string ScaleNumber { get => $"{PluScale.Line.Number:00000}"; set => _ = value; }
    [XmlElement] public virtual string ScaleDescription { get => $"{LocaleCore.Scales.LabelContextWorkShop}: {PluScale.Line.Description}"; set => _ = value; }
    [XmlElement] public virtual string ScaleCounter8 { get => $"{PluScale.Line.LabelCounter:00000000}"; set => _ = value; }
    [XmlElement] public virtual string ScaleCounter6 { get => $"{PluScale.Line.LabelCounter:000000}"; set => _ = value; }
    [XmlElement] public virtual string PluNesting2 { get => $"{ViewPluNesting.BundleCount:00}"; set => _ = value; }
    [XmlElement] public virtual string PluWeighingKg2 { get => $"{PluWeighing.NettoWeight:00.000}".Replace(',', '.').Split('.')[0]; set => _ = value; }
    [XmlElement] public virtual string PluWeighingKg3 { get => $"{PluWeighing.NettoWeight:000.000}".Replace(',', '.').Split('.')[0]; set => _ = value; }
    [XmlElement] public virtual string PluWeighing1Dot3Eng { get => $"{PluWeighing.NettoWeight:0.000}".Replace(',', '.'); set => _ = value; }
    [XmlElement] public virtual string PluWeighing2Dot3Eng { get => $"{PluWeighing.NettoWeight:#0.000}".Replace(',', '.'); set => _ = value; }
    [XmlElement] public virtual string PluWeighing1Dot3Rus { get => $"{PluWeighing.NettoWeight:0.000}".Replace('.', ','); set => _ = value; }
    [XmlElement] public virtual string PluWeighing2Dot3Rus { get => $"{PluWeighing.NettoWeight:#0.000}".Replace('.', ','); set => _ = value; }
    [XmlElement] public virtual string PluWeighing2Dot3RusKg { get => $"{LocaleCore.Scales.LabelContextWeight}: {PluWeighing.NettoWeight:#0.000} {LocaleCore.Scales.WeightUnitKg}".Replace('.', ','); set => _ = value; }
    [XmlElement] public virtual string PluWeighingGr2 { get => $"{PluWeighing.NettoWeight:#.00}".Replace(',', '.').Split('.')[1]; set => _ = value; }
    [XmlElement] public virtual string PluWeighingGr3 { get => $"{PluWeighing.NettoWeight:#.000}".Replace(',', '.').Split('.')[1]; set => _ = value; }
    [XmlElement] public virtual string PluWeighingKneading { get => $"{PluWeighing.Kneading:000}"; set => _ = value; }
    [XmlElement] public virtual string PluWeighingKneadingWithCaption { get => $"{LocaleCore.Scales.LabelContextKneading}: {PluWeighingKneading}"; set => _ = value; }
    [XmlElement] public virtual string BarCodeEan13 { get => PluScale.Plu.Ean13; set => _ = value; }
    [XmlElement] public virtual string BarCodeGtin14 { get => PluScale.Plu.Gtin.Length switch { 13 => WsSqlBarCodeController.Instance.GetGtinWithCheckDigit(PluScale.Plu.Gtin[..13]), 14 => PluScale.Plu.Gtin, _ => "ERROR" }; set => _ = value; }
    [XmlElement] public virtual string BarCodeItf14 { get => PluScale.Plu.Itf14; set => _ = value; }
    /// <summary>
    /// Верхний ШК для шаблонов.
    /// TSC 60X150 ВЕС ШАПКА СПРАВА | TSC 60X150 ШТ ШАПКА СПРАВА | TSC 60X150 ВЕС ШАПКА СЛЕВА | TSC 60X150 ШТ ШАПКА СЛЕВА
    /// </summary>
    [XmlElement]
    public virtual string BarCodeTop
    {
        /*
        Константа [3 симв]: 298
        Номер АРМ [5 симв]: ScaleNumber
        Счётчик [8 симв]:   ScaleCounter8
        Дата [6 симв]:      ProductDateBarCodeFormat
        Время [6 симв]:     ProductTimeBarCodeFormat
        ПЛУ [3 симв]:       PluNumber
        Вес [5 симв]:       PluWeighingKg2 PluWeighingGr3
        Замес [3 симв]:     PluWeighingKneading
        */
        get => $"298{ScaleNumber}{ScaleCounter8}{ProductDateBarCodeFormat}{ProductTimeBarCodeFormat}{PluNumber}{PluWeighingKg2}{PluWeighingGr3}{PluWeighingKneading}";
        set => _ = value;
    }
    /// <summary>
    /// Верхний ШК для шаблонов с кодом 230.
    /// TSC 60X150 ВЕС ШАПКА СПРАВА КОД 230 | TSC 60X150 ШТ ШАПКА СПРАВА КОД 230
    /// </summary>
    [XmlElement]
    public virtual string BarCodeTopV230
    {
        /*
        Константа [3 симв]:     233
        Номер АРМ [5 симв]:     ScaleNumber
        Вложенность [2 симв]:   PluNesting2
        Счётчик [6 симв]:       ScaleCounter6
        Дата [6 симв]:          ProductDateBarCodeFormat
        Время [6 симв]:         ProductTimeBarCodeFormat
        ПЛУ [3 симв]:           PluNumber
        Вес [5 симв]:           PluWeighingKg2 PluWeighingGr3
        Замес [3 симв]:         PluWeighingKneading
        */
        get => $"233{ScaleNumber}{PluNesting2}{ScaleCounter6}{ProductDateBarCodeFormat}{ProductTimeBarCodeFormat}{PluNumber}{PluWeighingKg2}{PluWeighingGr3}{PluWeighingKneading}";
        set => _ = value;
    }
    /// <summary>
    /// Правый ШК для шаблонов.
    /// TSC 60X150 ВЕС ШАПКА СПРАВА | TSC 60X150 ШТ ШАПКА СПРАВА | TSC 60X150 ВЕС ШАПКА СЛЕВА | TSC 60X150 ШТ ШАПКА СЛЕВА
    /// </summary>
    [XmlElement]
    public virtual string BarCodeRight
    {
        /*
        Константа [3 симв]: 299
        Номер АРМ [5 симв]: ScaleNumber
        Счётчик [8 симв]:   ScaleCounter8
        */
        get => $"299{ScaleNumber}{ScaleCounter8}";
        set => _ = value;
    }
    /// <summary>
    /// Правый ШК для шаблонов с кодом 230.
    /// TSC 60X150 ВЕС ШАПКА СПРАВА КОД 230 | TSC 60X150 ШТ ШАПКА СПРАВА КОД 230
    /// </summary>
    [XmlElement]
    public virtual string BarCodeRightV230
    {
        /*
        Константа [3 симв]:     234
        Номер АРМ [5 симв]:     ScaleNumber
        Вложенность [2 симв]:   PluNesting2
        Счётчик [8 симв]:       ScaleCounter6
        Дата [6 симв]:          ProductDateBarCodeFormat
        */
        get => $"234{ScaleNumber}{PluNesting2}{ScaleCounter6}{ProductDateBarCodeFormat}";
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

        set => _ = value;
    }

    public WsSqlPluLabelContextModel(WsSqlPluLabelModel pluLabel, WsSqlViewPluNestingModel viewPluNesting,
        WsSqlPluScaleModel pluScale, WsSqlProductionFacilityModel productionFacility, WsSqlPluWeighingModel pluWeighing)
    {
        PluLabel = pluLabel;
        ViewPluNesting = viewPluNesting;
        PluScale = pluScale;
        ProductionFacility = productionFacility;
        PluWeighing = pluWeighing;
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlPluLabelContextModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        PluLabel = (WsSqlPluLabelModel)info.GetValue(nameof(PluLabel), typeof(WsSqlPluLabelModel));
        ViewPluNesting = (WsSqlViewPluNestingModel)info.GetValue(nameof(ViewPluNesting), typeof(WsSqlViewPluNestingModel));
        PluScale = (WsSqlPluScaleModel)info.GetValue(nameof(PluScale), typeof(WsSqlPluScaleModel));
        PluWeighing = (WsSqlPluWeighingModel)info.GetValue(nameof(PluWeighing), typeof(WsSqlPluWeighingModel));
        ProductionFacility = (WsSqlProductionFacilityModel)info.GetValue(nameof(ProductionFacility), typeof(WsSqlProductionFacilityModel));

        //Address = info.GetString(nameof(Address));
        //BarCodeGtin14 = info.GetString(nameof(BarCodeGtin14));
        //ExpirationDt = info.GetString(nameof(ExpirationDt));
        //LotNumberFormat = info.GetString(nameof(LotNumberFormat));
        //Nesting = info.GetString(nameof(Nesting));
        //PluDescription = info.GetString(nameof(PluDescription));
        //PluFullName = info.GetString(nameof(PluFullName));
        //PluName = info.GetString(nameof(PluName));
        //PluNestingWeightTare = info.GetString(nameof(PluNestingWeightTare));
        //PluNumber = info.GetString(nameof(PluNumber));
        //ProductDateBarCodeFormat = info.GetString(nameof(ProductDateBarCodeFormat));
        //ProductDt = info.GetString(nameof(ProductDt));
        //ProductTimeBarCodeFormat = info.GetString(nameof(ProductTimeBarCodeFormat));
    }

    public WsSqlPluLabelContextModel() : this(new(), new(), new(), new(), new()) { }

    #endregion

    #region Public and private methods - override

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);

        info.AddValue(nameof(PluLabel), PluLabel);
        info.AddValue(nameof(ViewPluNesting), ViewPluNesting);
        info.AddValue(nameof(PluScale), PluScale);
        info.AddValue(nameof(ProductionFacility), ProductionFacility);
        info.AddValue(nameof(PluWeighing), PluWeighing);

        //info.AddValue(nameof(Address), Address);
        //info.AddValue(nameof(BarCodeGtin14), BarCodeGtin14);
        //info.AddValue(nameof(ExpirationDt), ExpirationDt);
        //info.AddValue(nameof(LotNumberFormat), LotNumberFormat);
        //info.AddValue(nameof(Nesting), Nesting);
        //info.AddValue(nameof(PluDescription), PluDescription);
        //info.AddValue(nameof(PluFullName), PluFullName);
        //info.AddValue(nameof(PluName), PluName);
        //info.AddValue(nameof(PluNestingWeightTare), PluNestingWeightTare);
        //info.AddValue(nameof(PluNumber), PluNumber);
        //info.AddValue(nameof(ProductDateBarCodeFormat), ProductDateBarCodeFormat);
        //info.AddValue(nameof(ProductDt), ProductDt);
        //info.AddValue(nameof(ProductTimeBarCodeFormat), ProductTimeBarCodeFormat);
    }

    #endregion
}