// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore;
using BlazorCore.DAL;
using BlazorCore.Models;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class EntityActions
    {
        #region Public and private fields and properties

        [Parameter] public EnumTable Table { get; set; }
        [Parameter] public BaseEntity ParentItem { get; set; }
        [Parameter] public BaseEntity ChildItem { get; set; }
        [Parameter] public bool IsShowAdd { get; set; }
        [Parameter] public bool IsShowEdit { get; set; }
        [Parameter] public bool IsShowCopy { get; set; }
        [Parameter] public bool IsShowNew { get; set; }
        [Parameter] public bool IsShowMark { get; set; }
        [Parameter] public bool IsShowDelete { get; set; }

        #endregion

        #region Public and private methods

        private async Task ActionNewAsync(EnumTable table, BaseEntity item, BaseEntity parentItem)
        {
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.New, item, parentItem).ConfigureAwait(true);
            await SetParametersAsync(new ParameterView()).ConfigureAwait(false);
        }

        private async Task ActionAddAsync(EnumTable table, BaseEntity item, BaseEntity parentItem)
        {
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Add, item, parentItem).ConfigureAwait(true);
            await SetParametersAsync(new ParameterView()).ConfigureAwait(false);
        }

        private async Task ActionEditAsync(EnumTable table, BaseEntity item, BaseEntity parentItem)
        {
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Edit, item, parentItem).ConfigureAwait(true);
            await SetParametersAsync(new ParameterView()).ConfigureAwait(false);
        }

        private async Task ActionCopyAsync(EnumTable table, BaseEntity item, BaseEntity parentItem)
        {
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Copy, item, parentItem).ConfigureAwait(true);
            await SetParametersAsync(new ParameterView()).ConfigureAwait(false);
        }

        private async Task ActionMarkAsync(EnumTable table, BaseEntity item, BaseEntity parentItem)
        {
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Mark, item, parentItem).ConfigureAwait(true);
            await SetParametersAsync(new ParameterView()).ConfigureAwait(false);
        }

        private async Task ActionDeleteAsync(EnumTable table, BaseEntity item, BaseEntity parentItem)
        {
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Delete, item, parentItem).ConfigureAwait(true);
            await SetParametersAsync(new ParameterView()).ConfigureAwait(false);
        }

        #endregion
    }
}