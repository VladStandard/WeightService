// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableDiagModels.Logs;
using DataCore.Sql.TableDiagModels.LogsTypes;
using DataCore.Sql.TableDiagModels.ScalesScreenshots;
using DataCore.Sql.TableScaleFkModels.DeviceScalesFks;
using DataCore.Sql.TableScaleFkModels.DeviceTypesFks;
using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;
using DataCore.Sql.TableScaleFkModels.PlusLabels;
using DataCore.Sql.TableScaleFkModels.PlusNestingFks;
using DataCore.Sql.TableScaleFkModels.PlusWeighingsFks;
using DataCore.Sql.TableScaleFkModels.PrintersResourcesFks;
using DataCore.Sql.TableScaleModels.Access;
using DataCore.Sql.TableScaleModels.Apps;
using DataCore.Sql.TableScaleModels.BarCodes;
using DataCore.Sql.TableScaleModels.Boxes;
using DataCore.Sql.TableScaleModels.Brands;
using DataCore.Sql.TableScaleModels.Bundles;
using DataCore.Sql.TableScaleModels.Devices;
using DataCore.Sql.TableScaleModels.DeviceTypes;
using DataCore.Sql.TableScaleModels.Orders;
using DataCore.Sql.TableScaleModels.OrdersWeighings;
using DataCore.Sql.TableScaleModels.Organizations;
using DataCore.Sql.TableScaleModels.Plus;
using DataCore.Sql.TableScaleModels.PlusGroups;
using DataCore.Sql.TableScaleModels.PlusScales;
using DataCore.Sql.TableScaleModels.Printers;
using DataCore.Sql.TableScaleModels.PrintersTypes;
using DataCore.Sql.TableScaleModels.ProductionFacilities;
using DataCore.Sql.TableScaleModels.ProductSeries;
using DataCore.Sql.TableScaleModels.Scales;
using DataCore.Sql.TableScaleModels.Tasks;
using DataCore.Sql.TableScaleModels.TasksTypes;
using DataCore.Sql.TableScaleModels.Templates;
using DataCore.Sql.TableScaleModels.TemplatesResources;
using DataCore.Sql.TableScaleModels.Versions;
using DataCore.Sql.TableScaleModels.WorkShops;
using DataCore.Sql.Xml;

namespace BlazorCore.Razors;

public partial class RazorComponentBase
{
    #region Public and private methods - Routes
    
    protected string GetRouteItemPathForLink<TItem>(TItem? item) where TItem : WsSqlTableBase, new()
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