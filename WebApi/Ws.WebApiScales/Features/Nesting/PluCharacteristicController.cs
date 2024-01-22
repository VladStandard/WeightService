using System.Xml.Linq;
using Ws.Services.Features.LogWeb;
using Ws.WebApiScales.Common;
using Ws.WebApiScales.Dto;
using Ws.WebApiScales.Features.Nesting.Dto;

namespace Ws.WebApiScales.Features.Nesting;

[AllowAnonymous]
[ApiController]
[Route("api/plu_characteristics/")]
public class PluCharacteristicController(
    IPluCharacteristicApiService pluCharacteristicApiService,
    ILogWebService logWebService,
    IHttpContextAccessor httpContextAccessor,
    ResponseDto responseDto) : BaseController(responseDto, logWebService, httpContextAccessor)
{
    
    [HttpPost("load")]
    [Produces("application/xml")]
    public ContentResult LoadCharacteristics([FromBody] XElement xml) => 
        HandleXmlRequest<PluCharacteristicsWrapper>(xml, pluCharacteristicApiService.Load);

}