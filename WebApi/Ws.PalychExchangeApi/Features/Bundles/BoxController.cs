using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ws.PalychExchangeApi.Features.Boxes.Common;
using Ws.PalychExchangeApi.Features.Boxes.Dto;
using Ws.PalychExchangeApi.Features.Bundles.Common;
using Ws.PalychExchangeApi.Features.Bundles.Dto;

namespace Ws.PalychExchangeApi.Features.Bundles;

[ApiController]
[AllowAnonymous]
[Route("api/bundles")]
public sealed class BoxController(IBundleService bundleService) : ControllerBase
{
    [HttpPost("load")]
    [Produces("application/xml")]
    public BundleWrapper Load([FromBody] BundleWrapper bundles) => bundleService.Load(bundles);
}