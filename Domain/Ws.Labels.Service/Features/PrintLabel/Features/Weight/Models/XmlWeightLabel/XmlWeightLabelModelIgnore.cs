using System.Xml.Serialization;
using Ws.Shared.TypeUtils;

namespace Ws.Labels.Service.Features.PrintLabel.Features.Weight.Models.XmlWeightLabel;

public partial class XmlWeightLabelModel
{
    [XmlIgnore] public required decimal Weight { get; set; }

    private string GetWeightStr(int len) => DecimalUtils.ToStrToLen(Weight, len);
}