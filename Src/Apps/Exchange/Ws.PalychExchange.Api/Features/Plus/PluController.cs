using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ws.PalychExchange.Api.Dto;
using Ws.PalychExchange.Api.Features.Plus.Common;
using Ws.PalychExchange.Api.Features.Plus.Dto;

namespace Ws.PalychExchange.Api.Features.Plus;

[ApiController]
[AllowAnonymous]
[Route("api/plus")]
public sealed class ClipController(IPluService pluService)
{
    [HttpPost("load")]
    [Produces("application/xml")]
    public ResponseDto Load(PlusWrapper wrapper) => pluService.Load(wrapper);
}