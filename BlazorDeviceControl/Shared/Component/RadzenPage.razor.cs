// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Radzen;

namespace BlazorDeviceControl.Shared.Component;

public partial class RadzenPage
{
    #region Public and private fields, properties, constructor

    private IEnumerable<string>? ListComPorts { get; set; }

	#endregion

	#region Public and private methods

	protected override void OnInitialized()
	{
		base.OnInitialized();

		ListComPorts = GetListComPorts();
	}
    
	protected override void OnParametersSet()
    {
        base.OnParametersSet();
        SetParametersWithAction(new()
        {
            () =>
            {
				//
			}
        });
    }

    private List<string> GetListComPorts()
    {
        List<string> result = new();
        for (int i = 1; i < 256; i++)
        {
            result.Add($"COM{i}");
        }
        return result;
    }

    private void ShowNotification(NotificationMessage message)
    {
        NotificationService.Notify(message);
    }

    #endregion
}
