using Refit;
using Ws.Desktop.Api.App.Shared.Labels.Api.Pallet.Input;
using Ws.Desktop.Api.App.Shared.Labels.Api.Pallet.Output;

namespace Ws.Desktop.Api.App.Shared.Labels.Api;

internal interface IPalychApi
{
    [Post("/ExchangeVesovayaPalletCard")]
    Task<PalletResponseDto> CreatePallet([Body] PalletCreateApiDto dto);

    [Post("/ExchangeVesovayaPalletCardStatus")]
    Task<PalletDeleteWrapperMsg> Delete([Body] PalletDeleteWrapper dto);
}