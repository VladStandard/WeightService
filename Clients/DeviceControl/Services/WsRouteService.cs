// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DeviceControl.Services;

public class WsRouteService
{
    private readonly NavigationManager _navigationManager;

    public WsRouteService(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
    }

    public void NavigateItemRoute(WsSqlTableBase? item)
    {
        if (item == null)
            return;
        _navigationManager.NavigateTo(GetItemRoute(item));
    }

    public void NavigateSectionRoute(WsSqlTableBase item)
    {
        _navigationManager.NavigateTo(GetSectionRoute(item));
    }

    public static string GetItemRoute(WsSqlTableBase? item)
    {
        string page = GetSectionRoute(item);
        
        if (page.IsEmpty() || item == null)
            return string.Empty;
        
        return item.Identity.Name switch
        {
            WsSqlEnumFieldIdentity.Id => item.IsNew ? $"{page}/new" : $"{page}/{item.IdentityValueId}",
            WsSqlEnumFieldIdentity.Uid => item.IsNew ? $"{page}/new" : $"{page}/{item.IdentityValueUid}",
            _ => page
        };
    }

    public static string GetSectionRoute(WsSqlTableBase? item) =>
        item switch
        {
            WsSqlAccessModel => RouteUtil.SectionAccess,
            WsSqlBarCodeModel => RouteUtil.SectionBarCodes,
            WsSqlBoxModel => RouteUtil.SectionBoxes,
            WsSqlBrandModel => RouteUtil.SectionBrands,
            WsSqlBundleModel => RouteUtil.SectionBundles,
            WsSqlContragentModel => RouteUtil.SectionContragents,
            WsSqlDeviceModel => RouteUtil.SectionDevices,
            WsSqlDeviceScaleFkModel => RouteUtil.SectionDevicesScalesFk,
            WsSqlDeviceTypeModel => RouteUtil.SectionDevicesTypes,
            WsSqlLogModel => RouteUtil.SectionLogs,
            WsSqlLogWebFkModel => RouteUtil.SectionLogsWebService,
            WsSqlPluGroupModel => RouteUtil.SectionPlusGroups,
            WsSqlOrganizationModel => RouteUtil.SectionOrganizations,
            WsSqlPluBundleFkModel => RouteUtil.SectionPlusBundlesFks,
            WsSqlPluLabelModel => RouteUtil.SectionPlusLabels,
            WsSqlPluModel => RouteUtil.SectionPlus,
            WsSqlPluScaleModel => RouteUtil.SectionPlusLines,
            WsSqlPluNestingFkModel => RouteUtil.SectionPlusNestingFks,
            WsSqlPluStorageMethodModel => RouteUtil.SectionPlusStorage,
            WsSqlPluWeighingModel => RouteUtil.SectionPlusWeightings,
            WsSqlPrinterModel => RouteUtil.SectionPrinters,
            WsSqlPrinterTypeModel => RouteUtil.SectionPrinterTypes,
            WsSqlProductionFacilityModel => RouteUtil.SectionProductionFacilities,
            WsSqlScaleModel => RouteUtil.SectionLines,
            WsSqlScaleScreenShotModel => RouteUtil.SectionScalesScreenShots,
            WsSqlTemplateModel => RouteUtil.SectionTemplates,
            WsSqlTemplateResourceModel => RouteUtil.SectionTemplateResources,
            WsSqlVersionModel => RouteUtil.SectionVersions,
            WsSqlWorkShopModel => RouteUtil.SectionWorkShops,
            WsSqlClipModel => RouteUtil.SectionClips,
            WsSqlViewLogModel => RouteUtil.SectionLogs,
            WsSqlViewLineModel => RouteUtil.SectionLines,
            WsSqlViewBarcodeModel => RouteUtil.SectionBarCodes,
            WsSqlViewPluLabelModel => RouteUtil.SectionPlusLabels,
            WsSqlViewPluWeightingModel => RouteUtil.SectionPlusWeightings,
            WsSqlViewDeviceModel => RouteUtil.SectionDevices,
            WsSqlViewWebLogModel => RouteUtil.SectionLogsWebService,
            _ => string.Empty
        };
}