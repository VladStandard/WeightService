using System.Security.Claims;

namespace DeviceControl.Source.Shared.Utils;

public class Redirector(IAuthorizationService authorizationService)
{
    #region Private

    private static string Link(Guid uid, string baseUrl) => Link(uid, baseUrl, true);

    private static string Link(Guid uid, string baseUrl, bool isActive) =>
        !isActive ? string.Empty : $"{baseUrl}?id={uid}";

    private bool CheckPolicy(ClaimsPrincipal user, string policyName) =>
        authorizationService.AuthorizeAsync(user, policyName).GetAwaiter().GetResult().Succeeded;

    #endregion

    public string ToPrinterPath(Guid uid, ClaimsPrincipal user) =>
        Link(uid, RouteUtils.SectionPrinters, CheckPolicy(user, PolicyEnum.Support));

    public string ToWarehousePath(Guid uid, ClaimsPrincipal user) =>
        Link(uid, RouteUtils.SectionWarehouses, CheckPolicy(user, PolicyEnum.Admin));

    public string ToTemplatePath(Guid uid, ClaimsPrincipal user) =>
        Link(uid, RouteUtils.SectionTemplates, CheckPolicy(user, PolicyEnum.Support));

    public string ToArmPath(Guid uid, ClaimsPrincipal user) =>
        Link(uid, RouteUtils.SectionLines, CheckPolicy(user, PolicyEnum.Support));

    public string ToPluPath(Guid uid) =>
        Link(uid, RouteUtils.SectionPlus);

    public string ToProductionPath(Guid uid, ClaimsPrincipal user) =>
        Link(uid, RouteUtils.SectionProductionSites, CheckPolicy(user, PolicyEnum.Admin));

    public string ToBrandPath(Guid uid) => Link(uid, RouteUtils.SectionBrands);

    public string ToBundlePath(Guid uid) => Link(uid, RouteUtils.SectionBundles);

    public string ToClipPath(Guid uid) => Link(uid, RouteUtils.SectionClips);
}
