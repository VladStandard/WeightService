using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ws.Desktop.Api.App.Features.PalletMen.Common;
using Ws.Desktop.Api.App.Shared.Auth;
using Ws.Desktop.Models.Features.PalletMen;

namespace Ws.Desktop.Api.App.Features.PalletMen;

[ApiController]
[Authorize(PolicyEnum.Pc)]
[Route(RouteUtil.PalletMen)]
public class PalletManController(IPalletManService palletManService)
{
    #region Queries

    [HttpGet]
    public List<PalletMan> GetAllByArm() => palletManService.GetAll();

    #endregion
}