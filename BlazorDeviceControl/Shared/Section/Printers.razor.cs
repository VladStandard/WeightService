// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore;
using BlazorCore.DAL;
using BlazorCore.DAL.DataModels;
using BlazorCore.Models;
using Microsoft.AspNetCore.Components;
using Radzen;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BlazorCore.Utils;

namespace BlazorDeviceControl.Shared.Section
{
    public partial class Printers
    {
        #region Public and private fields and properties

        private void ShowTooltipGetData(ElementReference elementReference, TooltipOptions options = null) =>
            Tooltip.Open(elementReference, LocalizationStrings.DeviceControl.TableReadData, options);
        public BaseIdEntity Entity { get; set; }
        public BaseIdEntity[] Entities { get; set; }
        private List<TypeEntity<string>> TemplateCategories { get; set; }
        private string TemplateCategory { get; set; }

        #endregion

        #region Public and private methods

        public override async Task GetDataAsync()
        {
            await GetDataAsync(new Task(delegate
            {
                Entities = AppSettings.DataAccess.ZebraPrinterCrud.GetEntities(
                    new FieldListEntity(new Dictionary<string, object> { { EnumField.Marked.ToString(), false } }),
                    new FieldOrderEntity(EnumField.Name, EnumOrderDirection.Asc));
            }));
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);

            await GetDataAsync().ConfigureAwait(true);
        }

        private string GetHeader() => LocalizationStrings.DeviceControl.TableTitlePrinters;

        private async Task RowSelectAsync(BaseIdEntity entity,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            try
            {
                Entity = entity;
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
                Entity = entity;
                await ActionEditAsync(EnumTable.Printer, Entity, null).ConfigureAwait(true);
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

        private async Task ActionEditAsync(EnumTable table, BaseIdEntity item, BaseIdEntity parentEntity)
        {
            if (AppSettings.IdentityAccessLevel != true)
                return;

            await RunTasks(LocalizationStrings.DeviceControl.GetItemTitle(table), "", LocalizationStrings.Share.DialogResultFail, "",
                new List<Task> {
                    new(() => {
                        ActionAsync(table, EnumTableAction.Edit, item, LocalizationStrings.DeviceControl.UriRouteItemPrinter, false)
                            .ConfigureAwait(true);
                    }),
                }, GuiRefreshAsync).ConfigureAwait(false);
        }

        private async Task ActionAddAsync(EnumTable table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            if (AppSettings.IdentityAccessLevel != true)
                return;

            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Add, entity, parentEntity).ConfigureAwait(true);
            await GetDataAsync().ConfigureAwait(false);
        }

        private async Task ActionCopyAsync(EnumTable table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            if (AppSettings.IdentityAccessLevel != true)
                return;

            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Copy, entity, parentEntity).ConfigureAwait(true);
            await GetDataAsync().ConfigureAwait(false);
        }

        private async Task ActionDeleteAsync(EnumTable table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            if (AppSettings.IdentityAccessLevel != true)
                return;

            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Delete, entity, parentEntity).ConfigureAwait(true);
            await GetDataAsync().ConfigureAwait(false);
        }

        private async Task ActionMarkedAsync(EnumTable table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            if (AppSettings.IdentityAccessLevel != true)
                return;

            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Marked, entity, parentEntity).ConfigureAwait(true);
            await GetDataAsync().ConfigureAwait(false);
        }

        private async Task OnChange(object value, string name)
        {
            switch (name)
            {
                case "TemlateCategories":
                    if (value is string strValue)
                    {
                        TemplateCategory = strValue;
                    }
                    break;
            }
            StateHasChanged();
            await GetDataAsync().ConfigureAwait(false);
        }

        #endregion
    }
}
