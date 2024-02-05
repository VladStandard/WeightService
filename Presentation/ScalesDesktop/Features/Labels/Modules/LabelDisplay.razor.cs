using CommunityToolkit.Mvvm.Messaging;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesDesktop.Resources;
using ScalesDesktop.Services;
using Ws.Scales.Enums;
using Ws.Scales.Events;

namespace ScalesDesktop.Features.Labels.Modules;

public sealed partial class LabelDisplay: ComponentBase, IDisposable
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    [Inject] private LabelContext LabelContext { get; set; } = null!;
    
    private bool IsScalesDisconnected { get; set; }
    
    protected override void OnInitialized()
    {
        LabelContext.OnStateChanged += StateHasChanged;
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
        LabelContext.OnStateChanged -= StateHasChanged;
        MassaUnsubscribe();
    }
}