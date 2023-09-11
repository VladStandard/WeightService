using WsStorageCore.Tables.TableRefModels.ProductionSites;
using WsStorageCore.Tables.TableRefModels.WorkShops;
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
            WsSqlAccessModel => WsRouteUtils.SectionAccess,
            WsSqlBarCodeModel => WsRouteUtils.SectionBarCodes,
            WsSqlBoxModel => WsRouteUtils.SectionBoxes,
            WsSqlBrandModel => WsRouteUtils.SectionBrands,
            WsSqlBundleModel => WsRouteUtils.SectionBundles,
            WsSqlContragentModel => WsRouteUtils.SectionContragents,
            WsSqlDeviceModel => WsRouteUtils.SectionDevices,
            WsSqlDeviceScaleFkModel => WsRouteUtils.SectionDevicesScalesFk,
            WsSqlDeviceTypeModel => WsRouteUtils.SectionDevicesTypes,
            WsSqlLogModel => WsRouteUtils.SectionLogs,
            WsSqlLogWebFkModel => WsRouteUtils.SectionLogsWebService,
            WsSqlOrganizationModel => WsRouteUtils.SectionOrganizations,
            WsSqlPluLabelModel => WsRouteUtils.SectionPlusLabels,
            WsSqlPluModel => WsRouteUtils.SectionPlus,
            WsSqlPluScaleModel => WsRouteUtils.SectionPlusLines,
            WsSqlPluNestingFkModel => WsRouteUtils.SectionPlusNestingFks,
            WsSqlPluStorageMethodModel => WsRouteUtils.SectionPlusStorage,
            WsSqlPluWeighingModel => WsRouteUtils.SectionPlusWeightings,
            WsSqlPrinterModel => WsRouteUtils.SectionPrinters,
            WsSqlPrinterTypeModel => WsRouteUtils.SectionPrinterTypes,
            WsSqlProductionSiteModel => WsRouteUtils.SectionProductionFacilities,
            WsSqlScaleModel => WsRouteUtils.SectionLines,
            WsSqlTemplateModel => WsRouteUtils.SectionTemplates,
            WsSqlTemplateResourceModel => WsRouteUtils.SectionTemplateResources,
            WsSqlVersionModel => WsRouteUtils.SectionVersions,
            WsSqlWorkShopModel => WsRouteUtils.SectionWorkShops,
            WsSqlClipModel => WsRouteUtils.SectionClips,
            WsSqlViewLogModel => WsRouteUtils.SectionLogs,
            WsSqlViewLineModel => WsRouteUtils.SectionLines,
            WsSqlViewBarcodeModel => WsRouteUtils.SectionBarCodes,
            WsSqlViewPluLabelModel => WsRouteUtils.SectionPlusLabels,
            WsSqlViewPluWeightingModel => WsRouteUtils.SectionPlusWeightings,
            WsSqlViewDeviceModel => WsRouteUtils.SectionDevices,
            WsSqlViewWebLogModel => WsRouteUtils.SectionLogsWebService,
            _ => string.Empty
        };
}
