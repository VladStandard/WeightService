using Ws.PalychExchange.Api.App.Features.Brands.Common;
using Ws.PalychExchange.Api.App.Features.Brands.Dto;

namespace Ws.PalychExchange.Api.App.Features.Brands;

[ApiController]
[AllowAnonymous]
[Route(ApiEndpoints.Brands)]
public sealed class BrandController(IBrandService brandService)
{
    [HttpPost("load")]
    [Produces("application/xml")]
    public ResponseDto Load([FromBody] BrandsWrapper wrapper) => brandService.Load(wrapper);
}