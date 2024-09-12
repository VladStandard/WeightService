using Ws.PalychExchange.Api.App.Features.Pallets.Common;
using Ws.PalychExchange.Api.App.Features.Pallets.Dto;

namespace Ws.PalychExchange.Api.App.Features.Pallets;

[ApiController]
[Route(ApiEndpoints.Pallets)]
public sealed class PalletController(IPalletService palletService) : ControllerBase
{
    [HttpPost("update")]
    public PalletMsgWrapper Load([FromBody] PalletUpdateWrapper wrapper) => new()
    {
        Status = palletService.Update(wrapper.Pallet)
    };
}