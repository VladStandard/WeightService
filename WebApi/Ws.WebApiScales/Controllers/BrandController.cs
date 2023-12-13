﻿using Ws.WebApiScales.Dto.Brand;
using Ws.WebApiScales.Dto.Response;
using Ws.WebApiScales.Services;

namespace Ws.WebApiScales.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/brands/")]
public class BrandController(BrandService brandService) : ControllerBase
{

    [HttpPost("load")]
    [Produces("application/xml")]
    public ActionResult<ResponseDto> LoadBrands([FromBody] BrandsDto brandsDto) 
        => brandService.LoadBrands(brandsDto);
    
}