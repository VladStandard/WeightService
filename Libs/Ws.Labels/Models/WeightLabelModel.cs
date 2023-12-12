using System.Globalization;
using System.Xml.Serialization;
using Ws.Labels.Common;

namespace Ws.Labels.Models;


[Serializable]
public class WeightLabelModel : BaseLabelModel, ILabelModel
{ 
    [XmlIgnore] public decimal Weight { get; set; }

    [XmlElement] public string WeightStr
    {
        get  {
            NumberFormatInfo formatInfo = new() { NumberDecimalSeparator = "," };
            return Weight.ToString(formatInfo);
        }
        set => _ = value;
    } 
    
    [XmlElement] public virtual string BarCodeTop { 
        get => $"298{IntToStr(LineNumber, 5)}" +
               $"{IntToStr(LineCounter,8)}{ProductDate}" +
               $"{ProductTime}{IntToStr(PluNumber, 3)}" +
               $"{GetWeightStr(5)}{IntToStr(Kneading, 3)}";
        set => _ = value;
    }

    [XmlElement] public virtual string BarCodeRight
    {
        get => $"299{IntToStr(LineNumber, 5)}{IntToStr(LineCounter, 8)}";
        set => _ = value;
    }

    [XmlElement] public virtual string BarCodeBottom
    {
        get => $"(01){PluGtin}(3103){GetWeightStr(6)}(11){ProductDate}(10){ProductDateShort}";
        set => _ = value;
    }

    public string GetWeightStr(int targetLength)
    {
        string formattedString = Weight.ToString(CultureInfo.InvariantCulture);
        string withoutSeparator = formattedString.Replace(".", "").Replace(",", "");
        string result = withoutSeparator.PadLeft(targetLength, '0');
        return result;
    }
}