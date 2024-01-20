using Ws.LabelsService.Features.PrintLabel.Dto;

namespace Ws.LabelsService.Features.PrintLabel;

public interface IPrintLabelService
{
    string GenerateLabel(LabelInfoDto labelInfo);
}