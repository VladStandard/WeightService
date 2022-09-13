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

    protected string GetColumnIdentityName<T>() where T : SqlTableBase, new()
    {
	    T item = new();
	    string page = GetRouteItemPath(item);
	    if (!string.IsNullOrEmpty(page))
		    return item.Identity.Name switch
		    {
			    SqlFieldIdentityEnum.Id => LocaleCore.Table.IdentityId,
			    SqlFieldIdentityEnum.Uid => LocaleCore.Table.IdentityUid,
			    _ => LocaleCore.Table.Identity
		    };
	    return string.Empty;
    }

    public string GetRouteItemPath(SqlTableBase? item)
	{
		if (item is not null)
		{
            string page = GetRouteItemPath<SqlTableBase>(item);
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

    public string GetRouteItemPath<T>() where T : SqlTableBase, new() => GetRouteItemPath(new T());

    private string GetRouteItemPath<T>(T? item) where T : SqlTableBase, new() => item switch
	{
		AccessModel => LocaleCore.DeviceControl.RouteItemAccess,
		AppModel => LocaleCore.DeviceControl.RouteItemApps,
		BarCodeModel => LocaleCore.DeviceControl.RouteItemBarCodeType,
		BarCodeTypeModel => LocaleCore.DeviceControl.RouteItemBarCodeType,
		ContragentModel => LocaleCore.DeviceControl.RouteItemContragent,
		HostModel => LocaleCore.DeviceControl.RouteItemHost,
		LogModel => LocaleCore.DeviceControl.RouteItemLog,
		LogQuickModel => LocaleCore.DeviceControl.RouteItemLog,
		LogTypeModel => LocaleCore.DeviceControl.RouteItemLogType,
		NomenclatureModel => LocaleCore.DeviceControl.RouteItemNomenclature,
		OrderModel => LocaleCore.DeviceControl.RouteItemOrder,
		OrderWeighingModel => LocaleCore.DeviceControl.RouteItemOrderWeighing,
		OrganizationModel => LocaleCore.DeviceControl.RouteItemOrganization,
		PluLabelModel => LocaleCore.DeviceControl.RouteItemPluLabel,
		PluModel => LocaleCore.DeviceControl.RouteItemPlu,
		//PluObsoleteModel => LocaleCore.DeviceControl.RouteItemPluObsolete,
		PluScaleModel => LocaleCore.DeviceControl.RouteItemPluScale,
		PluWeighingModel => LocaleCore.DeviceControl.RouteItemPluWeighing,
		PrinterModel => LocaleCore.DeviceControl.RouteItemPrinter,
		PrinterResourceModel => LocaleCore.DeviceControl.RouteItemPrinterResource,
		PrinterTypeModel => LocaleCore.DeviceControl.RouteItemPrinterType,
		ProductionFacilityModel => LocaleCore.DeviceControl.RouteItemProductionFacility,
		ProductSeriesModel => LocaleCore.DeviceControl.RouteItemProductSerie,
		ScaleModel => LocaleCore.DeviceControl.RouteItemScale,
		TaskModel => LocaleCore.DeviceControl.RouteItemTaskModule,
		TaskTypeModel => LocaleCore.DeviceControl.RouteItemTaskTypeModule,
		TemplateModel => LocaleCore.DeviceControl.RouteItemTemplate,
		TemplateResourceModel => LocaleCore.DeviceControl.RouteItemTemplateResource,
		VersionModel => LocaleCore.DeviceControl.RouteItemVersion,
		WorkShopModel => LocaleCore.DeviceControl.RouteItemWorkShop,
		_ => string.Empty
	};

    public string GetRouteSectionPath<T>(T? item) where T : SqlTableBase, new()
    {
        return item switch
        {
            AccessModel => LocaleCore.DeviceControl.RouteSectionAccess,
            AppModel => LocaleCore.DeviceControl.RouteSectionApps,
            BarCodeModel => LocaleCore.DeviceControl.RouteSectionBarCodes,
            BarCodeTypeModel => LocaleCore.DeviceControl.RouteSectionBarCodeTypes,
            ContragentModel => LocaleCore.DeviceControl.RouteSectionContragents,
            LogModel => LocaleCore.DeviceControl.RouteSectionLogs,
            LogTypeModel => LocaleCore.DeviceControl.RouteSectionLogTypes,
            LogQuickModel => LocaleCore.DeviceControl.RouteSectionLogs,
            OrderModel => LocaleCore.DeviceControl.RouteSectionOrders,
            OrderWeighingModel => LocaleCore.DeviceControl.RouteSectionOrdersWeighings,
            OrganizationModel => LocaleCore.DeviceControl.RouteSectionOrganizations,
            TaskModel => LocaleCore.DeviceControl.RouteSectionTaskModules,
            TaskTypeModel => LocaleCore.DeviceControl.RouteSectionTaskTypeModules,
            HostModel => LocaleCore.DeviceControl.RouteSectionHosts,
            PluLabelModel => LocaleCore.DeviceControl.RouteSectionPluLabels,
            NomenclatureModel => LocaleCore.DeviceControl.RouteSectionNomenclatures,
            //PluObsoleteModel => LocaleCore.DeviceControl.RouteSectionPlusObsolete,
            PluModel => LocaleCore.DeviceControl.RouteSectionPlus,
            PluScaleModel => LocaleCore.DeviceControl.RouteSectionScales,
            PrinterResourceModel => LocaleCore.DeviceControl.RouteSectionPrinterResources,
            PrinterModel => LocaleCore.DeviceControl.RouteSectionPrinters,
            PrinterTypeModel => LocaleCore.DeviceControl.RouteSectionPrinterTypes,
            ProductionFacilityModel => LocaleCore.DeviceControl.RouteSectionProductionFacilities,
            ProductSeriesModel => LocaleCore.DeviceControl.RouteSectionProductSeries,
            ScaleModel => LocaleCore.DeviceControl.RouteSectionScales,
            TemplateResourceModel => LocaleCore.DeviceControl.RouteSectionTemplateResources,
            TemplateModel => LocaleCore.DeviceControl.RouteSectionTemplates,
            PluWeighingModel => LocaleCore.DeviceControl.RouteSectionPlusWeighings,
            VersionModel => LocaleCore.DeviceControl.RouteSectionVersions,
            WorkShopModel => LocaleCore.DeviceControl.RouteSectionWorkShops,
            _ => string.Empty
        };
    }

    public string GetRouteSectionPath<T>() where T : SqlTableBase, new() => GetRouteSectionPath(new T());

    protected string GetRouteItemPath(string uriItem, SqlTableBase? item, long? id) =>
        item is null || id is null ? string.Empty : $"{uriItem}/{id}";

    protected string GetRouteItemPath(string uriItem, SqlTableBase? item, Guid? uid) =>
        item is null || uid is null ? string.Empty : $"{uriItem}/{uid}";

    private void SetRouteItemNavigate<T>(T? item, SqlTableActionEnum tableAction) where T : DataCore.Sql.Tables.SqlTableBase, new()
    {
        string page = GetRouteItemPath<SqlTableBase>(Item);
        if (string.IsNullOrEmpty(page))
            return;

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

    private void SetRouteSectionNavigate()
	{
        string page = GetRouteSectionPath(Item);
        if (string.IsNullOrEmpty(page))
            return;

        NavigationManager?.NavigateTo(page);
    }

    #endregion
}
