using Ws.DeviceControl.Models.Features.Admins.PalletMen.Commands.Create;
using Ws.DeviceControl.Models.Features.Admins.PalletMen.Commands.Update;
using Ws.DeviceControl.Models.Features.Admins.PalletMen.Queries;

namespace Ws.DeviceControl.Api.App.Features.Admins.PalletMen.Common;

public interface IPalletManService
{
    #region Queries

    Task<PalletManDto> GetByIdAsync(Guid id);
    Task<List<PalletManDto>> GetAllByProductionSiteAsync(Guid productionSiteId);

    #endregion

    #region Commands

    Task<PalletManDto> CreateAsync(PalletManCreateDto dto);
    Task<PalletManDto> UpdateAsync(Guid id, PalletManUpdateDto dto);

    #endregion
}