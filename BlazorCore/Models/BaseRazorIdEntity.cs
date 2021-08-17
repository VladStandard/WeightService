using BlazorCore.DAL;
using BlazorCore.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace BlazorCore.Models
{
    public class BaseRazorIdEntity : BaseRazorEntity
    {
        #region Public and private fields and properties

        public BaseIdEntity IdItem { get; set; }

        #endregion

        #region Constructor and destructor

        public BaseRazorIdEntity() { }

        #endregion

        #region Public and private methods

        public async Task ItemSelectAsync(BaseIdEntity item)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            RunTasks(LocalizationStrings.DeviceControl.MethodItemSelectAsync, "", LocalizationStrings.Share.DialogResultFail, "",
                new List<Task> {
                    new(async() => {
                        IdItem = item;
                        // Debug log.
                        if (AppSettings.IsDebug)
                        {
                            Console.WriteLine("--------------------------------------------------------------------------------");
                            Console.WriteLine($"---------- {nameof(BaseRazorIdEntity)}.{nameof(ItemSelectAsync)} (for Debug mode) ---------- ");
                            Console.WriteLine($"Item: {IdItem}");
                            Console.WriteLine("--------------------------------------------------------------------------------");
                        }
                        await GuiRefreshAsync(false).ConfigureAwait(false);
                    }),
                }, true);
        }

        #endregion
    }
}