using Ws.Tablet.Api.App.Features.Arms.Common;
using Ws.Tablet.Models.Features.Arms;

namespace Ws.Tablet.Api.App.Features.Arms;

[ApiController]
[Route(ApiEndpoints.Arms)]
public sealed class ArmController(IArmService armService)
{
    #region Queries

    [HttpGet]
    public ArmDto GetCurrent() => armService.GetCurrent();

    #endregion
}