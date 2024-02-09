using System.Xml.Linq;
using Ws.Domain.Services.Features.LogWeb;
using Ws.WebApiScales.Common;
using Ws.WebApiScales.Dto;
using Ws.WebApiScales.Features.Bundles.Dto;
using Ws.WebApiScales.Features.Bundles.Services;

namespace Ws.WebApiScales.Features.Bundles;

[AllowAnonymous]
[ApiController]
[Route("api/bundles/")]
internal sealed class BundleController(
    ResponseDto responseDto,
    IBundleApiService bundleApiService,
    ILogWebService logWebService,
    IHttpContextAccessor httpContextAccessor) : BaseController(responseDto, logWebService, httpContextAccessor)
{
    [AllowAnonymous]
    [HttpPost("load")]
    [Produces("application/xml")]
    public ContentResult LoadBoxes([FromBody] XElement xml) =>
        HandleXmlRequest<BundlesWrapper>(xml, bundleApiService.Load);
}