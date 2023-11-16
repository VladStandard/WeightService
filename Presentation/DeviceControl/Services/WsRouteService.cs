using Ws.StorageCore.Entities.SchemaRef.Hosts;
namespace DeviceControl.Services;

public class WsRouteService
{
    private readonly NavigationManager _navigationManager;

    public WsRouteService(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
    }

    public void NavigateItemRoute(SqlEntityBase? item)
    {
        if (item == null)
            return;
        _navigationManager.NavigateTo(GetItemRoute(item));
    }

    public void NavigateSectionRoute(SqlEntityBase item)
    {
        _navigationManager.NavigateTo(GetSectionRoute(item));
    }

    public static string GetItemRoute(SqlEntityBase? item)
    {
        string page = GetSectionRoute(item);
        
        if (string.IsNullOrEmpty(page) || item == null)
            return string.Empty;
        
        return item.Identity.Name switch
        {
            SqlEnumFieldIdentity.Id => item.IsNew ? $"{page}/new" : $"{page}/{item.IdentityValueId}",
            SqlEnumFieldIdentity.Uid => item.IsNew ? $"{page}/new" : $"{page}/{item.IdentityValueUid}",
            _ => page
        };
    }

    public static string GetSectionRoute(SqlEntityBase? item) =>
        item switch
        {
            SqlAccessEntity => WsRouteUtils.SectionAccess,
            SqlBarCodeEntity => WsRouteUtils.SectionBarCodes,
            SqlBoxEntity => WsRouteUtils.SectionBoxes,
            SqlBrandEntity => WsRouteUtils.SectionBrands,
            SqlBundleEntity => WsRouteUtils.SectionBundles,
            SqlHostEntity => WsRouteUtils.SectionHosts,
            SqlLogEntity => WsRouteUtils.SectionLogs,
            SqlPluLabelEntity => WsRouteUtils.SectionPlusLabels,
            SqlPluEntity => WsRouteUtils.SectionPlus,
            SqlPluScaleEntity => WsRouteUtils.SectionPlusLines,
            SqlPluNestingFkEntity => WsRouteUtils.SectionPlusNestingFks,
            SqlPluStorageMethodEntity => WsRouteUtils.SectionPlusStorage,
            SqlPluWeighingEntity => WsRouteUtils.SectionPlusWeightings,
            SqlPrinterEntity => WsRouteUtils.SectionPrinters,
            SqlProductionSiteEntity => WsRouteUtils.SectionProductionFacilities,
            SqlScaleEntity => WsRouteUtils.SectionLines,
            SqlTemplateEntity => WsRouteUtils.SectionTemplates,
            SqlTemplateResourceEntity => WsRouteUtils.SectionTemplateResources,
            SqlVersionEntity => WsRouteUtils.SectionVersions,
            SqlWorkShopEntity => WsRouteUtils.SectionWorkShops,
            SqlClipEntity => WsRouteUtils.SectionClips,
            SqlViewBarcodeModel => WsRouteUtils.SectionBarCodes,
            SqlViewPluLabelModel => WsRouteUtils.SectionPlusLabels,
            SqlViewPluWeightingModel => WsRouteUtils.SectionPlusWeightings,
            SqlLogWebEntity => WsRouteUtils.SectionLogsWebService,
            _ => string.Empty
        };
}
