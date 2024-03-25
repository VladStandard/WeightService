using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ws.PalychExchangeApi.Features.Boxes.Common;
using Ws.PalychExchangeApi.Features.Boxes.Dto;

namespace Ws.PalychExchangeApi.Features.Boxes;

[ApiController]
[AllowAnonymous]
[Route("api/boxes")]
public sealed class BoxController(IBoxService boxService) : ControllerBase
{
    [HttpPost("load")]
    [Produces("application/xml")]
    public BoxWrapper Load([FromBody] BoxWrapper boxes) => boxService.Load(boxes);
}