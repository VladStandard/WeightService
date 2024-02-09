using CommunityToolkit.Mvvm.Messaging;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesDesktop.Resources;
using ScalesDesktop.Services;
using Ws.Scales.Enums;
using Ws.Scales.Events;

namespace ScalesDesktop.Features.Labels.Modules;

public sealed partial class ScaleCalibrationButton : ComponentBase, IDisposable
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private ExternalDevicesService ExternalDevices { get; set; } = null!;

    private bool IsScalesTerminalWasOpened { get; set; }
    private bool IsScalesAvailable { get; set; } = true;
    private int SecToOpen { get; set; } = 0;

    protected override void OnInitialized()
    {
        ScalesSubscribe();
    }

    private string GetCooldownString() =>
        $"{Localizer["ButtonCooldown"]} {SecToOpen} {Localizer["TimeMeasureSecond"]}";

    private async Task HandleButtonOpenScalesTerminal()
    {
        if (IsScalesTerminalWasOpened) return;

        IsScalesTerminalWasOpened = true;
        SecToOpen = 10;
        StateHasChanged();
        ExternalDevices.Scales.Calibrate();
        while (SecToOpen > 0)
        {
            await Task.Delay(1000);
            SecToOpen--;
            StateHasChanged();
        }
        IsScalesTerminalWasOpened = false;
        StateHasChanged();
    }

    private void UpdateScalesStatus(object recipient, GetScaleStatusEvent message)
    {
        IsScalesAvailable = message.Status is ScalesStatus.IsConnect;
        InvokeAsync(StateHasChanged);
    }

    private void ScalesSubscribe() =>
        WeakReferenceMessenger.Default.Register<GetScaleStatusEvent>(this, UpdateScalesStatus);

    private void ScalesUnsubscribe() =>
        WeakReferenceMessenger.Default.Unregister<GetScaleStatusEvent>(this);

    public void Dispose() => ScalesUnsubscribe();
}