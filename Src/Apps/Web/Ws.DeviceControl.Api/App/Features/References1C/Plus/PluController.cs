using Ws.DeviceControl.Api.App.Features.References1C.Plus.Common;
using Ws.DeviceControl.Models.Features.References1C.Plus.Queries;

namespace Ws.DeviceControl.Api.App.Features.References1C.Plus;

[ApiController]
[Route(ApiEndpoints.Plu)]
public sealed class PluController(IPluService pluService)
{
    #region Queries

    [HttpGet]
    public Task<List<PluDto>> GetAll() => pluService.GetAllAsync();

    [HttpGet("{id:guid}")]
    public Task<PluDto> GetById([FromRoute] Guid id) => pluService.GetByIdAsync(id);

    #endregion
}