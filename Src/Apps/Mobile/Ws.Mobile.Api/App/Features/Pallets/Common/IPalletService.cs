using Ws.Mobile.Models.Features.Pallets;

namespace Ws.Mobile.Api.App.Features.Pallets.Common;

public interface IPalletService
{
    #region Commands

    void Move(PalletsMoveDto palletMoveDto);

    #endregion
}