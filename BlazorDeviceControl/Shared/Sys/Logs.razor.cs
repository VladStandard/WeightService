// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.DAL;
using BlazorCore.DAL.DataModels;
using BlazorCore.Models;
using BlazorCore.Utils;
using Microsoft.AspNetCore.Components;
using Radzen;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Sys
{
    public partial class Logs
    {
        #region Public and private fields and properties

        private BaseUidEntity Item { get; set; }
        private List<LogSummaryEntity> Items { get; set; }
        private object[] Objects { get; set; }

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationStrings.DeviceControl.Method} {nameof(SetParametersAsync)}", "", LocalizationStrings.Share.DialogResultFail, "",
                new List<Task> {
                    new(async() => {
                        Objects = AppSettings.DataAccess.GetEntitiesNativeObject(SqlQueries.GetLogs, string.Empty, 0, string.Empty);
                        Item = null;
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
                        await GuiRefreshAsync(false).ConfigureAwait(false);
                    }),
            }, true);
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
                    Duration = AppSettingsEntity.Delay
                };
                Notification.Notify(msg);
                AppSettings.DataAccess.LogExceptionToSql(ex, filePath, lineNumber, memberName);
            }
        }

        #endregion
    }
}