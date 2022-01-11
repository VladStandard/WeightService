// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Models;
using DataShareCore;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Toolbelt.Blazor.HotKeys;

namespace BlazorDeviceControl.Shared
{
    public partial class MainLayout
    {
        #region Public and private fields and properties

        //[Inject] public AuthenticationStateProvider AuthenticationState { get; private set; }
        [Inject] public HotKeys HotKeysItem { get; private set; }
        [Inject] public JsonSettingsEntity JsonAppSettings { get; private set; }
        //[CascadingParameter] private Task<AuthenticationState> authenticationStateTask { get; set; }
        //private string _authMessage;

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            //AuthenticationState authState = await authenticationStateTask;
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(() =>
                {
                    lock (Locker)
                    {
                        AppSettings.SetupMemory(GuiRefreshAsync);
                        AppSettings.SetupJsonSettings(JsonAppSettings);
                        AppSettings.SetupHotKeys(HotKeysItem);
                        if (AppSettings.HotKeysItem != null)
                            AppSettings.HotKeysContextItem = AppSettings.HotKeysItem.CreateContext()
                                .Add(ModKeys.Alt, Keys.Num1, HotKeysMenuRoot, "Menu root");

                        //AppSettings.SetupIdentity(AuthenticationState);
                        //AppSettings.SetupUserAccessLevel();

                        //System.Security.Claims.ClaimsPrincipal user = authState.User;
                        //if (user.Identity.IsAuthenticated)
                        //{
                        //    _authMessage = $"{user.Identity.Name} is authenticated.";
                        //}
                        //else
                        //{
                        //    _authMessage = "The user is NOT authenticated.";
                        //}

                        AppSettings.SetupUserAccessLevelForce();
                    }
                }), true);
        }

        #endregion
    }
}
