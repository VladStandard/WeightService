// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors;

public partial class MainLayout : RazorComponentBase
{
	#region Public and private methods
    
	protected override void OnInitialized()
	{
        RunActionsInitialized(new()
		{
			SetUserSettings
		});
	}

	protected override void OnParametersSet()
	{
        RunActionsParametersSet(new()
		{
			() =>
			{
				BlazorAppSettings.SetupMemory();
				BlazorAppSettings.Memory.OpenAsync().ConfigureAwait(false);
				//
			}
		});
	}

	#endregion
}