using Ws.Labels.Service.Features.PrintLabel.Dto;

namespace Ws.Labels.Service.Features.PrintLabel;

public interface IPrintLabelService
{
    string GenerateWeightLabel(LabelWeightInfoDto labelInfo);
}