using Ws.Labels.Service.Generate.Common.BarcodeLabel;
using Ws.Labels.Service.Generate.Models.XmlLabelBase;

namespace Ws.Labels.Service.Generate.Features.Weight.Models;

public class BarcodeWeightLabel : BarcodeLabelLabel, IBarcodeWeightLabel
{
    public required decimal Weight { get; set; }
}