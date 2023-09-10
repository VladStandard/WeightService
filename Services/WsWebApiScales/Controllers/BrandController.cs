using WsWebApiScales.Dto.Brand;
using WsWebApiScales.Dto.Response;
using WsWebApiScales.Services;

namespace WsWebApiScales.Controllers;

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