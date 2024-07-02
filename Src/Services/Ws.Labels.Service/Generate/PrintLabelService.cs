using Ws.Domain.Models.Entities.Print;
using Ws.Labels.Service.Generate.Features.Piece;
using Ws.Labels.Service.Generate.Features.Piece.Dto;
using Ws.Labels.Service.Generate.Features.Weight;
using Ws.Labels.Service.Generate.Features.Weight.Dto;

namespace Ws.Labels.Service.Generate;

internal class PrintLabelService(LabelPieceGenerator labelPieceGenerator, LabelWeightGenerator labelWeightGenerator)
    : IPrintLabelService
{
    public (Label, LabelZpl) GenerateWeightLabel(GenerateWeightLabelDto weightLabelDto) =>
        labelWeightGenerator.GenerateLabel(weightLabelDto);

    public Task<Guid> GeneratePiecePallet(GeneratePiecePalletDto piecePalletDto, int labelCount) =>
        labelPieceGenerator.GeneratePiecePallet(piecePalletDto, labelCount);
}