// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorShareCore.Models;
using DataProjectsCore.DAL.Models;
using DataProjectsCore.DAL.TableScaleModels;
using DataProjectsCore.DAL.TableSystemModels;
using DataProjectsCore.Models;
using DataCore;
using DataShareCore;
using DataShareCore.DAL.Interfaces;
using DataShareCore.DAL.Models;
using DataShareCore.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DataProjectsCore;

namespace BlazorProjectsCore.Models
{
    public class BaseRazorEntity : LayoutComponentBase, IDisposable
    {
        #region Public and private fields and properties - Inject

        [Inject] public DialogService Dialog { get; set; }
        [Inject] public IJSRuntime JsRuntime { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }
        [Inject] public NotificationService Notification { get; set; }
        [Inject] public TooltipService Tooltip { get; set; }

        #endregion

        #region Public and private fields and properties - Parameter

        public IBaseEntity Item { get; set; }
        public IBaseEntity ParentItem { get; set; }
        [Parameter] public List<IBaseEntity> Items { get; set; }
        [Parameter] public ITableEntity Table { get; set; }
        [Parameter] public bool IsShowNew { get; set; }
        [Parameter] public bool IsShowEdit { get; set; }
        [Parameter] public bool IsShowCopy { get; set; }
        [Parameter] public bool IsShowMark { get; set; }
        [Parameter] public bool IsShowDelete { get; set; }
        private readonly object _locker = new();

        #endregion

        #region Public and private fields and properties

        public AppSettingsEntity AppSettings { get; private set; } = AppSettingsEntity.Instance;

        #endregion

        #region IDisposable

        public void Dispose()
        {
            lock (_locker)
            {
                Dialog?.Dispose();
                Tooltip?.Dispose();
                AppSettings.HotKeysContextItem?.Dispose();
                
                // Disable the garbage collector from calling the destructor.
                GC.SuppressFinalize(this);
            }
        }

        #endregion

        #region Public and private methods - Item, ParentItem, Table

