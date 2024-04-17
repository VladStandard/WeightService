using Ws.Domain.Models.Entities.Print;
using Ws.Labels.Service.Features.PrintLabel.Features.Piece;
using Ws.Labels.Service.Features.PrintLabel.Features.Piece.Dto;
using Ws.Labels.Service.Features.PrintLabel.Features.Weight;
using Ws.Labels.Service.Features.PrintLabel.Features.Weight.Dto.PrintWeightPlu;

namespace Ws.Labels.Service.Features.PrintLabel;

internal class PrintLabelService(LabelPieceGenerator labelPieceGenerator, LabelWeightGenerator labelWeightGenerator) : IPrintLabelService
{
    public LabelEntity GenerateWeightLabel(GenerateWeightLabelDto weightLabelDto) =>
        labelWeightGenerator.GenerateLabel(weightLabelDto);

    public void GeneratePiecePallet(GeneratePiecePalletDto piecePalletDto, int labelCount) =>
        labelPieceGenerator.GeneratePiecePallet(piecePalletDto, labelCount);
}