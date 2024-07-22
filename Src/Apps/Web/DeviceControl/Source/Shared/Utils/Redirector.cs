using System.Security.Claims;
using Ws.DeviceControl.Models.Dto.Shared;
using Ws.Domain.Models.Common;
using Ws.Domain.Models.Entities.Devices;
using Ws.Domain.Models.Entities.Devices.Arms;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Ref1c.Plus;

namespace DeviceControl.Source.Shared.Utils;

public class Redirector(IAuthorizationService authorizationService)
{
    #region Private

    #region LinkByEntity

    private static string Link(EntityBase entity, string baseUrl) => Link(entity, baseUrl, true);

    private static string Link(EntityBase entity, string baseUrl, bool isActive) =>
        entity.IsNew || !isActive ? string.Empty : $"{baseUrl}?id={entity.Uid}";

    #endregion

    #region LinkByUid

    private static string Link(Guid uid, string baseUrl) => Link(uid, baseUrl, true);

    private static string Link(Guid uid, string baseUrl, bool isActive) =>
        !isActive ? string.Empty : $"{baseUrl}?id={uid}";

    #endregion

    private bool CheckPolicy(ClaimsPrincipal user, string policyName) =>
        authorizationService.AuthorizeAsync(user, policyName).GetAwaiter().GetResult().Succeeded;

    #endregion

    public string ToPath(Arm line, ClaimsPrincipal user) =>
        Link(line, RouteUtils.SectionLines, CheckPolicy(user, PolicyEnum.Support));

    public string ToPath(Plu item) => Link(item, RouteUtils.SectionPlus);

    public string ToPrinterPath(ProxyDto item, ClaimsPrincipal user) =>
        Link(item.Id, RouteUtils.SectionPrinters, CheckPolicy(user, PolicyEnum.Support));

    public string ToWarehousePath(ProxyDto item, ClaimsPrincipal user) =>
        Link(item.Id, RouteUtils.SectionWarehouses, CheckPolicy(user, PolicyEnum.Admin));

    public string ToBrandPath(ProxyDto item) => Link(item.Id, RouteUtils.SectionBrands);

    public string ToBundlePath(ProxyDto item) => Link(item.Id, RouteUtils.SectionBundles);

    public string ToClipPath(ProxyDto item) => Link(item.Id, RouteUtils.SectionClips);

    public string ToPath(Warehouse item, ClaimsPrincipal user) =>
        Link(item, RouteUtils.SectionWarehouses, CheckPolicy(user, PolicyEnum.Admin));

    public string ToPath(ProductionSite item, ClaimsPrincipal user) =>
        Link(item, RouteUtils.SectionProductionSites, CheckPolicy(user, PolicyEnum.Admin));

}