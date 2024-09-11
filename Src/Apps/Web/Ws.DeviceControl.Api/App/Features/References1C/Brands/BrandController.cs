using Ws.DeviceControl.Api.App.Features.References1C.Brands.Common;
using Ws.DeviceControl.Models.Features.References1C.Brands;

namespace Ws.DeviceControl.Api.App.Features.References1C.Brands;


[ApiController]
[Route(ApiEndpoints.Brands)]
public class BrandController(IBrandService brandService)
{
    #region Queries

    [HttpGet]
    public Task<List<BrandDto>> GetAll() => brandService.GetAllAsync();

    [HttpGet("{id:guid}")]
    public Task<BrandDto> GetById([FromRoute] Guid id) => brandService.GetByIdAsync(id);

    #endregion
}