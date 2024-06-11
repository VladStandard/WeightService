using Phetch.Core;
using Ws.Desktop.Models;
using Ws.Desktop.Models.Features.PalletMen;

namespace ScalesDesktop.Source.Shared.Services;

public class PalletApi(IDesktopApi desktopApi)
{
    public ParameterlessEndpoint<PalletMan[]> PalletMenEndpoint { get; } = new(
        async() => await desktopApi.GetPalletMen(),
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(5) });
}