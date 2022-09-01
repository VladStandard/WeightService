// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Localizations;
using DataCore.Models;
using DataCore.Protocols;
using DataCore.Sql.Models;
using DataCore.Sql.Tables;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using DataCore.Sql.Core;
using static DataCore.ShareEnums;
using Environment = System.Environment;

namespace BlazorCore.Models;

public class RazorPageModel : LayoutComponentBase
{
    #region Public and private fields, properties, constructor

    [Inject] public DialogService DialogService { get; set; }
    [Inject] public IJSRuntime JsRuntime { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public NotificationService NotificationService { get; set; }
    [Inject] public TooltipService TooltipService { get; set; }
    [Parameter] public TableModel? ItemFilter { get; set; }
    public event Action? Change;
    [Parameter] public bool IsShowAdditionalFilter { get; set; }
    [Parameter] public bool IsShowItemsCount { get; set; }
    [Parameter] public bool IsShowMarkedFilter { get; set; }
    [Parameter] public bool IsShowMarked { get; set; }
    [Parameter] public bool IsShowOnlyTop { get; set; }
    [Parameter] public ButtonSettingsModel ButtonSettings { get; set; }
    [Parameter] public DbTableAction TableAction { get; set; }
    [Parameter] public Guid? IdentityUid { get; set; }
    [Parameter] public string IdentityUidStr { get => IdentityUid?.ToString() ?? Guid.Empty.ToString(); set => IdentityUid = Guid.TryParse(value, out Guid uid) ? uid : Guid.Empty; }
    [Parameter] public long? IdentityId { get; set; }
    [Parameter] public List<TableModel>? Items { get; set; }
    [Parameter] public List<TableModel>? ItemsFilter { get; set; }
    [Parameter] public RazorPageModel? ParentRazor { get; set; }
    [Parameter] public string? FilterCaption { get; set; }
    [Parameter] public string? FilterName { get; set; }
    [Parameter] public TableBase Table { get; set; }
    private ItemSaveCheckModel ItemSaveCheck { get; set; }
    protected AppSettingsHelper AppSettings { get; } = AppSettingsHelper.Instance;
    public TableModel? Item { get; set; }
    protected object? ItemObject { get => Item; set => Item = (TableModel?)value; }
    protected bool IsLoaded { get; private set; }
    protected UserSettingsModel UserSettings { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public RazorPageModel()
    {
	    UserSettings = new();
    }

    #endregion

    #region Public and private methods

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Change += OnParametersSet;
        ButtonSettings = new();
        FilterCaption = string.Empty;
        FilterName = string.Empty;
        IsShowOnlyTop = true;
        Table = new(string.Empty);
        TableAction = DbTableAction.Default;
        ItemSaveCheck = new();
        if (ParentRazor != null)
        {
            if (ParentRazor.ItemFilter != null)
                ItemFilter = ParentRazor.ItemFilter;
            if (ParentRazor.ItemsFilter != null)
                ItemsFilter = ParentRazor.ItemsFilter;
            IsShowAdditionalFilter = ParentRazor.IsShowAdditionalFilter;
            IsShowItemsCount = ParentRazor.IsShowItemsCount;
            IsShowMarkedFilter = ParentRazor.IsShowMarkedFilter;
            IsShowMarked = ParentRazor.IsShowMarked;
            IsShowOnlyTop = ParentRazor.IsShowOnlyTop;
            if (IdentityId == null && ParentRazor.IdentityId != null)
                IdentityId = ParentRazor.IdentityId;
            if (IdentityUid == null && ParentRazor.IdentityUid != null)
                IdentityUid = ParentRazor.IdentityUid;
            if (ParentRazor.Item != null)
                Item = ParentRazor.Item;
            if (!string.IsNullOrEmpty(ParentRazor.Table.Name))
            {
                Table = ParentRazor.Table;
            }
            if (ParentRazor.TableAction != DbTableAction.Default)
                TableAction = ParentRazor.TableAction;

            ButtonSettings = ParentRazor.ButtonSettings;
        }
    }

    public void OnChangeCheckBox(object value, string name)
    {
        RunActions($"{LocaleCore.Action.ActionMethod} {nameof(OnChangeCheckBox)}", "", LocaleCore.Dialog.DialogResultFail, "",
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
        RunActions($"{LocaleCore.Action.ActionMethod} {nameof(OnItemValueChange)}", "", LocaleCore.Dialog.DialogResultFail, "",
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
            printer.PrinterType = AppSettings.DataAccess.Crud.GetItemById<PrinterTypeModel>(printerTypeId) ?? new();
        }
    }

    private void OnItemValueChangePrinterResource(string? filterName, object? value, PrinterResourceModel printerResource)
    {
        if (filterName == nameof(printerResource.Printer) && value is long printerId)
        {
            printerResource.Printer = AppSettings.DataAccess.Crud.GetItemById<PrinterModel>(printerId) ?? new();
        }
        if (filterName == nameof(printerResource.Resource) && value is long resourceId)
        {
            printerResource.Resource = AppSettings.DataAccess.Crud.GetItemById<TemplateResourceModel>(resourceId) ?? new();
        }
    }

    private void OnItemValueChangePlu(string? filterName, object? value, PluObsoleteModel pluObsolete)
    {
        if (filterName == nameof(pluObsolete.Nomenclature) && value is long nomenclatureId)
        {
            pluObsolete.Nomenclature = AppSettings.DataAccess.Crud.GetItemById<NomenclatureModel>(nomenclatureId) ?? new();
        }
        if (filterName == nameof(pluObsolete.Scale) && value is long scaleId)
        {
            pluObsolete.Scale = AppSettings.DataAccess.Crud.GetItemById<ScaleModel>(scaleId) ?? new();
        }
        if (filterName == nameof(pluObsolete.Template) && value is long templateId)
        {
            pluObsolete.Template = AppSettings.DataAccess.Crud.GetItemById<TemplateModel>(templateId) ?? new();
        }
    }

    private void OnItemValueChangeScale(string? filterName, object? value, ScaleModel scale)
    {
        if (filterName == nameof(TableModel.Identity.Id) && value is long id)
        {
            scale = AppSettings.DataAccess.Crud.GetItemById<ScaleModel>(id) ?? new();
        }
        if (filterName == nameof(ScaleModel.DeviceComPort) && value is string deviceComPort)
        {
            scale.DeviceComPort = deviceComPort;
        }
        if (filterName == nameof(ScaleModel.Host) && value is long hostId)
        {
            scale.Host = AppSettings.DataAccess.Crud.GetItemById<HostModel>(hostId);
        }
        if (filterName == nameof(ScaleModel.TemplateDefault) && value is long templateDefaultId)
        {
            scale.TemplateDefault = AppSettings.DataAccess.Crud.GetItemById<TemplateModel>(templateDefaultId);
        }
        if (filterName == nameof(ScaleModel.TemplateSeries) && value is long templateSeriesId)
        {
            scale.TemplateSeries = AppSettings.DataAccess.Crud.GetItemById<TemplateModel>(templateSeriesId);
        }
        if (filterName == nameof(ScaleModel.PrinterMain) && value is long printerId)
        {
            scale.PrinterMain = AppSettings.DataAccess.Crud.GetItemById<PrinterModel>(printerId);
        }
        if (filterName == nameof(ScaleModel.WorkShop) && value is long workShopId)
        {
            scale.WorkShop = AppSettings.DataAccess.Crud.GetItemById<WorkShopModel>(workShopId);
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
            workshop.ProductionFacility = AppSettings.DataAccess.Crud.GetItemById<ProductionFacilityModel>(productionFacilityId) ?? new();
        }
    }

    public async Task ItemSelectAsync(TableModel item)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        RunActions($"{LocaleCore.Action.ActionMethod} {nameof(ItemSelectAsync)}", "", LocaleCore.Dialog.DialogResultFail, "",
            () =>
            {
                ItemSelect(item);
            });
    }

    public void ItemSelect(TableModel item)
    {
        if (Item is not null && !Item.Equals(item))
            Item = item;
        if (IdentityId != item.Identity.Id)
            IdentityId = item.Identity.Id;
        if (IdentityUid != item.Identity.Uid)
            IdentityUid = item.Identity.Uid;
    }

    //public async Task GetDataAsync(Task task, bool continueOnCapturedContext)
    //{
    //    await RunTasksAsync(LocaleCore.Table.TableRead, "", LocaleCore.Dialog.DialogResultFail, "",
    //        new() { task }, continueOnCapturedContext).ConfigureAwait(false);
    //}

    //public override Task SetParametersAsync(ParameterView parameters)
    //{
    //    //int code = parameters.GetHashCode();
    //    //if (code == 0)
    //    //    return Task.CompletedTask;
    //    //parameters.SetParameterProperties(this);
    //    //Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(true);

    //    //AppSettings.FontSize = parameters.TryGetValue("FontSize", out int fontSize) ? fontSize : 14;
    //    //AppSettings.FontSizeHeader = parameters.TryGetValue("FontSizeHeader", out int fontSizeHeader) ? fontSizeHeader : 20;

    //    //if (Identity.Id == null && ParentRazor?.Identity.Id != null)
    //    //    Identity.Id = ParentRazor.Identity.Id;
    //    //if (IdentityUid == null && ParentRazor?.Identity.Uid != null)
    //    //    IdentityUid = ParentRazor.Identity.Uid;
    //    //if (string.IsNullOrEmpty(Table.Name))
    //    //{
    //    //    if (ParentRazor != null)
    //    //    {
    //    //        Table = ParentRazor.Table;
    //    //    }
    //    //}
    //    //if (TableAction == DbTableAction.Default && ParentRazor != null)
    //    //{
    //    //    if (ParentRazor.TableAction != DbTableAction.Default)
    //    //        TableAction = ParentRazor.TableAction;
    //    //}
    //    //switch (Table)
    //    //{
    //    //    case TableScaleEntity:
    //    //        SetParametersForTableScale(parameters, ProjectsEnums.GetTableScale(Table.Name));
    //    //        break;
    //    //}

    //    return base.SetParametersAsync(ParameterView.Empty);
    //}

    protected void RunActions(List<Action> actions)
    {
        List<Action> actionsInjected = new()
        {
            () =>
            {
                IsLoaded = false;
		        //ButtonSettings = new();
	        }
        };
        actionsInjected.AddRange(actions);
        actionsInjected.Add(() =>
        {
            IsLoaded = true;
            InvokeAsync(StateHasChanged);
        });
        RunActions($"{LocaleCore.Action.ActionMethod} {nameof(RunActions)}", "",
            LocaleCore.Dialog.DialogResultFail, "", actionsInjected);
    }

    //private void SetParametersForTableScale(ParameterView parameters, ProjectsEnums.TableScale table)
    //{
    //    switch (table)
    //    {
    //        case ProjectsEnums.TableScale.BarCodeTypes:
    //            if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idBarcodeType))
    //            {
    //                Item = AppSettings.DataAccess.Crud.GetItem<BarCodeTypeV2Entity>(
    //                    new(new() { new(DbField.IdentityId, DbComparer.Equal, idBarcodeType) }));
    //            }
    //            break;
    //        case ProjectsEnums.TableScale.Contragents:
    //            if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idContragent))
    //            {
    //                Item = AppSettings.DataAccess.Crud.GetItem<ContragentV2Entity>(
    //                    new(new() { new(DbField.IdentityId, DbComparer.Equal, idContragent) }));
    //            }
    //            break;
    //        case ProjectsEnums.TableScale.Hosts:
    //            if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idHost))
    //            {
    //                Item = AppSettings.DataAccess.Crud.GetItem<HostEntity>(
    //                    new(new() { new(DbField.IdentityId, DbComparer.Equal, idHost) }));
    //            }
    //            break;
    //        case ProjectsEnums.TableScale.Labels:
    //            if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idLabel))
    //            {
    //                Item = AppSettings.DataAccess.Crud.GetItem<LabelEntity>(
    //                    new(new() { new(DbField.IdentityId, DbComparer.Equal, idLabel) }));
    //            }
    //            break;
    //        case ProjectsEnums.TableScale.Nomenclatures:
    //            if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idNomenclature))
    //            {
    //                Item = AppSettings.DataAccess.Crud.GetItem<NomenclatureModel>(
    //                    new(new() { new(DbField.IdentityId, DbComparer.Equal, idNomenclature) }));
    //            }
    //            break;
    //        case ProjectsEnums.TableScale.Orders:
    //            if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idOrder))
    //            {
    //                Item = AppSettings.DataAccess.Crud.GetItem<OrderEntity>(
    //                    new(new() { new(DbField.IdentityId, DbComparer.Equal, idOrder) }));
    //            }
    //            break;
    //        case ProjectsEnums.TableScale.Plus:
    //            if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idPlu))
    //            {
    //                Item = AppSettings.DataAccess.Crud.GetItem<PluEntity>(
    //                    new(new() { new(DbField.IdentityId, DbComparer.Equal, idPlu) }));
    //            }
    //            break;
    //        case ProjectsEnums.TableScale.Printers:
    //            if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idPrinter))
    //            {
    //                Item = AppSettings.DataAccess.Crud.GetItem<PrinterEntity>(
    //                    new(new() { new(DbField.IdentityId, DbComparer.Equal, idPrinter) }));
    //            }
    //            break;
    //        case ProjectsEnums.TableScale.PrintersResources:
    //            if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idPrinterResource))
    //            {
    //                PrinterResourceEntity printerResourceEntity = AppSettings.DataAccess.Crud.GetItem<PrinterResourceEntity>(
    //                    new(new() { new(DbField.IdentityId, DbComparer.Equal, idPrinterResource) }));
    //                Item = printerResourceEntity;
    //            }
    //            break;
    //        case ProjectsEnums.TableScale.PrintersTypes:
    //            if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idPrinterType))
    //            {
    //                PrinterTypeEntity printerTypeEntity = AppSettings.DataAccess.Crud.GetItem<PrinterTypeEntity>(
    //                    new(new() { new(DbField.IdentityId, DbComparer.Equal, idPrinterType) }));
    //                Item = printerTypeEntity;
    //            }
    //            break;
    //        case ProjectsEnums.TableScale.ProductSeries:
    //            if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idProductSeries))
    //            {
    //                ProductSeriesEntity productSeriesEntity = AppSettings.DataAccess.Crud.GetItem<ProductSeriesEntity>(
    //                    new(new() { new(DbField.IdentityId, DbComparer.Equal, idProductSeries) }));
    //                Item = productSeriesEntity;
    //            }
    //            break;
    //        case ProjectsEnums.TableScale.ProductionFacilities:
    //            if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idProductionFacility))
    //            {
    //                ProductionFacilityEntity productionFacilityEntity = AppSettings.DataAccess.Crud.GetItem<ProductionFacilityEntity>(
    //                    new(new() { new(DbField.IdentityId, DbComparer.Equal, idProductionFacility) }));
    //                Item = productionFacilityEntity;
    //            }
    //            break;
    //        case ProjectsEnums.TableScale.Scales:
    //            if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idScale))
    //            {
    //                ScaleEntity scaleEntity = AppSettings.DataAccess.Crud.GetItem<ScaleEntity>(
    //                    new(new() { new(DbField.IdentityId, DbComparer.Equal, idScale) }));
    //                Item = scaleEntity;
    //            }
    //            break;
    //        case ProjectsEnums.TableScale.TemplatesResources:
    //            if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idTemplateResource))
    //            {
    //                TemplateResourceEntity templateResourceEntity = AppSettings.DataAccess.Crud.GetItem<TemplateResourceEntity>(
    //                    new(new() { new(DbField.IdentityId, DbComparer.Equal, idTemplateResource) }));
    //                Item = templateResourceEntity;
    //            }
    //            break;
    //        case ProjectsEnums.TableScale.Templates:
    //            if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idTemplate))
    //            {
    //                TemplateEntity templateEntity = AppSettings.DataAccess.Crud.GetItem<TemplateEntity>(
    //                    new(new() { new(DbField.IdentityId, DbComparer.Equal, idTemplate) }));
    //                Item = templateEntity;
    //            }
    //            break;
    //        case ProjectsEnums.TableScale.Workshops:
    //            if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idWorkshop))
    //            {
    //                WorkShopEntity workshopEntity = AppSettings.DataAccess.Crud.GetItem<WorkShopEntity>(
    //                    new(new() { new(DbField.IdentityId, DbComparer.Equal, idWorkshop) }));
    //                Item = workshopEntity;
    //            }
    //            break;
    //    }
    //}

    public static ConfirmOptions GetConfirmOptions() => new()
    {
        OkButtonText = LocaleCore.Dialog.DialogButtonYes,
        CancelButtonText = LocaleCore.Dialog.DialogButtonCancel
        //    ShowTitle = true,
        //    ShowClose = true,
        //    Bottom = null,
        //    ChildContent = null,
        //    Height = null,
        //    Left = null,
        //    Style = null,
        //    Top = null,
        //    Width = null,
    };

    //public async Task RunTasksAsync(string title, string detailSuccess, string detailFail, string detailCancel, List<Task> tasks,
    //    bool continueOnCapturedContext,
    //    [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    //{
    //    await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

    //    RunActions(title, detailSuccess, detailFail, detailCancel, tasks, continueOnCapturedContext, filePath, lineNumber, memberName);
    //}

    //public void RunTasks(string title, string detailSuccess, string detailFail, string detailCancel, List<Task> tasks, bool continueOnCapturedContext,
    //       [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    //   {
    //       try
    //       {
    //           RunTasksCore(title, detailSuccess, detailCancel, tasks, continueOnCapturedContext);
    //       }
    //       catch (Exception ex)
    //       {
    //           RunTasksCatch(ex, title, detailFail, filePath, lineNumber, memberName);
    //       }
    //   }

    public void RunActions(string title, string detailSuccess, string detailFail, string detailCancel, List<Action> actions,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    {
        try
        {
            RunActionsCore(title, detailSuccess, detailCancel, actions);
        }
        catch (Exception ex)
        {
            RunTasksCatch(ex, title, detailFail, filePath, lineNumber, memberName);
        }
    }

    //public void RunTasks(string title, string detailSuccess, string detailFail, string detailCancel, Task task, bool continueOnCapturedContext,
    //    [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    //{
    //    RunTasks(title, detailSuccess, detailFail, detailCancel, new List<Task> { task }, continueOnCapturedContext, filePath, lineNumber, memberName);
    //}

    public void RunActions(string title, string detailSuccess, string detailFail, string detailCancel, Action action,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    {
        RunActions(title, detailSuccess, detailFail, detailCancel, (List<Action>)new() { action }, filePath, lineNumber, memberName);
    }

    private void RunTasksCore(string title, string detailSuccess, string detailCancel, List<Task> tasks, bool continueOnCapturedContext)
    {
        if (tasks.Count > 0)
        {
            foreach (Task task in tasks)
            {
                task.ConfigureAwait(continueOnCapturedContext);
                task.Start();
            }
        }
        if (!string.IsNullOrEmpty(detailSuccess))
            NotificationService.Notify(NotificationSeverity.Success, title + Environment.NewLine, detailSuccess, AppSettingsHelper.Delay);
        else
        {
            if (!string.IsNullOrEmpty(detailCancel))
                NotificationService.Notify(NotificationSeverity.Info, title + Environment.NewLine, detailCancel, AppSettingsHelper.Delay);
        }
    }

    private void RunActionsCore(string title, string detailSuccess, string detailCancel, List<Action> actions)
    {
        if (actions.Count > 0)
        {
            foreach (Action action in actions)
            {
                //InvokeAsync(action);
                action.Invoke();
            }
        }
        if (!string.IsNullOrEmpty(detailSuccess))
            NotificationService.Notify(NotificationSeverity.Success, title + Environment.NewLine, detailSuccess, AppSettingsHelper.Delay);
        else
        {
            if (!string.IsNullOrEmpty(detailCancel))
                NotificationService.Notify(NotificationSeverity.Info, title + Environment.NewLine, detailCancel, AppSettingsHelper.Delay);
        }
    }

    public void RunTasksCatch(Exception ex, string title, string detailFail, string filePath, int lineNumber, string memberName)
    {
        // User log.
        string msg = ex.Message;
        if (!string.IsNullOrEmpty(ex.InnerException?.Message))
            msg += Environment.NewLine + ex.InnerException.Message;
        if (!string.IsNullOrEmpty(detailFail))
        {
            if (!string.IsNullOrEmpty(msg))
                NotificationService.Notify(NotificationSeverity.Error, title + Environment.NewLine, detailFail + Environment.NewLine + msg, AppSettingsHelper.Delay);
            else
                NotificationService.Notify(NotificationSeverity.Error, title + Environment.NewLine, detailFail, AppSettingsHelper.Delay);
        }
        else
        {
            if (!string.IsNullOrEmpty(msg))
                NotificationService.Notify(NotificationSeverity.Error, title + Environment.NewLine, msg, AppSettingsHelper.Delay);
        }
        // SQL log.
        AppSettings.DataAccess.Log.LogError(ex, NetUtils.GetLocalHostName(false), nameof(BlazorCore), filePath, lineNumber, memberName);
    }

    //public void RunTasksWithQeustion(string title, string detailSuccess, string detailFail, string detailCancel,
    //    string questionAdd, Task task, bool continueOnCapturedContext,
    //    [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    //{
    //    try
    //    {
    //        string question = string.IsNullOrEmpty(questionAdd)
    //            ? LocaleCore.Dialog.DialogQuestion
    //            : questionAdd;
    //        Task<bool?> dialog = DialogService.Confirm(question, title, GetConfirmOptions());
    //        bool? result = dialog.Result;
    //        if (result == true)
    //        {
    //            RunTasks(title, detailSuccess, detailFail, detailCancel, task, continueOnCapturedContext);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        RunTasksCatch(ex, title, detailFail, filePath, lineNumber, memberName);
    //    }
    //}

    public void RunActionsWithQeustion(string title, string detailSuccess, string detailFail, string detailCancel, string questionAdd,
        Action action, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    {
        try
        {
            string question = string.IsNullOrEmpty(questionAdd)
                ? LocaleCore.Dialog.DialogQuestion
                : questionAdd;
            Task<bool?> dialog = DialogService.Confirm(question, title, GetConfirmOptions());
            bool? result = dialog.Result;
            if (result == true)
            {
                RunActions(title, detailSuccess, detailFail, detailCancel, action);
            }
        }
        catch (Exception ex)
        {
            RunTasksCatch(ex, title, detailFail, filePath, lineNumber, memberName);
        }
    }

    public static string GetPath(string uriItemRoute, TableModel? item, long? id) =>
        item == null || id == null ? string.Empty : $"{uriItemRoute}/{id}";

    public static string GetPath(string uriItemRoute, TableModel? item, Guid? uid) =>
        item == null || uid == null ? string.Empty : $"{uriItemRoute}/{uid}";

    public void OnChange()
    {
        Change?.Invoke();
    }

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
        if (Table == null || string.IsNullOrEmpty(Table.Name))
            if (ParentRazor != null)
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
                if (item == null)
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
            if (IdentityUid != null && IdentityUid != Guid.Empty)
                await JsRuntime.InvokeAsync<object>("open", $"{page}/{IdentityUid}", "_blank").ConfigureAwait(false);
            else if (IdentityId != null && JsRuntime != null)
                await JsRuntime.InvokeAsync<object>("open", $"{page}/{IdentityId}", "_blank").ConfigureAwait(false);
        }).ConfigureAwait(true);
    }

    public void RouteSectionNavigateToRoot()
    {
        NavigationManager.NavigateTo(LocaleCore.DeviceControl.RouteRoot);
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
        RunActions(LocaleCore.Table.TableCancel, LocaleCore.Dialog.DialogResultSuccess,
            LocaleCore.Dialog.DialogResultFail, LocaleCore.Dialog.DialogResultCancel,
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
                if (ParentRazor?.Items != null)
                {
                    List<PluScaleModel> pluScales = ParentRazor.Items.Cast<PluScaleModel>().ToList();
                    foreach (PluScaleModel pluScale in pluScales)
                    {
                        ItemSaveCheck.PluScale(NotificationService, pluScale, DbTableAction.Save);
                    }
                }
                else if (ParentRazor?.Item != null)
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
            LocaleCore.Dialog.DialogResultFail, LocaleCore.Dialog.DialogResultCancel, GetQuestionAdd(),
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

    public async Task ActionNewAsync(UserSettingsModel userSettings, bool isNewWindow, bool isParentRazor)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (!userSettings.Identity.AccessRightsIsWrite)
            return;
        
        RunActions($"{LocaleCore.Action.ActionMethod} {nameof(ActionNewAsync)}", "", LocaleCore.Dialog.DialogResultFail, "",
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

    public async Task ActionCopyAsync(UserSettingsModel userSettings, bool isNewWindow, bool isParentRazor)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (!userSettings.Identity.AccessRightsIsWrite)
            return;
        
        RunActions($"{LocaleCore.Action.ActionMethod} {nameof(ActionCopyAsync)}", "", LocaleCore.Dialog.DialogResultFail, "",
            () =>
            {
                RouteItemNavigate(isNewWindow, isParentRazor ? ParentRazor?.Item : Item, DbTableAction.Copy);
            });

        ParentRazor?.OnChange();
    }

    public async Task ActionEditAsync(UserSettingsModel userSettings, bool isNewWindow, bool isParentRazor)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (!userSettings.Identity.AccessRightsIsWrite)
            return;
        
        RunActions($"{LocaleCore.Action.ActionMethod} {nameof(ActionEditAsync)}", "", LocaleCore.Dialog.DialogResultFail, "",
            () =>
            {
                RouteItemNavigate(isNewWindow, isParentRazor ? ParentRazor?.Item : Item, DbTableAction.Edit);
                InvokeAsync(StateHasChanged);
            });
    }

    protected async Task ActionPluScalePlusClickAsync(UserSettingsModel userSettings, PluScaleModel pluScale, List<PluScaleModel> pluScales)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (!userSettings.Identity.AccessRightsIsWrite)
            return;

        //foreach (PluScaleModel item in pluScales)
        //{
        // if (item.Identity.Uid.Equals(pluScale.Identity.Uid))
        // {
        //  item.IsActive = pluScale.IsActive;
        // }
        //}
    }

    public async Task ActionSaveAsync(UserSettingsModel userSettings, bool isNewWindow, bool isParentRazor)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (!userSettings.Identity.AccessRightsIsWrite)
            return;

		RunActions($"{LocaleCore.Action.ActionMethod} {nameof(ActionSaveAsync)}", "", LocaleCore.Dialog.DialogResultFail, "",
            () =>
            {
	            //AppSettings.DataAccess.Crud.Save(isParentRazor ? ParentRazor?.Item : Item);
				InvokeAsync(StateHasChanged);
            });
    }

