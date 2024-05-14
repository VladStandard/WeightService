using Ws.Domain.Models.Entities.Print;
using Ws.Labels.Service.Features.Generate.Features.Piece;
using Ws.Labels.Service.Features.Generate.Features.Piece.Dto;
using Ws.Labels.Service.Features.Generate.Features.Weight;
using Ws.Labels.Service.Features.Generate.Features.Weight.Dto;

namespace Ws.Labels.Service.Features.Generate;

internal class PrintLabelService(LabelPieceGenerator labelPieceGenerator, LabelWeightGenerator labelWeightGenerator)
    : IPrintLabelService
{
    public Label GenerateWeightLabel(GenerateWeightLabelDto weightLabelDto) =>
        labelWeightGenerator.GenerateLabel(weightLabelDto);

    public void GeneratePiecePallet(GeneratePiecePalletDto piecePalletDto, int labelCount) =>
        labelPieceGenerator.GeneratePiecePallet(piecePalletDto, labelCount);
}