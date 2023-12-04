using Ws.Labels.Common;

namespace Ws.Labels.Models;

public class LabelModel : BaseLabelModel, ILabelModel
{
    private readonly string NestingСaption = "Вложенность:";
    public int BundleCount { get; set; }
    
    public string BarCodeTop => $"233{IntToStr(LineNumber, 5)}" +
                                $"{IntToStr(BundleCount, 2)}" +
                                $"{IntToStr(LineCounter,6)}" +
                                $"{ProductDate}{ProductTime}" +
                                $"{IntToStr(PluNumber, 3)}" +
                                $"00000{IntToStr(Kneading, 3)}";
    
    public string BarCodeRight => $"234{IntToStr(LineNumber,5)}" +
                                  $"{IntToStr(LineCounter, 6)}" +
                                  $"{ProductDate}";
    
    public virtual string BarCodeBottom => $"(01){PluGtin}(37)" +
                                           $"{IntToStr(BundleCount,8)}" +
                                           $"(11){ProductDate}(10){ProductDateShort}";
    
}