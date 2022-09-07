// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Components;

namespace BlazorCore.Models;

public partial class RazorPageBase : LayoutComponentBase
{
	#region Public and private methods

	/// <summary>
	/// Write code for extension.
	/// </summary>
	protected override void OnInitialized()
    {
        base.OnInitialized();
        // Write code for extension.
    }

	/// <summary>
	/// Write code for extension.
	/// </summary>
	protected override void OnParametersSet()
    {
        base.OnParametersSet();
        // Write code for extension.
	}

	#endregion
}
