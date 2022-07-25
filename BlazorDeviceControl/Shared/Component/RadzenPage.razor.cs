// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Radzen;

namespace BlazorDeviceControl.Shared.Component;

public partial class RadzenPage
{
    private IEnumerable<string>? ListComPorts { get; set; }

    private List<string> GetListComPorts()
    {
        List<string> result = new();
        for (int i = 1; i < 256; i++)
        {
            result.Add($"COM{i}");
        }
        return result;
    }

    public RadzenPage()
    {
        ListComPorts = GetListComPorts();
    }

    private void ShowNotification(NotificationMessage message)
    {
        NotificationService.Notify(message);
    }
}
