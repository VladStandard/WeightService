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
        private bool IsBusy { get; set; }
        private bool IsComplete { get; set; }
        private readonly object _locker = new();

        #endregion

        #region Constructor and destructor

        public MainLayout()
        {
            IsBusy = false;
            IsComplete = false;
        }

        #endregion

        #region Public and private methods

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender).ConfigureAwait(true);
            if (!IsComplete)
                await SetParametersAsync(new()).ConfigureAwait(true);
            IsComplete = true;
        }

        private async void MemoryClearAsync(Radzen.MenuItemEventArgs args)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            GC.Collect();
        }

        //private async void SetParametersInvokeAsync(Radzen.MenuItemEventArgs args)
        //{
        //    await SetParametersAsync(new ParameterView()).ConfigureAwait(true);
        //}

        private void Default()
        {
            //lock (_locker)
            if (!IsBusy)
            {
                IsBusy = true;
                Table = new TableSystemEntity(ProjectsEnums.TableSystem.Default);
                Items = null;
                ButtonSettings = new();
                AppSettings.SetupMemory(GuiRefreshAsync);
                UserSettings.SetupHotKeys(HotKeysItem);
                if (UserSettings.HotKeys != null)
                    UserSettings.HotKeysContext = UserSettings.HotKeys.CreateContext()
                        .Add(ModKeys.Alt, Keys.Num1, HotKeysMenuRoot, "Menu root");
                UserSettings.SetupAccessRights(AppSettings.DataAccess);
                IsBusy = false;
            }
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async () =>
                {
                    Default();
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
