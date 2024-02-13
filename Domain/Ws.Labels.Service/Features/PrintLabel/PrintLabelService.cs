using Ws.Domain.Models.Entities.Print;
using Ws.Labels.Service.Features.PrintLabel.Piece;
using Ws.Labels.Service.Features.PrintLabel.Piece.Dto;
using Ws.Labels.Service.Features.PrintLabel.Weight;
using Ws.Labels.Service.Features.PrintLabel.Weight.Dto;

namespace Ws.Labels.Service.Features.PrintLabel;

public class PrintLabelService : IPrintLabelService
{
    public string GenerateWeightLabel(LabelWeightDto labelDto) =>
        new LabelWeightGenerator().GenerateLabel(labelDto);

    public string GeneratePieceLabel(LabelPieceDto labelDto) =>
        new LabelPieceGenerator().GenerateLabel(labelDto);
    
    public void GeneratePiecePallet(LabelPieceDto labelDto, PalletEntity pallet, int labelCount) =>
        new LabelPieceGenerator().GeneratePiecePallet(labelDto, pallet, labelCount);
}