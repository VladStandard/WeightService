// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorShareCore.Models;
using DataShareCore;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;
using Toolbelt.Blazor.HotKeys;

namespace BlazorDeviceControl.Shared
{
    public partial class MainLayout
    {
        #region Public and private fields and properties

        [Inject] public AuthenticationStateProvider AuthenticationState { get; private set; }
        //[Inject] public AuthEntity Auth { get; private set; }
        [Inject] public JsonSettingsEntity JsonAppSettings { get; private set; }
        [Inject] public HotKeys HotKeysItem { get; private set; }
        private readonly object _locker = new();

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(() =>
                {
                    lock (_locker)
                    {
                        AppSettings.SetupMemory(GuiRefreshAsync);
                        AppSettings.SetupJsonSettings(JsonAppSettings);
                        AppSettings.SetupHotKeys(HotKeysItem);
                        if (AppSettings.HotKeysItem != null)
                            AppSettings.HotKeysContextItem = AppSettings.HotKeysItem.CreateContext()
                                .Add(ModKeys.Alt, Keys.Num1, HotKeysMenuRoot, "Menu root");
                        AppSettings.SetupIdentity(AuthenticationState);
                        //AppSettings.SetupIdentity(Auth.GetIdentity());
                        AppSettings.SetupUserAccessLevel();
                    }
                }), true);
            //RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
            //    new Task(() => {
            //    }), true);
        }

        #endregion
    }
}
