// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Sql.Models;
using DataCore.Sql.TableScaleModels;
using DataCore.Files;
using DataCore.Localizations;
using DataCore.Models;
using DataCore.Protocols;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using static DataCore.ShareEnums;

namespace BlazorCore.Models
{
    public class RazorBase : LayoutComponentBase
    {
        #region Public and private fields and properties

        [Inject] public DialogService DialogService { get; set; }
        [Inject] public IJSRuntime JsRuntime { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public NotificationService NotificationService { get; set; }
        [Inject] public TooltipService TooltipService { get; set; }
        [Parameter] public BaseEntity? ItemFilter { get; set; }
        [Parameter] public bool IsMarked { get; set; }
        [Parameter] public bool IsShowAdditionalFilter { get; set; }
        [Parameter] public bool IsShowItemsCount { get; set; }
        [Parameter] public bool IsShowMarkedFilter { get; set; }
        [Parameter] public bool IsShowMarkedItems { get; set; }
        [Parameter] public bool IsSelectTopRows { get; set; }
        [Parameter] public ButtonSettingsEntity ButtonSettings { get; set; }
        [Parameter] public DbTableAction TableAction { get; set; }
        [Parameter] public Guid? IdentityUid { get; set; }
        [Parameter] public string IdentityUidStr { get => (IdentityUid?.ToString() is string str) ? str : Guid.Empty.ToString(); set => IdentityUid = Guid.TryParse(value, out Guid uid) ? uid : Guid.Empty; }
        [Parameter] public List<BaseEntity>? Items { get; set; }
        [Parameter] public List<BaseEntity>? ItemsFilter { get; set; }
        [Parameter] public long? IdentityId { get; set; }
        [Parameter] public RazorBase? ParentRazor { get; set; }
        [Parameter] public string? FilterCaption { get; set; }
        [Parameter] public string? FilterName { get; set; }
        [Parameter] public TableBase Table { get; set; }
        private ItemSaveCheckEntity ItemSaveCheck { get; set; }
        public AppSettingsHelper AppSettings { get; private set; }
        public BaseEntity? Item { get; set; }
        public bool IsLoaded { get; set; }
        public object? ItemObject { get => Item ?? null; set => Item = (BaseEntity?)value; }
        public UserSettingsHelper UserSettings { get; private set; }

        #endregion

        #region Constructor and destructor

        public RazorBase()
        {
            ButtonSettings = new();
            FilterCaption = string.Empty;
            FilterName = string.Empty;
            IsMarked = false;
            IsShowAdditionalFilter = false;
            IsShowItemsCount = false;
            IsShowMarkedFilter = false;
            IsShowMarkedItems = false;
            IsSelectTopRows = true;
            Item = null;
            ItemFilter = null;
            Items = null;
            ItemsFilter = null;
            ParentRazor = null;
            Table = new(string.Empty);
            TableAction = DbTableAction.Default;
            IdentityId = null;
            IdentityUid = null;

            AppSettings = AppSettingsHelper.Instance;
            ItemSaveCheck = new();
            UserSettings = UserSettingsHelper.Instance;
            IsLoaded = false;
        }

        #endregion

        #region Public and private methods

        public void OnChangeCheckBox(object value, string name)
        {
            RunTasks($"{LocaleCore.Action.ActionMethod} {nameof(OnChangeCheckBox)}", "", LocaleCore.Dialog.DialogResultFail, "",
                new Task(async () =>
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
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        public void OnLocalizationValueChange(List<TypeEntity<Lang>>? templateLanguages, object? value)
        {
            RunTasks($"{LocaleCore.Action.ActionMethod} {nameof(OnItemValueChange)}", "", LocaleCore.Dialog.DialogResultFail, "",
                new Task(async () =>
                {
                    if (value is Lang lang)
                    {
                        LocaleCore.Lang = lang;
                        LocaleData.Lang = lang;
                    }
                    templateLanguages = AppSettings.DataSourceDics.GetTemplateLanguages();
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        public void OnJsonValueChange(JsonSettingsEntity? jsonSettings, string? filterName, object? value)
        {
            RunTasks($"{LocaleCore.Action.ActionMethod} {nameof(OnItemValueChange)}", "", LocaleCore.Dialog.DialogResultFail, "",
                new Task(async () =>
                {
                    switch (filterName)
                    {
                        case nameof(jsonSettings.ItemRowsCount):
                            if (value is int itemRowCount)
                                AppSettings.DataAccess.JsonSettingsLocal.ItemRowsCount = itemRowCount;
                            break;
                        case nameof(jsonSettings.SectionRowsCount):
                            if (value is int sectionRowCount)
                                AppSettings.DataAccess.JsonSettingsLocal.SectionRowsCount = sectionRowCount;
                            break;
                        case nameof(jsonSettings.Sql.DataSource):
                            if (value is string server)
                                AppSettings.DataAccess.JsonSettingsLocal.Sql.DataSource = server;
                            break;
                        case nameof(jsonSettings.Sql.InitialCatalog):
                            if (value is string db)
                                AppSettings.DataAccess.JsonSettingsLocal.Sql.InitialCatalog = db;
                            break;
                        case nameof(jsonSettings.Sql.PersistSecurityInfo):
                            if (value is bool trusted)
                                AppSettings.DataAccess.JsonSettingsLocal.Sql.PersistSecurityInfo = trusted;
                            break;
                        case nameof(jsonSettings.Sql.UserId):
                            if (value is string username)
                                AppSettings.DataAccess.JsonSettingsLocal.Sql.UserId = username;
                            break;
                        case nameof(jsonSettings.Sql.Password):
                            if (value is string password)
                                AppSettings.DataAccess.JsonSettingsLocal.Sql.Password = password;
                            break;
                    }
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        public void OnItemValueChange(BaseEntity? item, string? filterName, object? value)
        {
            RunTasks($"{LocaleCore.Action.ActionMethod} {nameof(OnItemValueChange)}", "", LocaleCore.Dialog.DialogResultFail, "",
                new Task(async () =>
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
                        case PluEntity plu:
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
                    await GuiRefreshWithWaitAsync();
                }), true);
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
                    new FieldListEntity(new Dictionary<string, object?> { { DbField.IdentityId.ToString(), printerTypeId } }),
                null);
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
                    new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IdentityId, printerId } }),
                null);
            }
            if (filterName == nameof(printerResource.Resource) && value is long resourceId)
            {
                printerResource.Resource = AppSettings.DataAccess.Crud.GetEntity<TemplateResourceEntity>(
                    new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IdentityId, resourceId } }),
                null);
            }
        }

        private void OnItemValueChangePlu(string? filterName, object? value, PluEntity plu)
        {
            if (filterName == nameof(plu.Nomenclature) && value is long nomenclatureId)
            {
                plu.Nomenclature = AppSettings.DataAccess.Crud.GetEntity<NomenclatureEntity>(
                    new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IdentityId, nomenclatureId } }),
                null);
            }
            if (filterName == nameof(plu.Scale) && value is long scaleId)
            {
                plu.Scale = AppSettings.DataAccess.Crud.GetEntity<ScaleEntity>(
                    new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IdentityId, scaleId } }),
                null);
            }
            if (filterName == nameof(plu.Template) && value is long templateId)
            {
                plu.Template = AppSettings.DataAccess.Crud.GetEntity<TemplateEntity>(
                    new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IdentityId, templateId } }),
                null);
            }
        }

        private void OnItemValueChangeScale(string? filterName, object? value, ScaleEntity scale)
        {
            if (filterName == nameof(scale.IdentityId) && value is long id)
            {
                scale = AppSettings.DataAccess.Crud.GetEntity<ScaleEntity>(
                    new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IdentityId, id } }),
                null);
            }
            if (filterName == nameof(scale.DeviceComPort) && value is string deviceComPort)
            {
                scale.DeviceComPort = deviceComPort;
            }
            if (filterName == nameof(scale.Host) && value is long hostId)
            {
                scale.Host = AppSettings.DataAccess.Crud.GetEntity<HostEntity>(
                    new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IdentityId, hostId } }),
                    null);
            }
            if (filterName == nameof(scale.TemplateDefault) && value is long templateDefaultId)
            {
                scale.TemplateDefault = AppSettings.DataAccess.Crud.GetEntity<TemplateEntity>(
                    new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IdentityId, templateDefaultId } }),
                    null);
            }
            if (filterName == nameof(scale.TemplateSeries) && value is long TemplateSeriesId)
            {
                scale.TemplateSeries = AppSettings.DataAccess.Crud.GetEntity<TemplateEntity>(
                    new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IdentityId, TemplateSeriesId } }),
                    null);
            }
            if (filterName == nameof(scale.PrinterMain) && value is long printerId)
            {
                scale.PrinterMain = AppSettings.DataAccess.Crud.GetEntity<PrinterEntity>(
                    new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IdentityId, printerId } }),
                    null);
            }
            if (filterName == nameof(scale.WorkShop) && value is long workShopId)
            {
                scale.WorkShop = AppSettings.DataAccess.Crud.GetEntity<WorkShopEntity>(
                    new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IdentityId, workShopId } }),
                    null);
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
                    new FieldListEntity(new Dictionary<string, object?> { { DbField.IdentityId.ToString(), ProductionFacilityId } }),
                null);
            }
        }

        public async Task ItemSelectAsync(BaseEntity item)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            RunTasks($"{LocaleCore.Action.ActionMethod} {nameof(ItemSelectAsync)}", "", LocaleCore.Dialog.DialogResultFail, "",
                new Task(() =>
                {
                    ItemSelect(item);
                    //await GuiRefreshWithWaitAsync();
                }), true);
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

        public async Task GuiRefreshAsync(bool continueOnCapturedContext)
        {
            await InvokeAsync(StateHasChanged).ConfigureAwait(continueOnCapturedContext);
            await Task.Delay(TimeSpan.FromMilliseconds(1000)).ConfigureAwait(true);
        }

        public async Task GuiRefreshWithWaitAsync(bool continueOnCapturedContext = true, int millisecondsTimeout = 1000)
        {
            await InvokeAsync(StateHasChanged).ConfigureAwait(continueOnCapturedContext);
            await Task.Delay(TimeSpan.FromMilliseconds(millisecondsTimeout)).ConfigureAwait(continueOnCapturedContext);
        }

        public async Task GetDataAsync(Task task, bool continueOnCapturedContext)
        {
            await RunTasksAsync(LocaleCore.Table.TableRead, "", LocaleCore.Dialog.DialogResultFail, "",
                new List<Task> { task }, continueOnCapturedContext).ConfigureAwait(false);
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            if (parameters.Equals(new ParameterView()))
                return;
            await base.SetParametersAsync(parameters).ConfigureAwait(true);

            AppSettings.FontSize = parameters.TryGetValue("FontSize", out int fontSize) ? fontSize : 14;
            AppSettings.FontSizeHeader = parameters.TryGetValue("FontSizeHeader", out int fontSizeHeader) ? fontSizeHeader : 20;

            if (IdentityId == null && ParentRazor?.IdentityId != null)
                IdentityId = ParentRazor.IdentityId;
            if (IdentityUid == null && ParentRazor?.IdentityUid != null)
                IdentityUid = ParentRazor.IdentityUid;
            if (Table == null || string.IsNullOrEmpty(Table.Name))
            {
                if (ParentRazor != null)
                {
                    if (ParentRazor.Table != null)
                        Table = ParentRazor.Table;
                }
            }
            if (TableAction == DbTableAction.Default && ParentRazor != null)
            {
                if (ParentRazor.TableAction != DbTableAction.Default)
                    TableAction = ParentRazor.TableAction;
            }

            switch (Table)
            {
                case TableSystemEntity:
                    SetParametersForTableSystem(parameters, ProjectsEnums.GetTableSystem(Table.Name));
                    break;
                case TableScaleEntity:
                    SetParametersForTableScale(parameters, ProjectsEnums.GetTableScale(Table.Name));
                    break;
                    //case TableDwhEntity:
                    //    SetParametersForTableDwh(parameters, ProjectsEnums.GetTableDwh(Table.Name));
                    //    break;
            }
        }

        private void SetParametersForTableSystem(ParameterView parameters, ProjectsEnums.TableSystem table)
        {
            switch (table)
            {
                case ProjectsEnums.TableSystem.Default:
                    break;
                case ProjectsEnums.TableSystem.Accesses:
                    if (parameters.TryGetValue(DbField.IdentityUid.ToString(), out Guid? uidAccess))
                    {
                        Item = AppSettings.DataAccess.Crud.GetEntity<AccessEntity>(
                            new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IdentityUid, uidAccess }, }), null);
                    }
                    break;
                case ProjectsEnums.TableSystem.Logs:
                    if (parameters.TryGetValue(DbField.IdentityUid.ToString(), out Guid? uidLog))
                    {
                        Item = AppSettings.DataAccess.Crud.GetEntity<LogEntity>(
                            new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IdentityUid, uidLog }, }), null);
                    }
                    break;
                case ProjectsEnums.TableSystem.Errors:
                    break;
                case ProjectsEnums.TableSystem.LogTypes:
                    break;
                case ProjectsEnums.TableSystem.Tasks:
                    break;
                case ProjectsEnums.TableSystem.TasksTypes:
                    break;
            }
        }

        private void SetParametersForTableScale(ParameterView parameters, ProjectsEnums.TableScale table)
        {
            switch (table)
            {
                case ProjectsEnums.TableScale.BarCodeTypes:
                    if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idBarcodeType))
                    {
                        Item = AppSettings.DataAccess.Crud.GetEntity<BarCodeTypeEntityV2>(
                            new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IdentityId, idBarcodeType }, }), null);
                    }
                    break;
                case ProjectsEnums.TableScale.Contragents:
                    if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idContragent))
                    {
                        Item = AppSettings.DataAccess.Crud.GetEntity<ContragentEntityV2>(
                            new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IdentityId, idContragent }, }), null);
                    }
                    break;
                case ProjectsEnums.TableScale.Hosts:
                    if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idHost))
                    {
                        Item = AppSettings.DataAccess.Crud.GetEntity<HostEntity>(
                            new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IdentityId, idHost }, }), null);
                    }
                    break;
                case ProjectsEnums.TableScale.Labels:
                    if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idLabel))
                    {
                        Item = AppSettings.DataAccess.Crud.GetEntity<LabelEntity>(
                            new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IdentityId, idLabel }, }), null);
                    }
                    break;
                case ProjectsEnums.TableScale.Nomenclatures:
                    if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idNomenclature))
                    {
                        Item = AppSettings.DataAccess.Crud.GetEntity<NomenclatureEntity>(
                            new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IdentityId, idNomenclature }, }), null);
                    }
                    break;
                case ProjectsEnums.TableScale.Orders:
                    if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idOrder))
                    {
                        Item = AppSettings.DataAccess.Crud.GetEntity<OrderEntity>(
                            new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IdentityId, idOrder }, }), null);
                    }
                    break;
                case ProjectsEnums.TableScale.OrdersStatuses:
                    if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idOrderStatus))
                    {
                        Item = AppSettings.DataAccess.Crud.GetEntity<OrderStatusEntity>(
                            new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IdentityId, idOrderStatus }, }), null);
                    }
                    break;
                case ProjectsEnums.TableScale.OrdersTypes:
                    if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idOrderType))
                    {
                        Item = AppSettings.DataAccess.Crud.GetEntity<OrderTypeEntity>(
                            new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IdentityId, idOrderType }, }), null);
                    }
                    break;
                case ProjectsEnums.TableScale.Organizations:
                    break;
                case ProjectsEnums.TableScale.Plus:
                    if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idPlu))
                    {
                        Item = AppSettings.DataAccess.Crud.GetEntity<PluEntity>(
                            new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IdentityId, idPlu }, }), null);
                    }
                    break;
                case ProjectsEnums.TableScale.Printers:
                    if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idPrinter))
                    {
                        Item = AppSettings.DataAccess.Crud.GetEntity<PrinterEntity>(
                            new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IdentityId, idPrinter }, }), null);
                    }
                    break;
                case ProjectsEnums.TableScale.PrintersResources:
                    if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idPrinterResource))
                    {
                        PrinterResourceEntity printerResourceEntity = AppSettings.DataAccess.Crud.GetEntity<PrinterResourceEntity>(
                            new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IdentityId, idPrinterResource }, }), null);
                        Item = printerResourceEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.PrintersTypes:
                    if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idPrinterType))
                    {
                        PrinterTypeEntity printerTypeEntity = AppSettings.DataAccess.Crud.GetEntity<PrinterTypeEntity>(
                            new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IdentityId, idPrinterType }, }), null);
                        Item = printerTypeEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.ProductSeries:
                    if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idProductSeries))
                    {
                        ProductSeriesEntity productSeriesEntity = AppSettings.DataAccess.Crud.GetEntity<ProductSeriesEntity>(
                            new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IdentityId, idProductSeries }, }), null);
                        Item = productSeriesEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.ProductionFacilities:
                    if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idProductionFacility))
                    {
                        ProductionFacilityEntity productionFacilityEntity = AppSettings.DataAccess.Crud.GetEntity<ProductionFacilityEntity>(
                            new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IdentityId, idProductionFacility }, }), null);
                        Item = productionFacilityEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.Scales:
                    if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idScale))
                    {
                        ScaleEntity scaleEntity = AppSettings.DataAccess.Crud.GetEntity<ScaleEntity>(
                            new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IdentityId, idScale }, }), null);
                        Item = scaleEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.TemplatesResources:
                    if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idTemplateResource))
                    {
                        TemplateResourceEntity templateResourceEntity = AppSettings.DataAccess.Crud.GetEntity<TemplateResourceEntity>(
                            new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IdentityId, idTemplateResource }, }), null);
                        Item = templateResourceEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.Templates:
                    if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idTemplate))
                    {
                        TemplateEntity templateEntity = AppSettings.DataAccess.Crud.GetEntity<TemplateEntity>(
                            new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IdentityId, idTemplate }, }), null);
                        Item = templateEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.WeithingFacts:
                    if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idWeithingFact))
                    {
                        WeithingFactEntity weithingFactEntity = AppSettings.DataAccess.Crud.GetEntity<WeithingFactEntity>(
                            new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IdentityId, idWeithingFact }, }), null);
                        Item = weithingFactEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.Workshops:
                    if (parameters.TryGetValue(DbField.IdentityId.ToString(), out long? idWorkshop))
                    {
                        WorkShopEntity workshopEntity = AppSettings.DataAccess.Crud.GetEntity<WorkShopEntity>(
                            new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IdentityId, idWorkshop }, }), null);
                        Item = workshopEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.BarCodes:
                case ProjectsEnums.TableScale.Default:
                default:
                    break;
            }
        }

        //private void SetParametersForTableDwh(ParameterView parameters, ProjectsEnums.TableDwh table)
        //{
        //    switch (table)
        //    {
        //        case ProjectsEnums.TableDwh.Default:
        //            break;
        //        case ProjectsEnums.TableDwh.InformationSystem:
        //            break;
        //        case ProjectsEnums.TableDwh.Nomenclature:
        //            break;
        //        case ProjectsEnums.TableDwh.NomenclatureMaster:
        //            break;
        //        case ProjectsEnums.TableDwh.NomenclatureNonNormalize:
        //            break;
        //    }
        //}

        public async Task HotKeysMenuRoot()
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            NavigationManager?.NavigateTo(LocaleData.DeviceControl.UriRouteSection.Root);
        }

        public static ConfirmOptions GetConfirmOptions() => new()
        {
            OkButtonText = LocaleCore.Dialog.DialogButtonYes,
            CancelButtonText = LocaleCore.Dialog.DialogButtonCancel,
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

        public async Task RunTasksAsync(string title, string detailSuccess, string detailFail, string detailCancel, List<Task> tasks,
            bool continueOnCapturedContext,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

            RunTasks(title, detailSuccess, detailFail, detailCancel, tasks, continueOnCapturedContext, filePath, lineNumber, memberName);
        }

        public void RunTasks(string title, string detailSuccess, string detailFail, string detailCancel, List<Task> tasks, bool continueOnCapturedContext,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                RunTasksCore(title, detailSuccess, detailCancel, tasks, continueOnCapturedContext);
            }
            catch (Exception ex)
            {
                RunTasksCatch(ex, title, detailFail, filePath, lineNumber, memberName);
            }
        }

        public void RunTasks(string title, string detailSuccess, string detailFail, string detailCancel, Task task, bool continueOnCapturedContext,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            RunTasks(title, detailSuccess, detailFail, detailCancel, new List<Task> { task }, continueOnCapturedContext, filePath, lineNumber, memberName);
        }

        private void RunTasksCore(string title, string detailSuccess, string detailCancel, List<Task> tasks, bool continueOnCapturedContext)
        {
            if (tasks != null)
            {
                foreach (Task task in tasks)
                {
                    if (task != null)
                    {
                        task.Start();
                        task.ConfigureAwait(continueOnCapturedContext);
                    }
                }
            }
            if (!string.IsNullOrEmpty(detailSuccess))
                NotificationService?.Notify(NotificationSeverity.Success, title + Environment.NewLine, detailSuccess, AppSettingsHelper.Delay);
            else
            {
                if (!string.IsNullOrEmpty(detailCancel))
                    NotificationService?.Notify(NotificationSeverity.Info, title + Environment.NewLine, detailCancel, AppSettingsHelper.Delay);
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
                    NotificationService?.Notify(NotificationSeverity.Error, title + Environment.NewLine, detailFail + Environment.NewLine + msg, AppSettingsHelper.Delay);
                else
                    NotificationService?.Notify(NotificationSeverity.Error, title + Environment.NewLine, detailFail, AppSettingsHelper.Delay);
            }
            else
            {
                if (!string.IsNullOrEmpty(msg))
                    NotificationService?.Notify(NotificationSeverity.Error, title + Environment.NewLine, msg, AppSettingsHelper.Delay);
            }
            // SQL log.
            AppSettings.DataAccess.Log.LogError(ex, NetUtils.GetLocalHostName(false), nameof(BlazorCore), filePath, lineNumber, memberName);
        }

        public void RunTasksWithQeustion(string title, string detailSuccess, string detailFail, string detailCancel,
            string questionAdd, Task task, bool continueOnCapturedContext,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                string question = string.IsNullOrEmpty(questionAdd)
                    ? LocaleCore.Dialog.DialogQuestion
                    : questionAdd;
                if (DialogService != null)
                {
                    Task<bool?> dialog = DialogService.Confirm(question, title, GetConfirmOptions());
                    bool? result = dialog.Result;
                    if (result == true)
                    {
                        RunTasks(title, detailSuccess, detailFail, detailCancel, task, continueOnCapturedContext);
                    }
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
                        case ProjectsEnums.TableSystem.Errors:
                            page = LocaleData.DeviceControl.UriRouteItem.Error;
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
                        case ProjectsEnums.TableScale.Plus:
                            page = LocaleData.DeviceControl.UriRouteItem.Plu;
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
                case ProjectsEnums.TableSystem.Default:
                    break;
                case ProjectsEnums.TableSystem.Accesses:
                    IdentityUid = Guid.NewGuid();
                    break;
                case ProjectsEnums.TableSystem.Errors:
                    IdentityId = AppSettings.DataAccess.Crud.GetEntity<ErrorEntity>(null,
                        new FieldOrderEntity(DbField.IdentityId, DbOrderDirection.Desc)).IdentityId + 1;
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
                    IdentityId = AppSettings.DataAccess.Crud.GetEntity<BarCodeTypeEntityV2>(null,
                        new FieldOrderEntity(DbField.IdentityId, DbOrderDirection.Desc)).IdentityId + 1;
                    break;
                case ProjectsEnums.TableScale.Hosts:
                    IdentityId = AppSettings.DataAccess.Crud.GetEntity<HostEntity>(null,
                        new FieldOrderEntity(DbField.IdentityId, DbOrderDirection.Desc)).IdentityId + 1;
                    break;
                case ProjectsEnums.TableScale.Plus:
                    IdentityId = AppSettings.DataAccess.Crud.GetEntity<PluEntity>(null,
                        new FieldOrderEntity(DbField.IdentityId, DbOrderDirection.Desc)).IdentityId + 1;
                    break;
                case ProjectsEnums.TableScale.Printers:
                    IdentityId = AppSettings.DataAccess.Crud.GetEntity<PrinterEntity>(null,
                        new FieldOrderEntity(DbField.IdentityId, DbOrderDirection.Desc)).IdentityId + 1;
                    break;
                case ProjectsEnums.TableScale.PrintersResources:
                    IdentityId = AppSettings.DataAccess.Crud.GetEntity<PrinterResourceEntity>(null,
                        new FieldOrderEntity(DbField.IdentityId, DbOrderDirection.Desc)).IdentityId + 1;
                    break;
                case ProjectsEnums.TableScale.PrintersTypes:
                    IdentityId = AppSettings.DataAccess.Crud.GetEntity<PrinterTypeEntity>(null,
                        new FieldOrderEntity(DbField.IdentityId, DbOrderDirection.Desc)).IdentityId + 1;
                    break;
                case ProjectsEnums.TableScale.ProductionFacilities:
                    IdentityId = AppSettings.DataAccess.Crud.GetEntity<ProductionFacilityEntity>(null,
                        new FieldOrderEntity(DbField.IdentityId, DbOrderDirection.Desc)).IdentityId + 1;
                    break;
                case ProjectsEnums.TableScale.ProductSeries:
                    IdentityId = AppSettings.DataAccess.Crud.GetEntity<ProductSeriesEntity>(null,
                        new FieldOrderEntity(DbField.IdentityId, DbOrderDirection.Desc)).IdentityId + 1;
                    break;
                case ProjectsEnums.TableScale.Scales:
                    IdentityId = AppSettings.DataAccess.Crud.GetEntity<ScaleEntity>(null,
                        new FieldOrderEntity(DbField.IdentityId, DbOrderDirection.Desc)).IdentityId + 1;
                    break;
                case ProjectsEnums.TableScale.TemplatesResources:
                    IdentityId = AppSettings.DataAccess.Crud.GetEntity<TemplateResourceEntity>(null,
                        new FieldOrderEntity(DbField.IdentityId, DbOrderDirection.Desc)).IdentityId + 1;
                    break;
                case ProjectsEnums.TableScale.Templates:
                    IdentityId = AppSettings.DataAccess.Crud.GetEntity<TemplateEntity>(null,
                        new FieldOrderEntity(DbField.IdentityId, DbOrderDirection.Desc)).IdentityId + 1;
                    break;
                case ProjectsEnums.TableScale.Workshops:
                    IdentityId = AppSettings.DataAccess.Crud.GetEntity<WorkShopEntity>(null,
                        new FieldOrderEntity(DbField.IdentityId, DbOrderDirection.Desc)).IdentityId + 1;
                    break;
                default:
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
                    if (item.IdentityName == ColumnName.Id)
                        NavigationManager?.NavigateTo($"{page}/{item.IdentityId}/{tableAction}");
                    else if (item.IdentityName == ColumnName.Uid)
                        NavigationManager?.NavigateTo($"{page}/{item.IdentityUid}/{tableAction}");
                    break;
                case DbTableAction.Edit:
                    if (item == null)
                        return;
                    if (item.IdentityName == ColumnName.Id)
                        NavigationManager?.NavigateTo($"{page}/{item.IdentityId}");
                    else if (item.IdentityName == ColumnName.Uid)
                        NavigationManager?.NavigateTo($"{page}/{item.IdentityUid}");
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
                if (IdentityUid != null && IdentityUid != Guid.Empty && JsRuntime != null)
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
                        case ProjectsEnums.TableSystem.Errors:
                            page = LocaleData.DeviceControl.UriRouteSection.Errors;
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
                        case ProjectsEnums.TableScale.Plus:
                            page = LocaleData.DeviceControl.UriRouteSection.Plus;
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

        public async Task ItemCancelAsync(bool continueOnCapturedContext)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            RunTasks(LocaleCore.Table.TableCancel, LocaleCore.Dialog.DialogResultSuccess,
                LocaleCore.Dialog.DialogResultFail, LocaleCore.Dialog.DialogResultCancel,
                new Task(() =>
                {
                    RouteSectionNavigate(false);
                }), continueOnCapturedContext);
        }

        private string GetQuestionAdd()
        {
            if (ParentRazor?.Item != null)
            {
                if (ParentRazor.Item.IdentityName == ColumnName.Id)
                    return LocaleCore.Dialog.DialogQuestion + Environment.NewLine + $"{nameof(ParentRazor.Item.IdentityId)}: {ParentRazor.Item.IdentityId}";
                else if (ParentRazor.Item.IdentityName == ColumnName.Uid)
                    return LocaleCore.Dialog.DialogQuestion + Environment.NewLine + $"{nameof(ParentRazor.Item.IdentityUid)}: {ParentRazor.Item.IdentityUid}";
            }
            return string.Empty;
        }

        private void ItemSystemSave(ProjectsEnums.TableSystem tableSystem)
        {
            switch (tableSystem)
            {
                case ProjectsEnums.TableSystem.Default:
                    break;
                case ProjectsEnums.TableSystem.Accesses:
                    ItemSaveCheck.Access(NotificationService, (AccessEntity?)ParentRazor?.Item, IdentityUid, DbTableAction.Save);
                    break;
                case ProjectsEnums.TableSystem.Logs:
                    break;
                case ProjectsEnums.TableSystem.Errors:
                    break;
                case ProjectsEnums.TableSystem.LogTypes:
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
                    ItemSaveCheck.BarcodeType(NotificationService, (BarCodeTypeEntityV2?)ParentRazor?.Item, IdentityUid, DbTableAction.Save);
                    break;
                case ProjectsEnums.TableScale.Contragents:
                    ItemSaveCheck.Contragent(NotificationService, (ContragentEntityV2?)ParentRazor?.Item, IdentityUid, DbTableAction.Save);
                    break;
                case ProjectsEnums.TableScale.Hosts:
                    ItemSaveCheck.Host(NotificationService, (HostEntity?)ParentRazor?.Item, IdentityId, DbTableAction.Save);
                    break;
                case ProjectsEnums.TableScale.Nomenclatures:
                    ItemSaveCheck.Nomenclature(NotificationService, (NomenclatureEntity?)ParentRazor?.Item, IdentityId, DbTableAction.Save);
                    break;
                case ProjectsEnums.TableScale.Plus:
                    ItemSaveCheck.Plu(NotificationService, (PluEntity?)ParentRazor?.Item, IdentityId, DbTableAction.Save);
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

        public async Task ItemSaveAsync(bool continueOnCapturedContext)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            RunTasksWithQeustion(LocaleCore.Table.TableSave, LocaleCore.Dialog.DialogResultSuccess,
                LocaleCore.Dialog.DialogResultFail, LocaleCore.Dialog.DialogResultCancel, GetQuestionAdd(),
                new Task(async () =>
                {
                    switch (Table)
                    {
                        case TableSystemEntity:
                            ItemSystemSave(ProjectsEnums.GetTableSystem(Table.Name));
                            break;
                        case TableScaleEntity:
                            ItemScaleSave(ProjectsEnums.GetTableScale(Table.Name));
                            break;
                        default:
                            break;
                    }
                    RouteSectionNavigate(false);
                    await GuiRefreshWithWaitAsync();
                }), continueOnCapturedContext);
        }

        public async Task ActionNewAsync(UserSettingsHelper userSettings, bool isNewWindow, bool isParentRazor)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

            if (!userSettings.Identity.AccessRightsIsWrite)
                return;
            BaseEntity? item = isParentRazor ? ParentRazor?.Item : Item;

            RunTasks($"{LocaleCore.Action.ActionMethod} {nameof(ActionNewAsync)}", "", LocaleCore.Dialog.DialogResultFail, "",
                new Task(async () =>
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
                    throw new NotImplementedException("Fix here!");
                    // Uncomment here.
                    //item = new();
                    //IdentityId = null;
                    //IdentityUid = null;
                    //RouteItemNavigate(isNewWindow, item, DbTableAction.New);
                    //await GuiRefreshWithWaitAsync();
                }), true);
        }

        public async Task ActionCopyAsync(UserSettingsHelper userSettings, bool isNewWindow, bool isParentRazor)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

            if (!userSettings.Identity.AccessRightsIsWrite)
                return;
            BaseEntity? item = isParentRazor ? ParentRazor?.Item : Item;

            if (item == null)
                return;
            RunTasks($"{LocaleCore.Action.ActionMethod} {nameof(ActionCopyAsync)}", "", LocaleCore.Dialog.DialogResultFail, "",
                new Task(async () =>
                {
                    RouteItemNavigate(isNewWindow, item, DbTableAction.Copy);
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        public async Task ActionEditAsync(UserSettingsHelper userSettings, bool isNewWindow, bool isParentRazor)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

            if (!userSettings.Identity.AccessRightsIsWrite)
                return;
            BaseEntity? item = isParentRazor ? ParentRazor?.Item : Item;

            if (item == null)
                return;
            RunTasks($"{LocaleCore.Action.ActionMethod} {nameof(ActionEditAsync)}", "", LocaleCore.Dialog.DialogResultFail, "",
                new Task(async () =>
                {
                    RouteItemNavigate(isNewWindow, item, DbTableAction.Edit);
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        public async Task ActionSaveAsync(UserSettingsHelper userSettings, bool isNewWindow, bool isParentRazor)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

            if (!userSettings.Identity.AccessRightsIsWrite)
                return;
            BaseEntity? item = isParentRazor ? ParentRazor?.Item : Item;

            if (item == null)
                return;
            RunTasks($"{LocaleCore.Action.ActionMethod} {nameof(ActionSaveAsync)}", "", LocaleCore.Dialog.DialogResultFail, "",
                new Task(async () =>
                {
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        public async Task ActionMarkAsync(UserSettingsHelper userSettings, bool isNewWindow, bool isParentRazor)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

            if (!userSettings.Identity.AccessRightsIsWrite)
                return;
            BaseEntity? item = isParentRazor ? ParentRazor?.Item : Item;

            if (item == null)
                return;
            RunTasks($"{LocaleCore.Action.ActionMethod} {nameof(ActionMarkAsync)}", "", LocaleCore.Dialog.DialogResultFail, "",
                new Task(async () =>
                {
                    AppSettings.DataAccess.Crud.MarkedEntity(item);
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        public async Task ActionDeleteAsync(UserSettingsHelper userSettings, bool isParentRazor)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

            if (!userSettings.Identity.AccessRightsIsWrite)
                return;
            BaseEntity? item = isParentRazor ? ParentRazor?.Item : Item;

            if (item == null)
                return;
            RunTasksWithQeustion(LocaleCore.Table.TableDelete, LocaleCore.Dialog.DialogResultSuccess,
                LocaleCore.Dialog.DialogResultFail, LocaleCore.Dialog.DialogResultCancel, GetQuestionAdd(),
                new Task(async () =>
                {
                    AppSettings.DataAccess.Crud.DeleteEntity(item);
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        public async Task PrinterResourcesClear(UserSettingsHelper userSettings, PrinterEntity printer)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

            if (!userSettings.Identity.AccessRightsIsWrite)
                return;

            RunTasksWithQeustion(LocaleCore.Print.ResourcesClear, LocaleCore.Dialog.DialogResultSuccess,
                LocaleCore.Dialog.DialogResultFail, LocaleCore.Dialog.DialogResultCancel, GetQuestionAdd(),
                new Task(async () =>
                {
                    List<TemplateResourceEntity>? items = AppSettings.DataAccess.Crud.GetEntities<TemplateResourceEntity>(
                        null, new FieldOrderEntity(DbField.Description, DbOrderDirection.Asc))
                        ?.ToList();
                    if (items is List<TemplateResourceEntity> templates)
                    {
                        foreach (TemplateResourceEntity? resource in templates)
                        {
                            if (resource.Name.Contains("TTF"))
                            {
                                TcpClient client = ZplUtils.TcpClientSendData(printer.Ip, printer.Port,
                                    new List<ZplExchangeEntity>() {
                                        new ZplExchangeEntity($"^XA^ID"),
                                        new ZplExchangeEntity(resource.Name),
                                        new ZplExchangeEntity($"^FS^XZ"),
                                    });
                            }
                        }
                    }
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        public async Task PrinterResourcesLoad(UserSettingsHelper userSettings, PrinterEntity printer, string fileType)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

            if (!userSettings.Identity.AccessRightsIsWrite)
                return;

            RunTasksWithQeustion(LocaleCore.Print.ResourcesLoadTtf, LocaleCore.Dialog.DialogResultSuccess,
                LocaleCore.Dialog.DialogResultFail, LocaleCore.Dialog.DialogResultCancel, GetQuestionAdd(),
                new Task(async () =>
                {
                    List<TemplateResourceEntity>? items = AppSettings.DataAccess.Crud.GetEntities<TemplateResourceEntity>(
                        null, new FieldOrderEntity(DbField.Description, DbOrderDirection.Asc))
                        ?.ToList();
                    if (items is List<TemplateResourceEntity> templates)
                    {
                        foreach (TemplateResourceEntity? resource in templates)
                        {
                            if (resource.Name.Contains(fileType))
                            {
                                TcpClient client = ZplUtils.TcpClientSendData(printer.Ip, printer.Port,
                                    new List<ZplExchangeEntity>() {
                                        new ZplExchangeEntity($"^XA^MNN^LL500~DYE:{resource.Name}.TTF,B,T,{resource.ImageData.Value.Length},,"),
                                        new ZplExchangeEntity(resource.ImageData.Value),
                                        new ZplExchangeEntity($"^XZ"),
                                    });
                            }
                        }
                    }
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
