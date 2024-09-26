using System.Security.Claims;

namespace DeviceControl.Source.Shared.Utils;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class RedirectHelper(IAuthorizationService authorizationService)
{
    #region Private

    private static string Link(Guid uid, string baseUrl) => Link(uid, baseUrl, true);

    private static string Link(Guid uid, string baseUrl, bool isActive) =>
        !isActive ? string.Empty : $"{baseUrl}?id={uid}";

    private bool CheckPolicy(ClaimsPrincipal user, string policyName) =>
        authorizationService.AuthorizeAsync(user, policyName).GetAwaiter().GetResult().Succeeded;

    #endregion

    #region For Support

    public string ToTemplate(Guid uid, ClaimsPrincipal user) =>
        Link(uid, RouteUtils.SectionTemplates, CheckPolicy(user, PolicyEnum.Support));

    public string ToArm(Guid uid, ClaimsPrincipal user) =>
        Link(uid, RouteUtils.SectionLines, CheckPolicy(user, PolicyEnum.Support));

    public string ToPrinter(Guid uid, ClaimsPrincipal user) =>
        Link(uid, RouteUtils.SectionPrinters, CheckPolicy(user, PolicyEnum.Support));

    #endregion

    #region For Admin

    public string ToWarehouse(Guid uid, ClaimsPrincipal user) =>
        Link(uid, RouteUtils.SectionWarehouses, CheckPolicy(user, PolicyEnum.Admin));

    public string ToProductionSite(Guid uid, ClaimsPrincipal user) =>
        Link(uid, RouteUtils.SectionProductionSites, CheckPolicy(user, PolicyEnum.Admin));

    #endregion

    #region For All

    public string ToPlu(Guid uid) =>
        Link(uid, RouteUtils.SectionPlus);

    public string ToBrand(Guid uid) => Link(uid, RouteUtils.SectionBrands);

    public string ToBundle(Guid uid) => Link(uid, RouteUtils.SectionBundles);

    public string ToClip(Guid uid) => Link(uid, RouteUtils.SectionClips);

    #endregion
}