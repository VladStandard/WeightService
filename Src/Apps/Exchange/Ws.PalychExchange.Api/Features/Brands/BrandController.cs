using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ws.PalychExchange.Api.Dto;
using Ws.PalychExchange.Api.Features.Brands.Common;
using Ws.PalychExchange.Api.Features.Brands.Dto;

namespace Ws.PalychExchange.Api.Features.Brands;

[ApiController]
[AllowAnonymous]
[Route(RouteUtil.Brands)]
public sealed class BrandController(IBrandService brandService)
{
    [HttpPost("load")]
    [Produces("application/xml")]
    public ResponseDto Load([FromBody] BrandsWrapper wrapper) => brandService.Load(wrapper);
}