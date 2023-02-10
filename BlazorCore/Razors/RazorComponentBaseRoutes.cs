// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.DeviceScalesFks;
using DataCore.Sql.TableScaleFkModels.DeviceTypesFks;
using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;
using DataCore.Sql.TableScaleFkModels.PlusNestingFks;
using DataCore.Sql.TableScaleModels.Access;
using DataCore.Sql.TableScaleModels.Apps;
using DataCore.Sql.TableScaleModels.BarCodes;
using DataCore.Sql.TableScaleModels.Boxes;
using DataCore.Sql.TableScaleModels.Brands;
using DataCore.Sql.TableScaleModels.Bundles;
using DataCore.Sql.TableScaleModels.Devices;
using DataCore.Sql.TableScaleModels.DeviceTypes;
using DataCore.Sql.TableScaleModels.Logs;
using DataCore.Sql.TableScaleModels.LogsTypes;
using DataCore.Sql.TableScaleModels.Orders;
using DataCore.Sql.TableScaleModels.OrdersWeighings;
using DataCore.Sql.TableScaleModels.Organizations;
using DataCore.Sql.TableScaleModels.Plus;
using DataCore.Sql.TableScaleModels.PlusGroups;
using DataCore.Sql.TableScaleModels.PlusLabels;
using DataCore.Sql.TableScaleModels.PlusScales;
using DataCore.Sql.TableScaleModels.PlusWeighings;
using DataCore.Sql.TableScaleModels.Printers;
using DataCore.Sql.TableScaleModels.PrintersResources;
using DataCore.Sql.TableScaleModels.PrintersTypes;
using DataCore.Sql.TableScaleModels.ProductionFacilities;
using DataCore.Sql.TableScaleModels.ProductSeries;
using DataCore.Sql.TableScaleModels.Scales;
using DataCore.Sql.TableScaleModels.ScalesScreenshots;
using DataCore.Sql.TableScaleModels.Tasks;
using DataCore.Sql.TableScaleModels.TasksTypes;
using DataCore.Sql.TableScaleModels.Templates;
using DataCore.Sql.TableScaleModels.TemplatesResources;
using DataCore.Sql.TableScaleModels.Versions;
using DataCore.Sql.TableScaleModels.WorkShops;
using DataCore.Sql.Xml;
using Microsoft.JSInterop;

namespace BlazorCore.Razors;

public partial class RazorComponentBase
{
    #region Public and private methods - Routes

    protected string GetColumnIdentityName<TItem>() where TItem : SqlTableBase, new()
    {
        TItem item = new();
        string page = GetRouteItemPathShort(item);
        if (!string.IsNullOrEmpty(page))
            return item.Identity.Name switch
            {
                SqlFieldIdentity.Id => LocaleCore.Table.IdentityId,
                SqlFieldIdentity.Uid => LocaleCore.Table.IdentityUid,
                _ => LocaleCore.Table.Identity
            };
        return string.Empty;
    }

    protected string GetRouteItemPathForLink<TItem>(TItem? item) where TItem : SqlTableBase, new()
    {
        if (string.IsNullOrEmpty(RazorFieldConfig.LinkUrl))
        {
            return GetRouteItemPath(item);
        }
        return item switch
        {
            ScaleModel scale => RazorFieldConfig.SqlTable switch
            {
                SqlTableEmptyModel => GetRouteItemPathCombine(RazorFieldConfig.LinkUrl, item),
                PrinterModel => GetRouteItemPathCombine(RazorFieldConfig.LinkUrl, scale.PrinterMain),
                WorkShopModel => GetRouteItemPathCombine(RazorFieldConfig.LinkUrl, scale.WorkShop),
                _ => string.Empty,
            },
            DeviceScaleFkModel deviceScaleFk => RazorFieldConfig.SqlTable switch
            {
                SqlTableEmptyModel => GetRouteItemPathCombine(RazorFieldConfig.LinkUrl, item),
                DeviceModel => GetRouteItemPathCombine(RazorFieldConfig.LinkUrl, deviceScaleFk.Device),
                _ => string.Empty,
            },
            _ => string.Empty,
        };
    }

    public string GetRouteItemPath<TItem>(TItem? item) where TItem : SqlTableBase, new() =>
        GetRouteItemPathCombine(GetRouteItemPathShort<SqlTableBase>(item), item);

    public string GetRouteItemPathCombine<TItem>(string page, TItem? item) where TItem : SqlTableBase, new()
    {
        if (item is not null)
        {
            if (!string.IsNullOrEmpty(page))
                return item.Identity.Name switch
                {
                    SqlFieldIdentity.Id => $"{page}/{item.IdentityValueId}",
                    SqlFieldIdentity.Uid => $"{page}/{item.IdentityValueUid}",
                    SqlFieldIdentity.Test => $"{page}/{nameof(SqlFieldIdentity.Test)}",
                    _ => string.Empty
                };
        }
        return string.Empty;
    }

    public string GetRouteItemPathShort<TItem>() where TItem : SqlTableBase, new() => GetRouteItemPathShort(new TItem());

