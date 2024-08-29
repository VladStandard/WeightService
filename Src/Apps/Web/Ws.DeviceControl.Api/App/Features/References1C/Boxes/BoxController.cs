using Ws.DeviceControl.Api.App.Features.References1C.Boxes.Common;

namespace Ws.DeviceControl.Api.App.Features.References1C.Boxes;

[ApiController]
[Route("api/boxes/")]
public class BoxController(IBoxService boxService)
{
    #region Queries

    [HttpGet]
    public Task<List<PackageDto>> GetAll() => boxService.GetAllAsync();

    [HttpGet("{id:guid}")]
    public Task<PackageDto> GetById([FromRoute] Guid id) => boxService.GetByIdAsync(id);

    #endregion
}