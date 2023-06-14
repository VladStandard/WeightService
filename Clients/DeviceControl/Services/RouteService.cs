using DeviceControl.Utils;
using WsStorageCore.Common;
using WsStorageCore.TableDiagModels.Logs;
using WsStorageCore.TableDiagModels.LogsTypes;
using WsStorageCore.TableDiagModels.LogsWebsFks;
using WsStorageCore.TableDiagModels.ScalesScreenshots;
using WsStorageCore.TableScaleFkModels.DeviceScalesFks;
using WsStorageCore.TableScaleFkModels.DeviceTypesFks;
using WsStorageCore.TableScaleFkModels.PlusBundlesFks;
using WsStorageCore.TableScaleFkModels.PlusLabels;
using WsStorageCore.TableScaleFkModels.PlusNestingFks;
using WsStorageCore.TableScaleFkModels.PlusWeighingsFks;
using WsStorageCore.TableScaleFkModels.PrintersResourcesFks;
using WsStorageCore.TableScaleModels.Access;
using WsStorageCore.TableScaleModels.Apps;
using WsStorageCore.TableScaleModels.BarCodes;
using WsStorageCore.TableScaleModels.Boxes;
using WsStorageCore.TableScaleModels.Brands;
using WsStorageCore.TableScaleModels.Bundles;
using WsStorageCore.TableScaleModels.Contragents;
using WsStorageCore.TableScaleModels.Devices;
using WsStorageCore.TableScaleModels.DeviceTypes;
using WsStorageCore.TableScaleModels.Orders;
using WsStorageCore.TableScaleModels.OrdersWeighings;
using WsStorageCore.TableScaleModels.Organizations;
using WsStorageCore.TableScaleModels.Plus;
using WsStorageCore.TableScaleModels.PlusGroups;
using WsStorageCore.TableScaleModels.PlusScales;
using WsStorageCore.TableScaleModels.PlusStorageMethods;
using WsStorageCore.TableScaleModels.Printers;
using WsStorageCore.TableScaleModels.PrintersTypes;
using WsStorageCore.TableScaleModels.ProductionFacilities;
using WsStorageCore.TableScaleModels.ProductSeries;
using WsStorageCore.TableScaleModels.Scales;
using WsStorageCore.TableScaleModels.Tasks;
using WsStorageCore.TableScaleModels.TasksTypes;
using WsStorageCore.TableScaleModels.Templates;
using WsStorageCore.TableScaleModels.TemplatesResources;
using WsStorageCore.TableScaleModels.Versions;
using WsStorageCore.TableScaleModels.WorkShops;
using WsStorageCore.ViewScaleModels;

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
        if (item == null)
            return string.Empty;
        string page = GetSectionRoute(item);
        return item.Identity.Name switch
        {
            WsSqlFieldIdentity.Id => item.IsNew ? $"{page}/new" : $"{page}/{item.IdentityValueId}",
            WsSqlFieldIdentity.Uid => item.IsNew ? $"{page}/new" : $"{page}/{item.IdentityValueUid}",
            _ => page
        };
    }

    public static string GetSectionRoute(WsSqlTableBase? item)
    {
        return item switch
        {
            WsSqlAccessModel => RouteUtil.RouteSectionAccess,
            WsSqlAppModel => RouteUtil.RouteSectionApps,
            WsSqlBarCodeModel => RouteUtil.RouteSectionBarCodes,
            WsSqlBoxModel => RouteUtil.RouteSectionBoxes,
            WsSqlBrandModel => RouteUtil.RouteSectionBrands,
            WsSqlBundleModel => RouteUtil.RouteSectionBundles,
            WsSqlContragentModel => RouteUtil.RouteSectionContragents,
            WsSqlDeviceModel => RouteUtil.RouteSectionDevices,
            WsSqlDeviceScaleFkModel => RouteUtil.RouteSectionDevicesScalesFk,
            WsSqlDeviceTypeFkModel => RouteUtil.RouteSectionDevicesTypesFk,
            WsSqlDeviceTypeModel => RouteUtil.RouteSectionDevicesTypes,
            WsSqlLogModel => RouteUtil.RouteSectionLogs,
            WsSqlLogTypeModel => RouteUtil.RouteSectionLogTypes,
            WsSqlLogWebFkModel => RouteUtil.RouteSectionLogsWebService,
            WsSqlPluGroupModel => RouteUtil.RouteSectionNomenclaturesGroups,
            WsSqlOrderModel => RouteUtil.RouteSectionOrders,
            WsSqlOrderWeighingModel => RouteUtil.RouteSectionOrdersWeighings,
            WsSqlOrganizationModel => RouteUtil.RouteSectionOrganizations,
            WsSqlPluBundleFkModel => RouteUtil.RouteSectionPlusBundlesFks,
            WsSqlPluLabelModel => RouteUtil.RouteSectionPlusLabels,
            WsSqlPluModel => RouteUtil.RouteSectionPlus,
            WsSqlPluNestingFkModel => RouteUtil.RouteSectionPlusNestingFks,
            WsSqlPluScaleModel => RouteUtil.RouteSectionPlusScales,
            WsSqlPluStorageMethodModel => RouteUtil.RouteSectionPlusStorage,
            WsSqlPluWeighingModel => RouteUtil.RouteSectionPlusWeighings,
            WsSqlPrinterModel => RouteUtil.RouteSectionPrinters,
            WsSqlPrinterResourceFkModel => RouteUtil.RouteSectionPrinterResources,
            WsSqlPrinterTypeModel => RouteUtil.RouteSectionPrinterTypes,
            WsSqlProductionFacilityModel => RouteUtil.RouteSectionProductionFacilities,
            WsSqlProductSeriesModel => RouteUtil.RouteSectionProductSeries,
            WsSqlScaleModel => RouteUtil.RouteSectionScales,
            WsSqlScaleScreenShotModel => RouteUtil.RouteSectionScalesScreenShots,
            WsSqlTaskModel => RouteUtil.RouteSectionTaskModules,
            WsSqlTaskTypeModel => RouteUtil.RouteSectionTaskTypeModules,
            WsSqlTemplateModel => RouteUtil.RouteSectionTemplates,
            WsSqlTemplateResourceModel => RouteUtil.RouteSectionTemplateResources,
            WsSqlVersionModel => RouteUtil.RouteSectionVersions,
            WsSqlWorkShopModel => RouteUtil.RouteSectionWorkShops,

            WsSqlViewLogModel => RouteUtil.RouteSectionLogs,
            WsSqlViewLineModel => RouteUtil.RouteSectionScales,
            WsSqlViewBarcodeModel => RouteUtil.RouteSectionBarCodes,
            WsSqlViewPluLabelModel => RouteUtil.RouteSectionPlusLabels,
            WsSqlViewPluWeighting => RouteUtil.RouteSectionPlusWeighings,
            WsSqlViewDeviceModel => RouteUtil.RouteSectionDevices,
            WsSqlViewWebLogModel => RouteUtil.RouteSectionLogsWebService,
            _ => string.Empty
        };
    }

}