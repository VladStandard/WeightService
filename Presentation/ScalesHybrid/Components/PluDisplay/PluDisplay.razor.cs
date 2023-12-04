using CommunityToolkit.Mvvm.Messaging;
using Microsoft.AspNetCore.Components;
using ScalesHybrid.Services;
using Ws.Scales.Enums;
using Ws.Scales.Events;

namespace ScalesHybrid.Components.PluDisplay;

public sealed partial class PluDisplay: ComponentBase, IDisposable
{
    [Inject] private LineContext LineContext { get; set; }
    
    private bool IsScalesDisconnected { get; set; }
    
    protected override void OnInitialized()
    {
        LineContext.OnStateChanged += StateHasChanged;
        MassaSubscribe();
    }
    
    private void UpdateScalesStatus(object recipient, GetScaleStatusEvent message)
    {
        IsScalesDisconnected = message.Status == ScalesStatus.IsForceDisconnected;
        InvokeAsync(StateHasChanged);
    }

    private void MassaSubscribe() =>
        WeakReferenceMessenger.Default.Register<GetScaleStatusEvent>(this, UpdateScalesStatus);

    private void MassaUnsubscribe() =>
        WeakReferenceMessenger.Default.Unregister<GetScaleStatusEvent>(this);
    
    public void Dispose()
    {
        LineContext.OnStateChanged -= StateHasChanged;
        MassaUnsubscribe();
    }
}