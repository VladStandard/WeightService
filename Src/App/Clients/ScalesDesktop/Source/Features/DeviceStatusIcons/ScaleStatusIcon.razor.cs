using CommunityToolkit.Mvvm.Messaging;
using Microsoft.AspNetCore.Components;
using ScalesDesktop.Source.Shared.Models.Enums;
using Ws.Scales.Enums;
using Ws.Scales.Messages;

namespace ScalesDesktop.Source.Features.DeviceStatusIcons;

public sealed partial class ScaleStatusIcon : ComponentBase, IRecipient<ScaleStatusMsg>
{
    private DeviceStatusEnum DeviceStatus { get; set; } = DeviceStatusEnum.IsDisabled;

    protected override void OnInitialized() => WeakReferenceMessenger.Default.Register(this);

    public void Receive(ScaleStatusMsg message)
    {
        DeviceStatus = message.Status switch
        {
            ScalesStatus.IsDisabled => DeviceStatusEnum.IsDisabled,
            ScalesStatus.IsForceDisconnected => DeviceStatusEnum.IsForceDisconnected,
            _ => DeviceStatusEnum.Connected
        };
        StateHasChanged();
    }

    private string GetScaleStatusStyle() => DeviceStatus switch
    {
        DeviceStatusEnum.IsDisabled => "bg-gray-100 border-gray-300 text-gray-600",
        DeviceStatusEnum.IsForceDisconnected => "bg-red-100 border-red-500 text-red-500",
        _ => "bg-green-100 border-green-500 text-green-500"
    };
}