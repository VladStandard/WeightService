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
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

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
        [Parameter] public int? ParentId { get; set; } = null;
        [Parameter] public Guid? Uid { get; set; } = null;
        [Parameter] public Guid? ParentUid { get; set; } = null;
        public BaseEntity Item { get; set; }
        [Parameter] public List<BaseEntity> Items { get; set; }
        [Parameter] public TableBase Table { get; set; }
        public ProjectsEnums.TableScale? TableScale
        {
            get
            {
                if (Table != null && Enum.TryParse(Table.Name, out ProjectsEnums.TableScale tableScale))
                    return tableScale;
                return null;
            }
        }
        [Parameter] public bool IsShowNew { get; set; }
        [Parameter] public bool IsShowEdit { get; set; }
        [Parameter] public bool IsShowCopy { get; set; }
        [Parameter] public bool IsShowMark { get; set; }
        [Parameter] public bool IsShowDelete { get; set; }
        [Parameter] public RazorBase ParentRazor { get; set; } = null;

        #endregion

        #region Public and private fields and properties

        public AppSettingsHelper AppSettings { get; private set; } = AppSettingsHelper.Instance;
        public UserSettingsHelper UserSettings { get; private set; } = UserSettingsHelper.Instance;
        private ItemSaveCheckEntity ItemSaveCheck { get; set; } = new ItemSaveCheckEntity();
        public object Locker { get; private set; } = new();

        #endregion

        #region Public and private methods

        public void OnChange(object value, string name, BaseEntity item)
        {
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(OnChange)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async () =>
                {
                    lock (Locker)
                    {
                        switch (name)
                        {
                            case nameof(PrinterTypeEntity):
                                if (item is PrinterEntity printerItem)
                                {
                                    if (value is int id)
                                    {
                                        if (id <= 0)
                                            printerItem.PrinterType = null;
                                        else
                                        {
                                            printerItem.PrinterType = AppSettings.DataAccess.Crud.GetEntity<PrinterTypeEntity>(
                                                new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Id.ToString(), id } }),
                                            null);
                                        }
                                    }
                                }
                                break;
                            case nameof(ScaleEntity):
                                if (value is int idScale)
                                {
                                    //PluItem.Scale = AppSettings.DataAccess.ScalesCrud.GetEntity(
                                    //    new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Id.ToString(), idScale } }),
                                    //    null);
                                }
                                break;
                            case nameof(TaskEntity):
                                if (value is Guid uidTask)
                                {
                                    //PluItem.Scale = AppSettings.DataAccess.ScalesCrud.GetEntity(
                                    //    new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Id.ToString(), idScale } }),
                                    //    null);
                                }
                                break;
                            case nameof(NomenclatureEntity):
                                if (value is int idNomenclature)
                                {
                                    //PluItem.Nomenclature = AppSettings.DataAccess.NomenclaturesCrud.GetEntity(
                                    //    new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Id.ToString(), idNomenclature } }),
                                    //    null);
                                }
                                break;
                            case nameof(TemplateEntity):
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

            if (Table is TableSystemEntity && Enum.TryParse(Table.Name, out ProjectsEnums.TableSystem tableSystem))
            {
                SetParametersForTableSystem(parameters, tableSystem);
            }
            if (Table is TableScaleEntity && Enum.TryParse(Table.Name, out ProjectsEnums.TableScale tableScale))
            {
                SetParametersForTableScale(parameters, tableScale);
            }
            else if (Table is TableDwhEntity && Enum.TryParse(Table.Name, out ProjectsEnums.TableDwh tableDwh))
            {
                SetParametersForTableDwh(parameters, tableDwh);
            }
        }

        private void SetParametersForTableScale(ParameterView parameters, ProjectsEnums.TableScale table)
        {
            switch (table)
            {
                case ProjectsEnums.TableScale.BarcodeTypes:
                    if (parameters.TryGetValue(ShareEnums.DbField.Id.ToString(), out int? idBarcodeType))
                    {
                        BarcodeTypeEntity barcodeTypeEntity = AppSettings.DataAccess.Crud.GetEntity<BarcodeTypeEntity>(
                            new FieldListEntity(new Dictionary<string, object> {
                                        { ShareEnums.DbField.Id.ToString(), idBarcodeType },
                            }), null);
                        Item = barcodeTypeEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.Contragents:
                    if (parameters.TryGetValue(ShareEnums.DbField.Id.ToString(), out int? idContragent))
                    {
                        ContragentEntity contragentEntity = AppSettings.DataAccess.Crud.GetEntity<ContragentEntity>(
                            new FieldListEntity(new Dictionary<string, object> {
                                        { ShareEnums.DbField.Id.ToString(), idContragent },
                            }), null);
                        Item = contragentEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.Hosts:
                    if (parameters.TryGetValue(ShareEnums.DbField.Id.ToString(), out int? idHost))
                    {
                        HostEntity hostEntity = AppSettings.DataAccess.Crud.GetEntity<HostEntity>(
                            new FieldListEntity(new Dictionary<string, object> {
                                        { ShareEnums.DbField.Id.ToString(), idHost },
                            }), null);
                        Item = hostEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.Labels:
                    if (parameters.TryGetValue(ShareEnums.DbField.Id.ToString(), out int? idLabel))
                    {
                        LabelEntity labelEntity = AppSettings.DataAccess.Crud.GetEntity<LabelEntity>(
                            new FieldListEntity(new Dictionary<string, object> {
                                        { ShareEnums.DbField.Id.ToString(), idLabel },
                            }), null);
                        Item = labelEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.Nomenclatures:
                    if (parameters.TryGetValue(ShareEnums.DbField.Id.ToString(), out int? idNomenclature))
                    {
                        NomenclatureEntity nomenclatureEntity = AppSettings.DataAccess.Crud.GetEntity<NomenclatureEntity>(
                            new FieldListEntity(new Dictionary<string, object> {
                                        { ShareEnums.DbField.Id.ToString(), idNomenclature },
                            }), null);
                        Item = nomenclatureEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.OrderStatuses:
                    if (parameters.TryGetValue(ShareEnums.DbField.Id.ToString(), out int? idOrderStatus))
                    {
                        OrderStatusEntity orderStatusEntity = AppSettings.DataAccess.OrderStatusesCrud.GetEntity<OrderStatusEntity>(
                            new FieldListEntity(new Dictionary<string, object> {
                                        { ShareEnums.DbField.Id.ToString(), idOrderStatus },
                            }), null);
                        Item = orderStatusEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.OrderTypes:
                    if (parameters.TryGetValue(ShareEnums.DbField.Id.ToString(), out int? idOrderType))
                    {
                        OrderTypeEntity orderTypeEntity = AppSettings.DataAccess.OrderTypesCrud.GetEntity<OrderTypeEntity>(
                            new FieldListEntity(new Dictionary<string, object> {
                                        { ShareEnums.DbField.Id.ToString(), idOrderType },
                            }), null);
                        Item = orderTypeEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.Orders:
                    if (parameters.TryGetValue(ShareEnums.DbField.Id.ToString(), out int? idOrder))
                    {
                        OrderEntity orderEntity = AppSettings.DataAccess.Crud.GetEntity<OrderEntity>(
                            new FieldListEntity(new Dictionary<string, object> {
                                        { ShareEnums.DbField.Id.ToString(), idOrder },
                            }), null);
                        Item = orderEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.Plus:
                    if (parameters.TryGetValue(ShareEnums.DbField.Id.ToString(), out int? idPlu))
                    {
                        PluEntity pluEntity = AppSettings.DataAccess.PlusCrud.GetEntity<PluEntity>(
                            new FieldListEntity(new Dictionary<string, object> {
                                        { ShareEnums.DbField.Id.ToString(), idPlu },
                            }), null);
                        Item = pluEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.Printers:
                    if (parameters.TryGetValue(ShareEnums.DbField.Id.ToString(), out int? idPrinter))
                    {
                        PrinterEntity printerEntity = AppSettings.DataAccess.PrintersCrud.GetEntity<PrinterEntity>(
                            new FieldListEntity(new Dictionary<string, object> {
                                        { ShareEnums.DbField.Id.ToString(), idPrinter },
                            }), null);
                        Item = printerEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.PrinterResources:
                    if (parameters.TryGetValue(ShareEnums.DbField.Id.ToString(), out int? idPrinterResource))
                    {
                        PrinterResourceEntity printerResourceEntity = AppSettings.DataAccess.PrinterResourcesCrud.GetEntity<PrinterResourceEntity>(
                            new FieldListEntity(new Dictionary<string, object> {
                                        { ShareEnums.DbField.Id.ToString(), idPrinterResource },
                            }), null);
                        Item = printerResourceEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.PrinterTypes:
                    if (parameters.TryGetValue(ShareEnums.DbField.Id.ToString(), out int? idPrinterType))
                    {
                        PrinterTypeEntity printerTypeEntity = AppSettings.DataAccess.Crud.GetEntity<PrinterTypeEntity>(
                            new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Id.ToString(), idPrinterType }, }), null);
                        Item = printerTypeEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.ProductSeries:
                    if (parameters.TryGetValue(ShareEnums.DbField.Id.ToString(), out int? idProductSeries))
                    {
                        ProductSeriesEntity productSeriesEntity = AppSettings.DataAccess.ProductSeriesCrud.GetEntity<ProductSeriesEntity>(
                            new FieldListEntity(new Dictionary<string, object> {
                                        { ShareEnums.DbField.Id.ToString(), idProductSeries },
                            }), null);
                        Item = productSeriesEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.ProductionFacilities:
                    if (parameters.TryGetValue(ShareEnums.DbField.Id.ToString(), out int? idProductionFacility))
                    {
                        ProductionFacilityEntity productionFacilityEntity = AppSettings.DataAccess.Crud.GetEntity<ProductionFacilityEntity>(
                            new FieldListEntity(new Dictionary<string, object> {
                                        { ShareEnums.DbField.Id.ToString(), idProductionFacility },
                            }), null);
                        Item = productionFacilityEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.Scales:
                    if (parameters.TryGetValue(ShareEnums.DbField.Id.ToString(), out int? idScale))
                    {
                        ScaleEntity scaleEntity = AppSettings.DataAccess.ScalesCrud.GetEntity<ScaleEntity>(
                            new FieldListEntity(new Dictionary<string, object> {
                                        { ShareEnums.DbField.Id.ToString(), idScale },
                            }), null);
                        Item = scaleEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.TemplateResources:
                    if (parameters.TryGetValue(ShareEnums.DbField.Id.ToString(), out int? idTemplateResource))
                    {
                        TemplateResourceEntity templateResourceEntity = AppSettings.DataAccess.Crud.GetEntity<TemplateResourceEntity>(
                            new FieldListEntity(new Dictionary<string, object> {
                                        { ShareEnums.DbField.Id.ToString(), idTemplateResource },
                            }), null);
                        Item = templateResourceEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.Templates:
                    if (parameters.TryGetValue(ShareEnums.DbField.Id.ToString(), out int? idTemplate))
                    {
                        TemplateEntity templateEntity = AppSettings.DataAccess.TemplatesCrud.GetEntity<TemplateEntity>(
                            new FieldListEntity(new Dictionary<string, object> {
                                        { ShareEnums.DbField.Id.ToString(), idTemplate },
                            }), null);
                        Item = templateEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.WeithingFacts:
                    if (parameters.TryGetValue(ShareEnums.DbField.Id.ToString(), out int? idWeithingFact))
                    {
                        WeithingFactEntity weithingFactEntity = AppSettings.DataAccess.WeithingFactsCrud.GetEntity<WeithingFactEntity>(
                            new FieldListEntity(new Dictionary<string, object> {
                                        { ShareEnums.DbField.Id.ToString(), idWeithingFact },
                            }), null);
                        Item = weithingFactEntity;
                    }
                    break;
                case ProjectsEnums.TableScale.Workshops:
                    if (parameters.TryGetValue(ShareEnums.DbField.Id.ToString(), out int? idWorkshop))
                    {
                        WorkshopEntity workshopEntity = AppSettings.DataAccess.WorkshopsCrud.GetEntity<WorkshopEntity>(
                            new FieldListEntity(new Dictionary<string, object> {
                                        { ShareEnums.DbField.Id.ToString(), idWorkshop },
                            }), null);
                        Item = workshopEntity;
                    }
                    break;
                default:
                    break;
            }
        }

        private void SetParametersForTableSystem(ParameterView parameters, ProjectsEnums.TableSystem table)
        {
            switch (table)
            {
                case ProjectsEnums.TableSystem.Accesses:
                    if (parameters.TryGetValue(ShareEnums.DbField.Uid.ToString(), out Guid? uidAccess))
                    {
                        AccessEntity accessEntity = AppSettings.DataAccess.Crud.GetEntity<AccessEntity>(
                            new FieldListEntity(new Dictionary<string, object> {
                                    { ShareEnums.DbField.Uid.ToString(), uidAccess },
                            }), null);
                        Item = accessEntity;
                    }
                    break;
                case ProjectsEnums.TableSystem.Logs:
                    if (parameters.TryGetValue(ShareEnums.DbField.Uid.ToString(), out Guid? uidLog))
                    {
                        LogEntity logEntity = AppSettings.DataAccess.Crud.GetEntity<LogEntity>(
                            new FieldListEntity(new Dictionary<string, object> {
                                    { ShareEnums.DbField.Uid.ToString(), uidLog },
                            }), null);
                        Item = logEntity;
                    }
                    break;
                default:
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

        public string ChartDataFormat(object value) => ((int)value).ToString("####", CultureInfo.InvariantCulture);

        public ChartCountEntity[] GetContragentsChartEntities(ShareEnums.DbField field)
        {
            ChartCountEntity[] result = Array.Empty<ChartCountEntity>();
            ContragentEntity[] entities = AppSettings.DataAccess.Crud.GetEntities<ContragentEntity>(null,
                new FieldOrderEntity(ShareEnums.DbField.CreateDate, ShareEnums.DbOrderDirection.Asc));
            int i = 0;
            switch (field)
            {
                case ShareEnums.DbField.CreateDate:
                    List<ChartCountEntity> entitiesDateCreated = new();
                    foreach (ContragentEntity entity in entities)
                    {
                        if (entity.CreateDate != null)
                            entitiesDateCreated.Add(new ChartCountEntity(((DateTime)entity.CreateDate).Date, 1));
                        i++;
                    }
                    IGrouping<DateTime, ChartCountEntity>[] entitiesGroupCreated = entitiesDateCreated.GroupBy(entity => entity.Date).ToArray();
                    result = new ChartCountEntity[entitiesGroupCreated.Length];
                    i = 0;
                    foreach (IGrouping<DateTime, ChartCountEntity> entity in entitiesGroupCreated)
                    {
                        result[i] = new ChartCountEntity(entity.Key, entity.Count());
                        i++;
                    }
                    break;
                case ShareEnums.DbField.ModifiedDate:
                    List<ChartCountEntity> entitiesDateModified = new();
                    foreach (ContragentEntity entity in entities)
                    {
                        if (entity.ModifiedDate != null)
                            entitiesDateModified.Add(new ChartCountEntity(((DateTime)entity.ModifiedDate).Date, 1));
                        i++;
                    }
                    IGrouping<DateTime, ChartCountEntity>[] entitiesGroupModified = entitiesDateModified.GroupBy(entity => entity.Date).ToArray();
                    result = new ChartCountEntity[entitiesGroupModified.Length];
                    i = 0;
                    foreach (IGrouping<DateTime, ChartCountEntity> entity in entitiesGroupModified)
                    {
                        result[i] = new ChartCountEntity(entity.Key, entity.Count());
                        i++;
                    }
                    break;
            }
            return result;
        }

        public ChartCountEntity[] GetNomenclaturesChartEntities(ShareEnums.DbField field)
        {
            ChartCountEntity[] result = Array.Empty<ChartCountEntity>();
            NomenclatureEntity[] entities = AppSettings.DataAccess.Crud.GetEntities<NomenclatureEntity>(null,
                new FieldOrderEntity(ShareEnums.DbField.CreateDate, ShareEnums.DbOrderDirection.Asc));
            int i = 0;
            switch (field)
            {
                case ShareEnums.DbField.CreateDate:
                    List<ChartCountEntity> entitiesDateCreated = new();
                    foreach (NomenclatureEntity entity in entities)
                    {
                        if (entity.CreateDate != null)
                            entitiesDateCreated.Add(new ChartCountEntity(((DateTime)entity.CreateDate).Date, 1));
                        i++;
                    }
                    IGrouping<DateTime, ChartCountEntity>[] entitiesGroupCreated = entitiesDateCreated.GroupBy(entity => entity.Date).ToArray();
                    result = new ChartCountEntity[entitiesGroupCreated.Length];
                    i = 0;
                    foreach (IGrouping<DateTime, ChartCountEntity> entity in entitiesGroupCreated)
                    {
                        result[i] = new ChartCountEntity(entity.Key, entity.Count());
                        i++;
                    }
                    break;
                case ShareEnums.DbField.ModifiedDate:
                    List<ChartCountEntity> entitiesDateModified = new();
                    foreach (NomenclatureEntity entity in entities)
                    {
                        if (entity.ModifiedDate != null)
                            entitiesDateModified.Add(new ChartCountEntity(((DateTime)entity.ModifiedDate).Date, 1));
                        i++;
                    }

                    IGrouping<DateTime, ChartCountEntity>[] entitiesModied = entitiesDateModified.GroupBy(entity => entity.Date).ToArray();
                    result = new ChartCountEntity[entitiesModied.Length];
                    i = 0;
                    foreach (IGrouping<DateTime, ChartCountEntity> entity in entitiesModied)
                    {
                        result[i] = new ChartCountEntity(entity.Key, entity.Count());
                        i++;
                    }

                    break;
            }
            return result;
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
                RouteItemNavigateInside(page);
            else
                RouteItemNavigateNewPage(page);
        }

        private string RouteItemNavigatePage()
        {
            string page = string.Empty;
            if (Table is TableSystemEntity && Enum.TryParse(Table.Name, out ProjectsEnums.TableSystem tableSystem))
            {
                switch (tableSystem)
                {
                    case ProjectsEnums.TableSystem.Logs:
                        page = LocalizationData.DeviceControl.UriRouteItem.Log;
                        break;
                }
            }
            else if (Table is TableScaleEntity && Enum.TryParse(Table.Name, out ProjectsEnums.TableScale tableScale))
            {
                switch (tableScale)
                {
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
            }
            return page;
        }

        private void RouteItemNavigateInside(string page)
        {
            if (Uid != null && Uid != Guid.Empty)
                NavigationManager.NavigateTo($"{page}/{Uid}");
            else if (Id != null)
                NavigationManager.NavigateTo($"{page}/{Id}");
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
            if (Table is TableSystemEntity && Enum.TryParse(Table.Name, out ProjectsEnums.TableSystem tableSystem))
            {
                switch (tableSystem)
                {
                    case ProjectsEnums.TableSystem.Accesses:
                        page = LocalizationData.DeviceControl.UriRouteSection.Access;
                        break;
                    case ProjectsEnums.TableSystem.Logs:
                        page = LocalizationData.DeviceControl.UriRouteSection.Logs;
                        break;
                }
            }
            else if (Table is TableScaleEntity && Enum.TryParse(Table.Name, out ProjectsEnums.TableScale tableScale))
            {
                switch (tableScale)
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
            }
            else if (Table is TableSystemEntity && Enum.TryParse(Table.Name, out ProjectsEnums.TableSystem _))
            {
                //
            }
            else if (Table is TableDwhEntity && Enum.TryParse(Table.Name, out ProjectsEnums.TableDwh _))
            {
                //
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
                    if (Item is BaseEntity baseItem && baseItem.EqualsDefault())
                        return;
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
                    break;
                case ProjectsEnums.TableScale.Contragents:
                    break;
                case ProjectsEnums.TableScale.Hosts:
                    if (ParentRazor?.Item != null)
                        ItemSaveCheck.Host(NotificationService, (HostEntity)ParentRazor?.Item);
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
                    break;
                case ProjectsEnums.TableScale.PrinterResources:
                    ItemSaveCheck.PrinterResource(NotificationService, AppSettings.DataAccess.Crud.GetEntity<PrinterResourceEntity>((int)Id));
                    break;
                case ProjectsEnums.TableScale.Printers:
                    ItemSaveCheck.Printer(NotificationService, AppSettings.DataAccess.Crud.GetEntity<PrinterEntity>((int)Id));
                    break;
                case ProjectsEnums.TableScale.PrinterTypes:
                    ItemSaveCheck.PrinterType(NotificationService, AppSettings.DataAccess.Crud.GetEntity<PrinterTypeEntity>((int)Id));
                    break;
                case ProjectsEnums.TableScale.ProductionFacilities:
                    break;
                case ProjectsEnums.TableScale.ProductSeries:
                    break;
                case ProjectsEnums.TableScale.Scales:
                    ItemSaveCheck.Scale(NotificationService, AppSettings.DataAccess.Crud.GetEntity<ScaleEntity>((int)Id));
                    break;
                case ProjectsEnums.TableScale.Tasks:
                    ItemSaveCheck.Task(NotificationService, AppSettings.DataAccess.Crud.GetEntity<TaskEntity>((Guid)Uid));
                    break;
                case ProjectsEnums.TableScale.TasksTypes:
                    ItemSaveCheck.TaskType(NotificationService, AppSettings.DataAccess.Crud.GetEntity<TaskTypeEntity>((Guid)Uid));
                    break;
                case ProjectsEnums.TableScale.TemplateResources:
                    break;
                case ProjectsEnums.TableScale.Templates:
                    ItemSaveCheck.Template(NotificationService, AppSettings.DataAccess.Crud.GetEntity<TemplateEntity>((int)Id));
                    break;
                case ProjectsEnums.TableScale.WeithingFacts:
                    break;
                case ProjectsEnums.TableScale.Workshops:
                    ItemSaveCheck.Workshop(NotificationService, AppSettings.DataAccess.Crud.GetEntity<WorkshopEntity>((int)Id));
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
                        if (Table is TableSystemEntity && Enum.TryParse(Table.Name, out ProjectsEnums.TableSystem tableSystem))
                        {
                            ItemSaveSystem(tableSystem);
                        }
                        else if (Table is TableScaleEntity && Enum.TryParse(Table.Name, out ProjectsEnums.TableScale tableScalse))
                        {
                            ItemSaveScale(tableScalse);
                        }
                        else if (Table is TableDwhEntity && Enum.TryParse(Table.Name, out ProjectsEnums.TableDwh tableDwh))
                        {
                            ItemSaveDwh(tableDwh);
                        }
                        RouteSectionNavigate(isNewWindow);
                    }
                    await GuiRefreshWithWaitAsync();
                }), continueOnCapturedContext);
        }

        public async Task ActionAsync(UserSettingsHelper userSettings, ShareEnums.DbTableAction tableAction, bool isNewWindow)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(ActionAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async () =>
                {
                    switch (tableAction)
                    {
                        case ShareEnums.DbTableAction.New:
                        case ShareEnums.DbTableAction.Edit:
                        case ShareEnums.DbTableAction.Copy:
                            if (userSettings.Identity.AccessLevel == true)
                            {
                                if (Enum.TryParse(Table?.Name, out ProjectsEnums.TableScale tableScale))
                                {
                                    switch (tableScale)
                                    {
                                        case ProjectsEnums.TableScale.Scales:
                                        case ProjectsEnums.TableScale.Plus:
                                        case ProjectsEnums.TableScale.Printers:
                                        case ProjectsEnums.TableScale.PrinterTypes:
                                        case ProjectsEnums.TableScale.Tasks:
                                        case ProjectsEnums.TableScale.TasksTypes:
                                        case ProjectsEnums.TableScale.Hosts:
                                            RouteItemNavigate(isNewWindow);
                                            break;
                                    }
                                }
                            }
                            break;
                        case ShareEnums.DbTableAction.Delete:
                            if (userSettings.Identity.AccessLevel == true)
                            {
                                if (Item is BaseEntity baseItem)
                                    AppSettings.DataAccess.ActionDeleteEntity(baseItem);
                            }
                            break;
                        case ShareEnums.DbTableAction.Mark:
                            if (userSettings.Identity.AccessLevel == true)
                            {
                                if (Item is BaseEntity baseItem)
                                    AppSettings.DataAccess.ActionMarkedEntity(baseItem);
                            }
                            break;
                    }
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
