using Ws.Desktop.Models.Features.PalletMen;

namespace Ws.Desktop.Api.App.Features.PalletMen.Common;

public interface IPalletManService
{
    #region Queries

    List<PalletMan> GetAllByArm(Guid armId);

    #endregion
}