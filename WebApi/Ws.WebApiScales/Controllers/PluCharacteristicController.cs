using Ws.WebApiScales.Dto.PluCharacteristic;
using Ws.WebApiScales.Dto.Response;
using Ws.WebApiScales.Services;

namespace Ws.WebApiScales.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/plu_characteristics/")]
public class PluCharacteristicController(PluCharacteristicService pluCharacteristicService)
{

    [HttpPost("load")]
    [Produces("application/xml")]
    public ActionResult<ResponseDto> LoadCharacteristics([FromBody] PluCharacteristicsDto pluCharacteristics)
        => pluCharacteristicService.LoadCharacteristics(pluCharacteristics);

}