        public void OnChange(object value, string name, IBaseEntity item)
        {
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(Action)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async() => {
                    lock (_locker)
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
                                            printerItem.PrinterType = AppSettings.DataAccess.PrinterTypesCrud.GetEntity(
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

        public async Task ItemSelectAsync(IBaseEntity item)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(ItemSelectAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async() => {
                    lock (_locker)
                    {
                        Item = item;
                    }
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion

        #region Public and private methods

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
            {
                return;
            }

            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            AppSettings.FontSize = parameters.TryGetValue("FontSize", out int fontSize) ? fontSize : 14;
            AppSettings.FontSizeHeader = parameters.TryGetValue("FontSizeHeader", out int fontSizeHeader) ? fontSizeHeader : 20;

            if (Table is TableScaleEntity tableScale)
            {
                switch (tableScale.Value)
                {
                    case ProjectsEnums.TableScale.BarcodeTypes:
                        if (parameters.TryGetValue(ShareEnums.DbField.Id.ToString(), out int idBarcodeType))
                        {
                            BarcodeTypeEntity barcodeTypeEntity = AppSettings.DataAccess.BarcodeTypesCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { ShareEnums.DbField.Id.ToString(), idBarcodeType },
                                }), null);
                            Item = barcodeTypeEntity;
                        }
                        break;
                    case ProjectsEnums.TableScale.Contragents:
                        if (parameters.TryGetValue(ShareEnums.DbField.Id.ToString(), out int idContragent))
                        {
                            ContragentEntity contragentEntity = AppSettings.DataAccess.ContragentsCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { ShareEnums.DbField.Id.ToString(), idContragent },
                                }), null);
                            Item = contragentEntity;
                        }
                        break;
                    case ProjectsEnums.TableScale.Hosts:
                        if (parameters.TryGetValue(ShareEnums.DbField.Id.ToString(), out int idHost))
                        {
                            HostEntity hostEntity = AppSettings.DataAccess.HostsCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { ShareEnums.DbField.Id.ToString(), idHost },
                                }), null);
                            Item = hostEntity;
                        }
                        break;
                    case ProjectsEnums.TableScale.Labels:
                        if (parameters.TryGetValue(ShareEnums.DbField.Id.ToString(), out int idLabel))
                        {
                            LabelEntity labelEntity = AppSettings.DataAccess.LabelsCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { ShareEnums.DbField.Id.ToString(), idLabel },
                                }), null);
                            Item = labelEntity;
                        }
                        break;
                    case ProjectsEnums.TableScale.Nomenclatures:
                        if (parameters.TryGetValue(ShareEnums.DbField.Id.ToString(), out int idNomenclature))
                        {
                            NomenclatureEntity nomenclatureEntity = AppSettings.DataAccess.NomenclaturesCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { ShareEnums.DbField.Id.ToString(), idNomenclature },
                                }), null);
                            Item = nomenclatureEntity;
                        }
                        break;
                    case ProjectsEnums.TableScale.OrderStatuses:
                        if (parameters.TryGetValue(ShareEnums.DbField.Id.ToString(), out int idOrderStatus))
                        {
                            OrderStatusEntity orderStatusEntity = AppSettings.DataAccess.OrderStatusesCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { ShareEnums.DbField.Id.ToString(), idOrderStatus },
                                }), null);
                            Item = orderStatusEntity;
                        }
                        break;
                    case ProjectsEnums.TableScale.OrderTypes:
                        if (parameters.TryGetValue(ShareEnums.DbField.Id.ToString(), out int idOrderType))
                        {
                            OrderTypeEntity orderTypeEntity = AppSettings.DataAccess.OrderTypesCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { ShareEnums.DbField.Id.ToString(), idOrderType },
                                }), null);
                            Item = orderTypeEntity;
                        }
                        break;
                    case ProjectsEnums.TableScale.Orders:
                        if (parameters.TryGetValue(ShareEnums.DbField.Id.ToString(), out int idOrder))
                        {
                            OrderEntity orderEntity = AppSettings.DataAccess.OrdersCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { ShareEnums.DbField.Id.ToString(), idOrder },
                                }), null);
                            Item = orderEntity;
                        }
                        break;
                    case ProjectsEnums.TableScale.Plus:
                        if (parameters.TryGetValue(ShareEnums.DbField.Id.ToString(), out int idPlu))
                        {
                            PluEntity pluEntity = AppSettings.DataAccess.PlusCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { ShareEnums.DbField.Id.ToString(), idPlu },
                                }), null);
                            Item = pluEntity;
                        }
                        break;
                    case ProjectsEnums.TableScale.Printers:
                        if (parameters.TryGetValue(ShareEnums.DbField.Id.ToString(), out int idPrinter))
                        {
                            PrinterEntity printerEntity = AppSettings.DataAccess.PrintersCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { ShareEnums.DbField.Id.ToString(), idPrinter },
                                }), null);
                            Item = printerEntity;
                        }
                        break;
                    case ProjectsEnums.TableScale.PrinterResources:
                        if (parameters.TryGetValue(ShareEnums.DbField.Id.ToString(), out int idPrinterResource))
                        {
                            PrinterResourceEntity printerResourceEntity = AppSettings.DataAccess.PrinterResourcesCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { ShareEnums.DbField.Id.ToString(), idPrinterResource },
                                }), null);
                            Item = printerResourceEntity;
                        }
                        break;
                    case ProjectsEnums.TableScale.PrinterTypes:
                        if (parameters.TryGetValue(ShareEnums.DbField.Id.ToString(), out int idPrinterType))
                        {
                            PrinterTypeEntity printerTypeEntity = AppSettings.DataAccess.PrinterTypesCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { ShareEnums.DbField.Id.ToString(), idPrinterType },
                                }), null);
                            Item = printerTypeEntity;
                        }
                        break;
                    case ProjectsEnums.TableScale.ProductSeries:
                        if (parameters.TryGetValue(ShareEnums.DbField.Id.ToString(), out int idProductSeries))
                        {
                            ProductSeriesEntity productSeriesEntity = AppSettings.DataAccess.ProductSeriesCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { ShareEnums.DbField.Id.ToString(), idProductSeries },
                                }), null);
                            Item = productSeriesEntity;
                        }
                        break;
                    case ProjectsEnums.TableScale.ProductionFacilities:
                        if (parameters.TryGetValue(ShareEnums.DbField.Id.ToString(), out int idProductionFacility))
                        {
                            ProductionFacilityEntity productionFacilityEntity = AppSettings.DataAccess.ProductionFacilitiesCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { ShareEnums.DbField.Id.ToString(), idProductionFacility },
                                }), null);
                            Item = productionFacilityEntity;
                        }
                        break;
                    case ProjectsEnums.TableScale.Scales:
                        if (parameters.TryGetValue(ShareEnums.DbField.Id.ToString(), out int idScale))
                        {
                            ScaleEntity scaleEntity = AppSettings.DataAccess.ScalesCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { ShareEnums.DbField.Id.ToString(), idScale },
                                }), null);
                            Item = scaleEntity;
                        }
                        break;
                    case ProjectsEnums.TableScale.TemplateResources:
                        if (parameters.TryGetValue(ShareEnums.DbField.Id.ToString(), out int idTemplateResource))
                        {
                            TemplateResourceEntity templateResourceEntity = AppSettings.DataAccess.TemplateResourcesCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { ShareEnums.DbField.Id.ToString(), idTemplateResource },
                                }), null);
                            Item = templateResourceEntity;
                        }
                        break;
                    case ProjectsEnums.TableScale.Templates:
                        if (parameters.TryGetValue(ShareEnums.DbField.Id.ToString(), out int idTemplate))
                        {
                            TemplateEntity templateEntity = AppSettings.DataAccess.TemplatesCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { ShareEnums.DbField.Id.ToString(), idTemplate },
                                }), null);
                            Item = templateEntity;
                        }
                        break;
                    case ProjectsEnums.TableScale.WeithingFacts:
                        if (parameters.TryGetValue(ShareEnums.DbField.Id.ToString(), out int idWeithingFact))
                        {
                            WeithingFactEntity weithingFactEntity = AppSettings.DataAccess.WeithingFactsCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { ShareEnums.DbField.Id.ToString(), idWeithingFact },
                                }), null);
                            Item = weithingFactEntity;
                        }
                        break;
                    case ProjectsEnums.TableScale.Workshops:
                        if (parameters.TryGetValue(ShareEnums.DbField.Id.ToString(), out int idWorkshop))
                        {
                            WorkshopEntity workshopEntity = AppSettings.DataAccess.WorkshopsCrud.GetEntity(
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
            else if (Table is TableSystemEntity tableSystem)
            {
                switch (tableSystem.Value)
                {
                    case ProjectsEnums.TableSystem.Accesses:
                        if (parameters.TryGetValue(ShareEnums.DbField.Uid.ToString(), out Guid uidAccess))
                        {
                            AccessEntity accessEntity = AppSettings.DataAccess.AccessesCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { ShareEnums.DbField.Uid.ToString(), uidAccess },
                                }), null);
                            Item = accessEntity;
                        }
                        break;
                    case ProjectsEnums.TableSystem.Logs:
                        if (parameters.TryGetValue(ShareEnums.DbField.Uid.ToString(), out Guid uidLog))
                        {
                            LogEntity logEntity = AppSettings.DataAccess.LogsCrud.GetEntity(
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
        }

        public string ChartDataFormat(object value) => ((int)value).ToString("####", CultureInfo.InvariantCulture);

        public ChartCountEntity[] GetContragentsChartEntities(ShareEnums.DbField field)
        {
            ChartCountEntity[] result = Array.Empty<ChartCountEntity>();
            ContragentEntity[] entities = AppSettings.DataAccess.ContragentsCrud.GetEntities(null,
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
            NomenclatureEntity[] entities = AppSettings.DataAccess.NomenclaturesCrud.GetEntities(null,
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
            Navigation.NavigateTo(LocalizationCore.Strings.UriRouteRoot);
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
                Notification.Notify(NotificationSeverity.Success, title + Environment.NewLine, detailSuccess, AppSettingsEntity.Delay);
            else
            {
                if (!string.IsNullOrEmpty(detailCancel))
                    Notification.Notify(NotificationSeverity.Info, title + Environment.NewLine, detailCancel, AppSettingsEntity.Delay);
            }
        }

        private void RunTasksCatch(Exception ex, string title, string detailFail, string filePath, int lineNumber, string memberName)
        {
            // Debug log.
            if (AppSettings.IsDebug)
            {
                Console.WriteLine("--------------------------------------------------------------------------------");
                Console.WriteLine($"---------- {nameof(BaseRazorEntity)}.{nameof(RunTasksAsync)} - Catch the Exception (for Debug mode) ---------- ");
                Console.WriteLine($"filePath: {filePath}");
                Console.WriteLine($"memberName: {memberName} | lineNumber: {lineNumber}");
                Console.WriteLine($"Exception: {ex.Message}");
                if (ex.InnerException != null)
                    Console.WriteLine($"InnerException: {ex.InnerException.Message}");
                Console.WriteLine("--------------------------------------------------------------------------------");
            }
            // User log.
            string msg = ex.Message;
            if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                msg += Environment.NewLine + ex.InnerException.Message;
            if (!string.IsNullOrEmpty(detailFail))
            {
                if (!string.IsNullOrEmpty(msg))
                    Notification.Notify(NotificationSeverity.Error, title + Environment.NewLine, detailFail + Environment.NewLine + msg, AppSettingsEntity.Delay);
                else
                    Notification.Notify(NotificationSeverity.Error, title + Environment.NewLine, detailFail, AppSettingsEntity.Delay);
            }
            else
            {
                if (!string.IsNullOrEmpty(msg))
                    Notification.Notify(NotificationSeverity.Error, title + Environment.NewLine, msg, AppSettingsEntity.Delay);
            }
            // SQL log.
            AppSettings.DataAccess.LogExceptionToSql(ex, filePath, lineNumber, memberName);
        }

        public void RunTasksWithQeustion(string title, string detailSuccess, string detailFail, string detailCancel, string questionAdd,
            List<Task> tasks, bool continueOnCapturedContext,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                string question = string.IsNullOrEmpty(questionAdd) ? LocalizationCore.Strings.DialogQuestion : questionAdd;
                Task<bool?> dialog = Dialog.Confirm(question, title, GetConfirmOptions());
                bool? result = dialog.Result;
                if (result == true)
                {
                    RunTasksCore(title, detailSuccess, detailCancel, tasks, continueOnCapturedContext);
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
                RouteItemNavigateInside(page);
            }
            else
            {
                RouteItemNavigateNewPage(page);
            }
        }

        private string RouteItemNavigatePage()
        {
            string page = string.Empty;
            if (Item is PrinterEntity)
            {
                page = LocalizationData.DeviceControl.UriRouteItemPrinter;
            }
            else if (Item is PrinterTypeEntity)
            {
                page = LocalizationData.DeviceControl.UriRouteItemPrinterType;
            }
            else if (Item is PluEntity)
            {
                page = LocalizationData.DeviceControl.UriRouteItemPlu;
            }
            else if (Item is ScaleEntity)
            {
                page = LocalizationData.DeviceControl.UriRouteItemScale;
            }
            else if (Item is TaskEntity)
            {
                page = LocalizationData.DeviceControl.UriRouteItemTaskModule;
            }
            return page;
        }

        private void RouteItemNavigateInside(string page)
        {
            if (Item is BaseIdEntity idItem)
            {
                Navigation.NavigateTo($"{page}/{idItem.Id}");
            }
            else if (Item is BaseUidEntity uidItem)
            {
                Navigation.NavigateTo($"{page}/{uidItem.Uid}");
            }
            else
            {
                Navigation.NavigateTo(page);
            }
        }

        private void RouteItemNavigateNewPage(string page)
        {
            if (Item is BaseIdEntity idItem)
            {
                _ = JsRuntime.InvokeAsync<object>("open", $"{page}/{idItem.Id}", "_blank").ConfigureAwait(false);
            }
            else if (Item is BaseUidEntity uidItem)
            {
                _ = JsRuntime.InvokeAsync<object>("open", $"{page}/{uidItem.Uid}", "_blank").ConfigureAwait(false);
            }
            else
            {
                _ = JsRuntime.InvokeAsync<object>("open", $"{page}", "_blank").ConfigureAwait(false);
            }
        }

        public void RouteSectionNavigate(bool isNewWindow)
        {
            string page = RouteSectionNavigatePage();
            if (string.IsNullOrEmpty(page))
                return;

            if (!isNewWindow)
            {
                Navigation.NavigateTo(page);
            }
            else
            {
                _ = JsRuntime.InvokeAsync<object>("open", $"{page}", "_blank").ConfigureAwait(false);
            }
        }

        private string RouteSectionNavigatePage()
        {
            string page = string.Empty;
            if (Item is PrinterEntity)
            {
                page = LocalizationData.DeviceControl.UriRouteSectionPrinters;
            }
            else if (Item is PrinterTypeEntity)
            {
                page = LocalizationData.DeviceControl.UriRouteSectionPrinterTypes;
            }
            else if (Item is ScaleEntity)
            {
                page = LocalizationData.DeviceControl.UriRouteSectionScales;
            }

            return page;
        }

        public async Task ItemCancelAsync(bool isNewWindow)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(ItemCancelAsync)}", LocalizationCore.Strings.DialogResultSuccess,
                LocalizationCore.Strings.DialogResultFail, LocalizationCore.Strings.DialogResultCancel,
                new Task(() => {
                    if (Item == null)
                        return;
                    if (Item is BaseIdEntity idItem && idItem.EqualsDefault())
                        return;
                    if (Item is BaseUidEntity uidItem && uidItem.EqualsDefault())
                        return;
                    RouteSectionNavigate(isNewWindow);
                }), false);
        }

        public bool FieldControlDeny(BaseEntity item, string field)
        {
            bool result = item != null;
            if (item is BarcodeTypeEntity barCodeTypesEntity)
            {
                if (barCodeTypesEntity.EqualsDefault())
                    result = false;
            }
            else if (item is ContragentEntity contragentsEntity)
            {
                if (contragentsEntity.EqualsDefault())
                    result = false;
            }
            else if (item is HostEntity hostsEntity)
            {
                if (hostsEntity.EqualsDefault())
                    result = false;
            }
            else if (item is LabelEntity labelsEntity)
            {
                if (labelsEntity.EqualsDefault())
                    result = false;
            }
            else if (item is NomenclatureEntity nomenclatureEntity)
            {
                if (nomenclatureEntity.EqualsDefault())
                    result = false;
            }
            else if (item is OrderEntity ordersEntity)
            {
                if (ordersEntity.EqualsDefault())
                    result = false;
            }
            else if (item is OrderStatusEntity orderStatusEntity)
            {
                if (orderStatusEntity.EqualsDefault())
                    result = false;
            }
            else if (item is OrderTypeEntity orderTypesEntity)
            {
                if (orderTypesEntity.EqualsDefault())
                    result = false;
            }
            else if (item is PluEntity pluEntity)
            {
                if (pluEntity.EqualsDefault())
                    result = false;
            }
            else if (item is ProductionFacilityEntity productionFacilityEntity)
            {
                if (productionFacilityEntity.EqualsDefault())
                    result = false;
            }
            else if (item is ProductSeriesEntity productSeriesEntity)
            {
                if (productSeriesEntity.EqualsDefault())
                    result = false;
            }
            else if (item is ScaleEntity scalesEntity)
            {
                if (scalesEntity.EqualsDefault())
                    result = false;
            }
            else if (item is TaskTypeEntity taskTypeEntity)
            {
                if (taskTypeEntity.EqualsDefault())
                    result = false;
            }
            else if (item is TemplateResourceEntity templateResourcesEntity)
            {
                if (templateResourcesEntity.EqualsDefault())
                    result = false;
            }
            else if (item is TemplateEntity templatesEntity)
            {
                if (templatesEntity.EqualsDefault())
                    result = false;
            }
            else if (item is WeithingFactEntity weithingFactEntity)
            {
                if (weithingFactEntity.EqualsDefault())
                    result = false;
            }
            else if (item is WorkshopEntity workshopEntity)
            {
                if (workshopEntity.EqualsDefault())
                    result = false;
            }
            else if (item is PrinterEntity zebraPrinterEntity)
            {
                if (zebraPrinterEntity.EqualsDefault())
                    result = false;
            }
            else if (item is PrinterResourceEntity zebraPrinterResourceRefEntity)
            {
                if (zebraPrinterResourceRefEntity.EqualsDefault())
                    result = false;
            }
            else if (item is PrinterTypeEntity zebraPrinterTypeEntity)
            {
                if (zebraPrinterTypeEntity.EqualsDefault())
                    result = false;
            }
            if (!result)
            {
                NotificationMessage msg = new()
                {
                    Severity = NotificationSeverity.Warning,
                    Summary = LocalizationCore.Strings.DataControl,
                    Detail = $"{LocalizationCore.Strings.DataControlField} [{field}]!",
                    Duration = AppSettingsEntity.Delay
                };
                Notification.Notify(msg);
                return false;
            }
            return true;
        }

        public void ItemSaveCheckPrinter(PrinterEntity item)
        {
            item.CreateDate = DateTime.Now;
            item.ModifiedDate = DateTime.Now;
            bool success = FieldControlDeny(item.PrinterType, "Тип принтера");
            if (success)
            {
                if (item.Id == 0)
                    AppSettings.DataAccess.PrintersCrud.SaveEntity(item);
                else
                    AppSettings.DataAccess.PrintersCrud.UpdateEntity(item);
            }
        }

        public void ItemSaveCheckPrinterType(PrinterTypeEntity item)
        {
            bool success = true;
            if (success)
            {
                int idLast = AppSettings.DataAccess.PrinterTypesCrud.GetEntity(null, 
                    new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
                item.Id = idLast + 1;
                if (item.Id == 0)
                    AppSettings.DataAccess.PrinterTypesCrud.SaveEntity(item);
                else
                    AppSettings.DataAccess.PrinterTypesCrud.UpdateEntity(item);
            }
        }

        public void ItemSaveCheckPrinterResource(PrinterResourceEntity item)
        {
            bool success = true;
            item.CreateDate = DateTime.Now;
            item.ModifiedDate = DateTime.Now;
            if (success)
            {
                if (item.Id == 0)
                {
                    AppSettings.DataAccess.PrinterResourcesCrud.SaveEntity(item);
                }
                else
                {
                    bool existsEntity = AppSettings.DataAccess.PrinterResourcesCrud.ExistsEntity(
                        new FieldListEntity(new Dictionary<string, object>
                            {{ShareEnums.DbField.Id.ToString(), item.Id}}),
                        new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc));
                    if (existsEntity)
                    {
                        //int idLast = AppSettings.DataAccess.PrinterResourcesCrud.GetEntity(
                        //    new FieldListEntity(new Dictionary<string, object>
                        //        { { "Printer.Id", printerResourceItem.Printer.Id }}),
                        //    new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
                        //printerResourceItem.Id = idLast + 1;
                        AppSettings.DataAccess.PrinterResourcesCrud.UpdateEntity(item);
                    }
                    else
                    {
                        AppSettings.DataAccess.PrinterResourcesCrud.UpdateEntity(item);
                    }
                }
            }
        }

        public void ItemSaveCheckScale(ScaleEntity item)
        {
            item.CreateDate = DateTime.Now;
            item.ModifiedDate = DateTime.Now;
            bool success = FieldControlDeny(item.Printer, "Принтер");
            if (success)
                success = FieldControlDeny(item.Host, "Хост");
            if (success)
                success = FieldControlDeny(item.TemplateDefault, "Шаблон по-умолчанию");
            if (success)
                success = FieldControlDeny(item.WorkShop, "Цех");
            if (success)
            {
                if (item.Id == 0)
                {
                    if (item.TemplateSeries != null && item.TemplateSeries.EqualsDefault())
                        item.TemplateSeries = null;
                    AppSettings.DataAccess.ScalesCrud.SaveEntity(item);
                }
                else
                    AppSettings.DataAccess.ScalesCrud.UpdateEntity(item);
            }
        }

        public void ItemSaveCheckTask(TaskEntity item)
        {
            bool success = FieldControlDeny(item.TaskType, "Тип задачи");
            if (success)
                success = FieldControlDeny(item.Scale, "Устройство");
            if (success)
            {
                if (item.Uid == Guid.Empty)
                {
                    AppSettings.DataAccess.TaskCrud.SaveEntity(item);
                }
                else
                    AppSettings.DataAccess.TaskCrud.UpdateEntity(item);
            }
        }

        public void ItemSaveCheckTaskType(TaskTypeEntity item)
        {
            if (item.Uid == Guid.Empty)
            {
                AppSettings.DataAccess.TaskTypeCrud.SaveEntity(item);
            }
            else
                AppSettings.DataAccess.TaskTypeCrud.UpdateEntity(item);
        }

        public void ItemSaveCheck(BaseEntity item)
        {
            bool success = true;
            switch (item)
            {
                case TemplateEntity templateItem:
                    if (success)
                    {
                        if (templateItem.Id == 0)
                            AppSettings.DataAccess.TemplatesCrud.SaveEntity(templateItem);
                        else
                            AppSettings.DataAccess.TemplatesCrud.UpdateEntity(templateItem);
                    }
                    break;
                case WorkshopEntity workshopItem:
                    workshopItem.CreateDate ??= DateTime.Now;
                    workshopItem.ModifiedDate = DateTime.Now;
                    if (success)
                    {
                        if (workshopItem.Id == 0)
                            AppSettings.DataAccess.WorkshopsCrud.SaveEntity(workshopItem);
                        else
                            AppSettings.DataAccess.WorkshopsCrud.UpdateEntity(workshopItem);
                    }
                    break;
            }
        }

        public void ItemSaveCheckWorkshop(WorkshopEntity item)
        {
            //
        }

        public async Task ItemSaveAsync(bool continueOnCapturedContext, bool isNewWindow)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            RunTasksWithQeustion(LocalizationCore.Strings.TableRecordSave, LocalizationCore.Strings.DialogResultSuccess,
                LocalizationCore.Strings.DialogResultFail, LocalizationCore.Strings.DialogResultCancel, "",
                new List<Task> {
                    new(() => {
                        if (Item == null)
                            return;
                        if (Item is BaseIdEntity idItem && idItem.EqualsDefault())
                            return;
                        UidItemSaveAsync();
                        IdItemSaveAsync();
                        RouteSectionNavigate(isNewWindow);
                    })}, continueOnCapturedContext);
        }

        private void UidItemSaveAsync()
        {
            if (Item is BaseUidEntity uidItem && uidItem.EqualsDefault())
            {
                switch (uidItem)
                {
                    case TaskEntity taskItem:
                        ItemSaveCheckTask(taskItem);
                        break;
                    case TaskTypeEntity taskTypeItem:
                        ItemSaveCheckTaskType(taskTypeItem);
                        break;
                }
            }
        }

        private void IdItemSaveAsync()
        {
            if (Item is BaseIdEntity idItem2)
            {
                switch (idItem2)
                {
                    case PrinterEntity printerItem:
                        ItemSaveCheckPrinter(printerItem);
                        break;
                    case PrinterTypeEntity printerTypeItem:
                        ItemSaveCheckPrinterType(printerTypeItem);
                        break;
                    case PrinterResourceEntity printerResourceItem:
                        ItemSaveCheckPrinterResource(printerResourceItem);
                        break;
                    case ScaleEntity scaleItem:
                        ItemSaveCheckScale(scaleItem);
                        break;
                    case WorkshopEntity workshopItem:
                        ItemSaveCheckWorkshop(workshopItem);
                        break;
                    default:
                        ItemSaveCheck(idItem2);
                        break;
                }
            }
        }

        [Obsolete(@"Deprecated method. Use Action method.")]
        private async Task ActionAsync<T>(ITableEntity table, ShareEnums.DbTableAction tableAction, IBaseEntity item, IBaseEntity parentItem = null)
            where T : BaseRazorEntity
        {
            await RunTasksAsync(LocalizationCore.Strings.TableRead, "", LocalizationCore.Strings.DialogResultFail, "",
                new List<Task> { new Task(delegate {
                    Console.WriteLine($"ActionAsync. table: {table}. tableAction: {tableAction}. item: {item}");
                    string title = string.Empty;
                    if (item is BaseIdEntity idEntity)
                    {
                        if (table is TableScaleEntity tableScales)
                        {
                            idEntity = tableScales.Value switch
                            {
                                ProjectsEnums.TableScale.BarcodeTypes => AppSettings.DataAccess.ActionGetIdEntity<BarcodeTypeEntity>(idEntity, tableAction),
                                ProjectsEnums.TableScale.Contragents => AppSettings.DataAccess.ActionGetIdEntity<ContragentEntity>(idEntity, tableAction),
                                ProjectsEnums.TableScale.Hosts => AppSettings.DataAccess.ActionGetIdEntity<HostEntity>(idEntity, tableAction),
                                ProjectsEnums.TableScale.Nomenclatures => AppSettings.DataAccess.ActionGetIdEntity<NomenclatureEntity>(idEntity, tableAction),
                                ProjectsEnums.TableScale.OrderStatuses => AppSettings.DataAccess.ActionGetIdEntity<OrderStatusEntity>(idEntity, tableAction),
                                ProjectsEnums.TableScale.OrderTypes => AppSettings.DataAccess.ActionGetIdEntity<OrderTypeEntity>(idEntity, tableAction),
                                ProjectsEnums.TableScale.Plus => AppSettings.DataAccess.ActionGetIdEntity<PluEntity>(idEntity, tableAction),
                                ProjectsEnums.TableScale.ProductionFacilities => AppSettings.DataAccess.ActionGetIdEntity<ProductionFacilityEntity>(idEntity, tableAction),
                                ProjectsEnums.TableScale.ProductSeries => AppSettings.DataAccess.ActionGetIdEntity<ProductSeriesEntity>(idEntity, tableAction),
                                ProjectsEnums.TableScale.Scales => AppSettings.DataAccess.ActionGetIdEntity<ScaleEntity>(idEntity, tableAction),
                                ProjectsEnums.TableScale.TemplateResources => AppSettings.DataAccess.ActionGetIdEntity<TemplateResourceEntity>(idEntity, tableAction),
                                ProjectsEnums.TableScale.Templates => AppSettings.DataAccess.ActionGetIdEntity<TemplateEntity>(idEntity, tableAction),
                                ProjectsEnums.TableScale.Workshops => AppSettings.DataAccess.ActionGetIdEntity<WorkshopEntity>(idEntity, tableAction),
                                ProjectsEnums.TableScale.WeithingFacts => AppSettings.DataAccess.ActionGetIdEntity<WeithingFactEntity>(idEntity, tableAction),
                                ProjectsEnums.TableScale.Printers => AppSettings.DataAccess.ActionGetIdEntity<PrinterEntity>(idEntity, tableAction),
                                ProjectsEnums.TableScale.PrinterResources => AppSettings.DataAccess.ActionGetIdEntity<PrinterResourceEntity>(idEntity, tableAction),
                                ProjectsEnums.TableScale.PrinterTypes => AppSettings.DataAccess.ActionGetIdEntity<PrinterTypeEntity>(idEntity, tableAction),
                                _ => throw new ArgumentOutOfRangeException(nameof(tableAction), tableAction, null)
                            };
                        }
                        title = LocalizationData.Methods.GetItemTitle(table, idEntity.Id);
                    }
                    else if (item is BaseUidEntity uidEntity)
                    {
                        if (table is TableSystemEntity tableSystem)
                        {
                            uidEntity = tableSystem.Value switch
                            {
                                ProjectsEnums.TableSystem.Accesses => AppSettings.DataAccess.ActionGetUidEntity<AccessEntity>(uidEntity, tableAction),
                                ProjectsEnums.TableSystem.Logs => AppSettings.DataAccess.ActionGetUidEntity<LogEntity>(uidEntity, tableAction),
                                _ => throw new ArgumentOutOfRangeException(nameof(tableAction), tableAction, null)
                            };
                            title = LocalizationData.Methods.GetItemTitle(table, uidEntity.Uid);
                        }
                    }

                    // Printer from ZebraPrinter.razor.
                    if (item is PrinterResourceEntity zebraPrinterResourceRefEntity)
                    {
                        zebraPrinterResourceRefEntity.Printer = (PrinterEntity)parentItem;
                    }

                    if (tableAction == ShareEnums.DbTableAction.New)
                    {
                        if (item is PluEntity pluEntity)
                        {
                            pluEntity.Scale = (ScaleEntity)parentItem;
                        }
                    }

                    switch (tableAction)
                    {
                        case ShareEnums.DbTableAction.New:
                        case ShareEnums.DbTableAction.Edit:
                        case ShareEnums.DbTableAction.Copy:
                            if (AppSettings.IdentityItem.AccessLevel == true)
                            {
                                Console.WriteLine($"ActionAsync. AppSettings.IdentityItem.AccessLevel: {AppSettings.IdentityItem.AccessLevel}");
                                Dialog.OpenAsync<T>(title,
                                    new Dictionary<string, object>
                                    {
                                        {"Item", item},
                                        {"Table", table},
                                        {"TableAction", tableAction},
                                    },
                                    new DialogOptions() { Width = "1400px", Height = "970px" }).ConfigureAwait(false);
                            }
                            break;
                        case ShareEnums.DbTableAction.Delete:
                            if (AppSettings.IdentityItem.AccessLevel == true)
                            {
                                AppSettings.DataAccess.ActionDeleteEntity(item);
                            }
                            break;
                        case ShareEnums.DbTableAction.Mark:
                            if (AppSettings.IdentityItem.AccessLevel == true)
                            {
                                AppSettings.DataAccess.ActionMarkedEntity(item);
                            }
                            break;
                    }
                })}, true).ConfigureAwait(false);
        }

        private void Action(ShareEnums.DbTableAction tableAction, bool isNewWindow)
        {
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(Action)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async() => {
                    //if (item == null || item.EqualsDefault())
                    //    return;
                    BaseIdEntity idItem = null;
                    BaseUidEntity uidItem = null;
                    switch (Item)
                    {
                        case BaseIdEntity baseIdEntity:
                            idItem = baseIdEntity;
                            break;
                        case BaseUidEntity baseUidEntity:
                            uidItem = baseUidEntity;
                            break;
                    }
                    switch (tableAction)
                    {
                        case ShareEnums.DbTableAction.New:
                        case ShareEnums.DbTableAction.Edit:
                        case ShareEnums.DbTableAction.Copy:
                            if (AppSettings.IdentityItem.AccessLevel == true)
                            {
                                if (Table is TableScaleEntity tableScale)
                                {
                                    switch (tableScale.Value)
                                    {
                                        case ProjectsEnums.TableScale.Scales:
                                        case ProjectsEnums.TableScale.Plus:
                                        case ProjectsEnums.TableScale.Printers:
                                        case ProjectsEnums.TableScale.PrinterTypes:
                                        case ProjectsEnums.TableScale.Tasks:
                                            RouteItemNavigate(isNewWindow);
                                            break;
                                    }
                                }
                            }
                            break;
                        case ShareEnums.DbTableAction.Delete:
                            if (AppSettings.IdentityItem.AccessLevel == true)
                            {
                                AppSettings.DataAccess.ActionDeleteEntity(Item);
                            }
                            break;
                        case ShareEnums.DbTableAction.Mark:
                            if (AppSettings.IdentityItem.AccessLevel == true)
                            {
                                AppSettings.DataAccess.ActionMarkedEntity(Item);
                            }
                            break;
                    }
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        public async Task ActionNewAsync(bool isNewWindow = false)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            Action(ShareEnums.DbTableAction.New, isNewWindow);
        }

        public async Task ActionEditAsync(bool isNewWindow = false)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            Action(ShareEnums.DbTableAction.Edit, isNewWindow);
        }

        public async Task ActionCopyAsync(bool isNewWindow = false)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            Action(ShareEnums.DbTableAction.Copy, isNewWindow);
        }

        public async Task ActionMarkAsync(bool isNewWindow = false)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            Action(ShareEnums.DbTableAction.Mark, isNewWindow);
        }

        public async Task ActionDeleteAsync(bool isNewWindow = false)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            Action(ShareEnums.DbTableAction.Delete, isNewWindow);
        }

        #endregion
    }
}
