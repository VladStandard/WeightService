// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Shared
{
    public partial class MainLayout
    {
        #region Public and private fields and properties - Inject

        [Inject] public AuthenticationStateProvider AuthenticationState { get; private set; }
        [Inject] public JsonSettingsEntity JsonAppSettings { get; private set; }
        [Inject] public HotKeys HotKeysItem { get; private set; }

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new List<Task> {
                    //new Task(delegate {
                    new(() => {
                        AppSettings.Setup(AuthenticationState, JsonAppSettings, HotKeysItem);
                        if (AppSettings.HotKeysItem != null)
                            AppSettings.HotKeysContextItem = AppSettings.HotKeysItem.CreateContext()
                                .Add(ModKeys.Alt, Keys.Num1, HotKeysMenuRoot, "Menu root");
                    }),
                }, true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new List<Task> {
                    new(() => {
                        AppSettings.MemoryOpen(GuiRefreshAsync);
                    }),
                }, true);
        }

        #endregion
    }
}
