using Ws.Domain.Models.Entities.Print;
using Ws.Labels.Service.Features.PrintLabel.Piece;
using Ws.Labels.Service.Features.PrintLabel.Piece.Dto;
using Ws.Labels.Service.Features.PrintLabel.Weight;
using Ws.Labels.Service.Features.PrintLabel.Weight.Dto;

namespace Ws.Labels.Service.Features.PrintLabel;

internal class PrintLabelService(LabelPieceGenerator labelPieceGenerator, LabelWeightGenerator labelWeightGenerator) : IPrintLabelService
{
    public string GenerateWeightLabel(LabelWeightDto labelDto) =>
        labelWeightGenerator.GenerateLabel(labelDto);

    public string GeneratePieceLabel(LabelPieceDto labelDto) =>
        labelPieceGenerator.GenerateLabel(labelDto);
    
    public void GeneratePiecePallet(LabelPieceDto labelDto, PalletEntity pallet, int labelCount) =>
        labelPieceGenerator.GeneratePiecePallet(labelDto, pallet, labelCount);
}