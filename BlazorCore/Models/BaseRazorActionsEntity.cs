using BlazorCore.DAL;
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

        public async Task ActionNewAsync(EnumTableScales table, BaseEntity item, string page, bool isNewWindow = false, BaseEntity parentItem = null)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            Action(table, EnumTableAction.New, item, page, isNewWindow, parentItem);
        }

        public async Task ActionAddAsync(EnumTableScales table, BaseEntity item, string page, bool isNewWindow = false, BaseEntity parentItem = null)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            Action(table, EnumTableAction.Add, item, page, isNewWindow, parentItem);
        }

        public async Task ActionEditAsync(EnumTableScales table, BaseEntity item, string page, bool isNewWindow = false, BaseEntity parentItem = null)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            Action(table, EnumTableAction.Edit, item, page, isNewWindow, parentItem);
        }

        public async Task ActionCopyAsync(EnumTableScales table, BaseEntity item, string page, bool isNewWindow = false, BaseEntity parentItem = null)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            Action(table, EnumTableAction.Copy, item, page, isNewWindow, parentItem);
        }

        public async Task ActionMarkAsync(EnumTableScales table, BaseEntity item, string page, bool isNewWindow = false, BaseEntity parentItem = null)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            Action(table, EnumTableAction.Mark, item, page, isNewWindow, parentItem);
        }

        public async Task ActionDeleteAsync(EnumTableScales table, BaseEntity item, string page, bool isNewWindow = false, BaseEntity parentItem = null)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            Action(table, EnumTableAction.Delete, item, page, isNewWindow, parentItem);
        }

        #endregion
    }
}