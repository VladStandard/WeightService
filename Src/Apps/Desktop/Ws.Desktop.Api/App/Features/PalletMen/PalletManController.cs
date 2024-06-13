using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ws.Desktop.Api.App.Features.PalletMen.Common;
using Ws.Desktop.Models.Features.PalletMen;

namespace Ws.Desktop.Api.App.Features.PalletMen;

[ApiController]
[AllowAnonymous]
[Route("api/pallet-men")]
[Consumes(MediaTypeNames.Application.Json)]
public class PalletManController(IPalletManService palletManService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public List<PalletMan> GetAll() => palletManService.GetAll();
}