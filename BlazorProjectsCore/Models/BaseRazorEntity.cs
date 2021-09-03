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

        public IBaseEntity Item { get; private set; }
        public IBaseEntity ParentItem { get; private set; }
        [Parameter] public ITableEntity Table { get; set; }
        [Parameter] public bool IsShowNew { get; set; }
        [Parameter] public bool IsShowEdit { get; set; }
        [Parameter] public bool IsShowCopy { get; set; }
        [Parameter] public bool IsShowMark { get; set; }
        [Parameter] public bool IsShowDelete { get; set; }
        [Parameter] public int ItemsCount { get; set; }

        #endregion

        #region Public and private fields and properties

        public AppSettingsEntity AppSettings = AppSettingsEntity.Instance;

        #endregion

        #region IDisposable

        public void Dispose()
        {
            Dialog?.Dispose();
            Tooltip?.Dispose();
            AppSettings.HotKeysContextItem?.Dispose();
            GC.Collect();
        }

        #endregion

        #region Constructor and destructor

        public BaseRazorEntity() { }

        #endregion

        #region Public and private methods - Item, ParentItem, Table

        public void SetItem()
        {
            Item = null;
        }

        public void SetItem(IBaseEntity item)
        {
            Item = item;
        }

        public void SetParentItem(IBaseEntity parentItem)
        {
            ParentItem = parentItem;
        }

        public void OnChange(object value, string name, IBaseEntity item)
        {
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(Action)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new List<Task> {
                    new(async() => {
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
                                                new FieldListEntity(new Dictionary<string, object> { { EnumField.Id.ToString(), id } }),
                                            null);
                                        }
                                    }
                                }
                                break;
                            case nameof(ScaleEntity):
                                if (value is int idScale)
                                {
                                    //PluItem.Scale = AppSettings.DataAccess.ScalesCrud.GetEntity(
                                    //    new FieldListEntity(new Dictionary<string, object> { { EnumField.Id.ToString(), idScale } }),
                                    //    null);
                                }
                                break;
                            case nameof(NomenclatureEntity):
                                if (value is int idNomenclature)
                                {
                                    //PluItem.Nomenclature = AppSettings.DataAccess.NomenclaturesCrud.GetEntity(
                                    //    new FieldListEntity(new Dictionary<string, object> { { EnumField.Id.ToString(), idNomenclature } }),
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
                                    //        new FieldListEntity(new Dictionary<string, object> { { EnumField.Id.ToString(), idTemplate } }),
                                    //        null);
                                    //}
                                }
                                break;
                        }
                        await GuiRefreshWithWaitAsync();
                    }),
                }, true);
        }

        public async Task ItemSelectAsync(IBaseEntity item)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(ItemSelectAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new List<Task> {
                    new(async() => {
                        Item = item;
                        // Debug log.
                        //if (AppSettings.IsDebug)
                        //{
                        //    Console.WriteLine("--------------------------------------------------------------------------------");
                        //    Console.WriteLine($"---------- {nameof(BaseRazorIdEntity)}.{nameof(ItemSelectAsync)} (for Debug mode) ---------- ");
                        //    Console.WriteLine($"Item: {Item}");
                        //    Console.WriteLine("--------------------------------------------------------------------------------");
                        //}
                        await GuiRefreshWithWaitAsync();
                    }),
                }, true);
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
                    case EnumTableScale.BarcodeTypes:
                        if (parameters.TryGetValue(EnumField.Id.ToString(), out int idBarcodeType))
                        {
                            BarcodeTypeEntity barcodeTypeEntity = AppSettings.DataAccess.BarcodeTypesCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { EnumField.Id.ToString(), idBarcodeType },
                                }), null);
                            SetItem(barcodeTypeEntity);
                        }
                        break;
                    case EnumTableScale.Contragents:
                        if (parameters.TryGetValue(EnumField.Id.ToString(), out int idContragent))
                        {
                            ContragentEntity contragentEntity = AppSettings.DataAccess.ContragentsCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { EnumField.Id.ToString(), idContragent },
                                }), null);
                            SetItem(contragentEntity);
                        }
                        break;
                    case EnumTableScale.Hosts:
                        if (parameters.TryGetValue(EnumField.Id.ToString(), out int idHost))
                        {
                            HostEntity hostEntity = AppSettings.DataAccess.HostsCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { EnumField.Id.ToString(), idHost },
                                }), null);
                            SetItem(hostEntity);
                        }
                        break;
                    case EnumTableScale.Labels:
                        if (parameters.TryGetValue(EnumField.Id.ToString(), out int idLabel))
                        {
                            LabelEntity labelEntity = AppSettings.DataAccess.LabelsCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { EnumField.Id.ToString(), idLabel },
                                }), null);
                            SetItem(labelEntity);
                        }
                        break;
                    case EnumTableScale.Nomenclatures:
                        if (parameters.TryGetValue(EnumField.Id.ToString(), out int idNomenclature))
                        {
                            NomenclatureEntity nomenclatureEntity = AppSettings.DataAccess.NomenclaturesCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { EnumField.Id.ToString(), idNomenclature },
                                }), null);
                            SetItem(nomenclatureEntity);
                        }
                        break;
                    case EnumTableScale.OrderStatuses:
                        if (parameters.TryGetValue(EnumField.Id.ToString(), out int idOrderStatus))
                        {
                            OrderStatusEntity orderStatusEntity = AppSettings.DataAccess.OrderStatusesCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { EnumField.Id.ToString(), idOrderStatus },
                                }), null);
                            SetItem(orderStatusEntity);
                        }
                        break;
                    case EnumTableScale.OrderTypes:
                        if (parameters.TryGetValue(EnumField.Id.ToString(), out int idOrderType))
                        {
                            OrderTypeEntity orderTypeEntity = AppSettings.DataAccess.OrderTypesCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { EnumField.Id.ToString(), idOrderType },
                                }), null);
                            SetItem(orderTypeEntity);
                        }
                        break;
                    case EnumTableScale.Orders:
                        if (parameters.TryGetValue(EnumField.Id.ToString(), out int idOrder))
                        {
                            OrderEntity orderEntity = AppSettings.DataAccess.OrdersCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { EnumField.Id.ToString(), idOrder },
                                }), null);
                            SetItem(orderEntity);
                        }
                        break;
                    case EnumTableScale.Plus:
                        if (parameters.TryGetValue(EnumField.Id.ToString(), out int idPlu))
                        {
                            PluEntity pluEntity = AppSettings.DataAccess.PlusCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { EnumField.Id.ToString(), idPlu },
                                }), null);
                            SetItem(pluEntity);
                        }
                        break;
                    case EnumTableScale.Printers:
                        if (parameters.TryGetValue(EnumField.Id.ToString(), out int idPrinter))
                        {
                            PrinterEntity printerEntity = AppSettings.DataAccess.PrintersCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { EnumField.Id.ToString(), idPrinter },
                                }), null);
                            SetItem(printerEntity);
                        }
                        break;
                    case EnumTableScale.PrinterResources:
                        if (parameters.TryGetValue(EnumField.Id.ToString(), out int idPrinterResource))
                        {
                            PrinterResourceEntity printerResourceEntity = AppSettings.DataAccess.PrinterResourcesCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { EnumField.Id.ToString(), idPrinterResource },
                                }), null);
                            SetItem(printerResourceEntity);
                        }
                        break;
                    case EnumTableScale.PrinterTypes:
                        if (parameters.TryGetValue(EnumField.Id.ToString(), out int idPrinterType))
                        {
                            PrinterTypeEntity printerTypeEntity = AppSettings.DataAccess.PrinterTypesCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { EnumField.Id.ToString(), idPrinterType },
                                }), null);
                            SetItem(printerTypeEntity);
                        }
                        break;
                    case EnumTableScale.ProductSeries:
                        if (parameters.TryGetValue(EnumField.Id.ToString(), out int idProductSeries))
                        {
                            ProductSeriesEntity productSeriesEntity = AppSettings.DataAccess.ProductSeriesCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { EnumField.Id.ToString(), idProductSeries },
                                }), null);
                            SetItem(productSeriesEntity);
                        }
                        break;
                    case EnumTableScale.ProductionFacilities:
                        if (parameters.TryGetValue(EnumField.Id.ToString(), out int idProductionFacility))
                        {
                            ProductionFacilityEntity productionFacilityEntity = AppSettings.DataAccess.ProductionFacilitiesCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { EnumField.Id.ToString(), idProductionFacility },
                                }), null);
                            SetItem(productionFacilityEntity);
                        }
                        break;
                    case EnumTableScale.Scales:
                        if (parameters.TryGetValue(EnumField.Id.ToString(), out int idScale))
                        {
                            ScaleEntity scaleEntity = AppSettings.DataAccess.ScalesCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { EnumField.Id.ToString(), idScale },
                                }), null);
                            SetItem(scaleEntity);
                        }
                        break;
                    case EnumTableScale.TemplateResources:
                        if (parameters.TryGetValue(EnumField.Id.ToString(), out int idTemplateResource))
                        {
                            TemplateResourceEntity templateResourceEntity = AppSettings.DataAccess.TemplateResourcesCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { EnumField.Id.ToString(), idTemplateResource },
                                }), null);
                            SetItem(templateResourceEntity);
                        }
                        break;
                    case EnumTableScale.Templates:
                        if (parameters.TryGetValue(EnumField.Id.ToString(), out int idTemplate))
                        {
                            TemplateEntity templateEntity = AppSettings.DataAccess.TemplatesCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { EnumField.Id.ToString(), idTemplate },
                                }), null);
                            SetItem(templateEntity);
                        }
                        break;
                    case EnumTableScale.WeithingFacts:
                        if (parameters.TryGetValue(EnumField.Id.ToString(), out int idWeithingFact))
                        {
                            WeithingFactEntity weithingFactEntity = AppSettings.DataAccess.WeithingFactsCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { EnumField.Id.ToString(), idWeithingFact },
                                }), null);
                            SetItem(weithingFactEntity);
                        }
                        break;
                    case EnumTableScale.Workshops:
                        if (parameters.TryGetValue(EnumField.Id.ToString(), out int idWorkshop))
                        {
                            WorkshopEntity workshopEntity = AppSettings.DataAccess.WorkshopsCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { EnumField.Id.ToString(), idWorkshop },
                                }), null);
                            SetItem(workshopEntity);
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
                    case EnumTableSystem.Accesses:
                        if (parameters.TryGetValue(EnumField.Uid.ToString(), out Guid uidAccess))
                        {
                            AccessEntity accessEntity = AppSettings.DataAccess.AccessesCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { EnumField.Uid.ToString(), uidAccess },
                                }), null);
                            SetItem(accessEntity);
                        }
                        break;
                    case EnumTableSystem.Logs:
                        if (parameters.TryGetValue(EnumField.Uid.ToString(), out Guid uidLog))
                        {
                            LogEntity logEntity = AppSettings.DataAccess.LogsCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { EnumField.Uid.ToString(), uidLog },
                                }), null);
                            SetItem(logEntity);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public string ChartDataFormat(object value) => ((int)value).ToString("####", CultureInfo.InvariantCulture);

        public ChartCountEntity[] GetContragentsChartEntities(EnumField field)
        {
            ChartCountEntity[] result = new ChartCountEntity[0];
            ContragentEntity[] entities = AppSettings.DataAccess.ContragentsCrud.GetEntities(null,
                new FieldOrderEntity(EnumField.CreateDate, EnumOrderDirection.Asc));
            int i = 0;
            switch (field)
            {
                case EnumField.CreateDate:
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
                case EnumField.ModifiedDate:
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

        public ChartCountEntity[] GetNomenclaturesChartEntities(EnumField field)
        {
            ChartCountEntity[] result = new ChartCountEntity[0];
            NomenclatureEntity[] entities = AppSettings.DataAccess.NomenclaturesCrud.GetEntities(null,
                new FieldOrderEntity(EnumField.CreateDate, EnumOrderDirection.Asc));
            int i = 0;
            switch (field)
            {
                case EnumField.CreateDate:
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
                case EnumField.ModifiedDate:
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
                // Debug log.
                if (AppSettings.IsDebug)
                {
                    //Console.WriteLine("--------------------------------------------------------------------------------");
                    //Console.WriteLine($"---------- {nameof(BaseRazorEntity)}.{nameof(RunTasks)} (for Debug mode) ---------- ");
                    //Console.WriteLine($"filePath: {filePath}");
                    //Console.WriteLine($"memberName: {memberName} | lineNumber: {lineNumber}");
                    //Console.WriteLine($"tasks.Count: {tasks.Count}");
                    //Console.WriteLine("--------------------------------------------------------------------------------");
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

            if (string.IsNullOrEmpty(page))
                return;

            if (!isNewWindow)
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
            else
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
        }

        public void RouteSectionNavigate(bool isNewWindow)
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

        public async Task ItemCancelAsync(bool isNewWindow)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(ItemCancelAsync)}", LocalizationCore.Strings.DialogResultSuccess,
                LocalizationCore.Strings.DialogResultFail, LocalizationCore.Strings.DialogResultCancel,
                new List<Task> {
                    new(() => {
                        if (Item == null)
                            return;
                        if (Item is BaseIdEntity idItem && idItem.EqualsDefault())
                            return;
                        if (Item is BaseUidEntity uidItem && uidItem.EqualsDefault())
                            return;
                        RouteSectionNavigate(isNewWindow);
                    })}, false);
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
                int idLast = AppSettings.DataAccess.PrinterTypesCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
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
                            {{EnumField.Id.ToString(), item.Id}}),
                        new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc));
                    if (existsEntity)
                    {
                        //int idLast = AppSettings.DataAccess.PrinterResourcesCrud.GetEntity(
                        //    new FieldListEntity(new Dictionary<string, object>
                        //        { { "Printer.Id", printerResourceItem.Printer.Id }}),
                        //    new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
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
                        if (Item is BaseUidEntity uidItem && uidItem.EqualsDefault())
                            return;
                        if (Item is BaseIdEntity idItem2)
                        {
                            switch (idItem2)
                            {
                                case PrinterEntity printerItem: ItemSaveCheckPrinter(printerItem);
                                    break;
                                case PrinterTypeEntity printerTypeItem: ItemSaveCheckPrinterType(printerTypeItem);
                                    break;
                                case PrinterResourceEntity printerResourceItem: ItemSaveCheckPrinterResource(printerResourceItem);
                                    break;
                                case ScaleEntity scaleItem: ItemSaveCheckScale(scaleItem);
                                    break;
                                case WorkshopEntity workshopItem: ItemSaveCheckWorkshop(workshopItem);
                                    break;
                                default: ItemSaveCheck(idItem2);
                                    break;
                            }
                        }
                        RouteSectionNavigate(isNewWindow);
                    })}, continueOnCapturedContext);
        }

        [Obsolete(@"Deprecated method. Use Action method.")]
        private async Task ActionAsync<T>(ITableEntity table, EnumTableAction tableAction, IBaseEntity item, IBaseEntity parentItem = null)
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
                                EnumTableScale.BarcodeTypes => AppSettings.DataAccess.ActionGetIdEntity<BarcodeTypeEntity>(idEntity, tableAction),
                                EnumTableScale.Contragents => AppSettings.DataAccess.ActionGetIdEntity<ContragentEntity>(idEntity, tableAction),
                                EnumTableScale.Hosts => AppSettings.DataAccess.ActionGetIdEntity<HostEntity>(idEntity, tableAction),
                                EnumTableScale.Nomenclatures => AppSettings.DataAccess.ActionGetIdEntity<NomenclatureEntity>(idEntity, tableAction),
                                EnumTableScale.OrderStatuses => AppSettings.DataAccess.ActionGetIdEntity<OrderStatusEntity>(idEntity, tableAction),
                                EnumTableScale.OrderTypes => AppSettings.DataAccess.ActionGetIdEntity<OrderTypeEntity>(idEntity, tableAction),
                                EnumTableScale.Plus => AppSettings.DataAccess.ActionGetIdEntity<PluEntity>(idEntity, tableAction),
                                EnumTableScale.ProductionFacilities => AppSettings.DataAccess.ActionGetIdEntity<ProductionFacilityEntity>(idEntity, tableAction),
                                EnumTableScale.ProductSeries => AppSettings.DataAccess.ActionGetIdEntity<ProductSeriesEntity>(idEntity, tableAction),
                                EnumTableScale.Scales => AppSettings.DataAccess.ActionGetIdEntity<ScaleEntity>(idEntity, tableAction),
                                EnumTableScale.TemplateResources => AppSettings.DataAccess.ActionGetIdEntity<TemplateResourceEntity>(idEntity, tableAction),
                                EnumTableScale.Templates => AppSettings.DataAccess.ActionGetIdEntity<TemplateEntity>(idEntity, tableAction),
                                EnumTableScale.Workshops => AppSettings.DataAccess.ActionGetIdEntity<WorkshopEntity>(idEntity, tableAction),
                                EnumTableScale.WeithingFacts => AppSettings.DataAccess.ActionGetIdEntity<WeithingFactEntity>(idEntity, tableAction),
                                EnumTableScale.Printers => AppSettings.DataAccess.ActionGetIdEntity<PrinterEntity>(idEntity, tableAction),
                                EnumTableScale.PrinterResources => AppSettings.DataAccess.ActionGetIdEntity<PrinterResourceEntity>(idEntity, tableAction),
                                EnumTableScale.PrinterTypes => AppSettings.DataAccess.ActionGetIdEntity<PrinterTypeEntity>(idEntity, tableAction),
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
                                EnumTableSystem.Accesses => AppSettings.DataAccess.ActionGetUidEntity<AccessEntity>(uidEntity, tableAction),
                                EnumTableSystem.Logs => AppSettings.DataAccess.ActionGetUidEntity<LogEntity>(uidEntity, tableAction),
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

                    if (tableAction == EnumTableAction.New)
                    {
                        if (item is PluEntity pluEntity)
                        {
                            pluEntity.Scale = (ScaleEntity)parentItem;
                        }
                    }

                    switch (tableAction)
                    {
                        case EnumTableAction.New:
                        case EnumTableAction.Edit:
                        case EnumTableAction.Copy:
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
                        case EnumTableAction.Delete:
                            if (AppSettings.IdentityItem.AccessLevel == true)
                            {
                                AppSettings.DataAccess.ActionDeleteEntity(item);
                            }
                            break;
                        case EnumTableAction.Mark:
                            if (AppSettings.IdentityItem.AccessLevel == true)
                            {
                                AppSettings.DataAccess.ActionMarkedEntity(item);
                            }
                            break;
                    }
                })}, true).ConfigureAwait(false);
        }

        private void Action(EnumTableAction tableAction, bool isNewWindow)
        {
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(Action)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new List<Task> {
                    new(async() => {
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
                            case EnumTableAction.New:
                            case EnumTableAction.Edit:
                            case EnumTableAction.Copy:
                                if (AppSettings.IdentityItem.AccessLevel == true)
                                {
                                    if (Table is TableScaleEntity tableScale)
                                    {
                                        switch (tableScale.Value)
                                        {
                                            case EnumTableScale.Scales:
                                            case EnumTableScale.Plus:
                                            case EnumTableScale.Printers:
                                            case EnumTableScale.PrinterTypes:
                                                RouteItemNavigate(isNewWindow);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case EnumTableAction.Delete:
                                if (AppSettings.IdentityItem.AccessLevel == true)
                                {
                                    AppSettings.DataAccess.ActionDeleteEntity(Item);
                                }
                                break;
                            case EnumTableAction.Mark:
                                if (AppSettings.IdentityItem.AccessLevel == true)
                                {
                                    AppSettings.DataAccess.ActionMarkedEntity(Item);
                                }
                                break;
                        }
                        await GuiRefreshWithWaitAsync();
                    }),
                }, true);
        }

        public async Task ActionNewAsync(bool isNewWindow = false)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            Action(EnumTableAction.New, isNewWindow);
        }

        public async Task ActionEditAsync(bool isNewWindow = false)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            Action(EnumTableAction.Edit, isNewWindow);
        }

        public async Task ActionCopyAsync(bool isNewWindow = false)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            Action(EnumTableAction.Copy, isNewWindow);
        }

        public async Task ActionMarkAsync(bool isNewWindow = false)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            Action(EnumTableAction.Mark, isNewWindow);
        }

        public async Task ActionDeleteAsync(bool isNewWindow = false)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            Action(EnumTableAction.Delete, isNewWindow);
        }

        #endregion
    }
}
