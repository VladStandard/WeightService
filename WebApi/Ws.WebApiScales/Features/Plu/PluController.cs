using System.Xml.Linq;
using Ws.WebApiScales.Common;
using Ws.WebApiScales.Dto;
using Ws.WebApiScales.Features.Plu.Dto;

namespace Ws.WebApiScales.Features.Plu;

[AllowAnonymous]
[ApiController]
[Route("api/plu/")]
public class PluController(
    IPluService pluService,
    IHttpContextAccessor httpContextAccessor,
    ResponseDto responseDto) : BaseController(httpContextAccessor, responseDto)
{

    [HttpPost("load")]
    [Produces("application/xml")]
    public ContentResult LoadPlu([FromBody] XElement xml) => 
        HandleXmlRequest<PlusWrapper>(xml, pluService.Load);
}