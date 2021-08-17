using BlazorCore.DAL;
using BlazorCore.DAL.DataModels;
using BlazorCore.DAL.TableModels;
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

        #region IDisposable

        public void Dispose()
        {
            Dialog?.Dispose();
            Tooltip?.Dispose();
            AppSettings.HotKeysContextItem?.Dispose();
            GC.Collect();
        }

        #endregion

        #region Public and private fields and properties

        public AppSettingsEntity AppSettings = AppSettingsEntity.Instance;
        public delegate Task DelegateGuiRefreshAsync(bool continueOnCapturedContext);
        public delegate void DelegateGuiRefresh();
        public string Page { get; set; }

        #endregion

        #region Constructor and destructor

        public BaseRazorEntity() { }

        #endregion

        #region Public and private methods

        public async Task GuiRefreshAsync(bool continueOnCapturedContext) => await InvokeAsync(StateHasChanged).ConfigureAwait(continueOnCapturedContext);
        public void GuiRefresh() => StateHasChanged();

        public async Task GetDataAsync(Task task, bool continueOnCapturedContext)
        {
            await RunTasksAsync(LocalizationStrings.Share.TableRead, "", LocalizationStrings.Share.DialogResultFail, "",
                new List<Task> { task }, GuiRefreshAsync, continueOnCapturedContext).ConfigureAwait(false);
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
        }

        //public void Action<T>(EnumTableScales table, EnumTableAction tableAction, BaseEntity item, BaseEntity parentItem = null) where T : BaseRazorEntity
        //{
        //    RunTasks(LocalizationStrings.DeviceControl.MethodSetParametersAsync, "", LocalizationStrings.Share.DialogResultFail, "",
        //        new List<Task> {
        //            new(() => {
        //                Console.WriteLine($"ActionAsync. table: {table}. tableAction: {tableAction}. item: {item}");
        //                string title = string.Empty;
        //                if (item is BaseIdEntity idEntity)
        //                {
        //                    idEntity = table switch
        //                    {
        //                        EnumTableScales.BarcodeTypes => AppSettings.DataAccess.ActionGetIdEntity<BarCodeTypesEntity>(idEntity, tableAction),
        //                        EnumTableScales.Contragents => AppSettings.DataAccess.ActionGetIdEntity<ContragentsEntity>(idEntity, tableAction),
        //                        EnumTableScales.Hosts => AppSettings.DataAccess.ActionGetIdEntity<HostsEntity>(idEntity, tableAction),
        //                        EnumTableScales.Nomenclature => AppSettings.DataAccess.ActionGetIdEntity<NomenclatureEntity>(idEntity, tableAction),
        //                        EnumTableScales.OrderStatus => AppSettings.DataAccess.ActionGetIdEntity<OrderStatusEntity>(idEntity, tableAction),
        //                        EnumTableScales.OrderTypes => AppSettings.DataAccess.ActionGetIdEntity<OrderTypesEntity>(idEntity, tableAction),
        //                        EnumTableScales.Plu => AppSettings.DataAccess.ActionGetIdEntity<PluEntity>(idEntity, tableAction),
        //                        EnumTableScales.ProductionFacility => AppSettings.DataAccess.ActionGetIdEntity<ProductionFacilityEntity>(idEntity, tableAction),
        //                        EnumTableScales.ProductSeries => AppSettings.DataAccess.ActionGetIdEntity<ProductSeriesEntity>(idEntity, tableAction),
        //                        EnumTableScales.Scales => AppSettings.DataAccess.ActionGetIdEntity<ScalesEntity>(idEntity, tableAction),
        //                        EnumTableScales.TemplateResources => AppSettings.DataAccess.ActionGetIdEntity<TemplateResourcesEntity>(idEntity, tableAction),
        //                        EnumTableScales.Templates => AppSettings.DataAccess.ActionGetIdEntity<TemplatesEntity>(idEntity, tableAction),
        //                        EnumTableScales.WorkShop => AppSettings.DataAccess.ActionGetIdEntity<WorkshopEntity>(idEntity, tableAction),
        //                        EnumTableScales.WeithingFact => AppSettings.DataAccess.ActionGetIdEntity<WeithingFactEntity>(idEntity, tableAction),
        //                        EnumTableScales.Printer => AppSettings.DataAccess.ActionGetIdEntity<ZebraPrinterEntity>(idEntity, tableAction),
        //                        EnumTableScales.PrinterResourceRef => AppSettings.DataAccess.ActionGetIdEntity<ZebraPrinterResourceRefEntity>(idEntity, tableAction),
        //                        EnumTableScales.PrinterType => AppSettings.DataAccess.ActionGetIdEntity<ZebraPrinterTypeEntity>(idEntity, tableAction),
        //                        _ => throw new ArgumentOutOfRangeException(nameof(tableAction), tableAction, null)
        //                    };
        //                    title = LocalizationStrings.DeviceControl.GetItemTitle(table, idEntity.Id);
        //                }
        //                else if (item is BaseUidEntity uidEntity)
        //                {
        //                    uidEntity = table switch
        //                    {
        //                        EnumTableScales.Logs => AppSettings.DataAccess.ActionGetUidEntity<LogEntity>(uidEntity, tableAction),
        //                        _ => throw new ArgumentOutOfRangeException(nameof(tableAction), tableAction, null)
        //                    };
        //                    title = LocalizationStrings.DeviceControl.GetItemTitle(table, uidEntity.Uid);
        //                }

        //                // Printer from ZebraPrinter.razor.
        //                if (item is ZebraPrinterResourceRefEntity zebraPrinterResourceRefEntity)
        //                {
        //                    zebraPrinterResourceRefEntity.Printer = (ZebraPrinterEntity)parentItem;
        //                }

        //                if (tableAction == EnumTableAction.Add)
        //                {
        //                    if (item is PluEntity pluEntity)
        //                    {
        //                        pluEntity.Scale = (ScalesEntity)parentItem;
        //                    }
        //                }

        //                switch (tableAction)
        //                {
        //                    case EnumTableAction.Add:
        //                    case EnumTableAction.Edit:
        //                    case EnumTableAction.Copy:
        //                        if (AppSettings.IdentityItem.AccessLevel == true)
        //                        {
        //                            Console.WriteLine($"ActionAsync. AppSettings.IdentityItem.AccessLevel: {AppSettings.IdentityItem.AccessLevel}");
        //                            Dialog.OpenAsync<T>(title,
        //                                new Dictionary<string, object>
        //                                {
        //                                    {"Item", item},
        //                                    {"Table", table},
        //                                    {"TableAction", tableAction},
        //                                },
        //                                new DialogOptions() { Width = "1400px", Height = "970px" }).ConfigureAwait(false);
        //                        }
        //                        break;
        //                    case EnumTableAction.Delete:
        //                        if (AppSettings.IdentityItem.AccessLevel == true)
        //                        {
        //                            AppSettings.DataAccess.ActionDeleteEntity(item);
        //                        }
        //                        break;
        //                    case EnumTableAction.Mark:
        //                        if (AppSettings.IdentityItem.AccessLevel == true)
        //                        {
        //                            AppSettings.DataAccess.ActionMarkedEntity(item);
        //                        }
        //                        break;
        //                }
        //              }),
        //          }, true);
        //}

        [Obsolete(@"Deprecated method")]
        public async Task ActionAsync<T>(EnumTableScales table, EnumTableAction tableAction, BaseEntity item, BaseEntity parentItem = null) where T : BaseRazorEntity
        {
            await RunTasksAsync(LocalizationStrings.Share.TableRead, "", LocalizationStrings.Share.DialogResultFail, "",
                new List<Task> { new Task(delegate {
                    Console.WriteLine($"ActionAsync. table: {table}. tableAction: {tableAction}. item: {item}");
                    string title = string.Empty;
                    if (item is BaseIdEntity idEntity)
                    {
                        idEntity = table switch
                        {
                            EnumTableScales.BarcodeTypes => AppSettings.DataAccess.ActionGetIdEntity<BarCodeTypesEntity>(idEntity, tableAction),
                            EnumTableScales.Contragents => AppSettings.DataAccess.ActionGetIdEntity<ContragentsEntity>(idEntity, tableAction),
                            EnumTableScales.Hosts => AppSettings.DataAccess.ActionGetIdEntity<HostsEntity>(idEntity, tableAction),
                            EnumTableScales.Nomenclature => AppSettings.DataAccess.ActionGetIdEntity<NomenclatureEntity>(idEntity, tableAction),
                            EnumTableScales.OrderStatus => AppSettings.DataAccess.ActionGetIdEntity<OrderStatusEntity>(idEntity, tableAction),
                            EnumTableScales.OrderTypes => AppSettings.DataAccess.ActionGetIdEntity<OrderTypesEntity>(idEntity, tableAction),
                            EnumTableScales.Plu => AppSettings.DataAccess.ActionGetIdEntity<PluEntity>(idEntity, tableAction),
                            EnumTableScales.ProductionFacility => AppSettings.DataAccess.ActionGetIdEntity<ProductionFacilityEntity>(idEntity, tableAction),
                            EnumTableScales.ProductSeries => AppSettings.DataAccess.ActionGetIdEntity<ProductSeriesEntity>(idEntity, tableAction),
                            EnumTableScales.Scales => AppSettings.DataAccess.ActionGetIdEntity<ScalesEntity>(idEntity, tableAction),
                            EnumTableScales.TemplateResources => AppSettings.DataAccess.ActionGetIdEntity<TemplateResourcesEntity>(idEntity, tableAction),
                            EnumTableScales.Templates => AppSettings.DataAccess.ActionGetIdEntity<TemplatesEntity>(idEntity, tableAction),
                            EnumTableScales.WorkShop => AppSettings.DataAccess.ActionGetIdEntity<WorkshopEntity>(idEntity, tableAction),
                            EnumTableScales.WeithingFact => AppSettings.DataAccess.ActionGetIdEntity<WeithingFactEntity>(idEntity, tableAction),
                            EnumTableScales.Printer => AppSettings.DataAccess.ActionGetIdEntity<ZebraPrinterEntity>(idEntity, tableAction),
                            EnumTableScales.PrinterResourceRef => AppSettings.DataAccess.ActionGetIdEntity<ZebraPrinterResourceRefEntity>(idEntity, tableAction),
                            EnumTableScales.PrinterType => AppSettings.DataAccess.ActionGetIdEntity<ZebraPrinterTypeEntity>(idEntity, tableAction),
                            _ => throw new ArgumentOutOfRangeException(nameof(tableAction), tableAction, null)
                        };
                        title = LocalizationStrings.DeviceControl.GetItemTitle(table, idEntity.Id);
                    }
                    else if (item is BaseUidEntity uidEntity)
                    {
                        uidEntity = table switch
                        {
                            EnumTableScales.Logs => AppSettings.DataAccess.ActionGetUidEntity<LogEntity>(uidEntity, tableAction),
                            _ => throw new ArgumentOutOfRangeException(nameof(tableAction), tableAction, null)
                        };
                        title = LocalizationStrings.DeviceControl.GetItemTitle(table, uidEntity.Uid);
                    }

                    // Printer from ZebraPrinter.razor.
                    if (item is ZebraPrinterResourceRefEntity zebraPrinterResourceRefEntity)
                    {
                        zebraPrinterResourceRefEntity.Printer = (ZebraPrinterEntity)parentItem;
                    }

                    if (tableAction == EnumTableAction.Add)
                    {
                        if (item is PluEntity pluEntity)
                        {
                            pluEntity.Scale = (ScalesEntity)parentItem;
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
                })}, GuiRefreshAsync, true).ConfigureAwait(false);
        }

        [Obsolete(@"Deprecated method")]
        public async Task ActionAsync(EnumTableScales table, EnumTableAction tableAction, BaseEntity item, string page, bool isNewWindow)
        {
            await RunTasksAsync(LocalizationStrings.Share.TableRead, "", LocalizationStrings.Share.DialogResultFail, "",
                new List<Task> { new Task(delegate {
                    if (table == EnumTableScales.Default)
                        return;
                    //if (item == null || item.EqualsDefault())
                    //    return;
                    BaseIdEntity idItem = null;
                    BaseUidEntity uidItem = null;
                    switch (item)
                    {
                        case BaseIdEntity baseIdEntity:
                            idItem = baseIdEntity;
                            break;
                        case BaseUidEntity baseUidEntity:
                            uidItem = baseUidEntity;
                            break;
                    }
                    // Debug log.
                    if (AppSettings.IsDebug)
                    {
                        Console.WriteLine("--------------------------------------------------------------------------------");
                        Console.WriteLine($"---------- {nameof(BaseRazorEntity)}.{nameof(ActionAsync)} (for Debug mode) ---------- ");
                        Console.WriteLine($"{nameof(ActionAsync)}. {nameof(table)}: {table}. {nameof(tableAction)}: {tableAction}. {nameof(page)}: {page}. ");
                        if (idItem != null)
                        {
                            Console.WriteLine($"{nameof(idItem)}: {idItem}");
                            if (AppSettings.IdentityItem.AccessLevel == true)
                                Console.WriteLine($"Open page {page}/{idItem.Id}");
                        }
                        else if (uidItem != null)
                        {
                            Console.WriteLine($"{nameof(uidItem)}: {uidItem}");
                            if (AppSettings.IdentityItem.AccessLevel == true)
                                Console.WriteLine($"Open page {page}/{uidItem.Uid}");
                        }
                        Console.WriteLine("--------------------------------------------------------------------------------");
                    }

                    switch (tableAction)
                    {
                        case EnumTableAction.Add:
                        case EnumTableAction.Edit:
                        case EnumTableAction.Copy:
                            if (AppSettings.IdentityItem.AccessLevel == true)
                            {
                                switch (table)
                                {
                                    case EnumTableScales.Scales:
                                    case EnumTableScales.Printer:
                                        if (!isNewWindow)
                                        {
                                            if (idItem != null)
                                            {
                                                Navigation.NavigateTo($"{page}/{idItem.Id}");
                                            }
                                            else if (uidItem != null)
                                            {
                                                Navigation.NavigateTo($"{page}/{uidItem.Uid}");
                                            }
                                            else
                                            {
                                                Navigation.NavigateTo("{page}");
                                            }
                                        }
                                        else
                                        {
                                            if (idItem != null)
                                                JsRuntime.InvokeAsync<object>("open", $"{page}/{idItem.Id}", "_blank").ConfigureAwait(false);
                                            else if (uidItem != null)
                                                JsRuntime.InvokeAsync<object>("open", $"{page}/{uidItem.Uid}", "_blank").ConfigureAwait(false);
                                            else
                                                JsRuntime.InvokeAsync<object>("open", $"{page}", "_blank").ConfigureAwait(false);
                                        }
                                        break;
                                }
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
                })}, GuiRefreshAsync, true).ConfigureAwait(false);
        }

        public void Action(EnumTableScales table, EnumTableAction tableAction, BaseEntity item, string page, bool isNewWindow,
            BaseEntity parentItem = null)
        {
            RunTasks($"{LocalizationStrings.DeviceControl.Method} {nameof(Action)}", "", LocalizationStrings.Share.DialogResultFail, "",
                new List<Task> {
                    new(async() => {
                        if (table == EnumTableScales.Default)
                            return;
                        //if (item == null || item.EqualsDefault())
                        //    return;
                        BaseIdEntity idItem = null;
                        BaseUidEntity uidItem = null;
                        switch (item)
                        {
                            case BaseIdEntity baseIdEntity:
                                idItem = baseIdEntity;
                                break;
                            case BaseUidEntity baseUidEntity:
                                uidItem = baseUidEntity;
                                break;
                        }
                        // Debug log.
                        if (AppSettings.IsDebug)
                        {
                            Console.WriteLine("--------------------------------------------------------------------------------");
                            Console.WriteLine($"---------- {nameof(BaseRazorEntity)}.{nameof(ActionAsync)} (for Debug mode) ---------- ");
                            Console.WriteLine($"{nameof(ActionAsync)}. {nameof(table)}: {table}. {nameof(tableAction)}: {tableAction}. {nameof(page)}: {page}. ");
                            if (idItem != null)
                            {
                                Console.WriteLine($"{nameof(idItem)}: {idItem}");
                                if (AppSettings.IdentityItem.AccessLevel == true)
                                    Console.WriteLine($"Open page {page}/{idItem.Id}");
                            }
                            else if (uidItem != null)
                            {
                                Console.WriteLine($"{nameof(uidItem)}: {uidItem}");
                                if (AppSettings.IdentityItem.AccessLevel == true)
                                    Console.WriteLine($"Open page {page}/{uidItem.Uid}");
                            }
                            Console.WriteLine("--------------------------------------------------------------------------------");
                        }

                        switch (tableAction)
                        {
                            case EnumTableAction.Add:
                            case EnumTableAction.Edit:
                            case EnumTableAction.Copy:
                                if (AppSettings.IdentityItem.AccessLevel == true)
                                {
                                    switch (table)
                                    {
                                        case EnumTableScales.Scales:
                                        case EnumTableScales.Printer:
                                            if (!isNewWindow)
                                            {
                                                if (idItem != null)
                                                {
                                                    Navigation.NavigateTo($"{page}/{idItem.Id}");
                                                }
                                                else if (uidItem != null)
                                                {
                                                    Navigation.NavigateTo($"{page}/{uidItem.Uid}");
                                                }
                                                else
                                                {
                                                    Navigation.NavigateTo("{page}");
                                                }
                                            }
                                            else
                                            {
                                                if (idItem != null)
                                                    await JsRuntime.InvokeAsync<object>("open", $"{page}/{idItem.Id}", "_blank").ConfigureAwait(false);
                                                else if (uidItem != null)
                                                    await JsRuntime.InvokeAsync<object>("open", $"{page}/{uidItem.Uid}", "_blank").ConfigureAwait(false);
                                                else
                                                    await JsRuntime.InvokeAsync<object>("open", $"{page}", "_blank").ConfigureAwait(false);
                                            }
                                            break;
                                    }
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
                        await GuiRefreshAsync(false).ConfigureAwait(false);
                    }),
                }, true);
        }

        public string ChartDataFormat(object value) => ((int)value).ToString("####", CultureInfo.InvariantCulture);

        public ChartCountEntity[] GetContragentsChartEntities(EnumField field)
        {
            ChartCountEntity[] result = new ChartCountEntity[0];
            ContragentsEntity[] entities = AppSettings.DataAccess.ContragentsCrud.GetEntities(null,
                new FieldOrderEntity(EnumField.CreateDate, EnumOrderDirection.Asc));
            int i = 0;
            switch (field)
            {
                case EnumField.CreateDate:
                    List<ChartCountEntity> entitiesDateCreated = new();
                    foreach (ContragentsEntity entity in entities)
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
                    foreach (ContragentsEntity entity in entities)
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
            NomenclatureEntity[] entities = AppSettings.DataAccess.NomenclatureCrud.GetEntities(null,
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

        public ConfirmOptions GetConfirmOptions() =>
            //return new()
            //{
            //    ShowTitle = true,
            //    ShowClose = true,
            //    OkButtonText = LocalizationStrings.Share.DialogButtonYes,
            //    CancelButtonText = LocalizationStrings.Share.DialogButtonCancel,
            //    Bottom = null,
            //    ChildContent = null,
            //    Height = null,
            //    Left = null,
            //    Style = null,
            //    Top = null,
            //    Width = null,
            //};
            new ConfirmOptions()
            {
                OkButtonText = LocalizationStrings.Share.DialogButtonYes,
                CancelButtonText = LocalizationStrings.Share.DialogButtonCancel,
            };

        public async Task RunTasksAsync(string title, string detailSuccess, string detailFail, string detailCancel, List<Task> tasks,
            DelegateGuiRefreshAsync callRefresh, bool continueOnCapturedContext,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

            RunTasks(title, detailSuccess, detailFail, detailCancel, tasks, callRefresh, continueOnCapturedContext, filePath, lineNumber, memberName);
        }

        public void RunTasks(string title, string detailSuccess, string detailFail, string detailCancel, List<Task> tasks,
            bool continueOnCapturedContext,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                RunTasksCore(title, detailSuccess, detailFail, detailCancel, tasks, continueOnCapturedContext);
            }
            catch (Exception ex)
            {
                RunTasksCatch(ex, title, detailFail, filePath, lineNumber, memberName);
            }
        }

        public void RunTasks(string title, string detailSuccess, string detailFail, string detailCancel, List<Task> tasks,
            DelegateGuiRefreshAsync callRefreshAsync, bool continueOnCapturedContext,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                RunTasksCore(title, detailSuccess, detailFail, detailCancel, tasks, continueOnCapturedContext);
            }
            catch (Exception ex)
            {
                RunTasksCatch(ex, title, detailFail, filePath, lineNumber, memberName);
            }
            finally
            {
                callRefreshAsync?.Invoke(true).ConfigureAwait(false);
            }
        }

        public void RunTasks(string title, string detailSuccess, string detailFail, string detailCancel, List<Task> tasks,
            DelegateGuiRefresh callRefresh, bool continueOnCapturedContext,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                RunTasksCore(title, detailSuccess, detailFail, detailCancel, tasks, continueOnCapturedContext);
            }
            catch (Exception ex)
            {
                RunTasksCatch(ex, title, detailFail, filePath, lineNumber, memberName);
            }
            finally
            {
                if (callRefresh != null)
                {
                    callRefresh();
                }
            }
        }

        private void RunTasksCore(string title, string detailSuccess, string detailFail, string detailCancel, List<Task> tasks, bool continueOnCapturedContext)
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

        public async Task RunTasksWithQeustion(string title, string detailSuccess, string detailFail, string detailCancel,
            List<Task> tasks, DelegateGuiRefreshAsync callRefresh, string questionAdd = "", bool isWait = true,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            //await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            try
            {
                string question = string.IsNullOrEmpty(questionAdd) ? LocalizationStrings.Share.DialogQuestion : questionAdd;
                Console.WriteLine($"RunTasksWithQeustion. Dialog: {Dialog}");
                Console.WriteLine($"RunTasksWithQeustion. question: {question}");
                Console.WriteLine($"RunTasksWithQeustion. title: {title}");
                Console.WriteLine($"RunTasksWithQeustion. Dialog: {Dialog}");
                //bool? result = Dialog.Confirm(question, title, confirmOptions).Result;
                Task<bool?> dialog = Dialog.Confirm(question, title, GetConfirmOptions());
                Console.WriteLine("RunTasksWithQeustion 2");
                dialog.Start();
                Console.WriteLine("RunTasksWithQeustion 3");
                bool? result = dialog.Result;
                Console.WriteLine("RunTasksWithQeustion 4");
                if (result == true)
                {
                    Console.WriteLine("RunTasksWithQeustion 3");
                    if (tasks != null)
                    {
                        //Task.WaitAll(tasks.ToArray());
                        foreach (Task task in tasks)
                        {
                            task.Start();
                            if (isWait)
                                task.Wait();
                        }
                    }
                    if (!string.IsNullOrEmpty(detailSuccess))
                        Notification.Notify(NotificationSeverity.Success, title + Environment.NewLine, detailSuccess, AppSettingsEntity.Delay);
                }
                else
                {
                    if (!string.IsNullOrEmpty(detailCancel))
                        Notification.Notify(NotificationSeverity.Info, title + Environment.NewLine, detailCancel, AppSettingsEntity.Delay);
                }
            }
            catch (Exception ex)
            {
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

                Console.WriteLine(msg);
                Console.WriteLine($"{nameof(filePath)}: {filePath}. {nameof(lineNumber)}: {lineNumber}. {nameof(memberName)}: {memberName}.");
            }
            finally
            {
                if (callRefresh != null)
                    await callRefresh(true).ConfigureAwait(false);
            }
        }

        #endregion

        #region Public and private methods - Actions

        public async Task ItemCancelAsync(BaseEntity item, string page)
        {
            await RunTasksAsync(LocalizationStrings.Share.TableRecordCancel,
                LocalizationStrings.Share.DialogResultSuccess, LocalizationStrings.Share.DialogResultFail, LocalizationStrings.Share.DialogResultCancel,
                new List<Task> { new Task(delegate {
                    if (item == null)
                        return;
                    if (item is BaseIdEntity idItem && idItem.EqualsDefault())
                        return;
                    if (item is BaseUidEntity uidItem && uidItem.EqualsDefault())
                        return;
                    Navigation.NavigateTo(page);
                })}, GuiRefreshAsync, false).ConfigureAwait(false);
        }

        public async Task ItemSaveAsync(BaseEntity item, bool continueOnCapturedContext = true)
        {
            await RunTasksWithQeustion(LocalizationStrings.Share.TableRecordSave,
                LocalizationStrings.Share.DialogResultSuccess, LocalizationStrings.Share.DialogResultFail, LocalizationStrings.Share.DialogResultCancel,
                new List<Task> { new Task(delegate {
                    if (item == null)
                        return;
                    if (item is BaseIdEntity idItem && idItem.EqualsDefault())
                        return;
                    if (item is BaseUidEntity uidItem && uidItem.EqualsDefault())
                        return;
                    //if (ItemId == 0)
                    if (item is BaseIdEntity idItem2)
                    {
                        if (idItem2 is ZebraPrinterEntity printerItem)
                        {
                            if (printerItem.Id == 0)
                                AppSettings.DataAccess.ZebraPrinterCrud.SaveEntity(printerItem);
                            else
                                AppSettings.DataAccess.ZebraPrinterCrud.UpdateEntity(printerItem);
                        }
                        else if (idItem2 is ScalesEntity scaleItem)
                        {
                            if (scaleItem.Id == 0)
                                AppSettings.DataAccess.ScalesCrud.SaveEntity(scaleItem);
                            else
                                AppSettings.DataAccess.ScalesCrud.UpdateEntity(scaleItem);
                        }
                   }
                    Navigation.NavigateTo($"{LocalizationStrings.DeviceControl.UriRouteSectionPrinters}");
                })}, GuiRefreshAsync).ConfigureAwait(continueOnCapturedContext);
        }

        #endregion
    }
}