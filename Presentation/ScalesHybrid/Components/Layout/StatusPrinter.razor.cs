using CommunityToolkit.Mvvm.Messaging;
using Microsoft.AspNetCore.Components;
using ScalesHybrid.Models.Enums;
using Ws.Printers.Enums;
using Ws.Printers.Events;

namespace ScalesHybrid.Components.Layout;

public sealed partial class StatusPrinter: ComponentBase, IDisposable
{
    private DeviceStatusEnum DeviceStatus { get; set; } = DeviceStatusEnum.IsDisabled;
    
    protected override void OnInitialized()
    {
        WeakReferenceMessenger.Default.Register<GetPrinterStatusEvent>(this, GetStatus);
    }

    private void GetStatus(object recipient, GetPrinterStatusEvent message)
    {
        DeviceStatus = message.Status switch
        {
            PrinterStatusEnum.IsDisabled => DeviceStatusEnum.IsDisabled,
            PrinterStatusEnum.IsForceDisconnected => DeviceStatusEnum.IsForceDisconnected,
            _ => DeviceStatusEnum.Connected
        };
        InvokeAsync(StateHasChanged);
    }

    private string GetStatusStyle() => DeviceStatus switch
    {
        DeviceStatusEnum.IsDisabled => "bg-gray-200",
        DeviceStatusEnum.IsForceDisconnected => "bg-red-200",
        _ => "bg-green-200"
    };

    public void Dispose()
    {
        WeakReferenceMessenger.Default.Unregister<GetPrinterStatusEvent>(this);
    }
}