using Ws.DeviceControl.Models.Dto.Admins.PalletMen.Queries;

namespace Ws.DeviceControl.Api.App.Features.Admins.PalletMen.Common;

public interface IPalletManService
{
    #region Queries

    Task<PalletManDto> GetByIdAsync(Guid id);
    Task<List<PalletManDto>> GetAllByProductionSiteAsync(Guid productionSiteId);

    #endregion
}