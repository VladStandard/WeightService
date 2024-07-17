using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ws.PalychExchangeApi.Dto;
using Ws.PalychExchangeApi.Features.Brands.Common;
using Ws.PalychExchangeApi.Features.Brands.Dto;

namespace Ws.PalychExchangeApi.Features.Brands;

[ApiController]
[AllowAnonymous]
[Route("api/brands/")]
public sealed class BrandController(IBrandService brandService)
{
    [HttpPost("load")]
    [Produces("application/xml")]
    public ResponseDto Load([FromBody] BrandsWrapper wrapper) => brandService.Load(wrapper);
}