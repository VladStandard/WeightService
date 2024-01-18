using CommunityToolkit.Mvvm.Messaging;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Resources;
using ScalesHybrid.Services;
using Ws.Scales.Enums;
using Ws.Scales.Events;

namespace ScalesHybrid.Features.Labels.Modules;

public sealed partial class LabelDisplay: ComponentBase, IDisposable
{
    [Inject] private LineContext LineContext { get; set; } = null!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
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