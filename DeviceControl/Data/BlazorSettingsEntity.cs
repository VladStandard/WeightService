// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BlazorDeviceControl.Utils;
using DeviceControl.Core;
using DeviceControl.Core.DAL;
using DeviceControl.Core.DAL.DataModels;
using DeviceControl.Core.DAL.TableModels;
using DeviceControl.Core.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using Toolbelt.Blazor.HotKeys;

namespace BlazorDeviceControl.Data
{
    public class BlazorSettingsEntity
    {
        #region Public and private fields and properties

        public NavigationManager Navigation { get; private set; }
        public NotificationService Notification { get; private set; }
        public DialogService Dialog { get; private set; }
        public TooltipService Tooltip { get; private set; }
        public DataAccessEntity DataAccess { get; set; }
        public EnumAccessRights AccessRights { get; set; }
        public bool ShowActionsButtons { get; set; }
        public bool ChartSmooth { get; set; }
        public bool IsDebug
        {
            get
            {
#if DEBUG
                return true;
#else
                return false;
#endif
            }
        }
        public DataSourceEntity DataSource { get; set; } = new();
        public MemoryEntity Memory { get; set; }
        public IJSRuntime JsRuntime { get; set; }
        public HotKeysContext HotKeys { get; private set; }
        public int Delay { get; } = 5_000;
        public delegate Task DelegateGuiRefresh();

        #endregion

        #region Public and private methods

        public void Setup(DataAccessConfig dataAccessService, NotificationService notification, DialogService dialog, NavigationManager navigation,
            TooltipService tooltip, HotKeysContext hotKeys, IJSRuntime jsRuntime)
        {
            var appSettings = new CoreSettingsEntity(dataAccessService.Server, dataAccessService.Db, dataAccessService.Trusted, dataAccessService.Username, dataAccessService.Password);
            DataAccess = new DataAccessEntity(appSettings);
            Notification = notification;
            AccessRights = EnumAccessRights.Guest;
            Dialog = dialog;
            Navigation = navigation;
            ChartSmooth = false;
            Tooltip = tooltip;
            HotKeys = hotKeys;
            JsRuntime = jsRuntime;
        }

