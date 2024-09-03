using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ws.PalychExchange.Api.Dto;
using Ws.PalychExchange.Api.Features.Bundles.Common;
using Ws.PalychExchange.Api.Features.Bundles.Dto;

namespace Ws.PalychExchange.Api.Features.Bundles;

[ApiController]
[AllowAnonymous]
[Route("api/bundles")]
public sealed class BundleController(IBundleService bundleService) : ControllerBase
{
    [HttpPost("load")]
    [Produces("application/xml")]
    public ResponseDto Load([FromBody] BundlesWrapper wrapper) => bundleService.Load(wrapper);
}