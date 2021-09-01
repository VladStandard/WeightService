// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore;
using BlazorCore.DAL;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataCore.Models
{
    public class BaseRazorIdEntity : BaseRazorEntity
    {
        #region Public and private fields and properties

        [Parameter] public int Id { get; set; }
        public IBaseIdEntity IdItem { get => (BaseIdEntity)Item; set => SetItem(value); }

        #endregion

        #region Constructor and destructor

        public BaseRazorIdEntity() { }

        #endregion

        #region Public and private methods

        public void SetItem(IBaseIdEntity item)
        {
            SetItem((IBaseEntity)item);
        }

        public void SetParentItem(IBaseIdEntity parentItem)
        {
            SetParentItem((IBaseEntity)parentItem);
        }

        public async Task ItemSelectAsync(IBaseIdEntity item)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(ItemSelectAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new List<Task> {
                    new(async() => {
                        IdItem = item;
                        // Debug log.
                        //if (AppSettings.IsDebug)
                        //{
                        //    Console.WriteLine("--------------------------------------------------------------------------------");
                        //    Console.WriteLine($"---------- {nameof(BaseRazorIdEntity)}.{nameof(ItemSelectAsync)} (for Debug mode) ---------- ");
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
