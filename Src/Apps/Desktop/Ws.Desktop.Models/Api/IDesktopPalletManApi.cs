using Ws.Desktop.Models.Features.PalletMen;

namespace Ws.Desktop.Models.Api;

public interface IDesktopPalletManApi
{
    #region Queries

    [Get("/arms/{armUid}/pallet-men")]
    Task<PalletMan[]> GetPalletMenByArm(Guid armUid);

    #endregion
}