    protected async Task ActionMarkAsync(UserSettingsModel userSettings, bool isNewWindow, bool isParentRazor)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (!userSettings.Identity.AccessRightsIsWrite)
            return;
        
        RunActionsWithQeustion(LocaleCore.Table.TableMark, LocaleCore.Dialog.DialogResultSuccess,
            LocaleCore.Dialog.DialogResultFail, LocaleCore.Dialog.DialogResultCancel, GetQuestionAdd(),
            () =>
            {
                AppSettings.DataAccess.Crud.Mark(isParentRazor ? ParentRazor?.Item : Item);
            });

        ParentRazor?.OnChange();
    }

    protected async Task ActionDeleteAsync(UserSettingsModel userSettings, bool isParentRazor)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (!userSettings.Identity.AccessRightsIsWrite)
            return;
        
        RunActionsWithQeustion(LocaleCore.Table.TableDelete, LocaleCore.Dialog.DialogResultSuccess,
            LocaleCore.Dialog.DialogResultFail, LocaleCore.Dialog.DialogResultCancel, GetQuestionAdd(),
            () =>
            {
                AppSettings.DataAccess.Crud.Delete(isParentRazor ? ParentRazor?.Item : Item);
            });

        ParentRazor?.OnChange();
    }

    protected async Task PrinterResourcesClear(UserSettingsModel userSettings, PrinterModel printer)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (!userSettings.Identity.AccessRightsIsWrite)
            return;

        RunActionsWithQeustion(LocaleCore.Print.ResourcesClear, LocaleCore.Dialog.DialogResultSuccess,
            LocaleCore.Dialog.DialogResultFail, LocaleCore.Dialog.DialogResultCancel, GetQuestionAdd(),
            () =>
            {
	            SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Description), 0, false, false);
                List<TemplateResourceModel> templateResources = AppSettings.DataAccess.Crud.GetList<TemplateResourceModel>(sqlCrudConfig);
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

    protected async Task PrinterResourcesLoad(UserSettingsModel userSettings, PrinterModel printer, string fileType)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (!userSettings.Identity.AccessRightsIsWrite)
            return;

        RunActionsWithQeustion(LocaleCore.Print.ResourcesLoadTtf, LocaleCore.Dialog.DialogResultSuccess,
            LocaleCore.Dialog.DialogResultFail, LocaleCore.Dialog.DialogResultCancel, GetQuestionAdd(),
            () =>
            {
	            SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Description), 0, false, false);
				List<TemplateResourceModel> templateResources = AppSettings.DataAccess.Crud.GetList<TemplateResourceModel>(sqlCrudConfig);
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
