// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Models;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Toolbelt.Blazor.HotKeys;

namespace BlazorDeviceControl.Shared
{
    public partial class MainLayout
    {
        #region Public and private fields and properties

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
                        AppSettings.SetupJsonSettings(JsonAppSettings);
                        UserSettings.SetupHotKeys(HotKeysItem);
                        if (UserSettings.HotKeys != null)
                            UserSettings.HotKeysContext = UserSettings.HotKeys.CreateContext()
                                .Add(ModKeys.Alt, Keys.Num1, HotKeysMenuRoot, "Menu root");
                        //UserSettings.SetupIdentity();
                        UserSettings.SetupUserAccessLevel(AppSettings.DataAccess);
                    }
                }), true);

            // Don't change it, because GuiRefreshAsync can get exception!
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(() =>
                {
                    AppSettings.SetupMemory(GuiRefreshAsync);
                }), true);
        }

        #endregion
    }
}
