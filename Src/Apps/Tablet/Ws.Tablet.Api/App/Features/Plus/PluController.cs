using Ws.Tablet.Api.App.Features.Plus.Common;
using Ws.Tablet.Models.Features.Plus;

namespace Ws.Tablet.Api.App.Features.Plus;

[ApiController]
[Route(ApiEndpoints.Plu)]
public sealed class PluController(IPluService pluService)
{
    #region Queries

    [HttpGet]
    public PluDto GetPluByCode([FromQuery(Name = "number")] uint number) => pluService.GetByNumber(number);

    #endregion
}