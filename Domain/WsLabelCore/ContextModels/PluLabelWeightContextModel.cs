using System.Xml.Serialization;
using WsStorageCore.Entities.SchemaRef.ProductionSites;
using WsStorageCore.Entities.SchemaScale.PlusLabels;
using WsStorageCore.Entities.SchemaScale.PlusScales;
using WsStorageCore.Entities.SchemaScale.PlusWeightings;

namespace WsLabelCore.ContextModels;

public class PluLabelWeightContextModel : IPluLabelContext
{
     #region Public and private properties - References

    [XmlIgnore] public WsSqlPluLabelEntity PluLabel { get; private set; }
    [XmlIgnore] private WsSqlViewPluNestingModel ViewPluNesting { get; set; }
    [XmlIgnore] private WsSqlPluScaleEntity PluScale { get; set; }
    [XmlIgnore] private WsSqlPluWeighingEntity PluWeighing { get; set; }
    [XmlIgnore] private WsSqlProductionSiteEntity ProductionSite { get; set; }

    #endregion
    
    #region Barcodes
    
    [XmlElement] public string BarCodeTop => $"298{ScaleNumber}{ScaleCounter}{ProductDateBarCodeFormat}{ProductTimeBarCodeFormat}{PluNumber}{PluWeighingKg2}{PluWeighingGr}{PluWeighingKneading}";

    [XmlElement] public string BarCodeRight => $"299{ScaleNumber}{ScaleCounter}";

    [XmlElement] public string BarCodeBottom => $"(01){BarCodeGtin}(3103){PluWeighingKg3}{PluWeighingGr}(11){ProductDateBarCodeFormat}(10){LotNumberFormat}";
    
    #endregion

    #region Accepted
  
    [XmlElement] public string Address => ProductionSite.Address;
    
    [XmlElement] public string ExpirationDtCaption => $"{WsLocaleCore.LabelPrint.LabelContextExpirationDt}: ";
    
    [XmlElement] public string ExpirationDt => $"{PluLabel.ExpirationDt:dd.MM.yyyy}";
    
    [XmlElement] public string ProductDtCaption =>  $"{WsLocaleCore.LabelPrint.LabelContextProductDt}: ";
    
    [XmlElement] public string ProductDt => $"{PluLabel.ProductDt:dd.MM.yyyy}";
    
    [XmlElement] public string PluWeighingСaption => $"{WsLocaleCore.LabelPrint.LabelContextWeight}: ";
    
    [XmlElement] public string PluWeighingValueDot3Rus => $"{PluWeighing.NettoWeight:#0.000} {WsLocaleCore.LabelPrint.WeightUnitKg}".Replace('.', ',');
    
    [XmlElement] public string PluWeighingKneadingWithCaption => $"{WsLocaleCore.LabelPrint.LabelContextKneading}: {PluWeighingKneading}";
    [XmlIgnore] private string LotNumberFormat => $"{PluLabel.ProductDt:yyMM}";
    [XmlIgnore] private string ProductDateBarCodeFormat  => $"{PluLabel.ProductDt:yyMMdd}";
    [XmlIgnore] private string ProductTimeBarCodeFormat  => $"{PluLabel.ProductDt:HHmmss}";
    [XmlIgnore] private string PluNumber  => $"{PluScale.Plu.Number:000}";
    [XmlIgnore] private string ScaleNumber => $"{PluScale.Line.Number:00000}";
    [XmlIgnore] private string ScaleCounter => $"{PluScale.Line.LabelCounter:00000000}";
    [XmlIgnore] private string PluWeighingKg2 => $"{PluWeighing.NettoWeight:00.000}".Replace(',', '.').Split('.')[0];
    [XmlIgnore] private string PluWeighingKg3 => $"{PluWeighing.NettoWeight:000.000}".Replace(',', '.').Split('.')[0];
    [XmlIgnore] private string PluWeighingGr => $"{PluWeighing.NettoWeight:#.000}".Replace(',', '.').Split('.')[1];
    [XmlIgnore] private string PluWeighingKneading => $"{PluWeighing.Kneading:000}";
    [XmlIgnore] private string BarCodeGtin => PluScale.Plu.Gtin;
    
    #endregion
    
    public PluLabelWeightContextModel(WsSqlPluLabelEntity pluLabel, WsSqlViewPluNestingModel viewPluNesting,
        WsSqlPluScaleEntity pluScale, WsSqlProductionSiteEntity productionSite, WsSqlPluWeighingEntity pluWeighing)
    {
        PluLabel = pluLabel;
        ViewPluNesting = viewPluNesting;
        PluScale = pluScale;
        ProductionSite = productionSite;
        PluWeighing = pluWeighing;
    }
}