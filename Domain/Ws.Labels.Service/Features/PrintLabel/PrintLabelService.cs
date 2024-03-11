using Ws.Labels.Service.Features.PrintLabel.Types.Piece;
using Ws.Labels.Service.Features.PrintLabel.Types.Piece.Dto;
using Ws.Labels.Service.Features.PrintLabel.Types.Weight;
using Ws.Labels.Service.Features.PrintLabel.Types.Weight.Dto;

namespace Ws.Labels.Service.Features.PrintLabel;

internal class PrintLabelService(LabelPieceGenerator labelPieceGenerator, LabelWeightGenerator labelWeightGenerator) : IPrintLabelService
{
    public string GenerateWeightLabel(LabelWeightDto labelDto) =>
        labelWeightGenerator.GenerateLabel(labelDto);
    
    public void GeneratePiecePallet(LabelPiecePalletDto labelPalletDto, int labelCount) =>
        labelPieceGenerator.GeneratePiecePallet(labelPalletDto, labelCount);
}