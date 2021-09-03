// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore;
using DataShareCore.DAL.Interfaces;
using DataShareCore.DAL.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorProjectsCore.Models
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
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(ItemSelectAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
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
