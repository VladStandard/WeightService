using DeviceControl.Utils;
using FluentNHibernate.Conventions;
using WsStorageCore.Tables.TableDiagModels.Logs;
using WsStorageCore.Tables.TableDiagModels.LogsWebsFks;
using WsStorageCore.Tables.TableDiagModels.ScalesScreenshots;
using WsStorageCore.Tables.TableScaleFkModels.DeviceScalesFks;
using WsStorageCore.Tables.TableScaleFkModels.PlusBundlesFks;
using WsStorageCore.Tables.TableScaleFkModels.PlusLabels;
using WsStorageCore.Tables.TableScaleFkModels.PlusNestingFks;
using WsStorageCore.Tables.TableScaleFkModels.PlusWeighingsFks;
using WsStorageCore.Tables.TableScaleModels.Access;
using WsStorageCore.Tables.TableScaleModels.BarCodes;
using WsStorageCore.Tables.TableScaleModels.Boxes;
using WsStorageCore.Tables.TableScaleModels.Brands;
using WsStorageCore.Tables.TableScaleModels.Bundles;
using WsStorageCore.Tables.TableScaleModels.Clips;
using WsStorageCore.Tables.TableScaleModels.Contragents;
using WsStorageCore.Tables.TableScaleModels.Devices;
using WsStorageCore.Tables.TableScaleModels.DeviceTypes;
using WsStorageCore.Tables.TableScaleModels.Organizations;
using WsStorageCore.Tables.TableScaleModels.Plus;
using WsStorageCore.Tables.TableScaleModels.PlusGroups;
using WsStorageCore.Tables.TableScaleModels.PlusScales;
using WsStorageCore.Tables.TableScaleModels.PlusStorageMethods;
using WsStorageCore.Tables.TableScaleModels.Printers;
using WsStorageCore.Tables.TableScaleModels.PrintersTypes;
using WsStorageCore.Tables.TableScaleModels.ProductionFacilities;
using WsStorageCore.Tables.TableScaleModels.Scales;
using WsStorageCore.Tables.TableScaleModels.Templates;
using WsStorageCore.Tables.TableScaleModels.TemplatesResources;
using WsStorageCore.Tables.TableScaleModels.Versions;
using WsStorageCore.Tables.TableScaleModels.WorkShops;
using WsStorageCore.Views.ViewScaleModels.Barcodes;
using WsStorageCore.Views.ViewScaleModels.Devices;
using WsStorageCore.Views.ViewScaleModels.Lines;
using WsStorageCore.Views.ViewScaleModels.Logs;
using WsStorageCore.Views.ViewScaleModels.PluLabels;
using WsStorageCore.Views.ViewScaleModels.PluWeightings;
using WsStorageCore.Views.ViewScaleModels.WebLogs;

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