// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Localizations;
using DataCore.Models;
using DataCore.Sql.Core;
using DataCore.Sql.Tables;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

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
                switch (SqlUtils.GetTableSystem(Table.Name))
                {
                    case SqlTableSystemEnum.Default:
                        break;
                    case SqlTableSystemEnum.Accesses:
                        page = LocaleCore.DeviceControl.RouteSectionAccess;
                        break;
                    case SqlTableSystemEnum.Logs:
                        page = LocaleCore.DeviceControl.RouteSectionLogs;
                        break;
                    case SqlTableSystemEnum.LogTypes:
                        page = LocaleCore.DeviceControl.RouteSectionLogTypes;
                        break;
                    case SqlTableSystemEnum.Tasks:
                        page = LocaleCore.DeviceControl.RouteSectionTaskModules;
                        break;
                    case SqlTableSystemEnum.TasksTypes:
                        page = LocaleCore.DeviceControl.RouteSectionTaskTypeModules;
                        break;
                }
                break;
            case TableScaleModel:
                switch (SqlUtils.GetTableScale(Table.Name))
                {
                    case SqlTableScaleEnum.BarCodeTypes:
                        page = LocaleCore.DeviceControl.RouteSectionBarCodeTypes;
                        break;
                    case SqlTableScaleEnum.Contragents:
                        page = LocaleCore.DeviceControl.RouteSectionContragents;
                        break;
                    case SqlTableScaleEnum.Hosts:
                        page = LocaleCore.DeviceControl.RouteSectionHosts;
                        break;
                    case SqlTableScaleEnum.PlusLabels:
                        page = LocaleCore.DeviceControl.RouteSectionPluLabels;
                        break;
                    case SqlTableScaleEnum.Nomenclatures:
                        page = LocaleCore.DeviceControl.RouteSectionNomenclatures;
                        break;
                    case SqlTableScaleEnum.PlusObsolete:
                        page = LocaleCore.DeviceControl.RouteSectionPlusObsolete;
                        break;
                    case SqlTableScaleEnum.Plus:
                        page = LocaleCore.DeviceControl.RouteSectionPlus;
                        break;
                    case SqlTableScaleEnum.PlusScales:
                        if (Item is PluScaleModel pluScale)
                            page = LocaleCore.DeviceControl.RouteItemScale + $"/{pluScale.Scale.Identity.Id}";
                        else
                            page = LocaleCore.DeviceControl.RouteSectionScales;
                        break;
                    case SqlTableScaleEnum.PrintersResources:
                        page = LocaleCore.DeviceControl.RouteSectionPrinterResources;
                        break;
                    case SqlTableScaleEnum.Printers:
                        page = LocaleCore.DeviceControl.RouteSectionPrinters;
                        break;
                    case SqlTableScaleEnum.PrintersTypes:
                        page = LocaleCore.DeviceControl.RouteSectionPrinterTypes;
                        break;
                    case SqlTableScaleEnum.ProductionFacilities:
                        page = LocaleCore.DeviceControl.RouteSectionProductionFacilities;
                        break;
                    case SqlTableScaleEnum.Scales:
                        if (Item is ScaleModel scale)
                            page = LocaleCore.DeviceControl.RouteItemScale + $"/{scale.Identity.Id}";
                        else
                            page = LocaleCore.DeviceControl.RouteSectionScales;
                        break;
                    case SqlTableScaleEnum.TemplatesResources:
                        page = LocaleCore.DeviceControl.RouteSectionTemplateResources;
                        break;
                    case SqlTableScaleEnum.Templates:
                        page = LocaleCore.DeviceControl.RouteSectionTemplates;
                        break;
                    case SqlTableScaleEnum.PlusWeighings:
                        page = LocaleCore.DeviceControl.RouteSectionPlusWeighings;
                        break;
                    case SqlTableScaleEnum.Workshops:
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
            TableSystemModel => SqlUtils.GetTableSystem(Table.Name) switch
            {
                SqlTableSystemEnum.Logs => LocaleCore.DeviceControl.RouteItemLog,
                SqlTableSystemEnum.Accesses => LocaleCore.DeviceControl.RouteItemAccess,
                SqlTableSystemEnum.LogTypes => LocaleCore.DeviceControl.RouteItemLogType,
                SqlTableSystemEnum.Tasks => LocaleCore.DeviceControl.RouteItemTaskModule,
                SqlTableSystemEnum.TasksTypes => LocaleCore.DeviceControl.RouteItemTaskTypeModule,
                _ => page
            },
            TableScaleModel => SqlUtils.GetTableScale(Table.Name) switch
            {
                SqlTableScaleEnum.BarCodeTypes => LocaleCore.DeviceControl.RouteItemBarCodeType,
                SqlTableScaleEnum.Contragents => LocaleCore.DeviceControl.RouteItemContragent,
                SqlTableScaleEnum.Hosts => LocaleCore.DeviceControl.RouteItemHost,
                SqlTableScaleEnum.PlusLabels => LocaleCore.DeviceControl.RouteItemPluLabel,
                SqlTableScaleEnum.Nomenclatures => LocaleCore.DeviceControl.RouteItemNomenclature,
                SqlTableScaleEnum.PlusObsolete => LocaleCore.DeviceControl.RouteItemPluObsolete,
                SqlTableScaleEnum.Plus => LocaleCore.DeviceControl.RouteItemPlu,
                SqlTableScaleEnum.PlusScales => LocaleCore.DeviceControl.RouteItemPluScale,
                SqlTableScaleEnum.PrintersResources => LocaleCore.DeviceControl.RouteItemPrinterResource,
                SqlTableScaleEnum.Printers => LocaleCore.DeviceControl.RouteItemPrinter,
                SqlTableScaleEnum.PrintersTypes => LocaleCore.DeviceControl.RouteItemPrinterType,
                SqlTableScaleEnum.ProductionFacilities => LocaleCore.DeviceControl.RouteItemProductionFacility,
                SqlTableScaleEnum.Scales => LocaleCore.DeviceControl.RouteItemScale,
                SqlTableScaleEnum.TemplatesResources => LocaleCore.DeviceControl.RouteItemTemplateResource,
                SqlTableScaleEnum.Templates => LocaleCore.DeviceControl.RouteItemTemplate,
                SqlTableScaleEnum.PlusWeighings => LocaleCore.DeviceControl.RouteItemPluWeighing,
                SqlTableScaleEnum.Workshops => LocaleCore.DeviceControl.RouteItemWorkShop,
                _ => page
            },
            _ => page
        };
        return page;
    }

    private void SetRouteItemNavigate<T>(bool isNewWindow, T? item, SqlTableActionEnum tableAction) where T : TableBaseModel, new()
    {
        string page = GetRouteItemNavigatePage();
        if (string.IsNullOrEmpty(page))
            return;

        if (!isNewWindow)
            SetRouteItemNavigateCore(item, page, tableAction);
        else
            SetRouteItemNavigateUsingJsRuntime(page);
    }

    private void SetRouteItemNavigateCore<T>(T? item, string page, SqlTableActionEnum tableAction) where T : TableBaseModel, new()
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
