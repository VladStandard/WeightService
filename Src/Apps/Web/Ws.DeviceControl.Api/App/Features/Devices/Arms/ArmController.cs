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

    [HttpGet("{id:guid}/plus")]
    public Task<List<PluArmDto>> GetArmPlus([FromRoute] Guid id) => armService.GetArmPlus(id);

    #endregion

    #region Commands

    [Authorize(PolicyEnum.SeniorSupport)]
    [HttpPost]
    public Task<ArmDto> Create([FromBody] ArmCreateDto dto) =>
        armService.CreateAsync(dto);

    [Authorize(PolicyEnum.SeniorSupport)]
    [HttpPost("{id:guid}/delete")]
    public Task Delete([FromRoute] Guid id) =>
        armService.DeleteAsync(id);

    [Authorize(PolicyEnum.Support)]
    [HttpPost("{id:guid}")]
    public Task<ArmDto> Update([FromRoute] Guid id, [FromBody] ArmUpdateDto dto) =>
        armService.UpdateAsync(id, dto);

    [Authorize(PolicyEnum.Support)]
    [HttpPost("{id:guid}/plus/{pluId:guid}")]
    public Task DeletePlu([FromRoute] Guid id, [FromRoute] Guid pluId) =>
        armService.DeletePluAsync(id, pluId);

    #endregion
}