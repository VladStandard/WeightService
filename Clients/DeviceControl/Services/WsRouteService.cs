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
        
        if (string.IsNullOrEmpty(page) || item == null)
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
            WsSqlAccessEntity => WsRouteUtils.SectionAccess,
            WsSqlBarCodeEntity => WsRouteUtils.SectionBarCodes,
            WsSqlBoxEntity => WsRouteUtils.SectionBoxes,
            WsSqlBrandEntity => WsRouteUtils.SectionBrands,
            WsSqlBundleEntity => WsRouteUtils.SectionBundles,
            WsSqlDeviceEntity => WsRouteUtils.SectionDevices,
            WsSqlDeviceTypeModel => WsRouteUtils.SectionDevicesTypes,
            WsSqlLogEntity => WsRouteUtils.SectionLogs,
            WsSqlOrganizationEntity => WsRouteUtils.SectionOrganizations,
            WsSqlPluLabelEntity => WsRouteUtils.SectionPlusLabels,
            WsSqlPluEntity => WsRouteUtils.SectionPlus,
            WsSqlPluScaleEntity => WsRouteUtils.SectionPlusLines,
            WsSqlPluNestingFkEntity => WsRouteUtils.SectionPlusNestingFks,
            WsSqlPluStorageMethodEntity => WsRouteUtils.SectionPlusStorage,
            WsSqlPluWeighingEntity => WsRouteUtils.SectionPlusWeightings,
            WsSqlPrinterEntity => WsRouteUtils.SectionPrinters,
            WsSqlPrinterTypeEntity => WsRouteUtils.SectionPrinterTypes,
            WsSqlProductionSiteEntity => WsRouteUtils.SectionProductionFacilities,
            WsSqlScaleEntity => WsRouteUtils.SectionLines,
            WsSqlTemplateEntity => WsRouteUtils.SectionTemplates,
            WsSqlTemplateResourceEntity => WsRouteUtils.SectionTemplateResources,
            WsSqlVersionEntity => WsRouteUtils.SectionVersions,
            WsSqlWorkShopEntity => WsRouteUtils.SectionWorkShops,
            WsSqlClipEntity => WsRouteUtils.SectionClips,
            WsSqlViewLogModel => WsRouteUtils.SectionLogs,
            WsSqlViewBarcodeModel => WsRouteUtils.SectionBarCodes,
            WsSqlViewPluLabelModel => WsRouteUtils.SectionPlusLabels,
            WsSqlViewPluWeightingModel => WsRouteUtils.SectionPlusWeightings,
            WsSqlViewDeviceModel => WsRouteUtils.SectionDevices,
            WsSqlLogWebEntity => WsRouteUtils.SectionLogsWebService,
            _ => string.Empty
        };
}
