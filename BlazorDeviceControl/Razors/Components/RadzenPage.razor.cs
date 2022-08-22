// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Components;

public partial class RadzenPage : RazorBase
{
    #region Public and private fields, properties, constructor

    private IEnumerable<string>? ListComPorts { get; set; }

	#endregion

	#region Public and private methods

	protected override void OnInitialized()
	{
		base.OnInitialized();

		ListComPorts = SerialPortsUtils.GetListComPorts(LocaleCore.Lang);
	}
    
    private void ShowNotification(NotificationMessage message)
    {
        NotificationService.Notify(message);
    }

    #endregion
}
