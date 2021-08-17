﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore;
using BlazorCore.DAL;
using BlazorCore.DAL.DataModels;
using BlazorCore.DAL.TableModels;
using BlazorCore.Models;
using Microsoft.AspNetCore.Components;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Item
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

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);

            ItemId ??= 0;

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
            }), false).ConfigureAwait(false);
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
                    Duration = AppSettingsEntity.Delay
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
                    await ActionEditAsync(EnumTableScales.PrinterResourceRef, ItemResource, Item).ConfigureAwait(true);
                }
            }
            catch (Exception ex)
            {
                NotificationMessage msg = new()
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Ошибка метода [{memberName}]!",
                    Detail = ex.Message,
                    Duration = AppSettingsEntity.Delay
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

        private async Task ActionEditAsync(EnumTableScales table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Edit, entity, parentEntity).ConfigureAwait(true);
            await SetParametersAsync(new ParameterView()).ConfigureAwait(false);
        }

        private async Task ActionAddAsync(EnumTableScales table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Add, entity, parentEntity).ConfigureAwait(true);
            await SetParametersAsync(new ParameterView()).ConfigureAwait(false);
        }

        private async Task ActionCopyAsync(EnumTableScales table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Copy, entity, parentEntity).ConfigureAwait(true);
            await SetParametersAsync(new ParameterView()).ConfigureAwait(false);
        }

        private async Task ActionDeleteAsync(EnumTableScales table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Delete, entity, parentEntity).ConfigureAwait(true);
            await SetParametersAsync(new ParameterView()).ConfigureAwait(false);
        }

        #endregion
    }
}