using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.FluentUI.AspNetCore.Components;
using ScalesDesktop.Source.Features;
using ScalesDesktop.Source.Shared.Localization;
using ScalesDesktop.Source.Shared.Services;
using Ws.Shared.Resources;

namespace ScalesDesktop.Source.Widgets.LabelDisplay;

public sealed partial class LabelDisplayKneading : ComponentBase, IDisposable
{
    # region Injects

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IDialogService DialogService { get; set; } = default!;
    [Inject] private LabelContext LabelContext { get; set; } = default!;

    # endregion

    protected override void OnInitialized() => LabelContext.OnStateChanged += StateHasChanged;

    private async Task ShowNumericKeyboard()
    {
        NumericKeyboardDialogContent data = new() { Kneading = LabelContext.KneadingModel.KneadingCount };
        IDialogReference dialog = await DialogService.ShowDialogAsync<NumericKeyboardDialog>(data, new());
        DialogResult result = await dialog.Result;
        if (result is { Cancelled: false, Data: int newKneading })
        {
            LabelContext.KneadingModel.KneadingCount = newKneading;
            StateHasChanged();
        }
    }

    public void Dispose() => LabelContext.OnStateChanged -= StateHasChanged;
}