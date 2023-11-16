using Ws.WebApiScales.Dto.Brand;
using Ws.WebApiScales.Dto.Response;
using Ws.WebApiScales.Services;

namespace Ws.WebApiScales.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/brands/")]
public class BrandController : ControllerBase
{ 
    private readonly BrandService _brandService;
    
    public BrandController(BrandService brandService)
    {
        _brandService = brandService;
    }

    [HttpPost("load")]
    [Produces("application/xml")]
    public ActionResult<ResponseDto> LoadBrands([FromBody] BrandsDto brandsDto) 
        => _brandService.LoadBrands(brandsDto);
    
}