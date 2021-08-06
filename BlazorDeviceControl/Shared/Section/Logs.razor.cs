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
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BlazorCore.Utils;
using BlazorCore.Models;

namespace BlazorDeviceControl.Shared.Section
{
    public partial class Logs
    {
        #region Public and private fields and properties

        private void ShowTooltipGetData(ElementReference elementReference, TooltipOptions options = null) =>
            Tooltip.Open(elementReference, LocalizationStrings.DeviceControl.TableReadData, options);
        public BaseUidEntity Item { get; set; }
        public List<LogSummaryEntity> Items { get; set; }
        public object[] Objects { get; set; }

        #endregion

        #region Public and private methods

        public override async Task GetDataAsync()
        {
            await GetDataAsync(new Task(delegate
            {
                Objects = AppSettings.DataAccess.GetEntitiesNativeObject(SqlQueries.GetLogs, string.Empty, 0, string.Empty);
                Items = new List<LogSummaryEntity>();
                foreach (object obj in Objects)
                {
                    if (obj is object[] { Length: 11 } item)
                    {
                        if (Guid.TryParse(Convert.ToString(item[0]), out Guid uid))
                        {
                            Items.Add(new LogSummaryEntity()
                            {
                                Uid = uid,
                                CreateDt = Convert.ToDateTime(item[1]),
                                Scale = Convert.ToString(item[2]),
                                Host = Convert.ToString(item[3]),
                                App = Convert.ToString(item[4]),
                                Version = Convert.ToString(item[5]),
                                File = Convert.ToString(item[6]),
                                Line = Convert.ToInt32(item[7]),
                                Member = Convert.ToString(item[8]),
                                Icon = Convert.ToString(item[9]),
                                Message = Convert.ToString(item[10]),
                            });
                        }
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

        private async Task ActionEditAsync(BaseUidEntity item, BaseUidEntity parentEntity)
        {
            //var title = LocalizationStrings.GetItemTitle(EnumTable.Logs);
            await ActionAsync<BaseRazorEntity>(EnumTable.Logs, EnumTableAction.Edit, item, parentEntity).ConfigureAwait(true);
            await GetDataAsync().ConfigureAwait(false);
        }

        private async Task ActionAddAsync(EnumTable table, BaseUidEntity entity, BaseUidEntity parentEntity)
        {
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Add, entity, parentEntity).ConfigureAwait(true);
            await GetDataAsync().ConfigureAwait(false);
        }

        private async Task ActionCopyAsync(EnumTable table, BaseUidEntity entity, BaseUidEntity parentEntity)
        {
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Copy, entity, parentEntity).ConfigureAwait(true);
            await GetDataAsync().ConfigureAwait(false);
        }

        private async Task ActionDeleteAsync(EnumTable table, BaseUidEntity entity, BaseUidEntity parentEntity)
        {
            LogEntity logEntity = AppSettings.DataAccess.LogCrud.GetEntity(new FieldListEntity(
            new Dictionary<string, object> { { EnumField.Uid.ToString(), entity.Uid } }), null);
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Delete, logEntity, parentEntity).ConfigureAwait(true);
            await GetDataAsync().ConfigureAwait(false);
        }

        private async Task ActionMarkedAsync(EnumTable table, BaseUidEntity entity, BaseUidEntity parentEntity)
        {
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Marked, entity, parentEntity).ConfigureAwait(true);
            await GetDataAsync().ConfigureAwait(false);
        }

        private async Task RowSelectAsync(LogSummaryEntity entity,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
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

        #endregion
    }
}