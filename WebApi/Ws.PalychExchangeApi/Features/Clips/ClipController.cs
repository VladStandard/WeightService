using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ws.PalychExchangeApi.Features.Clips.Common;
using Ws.PalychExchangeApi.Features.Clips.Dto;

namespace Ws.PalychExchangeApi.Features.Clips;

[ApiController]
[AllowAnonymous]
[Route("api/clips")]
public sealed class ClipController(IClipService clipService) : ControllerBase
{
    [HttpPost("load")]
    [Produces("application/xml")]
    public ClipWrapper Load([FromBody] ClipWrapper clips) => clipService.Load(clips);
}