// using System.Xml.Linq;
// using Ws.Domain.Services.Features.LogWeb;
// using Ws.PalychExchangeApi.Features.Nestings.Dto;
// using Ws.PalychExchangeApi.Features.Nestings.Services;
// using Ws.WebApiScales.Common;
// using Ws.WebApiScales.Dto;
//
// namespace Ws.WebApiScales.Features.Nesting;
//
// [AllowAnonymous]
// [ApiController]
// [Route("api/plu_characteristics/")]
// internal sealed class PluCharacteristicController(
//     IPluCharacteristicApiService pluCharacteristicApiService,
//     ILogWebService logWebService,
//     IHttpContextAccessor httpContextAccessor,
//     ResponseDto responseDto) : BaseController(responseDto, logWebService, httpContextAccessor)
// {
//
//     [HttpPost("load")]
//     [Produces("application/xml")]
//     public ContentResult LoadCharacteristics([FromBody] XElement xml) =>
//         HandleXmlRequest<CharacteristicsWrapper>(xml, pluCharacteristicApiService.Load);
//
// }