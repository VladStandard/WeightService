using BlazorCore.DAL;
using BlazorCore.Utils;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BlazorCore.Models
{
    public class BaseRazorActionsEntity : BaseRazorEntity
    {
        #region Public and private fields and properties

        [Parameter] public EnumTableScales Table { get; set; }
        [Parameter] public bool IsShowAdd { get; set; }
        [Parameter] public bool IsShowEdit { get; set; }
        [Parameter] public bool IsShowCopy { get; set; }
        [Parameter] public bool IsShowNew { get; set; }
        [Parameter] public bool IsShowMark { get; set; }
        [Parameter] public bool IsShowDelete { get; set; }

        #endregion

        #region Constructor and destructor

        public BaseRazorActionsEntity() { }

        #endregion

        #region Public and private methods

        public async Task ActionNewAsync(EnumTableScales table, BaseEntity item, BaseEntity parentItem = null)
        {
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.New, item, parentItem).ConfigureAwait(true);
            await SetParametersAsync(new ParameterView()).ConfigureAwait(false);
        }

        public async Task ActionAddAsync(EnumTableScales table, BaseEntity item, BaseEntity parentItem = null)
        {
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Add, item, parentItem).ConfigureAwait(true);
            await SetParametersAsync(new ParameterView()).ConfigureAwait(false);
        }

        public async Task ActionEditAsync(EnumTableScales table, BaseEntity item, string page, bool isNewWindow, 
            BaseEntity parentItem = null)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

            //Action<BaseRazorEntity>(table, EnumTableAction.Edit, item, parentItem);
            Action(table, EnumTableAction.Edit, item, page, isNewWindow, parentItem);
        }

        public async Task ActionCopyAsync(EnumTableScales table, BaseEntity item, BaseEntity parentItem = null)
        {
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Copy, item, parentItem).ConfigureAwait(true);
            await SetParametersAsync(new ParameterView()).ConfigureAwait(false);
        }

        public async Task ActionMarkAsync(EnumTableScales table, BaseEntity item, BaseEntity parentItem = null)
        {
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Mark, item, parentItem).ConfigureAwait(true);
            await SetParametersAsync(new ParameterView()).ConfigureAwait(false);
        }

        public async Task ActionDeleteAsync(EnumTableScales table, BaseEntity item, BaseEntity parentItem = null)
        {
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Delete, item, parentItem).ConfigureAwait(true);
            await SetParametersAsync(new ParameterView()).ConfigureAwait(false);
        }

        #endregion
    }
}