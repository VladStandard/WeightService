using System.Xml.Serialization;
using Ws.Labels.Common;

namespace Ws.Labels.Models;

[Serializable]
public class LabelModel : BaseLabelModel, ILabelModel
{
    [XmlElement] public short BundleCount { get; set; }

    [XmlElement] public string BarCodeTop { get => $"233{IntToStr(LineNumber, 5)}" +
                                                   $"{IntToStr(BundleCount, 2)}" +
                                                   $"{IntToStr(LineCounter,6)}" +
                                                   $"{ProductDate}{ProductTime}" +
                                                   $"{IntToStr(PluNumber, 3)}" +
                                                   $"00000{IntToStr(Kneading, 3)}";
        set => _ = value;
    }

    
    [XmlElement] public string BarCodeRight
    {
        get => $"234{IntToStr(LineNumber, 5)}" +
               $"{IntToStr(LineCounter, 6)}" +
               $"{ProductDate}";
        set => _ = value;
    }

    [XmlElement] public virtual string BarCodeBottom
    {
        get => $"(01){PluGtin}(37)" +
               $"{IntToStr(BundleCount, 8)}" +
               $"(11){ProductDate}(10){ProductDateShort}";
        set => _ = value;
    }

}