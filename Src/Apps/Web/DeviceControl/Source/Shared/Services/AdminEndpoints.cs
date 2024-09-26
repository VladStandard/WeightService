using Phetch.Core;
using Ws.DeviceControl.Models;
using Ws.DeviceControl.Models.Features.Admins.PalletMen.Queries;
using Ws.DeviceControl.Models.Features.Admins.Users.Queries;

namespace DeviceControl.Source.Shared.Services;

public class AdminEndpoints(IWebApi webApi)
{
    public Endpoint<Guid, UserDto[]> UserRelationshipEndpoint { get; } = new(
        webApi.GetUsersByProductionSite,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(5) });

    public void UpdateUserRelationship(Guid productionSiteId, UserDto user) =>
        UserRelationshipEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? query.Data! : query.Data.ReplaceItemByKey(user, p => p.Id == user.Id).ToArray());

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