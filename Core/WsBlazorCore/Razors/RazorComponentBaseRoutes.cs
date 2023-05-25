// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Models;
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

namespace WsBlazorCore.Razors;

public partial class RazorComponentBase
{
    #region Public and private methods - Routes
    
    protected string GetRouteItemPathForLink<TItem>(TItem item) where TItem : WsSqlTableBase, new()
    {
        string page = GetRouteSectionPath(item);
        if (string.IsNullOrEmpty(page)) 
            return string.Empty;

        page = item.Identity.Name switch
        {
            WsSqlFieldIdentity.Id => item.IsNew ? $"{page}/new" : $"{page}/{item.IdentityValueId}",
            WsSqlFieldIdentity.Uid => item.IsNew ? $"{page}/new" : $"{page}/{item.IdentityValueUid}",
            _ => page
        };
        return page;
    }

    public string GetRouteItemPath<TItem>(TItem? item) where TItem : WsSqlTableBase, new() =>
        GetRouteItemPathCombine(GetRouteSectionPath<WsSqlTableBase>(item), item);

    public string GetRouteItemPathCombine<TItem>(string page, TItem? item) where TItem : WsSqlTableBase, new()
    {
        if (item is null || string.IsNullOrEmpty(page))
            return string.Empty;
        return item.Identity.Name switch
        {
            WsSqlFieldIdentity.Id => $"{page}/{item.IdentityValueId}",
            WsSqlFieldIdentity.Uid => $"{page}/{item.IdentityValueUid}",
            WsSqlFieldIdentity.Test => $"{page}/{nameof(WsSqlFieldIdentity.Test)}",
            _ => string.Empty
        };
    }

    protected string GetRouteSectionPath<TItem>(TItem? item) where TItem : WsSqlTableBase, new() =>
        item switch
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
            WsSqlPluStorageMethodModel=> LocaleCore.DeviceControl.RouteSectionPlusStorage,
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
            // VIEWS
            LogView => LocaleCore.DeviceControl.RouteSectionLogs,
            LineView => LocaleCore.DeviceControl.RouteSectionScales,
            BarcodeView => LocaleCore.DeviceControl.RouteSectionBarCodes,
            PlusLabelView => LocaleCore.DeviceControl.RouteSectionPlusLabels,
            PluWeightingView => LocaleCore.DeviceControl.RouteSectionPlusWeighings,
            DeviceView => LocaleCore.DeviceControl.RouteSectionDevices,
            LogWebView => LocaleCore.DeviceControl.RouteSectionLogsWebService,
            _ => string.Empty
        };

    protected void SetRouteItemNavigate<TItem>(TItem? item) where TItem : WsSqlTableBase, new()
    {
        if (item is null) 
            return;
        
        string page = GetRouteSectionPath(item);
        if (string.IsNullOrEmpty(page))
            return;

        page = item.Identity.Name switch
        {
            WsSqlFieldIdentity.Id => item.IsNew ? $"{page}/new" : $"{page}/{item.IdentityValueId}",
            WsSqlFieldIdentity.Uid => item.IsNew ? $"{page}/new" : $"{page}/{item.IdentityValueUid}",
            _ => page
        };
        NavigationManager?.NavigateTo(page);
    }

    protected void SetRouteSectionNavigate()
    {
        string page = GetRouteSectionPath(SqlItem);
        if (string.IsNullOrEmpty(page))
            return;

        NavigationManager?.NavigateTo(page);
    }

    #endregion
}