using CommunityToolkit.Mvvm.Messaging;
using Microsoft.AspNetCore.Components;
using ScalesDesktop.Source.Shared.Models.Enums;
using Ws.Printers.Enums;
using Ws.Printers.Events;

namespace ScalesDesktop.Source.Features.StatusIcons;

public sealed partial class PrinterStatusIcon : ComponentBase, IDisposable
{
    private DeviceStatusEnum DeviceStatus { get; set; } = DeviceStatusEnum.IsDisabled;

    protected override void OnInitialized() => WeakReferenceMessenger.Default.Register<GetPrinterStatusEvent>(this, GetStatus);

    private void GetStatus(object recipient, GetPrinterStatusEvent message)
    {
        DeviceStatus = message.Status switch
        {
            PrinterStatusEnum.IsDisabled => DeviceStatusEnum.IsDisabled,
            PrinterStatusEnum.IsForceDisconnected => DeviceStatusEnum.IsForceDisconnected,
            PrinterStatusEnum.Ready => DeviceStatusEnum.Connected,
            _ => DeviceStatusEnum.Warning
        };
        StateHasChanged();
    }

    private string GetPrinterStatusStyle() => DeviceStatus switch
    {
        DeviceStatusEnum.IsDisabled => "bg-gray-100 border-gray-300 text-gray-600",
        DeviceStatusEnum.IsForceDisconnected => "bg-red-100 border-red-500 text-red-500",
        DeviceStatusEnum.Warning => "bg-amber-100 border-amber-500 text-amber-500",
        _ => "bg-green-100 border-green-500 text-green-500"
    };

    public void Dispose() => WeakReferenceMessenger.Default.Unregister<GetPrinterStatusEvent>(this);
}