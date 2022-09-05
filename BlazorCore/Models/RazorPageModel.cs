// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Localizations;
using DataCore.Models;
using DataCore.Protocols;
using DataCore.Sql.Core;
using DataCore.Sql.Models;
using DataCore.Sql.Tables;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Net.Sockets;
using static DataCore.ShareEnums;
using Environment = System.Environment;

namespace BlazorCore.Models;

public partial class RazorPageModel : LayoutComponentBase
{
    #region Public and private methods

    public void OnChangeCheckBox(object value, string name)
    {
        RunActionsSafe(nameof(OnChangeCheckBox), LocaleCore.Dialog.DialogResultFail,
            () =>
            {
                switch (name)
                {
                    case nameof(IsShowMarked):
                        if (value is bool isShowMarkedItems)
                            IsShowMarked = isShowMarkedItems;
                        break;
                    case nameof(IsShowOnlyTop):
                        if (value is bool isShowTOp100)
                            IsShowMarked = isShowTOp100;
                        break;
                }
            });

        ParentRazor?.OnChange();
    }

    public void OnItemValueChange(TableModel? item, string? filterName, object? value)
    {
        RunActionsSafe(nameof(OnItemValueChange), LocaleCore.Dialog.DialogResultFail,
            () =>
            {
                switch (item)
                {
                    case AccessModel access:
                        OnItemValueChangeAccess(filterName, value, access);
                        break;
                    case PrinterModel printer:
                        OnItemValueChangePrinter(filterName, value, printer);
                        break;
                    case PrinterResourceModel printerResource:
                        OnItemValueChangePrinterResource(filterName, value, printerResource);
                        break;
                    case PluObsoleteModel pluObsolete:
                        OnItemValueChangePlu(filterName, value, pluObsolete);
                        break;
                    case ScaleModel scale:
                        OnItemValueChangeScale(filterName, value, scale);
                        break;
                    case TemplateModel template:
                        OnItemValueChangeTemplate(filterName, value, template);
                        break;
                    case WorkShopModel workShop:
                        OnItemValueChangeWorkShop(filterName, value, workShop);
                        break;
                }
            });

        ParentRazor?.OnChange();
    }

    private static void OnItemValueChangeAccess(string? filterName, object? value, AccessModel access)
    {
        if (filterName == nameof(access.Rights) && value is AccessRights rights)
        {
            access.Rights = (byte)rights;
        }
    }

    private void OnItemValueChangePrinter(string? filterName, object? value, PrinterModel printer)
    {
        if (filterName == nameof(printer.PrinterType) && value is long printerTypeId)
        {
            printer.PrinterType = AppSettings.DataAccess.GetItemById<PrinterTypeModel>(printerTypeId) ?? new();
        }
    }

    private void OnItemValueChangePrinterResource(string? filterName, object? value, PrinterResourceModel printerResource)
    {
        if (filterName == nameof(printerResource.Printer) && value is long printerId)
        {
            printerResource.Printer = AppSettings.DataAccess.GetItemById<PrinterModel>(printerId) ?? new();
        }
        if (filterName == nameof(printerResource.Resource) && value is long resourceId)
        {
            printerResource.Resource = AppSettings.DataAccess.GetItemById<TemplateResourceModel>(resourceId) ?? new();
        }
    }

    private void OnItemValueChangePlu(string? filterName, object? value, PluObsoleteModel pluObsolete)
    {
        if (filterName == nameof(pluObsolete.Nomenclature) && value is long nomenclatureId)
        {
            pluObsolete.Nomenclature = AppSettings.DataAccess.GetItemById<NomenclatureModel>(nomenclatureId) ?? new();
        }
        if (filterName == nameof(pluObsolete.Scale) && value is long scaleId)
        {
            pluObsolete.Scale = AppSettings.DataAccess.GetItemById<ScaleModel>(scaleId) ?? new();
        }
        if (filterName == nameof(pluObsolete.Template) && value is long templateId)
        {
            pluObsolete.Template = AppSettings.DataAccess.GetItemById<TemplateModel>(templateId) ?? new();
        }
    }

