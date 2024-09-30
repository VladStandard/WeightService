using Ws.Database.EntityFramework.Entities.Print.Labels;
using Ws.Labels.Service.Api;
using Ws.Labels.Service.Api.Pallet.Output;
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
    public LabelEntity GenerateWeightLabel(GenerateWeightLabelDto weightLabelDto) =>
        labelWeightGenerator.GenerateLabel(weightLabelDto);

    public async Task<bool> DeletePallet(string palletNumber, bool isDelete)
    {
        PalletDeleteWrapperMsg ans =
            await palychApi.Delete(new() { Pallet = new() { IsDelete = isDelete, Number = palletNumber } });

        if (ans.Status.IsSuccess)
            return true;

        throw new ApiExceptionServer
        {
            ErrorDisplayMessage = "Ошибка в палыч",
            ErrorInternalMessage = ans.Status.Message
        };
    }

    public Task<PalletOutputData> GeneratePiecePallet(GeneratePiecePalletDto piecePalletDto, int labelCount) =>
        labelPieceGenerator.GeneratePiecePallet(piecePalletDto, labelCount);
}