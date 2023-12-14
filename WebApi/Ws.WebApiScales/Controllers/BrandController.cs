using System.Xml.Linq;
using Ws.WebApiScales.Common;
using Ws.WebApiScales.Dto.Brand;
using Ws.WebApiScales.Dto.Response;
using Ws.WebApiScales.Common.Services;

namespace Ws.WebApiScales.Controllers;

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