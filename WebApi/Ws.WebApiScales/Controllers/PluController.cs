using Ws.WebApiScales.Dto.Plu;
using Ws.WebApiScales.Dto.Response;
using Ws.WebApiScales.Services;

namespace Ws.WebApiScales.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/plu/")]
public class PluController(PluService pluService) : ControllerBase
{

    [HttpPost("load")]
    [Produces("application/xml")]
    public ActionResult<ResponseDto> LoadPlu([FromBody] PlusDto plusDto) => pluService.LoadPlu(plusDto);
    
}