        public async Task ActionAsync(EnumTable table, EnumTableAction tableAction, BaseEntity entity, BaseEntity parentEntity,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            try
            {
                string title = string.Empty;
                if (entity is BaseIdEntity idEntity)
                {
                    idEntity = table switch
                    {
                        EnumTable.BarCodeTypes => DataAccess.ActionGetIdEntity<BarCodeTypesEntity>(idEntity, tableAction),
                        EnumTable.Contragents => DataAccess.ActionGetIdEntity<ContragentsEntity>(idEntity, tableAction),
                        EnumTable.Hosts => DataAccess.ActionGetIdEntity<HostsEntity>(idEntity, tableAction),
                        EnumTable.Nomenclature => DataAccess.ActionGetIdEntity<NomenclatureEntity>(idEntity, tableAction),
                        EnumTable.OrderStatus => DataAccess.ActionGetIdEntity<OrderStatusEntity>(idEntity, tableAction),
                        EnumTable.OrderTypes => DataAccess.ActionGetIdEntity<OrderTypesEntity>(idEntity, tableAction),
                        EnumTable.Plu => DataAccess.ActionGetIdEntity<PluEntity>(idEntity, tableAction),
                        EnumTable.ProductionFacility => DataAccess.ActionGetIdEntity<ProductionFacilityEntity>(idEntity, tableAction),
                        EnumTable.ProductSeries => DataAccess.ActionGetIdEntity<ProductSeriesEntity>(idEntity, tableAction),
                        EnumTable.Scales => DataAccess.ActionGetIdEntity<ScalesEntity>(idEntity, tableAction),
                        EnumTable.TemplateResources => DataAccess.ActionGetIdEntity<TemplateResourcesEntity>(idEntity, tableAction),
                        EnumTable.Templates => DataAccess.ActionGetIdEntity<TemplatesEntity>(idEntity, tableAction),
                        EnumTable.WorkShop => DataAccess.ActionGetIdEntity<WorkshopEntity>(idEntity, tableAction),
                        EnumTable.WeithingFact => DataAccess.ActionGetIdEntity<WeithingFactEntity>(idEntity, tableAction),
                        EnumTable.Printer => DataAccess.ActionGetIdEntity<ZebraPrinterEntity>(idEntity, tableAction),
                        EnumTable.PrinterResourceRef => DataAccess.ActionGetIdEntity<ZebraPrinterResourceRefEntity>(idEntity, tableAction),
                        EnumTable.PrinterType => DataAccess.ActionGetIdEntity<ZebraPrinterTypeEntity>(idEntity, tableAction),
                        _ => throw new ArgumentOutOfRangeException(nameof(tableAction), tableAction, null)
                    };
                    title = table switch
                    {
                        EnumTable.BarCodeTypes => $"Штрих-код. ID {idEntity.Id}",
                        EnumTable.Contragents => $"Контрагент. ID {idEntity.Id}",
                        EnumTable.Hosts => $"Хост. ID {idEntity.Id}",
                        EnumTable.Nomenclature => $"Номенклатура. ID {idEntity.Id}",
                        EnumTable.OrderStatus => $"Статус заказа. ID {idEntity.Id}",
                        EnumTable.OrderTypes => $"Тип заказа. ID {idEntity.Id}",
                        EnumTable.Plu => $"PLU. ID {idEntity.Id}",
                        EnumTable.ProductionFacility => $"Производственная площадка. ID {idEntity.Id}",
                        EnumTable.ProductSeries => $"Серия продукта. ID {idEntity.Id}",
                        EnumTable.Scales => $"Устройство. ID {idEntity.Id}",
                        EnumTable.TemplateResources => $"Ресурс шаблона. ID {idEntity.Id}",
                        EnumTable.Templates => $"Шаблон. ID {idEntity.Id}",
                        EnumTable.WorkShop => $"Цех. ID {idEntity.Id}",
                        EnumTable.WeithingFact => $"Взвешивание. ID {idEntity.Id}",
                        EnumTable.Printer => $"Принтер Zebra. ID {idEntity.Id}",
                        EnumTable.PrinterResourceRef => $"Ресурс принтера. ID {idEntity.Id}",
                        EnumTable.PrinterType => $"Тип принтера. ID {idEntity.Id}",
                        _ => throw new ArgumentOutOfRangeException(nameof(tableAction), tableAction, null)
                    };
                }
                else if (entity is BaseUidEntity uidEntity)
                {
                    uidEntity = table switch
                    {
                        EnumTable.Logs => DataAccess.ActionGetUidEntity<LogEntity>(uidEntity, tableAction),
                        _ => throw new ArgumentOutOfRangeException(nameof(tableAction), tableAction, null)
                    };
                    title = table switch
                    {
                        EnumTable.Logs => $"Лог. UID {uidEntity.Uid}",
                        _ => throw new ArgumentOutOfRangeException(nameof(tableAction), tableAction, null)
                    };
                }

                // Printer from ZebraPrinter.razor.
                if (entity is ZebraPrinterResourceRefEntity zebraPrinterResourceRefEntity)
                {
                    zebraPrinterResourceRefEntity.Printer = (ZebraPrinterEntity) parentEntity;
                }

                if (tableAction == EnumTableAction.Add)
                {
                    if (entity is PluEntity pluEntity)
                    {
                        pluEntity.Scale = (ScalesEntity) parentEntity;
                    }
                }

                switch (tableAction)
                {
                    case EnumTableAction.Add:
                    case EnumTableAction.Edit:
                    case EnumTableAction.Copy:
                        await Dialog.OpenAsync<Components.EntityPage>(title,
                            new Dictionary<string, object>
                            {
                                {"Item", entity},
                                {"Table", table},
                                {"TableAction", tableAction},
                            },
                            new DialogOptions() {Width = "1400px", Height = "970px"}).ConfigureAwait(false);
                        break;
                    case EnumTableAction.Delete:
                        DataAccess.ActionDeleteEntity(entity);
                        break;
                    case EnumTableAction.Marked:
                        DataAccess.ActionMarkedEntity(entity);
                        break;
                }
            }
            catch (Exception ex)
            {
                var msg = new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Ошибка метода [{memberName}]!",
                    Detail = ex.Message,
                    Duration = LocalizationStrings.Timeout
                };
                Notification.Notify(msg);
                Console.WriteLine(ex.Message);
                Console.WriteLine($"{nameof(filePath)}: {filePath}. {nameof(lineNumber)}: {lineNumber}. {nameof(memberName)}: {memberName}.");
                DataAccess.LogExceptionToSql(ex, filePath, lineNumber, memberName);
            }
        }

