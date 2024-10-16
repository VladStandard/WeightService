using Ws.Tablet.Models.Features.Pallets.Input;
using Ws.Tablet.Models.Features.Pallets.Output;

namespace Ws.Tablet.Models.Api;

public interface ITabletPalletApi
{
    [Post("/pallets")]
    Task<PalletDto> CreatePallet([Body] PalletCreateDto pallet);
}