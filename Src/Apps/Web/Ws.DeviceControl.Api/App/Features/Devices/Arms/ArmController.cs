using Ws.DeviceControl.Api.App.Features.Devices.Arms.Common;
using Ws.DeviceControl.Models.Features.Devices.Arms.Commands.Create;
using Ws.DeviceControl.Models.Features.Devices.Arms.Commands.Update;
using Ws.DeviceControl.Models.Features.Devices.Arms.Queries;

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

    #region Commands

    [HttpPost]
    public Task<ArmDto> Create([FromBody] ArmCreateDto dto) =>
        armService.CreateAsync(dto);

    [HttpPost("{id:guid}")]
    public Task<ArmDto> Update([FromRoute] Guid id, [FromBody] ArmUpdateDto dto) =>
        armService.UpdateAsync(id, dto);

    #endregion
}