// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Components;

namespace BlazorDeviceControl.Shared
{
    public partial class NavMenu
    {
        #region Public and private fields and properties

        private bool _collapseNavMenu = true;
        private string NavMenuCssClass => _collapseNavMenu ? "collapse" : string.Empty;
        [Parameter] public EventCallback<ParameterView> SetParameters { get; set; }

        #endregion

        #region Public and private methods

        private void ToggleNavMenu()
        {
            _collapseNavMenu = !_collapseNavMenu;
        }

        #endregion
    }
}
