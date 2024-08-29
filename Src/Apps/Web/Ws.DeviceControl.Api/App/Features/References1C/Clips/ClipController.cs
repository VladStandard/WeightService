using Ws.DeviceControl.Api.App.Features.References1C.Clips.Common;
using Ws.DeviceControl.Models.Shared;

namespace Ws.DeviceControl.Api.App.Features.References1C.Clips;

[ApiController]
[Route("api/clips/")]
public class ClipController(IClipService clipService)
{
    #region Queries

    [HttpGet]
    public Task<List<PackageDto>> GetAll() => clipService.GetAllAsync();

    [HttpGet("{id:guid}")]
    public Task<PackageDto> GetById([FromRoute] Guid id) => clipService.GetByIdAsync(id);

    #endregion
}