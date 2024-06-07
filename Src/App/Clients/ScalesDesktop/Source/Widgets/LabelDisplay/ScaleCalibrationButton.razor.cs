using Microsoft.AspNetCore.Components;
using ScalesDesktop.Source.Shared.Services;


namespace ScalesDesktop.Source.Widgets.LabelDisplay;

public sealed partial class ScaleCalibrationButton : ComponentBase, IDisposable
{
    #region Injects

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private ScalesService ScalesService { get; set; } = default!;

    # endregion

    private bool IsOnceClicked { get; set; }
    private int SecToOpen { get; set; } = 0;

    private const int ButtonDebounceSeconds = 10;

    protected override void OnInitialized() => ScalesService.StatusChanged += StateHasChanged;

    private string GetCooldownString() =>
        $"{Localizer["BtnCooldown"]} {SecToOpen} {WsDataLocalizer["MeasureSec"]}";

    private async Task HandleButtonOpenScalesTerminal()
    {
        if (IsOnceClicked) return;

        IsOnceClicked = true;
        SecToOpen = ButtonDebounceSeconds;
        StateHasChanged();
        ScalesService.Calibrate();
        while (SecToOpen > 0)
        {
            await Task.Delay(1000);
            SecToOpen--;
            StateHasChanged();
        }
        IsOnceClicked = false;
        StateHasChanged();
    }

    public void Dispose() => ScalesService.StatusChanged -= StateHasChanged;
}