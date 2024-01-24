using System.Xml.Linq;
using Ws.Domain.Services.Features.LogWeb;
using Ws.WebApiScales.Common;
using Ws.WebApiScales.Dto;
using Ws.WebApiScales.Features.Box.Dto;
using Ws.WebApiScales.Features.Box.Services;

namespace Ws.WebApiScales.Features.Box;

[AllowAnonymous]
[ApiController]
[Route("api/boxes/")]

internal sealed class BoxController(
    ResponseDto responseDto,
    IBoxApiService boxApiService,
    ILogWebService logWebService,
    IHttpContextAccessor httpContextAccessor) :  BaseController(responseDto, logWebService, httpContextAccessor)
{
    [AllowAnonymous]
    [HttpPost("load")]
    [Produces("application/xml")]
    public ContentResult LoadBoxes([FromBody] XElement xml) => 
        HandleXmlRequest<BoxWrapper>(xml, boxApiService.Load);
}