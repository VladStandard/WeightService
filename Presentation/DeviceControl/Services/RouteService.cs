using Ws.StorageCore.Entities.SchemaRef.Hosts;

namespace DeviceControl.Services;

public class RouteService
{
    private readonly NavigationManager _navigationManager;

    public RouteService(NavigationManager navigationManager)
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
            SqlAccessEntity => RouteUtils.SectionAccess,
            SqlBarCodeEntity => RouteUtils.SectionBarCodes,
            SqlBoxEntity => RouteUtils.SectionBoxes,
            SqlBrandEntity => RouteUtils.SectionBrands,
            SqlBundleEntity => RouteUtils.SectionBundles,
            SqlHostEntity => RouteUtils.SectionHosts,
            SqlLogEntity => RouteUtils.SectionLogs,
            SqlPluLabelEntity => RouteUtils.SectionPlusLabels,
            SqlPluEntity => RouteUtils.SectionPlus,
            SqlPluScaleEntity => RouteUtils.SectionPlusLines,
            SqlPluNestingFkEntity => RouteUtils.SectionPlusNestingFks,
            SqlPluStorageMethodEntity => RouteUtils.SectionPlusStorage,
            SqlPluWeighingEntity => RouteUtils.SectionPlusWeightings,
            SqlPrinterEntity => RouteUtils.SectionPrinters,
            SqlProductionSiteEntity => RouteUtils.SectionProductionFacilities,
            SqlLineEntity => RouteUtils.SectionLines,
            SqlTemplateEntity => RouteUtils.SectionTemplates,
            SqlTemplateResourceEntity => RouteUtils.SectionTemplateResources,
            SqlVersionEntity => RouteUtils.SectionVersions,
            SqlWorkShopEntity => RouteUtils.SectionWorkShops,
            SqlClipEntity => RouteUtils.SectionClips,
            SqlViewBarcodeModel => RouteUtils.SectionBarCodes,
            SqlViewPluLabelModel => RouteUtils.SectionPlusLabels,
            SqlViewPluWeightingModel => RouteUtils.SectionPlusWeightings,
            SqlLogWebEntity => RouteUtils.SectionLogsWebService,
            _ => string.Empty
        };
}
