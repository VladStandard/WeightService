using Ws.Labels.Service.Generate.Common;
using Ws.Labels.Service.Generate.Common.BarcodeLabel;

namespace Ws.Labels.Service.Generate.Features.Weight.Models;

public class BarcodeWeightLabel : BarcodeGeneratorModel, IBarcodeWeightLabel
{
    public required decimal Weight { get; init; }
}