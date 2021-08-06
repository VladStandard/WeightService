// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore;
using BlazorCore.DAL;
using BlazorCore.Models;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared
{
    public partial class EntityActions
    {
        #region Public and private fields and properties

        [Parameter] public EnumTable Table { get; set; }
        [Parameter] public BaseEntity ParentItem { get; set; }
        [Parameter] public BaseEntity ChildItem { get; set; }
        [Parameter] public bool IsAccessAdd { get; set; }
        [Parameter] public bool IsAccessEdit { get; set; }
        [Parameter] public bool IsAccessCopy { get; set; }
        [Parameter] public bool IsAccessNew { get; set; }
        [Parameter] public bool IsAccessDelete { get; set; }
        [Parameter] public bool IsAccessMark { get; set; }

        #endregion

        #region Public and private methods

        private async Task ActionEditAsync(EnumTable table, BaseEntity item, BaseEntity parentItem)
        {
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Edit, item, parentItem).ConfigureAwait(true);
            await GetDataAsync().ConfigureAwait(false);
        }

        private async Task ActionAddAsync(EnumTable table, BaseEntity item, BaseEntity parentItem)
        {
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Add, item, parentItem).ConfigureAwait(true);
            await GetDataAsync().ConfigureAwait(false);
        }

        private async Task ActionCopyAsync(EnumTable table, BaseEntity item, BaseEntity parentItem)
        {
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Copy, item, parentItem).ConfigureAwait(true);
            await GetDataAsync().ConfigureAwait(false);
        }

        private async Task ActionMarkedAsync(EnumTable table, BaseEntity item, BaseEntity parentItem)
        {
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Marked, item, parentItem).ConfigureAwait(true);
            await GetDataAsync().ConfigureAwait(false);
        }

        #endregion
    }
}