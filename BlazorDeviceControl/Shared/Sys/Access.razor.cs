// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore;
using BlazorCore.DAL;
using BlazorCore.DAL.DataModels;
using BlazorCore.DAL.TableModels;
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
    public partial class Access
    {
        #region Public and private fields and properties

        public BaseUidEntity Item { get; set; }
        public List<AccessEntity> Items { get; set; }
        public object[] Objects { get; set; }

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);

            RunTasks(LocalizationStrings.DeviceControl.MethodSetParametersAsync, "", LocalizationStrings.Share.DialogResultFail, "",
                new List<Task> {
                    new(async() => {
                        Objects = AppSettings.DataAccess.GetEntitiesNativeObject(SqlQueries.GetAccess, string.Empty, 0, string.Empty);
                        Item = null;
                        Items = new List<AccessEntity>();
                        foreach (object obj in Objects)
                        {
                            if (obj is object[] { Length: 5 } item)
                            {
                                if (Guid.TryParse(Convert.ToString(item[0]), out Guid uid))
                                {
                                    Items.Add(new AccessEntity()
                                    {
                                        Uid = uid,
                                        CreateDt = Convert.ToDateTime(item[1]),
                                        ChangeDt = Convert.ToDateTime(item[2]),
                                        User = Convert.ToString(item[3]),
                                        Level = item[4] == null ? null : Convert.ToBoolean(item[4]),
                                    });
                                }
                            }
                        }
                        await GuiRefreshAsync(false).ConfigureAwait(false);
                      }),
                  }, true);

        }

        private async Task ActionEditAsync(BaseUidEntity item, BaseUidEntity parentEntity)
        {
            await ActionAsync<BaseRazorEntity>(EnumTableScales.Logs, EnumTableAction.Edit, item, parentEntity).ConfigureAwait(true);
            await SetParametersAsync(new ParameterView()).ConfigureAwait(false);
        }

        private async Task ActionAddAsync(EnumTableScales table, BaseUidEntity entity, BaseUidEntity parentEntity)
        {
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Add, entity, parentEntity).ConfigureAwait(true);
            await SetParametersAsync(new ParameterView()).ConfigureAwait(false);
        }

        private async Task ActionCopyAsync(EnumTableScales table, BaseUidEntity entity, BaseUidEntity parentEntity)
        {
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Copy, entity, parentEntity).ConfigureAwait(true);
            await SetParametersAsync(new ParameterView()).ConfigureAwait(false);
        }

        private async Task ActionDeleteAsync(EnumTableScales table, BaseUidEntity entity, BaseUidEntity parentEntity)
        {
            LogEntity logEntity = AppSettings.DataAccess.LogCrud.GetEntity(new FieldListEntity(
            new Dictionary<string, object> { { EnumField.Uid.ToString(), entity.Uid } }), null);
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Delete, logEntity, parentEntity).ConfigureAwait(true);
            await SetParametersAsync(new ParameterView()).ConfigureAwait(false);
        }

        private async Task ActionMarkedAsync(EnumTableScales table, BaseUidEntity entity, BaseUidEntity parentEntity)
        {
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Mark, entity, parentEntity).ConfigureAwait(true);
            await SetParametersAsync(new ParameterView()).ConfigureAwait(false);
        }

        private async Task RowSelectAsync(AccessEntity entity,
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