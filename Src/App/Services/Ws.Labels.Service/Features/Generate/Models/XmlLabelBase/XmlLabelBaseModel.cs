using System.Runtime.Serialization;
using System.Xml.Serialization;
using Ws.Labels.Service.Extensions;
using Ws.Labels.Service.Features.Generate.Common.XmlBarcode;

namespace Ws.Labels.Service.Features.Generate.Models.XmlLabelBase;

[Serializable]
public abstract partial class XmlLabelBaseModel : IXmlBarcodeModel, ISerializable
{
    #region Line

    [XmlElement] public required int LineNumber { get; set; }
    [XmlElement] public required int LineCounter { get; set; }
    [XmlElement] public required string LineName { get; set; }
    [XmlElement] public required string LineAddress { get; set; }

    #endregion

    #region Plu

    [XmlElement] public required short PluNumber { get; set; }
    [XmlElement] public required string PluGtin { get; set; }
    [XmlElement] public required string PluFullName { set; get; }
    [XmlElement] public required string PluDescription { get; set; }

    #endregion

    #region Other

    [XmlElement] public string ProductDateStr { get => $"{ProductDt:dd.MM.yyyy}"; set => _ = value; }
    [XmlElement] public string ExpirationDateStr { get => $"{ExpirationDt:dd.MM.yyyy}"; set => _ = value; }
    [XmlElement] public string KneadingStr { get => Kneading.ToStrLenWithZero(3); set => _ = value; }

    #endregion

    #region Barcodes

    [XmlElement] public abstract string BarCodeTop { get; set; }
    [XmlElement] public abstract string BarCodeRight { get; set; }
    [XmlElement] public abstract string BarCodeBottom { get; set; }

    #endregion
}