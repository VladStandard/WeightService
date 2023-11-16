using System.Xml.Serialization;
using WsStorageCore.Entities.SchemaRef.ProductionSites;
using WsStorageCore.Entities.SchemaScale.PlusLabels;
using WsStorageCore.Entities.SchemaScale.PlusScales;
using WsStorageCore.Entities.SchemaScale.PlusWeightings;

namespace WsLabelCore.ContextModels;

[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlPluLabelContextModel : IPluLabelContext
{
    #region Public and private properties - References

    [XmlIgnore] public WsSqlPluLabelEntity PluLabel { get; private set; }
    [XmlIgnore] private WsSqlViewPluNestingModel ViewPluNesting { get; set; }
    [XmlIgnore] private WsSqlPluScaleEntity PluScale { get; set; }
    [XmlIgnore] private WsSqlPluWeighingEntity PluWeighing { get; set; }
    [XmlIgnore] private WsSqlProductionSiteEntity ProductionSite { get; set; }

    #endregion
    
    #region Barcodes
   
    [XmlElement] public virtual string BarCodeTop => $"233{ScaleNumber}{PluNesting}{ScaleCounter}{ProductDateBarCodeFormat}{ProductTimeBarCodeFormat}{PluNumber}{PluWeighingKg2}{PluWeighingGr3}{PluWeighingKneading}";
    
    [XmlElement] public virtual string BarCodeRight => $"234{ScaleNumber}{ScaleCounter}{ProductDateBarCodeFormat}";
    
    [XmlElement] public virtual string BarCodeBottom => $"(01){Gtin}(37){ViewPluNesting.BundleCount.ToString().PadLeft(8, '0')}(11){ProductDateBarCodeFormat}(10){LotNumberFormat}";
    
    #endregion
    
    #region Accepted

    [XmlElement] public string Address => ProductionSite.Address;
    
    [XmlElement] public string NestingCaption => $"{WsLocaleCore.LabelPrint.LabelContextNesting}: ";
    
    [XmlElement] public string NestingValue  => $"{ViewPluNesting.BundleCount} {WsLocaleCore.Table.NestingMeasurement}";
    
    [XmlElement] public string ExpirationDtCaption => $"{WsLocaleCore.LabelPrint.LabelContextExpirationDt}: ";
    
    [XmlElement] public string ExpirationDt => $"{PluLabel.ExpirationDt:dd.MM.yyyy}";
    
    [XmlElement] public string ProductDtCaption =>  $"{WsLocaleCore.LabelPrint.LabelContextProductDt}: ";
    
    [XmlElement] public string ProductDt => $"{PluLabel.ProductDt:dd.MM.yyyy}";
    
    [XmlElement] public string PluDescription => PluScale.Plu.Description;
    
    [XmlElement] public string PluFullName => PluScale.Plu.FullName;
    
    [XmlElement] public string PluScaleNumber => $"{WsLocaleCore.LabelPrint.LabelContextPlu}: {PluNumber} / {ScaleNumber}";
    
    [XmlElement] public string PluWeighingKneadingWithCaption => $"{WsLocaleCore.LabelPrint.LabelContextKneading}: {PluWeighingKneading}";
    
    [XmlElement] public string ScaleDescription => $"{WsLocaleCore.LabelPrint.LabelContextWorkShop}: {PluScale.Line.Description}";
    [XmlIgnore] private string LotNumberFormat => $"{PluLabel.ProductDt:yyMM}"; 
    [XmlIgnore] private string ProductDateBarCodeFormat => $"{PluLabel.ProductDt:yyMMdd}";
    [XmlIgnore] private string ProductTimeBarCodeFormat => $"{PluLabel.ProductDt:HHmmss}";
    [XmlIgnore] private string PluNumber => $"{PluScale.Plu.Number:000}";
    [XmlIgnore] private string ScaleNumber => $"{PluScale.Line.Number:00000}";
    [XmlIgnore] private string ScaleCounter => $"{PluScale.Line.LabelCounter:000000}";
    [XmlIgnore] private string PluNesting => $"{ViewPluNesting.BundleCount:00}";
    [XmlIgnore] private string PluWeighingKg2 => $"{PluWeighing.NettoWeight:00.000}".Replace(',', '.').Split('.')[0];
    [XmlIgnore] private string PluWeighingGr3 => $"{PluWeighing.NettoWeight:#.000}".Replace(',', '.').Split('.')[1];
    [XmlIgnore] private string PluWeighingKneading => $"{PluWeighing.Kneading:000}";
    [XmlIgnore] private string Gtin => PluScale.Plu.Gtin;
    
    #endregion
    
    public WsSqlPluLabelContextModel(WsSqlPluLabelEntity pluLabel, WsSqlViewPluNestingModel viewPluNesting,
        WsSqlPluScaleEntity pluScale, WsSqlProductionSiteEntity productionSite, WsSqlPluWeighingEntity pluWeighing)
    {
        PluLabel = pluLabel;
        ViewPluNesting = viewPluNesting;
        PluScale = pluScale;
        ProductionSite = productionSite;
        PluWeighing = pluWeighing;
    }
    
}