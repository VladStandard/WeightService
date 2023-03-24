// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable VirtualMemberCallInConstructor

using DataCore.Sql.Helpers;
using DataCore.Sql.TableScaleFkModels.PlusNestingFks;
using DataCore.Sql.TableScaleModels.PlusLabels;
using DataCore.Sql.TableScaleModels.PlusScales;
using DataCore.Sql.TableScaleModels.PlusWeighings;
using DataCore.Sql.TableScaleModels.ProductionFacilities;

namespace DataCore.Sql.Core.Models;

/// <summary>
/// PLU label context.
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(PluLabelContextModel)}")]
public class PluLabelContextModel : SerializeBase
{
    #region Public and private properties - References

    [XmlIgnore] private DataContextModel? DataContext { get; set; }
    [XmlIgnore] private PluLabelModel PluLabel { get; set; }
    [XmlIgnore] private PluNestingFkModel PluNestingFk { get; set; }
    [XmlIgnore] private PluScaleModel PluScale { get; set; }
    [XmlIgnore] private PluWeighingModel PluWeighing { get; set; }
    [XmlIgnore] private ProductionFacilityModel ProductionFacility { get; set; }

    #endregion

    #region Public and private properties - XSLT trasform for print labels

    [XmlElement] public virtual string ProductDt { get => $"{PluLabel.ProductDt:dd.MM.yyyy}"; set => _ = value; }
    [XmlElement] public virtual string LotNumberFormat { get => $"{PluLabel.ProductDt:yyMM}"; set => _ = value; }
    [XmlElement] public virtual string ProductDateBarCodeFormat { get => $"{PluLabel.ProductDt:yyMMdd}"; set => _ = value; }
    [XmlElement] public virtual string ProductTimeBarCodeFormat { get => $"{PluLabel.ProductDt:HHmmss}"; set => _ = value; }
    [XmlElement] public virtual string Nesting { get => $"{LocaleCore.Table.Nesting}: {DataContext?.GetPluNestingFkBundleCount(PluNestingFk) ?? 0}{LocaleCore.Table.NestingMeasurement}"; set => _ = value; }
    [XmlElement] public virtual string Address { get => ProductionFacility.Address; set => _ = value; }
    [XmlElement] public virtual string PluDescription { get => PluScale.Plu.Description; set => _ = value; }
    [XmlElement] public virtual string PluFullName { get => PluScale.Plu.FullName; set => _ = value; }
    [XmlElement] public virtual string PluName { get => PluScale.Plu.Name; set => _ = value; }
    [XmlElement] public virtual string PluNumber { get => $"{PluScale.Plu.Number:000}"; set => _ = value; }
    [XmlElement] public virtual string PluNestingWeightTare { get => $"{PluNestingFk.WeightTare:000}"; set => _ = value; }
    [XmlElement] public virtual string ExpirationDt { get => $"{PluLabel.ExpirationDt:dd.MM.yyyy}"; set => _ = value; }
    [XmlElement] public virtual string ScaleNumber { get => $"{PluScale.Scale.Number:00000}"; set => _ = value; }
    [XmlElement] public virtual string ScaleDescription { get => PluScale.Scale.Description; set => _ = value; }
    [XmlElement] public virtual string ScaleCounter { get => $"{PluScale.Scale.Counter:00000000}"; set => _ = value; }
    [XmlElement] public virtual string PluWeighingKg2 { get => $"{PluWeighing.NettoWeight:00.000}".Replace(',', '.').Split('.')[0]; set => _ = value; }
    [XmlElement] public virtual string PluWeighingKg3 { get => $"{PluWeighing.NettoWeight:000.000}".Replace(',', '.').Split('.')[0]; set => _ = value; }
    [XmlElement] public virtual string PluWeighing1Dot3Eng { get => $"{PluWeighing.NettoWeight:0.000}".Replace(',', '.'); set => _ = value; }
    [XmlElement] public virtual string PluWeighing2Dot3Eng { get => $"{PluWeighing.NettoWeight:#0.000}".Replace(',', '.'); set => _ = value; }
    [XmlElement] public virtual string PluWeighing1Dot3Rus { get => $"{PluWeighing.NettoWeight:0.000}".Replace('.', ','); set => _ = value; }
    [XmlElement] public virtual string PluWeighing2Dot3Rus { get => $"{PluWeighing.NettoWeight:#0.000}".Replace('.', ','); set => _ = value; }
    [XmlElement] public virtual string PluWeighing2Dot3RusKg { get => $"{PluWeighing.NettoWeight:#0.000} {LocaleCore.Scales.WeightUnitKg}".Replace('.', ','); set => _ = value; }
    [XmlElement] public virtual string PluWeighingGr2 { get => $"{PluWeighing.NettoWeight:#.00}".Replace(',', '.').Split('.')[1]; set => _ = value; }
    [XmlElement] public virtual string PluWeighingGr3 { get => $"{PluWeighing.NettoWeight:#.000}".Replace(',', '.').Split('.')[1]; set => _ = value; }
    [XmlElement] public virtual string PluWeighingKneading { get => $"{PluWeighing.Kneading:000}"; set => _ = value; }
    [XmlElement] public virtual string BarCodeEan13 { get => PluScale.Plu.Ean13; set => _ = value; }
    [XmlElement] public virtual string BarCodeGtin14 { get => PluScale.Plu.Gtin.Length switch { 13 => BarCodeHelper.Instance.GetGtinWithCheckDigit(PluScale.Plu.Gtin[..13]), 14 => PluScale.Plu.Gtin, _ => "ERROR" }; set => _ = value; }
    [XmlElement] public virtual string BarCodeItf14 { get => PluScale.Plu.Itf14; set => _ = value; }
    [XmlElement] public virtual string BarCodeTop
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
        set => _ = value;
    }
    [XmlElement] public virtual string BarCodeRight
    {
        /*
        Константа [3 симв]: 299
        Номер АРМ [5 симв]: ScaleNumber
        Счётчик [8 симв]:   ScaleCounter
        */
        get => $"299{ScaleNumber}{ScaleCounter}";
        set => _ = value;
    }
    [XmlElement] public virtual string BarCodeBottom
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
    [XmlElement] public virtual string BarCodeBottomString
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
        PluWeighing = (PluWeighingModel)info.GetValue(nameof(PluWeighing), typeof(PluWeighingModel));
        ProductionFacility = (ProductionFacilityModel)info.GetValue(nameof(ProductionFacility), typeof(ProductionFacilityModel));

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

    #region Public and private methods



    #endregion
}