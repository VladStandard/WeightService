using Ws.PalychExchange.Api.App.Features.Pallets.Dto;

namespace Ws.PalychExchange.Api.App.Features.Pallets.Common;

public interface IPalletService
{
    PalletUpdateStatus Update(PalletUpdateDto dto);
}