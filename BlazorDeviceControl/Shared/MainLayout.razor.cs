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
        [Parameter] public EventCallback<ParameterView> SetParameters { get; set; }
        private readonly object _locker = new();

        #endregion

        #region Public and private methods

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        //public async Task SetParametersAsync1(Microsoft.AspNetCore.Components.Web.MouseEventArgs args)
        public async Task SetParametersAsync1()
        {
            await base.SetParametersAsync(new ParameterView()).ConfigureAwait(true);
        }
        
        private async void OnChildClicked(Radzen.MenuItemEventArgs args)
        {
            //console.Log($"{args.Text} from child clicked");
            await SetParametersAsync(new ParameterView()).ConfigureAwait(true);
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);

            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(() =>
                {
                    lock (_locker)
                    {
                        UserSettings.SetupHotKeys(HotKeysItem);
                        if (UserSettings.HotKeys != null)
                            UserSettings.HotKeysContext = UserSettings.HotKeys.CreateContext()
                                .Add(ModKeys.Alt, Keys.Num1, HotKeysMenuRoot, "Menu root");
                        UserSettings.SetupAccessRights(AppSettings.DataAccess);
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

        public void OnClick(ChangeEventArgs args)
        {

        }

        #endregion
    }
}
