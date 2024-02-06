using System.Xml.Serialization;
using Ws.Labels.Service.Features.PrintLabel.Common;
using Ws.Shared.TypeUtils;

namespace Ws.Labels.Service.Features.PrintLabel.Weight.Models;


[Serializable]
public class XmlWeightLabelModel : XmlLabelBaseModel
{ 
    [XmlIgnore] public decimal Weight { get; set; }

    [XmlElement] public string WeightStr
    {
        get => DecimalUtils.ToStrWithSep(Weight, ",");
        set => _ = value;
    } 
    
    [XmlElement] public override string BarCodeTop { 
        get => $"298{IntUtils.ToStringToLen(LineNumber, 5)}" +
               $"{IntUtils.ToStringToLen(LineCounter,8)}{ProductDate}" +
               $"{ProductTime}{IntUtils.ToStringToLen(PluNumber, 3)}" +
               $"{GetWeightStr(5)}{IntUtils.ToStringToLen(Kneading, 3)}";
        set => _ = value;
    }

    [XmlElement] public override string BarCodeRight
    {
        get => $"299{IntUtils.ToStringToLen(LineNumber, 5)}{IntUtils.ToStringToLen(LineCounter, 8)}";
        set => _ = value;
    }

    [XmlElement] public override string BarCodeBottom
    {
        get => $"(01){PluGtin}(3103){GetWeightStr(6)}(11){ProductDate}(10){ProductDateShort}";
        set => _ = value;
    }

    public string GetWeightStr(int len) => DecimalUtils.ToStrToLen(Weight, len);
}