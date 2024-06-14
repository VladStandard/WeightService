using Microsoft.AspNetCore.Mvc;
using Ws.Desktop.Api.App.Features.PalletMen.Common;
using Ws.Desktop.Models.Features.PalletMen;

namespace Ws.Desktop.Api.App.Features.PalletMen;

[ApiController]
[Route("api/pallet-men")]
public class PalletManController(IPalletManService palletManService) : ControllerBase
{
    [HttpGet]
    public List<PalletMan> GetAll() => palletManService.GetAll();
}