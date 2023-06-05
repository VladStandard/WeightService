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

    public void NavigateSectionRoute(WsSqlTableBase? item)
    {
        if (item == null)
            return;
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
            WsSqlAccessModel => LocaleCore.DeviceControl.RouteSectionAccess,
            WsSqlAppModel => LocaleCore.DeviceControl.RouteSectionApps,
            WsSqlBarCodeModel => LocaleCore.DeviceControl.RouteSectionBarCodes,
            WsSqlBoxModel => LocaleCore.DeviceControl.RouteSectionBoxes,
            WsSqlBrandModel => LocaleCore.DeviceControl.RouteSectionBrands,
            WsSqlBundleModel => LocaleCore.DeviceControl.RouteSectionBundles,
            WsSqlContragentModel => LocaleCore.DeviceControl.RouteSectionContragents,
            WsSqlDeviceModel => LocaleCore.DeviceControl.RouteSectionDevices,
            WsSqlDeviceScaleFkModel => LocaleCore.DeviceControl.RouteSectionDevicesScalesFk,
            WsSqlDeviceTypeFkModel => LocaleCore.DeviceControl.RouteSectionDevicesTypesFk,
            WsSqlDeviceTypeModel => LocaleCore.DeviceControl.RouteSectionDevicesTypes,
            WsSqlLogModel => LocaleCore.DeviceControl.RouteSectionLogs,
            WsSqlLogTypeModel => LocaleCore.DeviceControl.RouteSectionLogTypes,
            WsSqlLogWebFkModel => LocaleCore.DeviceControl.RouteSectionLogsWebService,
            WsSqlPluGroupModel => LocaleCore.DeviceControl.RouteSectionNomenclaturesGroups,
            WsSqlOrderModel => LocaleCore.DeviceControl.RouteSectionOrders,
            WsSqlOrderWeighingModel => LocaleCore.DeviceControl.RouteSectionOrdersWeighings,
            WsSqlOrganizationModel => LocaleCore.DeviceControl.RouteSectionOrganizations,
            WsSqlPluBundleFkModel => LocaleCore.DeviceControl.RouteSectionPlusBundlesFks,
            WsSqlPluLabelModel => LocaleCore.DeviceControl.RouteSectionPlusLabels,
            WsSqlPluModel => LocaleCore.DeviceControl.RouteSectionPlus,
            WsSqlPluNestingFkModel => LocaleCore.DeviceControl.RouteSectionPlusNestingFks,
            WsSqlPluScaleModel => LocaleCore.DeviceControl.RouteSectionPlusScales,
            WsSqlPluStorageMethodModel => LocaleCore.DeviceControl.RouteSectionPlusStorage,
            WsSqlPluWeighingModel => LocaleCore.DeviceControl.RouteSectionPlusWeighings,
            WsSqlPrinterModel => LocaleCore.DeviceControl.RouteSectionPrinters,
            WsSqlPrinterResourceFkModel => LocaleCore.DeviceControl.RouteSectionPrinterResources,
            WsSqlPrinterTypeModel => LocaleCore.DeviceControl.RouteSectionPrinterTypes,
            WsSqlProductionFacilityModel => LocaleCore.DeviceControl.RouteSectionProductionFacilities,
            WsSqlProductSeriesModel => LocaleCore.DeviceControl.RouteSectionProductSeries,
            WsSqlScaleModel => LocaleCore.DeviceControl.RouteSectionScales,
            WsSqlScaleScreenShotModel => LocaleCore.DeviceControl.RouteSectionScalesScreenShots,
            WsSqlTaskModel => LocaleCore.DeviceControl.RouteSectionTaskModules,
            WsSqlTaskTypeModel => LocaleCore.DeviceControl.RouteSectionTaskTypeModules,
            WsSqlTemplateModel => LocaleCore.DeviceControl.RouteSectionTemplates,
            WsSqlTemplateResourceModel => LocaleCore.DeviceControl.RouteSectionTemplateResources,
            WsSqlVersionModel => LocaleCore.DeviceControl.RouteSectionVersions,
            WsSqlWorkShopModel => LocaleCore.DeviceControl.RouteSectionWorkShops,

            LogView => LocaleCore.DeviceControl.RouteSectionLogs,
            LineView => LocaleCore.DeviceControl.RouteSectionScales,
            BarcodeView => LocaleCore.DeviceControl.RouteSectionBarCodes,
            PlusLabelView => LocaleCore.DeviceControl.RouteSectionPlusLabels,
            PluWeightingView => LocaleCore.DeviceControl.RouteSectionPlusWeighings,
            DeviceView => LocaleCore.DeviceControl.RouteSectionDevices,
            LogWebView => LocaleCore.DeviceControl.RouteSectionLogsWebService,
            _ => string.Empty
        };
    }

}