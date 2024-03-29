using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ws.PalychExchangeApi.Dto;
using Ws.PalychExchangeApi.Features.Plus.Common;
using Ws.PalychExchangeApi.Features.Plus.Dto;

namespace Ws.PalychExchangeApi.Features.Plus;

[ApiController]
[AllowAnonymous]
[Route("api/plus")]
public sealed class ClipController(IPluService pluService)
{
    [HttpPost("load")]
    [Produces("application/xml")]
    public ResponseDto Load(PlusWrapper wrapper) => pluService.Load(wrapper);
}