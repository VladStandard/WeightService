using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ws.PalychExchange.Api.Features.Pallets.Common;
using Ws.PalychExchange.Api.Features.Pallets.Dto;

namespace Ws.PalychExchange.Api.Features.Pallets;

[ApiController]
[AllowAnonymous]
[Route("api/pallets")]
public sealed class PalletController(IPalletService palletService) : ControllerBase
{
    [HttpPost("update")]
    [Produces("application/xml")]
    public PalletMsgWrapper Load([FromBody] PalletUpdateWrapper wrapper) => new()
    {
        Status = palletService.Update(wrapper.Pallet)
    };
}