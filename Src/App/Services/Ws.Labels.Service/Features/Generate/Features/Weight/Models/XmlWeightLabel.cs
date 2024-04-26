using System.Xml.Serialization;
using Ws.Labels.Service.Extensions;
using XmlLabelBaseModel = Ws.Labels.Service.Features.Generate.Models.XmlLabelBase.XmlLabelBaseModel;

namespace Ws.Labels.Service.Features.Generate.Features.Weight.Models;

[Serializable]
public class XmlWeightLabel : XmlLabelBaseModel
{
    [XmlIgnore] public required decimal Weight { get; set; }

    [XmlElement]
    public string WeightStr
    {
        get => Weight.ToStrWithSep(",");
        set => _ = value;
    }

    [XmlElement]
    public override string BarCodeTop
    {
        get => $"298{LineNumber.ToStrLenWithZero(5)}" +
               $"{LineCounter.ToStrLenWithZero(8)}" +
               $"{ProductDate}" +
               $"{ProductTime}" +
               $"{PluNumber.ToStrLenWithZero(3)}" +
               $"{Weight.ToStrWithLen(5)}" +
               $"{Kneading.ToStrLenWithZero(3)}";
        set => _ = value;
    }

    [XmlElement]
    public override string BarCodeRight
    {
        get => $"299{LineNumber.ToStrLenWithZero(5)}{LineCounter.ToStrLenWithZero(8)}";
        set => _ = value;
    }

    [XmlElement]
    public override string BarCodeBottom
    {
        get => $"(01){PluGtin}(3103){Weight.ToStrWithLen(6)}(11){ProductDate}(10){ProductDateShort}";
        set => _ = value;
    }

    public override HashSet<string> GetTypes =>
    [
        nameof(PluGtin), nameof(Weight), nameof(ProductDate), nameof(ProductDateShort),
        nameof(LineCounter), nameof(LineNumber), nameof(ProductTime), nameof(Kneading), nameof(PluNumber)
    ];
}