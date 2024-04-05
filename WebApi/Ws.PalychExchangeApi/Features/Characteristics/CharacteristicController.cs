using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ws.PalychExchangeApi.Dto;
using Ws.PalychExchangeApi.Features.Characteristics.Common;
using Ws.PalychExchangeApi.Features.Characteristics.Dto;

namespace Ws.PalychExchangeApi.Features.Characteristics;

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