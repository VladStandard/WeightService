using Ws.Labels.Common;

namespace Ws.Labels.Models;

public class WeightLabelModel : BaseLabelModel, ILabelModel
{ 
    public decimal Weight { get; set; } 
    
    public virtual string BarCodeTop => $"298{IntToStr(LineNumber, 5)}" +
                                        $"{IntToStr(LineCounter,8)}{ProductDate}" +
                                        $"{ProductTime}{IntToStr(PluNumber, 3)}" +
                                        $"{GetWeightStr(5)}{IntToStr(Kneading, 3)}";
    
    public virtual string BarCodeRight => $"299{IntToStr(LineNumber, 5)}{IntToStr(LineCounter,8)}";
    
    public virtual string BarCodeBottom => $"(01){PluGtin}(3103){GetWeightStr(6)}" +
                                           $"(11){ProductDate}(10){ProductDateShort}";

    private string GetWeightStr(int charCount) => 
        Weight.ToString("0.#####", System.Globalization.CultureInfo.InvariantCulture)
            .Replace(".", "").PadLeft(charCount, '0');
}