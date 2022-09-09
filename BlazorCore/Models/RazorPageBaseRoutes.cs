// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Localizations;
using DataCore.Sql.Core;
using DataCore.Sql.Tables;
using DataCore.Sql.Xml;
using Microsoft.JSInterop;

namespace BlazorCore.Models;

public partial class RazorPageBase
{
    #region Public and private methods - Routes

    protected string GetColumnIdentityName<T>() where T : TableBase, new()
    {
	    T item = new();
	    string page = GetRouteItemNavigatePage(item);
	    if (!string.IsNullOrEmpty(page))
		    return item.Identity.Name switch
		    {
			    SqlFieldIdentityEnum.Id => LocaleCore.Table.IdentityId,
			    SqlFieldIdentityEnum.Uid => LocaleCore.Table.IdentityUid,
			    _ => LocaleCore.Table.Identity
		    };
	    return string.Empty;
    }

    public string GetRoutePath(TableBase? item)
	{
		if (item is not null)
		{
            string page = GetRouteItemNavigatePage(item);
            if (!string.IsNullOrEmpty(page))
                return item.Identity.Name switch
                {
                    SqlFieldIdentityEnum.Id => $"{page}/{item.Identity.Id}",
                    SqlFieldIdentityEnum.Uid => $"{page}/{item.Identity.Uid}",
                    SqlFieldIdentityEnum.Test => $"{page}/{nameof(SqlFieldIdentityEnum.Test)}",
                    _ => string.Empty
                };
        }
        return string.Empty;
    }

    protected string GetRoutePath(string uriItemRoute, DataCore.Sql.Tables.TableBase? item, long? id) =>
        item is null || id is null ? string.Empty : $"{uriItemRoute}/{id}";

    protected string GetRoutePath(string uriItemRoute, DataCore.Sql.Tables.TableBase? item, Guid? uid) =>
        item is null || uid is null ? string.Empty : $"{uriItemRoute}/{uid}";

    private string GetRouteSectionNavigatePage(TableBase? item)
	{
        string page = string.Empty;
        switch (item)
        {
            case AccessModel:
                page = LocaleCore.DeviceControl.RouteSectionAccess;
                break;
            case AppModel:
                page = LocaleCore.DeviceControl.RouteSectionApps;
                break;
            case BarCodeModel:
                page = LocaleCore.DeviceControl.RouteSectionBarCodes;
                break;
            case BarCodeTypeModel:
                page = LocaleCore.DeviceControl.RouteSectionBarCodeTypes;
                break;
            case ContragentModel:
                page = LocaleCore.DeviceControl.RouteSectionContragents;
                break;
            case LogModel:
                page = LocaleCore.DeviceControl.RouteSectionLogs;
                break;
            case LogTypeModel:
                page = LocaleCore.DeviceControl.RouteSectionLogTypes;
                break;
            case OrderModel:
                page = LocaleCore.DeviceControl.RouteSectionOrders;
                break;
            case OrderWeighingModel:
                page = LocaleCore.DeviceControl.RouteSectionOrdersWeighings;
                break;
            case OrganizationModel:
                page = LocaleCore.DeviceControl.RouteSectionOrganizations;
                break;
            case TaskModel:
                page = LocaleCore.DeviceControl.RouteSectionTaskModules;
                break;
            case TaskTypeModel:
                page = LocaleCore.DeviceControl.RouteSectionTaskTypeModules;
                break;
            case HostModel:
                page = LocaleCore.DeviceControl.RouteSectionHosts;
                break;
            case PluLabelModel:
                page = LocaleCore.DeviceControl.RouteSectionPluLabels;
                break;
            case NomenclatureModel:
                page = LocaleCore.DeviceControl.RouteSectionNomenclatures;
                break;
            case PluObsoleteModel:
                page = LocaleCore.DeviceControl.RouteSectionPlusObsolete;
                break;
            case PluModel:
                page = LocaleCore.DeviceControl.RouteSectionPlus;
                break;
            case PluScaleModel:
                if (Item is PluScaleModel pluScale)
                    page = LocaleCore.DeviceControl.RouteItemScale + $"/{pluScale.Scale.Identity.Id}";
                else
                    page = LocaleCore.DeviceControl.RouteSectionScales;
                break;
            case PrinterResourceModel:
                page = LocaleCore.DeviceControl.RouteSectionPrinterResources;
                break;
            case PrinterModel:
                page = LocaleCore.DeviceControl.RouteSectionPrinters;
                break;
            case PrinterTypeModel:
                page = LocaleCore.DeviceControl.RouteSectionPrinterTypes;
                break;
            case ProductionFacilityModel:
                page = LocaleCore.DeviceControl.RouteSectionProductionFacilities;
                break;
            case ProductSeriesModel:
                page = LocaleCore.DeviceControl.RouteSectionProductSeries;
                break;
            case ScaleModel:
                if (Item is ScaleModel scale)
                    page = LocaleCore.DeviceControl.RouteItemScale + $"/{scale.Identity.Id}";
                else
                    page = LocaleCore.DeviceControl.RouteSectionScales;
                break;
            case TemplateResourceModel:
                page = LocaleCore.DeviceControl.RouteSectionTemplateResources;
                break;
            case TemplateModel:
                page = LocaleCore.DeviceControl.RouteSectionTemplates;
                break;
            case PluWeighingModel:
                page = LocaleCore.DeviceControl.RouteSectionPlusWeighings;
                break;
            case VersionModel:
                page = LocaleCore.DeviceControl.RouteSectionVersions;
                break;
            case WorkShopModel:
                page = LocaleCore.DeviceControl.RouteSectionWorkShops;
                break;
        }
        return page;
    }

