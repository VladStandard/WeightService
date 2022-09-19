// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors;

public partial class MainLayout : RazorComponentBase
{
	#region Public and private methods
    
	private void MemoryClear()
    {
        GC.Collect();
    }

	protected override void OnInitialized()
	{
		RunActionsInitialized(new()
		{
			() =>
			{
				if (HttpContextAccess?.HttpContext is not null)
                {
                    HttpContext = HttpContextAccess.HttpContext;
                }
            }
		});
	}

	protected override void OnParametersSet()
	{
		RunActionsParametersSet(new()
		{
			() =>
			{
				AppSettings.SetupMemory();
				AppSettings.Memory.OpenAsync().ConfigureAwait(false);
				//
			}
		});
	}

	#endregion
}
