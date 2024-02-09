using System.Xml.Linq;
using Ws.Domain.Services.Features.LogWeb;
using Ws.WebApiScales.Common;
using Ws.WebApiScales.Dto;
using Ws.WebApiScales.Features.Characteristics.Dto;
using Ws.WebApiScales.Features.Characteristics.Services;

namespace Ws.WebApiScales.Features.Nesting;

[AllowAnonymous]
[ApiController]
[Route("api/plu_characteristics/")]
internal sealed class PluCharacteristicController(
    IPluCharacteristicApiService pluCharacteristicApiService,
    ILogWebService logWebService,
    IHttpContextAccessor httpContextAccessor,
    ResponseDto responseDto) : BaseController(responseDto, logWebService, httpContextAccessor)
{

    [HttpPost("load")]
    [Produces("application/xml")]
    public ContentResult LoadCharacteristics([FromBody] XElement xml) =>
        HandleXmlRequest<CharacteristicsWrapper>(xml, pluCharacteristicApiService.Load);

}