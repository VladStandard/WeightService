// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore;
using BlazorCore.DAL;
using BlazorCore.DAL.DataModels;
using Microsoft.AspNetCore.Components;
using Radzen;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BlazorCore.Utils;

namespace BlazorDeviceControl.Shared.Section
{
    public partial class WeithingFacts
    {
        #region Public and private fields and properties

        private void ShowTooltipGetData(ElementReference elementReference, TooltipOptions options = null) =>
            Tooltip.Open(elementReference, LocalizationStrings.DeviceControl.TableReadData, options);
        public BaseIdEntity Item { get; set; }
        public List<WeithingFactSummaryEntity> Items { get; set; }
        public object[] Objects { get; set; }
        private string TemplateCategory { get; set; }

        #endregion

        #region Public and private methods

        public override async Task GetDataAsync()
        {
            await GetDataAsync(new Task(delegate
            {
                Objects = AppSettings.DataAccess.GetEntitiesNativeObject(SqlQueries.GetWeithingFacts, string.Empty, 0, string.Empty);
                Items = new List<WeithingFactSummaryEntity>();
                foreach (object obj in Objects)
                {
                    if (obj is object[] { Length: 5 } item)
                    {
                        Items.Add(new WeithingFactSummaryEntity
                        {
                            WeithingDate = Convert.ToDateTime(item[0]),
                            Count = Convert.ToInt32(item[1]),
                            Scale = Convert.ToString(item[2]),
                            Host = Convert.ToString(item[3]),
                            Printer = Convert.ToString(item[4]),
                        });
                    }
                }
            }));
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);

            await GetDataAsync().ConfigureAwait(true);
        }
        
        private string GetHeader()
        {
            return LocalizationStrings.DeviceControl.TableTitleWeithingFactShort;
        }

        private async Task RowSelectAsync(BaseIdEntity entity, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            try
            {
                Item = entity;
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
                AppSettings.DataAccess.LogExceptionToSql(ex, filePath, lineNumber, memberName);
            }
        }

        private async Task RowDoubleClickAsync(BaseIdEntity entity,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            try
            {
                Item = entity;
                await ActionEditAsync(EnumTable.WeithingFact, Item, null).ConfigureAwait(true);
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
                AppSettings.DataAccess.LogExceptionToSql(ex, filePath, lineNumber, memberName);
            }
        }

        private async Task ActionEditAsync(EnumTable table, BaseIdEntity item, BaseIdEntity parentEntity)
        {
            // Backup
            //await AppSettings.ActionAsync(table, EnumTableAction.Edit, entity, parentEntity).ConfigureAwait(true);
            //await GetDataAsync().ConfigureAwait(false);

            Task task = null;
            string title = LocalizationStrings.DeviceControl.GetItemTitle(table);
            switch (table)
            {
                case EnumTable.Printer:
                    task = new Task(() =>
                    {
                        ActionAsync(table, EnumTableAction.Edit, item, LocalizationStrings.DeviceControl.UriRouteItemPrinter, false)
                            .ConfigureAwait(true);
                    });
                    break;
                default:
                    await ActionAsync(table, EnumTableAction.Edit, item, parentEntity).ConfigureAwait(true);
                    await GetDataAsync().ConfigureAwait(false);
                    break;
            }
            await RunTasks(title, "", LocalizationStrings.Share.DialogResultFail, "",
                new List<Task> {
                    task,
                }, GuiRefreshAsync).ConfigureAwait(false);
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

        private async Task ActionMarkedAsync(EnumTable table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            await ActionAsync(table, EnumTableAction.Marked, entity, parentEntity).ConfigureAwait(true);
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