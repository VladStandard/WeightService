using CommunityToolkit.Mvvm.Messaging;
using Microsoft.AspNetCore.Components;
using ScalesDesktop.Source.Shared.Services;
using Ws.Scales.Enums;
using Ws.Scales.Messages;

namespace ScalesDesktop.Source.Widgets.LabelDisplay;

public sealed partial class LabelDisplay : ComponentBase, IRecipient<ScaleStatusMsg>, IDisposable
{
    # region Injects

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private LabelContext LabelContext { get; set; } = default!;

    # endregion

    private bool IsScalesDisconnected { get; set; }

    protected override void OnInitialized()
    {
        LabelContext.OnStateChanged += StateHasChanged;
        WeakReferenceMessenger.Default.Register(this);
    }

    public void Dispose()
    {
        LabelContext.OnStateChanged -= StateHasChanged;
    }

    public void Receive(ScaleStatusMsg message)
    {
        IsScalesDisconnected = message.Status == ScalesStatus.IsForceDisconnected;
        InvokeAsync(StateHasChanged);
    }
}