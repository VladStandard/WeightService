using System.Xml.Linq;
using Ws.Domain.Services.Features.LogWeb;
using Ws.WebApiScales.Common;
using Ws.WebApiScales.Dto;
using Ws.WebApiScales.Features.Brands.Dto;
using Ws.WebApiScales.Features.Brands.Services;

namespace Ws.WebApiScales.Features.Brands;

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