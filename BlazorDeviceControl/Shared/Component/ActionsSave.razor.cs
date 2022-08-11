// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Shared.Component;

/// <summary>
/// Actions save.
/// </summary>
public partial class ActionsSave
{
	#region Public and private methods

	protected override void OnParametersSet()
	{
		base.OnParametersSet();
		RunActions(new());
	}

	#endregion
}
