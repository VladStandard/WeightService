using System.Xml.Linq;
using Ws.WebApiScales.Common;
using Ws.WebApiScales.Common.Services;
using Ws.WebApiScales.Dto.Plu;
using Ws.WebApiScales.Dto.Response;

namespace Ws.WebApiScales.Controllers;

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