// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.CssStyles;

namespace BlazorDeviceControl.Razors.Items.Componets;

public partial class ItemTableBody : RazorPageBase
{
	#region Public and private fields, properties, constructor

	[Parameter] public TableBodyStyleModel? TableStyle { get; set; }

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		base.OnParametersSet();

		RunActionsParametersSet(new()
		{
			//
		});
	}

	#endregion
}
