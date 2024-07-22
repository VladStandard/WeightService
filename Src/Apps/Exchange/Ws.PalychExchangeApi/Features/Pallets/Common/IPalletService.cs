using Ws.PalychExchangeApi.Features.Pallets.Dto;

namespace Ws.PalychExchangeApi.Features.Pallets.Common;

public interface IPalletService
{
    PalletUpdateStatus Update(PalletUpdateDto dto);
}