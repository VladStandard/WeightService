// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableDiagModels.Logs;
using WsStorageCore.TableDiagModels.LogsTypes;
using WsStorageCore.TableDiagModels.LogsWebsFks;
using WsStorageCore.TableDiagModels.ScalesScreenshots;
using WsStorageCore.Tables;
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
using WsStorageCore.Xml;

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
        if (item is not null)
        {
            if (!string.IsNullOrEmpty(page))
                return item.Identity.Name switch
                {
                    WsSqlFieldIdentity.Id => $"{page}/{item.IdentityValueId}",
                    WsSqlFieldIdentity.Uid => $"{page}/{item.IdentityValueUid}",
                    WsSqlFieldIdentity.Test => $"{page}/{nameof(WsSqlFieldIdentity.Test)}",
                    _ => string.Empty
                };
        }
        return string.Empty;
    }

    public string GetRouteItemPathShort<TItem>() where TItem : WsSqlTableBase, new() => GetRouteSectionPath(new TItem());

    protected string GetRouteSectionPath<TItem>(TItem? item) where TItem : WsSqlTableBase, new() =>
        item switch
        {
            AccessModel => LocaleCore.DeviceControl.RouteSectionAccess,
            AppModel => LocaleCore.DeviceControl.RouteSectionApps,
            BarCodeModel => LocaleCore.DeviceControl.RouteSectionBarCodes,
            BoxModel => LocaleCore.DeviceControl.RouteSectionBoxes,
            BrandModel => LocaleCore.DeviceControl.RouteSectionBrands,
            BundleModel => LocaleCore.DeviceControl.RouteSectionBundles,
            ContragentModel => LocaleCore.DeviceControl.RouteSectionContragents,
            DeviceModel => LocaleCore.DeviceControl.RouteSectionDevices,
            DeviceScaleFkModel => LocaleCore.DeviceControl.RouteSectionDevicesScalesFk,
            DeviceTypeFkModel => LocaleCore.DeviceControl.RouteSectionDevicesTypesFk,
            DeviceTypeModel => LocaleCore.DeviceControl.RouteSectionDevicesTypes,
            LogModel => LocaleCore.DeviceControl.RouteSectionLogs,
            LogQuickModel => LocaleCore.DeviceControl.RouteSectionLogs,
            LogTypeModel => LocaleCore.DeviceControl.RouteSectionLogTypes,
            LogWebFkModel => LocaleCore.DeviceControl.RouteSectionLogsWebService,
            PluGroupModel => LocaleCore.DeviceControl.RouteSectionNomenclaturesGroups,
            OrderModel => LocaleCore.DeviceControl.RouteSectionOrders,
            OrderWeighingModel => LocaleCore.DeviceControl.RouteSectionOrdersWeighings,
            OrganizationModel => LocaleCore.DeviceControl.RouteSectionOrganizations,
            PluBundleFkModel => LocaleCore.DeviceControl.RouteSectionPlusBundlesFks,
            PluLabelModel => LocaleCore.DeviceControl.RouteSectionPlusLabels,
            PluModel => LocaleCore.DeviceControl.RouteSectionPlus,
            PluNestingFkModel => LocaleCore.DeviceControl.RouteSectionPlusNestingFks,
            PluScaleModel => LocaleCore.DeviceControl.RouteSectionPlusScales,
            PluWeighingModel => LocaleCore.DeviceControl.RouteSectionPlusWeighings,
            PrinterModel => LocaleCore.DeviceControl.RouteSectionPrinters,
            PrinterResourceFkModel => LocaleCore.DeviceControl.RouteSectionPrinterResources,
            PrinterTypeModel => LocaleCore.DeviceControl.RouteSectionPrinterTypes,
            ProductionFacilityModel => LocaleCore.DeviceControl.RouteSectionProductionFacilities,
            ProductSeriesModel => LocaleCore.DeviceControl.RouteSectionProductSeries,
            ScaleModel => LocaleCore.DeviceControl.RouteSectionScales,
            ScaleScreenShotModel => LocaleCore.DeviceControl.RouteSectionScalesScreenShots,
            TaskModel => LocaleCore.DeviceControl.RouteSectionTaskModules,
            TaskTypeModel => LocaleCore.DeviceControl.RouteSectionTaskTypeModules,
            TemplateModel => LocaleCore.DeviceControl.RouteSectionTemplates,
            TemplateResourceModel => LocaleCore.DeviceControl.RouteSectionTemplateResources,
            VersionModel => LocaleCore.DeviceControl.RouteSectionVersions,
            WorkShopModel => LocaleCore.DeviceControl.RouteSectionWorkShops,
            _ => string.Empty
        };

    public string GetRouteSectionPath<TItem>() where TItem : WsSqlTableBase, new() => GetRouteSectionPath(new TItem());
    
    protected void SetRouteItemNavigate<TItem>(TItem? item) where TItem : WsSqlTableBase, new()
    {
        if (item is null) return;
        string page = GetRouteSectionPath(item);
        if (string.IsNullOrEmpty(page)) return;

        page = item.Identity.Name switch
        {
            WsSqlFieldIdentity.Id => item.IsNew ? $"{page}/new" : $"{page}/{item.IdentityValueId}",
            WsSqlFieldIdentity.Uid => item.IsNew ? $"{page}/new" : $"{page}/{item.IdentityValueUid}",
            _ => page
        };
        NavigationManager?.NavigateTo(page);
    }

    public void SetRouteSectionNavigateToRoot()
    {
        NavigationManager?.NavigateTo(LocaleCore.DeviceControl.RouteSystemRoot);
    }

    private void SetRouteSectionNavigate()
    {
        string page = GetRouteSectionPath(SqlItem);
        if (string.IsNullOrEmpty(page))
            return;

        NavigationManager?.NavigateTo(page);
    }

    #endregion
}