        public async Task ActionAsync(EnumTable table, EnumTableAction tableAction, BaseEntity item, string page, bool isNewWindow,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

            try
            {
                if (table == EnumTable.Default)
                    return;
                //if (item == null || item.EqualsDefault())
                //    return;
                BaseIdEntity idEntity = null;
                BaseUidEntity uidEntity = null;
                if (item is BaseIdEntity baseIdEntity)
                    idEntity = baseIdEntity;
                else if (item is BaseUidEntity baseUidEntity)
                    uidEntity = baseUidEntity;

                switch (tableAction)
                {
                    case EnumTableAction.Add:
                    case EnumTableAction.Edit:
                    case EnumTableAction.Copy:
                        switch (table)
                        {
                            case EnumTable.Printer:
                                if (!isNewWindow)
                                {
                                    if (idEntity != null)
                                        Navigation.NavigateTo($"{page}/{idEntity.Id}");
                                    else if (uidEntity != null)
                                        Navigation.NavigateTo($"{page}/{uidEntity.Uid}");
                                    else
                                        Navigation.NavigateTo(@"{page}");
                                }
                                else
                                {
                                    if (idEntity != null)
                                        await JsRuntime.InvokeAsync<object>("open", $"{page}/{idEntity.Id}", "_blank").ConfigureAwait(false);
                                    else if (uidEntity != null)
                                        await JsRuntime.InvokeAsync<object>("open", $"{page}/{uidEntity.Uid}", "_blank").ConfigureAwait(false);
                                    else
                                        await JsRuntime.InvokeAsync<object>("open", $"{page}", "_blank").ConfigureAwait(false);
                                }
                                break;
                        }
                        break;
                    case EnumTableAction.Delete:
                        DataAccess.ActionDeleteEntity(item);
                        break;
                    case EnumTableAction.Marked:
                        DataAccess.ActionMarkedEntity(item);
                        break;
                }
            }
            catch (Exception ex)
            {
                var msg = new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Ошибка метода [{memberName}]!",
                    Detail = ex.Message,
                    Duration = LocalizationStrings.Timeout
                };
                Notification.Notify(msg);
                Console.WriteLine(ex.Message);
                Console.WriteLine($"{nameof(filePath)}: {filePath}. {nameof(lineNumber)}: {lineNumber}. {nameof(memberName)}: {memberName}.");
            }
        }

        public string ChartDataFormat(object value)
        {
            return ((int)value).ToString("####", CultureInfo.InvariantCulture);
        }

        public ChartCountEntity[] GetContragentsChartEntities(EnumField field)
        {
            var result = new ChartCountEntity[0];
            var entities = DataAccess.ContragentsCrud.GetEntities(null,
                new FieldOrderEntity(EnumField.CreateDate, EnumOrderDirection.Asc));
            var i = 0;
            switch (field)
            {
                case EnumField.CreateDate:
                    var entitiesDateCreated = new List<ChartCountEntity>();
                    foreach (var entity in entities)
                    {
                        if (entity.CreateDate != null)
                            entitiesDateCreated.Add(new ChartCountEntity(((DateTime)entity.CreateDate).Date, 1));
                        i++;
                    }
                    var entitiesGroupCreated = entitiesDateCreated.GroupBy(entity => entity.Date).ToArray();
                    result = new ChartCountEntity[entitiesGroupCreated.Length];
                    i = 0;
                    foreach (var entity in entitiesGroupCreated)
                    {
                        result[i] = new ChartCountEntity(entity.Key, entity.Count());
                        i++;
                    }
                    break;
                case EnumField.ModifiedDate:
                    var entitiesDateModified = new List<ChartCountEntity>();
                    foreach (var entity in entities)
                    {
                        if (entity.ModifiedDate != null)
                            entitiesDateModified.Add(new ChartCountEntity(((DateTime)entity.ModifiedDate).Date, 1));
                        i++;
                    }
                    var entitiesGroupModified = entitiesDateModified.GroupBy(entity => entity.Date).ToArray();
                    result = new ChartCountEntity[entitiesGroupModified.Length];
                    i = 0;
                    foreach (var entity in entitiesGroupModified)
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
            var result = new ChartCountEntity[0];
            var entities = DataAccess.NomenclatureCrud.GetEntities(null,
                new FieldOrderEntity(EnumField.CreateDate, EnumOrderDirection.Asc));
            var i = 0;
            switch (field)
            {
                case EnumField.CreateDate:
                    var entitiesDateCreated = new List<ChartCountEntity>();
                    foreach (var entity in entities)
                    {
                        if (entity.CreateDate != null)
                            entitiesDateCreated.Add(new ChartCountEntity(((DateTime)entity.CreateDate).Date, 1));
                        i++;
                    }
                    var entitiesGroupCreated = entitiesDateCreated.GroupBy(entity => entity.Date).ToArray();
                    result = new ChartCountEntity[entitiesGroupCreated.Length];
                    i = 0;
                    foreach (var entity in entitiesGroupCreated)
                    {
                        result[i] = new ChartCountEntity(entity.Key, entity.Count());
                        i++;
                    }
                    break;
                case EnumField.ModifiedDate:
                    var entitiesDateModified = new List<ChartCountEntity>();
                    foreach (var entity in entities)
                    {
                        if (entity.ModifiedDate != null)
                            entitiesDateModified.Add(new ChartCountEntity(((DateTime)entity.ModifiedDate).Date, 1));
                        i++;
                    }
                    
                    var entitiesModied = entitiesDateModified.GroupBy(entity => entity.Date).ToArray();
                    result = new ChartCountEntity[entitiesModied.Length];
                    i = 0;
                    foreach (var entity in entitiesModied)
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
            Navigation.NavigateTo($"{LocalizationStrings.UriRouteRoot}");
        }

        public ConfirmOptions GetConfirmOptions()
        {
            return new()
            {
                ShowTitle = true,
                ShowClose = true,
                OkButtonText = LocalizationStrings.DialogButtonYes,
                CancelButtonText = LocalizationStrings.DialogButtonCancel,
                Bottom = null,
                ChildContent = null,
                Height = null,
                Left = null,
                Style = null,
                Top = null,
                Width = null,
            };
        }

        public async Task RunTasks(string title, string detailSuccess, string detailFail, string detailCancel, List<Task> tasks,
            DelegateGuiRefresh callRefresh, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

            try
            {
                if (tasks != null)
                {
                    foreach (var task in tasks)
                    {
                        if (task != null)
                        {
                            task.Start();
                            task.Wait();
                        }
                    }
                }
                if (!string.IsNullOrEmpty(detailSuccess))
                    Notification.Notify(NotificationSeverity.Success, title + Environment.NewLine, detailSuccess, Delay);
                else
                {
                    if (!string.IsNullOrEmpty(detailCancel))
                        Notification.Notify(NotificationSeverity.Info, title + Environment.NewLine, detailCancel, Delay);
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    msg += Environment.NewLine + ex.InnerException.Message;
                if (!string.IsNullOrEmpty(detailFail))
                {
                    if (!string.IsNullOrEmpty(msg))
                        Notification.Notify(NotificationSeverity.Error, title + Environment.NewLine, detailFail + Environment.NewLine + msg, Delay);
                    else
                        Notification.Notify(NotificationSeverity.Error, title + Environment.NewLine, detailFail, Delay);
                }
                else
                {
                    if (!string.IsNullOrEmpty(msg))
                        Notification.Notify(NotificationSeverity.Error, title + Environment.NewLine, msg, Delay);
                }
                Console.WriteLine(msg);
                Console.WriteLine($"{nameof(filePath)}: {filePath}. {nameof(lineNumber)}: {lineNumber}. {nameof(memberName)}: {memberName}.");
            }
            finally
            {
                if (callRefresh != null)
                    await callRefresh().ConfigureAwait(false);
            }
        }

        public async Task RunTasksWithQeustion(string title, string detailSuccess, string detailFail, string detailCancel,
            List<Task> tasks, DelegateGuiRefresh callRefresh, string questionAdd = "",
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            var confirmOptions = GetConfirmOptions();

            try
            {
                var question = string.IsNullOrEmpty(questionAdd) ? LocalizationStrings.DialogQuestion : questionAdd;
                var result = Dialog.Confirm(question, title, confirmOptions).Result;
                if (result == true)
                {
                    if (tasks != null)
                    {
                        //Task.WaitAll(tasks.ToArray());
                        foreach (var task in tasks)
                        {
                            task.Start();
                            task.Wait();
                        }
                    }
                    if (!string.IsNullOrEmpty(detailSuccess))
                        Notification.Notify(NotificationSeverity.Success, title + Environment.NewLine, detailSuccess, Delay);
                }
                else
                {
                    if (!string.IsNullOrEmpty(detailCancel))
                        Notification.Notify(NotificationSeverity.Info, title + Environment.NewLine, detailCancel, Delay);
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    msg += Environment.NewLine + ex.InnerException.Message;
                if (!string.IsNullOrEmpty(detailFail))
                {
                    if (!string.IsNullOrEmpty(msg))
                        Notification.Notify(NotificationSeverity.Error, title + Environment.NewLine, detailFail + Environment.NewLine + msg, Delay);
                    else
                        Notification.Notify(NotificationSeverity.Error, title + Environment.NewLine, detailFail, Delay);
                }
                else
                {
                    if (!string.IsNullOrEmpty(msg))
                        Notification.Notify(NotificationSeverity.Error, title + Environment.NewLine, msg, Delay);
                }

                Console.WriteLine(msg);
                Console.WriteLine($"{nameof(filePath)}: {filePath}. {nameof(lineNumber)}: {lineNumber}. {nameof(memberName)}: {memberName}.");
            }
            finally
            {
                if (callRefresh != null)
                    await callRefresh().ConfigureAwait(false);
            }
        }

        #endregion

        #region Public and private methods - Memory manager

        public void MemoryOpen(MemoryEntity.DelegateGuiRefresh callRefresh)
        {
            if (Memory != null)
                return;
            Memory = new MemoryEntity(1_000, 5_000);
            Memory.Open(callRefresh);
        }

        #endregion
    }
}
