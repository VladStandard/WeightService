// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Localizations;
using DataCore.Models;
using DataCore.Sql.Tables;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using static DataCore.ShareEnums;

namespace BlazorCore.Models;

public partial class RazorPageBase : LayoutComponentBase
{
    #region Public and private methods - Routes

    protected static string GetRoutePath(string uriItemRoute, TableBaseModel? item, long? id) =>
        item is null || id is null ? string.Empty : $"{uriItemRoute}/{id}";

    protected static string GetRoutePath(string uriItemRoute, TableBaseModel? item, Guid? uid) =>
        item is null || uid is null ? string.Empty : $"{uriItemRoute}/{uid}";

    private string GetRouteSectionNavigatePage()
    {
        string page = string.Empty;
        switch (Table)
        {
            case TableSystemModel:
                switch (ProjectsEnums.GetTableSystem(Table.Name))
                {
                    case ProjectsEnums.TableSystem.Default:
                        break;
                    case ProjectsEnums.TableSystem.Accesses:
                        page = LocaleCore.DeviceControl.RouteSectionAccess;
                        break;
                    case ProjectsEnums.TableSystem.Logs:
                        page = LocaleCore.DeviceControl.RouteSectionLogs;
                        break;
                    case ProjectsEnums.TableSystem.LogTypes:
                        page = LocaleCore.DeviceControl.RouteSectionLogTypes;
                        break;
                    case ProjectsEnums.TableSystem.Tasks:
                        page = LocaleCore.DeviceControl.RouteSectionTaskModules;
                        break;
                    case ProjectsEnums.TableSystem.TasksTypes:
                        page = LocaleCore.DeviceControl.RouteSectionTaskTypeModules;
                        break;
                }
                break;
            case TableScaleModel:
                switch (ProjectsEnums.GetTableScale(Table.Name))
                {
                    case ProjectsEnums.TableScale.BarCodeTypes:
                        page = LocaleCore.DeviceControl.RouteSectionBarCodeTypes;
                        break;
                    case ProjectsEnums.TableScale.Contragents:
                        page = LocaleCore.DeviceControl.RouteSectionContragents;
                        break;
                    case ProjectsEnums.TableScale.Hosts:
                        page = LocaleCore.DeviceControl.RouteSectionHosts;
                        break;
                    case ProjectsEnums.TableScale.PlusLabels:
                        page = LocaleCore.DeviceControl.RouteSectionPluLabels;
                        break;
                    case ProjectsEnums.TableScale.Nomenclatures:
                        page = LocaleCore.DeviceControl.RouteSectionNomenclatures;
                        break;
                    case ProjectsEnums.TableScale.PlusObsolete:
                        page = LocaleCore.DeviceControl.RouteSectionPlusObsolete;
                        break;
                    case ProjectsEnums.TableScale.Plus:
                        page = LocaleCore.DeviceControl.RouteSectionPlus;
                        break;
                    case ProjectsEnums.TableScale.PlusScales:
                        if (Item is PluScaleModel pluScale)
                            page = LocaleCore.DeviceControl.RouteItemScale + $"/{pluScale.Scale.Identity.Id}";
                        else
                            page = LocaleCore.DeviceControl.RouteSectionScales;
                        break;
                    case ProjectsEnums.TableScale.PrintersResources:
                        page = LocaleCore.DeviceControl.RouteSectionPrinterResources;
                        break;
                    case ProjectsEnums.TableScale.Printers:
                        page = LocaleCore.DeviceControl.RouteSectionPrinters;
                        break;
                    case ProjectsEnums.TableScale.PrintersTypes:
                        page = LocaleCore.DeviceControl.RouteSectionPrinterTypes;
                        break;
                    case ProjectsEnums.TableScale.ProductionFacilities:
                        page = LocaleCore.DeviceControl.RouteSectionProductionFacilities;
                        break;
                    case ProjectsEnums.TableScale.Scales:
                        if (Item is ScaleModel scale)
                            page = LocaleCore.DeviceControl.RouteItemScale + $"/{scale.Identity.Id}";
                        else
                            page = LocaleCore.DeviceControl.RouteSectionScales;
                        break;
                    case ProjectsEnums.TableScale.TemplatesResources:
                        page = LocaleCore.DeviceControl.RouteSectionTemplateResources;
                        break;
                    case ProjectsEnums.TableScale.Templates:
                        page = LocaleCore.DeviceControl.RouteSectionTemplates;
                        break;
                    case ProjectsEnums.TableScale.PlusWeighings:
                        page = LocaleCore.DeviceControl.RouteSectionPlusWeighings;
                        break;
                    case ProjectsEnums.TableScale.Workshops:
                        page = LocaleCore.DeviceControl.RouteSectionWorkShops;
                        break;
                }
                break;
            case TableDwhModel:
                break;
        }
        return page;
    }

