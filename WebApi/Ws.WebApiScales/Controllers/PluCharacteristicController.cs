using Ws.WebApiScales.Dto.PluCharacteristic;
using Ws.WebApiScales.Dto.Response;
using Ws.WebApiScales.Services;

namespace Ws.WebApiScales.Controllers;

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