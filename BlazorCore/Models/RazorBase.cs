// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Localizations;
using DataCore.Models;
using DataCore.Protocols;
using DataCore.Sql.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using static DataCore.ShareEnums;

namespace BlazorCore.Models;

public class RazorBase : LayoutComponentBase
{
    #region Public and private fields, properties, constructor

    [Inject] public DialogService DialogService { get; set; }
    [Inject] public IJSRuntime JsRuntime { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public NotificationService NotificationService { get; set; }
    [Inject] public TooltipService TooltipService { get; set; }
    [Parameter] public BaseEntity? ItemFilter { get; set; }
    public event Action? Change;
    [Parameter] public bool IsShowAdditionalFilter { get; set; }
    [Parameter] public bool IsShowItemsCount { get; set; }
    [Parameter] public bool IsShowMarkedFilter { get; set; }
    [Parameter] public bool IsShowMarkedItems { get; set; }
    [Parameter] public bool IsSelectTopRows { get; set; }
    [Parameter] public ButtonSettingsEntity ButtonSettings { get; set; }
    [Parameter] public DbTableAction TableAction { get; set; }
    [Parameter] public Guid? IdentityUid { get; set; }
    [Parameter] public string IdentityUidStr { get => IdentityUid?.ToString() ?? Guid.Empty.ToString(); set => IdentityUid = Guid.TryParse(value, out Guid uid) ? uid : Guid.Empty; }
    [Parameter] public long? IdentityId { get; set; }
    [Parameter] public List<BaseEntity>? Items { get; set; }
    [Parameter] public List<BaseEntity>? ItemsFilter { get; set; }
    [Parameter] public RazorBase? ParentRazor { get; set; }
    [Parameter] public string? FilterCaption { get; set; }
    [Parameter] public string? FilterName { get; set; }
    [Parameter] public TableBase Table { get; set; }
    private ItemSaveCheckEntity ItemSaveCheck { get; set; }
    protected AppSettingsHelper AppSettings { get; } = AppSettingsHelper.Instance;
    public BaseEntity? Item { get; set; }
    protected bool IsLoaded { get; private set; }
    protected object? ItemObject { get => Item ?? null; set => Item = (BaseEntity?)value; }
    protected UserSettingsHelper UserSettings { get; } = UserSettingsHelper.Instance;

    /// <summary>
    /// Constructor.
    /// </summary>
    public RazorBase()
    {
        //
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
        IsSelectTopRows = true;
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
            IsShowMarkedItems = ParentRazor.IsShowMarkedItems;
            IsSelectTopRows = ParentRazor.IsSelectTopRows;
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
                    case nameof(IsShowMarkedItems):
                        if (value is bool isShowMarkedItems)
                            IsShowMarkedItems = isShowMarkedItems;
                        break;
                    case nameof(IsSelectTopRows):
                        if (value is bool isShowTOp100)
                            IsShowMarkedItems = isShowTOp100;
                        break;
                }
                StateHasChanged();
            });
    }

    public void OnItemValueChange(BaseEntity? item, string? filterName, object? value)
    {
        RunActions($"{LocaleCore.Action.ActionMethod} {nameof(OnItemValueChange)}", "", LocaleCore.Dialog.DialogResultFail, "",
            () =>
            {
                switch (item)
                {
                    case AccessEntity access:
                        OnItemValueChangeAccess(filterName, value, access);
                        break;
                    case PrinterEntity printer:
                        OnItemValueChangePrinter(filterName, value, printer);
                        break;
                    case PrinterResourceEntity printerResource:
                        OnItemValueChangePrinterResource(filterName, value, printerResource);
                        break;
                    case PluObsoleteEntity plu:
                        OnItemValueChangePlu(filterName, value, plu);
                        break;
                    case ScaleEntity scale:
                        OnItemValueChangeScale(filterName, value, scale);
                        break;
                    case TemplateEntity template:
                        OnItemValueChangeTemplate(filterName, value, template);
                        break;
                    case WorkShopEntity workShop:
                        OnItemValueChangeWorkShop(filterName, value, workShop);
                        break;
                }
                StateHasChanged();
            });
    }

    private static void OnItemValueChangeAccess(string? filterName, object? value, AccessEntity access)
    {
        if (filterName == nameof(access.Rights) && value is AccessRights rights)
        {
            access.Rights = (byte)rights;
        }
    }

    private void OnItemValueChangePrinter(string? filterName, object? value, PrinterEntity printer)
    {
        if (filterName == nameof(printer.PrinterType) && value is long printerTypeId)
        {
            printer.PrinterType = AppSettings.DataAccess.Crud.GetEntity<PrinterTypeEntity>(
                new(new() { new(DbField.IdentityId, DbComparer.Equal, printerTypeId) }));
        }
    }

    //private void OnItemValueChangePrinterType(string? filterName, object? value, PrinterTypeEntity printerType)
    //{
    //    if (filterName == nameof(printerType) && value is long printerTypeId)
    //    {
    //        printerType = AppSettings.DataAccess.Crud.GetEntity<PrinterTypeEntity>(
    //            new FieldListEntity(new Dictionary<string, object?> { { DbField.IdentityId.ToString(), printerTypeId } }),
    //        null);
    //    }
    //}

    private void OnItemValueChangePrinterResource(string? filterName, object? value, PrinterResourceEntity printerResource)
    {
        if (filterName == nameof(printerResource.Printer) && value is long printerId)
        {
            printerResource.Printer = AppSettings.DataAccess.Crud.GetEntity<PrinterEntity>(
                new(new() { new(DbField.IdentityId, DbComparer.Equal, printerId) }));
        }
        if (filterName == nameof(printerResource.Resource) && value is long resourceId)
        {
            printerResource.Resource = AppSettings.DataAccess.Crud.GetEntity<TemplateResourceEntity>(
                new(new() { new(DbField.IdentityId, DbComparer.Equal, resourceId) }));
        }
    }

    private void OnItemValueChangePlu(string? filterName, object? value, PluObsoleteEntity plu)
    {
        if (filterName == nameof(plu.Nomenclature) && value is long nomenclatureId)
        {
            plu.Nomenclature = AppSettings.DataAccess.Crud.GetEntity<NomenclatureEntity>(
                new(new() { new(DbField.IdentityId, DbComparer.Equal, nomenclatureId) }));
        }
        if (filterName == nameof(plu.Scale) && value is long scaleId)
        {
            plu.Scale = AppSettings.DataAccess.Crud.GetEntity<ScaleEntity>(
                new(new() { new(DbField.IdentityId, DbComparer.Equal, scaleId) }));
        }
        if (filterName == nameof(plu.Template) && value is long templateId)
        {
            plu.Template = AppSettings.DataAccess.Crud.GetEntity<TemplateEntity>(
                new(new() { new(DbField.IdentityId, DbComparer.Equal, templateId) }));
        }
    }

    private void OnItemValueChangeScale(string? filterName, object? value, ScaleEntity scale)
    {
        if (filterName == nameof(BaseEntity.IdentityId) && value is long id)
        {
            scale = AppSettings.DataAccess.Crud.GetEntity<ScaleEntity>(
                new(new() { new(DbField.IdentityId, DbComparer.Equal, id) }));
        }
        if (filterName == nameof(ScaleEntity.DeviceComPort) && value is string deviceComPort)
        {
            scale.DeviceComPort = deviceComPort;
        }
        if (filterName == nameof(ScaleEntity.Host) && value is long hostId)
        {
            scale.Host = AppSettings.DataAccess.Crud.GetEntity<HostEntity>(
                new(new() { new(DbField.IdentityId, DbComparer.Equal, hostId) }));
        }
        if (filterName == nameof(ScaleEntity.TemplateDefault) && value is long templateDefaultId)
        {
            scale.TemplateDefault = AppSettings.DataAccess.Crud.GetEntity<TemplateEntity>(
                new(new() { new(DbField.IdentityId, DbComparer.Equal, templateDefaultId) }));
        }
        if (filterName == nameof(ScaleEntity.TemplateSeries) && value is long templateSeriesId)
        {
            scale.TemplateSeries = AppSettings.DataAccess.Crud.GetEntity<TemplateEntity>(
                new(new() { new(DbField.IdentityId, DbComparer.Equal, templateSeriesId) }));
        }
        if (filterName == nameof(ScaleEntity.PrinterMain) && value is long printerId)
        {
            scale.PrinterMain = AppSettings.DataAccess.Crud.GetEntity<PrinterEntity>(
                new(new() { new(DbField.IdentityId, DbComparer.Equal, printerId) }));
        }
        if (filterName == nameof(ScaleEntity.WorkShop) && value is long workShopId)
        {
            scale.WorkShop = AppSettings.DataAccess.Crud.GetEntity<WorkShopEntity>(
                new(new() { new(DbField.IdentityId, DbComparer.Equal, workShopId) }));
        }
    }

    private static void OnItemValueChangeTemplate(string? filterName, object? value, TemplateEntity template)
    {
        if (filterName == nameof(template.CategoryId) && value is string categoryId)
        {
            template.CategoryId = categoryId;
        }
    }

    private void OnItemValueChangeWorkShop(string? filterName, object? value, WorkShopEntity workshop)
    {
        if (filterName == nameof(workshop.ProductionFacility) && value is int ProductionFacilityId)
        {
            workshop.ProductionFacility = AppSettings.DataAccess.Crud.GetEntity<ProductionFacilityEntity>(
                new(new() { new(DbField.IdentityId, DbComparer.Equal, ProductionFacilityId) }));
        }
    }

    public async Task ItemSelectAsync(BaseEntity item)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        RunActions($"{LocaleCore.Action.ActionMethod} {nameof(ItemSelectAsync)}", "", LocaleCore.Dialog.DialogResultFail, "",
            () =>
            {
                ItemSelect(item);
                //await GuiRefreshWithWaitAsync();
            });
    }

    public void ItemSelect(BaseEntity item)
    {
        if (Item != item)
            Item = item;
        if (IdentityId != item.IdentityId)
            IdentityId = item.IdentityId;
        if (IdentityUid != item.IdentityUid)
            IdentityUid = item.IdentityUid;
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

    //    //if (IdentityId == null && ParentRazor?.IdentityId != null)
    //    //    IdentityId = ParentRazor.IdentityId;
    //    //if (IdentityUid == null && ParentRazor?.IdentityUid != null)
    //    //    IdentityUid = ParentRazor.IdentityUid;
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
        List<Action> actionsInjected = new();
        actionsInjected.Add(() =>
        {
            IsLoaded = false;
            //ButtonSettings = new();
        });
        actionsInjected.AddRange(actions);
        actionsInjected.Add(() =>
        {
            IsLoaded = true;
            StateHasChanged();
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
    //                Item = AppSettings.DataAccess.Crud.GetEntity<BarCodeTypeV2Entity>(
    //                    new(new() { new(DbField.IdentityId, DbComparer.Equal, idBarcodeType) }));
    //            }
    //            break;
    //        case ProjectsEnums.TableScale.Contragents:
    //            if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idContragent))
    //            {
    //                Item = AppSettings.DataAccess.Crud.GetEntity<ContragentV2Entity>(
    //                    new(new() { new(DbField.IdentityId, DbComparer.Equal, idContragent) }));
    //            }
    //            break;
    //        case ProjectsEnums.TableScale.Hosts:
    //            if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idHost))
    //            {
    //                Item = AppSettings.DataAccess.Crud.GetEntity<HostEntity>(
    //                    new(new() { new(DbField.IdentityId, DbComparer.Equal, idHost) }));
    //            }
    //            break;
    //        case ProjectsEnums.TableScale.Labels:
    //            if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idLabel))
    //            {
    //                Item = AppSettings.DataAccess.Crud.GetEntity<LabelEntity>(
    //                    new(new() { new(DbField.IdentityId, DbComparer.Equal, idLabel) }));
    //            }
    //            break;
    //        case ProjectsEnums.TableScale.Nomenclatures:
    //            if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idNomenclature))
    //            {
    //                Item = AppSettings.DataAccess.Crud.GetEntity<NomenclatureEntity>(
    //                    new(new() { new(DbField.IdentityId, DbComparer.Equal, idNomenclature) }));
    //            }
    //            break;
    //        case ProjectsEnums.TableScale.Orders:
    //            if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idOrder))
    //            {
    //                Item = AppSettings.DataAccess.Crud.GetEntity<OrderEntity>(
    //                    new(new() { new(DbField.IdentityId, DbComparer.Equal, idOrder) }));
    //            }
    //            break;
    //        case ProjectsEnums.TableScale.OrdersStatuses:
    //            if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idOrderStatus))
    //            {
    //                Item = AppSettings.DataAccess.Crud.GetEntity<OrderStatusEntity>(
    //                    new(new() { new(DbField.IdentityId, DbComparer.Equal, idOrderStatus) }));
    //            }
    //            break;
    //        case ProjectsEnums.TableScale.OrdersTypes:
    //            if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idOrderType))
    //            {
    //                Item = AppSettings.DataAccess.Crud.GetEntity<OrderTypeEntity>(
    //                    new(new() { new(DbField.IdentityId, DbComparer.Equal, idOrderType) }));
    //            }
    //            break;
    //        case ProjectsEnums.TableScale.Organizations:
    //            break;
    //        case ProjectsEnums.TableScale.Plus:
    //            if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idPlu))
    //            {
    //                Item = AppSettings.DataAccess.Crud.GetEntity<PluEntity>(
    //                    new(new() { new(DbField.IdentityId, DbComparer.Equal, idPlu) }));
    //            }
    //            break;
    //        case ProjectsEnums.TableScale.Printers:
    //            if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idPrinter))
    //            {
    //                Item = AppSettings.DataAccess.Crud.GetEntity<PrinterEntity>(
    //                    new(new() { new(DbField.IdentityId, DbComparer.Equal, idPrinter) }));
    //            }
    //            break;
    //        case ProjectsEnums.TableScale.PrintersResources:
    //            if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idPrinterResource))
    //            {
    //                PrinterResourceEntity printerResourceEntity = AppSettings.DataAccess.Crud.GetEntity<PrinterResourceEntity>(
    //                    new(new() { new(DbField.IdentityId, DbComparer.Equal, idPrinterResource) }));
    //                Item = printerResourceEntity;
    //            }
    //            break;
    //        case ProjectsEnums.TableScale.PrintersTypes:
    //            if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idPrinterType))
    //            {
    //                PrinterTypeEntity printerTypeEntity = AppSettings.DataAccess.Crud.GetEntity<PrinterTypeEntity>(
    //                    new(new() { new(DbField.IdentityId, DbComparer.Equal, idPrinterType) }));
    //                Item = printerTypeEntity;
    //            }
    //            break;
    //        case ProjectsEnums.TableScale.ProductSeries:
    //            if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idProductSeries))
    //            {
    //                ProductSeriesEntity productSeriesEntity = AppSettings.DataAccess.Crud.GetEntity<ProductSeriesEntity>(
    //                    new(new() { new(DbField.IdentityId, DbComparer.Equal, idProductSeries) }));
    //                Item = productSeriesEntity;
    //            }
    //            break;
    //        case ProjectsEnums.TableScale.ProductionFacilities:
    //            if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idProductionFacility))
    //            {
    //                ProductionFacilityEntity productionFacilityEntity = AppSettings.DataAccess.Crud.GetEntity<ProductionFacilityEntity>(
    //                    new(new() { new(DbField.IdentityId, DbComparer.Equal, idProductionFacility) }));
    //                Item = productionFacilityEntity;
    //            }
    //            break;
    //        case ProjectsEnums.TableScale.Scales:
    //            if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idScale))
    //            {
    //                ScaleEntity scaleEntity = AppSettings.DataAccess.Crud.GetEntity<ScaleEntity>(
    //                    new(new() { new(DbField.IdentityId, DbComparer.Equal, idScale) }));
    //                Item = scaleEntity;
    //            }
    //            break;
    //        case ProjectsEnums.TableScale.TemplatesResources:
    //            if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idTemplateResource))
    //            {
    //                TemplateResourceEntity templateResourceEntity = AppSettings.DataAccess.Crud.GetEntity<TemplateResourceEntity>(
    //                    new(new() { new(DbField.IdentityId, DbComparer.Equal, idTemplateResource) }));
    //                Item = templateResourceEntity;
    //            }
    //            break;
    //        case ProjectsEnums.TableScale.Templates:
    //            if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idTemplate))
    //            {
    //                TemplateEntity templateEntity = AppSettings.DataAccess.Crud.GetEntity<TemplateEntity>(
    //                    new(new() { new(DbField.IdentityId, DbComparer.Equal, idTemplate) }));
    //                Item = templateEntity;
    //            }
    //            break;
    //        case ProjectsEnums.TableScale.WeithingFacts:
    //            if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idWeithingFact))
    //            {
    //                WeithingFactEntity weithingFactEntity = AppSettings.DataAccess.Crud.GetEntity<WeithingFactEntity>(
    //                    new(new() { new(DbField.IdentityId, DbComparer.Equal, idWeithingFact) }));
    //                Item = weithingFactEntity;
    //            }
    //            break;
    //        case ProjectsEnums.TableScale.Workshops:
    //            if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idWorkshop))
    //            {
    //                WorkShopEntity workshopEntity = AppSettings.DataAccess.Crud.GetEntity<WorkShopEntity>(
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

    public static string GetPath(string uriItemRoute, BaseEntity? item, long? id) =>
        item == null || id == null ? string.Empty : $"{uriItemRoute}/{id}";

    public static string GetPath(string uriItemRoute, BaseEntity? item, Guid? uid) =>
        item == null || uid == null ? string.Empty : $"{uriItemRoute}/{uid}";

    public void OnChange()
    {
        Change?.Invoke();
    }

    #endregion

    #region Public and private methods - Actions

    public void RouteItemNavigate(bool isNewWindow, BaseEntity? item, DbTableAction tableAction)
    {
        string page = RouteItemNavigatePage();
        if (string.IsNullOrEmpty(page))
            return;

        if (!isNewWindow)
            RouteItemNavigateForItem(item, page, tableAction);
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
            case TableSystemEntity:
                switch (ProjectsEnums.GetTableSystem(Table.Name))
                {
                    case ProjectsEnums.TableSystem.Default:
                        break;
                    case ProjectsEnums.TableSystem.Logs:
                        page = LocaleData.DeviceControl.UriRouteItem.Log;
                        break;
                    case ProjectsEnums.TableSystem.Accesses:
                        page = LocaleData.DeviceControl.UriRouteItem.Access;
                        break;
                    case ProjectsEnums.TableSystem.LogTypes:
                        page = LocaleData.DeviceControl.UriRouteItem.LogType;
                        break;
                    case ProjectsEnums.TableSystem.Tasks:
                        page = LocaleData.DeviceControl.UriRouteItem.TaskModule;
                        break;
                    case ProjectsEnums.TableSystem.TasksTypes:
                        page = LocaleData.DeviceControl.UriRouteItem.TaskTypeModule;
                        break;
                }
                break;
            case TableScaleEntity:
                switch (ProjectsEnums.GetTableScale(Table.Name))
                {
                    case ProjectsEnums.TableScale.BarCodeTypes:
                        page = LocaleData.DeviceControl.UriRouteItem.BarCodeType;
                        break;
                    case ProjectsEnums.TableScale.Contragents:
                        page = LocaleData.DeviceControl.UriRouteItem.Contragent;
                        break;
                    case ProjectsEnums.TableScale.Hosts:
                        page = LocaleData.DeviceControl.UriRouteItem.Host;
                        break;
                    case ProjectsEnums.TableScale.Labels:
                        page = LocaleData.DeviceControl.UriRouteItem.Label;
                        break;
                    case ProjectsEnums.TableScale.Nomenclatures:
                        page = LocaleData.DeviceControl.UriRouteItem.Nomenclature;
                        break;
                    case ProjectsEnums.TableScale.PlusObsolete:
                        page = LocaleData.DeviceControl.UriRouteItem.PluObsolete;
                        break;
                    case ProjectsEnums.TableScale.Plus:
                        page = LocaleData.DeviceControl.UriRouteItem.Plu;
                        break;
                    case ProjectsEnums.TableScale.PlusScales:
                        page = LocaleData.DeviceControl.UriRouteItem.PluScale;
                        break;
                    case ProjectsEnums.TableScale.PrintersResources:
                        page = LocaleData.DeviceControl.UriRouteItem.PrinterResource;
                        break;
                    case ProjectsEnums.TableScale.Printers:
                        page = LocaleData.DeviceControl.UriRouteItem.Printer;
                        break;
                    case ProjectsEnums.TableScale.PrintersTypes:
                        page = LocaleData.DeviceControl.UriRouteItem.PrinterType;
                        break;
                    case ProjectsEnums.TableScale.ProductionFacilities:
                        page = LocaleData.DeviceControl.UriRouteItem.ProductionFacility;
                        break;
                    case ProjectsEnums.TableScale.Scales:
                        page = LocaleData.DeviceControl.UriRouteItem.Scale;
                        break;
                    case ProjectsEnums.TableScale.TemplatesResources:
                        page = LocaleData.DeviceControl.UriRouteItem.TemplateResource;
                        break;
                    case ProjectsEnums.TableScale.Templates:
                        page = LocaleData.DeviceControl.UriRouteItem.Template;
                        break;
                    case ProjectsEnums.TableScale.WeithingFacts:
                        page = LocaleData.DeviceControl.UriRouteItem.WeithingFact;
                        break;
                    case ProjectsEnums.TableScale.Workshops:
                        page = LocaleData.DeviceControl.UriRouteItem.WorkShop;
                        break;
                }
                break;
        }
        return page;
    }

    private void RouteItemNavigatePrepareTableSystem(BaseEntity item)
    {
        switch (ProjectsEnums.GetTableSystem(Table.Name))
        {
            case ProjectsEnums.TableSystem.Accesses:
                IdentityUid = Guid.NewGuid();
                break;
            case ProjectsEnums.TableSystem.Logs:
                IdentityUid = Guid.NewGuid();
                break;
            case ProjectsEnums.TableSystem.LogTypes:
                IdentityUid = Guid.NewGuid();
                break;
            case ProjectsEnums.TableSystem.Tasks:
                IdentityUid = Guid.NewGuid();
                break;
            case ProjectsEnums.TableSystem.TasksTypes:
                IdentityUid = Guid.NewGuid();
                break;
        }
    }

    private void RouteItemNavigatePrepareTableScale(BaseEntity item)
    {
        switch (ProjectsEnums.GetTableScale(Table.Name))
        {
            case ProjectsEnums.TableScale.BarCodeTypes:
                IdentityId = AppSettings.DataAccess.Crud.GetEntity<BarCodeTypeV2Entity>(null,
                    new(DbField.IdentityId, DbOrderDirection.Desc)).IdentityId + 1;
                break;
            case ProjectsEnums.TableScale.Hosts:
                IdentityId = AppSettings.DataAccess.Crud.GetEntity<HostEntity>(null,
                    new(DbField.IdentityId, DbOrderDirection.Desc)).IdentityId + 1;
                break;
            case ProjectsEnums.TableScale.PlusObsolete:
                IdentityId = AppSettings.DataAccess.Crud.GetEntity<PluObsoleteEntity>(null,
                    new(DbField.IdentityId, DbOrderDirection.Desc)).IdentityId + 1;
                break;
            case ProjectsEnums.TableScale.Plus:
                IdentityUid = Guid.NewGuid();
                break;
            case ProjectsEnums.TableScale.PlusScales:
                IdentityUid = Guid.NewGuid();
                break;
            case ProjectsEnums.TableScale.Printers:
                IdentityId = AppSettings.DataAccess.Crud.GetEntity<PrinterEntity>(null,
                    new(DbField.IdentityId, DbOrderDirection.Desc)).IdentityId + 1;
                break;
            case ProjectsEnums.TableScale.PrintersResources:
                IdentityId = AppSettings.DataAccess.Crud.GetEntity<PrinterResourceEntity>(null,
                    new(DbField.IdentityId, DbOrderDirection.Desc)).IdentityId + 1;
                break;
            case ProjectsEnums.TableScale.PrintersTypes:
                IdentityId = AppSettings.DataAccess.Crud.GetEntity<PrinterTypeEntity>(null,
                    new(DbField.IdentityId, DbOrderDirection.Desc)).IdentityId + 1;
                break;
            case ProjectsEnums.TableScale.ProductionFacilities:
                IdentityId = AppSettings.DataAccess.Crud.GetEntity<ProductionFacilityEntity>(null,
                    new(DbField.IdentityId, DbOrderDirection.Desc)).IdentityId + 1;
                break;
            case ProjectsEnums.TableScale.ProductSeries:
                IdentityId = AppSettings.DataAccess.Crud.GetEntity<ProductSeriesEntity>(null,
                    new(DbField.IdentityId, DbOrderDirection.Desc)).IdentityId + 1;
                break;
            case ProjectsEnums.TableScale.Scales:
                IdentityId = AppSettings.DataAccess.Crud.GetEntity<ScaleEntity>(null,
                    new(DbField.IdentityId, DbOrderDirection.Desc)).IdentityId + 1;
                break;
            case ProjectsEnums.TableScale.TemplatesResources:
                IdentityId = AppSettings.DataAccess.Crud.GetEntity<TemplateResourceEntity>(null,
                    new(DbField.IdentityId, DbOrderDirection.Desc)).IdentityId + 1;
                break;
            case ProjectsEnums.TableScale.Templates:
                IdentityId = AppSettings.DataAccess.Crud.GetEntity<TemplateEntity>(null,
                    new(DbField.IdentityId, DbOrderDirection.Desc)).IdentityId + 1;
                break;
            case ProjectsEnums.TableScale.Workshops:
                IdentityId = AppSettings.DataAccess.Crud.GetEntity<WorkShopEntity>(null,
                    new(DbField.IdentityId, DbOrderDirection.Desc)).IdentityId + 1;
                break;
        }
    }

    private static void RouteItemNavigatePrepareTableDwh(BaseEntity item)
    {
        //
    }

    private void RouteItemNavigateForItem(BaseEntity? item, string page, DbTableAction tableAction)
    {
        switch (tableAction)
        {
            case DbTableAction.Copy:
            case DbTableAction.New:
                if (item == null)
                    return;
                switch (Table)
                {
                    case TableSystemEntity:
                        RouteItemNavigatePrepareTableSystem(item);
                        break;
                    case TableScaleEntity:
                        RouteItemNavigatePrepareTableScale(item);
                        break;
                    case TableDwhEntity:
                        RouteItemNavigatePrepareTableDwh(item);
                        break;
                }
                switch (AppSettings.DataAccess.Crud.GetColumnIdentity(item))
                {
                    case ColumnName.Id:
                        NavigationManager?.NavigateTo($"{page}/{item.IdentityId}/{tableAction}");
                        break;
                    case ColumnName.Uid:
                        NavigationManager?.NavigateTo($"{page}/{item.IdentityUid}/{tableAction}");
                        break;
                }
                break;
            case DbTableAction.Edit:
                if (item == null)
                    return;
                switch (AppSettings.DataAccess.Crud.GetColumnIdentity(item))
                {
                    case ColumnName.Id:
                        NavigationManager?.NavigateTo($"{page}/{item.IdentityId}");
                        break;
                    case ColumnName.Uid:
                        NavigationManager?.NavigateTo($"{page}/{item.IdentityUid}");
                        break;
                }
                break;
            case DbTableAction.Default:
                break;
            case DbTableAction.Cancel:
                break;
            case DbTableAction.Delete:
                break;
            case DbTableAction.Mark:
                break;
            case DbTableAction.Reload:
                break;
            case DbTableAction.Save:
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
        NavigationManager?.NavigateTo(LocaleData.DeviceControl.UriRouteSection.Root);
    }

    public void RouteSectionNavigate(bool isNewWindow)
    {
        string page = RouteSectionNavigatePage();
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
                if (JsRuntime != null)
                    await JsRuntime.InvokeAsync<object>("open", $"{page}", "_blank").ConfigureAwait(false);
            }).ConfigureAwait(true);
        }
    }

    private string RouteSectionNavigatePage()
    {
        string page = string.Empty;
        switch (Table)
        {
            case TableSystemEntity:
                switch (ProjectsEnums.GetTableSystem(Table.Name))
                {
                    case ProjectsEnums.TableSystem.Default:
                        break;
                    case ProjectsEnums.TableSystem.Accesses:
                        page = LocaleData.DeviceControl.UriRouteSection.Access;
                        break;
                    case ProjectsEnums.TableSystem.Logs:
                        page = LocaleData.DeviceControl.UriRouteSection.Logs;
                        break;
                    case ProjectsEnums.TableSystem.LogTypes:
                        page = LocaleData.DeviceControl.UriRouteSection.LogTypes;
                        break;
                    case ProjectsEnums.TableSystem.Tasks:
                        page = LocaleData.DeviceControl.UriRouteSection.TaskModules;
                        break;
                    case ProjectsEnums.TableSystem.TasksTypes:
                        page = LocaleData.DeviceControl.UriRouteSection.TaskTypeModules;
                        break;
                }
                break;
            case TableScaleEntity:
                switch (ProjectsEnums.GetTableScale(Table.Name))
                {
                    case ProjectsEnums.TableScale.BarCodeTypes:
                        page = LocaleData.DeviceControl.UriRouteSection.BarCodeTypes;
                        break;
                    case ProjectsEnums.TableScale.Contragents:
                        page = LocaleData.DeviceControl.UriRouteSection.Contragents;
                        break;
                    case ProjectsEnums.TableScale.Hosts:
                        page = LocaleData.DeviceControl.UriRouteSection.Hosts;
                        break;
                    case ProjectsEnums.TableScale.Labels:
                        page = LocaleData.DeviceControl.UriRouteSection.Labels;
                        break;
                    case ProjectsEnums.TableScale.Nomenclatures:
                        page = LocaleData.DeviceControl.UriRouteSection.Nomenclatures;
                        break;
                    case ProjectsEnums.TableScale.PlusObsolete:
                        page = LocaleData.DeviceControl.UriRouteSection.PlusObsolete;
                        break;
                    case ProjectsEnums.TableScale.Plus:
                        page = LocaleData.DeviceControl.UriRouteSection.Plus;
                        break;
                    case ProjectsEnums.TableScale.PlusScales:
                        page = LocaleData.DeviceControl.UriRouteSection.PlusScales;
                        break;
                    case ProjectsEnums.TableScale.PrintersResources:
                        page = LocaleData.DeviceControl.UriRouteSection.PrinterResources;
                        break;
                    case ProjectsEnums.TableScale.Printers:
                        page = LocaleData.DeviceControl.UriRouteSection.Printers;
                        break;
                    case ProjectsEnums.TableScale.PrintersTypes:
                        page = LocaleData.DeviceControl.UriRouteSection.PrinterTypes;
                        break;
                    case ProjectsEnums.TableScale.ProductionFacilities:
                        page = LocaleData.DeviceControl.UriRouteSection.ProductionFacilities;
                        break;
                    case ProjectsEnums.TableScale.Scales:
                        page = LocaleData.DeviceControl.UriRouteSection.Scales;
                        break;
                    case ProjectsEnums.TableScale.TemplatesResources:
                        page = LocaleData.DeviceControl.UriRouteSection.TemplateResources;
                        break;
                    case ProjectsEnums.TableScale.Templates:
                        page = LocaleData.DeviceControl.UriRouteSection.Templates;
                        break;
                    case ProjectsEnums.TableScale.WeithingFacts:
                        page = LocaleData.DeviceControl.UriRouteSection.WeithingFacts;
                        break;
                    case ProjectsEnums.TableScale.Workshops:
                        page = LocaleData.DeviceControl.UriRouteSection.WorkShops;
                        break;
                }
                break;
            case TableDwhEntity:
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
        if (ParentRazor?.Item != null)
        {
            switch (AppSettings.DataAccess.Crud.GetColumnIdentity(Item))
            {
                case ColumnName.Id:
                    return LocaleCore.Dialog.DialogQuestion + Environment.NewLine + $"{nameof(ParentRazor.Item.IdentityId)}: {ParentRazor.Item.IdentityId}";
                case ColumnName.Uid:
                    return LocaleCore.Dialog.DialogQuestion + Environment.NewLine + $"{nameof(ParentRazor.Item.IdentityUid)}: {ParentRazor.Item.IdentityUid}";
            }
        }
        return string.Empty;
    }

    private void ItemSystemSave(ProjectsEnums.TableSystem tableSystem)
    {
        switch (tableSystem)
        {
            case ProjectsEnums.TableSystem.Accesses:
                ItemSaveCheck.Access(NotificationService, (AccessEntity?)ParentRazor?.Item, IdentityUid, DbTableAction.Save);
                break;
            case ProjectsEnums.TableSystem.Tasks:
                ItemSaveCheck.Task(NotificationService, (TaskEntity?)ParentRazor?.Item, IdentityUid, DbTableAction.Save);
                break;
            case ProjectsEnums.TableSystem.TasksTypes:
                ItemSaveCheck.TaskType(NotificationService, (TaskTypeEntity?)ParentRazor?.Item, IdentityUid, DbTableAction.Save);
                break;
        }
    }

    private void ItemScaleSave(ProjectsEnums.TableScale tableScale)
    {
        switch (tableScale)
        {
            case ProjectsEnums.TableScale.BarCodeTypes:
                ItemSaveCheck.BarcodeType(NotificationService, (BarCodeTypeV2Entity?)ParentRazor?.Item, IdentityUid, DbTableAction.Save);
                break;
            case ProjectsEnums.TableScale.Contragents:
                ItemSaveCheck.Contragent(NotificationService, (ContragentV2Entity?)ParentRazor?.Item, IdentityUid, DbTableAction.Save);
                break;
            case ProjectsEnums.TableScale.Hosts:
                ItemSaveCheck.Host(NotificationService, (HostEntity?)ParentRazor?.Item, IdentityId, DbTableAction.Save);
                break;
            case ProjectsEnums.TableScale.Nomenclatures:
                ItemSaveCheck.Nomenclature(NotificationService, (NomenclatureEntity?)ParentRazor?.Item, IdentityId, DbTableAction.Save);
                break;
            case ProjectsEnums.TableScale.PlusObsolete:
                ItemSaveCheck.PluObsolete(NotificationService, (PluObsoleteEntity?)ParentRazor?.Item, IdentityId, DbTableAction.Save);
                break;
            case ProjectsEnums.TableScale.Plus:
                ItemSaveCheck.Plu(NotificationService, (PluEntity?)ParentRazor?.Item, IdentityUid, DbTableAction.Save);
                break;
            case ProjectsEnums.TableScale.PlusScales:
                ItemSaveCheck.PluScale(NotificationService, (PluScaleEntity?)ParentRazor?.Item, IdentityUid, DbTableAction.Save);
                break;
            case ProjectsEnums.TableScale.PrintersResources:
                ItemSaveCheck.PrinterResource(NotificationService, (PrinterResourceEntity?)ParentRazor?.Item, IdentityId, DbTableAction.Save);
                break;
            case ProjectsEnums.TableScale.Printers:
                ItemSaveCheck.Printer(NotificationService, (PrinterEntity?)ParentRazor?.Item, IdentityId, DbTableAction.Save);
                break;
            case ProjectsEnums.TableScale.PrintersTypes:
                ItemSaveCheck.PrinterType(NotificationService, (PrinterTypeEntity?)ParentRazor?.Item, IdentityId, DbTableAction.Save);
                break;
            case ProjectsEnums.TableScale.ProductionFacilities:
                ItemSaveCheck.ProductionFacility(NotificationService, (ProductionFacilityEntity?)ParentRazor?.Item, IdentityId, DbTableAction.Save);
                break;
            case ProjectsEnums.TableScale.Scales:
                ItemSaveCheck.Scale(NotificationService, (ScaleEntity?)ParentRazor?.Item, IdentityId, DbTableAction.Save);
                break;
            case ProjectsEnums.TableScale.Templates:
                ItemSaveCheck.Template(NotificationService, (TemplateEntity?)ParentRazor?.Item, IdentityId, ParentRazor?.TableAction);
                break;
            case ProjectsEnums.TableScale.TemplatesResources:
                ItemSaveCheck.TemplateResource(NotificationService, (TemplateResourceEntity?)ParentRazor?.Item, IdentityId, ParentRazor?.TableAction);
                break;
            case ProjectsEnums.TableScale.Workshops:
                ItemSaveCheck.Workshop(NotificationService, (WorkShopEntity?)ParentRazor?.Item, IdentityId, DbTableAction.Save);
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
                    case TableSystemEntity:
                        ItemSystemSave(ProjectsEnums.GetTableSystem(Table.Name));
                        break;
                    case TableScaleEntity:
                        ItemScaleSave(ProjectsEnums.GetTableScale(Table.Name));
                        break;
                }
                RouteSectionNavigate(false);
                InvokeAsync(StateHasChanged);
            });
    }

    public async Task ActionNewAsync(UserSettingsHelper userSettings, bool isNewWindow, bool isParentRazor)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (!userSettings.Identity.AccessRightsIsWrite)
            return;
        BaseEntity? item = isParentRazor ? ParentRazor?.Item : Item;

        RunActions($"{LocaleCore.Action.ActionMethod} {nameof(ActionNewAsync)}", "", LocaleCore.Dialog.DialogResultFail, "",
            () =>
            {
                throw new NotImplementedException("Fix here!");
                // Uncomment here.
                //item = new();
                //IdentityId = null;
                //IdentityUid = null;
                //RouteItemNavigate(isNewWindow, item, DbTableAction.New);
            });
    }

    public async Task ActionCopyAsync(UserSettingsHelper userSettings, bool isNewWindow, bool isParentRazor)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (!userSettings.Identity.AccessRightsIsWrite)
            return;
        BaseEntity? item = isParentRazor ? ParentRazor?.Item : Item;

        if (item == null)
            return;
        RunActions($"{LocaleCore.Action.ActionMethod} {nameof(ActionCopyAsync)}", "", LocaleCore.Dialog.DialogResultFail, "",
            () =>
            {
                RouteItemNavigate(isNewWindow, item, DbTableAction.Copy);
                InvokeAsync(StateHasChanged);
            });
    }

    public async Task ActionEditAsync(UserSettingsHelper userSettings, bool isNewWindow, bool isParentRazor)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (!userSettings.Identity.AccessRightsIsWrite)
            return;
        BaseEntity? item = isParentRazor ? ParentRazor?.Item : Item;

        if (item == null)
            return;
        RunActions($"{LocaleCore.Action.ActionMethod} {nameof(ActionEditAsync)}", "", LocaleCore.Dialog.DialogResultFail, "",
            () =>
            {
                RouteItemNavigate(isNewWindow, item, DbTableAction.Edit);
                InvokeAsync(StateHasChanged);
            });
    }

    public async Task ActionSaveAsync(UserSettingsHelper userSettings, bool isNewWindow, bool isParentRazor)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (!userSettings.Identity.AccessRightsIsWrite)
            return;
        BaseEntity? item = isParentRazor ? ParentRazor?.Item : Item;

        if (item == null)
            return;
        RunActions($"{LocaleCore.Action.ActionMethod} {nameof(ActionSaveAsync)}", "", LocaleCore.Dialog.DialogResultFail, "",
            () =>
            {
                InvokeAsync(StateHasChanged);
            });
    }

    public async Task ActionMarkAsync(UserSettingsHelper userSettings, bool isNewWindow, bool isParentRazor)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (!userSettings.Identity.AccessRightsIsWrite)
            return;
        BaseEntity? item = isParentRazor ? ParentRazor?.Item : Item;

        if (item == null)
            return;
        RunActions($"{LocaleCore.Action.ActionMethod} {nameof(ActionMarkAsync)}", "", LocaleCore.Dialog.DialogResultFail, "",
            () =>
            {
                AppSettings.DataAccess.Crud.MarkedEntity(item);
                InvokeAsync(StateHasChanged);
            });
    }

    public async Task ActionDeleteAsync(UserSettingsHelper userSettings, bool isParentRazor)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (!userSettings.Identity.AccessRightsIsWrite)
            return;
        BaseEntity? item = isParentRazor ? ParentRazor?.Item : Item;

        if (item == null)
            return;
        RunActionsWithQeustion(LocaleCore.Table.TableDelete, LocaleCore.Dialog.DialogResultSuccess,
            LocaleCore.Dialog.DialogResultFail, LocaleCore.Dialog.DialogResultCancel, GetQuestionAdd(),
            () =>
            {
                AppSettings.DataAccess.Crud.DeleteEntity(item);
                InvokeAsync(StateHasChanged);
            });
    }

    public async Task PrinterResourcesClear(UserSettingsHelper userSettings, PrinterEntity printer)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (!userSettings.Identity.AccessRightsIsWrite)
            return;

        RunActionsWithQeustion(LocaleCore.Print.ResourcesClear, LocaleCore.Dialog.DialogResultSuccess,
            LocaleCore.Dialog.DialogResultFail, LocaleCore.Dialog.DialogResultCancel, GetQuestionAdd(),
            () =>
            {
                List<TemplateResourceEntity>? items = AppSettings.DataAccess.Crud.GetEntities<TemplateResourceEntity>(
                    null, new(DbField.Description, DbOrderDirection.Asc))
                    ?.ToList();
                if (items is List<TemplateResourceEntity> templates)
                {
                    foreach (TemplateResourceEntity? resource in templates)
                    {
                        if (resource.Name.Contains("TTF"))
                        {
                            TcpClient client = ZplUtils.TcpClientSendData(printer.Ip, printer.Port,
                                new() {
                                    new($"^XA^ID"),
                                    new(resource.Name),
                                    new($"^FS^XZ")
                                });
                        }
                    }
                }
            });
    }

    public async Task PrinterResourcesLoad(UserSettingsHelper userSettings, PrinterEntity printer, string fileType)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (!userSettings.Identity.AccessRightsIsWrite)
            return;

        RunActionsWithQeustion(LocaleCore.Print.ResourcesLoadTtf, LocaleCore.Dialog.DialogResultSuccess,
            LocaleCore.Dialog.DialogResultFail, LocaleCore.Dialog.DialogResultCancel, GetQuestionAdd(),
            () =>
            {
                List<TemplateResourceEntity>? items = AppSettings.DataAccess.Crud.GetEntities<TemplateResourceEntity>(
                    null, new(DbField.Description, DbOrderDirection.Asc))
                    ?.ToList();
                if (items is { })
                {
                    foreach (TemplateResourceEntity? resource in items)
                    {
                        if (resource.Name.Contains(fileType))
                        {
                            TcpClient client = ZplUtils.TcpClientSendData(printer.Ip, printer.Port,
                                new() {
                                    new($"^XA^MNN^LL500~DYE:{resource.Name}.TTF,B,T,{resource.ImageData.Value.Length},,"),
                                    new(resource.ImageData.Value),
                                    new($"^XZ")
                                });
                        }
                    }
                }
            });
    }

    #endregion
}
