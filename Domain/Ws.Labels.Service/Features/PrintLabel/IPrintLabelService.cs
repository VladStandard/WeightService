using Ws.Labels.Service.Features.PrintLabel.Types.Piece.Dto;
using Ws.Labels.Service.Features.PrintLabel.Types.Weight.Dto;

namespace Ws.Labels.Service.Features.PrintLabel;

public interface IPrintLabelService
{
    string GenerateWeightLabel(LabelWeightDto labelInfo);
    void GeneratePiecePallet(LabelPiecePalletDto labelPalletDto, int labelCount);
}