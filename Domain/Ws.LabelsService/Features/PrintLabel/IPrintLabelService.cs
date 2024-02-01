using Ws.LabelsService.Features.PrintLabel.Dto;

namespace Ws.LabelsService.Features.PrintLabel;

public interface IPrintLabelService
{
    string GenerateWeightLabel(LabelWeightInfoDto labelInfo);
}