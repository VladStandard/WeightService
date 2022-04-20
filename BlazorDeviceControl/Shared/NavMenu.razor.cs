// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Localizations;
using DataCore.Models;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared
{
    public partial class NavMenu
    {
        #region Public and private fields and properties

        private bool CollapseNavMenu { get; set; } = true;
        [Parameter] public EventCallback<ParameterView> SetParameters { get; set; }
        private bool IsDebug { get; set; } =
#if DEBUG
    true;
#else
    false;
#endif

#endregion

#region Public and private methods

        private void ToggleNavMenu()
        {
            CollapseNavMenu = !CollapseNavMenu;
        }

#endregion

#region Constructor and destructor

        public NavMenu() : base()
        {
            //
        }

#endregion

#region Public and private methods

        private void Default()
        {
            IsLoaded = false;
            Table = new TableSystemEntity(ProjectsEnums.TableSystem.Default);
            Items = new();
            ButtonSettings = new();
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocaleCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocaleCore.Strings.DialogResultFail, "",
                new Task(async () =>
                {
                    Default();
                    IsLoaded = true;
                    await GuiRefreshWithWaitAsync();
                }), true);

        }

#endregion
    }
}
