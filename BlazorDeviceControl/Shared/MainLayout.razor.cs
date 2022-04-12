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

        #endregion

        #region Constructor and destructor

        public MainLayout() : base()
        {
            //
        }

        #endregion

        #region Public and private methods

        private async void MemoryClearAsync(Radzen.MenuItemEventArgs args)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            GC.Collect();
        }

        private void Default()
        {
            IsLoaded = false;
            Table = new TableSystemEntity(ProjectsEnums.TableSystem.Default);
            Items = new();
            ButtonSettings = new();
            UserSettings.SetupHotKeys(HotKeysItem);
            if (UserSettings.HotKeys != null)
                UserSettings.HotKeysContext = UserSettings.HotKeys.CreateContext()
                    .Add(ModKeys.Alt, Keys.Num1, HotKeysMenuRoot, "Menu root");
            UserSettings.SetupAccessRights(AppSettings.DataAccess);
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async () =>
                {
                    Default();
                    IsLoaded = true;
                    await GuiRefreshWithWaitAsync();
                }), true);

            // Don't change it, because GuiRefreshAsync can get exception!
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async () =>
                {
                    AppSettings.SetupMemory();
                    //await AppSettings.Memory.OpenAsync(GuiRefreshAsync).ConfigureAwait(false);
                    await AppSettings.Memory.OpenAsync().ConfigureAwait(false);
                }), true);
        }

        #endregion
    }
}
