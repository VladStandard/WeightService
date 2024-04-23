using CommunityToolkit.Mvvm.Messaging;
using Microsoft.AspNetCore.Components;
using ScalesDesktop.Source.Shared.Models.Enums;
using Ws.Printers.Enums;
using Ws.Printers.Messages;

namespace ScalesDesktop.Source.Features.DeviceStatusIcons;

public sealed partial class PrinterStatusIcon : ComponentBase, IRecipient<PrinterStatusMsg>
{
    private DeviceStatusEnum DeviceStatus { get; set; } = DeviceStatusEnum.Connected;

    protected override void OnInitialized() => WeakReferenceMessenger.Default.Register(this);

    public void Receive(PrinterStatusMsg message)
    {
        DeviceStatus = message.Status switch
        {
            PrinterStatusEnum.IsDisabled => DeviceStatusEnum.IsDisabled,
            PrinterStatusEnum.IsForceDisconnected => DeviceStatusEnum.IsForceDisconnected,
            PrinterStatusEnum.Ready => DeviceStatusEnum.Connected,
            PrinterStatusEnum.Busy => DeviceStatusEnum.Connected,
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
}