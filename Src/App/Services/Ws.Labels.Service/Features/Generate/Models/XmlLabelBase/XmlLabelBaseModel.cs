using System.Runtime.Serialization;
using System.Xml.Serialization;
using Ws.Labels.Service.Extensions;

namespace Ws.Labels.Service.Features.Generate.Models.XmlLabelBase;

[Serializable]
public abstract partial class XmlLabelBaseModel : ISerializable
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

    [XmlElement] public string ProductDateStr { get => $"{ProductDtValue:dd.MM.yyyy}"; set => _ = value; }
    [XmlElement] public string ExpirationDateStr { get => $"{ExpirationDtValue:dd.MM.yyyy}"; set => _ = value; }
    [XmlElement] public string KneadingStr { get => DigitUtils.ToStrLenWithZero((Int16)Kneading, 3); set => _ = value; }

    #endregion

    #region Barcodes

    [XmlElement] public abstract string BarCodeTop { get; set; }
    [XmlElement] public abstract string BarCodeRight { get; set; }
    [XmlElement] public abstract string BarCodeBottom { get; set; }

    #endregion
}