    private void OnItemValueChangeScale(string? filterName, object? value, ScaleModel scale)
    {
        if (filterName == nameof(TableModel.Identity.Id) && value is long id)
        {
            scale = AppSettings.DataAccess.GetItemById<ScaleModel>(id) ?? new();
        }
        if (filterName == nameof(ScaleModel.DeviceComPort) && value is string deviceComPort)
        {
            scale.DeviceComPort = deviceComPort;
        }
        if (filterName == nameof(ScaleModel.Host) && value is long hostId)
        {
            scale.Host = AppSettings.DataAccess.GetItemById<HostModel>(hostId);
        }
        if (filterName == nameof(ScaleModel.TemplateDefault) && value is long templateDefaultId)
        {
            scale.TemplateDefault = AppSettings.DataAccess.GetItemById<TemplateModel>(templateDefaultId);
        }
        if (filterName == nameof(ScaleModel.TemplateSeries) && value is long templateSeriesId)
        {
            scale.TemplateSeries = AppSettings.DataAccess.GetItemById<TemplateModel>(templateSeriesId);
        }
        if (filterName == nameof(ScaleModel.PrinterMain) && value is long printerId)
        {
            scale.PrinterMain = AppSettings.DataAccess.GetItemById<PrinterModel>(printerId);
        }
        if (filterName == nameof(ScaleModel.WorkShop) && value is long workShopId)
        {
            scale.WorkShop = AppSettings.DataAccess.GetItemById<WorkShopModel>(workShopId);
        }
    }

    private static void OnItemValueChangeTemplate(string? filterName, object? value, TemplateModel template)
    {
        if (filterName == nameof(template.CategoryId) && value is string categoryId)
        {
            template.CategoryId = categoryId;
        }
    }

    private void OnItemValueChangeWorkShop(string? filterName, object? value, WorkShopModel workshop)
    {
        if (filterName == nameof(workshop.ProductionFacility) && value is int productionFacilityId)
        {
            workshop.ProductionFacility = AppSettings.DataAccess.GetItemById<ProductionFacilityModel>(productionFacilityId) ?? new();
        }
    }

