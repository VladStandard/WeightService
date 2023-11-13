using WsWebApiScales.Dto.PluCharacteristic;
using WsWebApiScales.Dto.Response;
using WsWebApiScales.Services;

namespace WsWebApiScales.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/plu_characteristics/")]
public class PluCharacteristicController
{
    private readonly PluCharacteristicService _pluCharacteristicService;

    public PluCharacteristicController(PluCharacteristicService pluCharacteristicService)
    {
        _pluCharacteristicService = pluCharacteristicService;
    }

    [HttpPost("load")]
    [Produces("application/xml")]
    public ActionResult<ResponseDto> LoadCharacteristics([FromBody] PluCharacteristicsDto pluCharacteristics)
        => _pluCharacteristicService.LoadCharacteristics(pluCharacteristics);

}