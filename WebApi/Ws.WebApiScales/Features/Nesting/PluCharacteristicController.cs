using System.Xml.Linq;
using Ws.WebApiScales.Common;
using Ws.WebApiScales.Dto;
using Ws.WebApiScales.Features.Nesting.Dto;

namespace Ws.WebApiScales.Features.Nesting;

[AllowAnonymous]
[ApiController]
[Route("api/plu_characteristics/")]
public class PluCharacteristicController(
    IPluCharacteristicService pluCharacteristicService,
    IHttpContextAccessor httpContextAccessor,
    ResponseDto responseDto) : BaseController(httpContextAccessor, responseDto)
{
    
    [HttpPost("load")]
    [Produces("application/xml")]
    public ContentResult LoadCharacteristics([FromBody] XElement xml) => 
        HandleXmlRequest<PluCharacteristicsWrapper>(xml, pluCharacteristicService.Load);

}