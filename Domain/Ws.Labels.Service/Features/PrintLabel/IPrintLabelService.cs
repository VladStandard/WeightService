using Ws.Labels.Service.Features.PrintLabel.Piece.Dto;
using Ws.Labels.Service.Features.PrintLabel.Weight.Dto;

namespace Ws.Labels.Service.Features.PrintLabel;

public interface IPrintLabelService
{
    string GenerateWeightLabel(LabelWeightDto labelInfo);
    string GeneratePieceLabel(LabelPieceDto labelInfo);
}