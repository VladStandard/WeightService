// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
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
            //Default();
        }

        #endregion

        #region Public and private methods

        private void Default()
        {
            Table = new TableSystemEntity(ProjectsEnums.TableSystem.Default);
            Items = null;
            ButtonSettings = new();
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async () =>
                {
                    Default();
                    await GuiRefreshWithWaitAsync();
                }), true);

        }

        #endregion
    }
}
