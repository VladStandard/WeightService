using WsWebApiScales.Dto.Plu;
using WsWebApiScales.Dto.Response;
using WsWebApiScales.Services;

namespace WsWebApiScales.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/plu/")]
public class PluController : ControllerBase
{ 
    private readonly PluService _pluService;

    public PluController(PluService pluService)
    {
        _pluService = pluService;
    }

    [HttpPost("load")]
    [Produces("application/xml")]
    public ActionResult<ResponseDto> LoadPlu([FromBody] PlusDto plusDto) => _pluService.LoadPlu(plusDto);
    
}