    private string GetRouteItemPathShort<TItem>(TItem? item) where TItem : SqlTableBase, new() => item switch
    {
        AccessModel => LocaleCore.DeviceControl.RouteItemAccess,
        AppModel => LocaleCore.DeviceControl.RouteItemApp,
        BrandModel => LocaleCore.DeviceControl.RouteItemBrand,
        BarCodeModel => LocaleCore.DeviceControl.RouteItemBarCode,
        BoxModel => LocaleCore.DeviceControl.RouteItemBox,
        BundleModel => LocaleCore.DeviceControl.RouteItemBundle,
        ContragentModel => LocaleCore.DeviceControl.RouteItemContragent,
        DeviceModel => LocaleCore.DeviceControl.RouteItemDevice,
        DeviceScaleFkModel => LocaleCore.DeviceControl.RouteItemDeviceScaleFk,
        DeviceTypeFkModel => LocaleCore.DeviceControl.RouteItemDeviceTypeFk,
        DeviceTypeModel => LocaleCore.DeviceControl.RouteItemDeviceType,
        LogModel => LocaleCore.DeviceControl.RouteItemLog,
        LogQuickModel => LocaleCore.DeviceControl.RouteItemLog,
        LogTypeModel => LocaleCore.DeviceControl.RouteItemLogType,
        PluGroupModel => LocaleCore.DeviceControl.RouteItemNomenclatureGroup,
        OrderModel => LocaleCore.DeviceControl.RouteItemOrder,
        OrderWeighingModel => LocaleCore.DeviceControl.RouteItemOrderWeighing,
        OrganizationModel => LocaleCore.DeviceControl.RouteItemOrganization,
        PluBundleFkModel => LocaleCore.DeviceControl.RouteItemPluBundleFk,
        PluLabelModel => LocaleCore.DeviceControl.RouteItemPluLabel,
        PluModel => LocaleCore.DeviceControl.RouteItemPlu,
        PluNestingFkModel => LocaleCore.DeviceControl.RouteItemPluNestingFk,
        PluScaleModel => LocaleCore.DeviceControl.RouteItemPluScale,
        PluWeighingModel => LocaleCore.DeviceControl.RouteItemPluWeighing,
        PrinterModel => LocaleCore.DeviceControl.RouteItemPrinter,
        PrinterResourceModel => LocaleCore.DeviceControl.RouteItemPrinterResource,
        PrinterTypeModel => LocaleCore.DeviceControl.RouteItemPrinterType,
        ProductionFacilityModel => LocaleCore.DeviceControl.RouteItemProductionFacility,
        ProductSeriesModel => LocaleCore.DeviceControl.RouteItemProductSerie,
        ScaleScreenShotModel => LocaleCore.DeviceControl.RouteItemScalesScreenShots,
        ScaleModel => LocaleCore.DeviceControl.RouteItemScale,
        TaskModel => LocaleCore.DeviceControl.RouteItemTaskModule,
        TaskTypeModel => LocaleCore.DeviceControl.RouteItemTaskTypeModule,
        TemplateModel => LocaleCore.DeviceControl.RouteItemTemplate,
        TemplateResourceModel => LocaleCore.DeviceControl.RouteItemTemplateResource,
        VersionModel => LocaleCore.DeviceControl.RouteItemVersion,
        WorkShopModel => LocaleCore.DeviceControl.RouteItemWorkShop,
        _ => string.Empty
    };

    private string GetRouteSectionPath<TItem>(TItem? item) where TItem : SqlTableBase, new() =>
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
            PluScaleModel => LocaleCore.DeviceControl.RouteSectionScales,
            PluWeighingModel => LocaleCore.DeviceControl.RouteSectionPlusWeighings,
            PrinterModel => LocaleCore.DeviceControl.RouteSectionPrinters,
            PrinterResourceModel => LocaleCore.DeviceControl.RouteSectionPrinterResources,
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

    public string GetRouteSectionPath<TItem>() where TItem : SqlTableBase, new() => GetRouteSectionPath(new TItem());

    protected string GetRouteItemPath(string uriItem, SqlTableBase? item, object? value) =>
        value switch
        {
            Guid uid => item is null ? $"{uriItem}/" : $"{uriItem}/{uid}",
            long id => item is null ? $"{uriItem}/" : $"{uriItem}/{id}",
            _ => $"{uriItem}/",
        };

    protected void SetRouteItemNavigate<TItem>(TItem? item) where TItem : SqlTableBase, new()
    {
        if (item is null) return;
        string page = GetRouteItemPathShort(item);
        if (string.IsNullOrEmpty(page)) return;

        page = item.Identity.Name switch
        {
            SqlFieldIdentity.Id => item.IsNew ? $"{page}/new" : $"{page}/{item.IdentityValueId}",
            SqlFieldIdentity.Uid => item.IsNew ? $"{page}/new" : $"{page}/{item.IdentityValueUid}",
            _ => page
        };
        NavigationManager?.NavigateTo(page);
    }

    private void SetRouteItemNavigateUsingJsRuntime(string page)
    {
        _ = Task.Run(async () =>
        {
            if (IdentityUid is not null && IdentityUid != Guid.Empty && JsRuntime is not null)
                await JsRuntime.InvokeAsync<object>("open", $"{page}/{IdentityUid}", "_blank").ConfigureAwait(false);
            else if (IdentityId is not null && JsRuntime is not null)
                await JsRuntime.InvokeAsync<object>("open", $"{page}/{IdentityId}", "_blank").ConfigureAwait(false);
        }).ConfigureAwait(true);
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