using Ws.DeviceControl.Api.App.Features.Devices.Arms.Common;
using Ws.DeviceControl.Models.Features.Devices.Arms.Commands.Create;
using Ws.DeviceControl.Models.Features.Devices.Arms.Commands.Update;
using Ws.DeviceControl.Models.Features.Devices.Arms.Queries;

namespace Ws.DeviceControl.Api.App.Features.Devices.Arms;

[ApiController]
[Route(ApiEndpoints.Arms)]
[Authorize(PolicyEnum.Support)]
public class ArmController(IArmService armService)
{
    #region Queries

    [HttpGet]
    public Task<List<ArmDto>> GetAllByProductionSite([FromQuery(Name = "productionSite")] Guid productionSiteId) =>
        armService.GetAllByProductionSiteAsync(productionSiteId);

    [HttpGet("{id:guid}")]
    public Task<ArmDto> GetById([FromRoute] Guid id) => armService.GetByIdAsync(id);

    [HttpGet("{id:guid}/plus")]
    public Task<List<PluArmDto>> GetArmPlus([FromRoute] Guid id) => armService.GetArmPlus(id);

    #endregion

    #region Commands

    [HttpPut("{id:guid}")]
    public Task<ArmDto> Update([FromRoute] Guid id, [FromBody] ArmUpdateDto dto) =>
        armService.UpdateAsync(id, dto);

    [HttpDelete("{id:guid}/plus/{pluId:guid}")]
    public Task DeletePlu([FromRoute] Guid id, [FromRoute] Guid pluId) =>
        armService.DeletePluAsync(id, pluId);

    // Senior support

    [Authorize(PolicyEnum.SeniorSupport)]
    [HttpPost]
    public Task<ArmDto> Create([FromBody] ArmCreateDto dto) =>
        armService.CreateAsync(dto);

    [Authorize(PolicyEnum.SeniorSupport)]
    [HttpDelete("{id:guid}")]
    public Task Delete([FromRoute] Guid id) =>
        armService.DeleteAsync(id);

    #endregion
}