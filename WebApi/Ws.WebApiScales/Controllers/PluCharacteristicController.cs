using System.Xml.Linq;
using Ws.WebApiScales.Common;
using Ws.WebApiScales.Common.Services;
using Ws.WebApiScales.Dto.PluCharacteristic;
using Ws.WebApiScales.Dto.Response;

namespace Ws.WebApiScales.Controllers;

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