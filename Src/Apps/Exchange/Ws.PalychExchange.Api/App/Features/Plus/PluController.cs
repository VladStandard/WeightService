using Ws.PalychExchange.Api.App.Features.Plus.Common;
using Ws.PalychExchange.Api.App.Features.Plus.Dto;

namespace Ws.PalychExchange.Api.App.Features.Plus;

[ApiController]
[AllowAnonymous]
[Route(ApiEndpoints.Plu)]
public sealed class ClipController(IPluService pluService)
{
    [HttpPost("load")]
    [Produces("application/xml")]
    public ResponseDto Load(PlusWrapper wrapper) => pluService.Load(wrapper);
}