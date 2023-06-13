using WsLocalizationCore.Utils;
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
            WsSqlAccessModel => WsLocaleCore.DeviceControl.RouteSectionAccess,
            WsSqlAppModel => WsLocaleCore.DeviceControl.RouteSectionApps,
            WsSqlBarCodeModel => WsLocaleCore.DeviceControl.RouteSectionBarCodes,
            WsSqlBoxModel => WsLocaleCore.DeviceControl.RouteSectionBoxes,
            WsSqlBrandModel => WsLocaleCore.DeviceControl.RouteSectionBrands,
            WsSqlBundleModel => WsLocaleCore.DeviceControl.RouteSectionBundles,
            WsSqlContragentModel => WsLocaleCore.DeviceControl.RouteSectionContragents,
            WsSqlDeviceModel => WsLocaleCore.DeviceControl.RouteSectionDevices,
            WsSqlDeviceScaleFkModel => WsLocaleCore.DeviceControl.RouteSectionDevicesScalesFk,
            WsSqlDeviceTypeFkModel => WsLocaleCore.DeviceControl.RouteSectionDevicesTypesFk,
            WsSqlDeviceTypeModel => WsLocaleCore.DeviceControl.RouteSectionDevicesTypes,
            WsSqlLogModel => WsLocaleCore.DeviceControl.RouteSectionLogs,
            WsSqlLogTypeModel => WsLocaleCore.DeviceControl.RouteSectionLogTypes,
            WsSqlLogWebFkModel => WsLocaleCore.DeviceControl.RouteSectionLogsWebService,
            WsSqlPluGroupModel => WsLocaleCore.DeviceControl.RouteSectionNomenclaturesGroups,
            WsSqlOrderModel => WsLocaleCore.DeviceControl.RouteSectionOrders,
            WsSqlOrderWeighingModel => WsLocaleCore.DeviceControl.RouteSectionOrdersWeighings,
            WsSqlOrganizationModel => WsLocaleCore.DeviceControl.RouteSectionOrganizations,
            WsSqlPluBundleFkModel => WsLocaleCore.DeviceControl.RouteSectionPlusBundlesFks,
            WsSqlPluLabelModel => WsLocaleCore.DeviceControl.RouteSectionPlusLabels,
            WsSqlPluModel => WsLocaleCore.DeviceControl.RouteSectionPlus,
            WsSqlPluNestingFkModel => WsLocaleCore.DeviceControl.RouteSectionPlusNestingFks,
            WsSqlPluScaleModel => WsLocaleCore.DeviceControl.RouteSectionPlusScales,
            WsSqlPluStorageMethodModel => WsLocaleCore.DeviceControl.RouteSectionPlusStorage,
            WsSqlPluWeighingModel => WsLocaleCore.DeviceControl.RouteSectionPlusWeighings,
            WsSqlPrinterModel => WsLocaleCore.DeviceControl.RouteSectionPrinters,
            WsSqlPrinterResourceFkModel => WsLocaleCore.DeviceControl.RouteSectionPrinterResources,
            WsSqlPrinterTypeModel => WsLocaleCore.DeviceControl.RouteSectionPrinterTypes,
            WsSqlProductionFacilityModel => WsLocaleCore.DeviceControl.RouteSectionProductionFacilities,
            WsSqlProductSeriesModel => WsLocaleCore.DeviceControl.RouteSectionProductSeries,
            WsSqlScaleModel => WsLocaleCore.DeviceControl.RouteSectionScales,
            WsSqlScaleScreenShotModel => WsLocaleCore.DeviceControl.RouteSectionScalesScreenShots,
            WsSqlTaskModel => WsLocaleCore.DeviceControl.RouteSectionTaskModules,
            WsSqlTaskTypeModel => WsLocaleCore.DeviceControl.RouteSectionTaskTypeModules,
            WsSqlTemplateModel => WsLocaleCore.DeviceControl.RouteSectionTemplates,
            WsSqlTemplateResourceModel => WsLocaleCore.DeviceControl.RouteSectionTemplateResources,
            WsSqlVersionModel => WsLocaleCore.DeviceControl.RouteSectionVersions,
            WsSqlWorkShopModel => WsLocaleCore.DeviceControl.RouteSectionWorkShops,

            WsSqlViewLogModel => WsLocaleCore.DeviceControl.RouteSectionLogs,
            WsSqlViewLineModel => WsLocaleCore.DeviceControl.RouteSectionScales,
            WsSqlViewBarcodeModel => WsLocaleCore.DeviceControl.RouteSectionBarCodes,
            WsSqlViewPluLabelModel => WsLocaleCore.DeviceControl.RouteSectionPlusLabels,
            WsSqlViewPluWeighting => WsLocaleCore.DeviceControl.RouteSectionPlusWeighings,
            WsSqlViewDeviceModel => WsLocaleCore.DeviceControl.RouteSectionDevices,
            WsSqlViewWebLogModel => WsLocaleCore.DeviceControl.RouteSectionLogsWebService,
            _ => string.Empty
        };
    }

}