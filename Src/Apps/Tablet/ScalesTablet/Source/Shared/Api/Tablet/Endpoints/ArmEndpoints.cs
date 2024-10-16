using Phetch.Core;
using Ws.Tablet.Models;
using Ws.Tablet.Models.Features.Arms;

namespace ScalesTablet.Source.Shared.Api.Tablet.Endpoints;

public class ArmEndpoints(ITabletApi tabletApi)
{
    public ParameterlessEndpoint<ArmDto> ArmEndpoint { get; } = new(
        tabletApi.GetCurrentArm,
        options: new() { DefaultStaleTime = TimeSpan.FromHours(1) });
}