using Ws.PalychExchange.Api.App.Features.Brands.Common;
using Ws.PalychExchange.Api.App.Features.Brands.Dto;

namespace Ws.PalychExchange.Api.App.Features.Brands;

[ApiController]
[Route(ApiEndpoints.Brands)]
public sealed class BrandController(IBrandService brandService)
{
    [HttpPost("load")]
    public ResponseDto Load([FromBody] BrandsWrapper wrapper) => brandService.Load(wrapper.Brands);
}