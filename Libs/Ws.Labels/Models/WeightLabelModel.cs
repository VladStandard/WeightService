using System.Xml.Serialization;
using Ws.Labels.Common;

namespace Ws.Labels.Models;


[Serializable]
public class WeightLabelModel : BaseLabelModel, ILabelModel
{ 
    [XmlElement] public decimal Weight { get; set; } 
    
    [XmlElement] public virtual string BarCodeTop => $"298{IntToStr(LineNumber, 5)}" +
                                                    $"{IntToStr(LineCounter,8)}{ProductDate}" +
                                                    $"{ProductTime}{IntToStr(PluNumber, 3)}" +
                                                    $"{GetWeightStr(5)}{IntToStr(Kneading, 3)}";
    
    [XmlElement] public virtual string BarCodeRight => $"299{IntToStr(LineNumber, 5)}{IntToStr(LineCounter,8)}";
    
    [XmlElement] public virtual string BarCodeBottom => $"(01){PluGtin}(3103){GetWeightStr(6)}" +
                                                       $"(11){ProductDate}(10){ProductDateShort}";

    private string GetWeightStr(int charCount) => 
        Weight.ToString("0.#####", System.Globalization.CultureInfo.InvariantCulture)
            .Replace(".", "").PadLeft(charCount, '0');
}