using CommunityToolkit.Mvvm.Messaging;
using Microsoft.AspNetCore.Components;
using ScalesHybrid.Models.Enums;
using Ws.Printers.Enums;
using Ws.Printers.Events;
using Ws.Scales.Enums;
using Ws.Scales.Events;

namespace ScalesHybrid.Components;

public sealed partial class StatusScales: ComponentBase, IDisposable
{
    private DeviceStatusEnum DeviceStatus { get; set; } = DeviceStatusEnum.IsDisabled;
    
    protected override void OnInitialized()
    {
        WeakReferenceMessenger.Default.Register<GetScaleStatusEvent>(this, GetStatus);
    }

    private void GetStatus(object recipient, GetScaleStatusEvent message)
    {
        DeviceStatus = message.Status switch
        {
            ScalesStatus.IsDisabled => DeviceStatusEnum.IsDisabled,
            ScalesStatus.IsForceDisconnected => DeviceStatusEnum.IsForceDisconnected,
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
        WeakReferenceMessenger.Default.Unregister<GetScaleStatusEvent>(this);
    }
}