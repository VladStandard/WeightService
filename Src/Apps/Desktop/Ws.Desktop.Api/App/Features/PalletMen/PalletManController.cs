using Microsoft.AspNetCore.Mvc;
using Ws.Desktop.Api.App.Features.PalletMen.Common;
using Ws.Desktop.Models.Features.PalletMen;

namespace Ws.Desktop.Api.App.Features.PalletMen;

[ApiController]
[Route("api/arms/{armId:guid}/pallet-men/")]
public class PalletManController(IPalletManService palletManService) : ControllerBase
{
    #region Queries

    [HttpGet]
    public List<PalletMan> GetAllByArm([FromRoute] Guid armId) => palletManService.GetAllByArm(armId);

    #endregion
}