using Microsoft.AspNetCore.Mvc;
using Refit;
using Ws.Tablet.Api.App.Features.Pallets.Common;
using Ws.Tablet.Api.App.Shared;
using Ws.Tablet.Models.Features.Pallets.Input;
using Ws.Tablet.Models.Features.Pallets.Output;

namespace Ws.Tablet.Api.App.Features.Pallets;

[ApiController]
[Route(ApiEndpoints.Pallets)]
public sealed class PalletController(IPalletService palletService)
{
    #region Commands

    [HttpPost]
    public PalletDto GetPluByCode([Body] PalletCreateDto palletCreateDto) => palletService.Create(palletCreateDto);

    #endregion
}