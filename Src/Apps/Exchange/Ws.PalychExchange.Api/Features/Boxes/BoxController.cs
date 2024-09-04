using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ws.PalychExchange.Api.Dto;
using Ws.PalychExchange.Api.Features.Boxes.Common;
using Ws.PalychExchange.Api.Features.Boxes.Dto;

namespace Ws.PalychExchange.Api.Features.Boxes;

[ApiController]
[AllowAnonymous]
[Route(RouteUtil.Boxes)]
public sealed class BoxController(IBoxService boxService) : ControllerBase
{
    [HttpPost("load")]
    [Produces("application/xml")]
    public ResponseDto Load([FromBody] BoxesWrapper wrapper) => boxService.Load(wrapper);
}