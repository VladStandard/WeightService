using Ws.Domain.Models.Entities.Print;
using Ws.Labels.Service.Api;
using Ws.Labels.Service.Api.Pallet.Output;
using Ws.Labels.Service.Generate.Exceptions;
using Ws.Labels.Service.Generate.Features.Piece;
using Ws.Labels.Service.Generate.Features.Piece.Dto;
using Ws.Labels.Service.Generate.Features.Weight;
using Ws.Labels.Service.Generate.Features.Weight.Dto;
using Ws.Shared.Api.ApiException;

namespace Ws.Labels.Service.Generate;

internal class PrintLabelService(
    LabelPieceGenerator labelPieceGenerator,
    LabelWeightGenerator labelWeightGenerator,
    IPalychApi palychApi
    )
    : IPrintLabelService
{
    public (Label, LabelZpl) GenerateWeightLabel(GenerateWeightLabelDto weightLabelDto) =>
        labelWeightGenerator.GenerateLabel(weightLabelDto);

    public async Task<bool> DeletePallet(string palletNumber, bool isDelete)
    {
        PalletDeleteWrapperMsg ans =
            await palychApi.Delete(new() { Pallet = new() { IsDelete = isDelete, Number = palletNumber }});

        if (ans.Status.IsSuccess)
            return true;

        throw new ApiExceptionServer
        {
            ExceptionType = LabelGenExceptions.ExchangeFailed,
            ErrorInternalMessage = ans.Status.Message
        };
    }

    public Task<Guid> GeneratePiecePallet(GeneratePiecePalletDto piecePalletDto, int labelCount, uint counter) =>
        labelPieceGenerator.GeneratePiecePallet(piecePalletDto, labelCount, counter);
}