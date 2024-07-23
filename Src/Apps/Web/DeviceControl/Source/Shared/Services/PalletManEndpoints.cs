using Phetch.Core;
using Ws.DeviceControl.Models;
using Ws.DeviceControl.Models.Dto.Admins.PalletMen.Queries;

namespace DeviceControl.Source.Shared.Services;

public class PalletManEndpoints(IWebApi webApi)
{
    public Endpoint<Guid, PalletManDto[]> PalletMenEndpoint { get; } = new(
        webApi.GetPalletMenByProductionSite,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });
}