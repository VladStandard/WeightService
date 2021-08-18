// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Models;
using BlazorCore.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using Toolbelt.Blazor.HotKeys;

namespace BlazorDeviceControl.Shared
{
    public partial class MainLayout
    {
        #region Public and private fields and properties - Inject

        [Inject] public AuthenticationStateProvider AuthenticationState { get; private set; }
        [Inject] public JsonAppSettingsEntity JsonAppSettings { get; private set; }
        [Inject] public HotKeys HotKeysItem { get; private set; }

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationStrings.DeviceControl.Method} {nameof(SetParametersAsync)}", "", LocalizationStrings.Share.DialogResultFail, "",
                new List<Task> {
                    //new Task(delegate {
                    new(() => {
                        AppSettings.Setup(AuthenticationState, JsonAppSettings, HotKeysItem);
                        if (AppSettings.HotKeysItem != null)
                            AppSettings.HotKeysContextItem = AppSettings.HotKeysItem.CreateContext()
                                .Add(ModKeys.Alt, Keys.Num1, HotKeysMenuRoot, "Menu root");
                    }),
                }, true);
            RunTasks($"{LocalizationStrings.DeviceControl.Method} {nameof(SetParametersAsync)}", "", LocalizationStrings.Share.DialogResultFail, "",
                new List<Task> {
                    new(() => {
                        AppSettings.MemoryOpen(GuiRefreshAsync);
                    }),
                }, true);
        }

        #endregion
    }
}