// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore;
using BlazorCore.DAL;
using BlazorCore.DAL.DataModels;
using BlazorCore.DAL.TableModels;
using BlazorCore.Models;
using BlazorCore.Utils;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Section
{
    public partial class Scales
    {
        #region Public and private fields and properties

        private List<ScalesEntity> Items { get; set; }

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);

            RunTasks(LocalizationStrings.DeviceControl.MethodSetParametersAsync, "", LocalizationStrings.Share.DialogResultFail, "",
                new List<Task> {
                    new(async() => {
                        IdItem = null;
                        Items = AppSettings.DataAccess.ScalesCrud.GetEntities(
                            new FieldListEntity(new Dictionary<string, object> { { EnumField.Marked.ToString(), false } }),
                            new FieldOrderEntity(EnumField.Description, EnumOrderDirection.Asc))
                        .OrderBy(x => x.Description).ToList();
                        await GuiRefreshAsync(false).ConfigureAwait(false);
                    }),
            }, true);
        }

        private async Task ItemEditAsync()
        {
            //await RunTasksAsync(LocalizationStrings.Share.TableRead, "", LocalizationStrings.Share.DialogResultFail, "",
            //    new List<Task> { new Task(delegate {
            //            //ActionAsync(EnumTable.Scales, EnumTableAction.Edit, Item, null).ConfigureAwait(true);
            //            ActionAsync(EnumTableScales.Scales, EnumTableAction.Edit, Item, LocalizationStrings.DeviceControl.UriRouteItemScale, false)
            //                .ConfigureAwait(true);
            //    })}, GuiRefreshAsync, true).ConfigureAwait(false);
            RunTasks(LocalizationStrings.DeviceControl.MethodSetParametersAsync, "", LocalizationStrings.Share.DialogResultFail, "",
              new List<Task> {
                  new(async() => {
                      await ActionAsync(EnumTableScales.Scales, EnumTableAction.Edit, IdItem, LocalizationStrings.DeviceControl.UriRouteItemScale, false).ConfigureAwait(true);
                  }),
              }, true);
        }

        private async Task ItemAddAsync(EnumTableScales table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Add, entity, parentEntity).ConfigureAwait(true);
            await SetParametersAsync(new ParameterView()).ConfigureAwait(false);
        }

        private async Task ItemCopyAsync(EnumTableScales table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Copy, entity, parentEntity).ConfigureAwait(true);
            await SetParametersAsync(new ParameterView()).ConfigureAwait(false);
        }

        private async Task ItemDeleteAsync(EnumTableScales table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Delete, entity, parentEntity).ConfigureAwait(true);
            await SetParametersAsync(new ParameterView()).ConfigureAwait(false);
        }

        private async Task ItemMarkedAsync(EnumTableScales table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Mark, entity, parentEntity).ConfigureAwait(true);
            await SetParametersAsync(new ParameterView()).ConfigureAwait(false);
        }

        #endregion
    }
}