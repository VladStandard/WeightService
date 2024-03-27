using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ws.PalychExchangeApi.Dto;
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
    public ResponseDto Load([FromBody] ClipsWrapper wrapper) => clipService.Load(wrapper);
}