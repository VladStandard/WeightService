using Phetch.Core;
using Ws.Tablet.Models;
using Ws.Tablet.Models.Features.Plus;

namespace ScalesTablet.Source.Shared.Api.Tablet.Endpoints;

public class PluEndpoints(ITabletApi tabletApi)
{
    public Endpoint<uint, PluDto> PluEndpoint { get; } = new(
        tabletApi.GetPluByNumber,
        options: new() { DefaultStaleTime = TimeSpan.FromHours(1) });
}