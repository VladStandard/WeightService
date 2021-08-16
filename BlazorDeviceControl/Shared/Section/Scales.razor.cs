// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore;
using BlazorCore.DAL;
using BlazorCore.DAL.DataModels;
using BlazorCore.Models;
using BlazorCore.Utils;
using Microsoft.AspNetCore.Components;
using Radzen;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Section
{
    public partial class Scales
    {
        #region Public and private fields and properties

        private void ShowTooltipGetData(ElementReference elementReference, TooltipOptions options = null) =>
            Tooltip.Open(elementReference, LocalizationStrings.DeviceControl.TableReadData, options);
        public BaseIdEntity Item { get; set; }
        public BaseIdEntity[] Items { get; set; }

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);

            await GetDataAsync(new Task(delegate
            {
                Items = AppSettings.DataAccess.ScalesCrud.GetEntities(
                    new FieldListEntity(new Dictionary<string, object> { { EnumField.Marked.ToString(), false } }),
                    new FieldOrderEntity(EnumField.Description, EnumOrderDirection.Asc));
            }), false).ConfigureAwait(false);
        }

        private async Task ItemSelectAsync(BaseIdEntity entity)
        {
            await RunTasksAsync(LocalizationStrings.Share.TableRead, "", LocalizationStrings.Share.DialogResultFail, "",
                new List<Task> {
                    new Task(delegate
                    {
                        Item = entity;
                    })
                }, GuiRefreshAsync, true).ConfigureAwait(false);
        }

        private async Task ItemEditAsync()
        {
            await RunTasksAsync(LocalizationStrings.Share.TableRead, "", LocalizationStrings.Share.DialogResultFail, "",
                new List<Task> { new Task(delegate {
                        //ActionAsync(EnumTable.Scales, EnumTableAction.Edit, Item, null).ConfigureAwait(true);
                        ActionAsync(EnumTable.Scales, EnumTableAction.Edit, Item, LocalizationStrings.DeviceControl.UriRouteItemScale, false)
                            .ConfigureAwait(true);
                })}, GuiRefreshAsync, true).ConfigureAwait(false);
        }

        private async Task ItemAddAsync(EnumTable table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Add, entity, parentEntity).ConfigureAwait(true);
            await SetParametersAsync(new ParameterView()).ConfigureAwait(false);
        }

        private async Task ItemCopyAsync(EnumTable table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Copy, entity, parentEntity).ConfigureAwait(true);
            await SetParametersAsync(new ParameterView()).ConfigureAwait(false);
        }

        private async Task ItemDeleteAsync(EnumTable table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Delete, entity, parentEntity).ConfigureAwait(true);
            await SetParametersAsync(new ParameterView()).ConfigureAwait(false);
        }

        private async Task ItemMarkedAsync(EnumTable table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Mark, entity, parentEntity).ConfigureAwait(true);
            await SetParametersAsync(new ParameterView()).ConfigureAwait(false);
        }

        #endregion
    }
}