using Ws.Services.Dto;

namespace Ws.Services.Services.PrintLabel;

public interface IPrintLabelService
{
    string GenerateLabel(LabelInfoDto labelInfo);
}