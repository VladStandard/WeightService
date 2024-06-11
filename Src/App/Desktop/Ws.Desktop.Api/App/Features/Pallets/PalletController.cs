using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ws.Desktop.Api.App.Features.Pallets.Common;
using Ws.Desktop.Models.Features.Pallets;

namespace Ws.Desktop.Api.App.Features.Pallets;

[ApiController]
[AllowAnonymous]
[Route("api/arms/{armId:guid}/pallets")]
[Consumes(MediaTypeNames.Application.Json)]
public class PalletController(IPalletApiService palletApiService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<List<PalletList>> GetAllByArm([FromRoute] Guid armId) =>
        Ok(palletApiService.GetAllByArm(armId));
}