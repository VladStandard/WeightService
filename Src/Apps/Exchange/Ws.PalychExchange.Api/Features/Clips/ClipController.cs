using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ws.PalychExchange.Api.Dto;
using Ws.PalychExchange.Api.Features.Clips.Common;
using Ws.PalychExchange.Api.Features.Clips.Dto;

namespace Ws.PalychExchange.Api.Features.Clips;

[ApiController]
[AllowAnonymous]
[Route("api/clips")]
public sealed class ClipController(IClipService clipService) : ControllerBase
{
    [HttpPost("load")]
    [Produces("application/xml")]
    public ResponseDto Load([FromBody] ClipsWrapper wrapper) => clipService.Load(wrapper);
}