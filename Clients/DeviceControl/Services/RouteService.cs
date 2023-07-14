using DeviceControl.Utils;
using FluentNHibernate.Conventions;
using WsStorageCore.TableDiagModels.Logs;
using WsStorageCore.TableDiagModels.LogsWebsFks;
using WsStorageCore.TableDiagModels.ScalesScreenshots;
using WsStorageCore.TableScaleFkModels.DeviceScalesFks;
using WsStorageCore.TableScaleFkModels.PlusBundlesFks;
using WsStorageCore.TableScaleFkModels.PlusLabels;
using WsStorageCore.TableScaleFkModels.PlusNestingFks;
using WsStorageCore.TableScaleFkModels.PlusWeighingsFks;
using WsStorageCore.TableScaleModels.Access;
using WsStorageCore.TableScaleModels.BarCodes;
using WsStorageCore.TableScaleModels.Boxes;
using WsStorageCore.TableScaleModels.Brands;
using WsStorageCore.TableScaleModels.Bundles;
using WsStorageCore.TableScaleModels.Clips;
using WsStorageCore.TableScaleModels.Contragents;
using WsStorageCore.TableScaleModels.Devices;
using WsStorageCore.TableScaleModels.DeviceTypes;
using WsStorageCore.TableScaleModels.Organizations;
using WsStorageCore.TableScaleModels.Plus;
using WsStorageCore.TableScaleModels.PlusGroups;
using WsStorageCore.TableScaleModels.PlusScales;
using WsStorageCore.TableScaleModels.PlusStorageMethods;
using WsStorageCore.TableScaleModels.Printers;
using WsStorageCore.TableScaleModels.PrintersTypes;
using WsStorageCore.TableScaleModels.ProductionFacilities;
using WsStorageCore.TableScaleModels.Scales;
using WsStorageCore.TableScaleModels.Templates;
using WsStorageCore.TableScaleModels.TemplatesResources;
using WsStorageCore.TableScaleModels.Versions;
using WsStorageCore.TableScaleModels.WorkShops;
using WsStorageCore.ViewScaleModels;
using WsStorageCore.ViewScaleModels.Barcodes;
using WsStorageCore.ViewScaleModels.Devices;
using WsStorageCore.ViewScaleModels.Lines;
using WsStorageCore.ViewScaleModels.Logs;
using WsStorageCore.ViewScaleModels.PluLabels;
using WsStorageCore.ViewScaleModels.PluWeightings;
using WsStorageCore.ViewScaleModels.WebLogs;

namespace DeviceControl.Services;

public class RouteService
{

    private readonly NavigationManager _navigationManager;

    public RouteService(NavigationManager navigationManager)
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

    public static string GetSectionRoute(WsSqlTableBase? item)
    {
        return item switch
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

}