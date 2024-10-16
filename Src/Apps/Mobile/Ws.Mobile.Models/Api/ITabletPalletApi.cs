using Ws.Mobile.Models.Features.Pallets;

namespace Ws.Mobile.Models.Api;

public interface ITabletPalletApi
{
    [Post("/pallets")]
    Task MovePallets([Body] PalletsMoveDto pallet);
}