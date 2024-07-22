using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ws.PalychExchangeApi.Features.Pallets.Common;
using Ws.PalychExchangeApi.Features.Pallets.Dto;

namespace Ws.PalychExchangeApi.Features.Pallets;

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