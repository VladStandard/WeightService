using System.Xml.Linq;
using Ws.WebApiScales.Common;
using Ws.WebApiScales.Dto;
using Ws.WebApiScales.Features.Brand.Dto;

namespace Ws.WebApiScales.Features.Brand;

[AllowAnonymous]
[ApiController]
[Route("api/brands/")]
public class BrandController(
    IBrandService brandService, 
    IHttpContextAccessor httpContextAccessor,
    ResponseDto responseDto) : BaseController(httpContextAccessor, responseDto)
{
    [AllowAnonymous]
    [HttpPost("load")]
    [Produces("application/xml")]
    public ContentResult LoadBrands([FromBody] XElement xml) => 
        HandleXmlRequest<BrandsWrapper>(xml, brandService.Load);
}