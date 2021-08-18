using BlazorCore.DAL;
using BlazorCore.Utils;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorCore.Models
{
    public class BaseRazorIdEntity : BaseRazorEntity
    {
        #region Public and private fields and properties

        [Parameter] public BaseIdEntity Item { get; set; }

        #endregion

        #region Constructor and destructor

        public BaseRazorIdEntity() { }

        #endregion

        #region Public and private methods

        public async Task ItemSelectAsync(BaseIdEntity item)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            RunTasks($"{LocalizationStrings.DeviceControl.Method} {nameof(ItemSelectAsync)}", "", LocalizationStrings.Share.DialogResultFail, "",
                new List<Task> {
                    new(async() => {
                        Item = item;
                        // Debug log.
                        //if (AppSettings.IsDebug)
                        //{
                        //    Console.WriteLine("--------------------------------------------------------------------------------");
                        //    Console.WriteLine($"---------- {nameof(BaseRazorIdEntity)}.{nameof(ItemSelectAsync)} (for Debug mode) ---------- ");
                        //    Console.WriteLine($"Item: {Item}");
                        //    Console.WriteLine("--------------------------------------------------------------------------------");
                        //}
                        await GuiRefreshAsync(false).ConfigureAwait(false);
                    }),
                }, true);
        }

        #endregion
    }
}