    private string GetRouteItemNavigatePage(TableBase? item)
    {
        string page = string.Empty;
        page = item switch
        {
	        AccessModel => LocaleCore.DeviceControl.RouteItemAccess,
	        BarCodeTypeModel => LocaleCore.DeviceControl.RouteItemBarCodeType,
	        ContragentModel => LocaleCore.DeviceControl.RouteItemContragent,
	        HostModel => LocaleCore.DeviceControl.RouteItemHost,
	        LogModel => LocaleCore.DeviceControl.RouteItemLog,
	        LogQuickModel => LocaleCore.DeviceControl.RouteItemLog,
	        LogTypeModel => LocaleCore.DeviceControl.RouteItemLogType,
	        NomenclatureModel => LocaleCore.DeviceControl.RouteItemNomenclature,
	        PluLabelModel => LocaleCore.DeviceControl.RouteItemPluLabel,
	        PluModel => LocaleCore.DeviceControl.RouteItemPlu,
	        PluObsoleteModel => LocaleCore.DeviceControl.RouteItemPluObsolete,
	        PluScaleModel => LocaleCore.DeviceControl.RouteItemPluScale,
	        PluWeighingModel => LocaleCore.DeviceControl.RouteItemPluWeighing,
	        PrinterModel => LocaleCore.DeviceControl.RouteItemPrinter,
	        PrinterResourceModel => LocaleCore.DeviceControl.RouteItemPrinterResource,
	        PrinterTypeModel => LocaleCore.DeviceControl.RouteItemPrinterType,
	        ProductionFacilityModel => LocaleCore.DeviceControl.RouteItemProductionFacility,
	        ScaleModel => LocaleCore.DeviceControl.RouteItemScale,
	        TaskModel => LocaleCore.DeviceControl.RouteItemTaskModule,
	        TaskTypeModel => LocaleCore.DeviceControl.RouteItemTaskTypeModule,
	        TemplateModel => LocaleCore.DeviceControl.RouteItemTemplate,
	        TemplateResourceModel => LocaleCore.DeviceControl.RouteItemTemplateResource,
	        WorkShopModel => LocaleCore.DeviceControl.RouteItemWorkShop,
	        _ => page
        };
        return page;
    }

    private void SetRouteItemNavigate<T>(bool isNewWindow, T? item, SqlTableActionEnum tableAction) where T : DataCore.Sql.Tables.TableBase, new()
    {
        string page = GetRouteItemNavigatePage(Item);
        if (string.IsNullOrEmpty(page))
            return;

        if (!isNewWindow)
            SetRouteItemNavigateCore(item, page, tableAction);
        else
            SetRouteItemNavigateUsingJsRuntime(page);
    }

    private void SetRouteItemNavigateCore<T>(T? item, string page, SqlTableActionEnum tableAction) where T : DataCore.Sql.Tables.TableBase, new()
    {
        switch (tableAction)
        {
            case SqlTableActionEnum.Copy:
            case SqlTableActionEnum.New:
                NavigationManager?.NavigateTo($"{page}/{tableAction}");
                break;
            case SqlTableActionEnum.Edit:
                if (item is null)
                    return;
                switch (item.Identity.Name)
                {
                    case SqlFieldIdentityEnum.Id:
                        NavigationManager?.NavigateTo($"{page}/{item.Identity.Id}");
                        break;
                    case SqlFieldIdentityEnum.Uid:
                        NavigationManager?.NavigateTo($"{page}/{item.Identity.Uid}");
                        break;
                }
                break;
        }
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

    private void SetRouteSectionNavigate(bool isNewWindow)
    {
        string page = GetRouteSectionNavigatePage(Item);
        if (string.IsNullOrEmpty(page))
            return;

        if (!isNewWindow)
        {
            NavigationManager?.NavigateTo(page);
        }
        else
        {
            _ = Task.Run(async () =>
            {
                if (JsRuntime is not null)
                    await JsRuntime.InvokeAsync<object>("open", $"{page}", "_blank").ConfigureAwait(false);
            }).ConfigureAwait(true);
        }
    }

    #endregion
}
