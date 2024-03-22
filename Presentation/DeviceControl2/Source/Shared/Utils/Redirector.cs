using System.Security.Claims;
using DeviceControl2.Source.Shared.Auth.Policies;
using Microsoft.AspNetCore.Authorization;
using Ws.Domain.Models.Common;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;

namespace DeviceControl2.Source.Shared.Utils;


public class Redirector(IAuthorizationService authorizationService)
{
    #region Private

    private static string Link(EntityBase entity, string baseUrl) => Link(entity, baseUrl, true);

    private static string Link(EntityBase entity, string baseUrl, bool isActive) =>
        entity.IsNew || !isActive ? string.Empty : $"{baseUrl}/{entity.Uid}";

    private bool CheckPolicy(ClaimsPrincipal user, string policyName) =>
        authorizationService.AuthorizeAsync(user, policyName).GetAwaiter().GetResult().Succeeded;

    #endregion
    
    public string ToPath(LineEntity line, ClaimsPrincipal user) =>
        Link(line, RouteUtils.SectionLines, CheckPolicy(user, PolicyEnum.Support));

    public string ToPath(TemplateEntity item, ClaimsPrincipal user) =>
        Link(item, RouteUtils.SectionTemplates, CheckPolicy(user, PolicyEnum.Admin));

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

    public string ToPath(StorageMethodEntity item, ClaimsPrincipal user) =>
        Link(item, RouteUtils.SectionStorageMethods, CheckPolicy(user, PolicyEnum.Admin));
}