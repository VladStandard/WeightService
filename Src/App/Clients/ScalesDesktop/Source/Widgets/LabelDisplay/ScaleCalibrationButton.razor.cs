using CommunityToolkit.Mvvm.Messaging;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesDesktop.Source.Shared.Localization;
using ScalesDesktop.Source.Shared.Services;
using Ws.Scales.Enums;
using Ws.Scales.Messages;
using Ws.Shared.Resources;

namespace ScalesDesktop.Source.Widgets.LabelDisplay;

public sealed partial class ScaleCalibrationButton : ComponentBase, IRecipient<ScaleStatusMsg>
{
    #region Injects

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private ExternalDevicesService ExternalDevices { get; set; } = default!;

    # endregion

    private bool IsOnceClicked { get; set; }
    private bool IsScalesAvailable { get; set; } = true;
    private int SecToOpen { get; set; } = 0;

    private const int ButtonDebounceSeconds = 10;


    protected override void OnInitialized() => WeakReferenceMessenger.Default.Register(this);

    private string GetCooldownString() =>
        $"{Localizer["BtnCooldown"]} {SecToOpen} {WsDataLocalizer["MeasureSec"]}";

    private async Task HandleButtonOpenScalesTerminal()
    {
        if (IsOnceClicked) return;

        IsOnceClicked = true;
        SecToOpen = ButtonDebounceSeconds;
        StateHasChanged();
        ExternalDevices.Scales.Calibrate();
        while (SecToOpen > 0)
        {
            await Task.Delay(1000);
            SecToOpen--;
            StateHasChanged();
        }
        IsOnceClicked = false;
        StateHasChanged();
    }

    public void Receive(ScaleStatusMsg message)
    {
        IsScalesAvailable = message.Status is ScalesStatus.IsConnect;
        InvokeAsync(StateHasChanged);
    }
}