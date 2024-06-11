using Phetch.Core;
using Ws.Desktop.Models;
using Ws.Desktop.Models.Features.Plus.Output;

namespace ScalesDesktop.Source.Shared.Services;

public class PluApi(IDesktopApi desktopApi)
{
    public Endpoint<Guid, PluWeight[]> WeightPlusEndpoint { get; } = new(
        async value => await desktopApi.GetPlusByArm(value),
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(5) });
}