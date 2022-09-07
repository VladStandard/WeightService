// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors;

public partial class MainLayout : RazorPageBase
{
	#region Public and private methods
    
	private void MemoryClear()
    {
        GC.Collect();
    }

	protected override void OnInitialized()
	{
		base.OnInitialized();

		RunActionsInitialized(new()
		{
			() =>
			{
				Table = new TableSystemModel(SqlTableSystemEnum.Default);
				Items = new();
			}
		});
	}

	protected override void OnParametersSet()
	{
		base.OnParametersSet();

		RunActionsParametersSet(new()
		{
			() =>
			{
				AppSettings.SetupMemory();
				AppSettings.Memory.OpenAsync().ConfigureAwait(false);
			}
		});
	}

	#endregion
}
