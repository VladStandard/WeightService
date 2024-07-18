using Ws.DeviceControl.Api.App.Features.References1C.Brands.Common;
using Ws.DeviceControl.Models.Dto.References1C.Brands;

namespace Ws.DeviceControl.Api.App.Features.References1C.Brands;


[ApiController]
[Route("api/brands/")]
public class BrandController(IBrandService brandService)
{
    #region Queries

    [HttpGet]
    public Task<List<BrandDto>> GetAll() => brandService.GetAllAsync();

    [HttpGet("{id:guid}")]
    public Task<BrandDto> GetById([FromRoute] Guid id) => brandService.GetByIdAsync(id);

    #endregion
}