using Phetch.Core;
using Ws.DeviceControl.Models;
using Ws.DeviceControl.Models.Dto.Admins.PalletMen.Queries;
using Ws.Shared.Extensions;

namespace DeviceControl.Source.Shared.Services;

public class PalletManEndpoints(IWebApi webApi)
{
    public Endpoint<Guid, PalletManDto[]> PalletMenEndpoint { get; } = new(
        webApi.GetPalletMenByProductionSite,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public void AddPalletMan(Guid productionSiteId, PalletManDto palletMan) =>
        PalletMenEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? query.Data! : query.Data.Prepend(palletMan).ToArray());

    public void UpdatePalletMan(Guid productionSiteId, PalletManDto palletMan) =>
        PalletMenEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? query.Data! : query.Data.ReplaceItemByKey(palletMan, p => p.Id == palletMan.Id).ToArray());

    public void DeletePalletMan(Guid productionSiteId, Guid palletManId) =>
        PalletMenEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? query.Data! : query.Data.Where(x => x.Id != palletManId).ToArray());
}