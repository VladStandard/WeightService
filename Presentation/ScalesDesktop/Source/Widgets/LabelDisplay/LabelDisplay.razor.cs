using CommunityToolkit.Mvvm.Messaging;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesDesktop.Source.Shared.Localization;
using ScalesDesktop.Source.Shared.Services;
using Ws.Scales.Enums;
using Ws.Scales.Events;

namespace ScalesDesktop.Source.Widgets.LabelDisplay;

public sealed partial class LabelDisplay : ComponentBase, IDisposable
{
    # region Injects

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private LabelContext LabelContext { get; set; } = default!;

    # endregion

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