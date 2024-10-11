using Microsoft.Extensions.Localization;
using Ws.Database.Entities.Print.Labels;
using Ws.Desktop.Api.App.Shared.Labels.Api;
using Ws.Desktop.Api.App.Shared.Labels.Api.Pallet.Output;
using Ws.Desktop.Api.App.Shared.Labels.Generate.Features.Piece;
using Ws.Desktop.Api.App.Shared.Labels.Generate.Features.Piece.Dto;
using Ws.Desktop.Api.App.Shared.Labels.Generate.Features.Weight;
using Ws.Desktop.Api.App.Shared.Labels.Generate.Features.Weight.Dto;
using Ws.Desktop.Api.App.Shared.Labels.Localization;
using Ws.Shared.Exceptions;

namespace Ws.Desktop.Api.App.Shared.Labels.Generate;

internal class PrintLabelService(
    LabelPieceGenerator labelPieceGenerator,
    LabelWeightGenerator labelWeightGenerator,
    IStringLocalizer<LabelGenResources> localizer,
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

        throw new ApiInternalException
        {
            ErrorDisplayMessage = localizer["ExchangeFailed"],
            ErrorInternalMessage = ans.Status.Message
        };
    }

    public Task<PalletOutputData> GeneratePiecePallet(GeneratePiecePalletDto piecePalletDto, int labelCount) =>
        labelPieceGenerator.GeneratePiecePallet(piecePalletDto, labelCount);
}