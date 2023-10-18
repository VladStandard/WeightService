// ReSharper disable VirtualMemberCallInConstructor

using WsStorageCore.Tables.TableRefModels.ProductionSites;
namespace WsStorageCore.Models;

[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlPluLabelContextModel : SerializeBase
{
    #region Public and private properties - References

    [XmlIgnore] public WsSqlPluLabelModel PluLabel { get; private set; }
    [XmlIgnore] private WsSqlViewPluNestingModel ViewPluNesting { get; set; }
    [XmlIgnore] private WsSqlPluScaleModel PluScale { get; set; }
    [XmlIgnore] private WsSqlPluWeighingModel PluWeighing { get; set; }
    [XmlIgnore] private WsSqlProductionSiteModel ProductionSite { get; set; }

    #endregion

    #region Public and private properties - XSLT trasform for print labels

    [XmlElement] public virtual string ProductDt { get => $"{PluLabel.ProductDt:dd.MM.yyyy}"; set => _ = value; }
    [XmlElement] public virtual string ProductDtCaption { get =>  $"{WsLocaleCore.LabelPrint.LabelContextProductDt}: "; set => _ = value; }
    [XmlElement] public virtual string LotNumberFormat { get => $"{PluLabel.ProductDt:yyMM}"; set => _ = value; }
    [XmlElement] public virtual string ProductDateBarCodeFormat { get => $"{PluLabel.ProductDt:yyMMdd}"; set => _ = value; }
    [XmlElement] public virtual string ProductTimeBarCodeFormat { get => $"{PluLabel.ProductDt:HHmmss}"; set => _ = value; }
    [XmlElement] public virtual string Nesting { get => $"{WsLocaleCore.LabelPrint.LabelContextNesting}: {ViewPluNesting.BundleCount}{WsLocaleCore.Table.NestingMeasurement}"; set => _ = value; }
    [XmlElement] public virtual string NestingCaption { get => $"{WsLocaleCore.LabelPrint.LabelContextNesting}: "; set => _ = value; }
    [XmlElement] public virtual string NestingValue { get => $"{ViewPluNesting.BundleCount} {WsLocaleCore.Table.NestingMeasurement}"; set => _ = value; }
    [XmlElement] public virtual string Address { get => ProductionSite.Address; set => _ = value; }
    [XmlElement] public virtual string PluDescription { get => PluScale.Plu.Description; set => _ = value; }
    [XmlElement] public virtual string PluName { get => PluScale.Plu.Name; set => _ = value; }
    [XmlElement] public virtual string PluFullName { get => PluScale.Plu.FullName; set => _ = value; }
    [XmlElement] public virtual string PluNumber { get => $"{PluScale.Plu.Number:000}"; set => _ = value; }
    [XmlElement] public virtual string PluScaleNumber { get => $"{WsLocaleCore.LabelPrint.LabelContextPlu}: {PluNumber} / {ScaleNumber}"; set => _ = value; }
    [XmlElement] public virtual string ExpirationDt { get => $"{PluLabel.ExpirationDt:dd.MM.yyyy}"; set => _ = value; }
    [XmlElement] public virtual string ExpirationDtCaption { get => $"{WsLocaleCore.LabelPrint.LabelContextExpirationDt}: "; set => _ = value; }
    [XmlElement] public virtual string ScaleNumber { get => $"{PluScale.Line.Number:00000}"; set => _ = value; }
    [XmlElement] public virtual string ScaleDescription { get => $"{WsLocaleCore.LabelPrint.LabelContextWorkShop}: {PluScale.Line.Description}"; set => _ = value; }
    [XmlElement] public virtual string ScaleCounter8 { get => $"{PluScale.Line.LabelCounter:00000000}"; set => _ = value; }
    [XmlElement] public virtual string ScaleCounter6 { get => $"{PluScale.Line.LabelCounter:000000}"; set => _ = value; }
    [XmlElement] public virtual string PluNesting2 { get => $"{ViewPluNesting.BundleCount:00}"; set => _ = value; }
    [XmlElement] public virtual string PluWeighingСaption { get => $"{WsLocaleCore.LabelPrint.LabelContextWeight}: "; set => _ = value; }
    [XmlElement] public virtual string PluWeighingValueDot3Rus { get => $"{PluWeighing.NettoWeight:#0.000} {WsLocaleCore.LabelPrint.WeightUnitKg}".Replace('.', ','); set => _ = value; }
    [XmlElement] public virtual string PluWeighingKg2 { get => $"{PluWeighing.NettoWeight:00.000}".Replace(',', '.').Split('.')[0]; set => _ = value; }
    [XmlElement] public virtual string PluWeighingKg3 { get => $"{PluWeighing.NettoWeight:000.000}".Replace(',', '.').Split('.')[0]; set => _ = value; }
    [XmlElement] public virtual string PluWeighing1Dot3Eng { get => $"{PluWeighing.NettoWeight:0.000}".Replace(',', '.'); set => _ = value; }
    [XmlElement] public virtual string PluWeighing2Dot3Eng { get => $"{PluWeighing.NettoWeight:#0.000}".Replace(',', '.'); set => _ = value; }
    [XmlElement] public virtual string PluWeighing1Dot3Rus { get => $"{PluWeighing.NettoWeight:0.000}".Replace('.', ','); set => _ = value; }
    [XmlElement] public virtual string PluWeighing2Dot3Rus { get => $"{PluWeighing.NettoWeight:#0.000}".Replace('.', ','); set => _ = value; }
    [XmlElement] public virtual string PluWeighingGr2 { get => $"{PluWeighing.NettoWeight:#.00}".Replace(',', '.').Split('.')[1]; set => _ = value; }
    [XmlElement] public virtual string PluWeighingGr3 { get => $"{PluWeighing.NettoWeight:#.000}".Replace(',', '.').Split('.')[1]; set => _ = value; }
    [XmlElement] public virtual string PluWeighingKneading { get => $"{PluWeighing.Kneading:000}"; set => _ = value; }
    [XmlElement] public virtual string PluWeighingKneadingWithCaption { get => $"{WsLocaleCore.LabelPrint.LabelContextKneading}: {PluWeighingKneading}"; set => _ = value; }
    [XmlElement] public virtual string BarCodeItf14 { get => PluScale.Plu.Itf14; set => _ = value; }
    [XmlElement] public virtual string BarCodeGtin14 { get => PluScale.Plu.Gtin.Length switch { 13 => WsSqlBarCodeController.Instance.GetGtinWithCheckDigit(PluScale.Plu.Gtin[..13]), 14 => PluScale.Plu.Gtin, _ => "ERROR" }; set => _ = value; }

    
    /// <summary>
    [XmlElement]
    public virtual string BarCodeTop
    {
        get => $"233{ScaleNumber}{PluNesting2}{ScaleCounter6}{ProductDateBarCodeFormat}{ProductTimeBarCodeFormat}{PluNumber}{PluWeighingKg2}{PluWeighingGr3}{PluWeighingKneading}";
        set => _ = value;
    }
    
    /// <summary>
    /// Правый ШК для шаблонов с кодом 234.
    /// </summary>
    [XmlElement]
    public virtual string BarCodeRight
    {
        /*
        Константа [3 симв]:     299/234
        Номер АРМ [5 симв]:     ScaleNumber
        Счётчик [8 симв]:       ScaleCounter6
        Дата [6 симв]:          ProductDateBarCodeFormat у ШТ
        */
        get => PluWeighing.IsExists ? $"299{ScaleNumber}{ScaleCounter8}" : $"234{ScaleNumber}{ScaleCounter6}{ProductDateBarCodeFormat}";
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
        get => PluWeighing.IsExists ? $"(01){BarCodeGtin14}(3103){PluWeighingKg3}{PluWeighingGr3}(11){ProductDateBarCodeFormat}(10){LotNumberFormat}" 
            : $"(01){BarCodeGtin14}(37){ViewPluNesting.BundleCount.ToString().PadLeft(8, '0')}(11){ProductDateBarCodeFormat}(10){LotNumberFormat}";
        set => _ = value;
    }
    
    public WsSqlPluLabelContextModel(WsSqlPluLabelModel pluLabel, WsSqlViewPluNestingModel viewPluNesting,
        WsSqlPluScaleModel pluScale, WsSqlProductionSiteModel productionSite, WsSqlPluWeighingModel pluWeighing)
    {
        PluLabel = pluLabel;
        ViewPluNesting = viewPluNesting;
        PluScale = pluScale;
        ProductionSite = productionSite;
        PluWeighing = pluWeighing;
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    protected WsSqlPluLabelContextModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        PluLabel = (WsSqlPluLabelModel)info.GetValue(nameof(PluLabel), typeof(WsSqlPluLabelModel));
        ViewPluNesting = (WsSqlViewPluNestingModel)info.GetValue(nameof(ViewPluNesting), typeof(WsSqlViewPluNestingModel));
        PluScale = (WsSqlPluScaleModel)info.GetValue(nameof(PluScale), typeof(WsSqlPluScaleModel));
        PluWeighing = (WsSqlPluWeighingModel)info.GetValue(nameof(PluWeighing), typeof(WsSqlPluWeighingModel));
        ProductionSite = (WsSqlProductionSiteModel)info.GetValue(nameof(ProductionSite), typeof(WsSqlProductionSiteModel));

        //Address = info.GetString(nameof(Address));
        //BarCodeGtin14 = info.GetString(nameof(BarCodeGtin14));
        //ExpirationDt = info.GetString(nameof(ExpirationDt));
        //LotNumberFormat = info.GetString(nameof(LotNumberFormat));
        //Nesting = info.GetString(nameof(Nesting));
        //PluDescription = info.GetString(nameof(PluDescription));
        //PluFullName = info.GetString(nameof(PluFullName));
        //PluName = info.GetString(nameof(PluName));
        //PluNumber = info.GetString(nameof(PluNumber));
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
        info.AddValue(nameof(ProductionSite), ProductionSite);
        info.AddValue(nameof(PluWeighing), PluWeighing);
    }

    #endregion
}