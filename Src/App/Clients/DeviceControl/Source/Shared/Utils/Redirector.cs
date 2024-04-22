using System.Security.Claims;
using DeviceControl.Source.Shared.Auth.Policies;
using Microsoft.AspNetCore.Authorization;
using Ws.Domain.Models.Common;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;

namespace DeviceControl.Source.Shared.Utils;

public class Redirector(IAuthorizationService authorizationService)
{
    #region Private

    #region LinkByEntity

    private static string Link(EntityBase entity, string baseUrl) => Link(entity, baseUrl, true);

    private static string Link(EntityBase entity, string baseUrl, bool isActive) =>
        entity.IsNew || !isActive ? string.Empty : $"{baseUrl}/{entity.Uid}";

    #endregion

    #region LinkByUid

    private static string Link(Guid uid, string baseUrl) => Link(uid, baseUrl, true);

    private static string Link(Guid uid, string baseUrl, bool isActive) =>
        !isActive ? string.Empty : $"{baseUrl}/{uid}";

    #endregion

    private bool CheckPolicy(ClaimsPrincipal user, string policyName) =>
        authorizationService.AuthorizeAsync(user, policyName).GetAwaiter().GetResult().Succeeded;

    #endregion

    public string ToPath(LineEntity line, ClaimsPrincipal user) =>
        Link(line, RouteUtils.SectionLines, CheckPolicy(user, PolicyEnum.Support));

    public string ToPath(PluEntity item) => Link(item, RouteUtils.SectionPlus);

    public string ToPath(PrinterEntity item, ClaimsPrincipal user) =>
        Link(item, RouteUtils.SectionPrinters, CheckPolicy(user, PolicyEnum.Support));

    public string ToPath(WarehouseEntity item, ClaimsPrincipal user) =>
        Link(item, RouteUtils.SectionWarehouses, CheckPolicy(user, PolicyEnum.Admin));

    public string ToPath(ProductionSiteEntity item, ClaimsPrincipal user) =>
        Link(item, RouteUtils.SectionProductionSites, CheckPolicy(user, PolicyEnum.Admin));

    public string ToPath(BundleEntity item) => Link(item, RouteUtils.SectionBundles);

    public string ToPath(ClipEntity item) => Link(item, RouteUtils.SectionClips);

    public string ToPath(BrandEntity item) => Link(item, RouteUtils.SectionBrands);

    public string ToPath(BoxEntity item) => Link(item, RouteUtils.SectionBoxes);

    public string ToPath(StorageMethodEntity item, ClaimsPrincipal user) =>
        Link(item, RouteUtils.SectionStorageMethods, CheckPolicy(user, PolicyEnum.Admin));

    public string ToTemplatePath(Guid uid, ClaimsPrincipal user) =>
        Link(uid, RouteUtils.SectionTemplates, uid != Guid.Empty);

}