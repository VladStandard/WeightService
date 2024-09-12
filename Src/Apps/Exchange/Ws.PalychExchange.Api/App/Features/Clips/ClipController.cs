using Ws.PalychExchange.Api.App.Features.Clips.Common;
using Ws.PalychExchange.Api.App.Features.Clips.Dto;

namespace Ws.PalychExchange.Api.App.Features.Clips;

[ApiController]
[Route(ApiEndpoints.Clips)]
public sealed class ClipController(IClipService clipService) : ControllerBase
{
    [HttpPost("load")]
    public ResponseDto Load([FromBody] ClipsWrapper wrapper) => clipService.Load(wrapper.Clips);
}