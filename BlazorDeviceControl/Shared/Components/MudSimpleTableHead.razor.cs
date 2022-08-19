// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Models;
using BlazorCore.Models.CssStyles;
using Microsoft.AspNetCore.Components;

namespace BlazorDeviceControl.Shared.Components;

public partial class MudSimpleTableHead : RazorBase
{
	#region Public and private fields, properties, constructor

	[Parameter] public TheadStyleModel? TheadStyle { get; set; }

	#endregion

	#region Public and private methods

	//

	#endregion
}
