// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore;
using BlazorCore.DAL;
using BlazorCore.DAL.DataModels;
using BlazorCore.DAL.TableModels;
using Microsoft.AspNetCore.Components;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BlazorCore.Utils;

namespace BlazorDeviceControl.Shared.Record
{
    public partial class Printer
    {
        #region Public and private fields and properties

        [Parameter]
        public int? ItemId { get; set; }
        public ZebraPrinterEntity Item { get; set; }
        public ZebraPrinterResourceRefEntity ItemResource { get; set; } = null;
        public List<ZebraPrinterResourceRefEntity> ItemsResource { get; set; }
        public List<ZebraPrinterTypeEntity> ItemsZebraPrinterType { get; set; } = null;

        #endregion

        #region Public and private methods

        public override async Task GetDataAsync()
        {
            await GetDataAsync(new Task(delegate
            {
                Item = AppSettings.DataAccess.ZebraPrinterCrud.GetEntity(new FieldListEntity(new Dictionary<string, object>
                    { { EnumField.Id.ToString(), ItemId } }), null);

                ItemsZebraPrinterType = AppSettings.DataAccess.ZebraPrinterTypeCrud.GetEntities(null, null).ToList();

                ItemsResource = new List<ZebraPrinterResourceRefEntity>();
                ZebraPrinterResourceRefEntity[] items = AppSettings.DataAccess.ZebraPrinterResourceRefCrud.GetEntities(
                    new FieldListEntity(new Dictionary<string, object> { { "Printer.Id", Item.Id } }),
                    new FieldOrderEntity(EnumField.Description, EnumOrderDirection.Asc));
                ItemsResource.AddRange(items);
            }));
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);

            ItemId ??= 0;

            await GetDataAsync().ConfigureAwait(true);
        }

        private async Task RowSelectAsync(BaseIdEntity entity,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            try
            {
                if (entity is ZebraPrinterResourceRefEntity zebraPrinterResourceRefEntity)
                {
                    ItemResource = zebraPrinterResourceRefEntity;
                }
            }
            catch (Exception ex)
            {
                NotificationMessage msg = new()
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Ошибка метода [{memberName}]!",
                    Detail = ex.Message,
                    Duration = AppSettings.Delay
                };
                Notification.Notify(msg);
                Console.WriteLine(msg.Detail);
                AppSettings.DataAccess.LogExceptionToSql(ex, filePath, lineNumber, memberName);
            }
        }

        private async Task RowDoubleClickAsync(BaseIdEntity entity,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            try
            {
                if (entity is ZebraPrinterResourceRefEntity zebraPrinterResourceRefEntity)
                {
                    ItemResource = zebraPrinterResourceRefEntity;
                    await ActionEditAsync(EnumTable.PrinterResourceRef, ItemResource, Item).ConfigureAwait(true);
                }
            }
            catch (Exception ex)
            {
                NotificationMessage msg = new()
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Ошибка метода [{memberName}]!",
                    Detail = ex.Message,
                    Duration = AppSettings.Delay
                };
                Notification.Notify(msg);
                Console.WriteLine(msg.Detail);
                AppSettings.DataAccess.LogExceptionToSql(ex, filePath, lineNumber, memberName);
            }
        }

        private void OnChange(object value, string name)
        {
            switch (name)
            {
                case "ZebraPrinterTypeItems":
                    if (value is int id)
                    {
                        if (id <= 0)
                            Item.PrinterType = null;
                        else
                        {
                            Item.PrinterType = AppSettings.DataAccess.ZebraPrinterTypeCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> { { EnumField.Id.ToString(), id } }),
                            null);
                        }
                    }
                    break;
            }
            StateHasChanged();
        }

        private async Task ActionEditAsync(EnumTable table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            await ActionAsync(table, EnumTableAction.Edit, entity, parentEntity).ConfigureAwait(true);
            await GetDataAsync().ConfigureAwait(false);
        }

        private async Task ActionAddAsync(EnumTable table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            await ActionAsync(table, EnumTableAction.Add, entity, parentEntity).ConfigureAwait(true);
            await GetDataAsync().ConfigureAwait(false);
        }

        private async Task ActionCopyAsync(EnumTable table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            await ActionAsync(table, EnumTableAction.Copy, entity, parentEntity).ConfigureAwait(true);
            await GetDataAsync().ConfigureAwait(false);
        }

        private async Task ActionDeleteAsync(EnumTable table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            await ActionAsync(table, EnumTableAction.Delete, entity, parentEntity).ConfigureAwait(true);
            await GetDataAsync().ConfigureAwait(false);
        }

        private void Save()
        {
            if (Item == null || Item.EqualsDefault())
                return;
            if (ItemId == 0)
            {
                AppSettings.DataAccess.ZebraPrinterCrud.SaveEntity(Item);
            }
            else
            {
                AppSettings.DataAccess.ZebraPrinterCrud.UpdateEntity(Item);
            }
            Navigation.NavigateTo($"{LocalizationStrings.DeviceControl.UriRouteTablePrinters}");
        }

        private async Task SaveAsync()
        {
            Task task = new(Save);
            await RunTasksWithQeustion(LocalizationStrings.Share.TableRecordSave,
                LocalizationStrings.Share.DialogResultSuccess, LocalizationStrings.Share.DialogResultFail, LocalizationStrings.Share.DialogResultCancel,
                new List<Task> { task }, GuiRefreshAsync);
        }

        private async Task CancelAsync()
        {
            await RunTasks(LocalizationStrings.Share.TableRecordCancel,
                LocalizationStrings.Share.DialogResultSuccess, LocalizationStrings.Share.DialogResultFail, LocalizationStrings.Share.DialogResultCancel,
                new List<Task> {
                    new(() => {
                        if (Item == null || Item.EqualsDefault())
                            return;
                        Navigation.NavigateTo($"{LocalizationStrings.DeviceControl.UriRouteTablePrinters}");
                }),
            }, GuiRefreshAsync).ConfigureAwait(false);
        }

        #endregion
    }
}