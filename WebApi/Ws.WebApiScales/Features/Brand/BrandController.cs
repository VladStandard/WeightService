using System.Xml.Linq;
using Ws.Domain.Services.Features.LogWeb;
using Ws.WebApiScales.Common;
using Ws.WebApiScales.Dto;
using Ws.WebApiScales.Features.Brand.Dto;
using Ws.WebApiScales.Features.Brand.Services;

namespace Ws.WebApiScales.Features.Brand;

[AllowAnonymous]
[ApiController]
[Route("api/brands/")]
internal sealed class BrandController(
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