    public async Task ItemSelectAsync(TableModel item)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        RunActionsSafe(nameof(ItemSelectAsync), LocaleCore.Dialog.DialogResultFail,
            () =>
            {
                ItemSelect(item);
            });
    }

    private void ItemSelect(TableModel item)
    {
        if (Item is not null && !Item.Equals(item))
            Item = item;
        if (IdentityId != item.Identity.Id)
            IdentityId = item.Identity.Id;
        if (IdentityUid != item.Identity.Uid)
            IdentityUid = item.Identity.Uid;
    }

    protected static string GetPath(string uriItemRoute, TableModel? item, long? id) =>
        item is null || id is null ? string.Empty : $"{uriItemRoute}/{id}";

    protected static string GetPath(string uriItemRoute, TableModel? item, Guid? uid) =>
        item is null || uid is null ? string.Empty : $"{uriItemRoute}/{uid}";

    #endregion

    #region Public and private methods - Actions

    private void RouteItemNavigate<T>(bool isNewWindow, T? item, DbTableAction tableAction) where T : TableModel, new()
    {
        string page = RouteItemNavigatePage();
        if (string.IsNullOrEmpty(page))
            return;

        if (!isNewWindow)
            RouteItemNavigateCore(item, page, tableAction);
        else
            RouteItemNavigateUsingJsRuntime(page);
    }

    private string RouteItemNavigatePage()
    {
        string page = string.Empty;
        if (Table is null || string.IsNullOrEmpty(Table.Name))
            if (ParentRazor is not null)
                Table = ParentRazor.Table;
        switch (Table)
        {
            case TableSystemModel:
                switch (ProjectsEnums.GetTableSystem(Table.Name))
                {
                    case ProjectsEnums.TableSystem.Default:
                        break;
                    case ProjectsEnums.TableSystem.Logs:
                        page = LocaleCore.DeviceControl.RouteItemLog;
                        break;
                    case ProjectsEnums.TableSystem.Accesses:
                        page = LocaleCore.DeviceControl.RouteItemAccess;
                        break;
                    case ProjectsEnums.TableSystem.LogTypes:
                        page = LocaleCore.DeviceControl.RouteItemLogType;
                        break;
                    case ProjectsEnums.TableSystem.Tasks:
                        page = LocaleCore.DeviceControl.RouteItemTaskModule;
                        break;
                    case ProjectsEnums.TableSystem.TasksTypes:
                        page = LocaleCore.DeviceControl.RouteItemTaskTypeModule;
                        break;
                }
                break;
            case TableScaleModel:
                switch (ProjectsEnums.GetTableScale(Table.Name))
                {
                    case ProjectsEnums.TableScale.BarCodeTypes:
                        page = LocaleCore.DeviceControl.RouteItemBarCodeType;
                        break;
                    case ProjectsEnums.TableScale.Contragents:
                        page = LocaleCore.DeviceControl.RouteItemContragent;
                        break;
                    case ProjectsEnums.TableScale.Hosts:
                        page = LocaleCore.DeviceControl.RouteItemHost;
                        break;
                    case ProjectsEnums.TableScale.PlusLabels:
                        page = LocaleCore.DeviceControl.RouteItemPluLabel;
                        break;
                    case ProjectsEnums.TableScale.Nomenclatures:
                        page = LocaleCore.DeviceControl.RouteItemNomenclature;
                        break;
                    case ProjectsEnums.TableScale.PlusObsolete:
                        page = LocaleCore.DeviceControl.RouteItemPluObsolete;
                        break;
                    case ProjectsEnums.TableScale.Plus:
                        page = LocaleCore.DeviceControl.RouteItemPlu;
                        break;
                    case ProjectsEnums.TableScale.PlusScales:
                        page = LocaleCore.DeviceControl.RouteItemPluScale;
                        break;
                    case ProjectsEnums.TableScale.PrintersResources:
                        page = LocaleCore.DeviceControl.RouteItemPrinterResource;
                        break;
                    case ProjectsEnums.TableScale.Printers:
                        page = LocaleCore.DeviceControl.RouteItemPrinter;
                        break;
                    case ProjectsEnums.TableScale.PrintersTypes:
                        page = LocaleCore.DeviceControl.RouteItemPrinterType;
                        break;
                    case ProjectsEnums.TableScale.ProductionFacilities:
                        page = LocaleCore.DeviceControl.RouteItemProductionFacility;
                        break;
                    case ProjectsEnums.TableScale.Scales:
                        page = LocaleCore.DeviceControl.RouteItemScale;
                        break;
                    case ProjectsEnums.TableScale.TemplatesResources:
                        page = LocaleCore.DeviceControl.RouteItemTemplateResource;
                        break;
                    case ProjectsEnums.TableScale.Templates:
                        page = LocaleCore.DeviceControl.RouteItemTemplate;
                        break;
                    case ProjectsEnums.TableScale.PlusWeighings:
                        page = LocaleCore.DeviceControl.RouteItemPluWeighing;
                        break;
                    case ProjectsEnums.TableScale.Workshops:
                        page = LocaleCore.DeviceControl.RouteItemWorkShop;
                        break;
                }
                break;
        }
        return page;
    }

    private void RouteItemNavigateCore<T>(T? item, string page, DbTableAction tableAction) where T : TableModel, new()
    {
        switch (tableAction)
        {
            case DbTableAction.Copy:
            case DbTableAction.New:
                NavigationManager.NavigateTo($"{page}/{tableAction}");
                break;
            case DbTableAction.Edit:
                if (item is null)
                    return;
                switch (item.Identity.Name)
                {
                    case ColumnName.Id:
                        NavigationManager.NavigateTo($"{page}/{item.Identity.Id}");
                        break;
                    case ColumnName.Uid:
                        NavigationManager.NavigateTo($"{page}/{item.Identity.Uid}");
                        break;
                }
                break;
        }
    }

    private void RouteItemNavigateUsingJsRuntime(string page)
    {
        _ = Task.Run(async () =>
        {
            if (IdentityUid is not null && IdentityUid != Guid.Empty)
                await JsRuntime.InvokeAsync<object>("open", $"{page}/{IdentityUid}", "_blank").ConfigureAwait(false);
            else if (IdentityId is not null && JsRuntime is not null)
                await JsRuntime.InvokeAsync<object>("open", $"{page}/{IdentityId}", "_blank").ConfigureAwait(false);
        }).ConfigureAwait(true);
    }

    public void RouteSectionNavigateToRoot()
    {
        NavigationManager.NavigateTo(LocaleCore.DeviceControl.RouteSystemRoot);
    }

    private void RouteSectionNavigate(bool isNewWindow)
    {
        string page = RouteSectionNavigatePage();
        if (string.IsNullOrEmpty(page))
            return;

        if (!isNewWindow)
        {
            NavigationManager.NavigateTo(page);
        }
        else
        {
            _ = Task.Run(async () =>
            {
                await JsRuntime.InvokeAsync<object>("open", $"{page}", "_blank").ConfigureAwait(false);
            }).ConfigureAwait(true);
        }
    }

    private string RouteSectionNavigatePage()
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

    public async Task ItemCancelAsync()
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        RunActionsSafe(LocaleCore.Dialog.DialogResultSuccess, LocaleCore.Dialog.DialogResultFail,
            () =>
            {
                RouteSectionNavigate(false);
            });
    }

    private string GetQuestionAdd()
    {
        switch (ParentRazor?.Item?.Identity.Name)
        {
            case ColumnName.Id:
                return LocaleCore.Dialog.DialogQuestion + Environment.NewLine +
                       $"{nameof(ParentRazor.Item.Identity.Id)}: {ParentRazor.Item.Identity.Id}";
            case ColumnName.Uid:
                return LocaleCore.Dialog.DialogQuestion + Environment.NewLine +
                       $"{nameof(ParentRazor.Item.Identity.Uid)}: {ParentRazor.Item.Identity.Uid}";
        }
        return string.Empty;
    }

    private void ItemSystemSave(ProjectsEnums.TableSystem tableSystem)
    {
        switch (tableSystem)
        {
            case ProjectsEnums.TableSystem.Accesses:
                ItemSaveCheck.Access(NotificationService, (AccessModel?)ParentRazor?.Item, DbTableAction.Save);
                break;
            case ProjectsEnums.TableSystem.Tasks:
                ItemSaveCheck.Task(NotificationService, (TaskModel?)ParentRazor?.Item, DbTableAction.Save);
                break;
            case ProjectsEnums.TableSystem.TasksTypes:
                ItemSaveCheck.TaskType(NotificationService, (TaskTypeModel?)ParentRazor?.Item, DbTableAction.Save);
                break;
        }
    }

    private void ItemScaleSave(ProjectsEnums.TableScale tableScale)
    {
        switch (tableScale)
        {
            case ProjectsEnums.TableScale.BarCodeTypes:
                ItemSaveCheck.BarcodeType(NotificationService, (BarCodeTypeModel?)ParentRazor?.Item, DbTableAction.Save);
                break;
            case ProjectsEnums.TableScale.Contragents:
                ItemSaveCheck.Contragent(NotificationService, (ContragentModel?)ParentRazor?.Item, DbTableAction.Save);
                break;
            case ProjectsEnums.TableScale.Hosts:
                ItemSaveCheck.Host(NotificationService, (HostModel?)ParentRazor?.Item, DbTableAction.Save);
                break;
            case ProjectsEnums.TableScale.Nomenclatures:
                ItemSaveCheck.Nomenclature(NotificationService, (NomenclatureModel?)ParentRazor?.Item, DbTableAction.Save);
                break;
            case ProjectsEnums.TableScale.PlusObsolete:
                ItemSaveCheck.PluObsolete(NotificationService, (PluObsoleteModel?)ParentRazor?.Item, DbTableAction.Save);
                break;
            case ProjectsEnums.TableScale.Plus:
                ItemSaveCheck.Plu(NotificationService, (PluModel?)ParentRazor?.Item, DbTableAction.Save);
                break;
            case ProjectsEnums.TableScale.PlusScales:
                if (ParentRazor?.Items is not null)
                {
                    List<PluScaleModel> pluScales = ParentRazor.Items.Cast<PluScaleModel>().ToList();
                    foreach (PluScaleModel pluScale in pluScales)
                    {
                        ItemSaveCheck.PluScale(NotificationService, pluScale, DbTableAction.Save);
                    }
                }
                else if (ParentRazor?.Item is not null)
                {
                    ItemSaveCheck.PluScale(NotificationService, (PluScaleModel?)ParentRazor?.Item, DbTableAction.Save);
                }
                break;
            case ProjectsEnums.TableScale.PrintersResources:
                ItemSaveCheck.PrinterResource(NotificationService, (PrinterResourceModel?)ParentRazor?.Item, DbTableAction.Save);
                break;
            case ProjectsEnums.TableScale.Printers:
                ItemSaveCheck.Printer(NotificationService, (PrinterModel?)ParentRazor?.Item, DbTableAction.Save);
                break;
            case ProjectsEnums.TableScale.PrintersTypes:
                ItemSaveCheck.PrinterType(NotificationService, (PrinterTypeModel?)ParentRazor?.Item, DbTableAction.Save);
                break;
            case ProjectsEnums.TableScale.ProductionFacilities:
                ItemSaveCheck.ProductionFacility(NotificationService, (ProductionFacilityModel?)ParentRazor?.Item, DbTableAction.Save);
                break;
            case ProjectsEnums.TableScale.Scales:
                ItemSaveCheck.Scale(NotificationService, Item, DbTableAction.Save);
                break;
            case ProjectsEnums.TableScale.Templates:
                ItemSaveCheck.Template(NotificationService, (TemplateModel?)ParentRazor?.Item, ParentRazor?.TableAction);
                break;
            case ProjectsEnums.TableScale.TemplatesResources:
                ItemSaveCheck.TemplateResource(NotificationService, (TemplateResourceModel?)ParentRazor?.Item, ParentRazor?.TableAction);
                break;
            case ProjectsEnums.TableScale.Workshops:
                ItemSaveCheck.Workshop(NotificationService, (WorkShopModel?)ParentRazor?.Item, DbTableAction.Save);
                break;
        }
    }

    public async Task ItemSaveAsync()
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        RunActionsWithQeustion(LocaleCore.Table.TableSave, LocaleCore.Dialog.DialogResultSuccess,
            LocaleCore.Dialog.DialogResultFail, GetQuestionAdd(),
            () =>
            {
                switch (Table)
                {
                    case TableSystemModel:
                        ItemSystemSave(ProjectsEnums.GetTableSystem(Table.Name));
                        break;
                    case TableScaleModel:
                        ItemScaleSave(ProjectsEnums.GetTableScale(Table.Name));
                        break;
                }
                RouteSectionNavigate(false);
            });

        ParentRazor?.OnChange();
    }

    public async Task ActionNewAsync(UserSettingsModel? userSettings, bool isNewWindow, bool isParentRazor)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (userSettings is null || !userSettings.AccessRightsIsWrite)
            return;

        RunActionsSafe(string.Empty, LocaleCore.Dialog.DialogResultFail,
            () =>
            {
                throw new NotImplementedException("Fix here!");
                // Uncomment here.
                //item = new();
                //Identity.Id = null;
                //IdentityUid = null;
                //RouteItemNavigate(isNewWindow, item, DbTableAction.New);
            });
    }

    public async Task ActionCopyAsync(UserSettingsModel? userSettings, bool isNewWindow, bool isParentRazor)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (userSettings is null || !userSettings.AccessRightsIsWrite)
            return;

        RunActionsSafe(string.Empty, LocaleCore.Dialog.DialogResultFail,
            () =>
            {
                RouteItemNavigate(isNewWindow, isParentRazor ? ParentRazor?.Item : Item, DbTableAction.Copy);
            });

        ParentRazor?.OnChange();
    }

    public async Task ActionEditAsync(UserSettingsModel? userSettings, bool isNewWindow, bool isParentRazor)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (userSettings is null || !userSettings.AccessRightsIsWrite)
            return;

        RunActionsSafe(string.Empty, LocaleCore.Dialog.DialogResultFail,
            () =>
            {
                RouteItemNavigate(isNewWindow, isParentRazor ? ParentRazor?.Item : Item, DbTableAction.Edit);
                InvokeAsync(StateHasChanged);
            });
    }

    protected async Task ActionPluScalePlusClickAsync(UserSettingsModel? userSettings, PluScaleModel pluScale, List<PluScaleModel> pluScales)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (userSettings is null || !userSettings.AccessRightsIsWrite)
            return;

        //foreach (PluScaleModel item in pluScales)
        //{
        // if (item.Identity.Uid.Equals(pluScale.Identity.Uid))
        // {
        //  item.IsActive = pluScale.IsActive;
        // }
        //}
    }

    public async Task ActionSaveAsync(UserSettingsModel? userSettings, bool isNewWindow, bool isParentRazor)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (userSettings is null || !userSettings.AccessRightsIsWrite)
            return;

        RunActionsSafe(string.Empty, LocaleCore.Dialog.DialogResultFail,
            () =>
            {
                //AppSettings.DataAccess.Save(isParentRazor ? ParentRazor?.Item : Item);
                InvokeAsync(StateHasChanged);
            });
    }

    protected async Task ActionMarkAsync(UserSettingsModel? userSettings, bool isNewWindow, bool isParentRazor)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (userSettings is null || !userSettings.AccessRightsIsWrite)
            return;

        RunActionsWithQeustion(LocaleCore.Table.TableMark, LocaleCore.Dialog.DialogResultSuccess,
            LocaleCore.Dialog.DialogResultFail, GetQuestionAdd(),
            () =>
            {
                AppSettings.DataAccess.Mark(isParentRazor ? ParentRazor?.Item : Item);
            });

        ParentRazor?.OnChange();
    }

    protected async Task ActionDeleteAsync(UserSettingsModel? userSettings, bool isParentRazor)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (userSettings is null || !userSettings.AccessRightsIsWrite)
            return;

        RunActionsWithQeustion(LocaleCore.Table.TableDelete, LocaleCore.Dialog.DialogResultSuccess,
            LocaleCore.Dialog.DialogResultFail, GetQuestionAdd(),
            () =>
            {
                AppSettings.DataAccess.Delete(isParentRazor ? ParentRazor?.Item : Item);
            });

        ParentRazor?.OnChange();
    }

    protected async Task PrinterResourcesClear(UserSettingsModel? userSettings, PrinterModel printer)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (userSettings is null || !userSettings.AccessRightsIsWrite)
            return;

        RunActionsWithQeustion(LocaleCore.Print.ResourcesClear, LocaleCore.Dialog.DialogResultSuccess,
            LocaleCore.Dialog.DialogResultFail, GetQuestionAdd(),
            () =>
            {
                SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Description), 0, false, false);
                List<TemplateResourceModel> templateResources = AppSettings.DataAccess.GetList<TemplateResourceModel>(sqlCrudConfig);
                foreach (TemplateResourceModel templateResource in templateResources)
                {
                    if (templateResource.Name.Contains("TTF"))
                    {
                        TcpClient client = ZplUtils.TcpClientSendData(printer.Ip, printer.Port,
                            new()
                            {
                                new($"^XA^ID"),
                                new(templateResource.Name),
                                new($"^FS^XZ")
                            });
                    }
                }
            });
    }

    protected async Task PrinterResourcesLoad(UserSettingsModel? userSettings, PrinterModel printer, string fileType)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (userSettings is null || !userSettings.AccessRightsIsWrite)
            return;

        RunActionsWithQeustion(LocaleCore.Print.ResourcesLoadTtf, LocaleCore.Dialog.DialogResultSuccess,
            LocaleCore.Dialog.DialogResultFail, GetQuestionAdd(),
            () =>
            {
                SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Description), 0, false, false);
                List<TemplateResourceModel> templateResources = AppSettings.DataAccess.GetList<TemplateResourceModel>(sqlCrudConfig);
                foreach (TemplateResourceModel templateResource in templateResources)
                {
                    if (templateResource.Name.Contains(fileType))
                    {
                        TcpClient client = ZplUtils.TcpClientSendData(printer.Ip, printer.Port,
                            new()
                            {
                                new($"^XA^MNN^LL500~DYE:{templateResource.Name}.TTF,B,T,{templateResource.ImageData.Value.Length},,"),
                                new(templateResource.ImageData.Value),
                                new($"^XZ")
                            });
                    }
                }
            });
    }

    #endregion
}
