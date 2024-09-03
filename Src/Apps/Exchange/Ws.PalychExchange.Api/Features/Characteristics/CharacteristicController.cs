using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ws.PalychExchange.Api.Dto;
using Ws.PalychExchange.Api.Features.Characteristics.Common;
using Ws.PalychExchange.Api.Features.Characteristics.Dto;

namespace Ws.PalychExchange.Api.Features.Characteristics;

[AllowAnonymous]
[ApiController]
[Route("api/characteristics")]
public sealed class CharacteristicController(ICharacteristicService characteristicService)
{

    [HttpPost("load")]
    [Produces("application/xml")]
    public ResponseDto Load([FromBody] PluCharacteristicsWrapper wrapper) =>
        characteristicService.Load(wrapper);
}