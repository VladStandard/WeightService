using System.Xml.Linq;
using Ws.Services.Features.LogWeb;
using Ws.WebApiScales.Common;
using Ws.WebApiScales.Dto;
using Ws.WebApiScales.Features.Brand.Dto;

namespace Ws.WebApiScales.Features.Brand;

[AllowAnonymous]
[ApiController]
[Route("api/brands/")]
public class BrandController(
    IBrandApiService brandApiService, 
    IHttpContextAccessor httpContextAccessor,
    ILogWebService logWebService,
    ResponseDto responseDto) : BaseController(responseDto, logWebService, httpContextAccessor)
{
    [AllowAnonymous]
    [HttpPost("load")]
    [Produces("application/xml")]
    public ContentResult LoadBrands([FromBody] XElement xml) => 
        HandleXmlRequest<BrandsWrapper>(xml, brandApiService.Load);
}