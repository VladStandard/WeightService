using Ws.PalychExchange.Api.App.Features.Boxes.Common;
using Ws.PalychExchange.Api.App.Features.Boxes.Dto;

namespace Ws.PalychExchange.Api.App.Features.Boxes;

[ApiController]
[Route(ApiEndpoints.Boxes)]
public sealed class BoxController(IBoxService boxService) : ControllerBase
{
    [HttpPost("load")]
    public ResponseDto Load([FromBody] BoxesWrapper wrapper) => boxService.Load(wrapper.Boxes);
}