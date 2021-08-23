// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.DAL;
using BlazorCore.Utils;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorCore.Models
{
    public class BaseRazorUidEntity : BaseRazorEntity
    {
        #region Public and private fields and properties

        [Parameter] public Guid Uid { get; set; }
        public IBaseUidEntity UidItem { get => (BaseUidEntity)Item; set => SetItem(value); }

        #endregion

        #region Constructor and destructor

        public BaseRazorUidEntity() { }

        #endregion

        #region Public and private methods

        public void SetItem(IBaseUidEntity item)
        {
            SetItem((IBaseEntity)item);
        }

        public void SetParentItem(IBaseUidEntity parentItem)
        {
            SetParentItem((IBaseEntity)parentItem);
        }

        public async Task ItemSelectAsync(IBaseUidEntity item)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            RunTasks($"{LocalizationStrings.DeviceControl.Method} {nameof(ItemSelectAsync)}", "", LocalizationStrings.Share.DialogResultFail, "",
                new List<Task> {
                    new(async() => {
                        UidItem = item;
                        // Debug log.
                        //if (AppSettings.IsDebug)
                        //{
                        //    Console.WriteLine("--------------------------------------------------------------------------------");
                        //    Console.WriteLine($"---------- {nameof(BaseRazorUidEntity)}.{nameof(ItemSelectAsync)} (for Debug mode) ---------- ");
                        //    Console.WriteLine($"Item: {Item}");
                        //    Console.WriteLine("--------------------------------------------------------------------------------");
                        //}
                        await GuiRefreshWithWaitAsync();
                    }),
                }, true);
        }

        #endregion
    }
}