    private string GetRouteItemNavigatePage()
    {
        string page = string.Empty;
        if (string.IsNullOrEmpty(Table.Name))
            if (ParentRazor is not null)
                Table = ParentRazor.Table;
        page = Table switch
        {
	        TableSystemModel => ProjectsEnums.GetTableSystem(Table.Name) switch
	        {
		        ProjectsEnums.TableSystem.Logs => LocaleCore.DeviceControl.RouteItemLog,
		        ProjectsEnums.TableSystem.Accesses => LocaleCore.DeviceControl.RouteItemAccess,
		        ProjectsEnums.TableSystem.LogTypes => LocaleCore.DeviceControl.RouteItemLogType,
		        ProjectsEnums.TableSystem.Tasks => LocaleCore.DeviceControl.RouteItemTaskModule,
		        ProjectsEnums.TableSystem.TasksTypes => LocaleCore.DeviceControl.RouteItemTaskTypeModule,
		        _ => page
	        },
	        TableScaleModel => ProjectsEnums.GetTableScale(Table.Name) switch
	        {
		        ProjectsEnums.TableScale.BarCodeTypes => LocaleCore.DeviceControl.RouteItemBarCodeType,
		        ProjectsEnums.TableScale.Contragents => LocaleCore.DeviceControl.RouteItemContragent,
		        ProjectsEnums.TableScale.Hosts => LocaleCore.DeviceControl.RouteItemHost,
		        ProjectsEnums.TableScale.PlusLabels => LocaleCore.DeviceControl.RouteItemPluLabel,
		        ProjectsEnums.TableScale.Nomenclatures => LocaleCore.DeviceControl.RouteItemNomenclature,
		        ProjectsEnums.TableScale.PlusObsolete => LocaleCore.DeviceControl.RouteItemPluObsolete,
		        ProjectsEnums.TableScale.Plus => LocaleCore.DeviceControl.RouteItemPlu,
		        ProjectsEnums.TableScale.PlusScales => LocaleCore.DeviceControl.RouteItemPluScale,
		        ProjectsEnums.TableScale.PrintersResources => LocaleCore.DeviceControl.RouteItemPrinterResource,
		        ProjectsEnums.TableScale.Printers => LocaleCore.DeviceControl.RouteItemPrinter,
		        ProjectsEnums.TableScale.PrintersTypes => LocaleCore.DeviceControl.RouteItemPrinterType,
		        ProjectsEnums.TableScale.ProductionFacilities => LocaleCore.DeviceControl.RouteItemProductionFacility,
		        ProjectsEnums.TableScale.Scales => LocaleCore.DeviceControl.RouteItemScale,
		        ProjectsEnums.TableScale.TemplatesResources => LocaleCore.DeviceControl.RouteItemTemplateResource,
		        ProjectsEnums.TableScale.Templates => LocaleCore.DeviceControl.RouteItemTemplate,
		        ProjectsEnums.TableScale.PlusWeighings => LocaleCore.DeviceControl.RouteItemPluWeighing,
		        ProjectsEnums.TableScale.Workshops => LocaleCore.DeviceControl.RouteItemWorkShop,
		        _ => page
	        },
	        _ => page
        };
        return page;
    }

    private void SetRouteItemNavigate<T>(bool isNewWindow, T? item, DbTableAction tableAction) where T : TableBaseModel, new()
    {
        string page = GetRouteItemNavigatePage();
        if (string.IsNullOrEmpty(page))
            return;

        if (!isNewWindow)
            SetRouteItemNavigateCore(item, page, tableAction);
        else
            SetRouteItemNavigateUsingJsRuntime(page);
    }

    private void SetRouteItemNavigateCore<T>(T? item, string page, DbTableAction tableAction) where T : TableBaseModel, new()
    {
        switch (tableAction)
        {
            case DbTableAction.Copy:
            case DbTableAction.New:
                NavigationManager?.NavigateTo($"{page}/{tableAction}");
                break;
            case DbTableAction.Edit:
                if (item is null)
                    return;
                switch (item.Identity.Name)
                {
                    case ColumnName.Id:
                        NavigationManager?.NavigateTo($"{page}/{item.Identity.Id}");
                        break;
                    case ColumnName.Uid:
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
        string page = GetRouteSectionNavigatePage();
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
