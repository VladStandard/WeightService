using Ws.Desktop.Models.Features.PalletMen;

namespace Ws.Desktop.Models.Api;

public interface IDesktopPalletManApi
{
    #region Queries

    [Get("/pallet-men")]
    Task<PalletMan> GetPalletManByCode(string code);

    #endregion
}