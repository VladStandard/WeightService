using Ws.PalychExchange.Api.Features.Pallets.Dto;

namespace Ws.PalychExchange.Api.Features.Pallets.Common;

public interface IPalletService
{
    PalletUpdateStatus Update(PalletUpdateDto dto);
}