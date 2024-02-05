using System.Xml.Serialization;
using Ws.Labels.Service.Features.PrintLabel.Common;
using Ws.Shared.TypeUtils;

namespace Ws.Labels.Service.Features.PrintLabel.Models;

[Serializable]
public class LabelModel : BaseLabelModel, ILabelModel
{
    [XmlElement] public short BundleCount { get; set; }

    [XmlElement] public string BarCodeTop {
        get => $"233{IntUtils.ToStringToLen(LineNumber, 5)}" +
               $"{IntUtils.ToStringToLen(BundleCount, 2)}" +
               $"{IntUtils.ToStringToLen(LineCounter,6)}" +
               $"{ProductDate}{ProductTime}" +
               $"{IntUtils.ToStringToLen(PluNumber, 3)}" +
               $"00000{IntUtils.ToStringToLen(Kneading, 3)}";
        set => _ = value;
    }

    
    [XmlElement] public string BarCodeRight
    {
        get => $"234{IntUtils.ToStringToLen(LineNumber, 5)}" +
               $"{IntUtils.ToStringToLen(LineCounter, 6)}" +
               $"{ProductDate}";
        set => _ = value;
    }

    [XmlElement] public virtual string BarCodeBottom
    {
        get => $"(01){PluGtin}(37)" +
               $"{IntUtils.ToStringToLen(BundleCount, 8)}" +
               $"(11){ProductDate}(10){ProductDateShort}";
        set => _ = value;
    }

}