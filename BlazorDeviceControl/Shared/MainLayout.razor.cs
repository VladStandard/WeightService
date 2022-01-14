// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Models;
using DataShareCore;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Threading.Tasks;
using Toolbelt.Blazor.HotKeys;

namespace BlazorDeviceControl.Shared
{
    public partial class MainLayout
    {
        #region Public and private fields and properties

        //[Inject] public AuthenticationStateProvider AuthenticationState { get; private set; }
        public AuthenticationState Authentication { get; private set; }
        [Inject] public HotKeys HotKeysItem { get; private set; }
        [Inject] public JsonSettingsEntity JsonAppSettings { get; private set; }

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(() =>
                {
                    lock (Locker)
                    {
                        _ = Task.Run(async () =>
                        {
                            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
                            AppSettings.SetupMemory(GuiRefreshAsync);
                        }).ConfigureAwait(false);
                        AppSettings.SetupJsonSettings(JsonAppSettings);
                        UserSettings.SetupHotKeys(HotKeysItem);
                        if (UserSettings.HotKeysItem != null)
                            UserSettings.HotKeysContextItem = UserSettings.HotKeysItem.CreateContext()
                                .Add(ModKeys.Alt, Keys.Num1, HotKeysMenuRoot, "Menu root");

                        UserSettings.SetupIdentity(Authentication);
                        UserSettings.SetupUserAccessLevel(AppSettings.DataAccess);
                    }
                }), true);
        }

        #endregion
    }
}
