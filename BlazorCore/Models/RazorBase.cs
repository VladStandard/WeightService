// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore;
using DataProjectsCore.DAL.Models;
using DataProjectsCore.DAL.TableScaleModels;
using DataProjectsCore.DAL.TableSystemModels;
using DataProjectsCore.Models;
using DataShareCore;
using DataShareCore.DAL.Models;
using DataShareCore.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using static DataShareCore.ShareEnums;

namespace BlazorCore.Models
{
    public class RazorBase : LayoutComponentBase
    {
        #region Public and private fields and properties - Inject

        [Inject] public DialogService DialogService { get; set; }
        [Inject] public IJSRuntime JsRuntime { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public NotificationService NotificationService { get; set; }
        [Inject] public TooltipService TooltipService { get; set; }

        #endregion

        #region Public and private fields and properties - Parameter

        [Parameter] public int? Id { get; set; } = null;
        [Parameter] public Guid? Uid { get; set; } = null;
        [Parameter] public RazorBase ParentRazor { get; set; } = null;
        [Parameter] public DbTableAction TableAction { get; set; } = DbTableAction.Default;
        public BaseEntity Item { get; set; }
        [Parameter] public List<BaseEntity> Items { get; set; }
        [Parameter] public TableBase Table { get; set; }
        //[Parameter] public bool IsNewItem { get; set; }
        [Parameter] public bool IsShowNew { get; set; }
        [Parameter] public bool IsShowEdit { get; set; }
        [Parameter] public bool IsShowCopy { get; set; }
        [Parameter] public bool IsShowMark { get; set; }
        [Parameter] public bool IsShowDelete { get; set; }

        #endregion

        #region Public and private fields and properties

        public AppSettingsHelper AppSettings { get; private set; } = AppSettingsHelper.Instance;
        public UserSettingsHelper UserSettings { get; private set; } = UserSettingsHelper.Instance;
        private ItemSaveCheckEntity ItemSaveCheck { get; set; } = new ItemSaveCheckEntity();
        public object Locker { get; private set; } = new();

        #endregion

        #region Public and private methods

        //public void OnChange(object value, string name, BaseEntity item)
        public void OnChange(object value, TableBase table, BaseEntity item,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(OnChange)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async () =>
                {
                    lock (Locker)
                    {
                        switch (table)
                        {
                            case TableSystemEntity:
                                switch (ProjectsEnums.GetTableSystem(table.Name))
                                {
                                    default:
                                        break;
                                }
                                break;
                            case TableScaleEntity:
                                {
                                    switch (ProjectsEnums.GetTableScale(table.Name))
                                    {
                                        case ProjectsEnums.TableScale.Nomenclatures:
                                            if (value is int idNomenclature)
                                            {
                                                //PluItem.Nomenclature = AppSettings.DataAccess.NomenclaturesCrud.GetEntity(
                                                //    new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Id.ToString(), idNomenclature } }),
                                                //    null);
                                            }
                                            break;
                                        case ProjectsEnums.TableScale.PrinterTypes:
                                            if (Item is PrinterEntity printerItem)
                                            {
                                                if (value is int id)
                                                {
                                                    if (id <= 0)
                                                        printerItem.PrinterType = null;
                                                    else
                                                    {
                                                        printerItem.PrinterType = AppSettings.DataAccess.Crud.GetEntity<PrinterTypeEntity>(
                                                            new FieldListEntity(new Dictionary<string, object> { { DbField.Id.ToString(), id } }),
                                                        null);
                                                    }
                                                }
                                            }
                                            break;
                                        case ProjectsEnums.TableScale.Scales:
                                            if (value is int idScale)
                                            {
                                                //PluItem.Scale = AppSettings.DataAccess.ScalesCrud.GetEntity(
                                                //    new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Id.ToString(), idScale } }),
                                                //    null);
                                            }
                                            break;
                                        case ProjectsEnums.TableScale.Tasks:
                                            if (value is Guid uidTask)
                                            {
                                                //PluItem.Scale = AppSettings.DataAccess.ScalesCrud.GetEntity(
                                                //    new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Id.ToString(), idScale } }),
                                                //    null);
                                            }
                                            break;
                                        case ProjectsEnums.TableScale.Templates:
                                            if (value is int idTemplate)
                                            {
                                                //if (idTemplate <= 0)
                                                //    PluItem.Templates = null;
                                                //else
                                                //{
                                                //    PluItem.Templates = AppSettings.DataAccess.TemplatesCrud.GetEntity(
                                                //        new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Id.ToString(), idTemplate } }),
                                                //        null);
                                                //}
                                            }
                                            break;
                                    }

                                    break;
                                }

                            case TableDwhEntity:
                                switch (ProjectsEnums.GetTableDwh(table.Name))
                                {
                                    default:
                                        break;
                                }
                                break;
                        }
                    }
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        public async Task ItemSelectAsync(BaseEntity item)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(ItemSelectAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async () =>
                {
                    lock (Locker)
                    {
                        Uid = item.Uid;
                        Id = item.Id;
                    }
                    await GuiRefreshWithWaitAsync();
                }), true);
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
            await RunTasksAsync(LocalizationCore.Strings.TableRead, "", LocalizationCore.Strings.DialogResultFail, "",
                new List<Task> { task }, continueOnCapturedContext).ConfigureAwait(false);
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            if (parameters.Equals(new ParameterView()))
                return;
            await base.SetParametersAsync(parameters).ConfigureAwait(true);

            AppSettings.FontSize = parameters.TryGetValue("FontSize", out int fontSize) ? fontSize : 14;
            AppSettings.FontSizeHeader = parameters.TryGetValue("FontSizeHeader", out int fontSizeHeader) ? fontSizeHeader : 20;

            if (Id == null && ParentRazor?.Id != null)
                Id = ParentRazor.Id;
            if (Uid == null && ParentRazor?.Uid != null)
                Uid = ParentRazor.Uid;
            if (Table == null && ParentRazor?.Table != null)
                Table = ParentRazor.Table;

            switch (Table)
            {
                case TableSystemEntity:
                    SetParametersForTableSystem(parameters, ProjectsEnums.GetTableSystem(Table.Name));
                    break;
                case TableScaleEntity:
                    SetParametersForTableScale(parameters, ProjectsEnums.GetTableScale(Table.Name));
                    break;
                case TableDwhEntity:
                    SetParametersForTableDwh(parameters, ProjectsEnums.GetTableDwh(Table.Name));
                    break;
            }
        }

        private void SetParametersForTableSystem(ParameterView parameters, ProjectsEnums.TableSystem table)
        {
            switch (table)
            {
                case ProjectsEnums.TableSystem.Accesses:
                    if (parameters.TryGetValue(DbField.Uid.ToString(), out Guid? uidAccess))
                    {
                        AccessEntity accessEntity = AppSettings.DataAccess.Crud.GetEntity<AccessEntity>(
                            new FieldListEntity(new Dictionary<string, object> { { DbField.Uid.ToString(), uidAccess }, }), null);
                        Item = accessEntity;
                    }
                    break;
                case ProjectsEnums.TableSystem.Logs:
                    if (parameters.TryGetValue(DbField.Uid.ToString(), out Guid? uidLog))
                    {
                        LogEntity logEntity = AppSettings.DataAccess.Crud.GetEntity<LogEntity>(
                            new FieldListEntity(new Dictionary<string, object> { { DbField.Uid.ToString(), uidLog }, }), null);
                        Item = logEntity;
                    }
                    break;
            }
        }

        private void SetParametersForTableScale(ParameterView parameters, ProjectsEnums.TableScale table)
        {
            switch (table)
            {
                case ProjectsEnums.TableScale.BarcodeTypes:
                    if (parameters.TryGetValue(DbField.Id.ToString(), out int? idBarcodeType))
                    {
                        BarcodeTypeEntity barcodeTypeEntity = AppSettings.DataAccess.Crud.GetEntity<BarcodeTypeEntity>(
                            new FieldListEntity(new Dictionary<string, object> { { DbField.Id.ToString(), idBarcodeType }, }), null);
                        Item = barcodeTypeEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.Contragents:
                    if (parameters.TryGetValue(DbField.Id.ToString(), out int? idContragent))
                    {
                        ContragentEntity contragentEntity = AppSettings.DataAccess.Crud.GetEntity<ContragentEntity>(
                            new FieldListEntity(new Dictionary<string, object> { { DbField.Id.ToString(), idContragent }, }), null);
                        Item = contragentEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.Hosts:
                    if (parameters.TryGetValue(DbField.Id.ToString(), out int? idHost))
                    {
                        HostEntity hostEntity = AppSettings.DataAccess.Crud.GetEntity<HostEntity>(
                            new FieldListEntity(new Dictionary<string, object> { { DbField.Id.ToString(), idHost }, }), null);
                        Item = hostEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.Labels:
                    if (parameters.TryGetValue(DbField.Id.ToString(), out int? idLabel))
                    {
                        LabelEntity labelEntity = AppSettings.DataAccess.Crud.GetEntity<LabelEntity>(
                            new FieldListEntity(new Dictionary<string, object> { { DbField.Id.ToString(), idLabel }, }), null);
                        Item = labelEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.Nomenclatures:
                    if (parameters.TryGetValue(DbField.Id.ToString(), out int? idNomenclature))
                    {
                        NomenclatureEntity nomenclatureEntity = AppSettings.DataAccess.Crud.GetEntity<NomenclatureEntity>(
                            new FieldListEntity(new Dictionary<string, object> { { DbField.Id.ToString(), idNomenclature }, }), null);
                        Item = nomenclatureEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.OrderStatuses:
                    if (parameters.TryGetValue(DbField.Id.ToString(), out int? idOrderStatus))
                    {
                        OrderStatusEntity orderStatusEntity = AppSettings.DataAccess.Crud.GetEntity<OrderStatusEntity>(
                            new FieldListEntity(new Dictionary<string, object> { { DbField.Id.ToString(), idOrderStatus }, }), null);
                        Item = orderStatusEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.OrderTypes:
                    if (parameters.TryGetValue(DbField.Id.ToString(), out int? idOrderType))
                    {
                        OrderTypeEntity orderTypeEntity = AppSettings.DataAccess.Crud.GetEntity<OrderTypeEntity>(
                            new FieldListEntity(new Dictionary<string, object> { { DbField.Id.ToString(), idOrderType }, }), null);
                        Item = orderTypeEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.Orders:
                    if (parameters.TryGetValue(DbField.Id.ToString(), out int? idOrder))
                    {
                        OrderEntity orderEntity = AppSettings.DataAccess.Crud.GetEntity<OrderEntity>(
                            new FieldListEntity(new Dictionary<string, object> { { DbField.Id.ToString(), idOrder }, }), null);
                        Item = orderEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.Plus:
                    if (parameters.TryGetValue(DbField.Id.ToString(), out int? idPlu))
                    {
                        PluEntity pluEntity = AppSettings.DataAccess.Crud.GetEntity<PluEntity>(
                            new FieldListEntity(new Dictionary<string, object> { { DbField.Id.ToString(), idPlu }, }), null);
                        Item = pluEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.Printers:
                    if (parameters.TryGetValue(DbField.Id.ToString(), out int? idPrinter))
                    {
                        PrinterEntity printerEntity = AppSettings.DataAccess.Crud.GetEntity<PrinterEntity>(
                            new FieldListEntity(new Dictionary<string, object> { { DbField.Id.ToString(), idPrinter }, }), null);
                        Item = printerEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.PrinterResources:
                    if (parameters.TryGetValue(DbField.Id.ToString(), out int? idPrinterResource))
                    {
                        PrinterResourceEntity printerResourceEntity = AppSettings.DataAccess.Crud.GetEntity<PrinterResourceEntity>(
                            new FieldListEntity(new Dictionary<string, object> { { DbField.Id.ToString(), idPrinterResource }, }), null);
                        Item = printerResourceEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.PrinterTypes:
                    if (parameters.TryGetValue(DbField.Id.ToString(), out int? idPrinterType))
                    {
                        PrinterTypeEntity printerTypeEntity = AppSettings.DataAccess.Crud.GetEntity<PrinterTypeEntity>(
                            new FieldListEntity(new Dictionary<string, object> { { DbField.Id.ToString(), idPrinterType }, }), null);
                        Item = printerTypeEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.ProductSeries:
                    if (parameters.TryGetValue(DbField.Id.ToString(), out int? idProductSeries))
                    {
                        ProductSeriesEntity productSeriesEntity = AppSettings.DataAccess.Crud.GetEntity<ProductSeriesEntity>(
                            new FieldListEntity(new Dictionary<string, object> { { DbField.Id.ToString(), idProductSeries }, }), null);
                        Item = productSeriesEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.ProductionFacilities:
                    if (parameters.TryGetValue(DbField.Id.ToString(), out int? idProductionFacility))
                    {
                        ProductionFacilityEntity productionFacilityEntity = AppSettings.DataAccess.Crud.GetEntity<ProductionFacilityEntity>(
                            new FieldListEntity(new Dictionary<string, object> { { DbField.Id.ToString(), idProductionFacility }, }), null);
                        Item = productionFacilityEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.Scales:
                    if (parameters.TryGetValue(DbField.Id.ToString(), out int? idScale))
                    {
                        ScaleEntity scaleEntity = AppSettings.DataAccess.Crud.GetEntity<ScaleEntity>(
                            new FieldListEntity(new Dictionary<string, object> { { DbField.Id.ToString(), idScale }, }), null);
                        Item = scaleEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.TemplateResources:
                    if (parameters.TryGetValue(DbField.Id.ToString(), out int? idTemplateResource))
                    {
                        TemplateResourceEntity templateResourceEntity = AppSettings.DataAccess.Crud.GetEntity<TemplateResourceEntity>(
                            new FieldListEntity(new Dictionary<string, object> { { DbField.Id.ToString(), idTemplateResource }, }), null);
                        Item = templateResourceEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.Templates:
                    if (parameters.TryGetValue(DbField.Id.ToString(), out int? idTemplate))
                    {
                        TemplateEntity templateEntity = AppSettings.DataAccess.Crud.GetEntity<TemplateEntity>(
                            new FieldListEntity(new Dictionary<string, object> { { DbField.Id.ToString(), idTemplate }, }), null);
                        Item = templateEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.WeithingFacts:
                    if (parameters.TryGetValue(DbField.Id.ToString(), out int? idWeithingFact))
                    {
                        WeithingFactEntity weithingFactEntity = AppSettings.DataAccess.Crud.GetEntity<WeithingFactEntity>(
                            new FieldListEntity(new Dictionary<string, object> { { DbField.Id.ToString(), idWeithingFact }, }), null);
                        Item = weithingFactEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.Workshops:
                    if (parameters.TryGetValue(DbField.Id.ToString(), out int? idWorkshop))
                    {
                        WorkshopEntity workshopEntity = AppSettings.DataAccess.Crud.GetEntity<WorkshopEntity>(
                            new FieldListEntity(new Dictionary<string, object> { { DbField.Id.ToString(), idWorkshop }, }), null);
                        Item = workshopEntity;
                    }
                    break;
            }
        }

        private void SetParametersForTableDwh(ParameterView parameters, ProjectsEnums.TableDwh table)
        {
            switch (table)
            {
                default:
                    break;
            }
        }

        public async Task HotKeysMenuRoot()
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            NavigationManager.NavigateTo(LocalizationCore.Strings.UriRouteRoot);
        }

        public static ConfirmOptions GetConfirmOptions() => new()
        {
            OkButtonText = LocalizationCore.Strings.DialogButtonYes,
            CancelButtonText = LocalizationCore.Strings.DialogButtonCancel,
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
                NotificationService.Notify(NotificationSeverity.Success, title + Environment.NewLine, detailSuccess, AppSettingsHelper.Delay);
            else
            {
                if (!string.IsNullOrEmpty(detailCancel))
                    NotificationService.Notify(NotificationSeverity.Info, title + Environment.NewLine, detailCancel, AppSettingsHelper.Delay);
            }
        }

        private void RunTasksCatch(Exception ex, string title, string detailFail, string filePath, int lineNumber, string memberName)
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
            AppSettings.DataAccess.Crud.LogExceptionToSql(ex, filePath, lineNumber, memberName);
        }

        public void RunTasksWithQeustion(string title, string detailSuccess, string detailFail, string detailCancel, string questionAdd,
            Task task, bool continueOnCapturedContext,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                string question = string.IsNullOrEmpty(questionAdd) ? LocalizationCore.Strings.DialogQuestion : questionAdd;
                Task<bool?> dialog = DialogService.Confirm(question, title, GetConfirmOptions());
                bool? result = dialog.Result;
                if (result == true)
                {
                    RunTasks(title, detailSuccess, detailFail, detailCancel, task, continueOnCapturedContext);
                }
            }
            catch (Exception ex)
            {
                RunTasksCatch(ex, title, detailFail, filePath, lineNumber, memberName);
            }
        }

        #endregion

        #region Public and private methods - Actions

        public void RouteItemNavigate(bool isNewWindow)
        {
            string page = RouteItemNavigatePage();
            if (string.IsNullOrEmpty(page))
                return;

            if (!isNewWindow)
            {
                RouteItemNavigatePrepare();
                RouteItemNavigateWork(page);
            }
            else
            {
                RouteItemNavigateNewPage(page);
            }
        }

        private string RouteItemNavigatePage()
        {
            string page = string.Empty;
            if (Table == null)
                Table = ParentRazor.Table;
            switch (Table)
            {
                case TableSystemEntity:
                    switch (ProjectsEnums.GetTableSystem(Table.Name))
                    {
                        case ProjectsEnums.TableSystem.Logs:
                            page = LocalizationData.DeviceControl.UriRouteItem.Log;
                            break;
                    }
                    break;
                case TableScaleEntity:
                    switch (ProjectsEnums.GetTableScale(Table.Name))
                    {
                        case ProjectsEnums.TableScale.BarcodeTypes:
                            page = LocalizationData.DeviceControl.UriRouteItem.BarCodeType;
                            break;
                        case ProjectsEnums.TableScale.Contragents:
                            page = LocalizationData.DeviceControl.UriRouteItem.Contragent;
                            break;
                        case ProjectsEnums.TableScale.Hosts:
                            page = LocalizationData.DeviceControl.UriRouteItem.Host;
                            break;
                        case ProjectsEnums.TableScale.Nomenclatures:
                            page = LocalizationData.DeviceControl.UriRouteItem.Nomenclature;
                            break;
                        case ProjectsEnums.TableScale.Plus:
                            page = LocalizationData.DeviceControl.UriRouteItem.Plu;
                            break;
                        case ProjectsEnums.TableScale.PrinterResources:
                            page = LocalizationData.DeviceControl.UriRouteItem.PrinterResource;
                            break;
                        case ProjectsEnums.TableScale.Printers:
                            page = LocalizationData.DeviceControl.UriRouteItem.Printer;
                            break;
                        case ProjectsEnums.TableScale.PrinterTypes:
                            page = LocalizationData.DeviceControl.UriRouteItem.PrinterType;
                            break;
                        case ProjectsEnums.TableScale.ProductionFacilities:
                            page = LocalizationData.DeviceControl.UriRouteItem.ProductionFacility;
                            break;
                        case ProjectsEnums.TableScale.Scales:
                            page = LocalizationData.DeviceControl.UriRouteItem.Scale;
                            break;
                        case ProjectsEnums.TableScale.Tasks:
                            page = LocalizationData.DeviceControl.UriRouteItem.TaskModule;
                            break;
                        case ProjectsEnums.TableScale.TasksTypes:
                            page = LocalizationData.DeviceControl.UriRouteItem.TaskTypeModule;
                            break;
                        case ProjectsEnums.TableScale.TemplateResources:
                            page = LocalizationData.DeviceControl.UriRouteItem.TemplateResource;
                            break;
                        case ProjectsEnums.TableScale.Templates:
                            page = LocalizationData.DeviceControl.UriRouteItem.Template;
                            break;
                        case ProjectsEnums.TableScale.WeithingFacts:
                            page = LocalizationData.DeviceControl.UriRouteItem.WeithingFact;
                            break;
                        case ProjectsEnums.TableScale.Workshops:
                            page = LocalizationData.DeviceControl.UriRouteItem.Workshop;
                            break;
                    }
                    break;
            }
            return page;
        }

        private void RouteItemNavigatePrepare()
        {
            if (TableAction == DbTableAction.New)
            {
                switch (Table)
                {
                    case TableSystemEntity:
                        switch (ProjectsEnums.GetTableSystem(Table.Name))
                        {
                            default:
                                break;
                        }
                        break;
                    case TableScaleEntity:
                        {
                            int idLast;
                            switch (ProjectsEnums.GetTableScale(Table.Name))
                            {
                                case ProjectsEnums.TableScale.BarcodeTypes:
                                    idLast = AppSettings.DataAccess.Crud.GetEntity<BarcodeTypeEntity>(null, new FieldOrderEntity(DbField.Id, DbOrderDirection.Desc)).Id;
                                    Id = idLast + 1;
                                    break;
                                case ProjectsEnums.TableScale.Hosts:
                                    idLast = AppSettings.DataAccess.Crud.GetEntity<HostEntity>(null, new FieldOrderEntity(DbField.Id, DbOrderDirection.Desc)).Id;
                                    Id = idLast + 1;
                                    break;
                                case ProjectsEnums.TableScale.Plus:
                                    idLast = AppSettings.DataAccess.Crud.GetEntity<PluEntity>(null, new FieldOrderEntity(DbField.Id, DbOrderDirection.Desc)).Id;
                                    Id = idLast + 1;
                                    break;
                                case ProjectsEnums.TableScale.Printers:
                                    idLast = AppSettings.DataAccess.Crud.GetEntity<PrinterEntity>(null, new FieldOrderEntity(DbField.Id, DbOrderDirection.Desc)).Id;
                                    Id = idLast + 1;
                                    break;
                                case ProjectsEnums.TableScale.PrinterResources:
                                    idLast = AppSettings.DataAccess.Crud.GetEntity<PrinterResourceEntity>(null, new FieldOrderEntity(DbField.Id, DbOrderDirection.Desc)).Id;
                                    Id = idLast + 1;
                                    break;
                                case ProjectsEnums.TableScale.PrinterTypes:
                                    idLast = AppSettings.DataAccess.Crud.GetEntity<PrinterTypeEntity>(null, new FieldOrderEntity(DbField.Id, DbOrderDirection.Desc)).Id;
                                    Id = idLast + 1;
                                    break;
                                case ProjectsEnums.TableScale.ProductionFacilities:
                                    idLast = AppSettings.DataAccess.Crud.GetEntity<ProductionFacilityEntity>(null, new FieldOrderEntity(DbField.Id, DbOrderDirection.Desc)).Id;
                                    Id = idLast + 1;
                                    break;
                                case ProjectsEnums.TableScale.ProductSeries:
                                    idLast = AppSettings.DataAccess.Crud.GetEntity<ProductSeriesEntity>(null, new FieldOrderEntity(DbField.Id, DbOrderDirection.Desc)).Id;
                                    Id = idLast + 1;
                                    break;
                                case ProjectsEnums.TableScale.Scales:
                                    idLast = AppSettings.DataAccess.Crud.GetEntity<ScaleEntity>(null, new FieldOrderEntity(DbField.Id, DbOrderDirection.Desc)).Id;
                                    Id = idLast + 1;
                                    break;
                                case ProjectsEnums.TableScale.TemplateResources:
                                    idLast = AppSettings.DataAccess.Crud.GetEntity<TemplateResourceEntity>(null, new FieldOrderEntity(DbField.Id, DbOrderDirection.Desc)).Id;
                                    Id = idLast + 1;
                                    break;
                                case ProjectsEnums.TableScale.Templates:
                                    idLast = AppSettings.DataAccess.Crud.GetEntity<TemplateEntity>(null, new FieldOrderEntity(DbField.Id, DbOrderDirection.Desc)).Id;
                                    Id = idLast + 1;
                                    break;
                                case ProjectsEnums.TableScale.Workshops:
                                    idLast = AppSettings.DataAccess.Crud.GetEntity<WorkshopEntity>(null, new FieldOrderEntity(DbField.Id, DbOrderDirection.Desc)).Id;
                                    Id = idLast + 1;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        }
                    case TableDwhEntity:
                        switch (ProjectsEnums.GetTableDwh(Table.Name))
                        {
                            default:
                                break;
                        }
                        break;
                }
            }
        }

        private void RouteItemNavigateWork(string page)
        {
            if (Id != null)
            {
                if (TableAction == DbTableAction.New)
                    NavigationManager.NavigateTo($"{page}/{TableAction}");
                else
                    NavigationManager.NavigateTo($"{page}/{Id}");
            }
            else if (Uid != null && !Equals(Uid, Guid.Empty))
            {
                if (TableAction == DbTableAction.New)
                    NavigationManager.NavigateTo($"{page}/{TableAction}");
                else
                    NavigationManager.NavigateTo($"{page}/{Uid}");
            }
        }

        private void RouteItemNavigateNewPage(string page)
        {
            if (Uid != null && Uid != Guid.Empty)
                _ = JsRuntime.InvokeAsync<object>("open", $"{page}/{Uid}", "_blank").ConfigureAwait(false);
            else if (Id != null)
                _ = JsRuntime.InvokeAsync<object>("open", $"{page}/{Id}", "_blank").ConfigureAwait(false);
        }

        public void RouteSectionNavigate(bool isNewWindow)
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
                _ = JsRuntime.InvokeAsync<object>("open", $"{page}", "_blank").ConfigureAwait(false);
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
                        case ProjectsEnums.TableSystem.Accesses:
                            page = LocalizationData.DeviceControl.UriRouteSection.Access;
                            break;
                        case ProjectsEnums.TableSystem.Logs:
                            page = LocalizationData.DeviceControl.UriRouteSection.Logs;
                            break;
                    }
                    break;
                case TableScaleEntity:
                    switch (ProjectsEnums.GetTableScale(Table.Name))
                    {
                        case ProjectsEnums.TableScale.BarcodeTypes:
                            page = LocalizationData.DeviceControl.UriRouteSection.BarCodeTypes;
                            break;
                        case ProjectsEnums.TableScale.Contragents:
                            page = LocalizationData.DeviceControl.UriRouteSection.Contragents;
                            break;
                        case ProjectsEnums.TableScale.Hosts:
                            page = LocalizationData.DeviceControl.UriRouteSection.Hosts;
                            break;
                        case ProjectsEnums.TableScale.Nomenclatures:
                            page = LocalizationData.DeviceControl.UriRouteSection.Nomenclatures;
                            break;
                        case ProjectsEnums.TableScale.PrinterResources:
                            page = LocalizationData.DeviceControl.UriRouteSection.PrinterResources;
                            break;
                        case ProjectsEnums.TableScale.Printers:
                            page = LocalizationData.DeviceControl.UriRouteSection.Printers;
                            break;
                        case ProjectsEnums.TableScale.PrinterTypes:
                            page = LocalizationData.DeviceControl.UriRouteSection.PrinterTypes;
                            break;
                        case ProjectsEnums.TableScale.ProductionFacilities:
                            page = LocalizationData.DeviceControl.UriRouteSection.ProductionFacilities;
                            break;
                        case ProjectsEnums.TableScale.Scales:
                            page = LocalizationData.DeviceControl.UriRouteSection.Scales;
                            break;
                        case ProjectsEnums.TableScale.Tasks:
                            page = LocalizationData.DeviceControl.UriRouteSection.TaskModules;
                            break;
                        case ProjectsEnums.TableScale.TasksTypes:
                            page = LocalizationData.DeviceControl.UriRouteSection.TaskTypeModules;
                            break;
                        case ProjectsEnums.TableScale.TemplateResources:
                            page = LocalizationData.DeviceControl.UriRouteSection.TemplateResources;
                            break;
                        case ProjectsEnums.TableScale.Templates:
                            page = LocalizationData.DeviceControl.UriRouteSection.Templates;
                            break;
                        case ProjectsEnums.TableScale.WeithingFacts:
                            page = LocalizationData.DeviceControl.UriRouteSection.WeithingFacts;
                            break;
                        case ProjectsEnums.TableScale.Workshops:
                            page = LocalizationData.DeviceControl.UriRouteSection.Workshops;
                            break;
                    }
                    break;
                case TableDwhEntity:
                    break;
            }
            return page;
        }

        public async Task ItemCancelAsync(bool isNewWindow)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(ItemCancelAsync)}", LocalizationCore.Strings.DialogResultSuccess,
                LocalizationCore.Strings.DialogResultFail, LocalizationCore.Strings.DialogResultCancel,
                new Task(() =>
                {
                    RouteSectionNavigate(isNewWindow);
                }), false);
        }

        private void ItemSaveSystem(ProjectsEnums.TableSystem tableSystem)
        {
            switch (tableSystem)
            {
                case ProjectsEnums.TableSystem.Accesses:
                    break;
                case ProjectsEnums.TableSystem.Logs:
                    break;
            }
        }

        private void ItemSaveScale(ProjectsEnums.TableScale tableScale)
        {
            switch (tableScale)
            {
                case ProjectsEnums.TableScale.BarcodeTypes:
                    if (ParentRazor?.Item != null)
                        ItemSaveCheck.BarcodeType(NotificationService, (BarcodeTypeEntity)ParentRazor?.Item, (int)Id, TableAction);
                    break;
                case ProjectsEnums.TableScale.Contragents:
                    break;
                case ProjectsEnums.TableScale.Hosts:
                    if (ParentRazor?.Item != null)
                        ItemSaveCheck.Host(NotificationService, (HostEntity)ParentRazor?.Item, (int)Id, TableAction);
                    break;
                case ProjectsEnums.TableScale.Labels:
                    break;
                case ProjectsEnums.TableScale.Nomenclatures:
                    break;
                case ProjectsEnums.TableScale.Orders:
                    break;
                case ProjectsEnums.TableScale.OrderStatuses:
                    break;
                case ProjectsEnums.TableScale.OrderTypes:
                    break;
                case ProjectsEnums.TableScale.Plus:
                    if (ParentRazor?.Item != null)
                        ItemSaveCheck.Plu(NotificationService, (PluEntity)ParentRazor?.Item, (int)Id, TableAction);
                    break;
                case ProjectsEnums.TableScale.PrinterResources:
                    if (ParentRazor?.Item != null)
                        ItemSaveCheck.PrinterResource(NotificationService, (PrinterResourceEntity)ParentRazor?.Item, (int)Id, TableAction);
                    break;
                case ProjectsEnums.TableScale.Printers:
                    if (ParentRazor?.Item != null)
                        ItemSaveCheck.Printer(NotificationService, (PrinterEntity)ParentRazor?.Item, (int)Id, TableAction);
                    break;
                case ProjectsEnums.TableScale.PrinterTypes:
                    if (ParentRazor?.Item != null)
                        ItemSaveCheck.PrinterType(NotificationService, (PrinterTypeEntity)ParentRazor?.Item, (int)Id, TableAction);
                    break;
                case ProjectsEnums.TableScale.ProductionFacilities:
                    break;
                case ProjectsEnums.TableScale.ProductSeries:
                    break;
                case ProjectsEnums.TableScale.Scales:
                    if (ParentRazor?.Item != null)
                        ItemSaveCheck.Scale(NotificationService, (ScaleEntity)ParentRazor?.Item, (int)Id, TableAction);
                    break;
                case ProjectsEnums.TableScale.Tasks:
                    if (ParentRazor?.Item != null)
                        ItemSaveCheck.Task(NotificationService, (TaskEntity)ParentRazor?.Item, (Guid)Uid, TableAction);
                    break;
                case ProjectsEnums.TableScale.TasksTypes:
                    if (ParentRazor?.Item != null)
                        ItemSaveCheck.TaskType(NotificationService, (TaskTypeEntity)ParentRazor?.Item, (Guid)Uid, TableAction);
                    break;
                case ProjectsEnums.TableScale.TemplateResources:
                    break;
                case ProjectsEnums.TableScale.Templates:
                    if (ParentRazor?.Item != null)
                        ItemSaveCheck.Template(NotificationService, (TemplateEntity)ParentRazor?.Item, (int)Id, TableAction);
                    break;
                case ProjectsEnums.TableScale.WeithingFacts:
                    break;
                case ProjectsEnums.TableScale.Workshops:
                    if (ParentRazor?.Item != null)
                        ItemSaveCheck.Workshop(NotificationService, (WorkshopEntity)ParentRazor?.Item, (int)Id, TableAction);
                    break;
            }
        }

        private void ItemSaveDwh(ProjectsEnums.TableDwh tableDwh)
        {
            switch (tableDwh)
            {
                case ProjectsEnums.TableDwh.Default:
                    break;
                case ProjectsEnums.TableDwh.InformationSystem:
                    break;
                case ProjectsEnums.TableDwh.Nomenclature:
                    break;
                case ProjectsEnums.TableDwh.NomenclatureMaster:
                    break;
                case ProjectsEnums.TableDwh.NomenclatureNonNormalize:
                    break;
            }
        }

        public async Task ItemSaveAsync(bool continueOnCapturedContext, bool isNewWindow)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            RunTasksWithQeustion(LocalizationCore.Strings.TableRecordSave, LocalizationCore.Strings.DialogResultSuccess,
                LocalizationCore.Strings.DialogResultFail, LocalizationCore.Strings.DialogResultCancel, "",
                new Task(async () =>
                {
                    lock (Locker)
                    {
                        switch (Table)
                        {
                            case TableSystemEntity:
                                ItemSaveSystem(ProjectsEnums.GetTableSystem(Table.Name));
                                break;
                            case TableScaleEntity:
                                ItemSaveScale(ProjectsEnums.GetTableScale(Table.Name));
                                break;
                            case TableDwhEntity:
                                ItemSaveDwh(ProjectsEnums.GetTableDwh(Table.Name));
                                break;
                        }
                        RouteSectionNavigate(isNewWindow);
                    }
                    await GuiRefreshWithWaitAsync();
                }), continueOnCapturedContext);
        }

        public async Task ActionAsync(UserSettingsHelper userSettings, DbTableAction tableAction, bool isNewWindow)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(ActionAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async () =>
                {
                    if (userSettings.Identity.AccessLevel != true)
                        return;
                    switch (tableAction)
                    {
                        case DbTableAction.New:
                            TableAction = DbTableAction.New;
                            Id = null;
                            Uid = null;
                            RouteItemNavigate(isNewWindow);
                            break;
                        case DbTableAction.Edit:
                        case DbTableAction.Copy:
                            RouteItemNavigate(isNewWindow);
                            break;
                        case DbTableAction.Delete:
                            AppSettings.DataAccess.ActionDeleteEntity(Item);
                            break;
                        case DbTableAction.Mark:
                            AppSettings.DataAccess.ActionMarkedEntity(Item);
                            break;
                    }
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
