// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorMdmControl.Shared;

public partial class NavMenu
{
	#region Public and private methods

	private bool _collapseNavMenu = true;
	private string NavMenuCssClass => _collapseNavMenu ? "collapse" : null;
	private void ToggleNavMenu()
	{
		_collapseNavMenu = !_collapseNavMenu;
	}

	#endregion
}