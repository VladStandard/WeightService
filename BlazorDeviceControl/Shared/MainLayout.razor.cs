// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using Toolbelt.Blazor.HotKeys;

namespace BlazorDeviceControl.Shared
{
    public partial class MainLayout
    {
        #region Public and private fields and properties

        [Inject] public HotKeys? HotKeysItem { get; private set; }
        [Parameter] public EventCallback<ParameterView> SetParameters { get; set; }
        private readonly object _locker = new();

        #endregion

        #region Public and private methods

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        private async void MemoryClearAsync(Radzen.MenuItemEventArgs args)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            GC.Collect();
        }

        private async void SetParametersInvokeAsync(Radzen.MenuItemEventArgs args)
        {
            await SetParametersAsync(new ParameterView()).ConfigureAwait(true);
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async () =>
                {
                    lock (_locker)
                    {
                        Table = new TableSystemEntity(ProjectsEnums.TableSystem.Default);
                        Items = null;
                        ButtonSettings = new();
                        AppSettings.SetupMemory(GuiRefreshAsync);
                        UserSettings.SetupHotKeys(HotKeysItem);
                        if (UserSettings.HotKeys != null)
                            UserSettings.HotKeysContext = UserSettings.HotKeys.CreateContext()
                                .Add(ModKeys.Alt, Keys.Num1, HotKeysMenuRoot, "Menu root");
                        UserSettings.SetupAccessRights(AppSettings.DataAccess);
                    }
                    await GuiRefreshWithWaitAsync();
                }), true);

            // Don't change it, because GuiRefreshAsync can get exception!
            //RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
            //    new Task(() =>
            //    {
            //        lock (_locker)
            //        {
            //            AppSettings.SetupMemory(GuiRefreshAsync);
            //        }
            //    }), true);
        }

        #endregion
    }
}
