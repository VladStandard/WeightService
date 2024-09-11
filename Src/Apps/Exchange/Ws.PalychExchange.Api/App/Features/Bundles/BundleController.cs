using Ws.PalychExchange.Api.App.Features.Bundles.Common;
using Ws.PalychExchange.Api.App.Features.Bundles.Dto;

namespace Ws.PalychExchange.Api.App.Features.Bundles;

[ApiController]
[AllowAnonymous]
[Route(ApiEndpoints.Bundles)]
public sealed class BundleController(IBundleService bundleService) : ControllerBase
{
    [HttpPost("load")]
    [Produces("application/xml")]
    public ResponseDto Load([FromBody] BundlesWrapper wrapper) => bundleService.Load(wrapper);
}