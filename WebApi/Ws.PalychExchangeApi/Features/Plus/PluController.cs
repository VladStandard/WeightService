// using System.Xml.Linq;
// using Ws.PalychExchangeApi.Features.Plus.Dto;
//
// namespace Ws.PalychExchangeApi.Features.Plus;
//
// [AllowAnonymous]
// [ApiController]
// [Route("api/plus/")]
// internal sealed class PluController(
//     IPluApiService pluApiService,
//     ILogWebService logWebService,
//     IHttpContextAccessor httpContextAccessor,
//     ResponseDto responseDto) : BaseController(responseDto, logWebService, httpContextAccessor)
// {
//
//     [HttpPost("load")]
//     [Produces("application/xml")]
//     public ContentResult LoadPlu([FromBody] XElement xml) =>
//         HandleXmlRequest<PlusWrapper>(xml, pluApiService.Load);
// }