using System.Xml.Linq;
using Ws.Domain.Services.Features.LogWeb;
using Ws.WebApiScales.Common;
using Ws.WebApiScales.Dto;
using Ws.WebApiScales.Features.Clips.Dto;
using Ws.WebApiScales.Features.Clips.Services;

namespace Ws.WebApiScales.Features.Clips;

[AllowAnonymous]
[ApiController]
[Route("api/clips/")]
internal sealed class BundleController(
    ResponseDto responseDto,
    IClipApiService clipApiService,
    ILogWebService logWebService,
    IHttpContextAccessor httpContextAccessor) : BaseController(responseDto, logWebService, httpContextAccessor)
{
    [AllowAnonymous]
    [HttpPost("load")]
    [Produces("application/xml")]
    public ContentResult LoadBoxes([FromBody] XElement xml) =>
        HandleXmlRequest<ClipsWrapper>(xml, clipApiService.Load);
}