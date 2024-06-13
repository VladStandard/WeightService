using Phetch.Core;
using Ws.Desktop.Models;
using Ws.Desktop.Models.Features.Plus.Weight.Output;

namespace ScalesDesktop.Source.Shared.Services;

public class PluApi(IDesktopApi desktopApi)
{
    public Endpoint<Guid, PluWeight[]> WeightPlusEndpoint { get; } = new(
        async value => await desktopApi.GetPlusWeightByArm(value),
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(5) });
}