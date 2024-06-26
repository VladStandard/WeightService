using Ws.Labels.Service.Api.Pallet.Input;
using Ws.Labels.Service.Api.Pallet.Output;

namespace Ws.Labels.Service.Api;

internal interface IPalychApi
{
    Task<PalletResponseDto> CreatePallet(PalletCreateApiDto dto);
}