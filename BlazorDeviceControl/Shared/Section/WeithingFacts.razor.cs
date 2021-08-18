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
using BlazorCore.Models;

namespace BlazorDeviceControl.Shared.Section
{
    public partial class WeithingFacts
    {
        #region Public and private fields and properties

        public BaseIdEntity Item { get; set; }
        public List<WeithingFactSummaryEntity> Items { get; set; }
        public object[] Objects { get; set; }
        private string TemplateCategory { get; set; }

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);

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
            }), false).ConfigureAwait(false);
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
                    Duration = AppSettingsEntity.Delay
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
                await ActionEditAsync(EnumTableScales.WeithingFact, Item, null).ConfigureAwait(true);
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
                AppSettings.DataAccess.LogExceptionToSql(ex, filePath, lineNumber, memberName);
            }
        }

        private async Task ActionEditAsync(EnumTableScales table, BaseIdEntity item, BaseIdEntity parentEntity)
        {
            // Backup
            //await AppSettings.ActionAsync(table, EnumTableAction.Edit, entity, parentEntity).ConfigureAwait(true);
            //await SetParametersAsync(new ParameterView()).ConfigureAwait(false);

            Task task = null;
            string title = LocalizationStrings.DeviceControl.GetItemTitle(table);
            switch (table)
            {
                case EnumTableScales.Printer:
                    task = new Task(() =>
                    {
                        Action(table, EnumTableAction.Edit, item, LocalizationStrings.DeviceControl.UriRouteItemPrinter, false);
                    });
                    break;
                default:
                    Action(table, EnumTableAction.Edit, item, LocalizationStrings.DeviceControl.UriRouteItemPrinter, false, parentEntity);
                    await SetParametersAsync(new ParameterView()).ConfigureAwait(false);
                    break;
            }
            await RunTasksAsync(title, "", LocalizationStrings.Share.DialogResultFail, "",
                new List<Task> {
                    task,
                }, GuiRefreshAsync, true).ConfigureAwait(false);
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

        private async Task ActionMarkedAsync(EnumTableScales table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Mark, entity, parentEntity).ConfigureAwait(true);
            await SetParametersAsync(new ParameterView()).ConfigureAwait(false);
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
            await SetParametersAsync(new ParameterView()).ConfigureAwait(false);
        }

        #endregion
    }
}