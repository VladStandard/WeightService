using Ws.DeviceControl.Api.App.Features.Admins.PalletMen.Common;
using Ws.DeviceControl.Models.Dto.Admins.PalletMen.Queries;

namespace Ws.DeviceControl.Api.App.Features.Admins.PalletMen;

[ApiController]
[Route("api/pallet-men/")]
public class PalletManController(IPalletManService palletManService)
{
    #region Queries

    [HttpGet]
    public Task<List<PalletManDto>> GetAllByProductionSite([FromQuery(Name = "productionSite")] Guid productionSiteId) =>
        palletManService.GetAllByProductionSiteAsync(productionSiteId);

    [HttpGet("{id:guid}")]
    public Task<PalletManDto> GetById([FromRoute] Guid id) => palletManService.GetByIdAsync(id);

    #endregion
}