using System.Xml.Serialization;
using Ws.Labels.Service.Extensions;
using Ws.Labels.Service.Features.Generate.Common.XmlBarcode;
using XmlLabelBaseModel = Ws.Labels.Service.Features.Generate.Models.XmlLabelBase.XmlLabelBaseModel;

namespace Ws.Labels.Service.Features.Generate.Features.Piece.Models;

[Serializable]
public class XmlPieceLabel : XmlLabelBaseModel, IXmlBarcodePieceXml
{
    [XmlElement] public short BundleCount { get; set; }

    [XmlElement]
    public override string BarCodeTop
    {
        get => $"233{LineNumber.ToStrLenWithZero(5)}" +
               $"{BundleCount.ToStrLenWithZero(2)}" +
               $"{LineCounter.ToStrLenWithZero(6)}" +
               $"{ProductDt:yyMMdd}{ProductDt:HHmmss}" +
               $"{PluNumber.ToStrLenWithZero(3)}" +
               $"00000{Kneading.ToStrLenWithZero(3)}";
        set => _ = value;
    }

    [XmlElement]
    public override string BarCodeRight
    {
        get => $"234{LineNumber.ToStrLenWithZero(5)}" +
               $"{LineCounter.ToStrLenWithZero(6)}" +
               $"{ProductDt:yyMMdd}";
        set => _ = value;
    }

    [XmlElement]
    public override string BarCodeBottom
    {
        get => $"(01){PluGtin}(37)" +
               $"{BundleCount.ToStrLenWithZero(8)}" +
               $"(11){ProductDt:yyMMdd}(10){ProductDt:yyMM}";
        set => _ = value;
    }
}