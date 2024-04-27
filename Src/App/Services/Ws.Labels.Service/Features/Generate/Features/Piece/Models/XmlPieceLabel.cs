using System.Xml.Serialization;
using Ws.Labels.Service.Extensions;
using XmlLabelBaseModel = Ws.Labels.Service.Features.Generate.Models.XmlLabelBase.XmlLabelBaseModel;

namespace Ws.Labels.Service.Features.Generate.Features.Piece.Models;

[Serializable]
public class XmlPieceLabel : XmlLabelBaseModel
{
    [XmlElement] public short BundleCount { get; set; }

    [XmlElement]
    public override string BarCodeTop
    {
        get => $"233{LineNumber.ToStrLenWithZero(5)}" +
               $"{BundleCount.ToStrLenWithZero(2)}" +
               $"{LineCounter.ToStrLenWithZero(6)}" +
               $"{ProductDate}{ProductTime}" +
               $"{PluNumber.ToStrLenWithZero(3)}" +
               $"00000{Kneading.ToStrLenWithZero(3)}";
        set => _ = value;
    }

    [XmlElement]
    public override string BarCodeRight
    {
        get => $"234{LineNumber.ToStrLenWithZero(5)}" +
               $"{LineCounter.ToStrLenWithZero(6)}" +
               $"{ProductDate}";
        set => _ = value;
    }

    [XmlElement]
    public override string BarCodeBottom
    {
        get => $"(01){PluGtin}(37)" +
               $"{BundleCount.ToStrLenWithZero(8)}" +
               $"(11){ProductDate}(10){ProductDateShort}";
        set => _ = value;
    }

    public static HashSet<string> GetTypes =>
    [
        nameof(PluGtin), nameof(BundleCount), nameof(ProductDate), nameof(ProductDateShort),
        nameof(LineCounter), nameof(LineNumber), nameof(ProductTime), nameof(Kneading), nameof(PluNumber)
    ];
}