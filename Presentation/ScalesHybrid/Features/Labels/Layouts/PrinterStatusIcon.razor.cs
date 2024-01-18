using CommunityToolkit.Mvvm.Messaging;
using Microsoft.AspNetCore.Components;
using ScalesHybrid.Models.Enums;
using Ws.Printers.Enums;
using Ws.Printers.Events;

namespace ScalesHybrid.Features.Labels.Layouts;

public sealed partial class PrinterStatusIcon: ComponentBase, IDisposable
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

    private string GetPrinterStatusStyle() => DeviceStatus switch
    {
        DeviceStatusEnum.IsDisabled => "bg-gray-300",
        DeviceStatusEnum.IsForceDisconnected => "bg-red-300 shadow-red-200",
        _ => "bg-green-300 shadow-green-200"
    };

    public void Dispose()
    {
        WeakReferenceMessenger.Default.Unregister<GetPrinterStatusEvent>(this);
    }
}