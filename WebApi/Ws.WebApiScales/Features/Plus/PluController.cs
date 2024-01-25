using System.Xml.Linq;
using Ws.Domain.Services.Features.LogWeb;
using Ws.WebApiScales.Common;
using Ws.WebApiScales.Dto;
using Ws.WebApiScales.Features.Plus.Dto;
using Ws.WebApiScales.Features.Plus.Services;

namespace Ws.WebApiScales.Features.Plus;

[AllowAnonymous]
[ApiController]
[Route("api/plus/")]
internal sealed class  PluController(
    IPluApiService pluApiService,
    ILogWebService logWebService,
    IHttpContextAccessor httpContextAccessor,
    ResponseDto responseDto) : BaseController(responseDto, logWebService, httpContextAccessor)
{

    [HttpPost("load")]
    [Produces("application/xml")]
    public ContentResult LoadPlu([FromBody] XElement xml) => 
        HandleXmlRequest<PlusWrapper>(xml, pluApiService.Load);
}