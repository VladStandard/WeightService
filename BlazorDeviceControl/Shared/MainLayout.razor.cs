// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Toolbelt.Blazor.HotKeys;

namespace BlazorDeviceControl.Shared
{
    public partial class MainLayout
    {
        #region Public and private fields and properties

        [Inject] public HotKeys HotKeysItem { get; private set; }
        private readonly object _locker = new();

        #endregion

        #region Public and private methods

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);

            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(() =>
                {
                    lock (_locker)
                    {
                        //AppSettings.SetupJsonSettings(JsonSettings);
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
                    lock (_locker)
                    {
                        AppSettings.SetupMemory(GuiRefreshAsync);
                    }
                }), true);
        }

        #endregion
    }
}
