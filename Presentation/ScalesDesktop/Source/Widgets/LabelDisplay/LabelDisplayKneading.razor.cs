using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesDesktop.Source.Features;
using ScalesDesktop.Source.Shared.Localization;
using ScalesDesktop.Source.Shared.Services;
using Ws.SharedUI.Resources;

namespace ScalesDesktop.Source.Widgets.LabelDisplay;

public sealed partial class LabelDisplayKneading : ComponentBase, IDisposable
{
    [Inject] private IStringLocalizer<Resources> LabelsLocalizer { get; set; } = null!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = null!;
    [Inject] private IModalService ModalService { get; set; } = null!;

    [Inject] private LabelContext LabelContext { get; set; } = null!;

    protected override void OnInitialized() => LabelContext.OnStateChanged += StateHasChanged;

    private void SetNewKneading(int newKneading)
    {
        LabelContext.KneadingModel.KneadingCount = newKneading;
        StateHasChanged();
    }

    private async Task ShowNumericKeyboard() => await ModalService.Show<NumericKeyboardDialog>(p =>
        p.Add(x => x.CallbackFunction, SetNewKneading), new() { Size = ModalSize.Default });

    public void Dispose() => LabelContext.OnStateChanged -= StateHasChanged;
}