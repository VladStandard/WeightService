using Ws.DeviceControl.Api.App.Features.Devices.Arms.Common;
using Ws.DeviceControl.Models.Dto.Devices.Arms.Queries;

namespace Ws.DeviceControl.Api.App.Features.Devices.Arms;

[ApiController]
[Route("api/arms/")]
public class ArmController(IArmService armService)
{
    #region Queries

    [HttpGet]
    public Task<List<ArmDto>> GetAllByProductionSite([FromQuery(Name = "productionSite")] Guid productionSiteId) =>
        armService.GetAllByProductionSiteAsync(productionSiteId);

    [HttpGet("{id:guid}")]
    public Task<ArmDto> GetById([FromRoute] Guid id) => armService.GetByIdAsync(id);

    #endregion
}