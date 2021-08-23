// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.DAL;
using BlazorCore.DAL.TableModels;
using BlazorCore.DAL.TableSystemModels;
using BlazorCore.Utils;
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
        [Parameter] public bool IsShowAdd { get; set; }
        [Parameter] public bool IsShowEdit { get; set; }
        [Parameter] public bool IsShowCopy { get; set; }
        [Parameter] public bool IsShowNew { get; set; }
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
            RunTasks($"{LocalizationStrings.DeviceControl.Method} {nameof(Action)}", "", LocalizationStrings.Share.DialogResultFail, "",
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
            RunTasks($"{LocalizationStrings.DeviceControl.Method} {nameof(ItemSelectAsync)}", "", LocalizationStrings.Share.DialogResultFail, "",
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
            await RunTasksAsync(LocalizationStrings.Share.TableRead, "", LocalizationStrings.Share.DialogResultFail, "",
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
                        break;
                    case EnumTableScale.Contragents:
                        break;
                    case EnumTableScale.Hosts:
                        break;
                    case EnumTableScale.Labels:
                        break;
                    case EnumTableScale.Nomenclatures:
                        break;
                    case EnumTableScale.OrderStatuses:
                        break;
                    case EnumTableScale.OrderTypes:
                        break;
                    case EnumTableScale.Orders:
                        break;
                    case EnumTableScale.Plus:
                        break;
                    case EnumTableScale.Printers:
                        break;
                    case EnumTableScale.PrinterResources:
                        break;
                    case EnumTableScale.PrinterTypes:
                        break;
                    case EnumTableScale.ProductSeries:
                        break;
                    case EnumTableScale.ProductionFacilities:
                        break;
                    case EnumTableScale.Scales:
                        if (parameters.TryGetValue("Id", out int id))
                        {
                            ScaleEntity scaleEntity = AppSettings.DataAccess.ScalesCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> {
                                    { EnumField.Id.ToString(), id },
                                }), null);
                            SetItem(scaleEntity);
                        }
                        break;
                    case EnumTableScale.TemplateResources:
                        break;
                    case EnumTableScale.Templates:
                        break;
                    case EnumTableScale.WeithingFacts:
                        break;
                    case EnumTableScale.Workshops:
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
            Navigation.NavigateTo(LocalizationStrings.Share.UriRouteRoot);
        }

        public static ConfirmOptions GetConfirmOptions() => new()
        {
            OkButtonText = LocalizationStrings.Share.DialogButtonYes,
            CancelButtonText = LocalizationStrings.Share.DialogButtonCancel,
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
                string question = string.IsNullOrEmpty(questionAdd) ? LocalizationStrings.Share.DialogQuestion : questionAdd;
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
                page = LocalizationStrings.DeviceControl.UriRouteItemPrinter;
            }
            else if (Item is PrinterTypeEntity)
            {
                page = LocalizationStrings.DeviceControl.UriRouteItemPrinterType;
            }
            else if (Item is ScaleEntity)
            {
                page = LocalizationStrings.DeviceControl.UriRouteItemScale;
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
                page = LocalizationStrings.DeviceControl.UriRouteSectionPrinters;
            }
            else if (Item is PrinterTypeEntity)
            {
                page = LocalizationStrings.DeviceControl.UriRouteSectionPrinterTypes;
            }
            else if (Item is ScaleEntity)
            {
                page = LocalizationStrings.DeviceControl.UriRouteSectionScales;
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
            RunTasks($"{LocalizationStrings.DeviceControl.Method} {nameof(ItemCancelAsync)}", LocalizationStrings.Share.DialogResultSuccess,
                LocalizationStrings.Share.DialogResultFail, LocalizationStrings.Share.DialogResultCancel,
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

        public async Task ItemSaveAsync(bool continueOnCapturedContext, bool isNewWindow)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            RunTasksWithQeustion(LocalizationStrings.Share.TableRecordSave, LocalizationStrings.Share.DialogResultSuccess,
                LocalizationStrings.Share.DialogResultFail, LocalizationStrings.Share.DialogResultCancel, "",
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
                            if (idItem2 is PrinterEntity printerItem)
                            {
                                if (printerItem.Id == 0)
                                    AppSettings.DataAccess.PrintersCrud.SaveEntity(printerItem);
                                else
                                    AppSettings.DataAccess.PrintersCrud.UpdateEntity(printerItem);
                            }
                            else if (idItem2 is PrinterTypeEntity printerTypeEntity)
                            {
                                if (printerTypeEntity.Id == 0)
                                    AppSettings.DataAccess.PrinterTypesCrud.SaveEntity(printerTypeEntity);
                                else
                                    AppSettings.DataAccess.PrinterTypesCrud.UpdateEntity(printerTypeEntity);
                            }
                            else if (idItem2 is ScaleEntity scaleItem)
                            {
                                if (scaleItem.Id == 0)
                                    AppSettings.DataAccess.ScalesCrud.SaveEntity(scaleItem);
                                else
                                    AppSettings.DataAccess.ScalesCrud.UpdateEntity(scaleItem);
                            }
                        }
                        RouteSectionNavigate(isNewWindow);
                    })}, continueOnCapturedContext);
        }

        [Obsolete(@"Deprecated method. Use Action method.")]
        private async Task ActionAsync<T>(ITableEntity table, EnumTableAction tableAction, IBaseEntity item, IBaseEntity parentItem = null) 
            where T : BaseRazorEntity
        {
            await RunTasksAsync(LocalizationStrings.Share.TableRead, "", LocalizationStrings.Share.DialogResultFail, "",
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
                        title = LocalizationStrings.GetItemTitle(table, idEntity.Id);
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
                            title = LocalizationStrings.GetItemTitle(table, uidEntity.Uid);
                        }
                    }

                    // Printer from ZebraPrinter.razor.
                    if (item is PrinterResourceEntity zebraPrinterResourceRefEntity)
                    {
                        zebraPrinterResourceRefEntity.Printer = (PrinterEntity)parentItem;
                    }

                    if (tableAction == EnumTableAction.Add)
                    {
                        if (item is PluEntity pluEntity)
                        {
                            pluEntity.Scale = (ScaleEntity)parentItem;
                        }
                    }

                    switch (tableAction)
                    {
                        case EnumTableAction.Add:
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
                                    new Radzen.DialogOptions() { Width = "1400px", Height = "970px" }).ConfigureAwait(false);
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
            RunTasks($"{LocalizationStrings.DeviceControl.Method} {nameof(Action)}", "", LocalizationStrings.Share.DialogResultFail, "",
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
                            case EnumTableAction.Add:
                            case EnumTableAction.Edit:
                            case EnumTableAction.Copy:
                                if (AppSettings.IdentityItem.AccessLevel == true)
                                {
                                    if (Table is TableScaleEntity tableScales)
                                    {
                                        switch (tableScales.Value)
                                        {
                                            case EnumTableScale.Scales:
                                            case EnumTableScale.Printers:
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

        public async Task ActionAddAsync(bool isNewWindow = false)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            Action(EnumTableAction.Add, isNewWindow);
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
