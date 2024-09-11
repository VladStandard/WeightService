using Ws.PalychExchange.Api.App.Features.Boxes.Common;
using Ws.PalychExchange.Api.App.Features.Boxes.Dto;

namespace Ws.PalychExchange.Api.App.Features.Boxes;

[ApiController]
[AllowAnonymous]
[Route(ApiEndpoints.Boxes)]
public sealed class BoxController(IBoxService boxService) : ControllerBase
{
    [HttpPost("load")]
    [Produces("application/xml")]
    public ResponseDto Load([FromBody] BoxesWrapper wrapper) => boxService.Load(wrapper);
}