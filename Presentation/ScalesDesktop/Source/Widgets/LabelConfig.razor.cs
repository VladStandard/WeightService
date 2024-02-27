using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesDesktop.Source.Shared.Localization;
using ScalesDesktop.Source.Shared.Services;
using Ws.SharedUI.Resources;

namespace ScalesDesktop.Source.Widgets;

public sealed partial class LabelConfig : ComponentBase, IDisposable
{
    [Inject] private IModalService ModalService { get; set; } = null!;
    [Inject] private LabelContext LabelContext { get; set; } = null!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = null!;
    [Inject] private IStringLocalizer<Resources> LabelsLocalizer { get; set; } = null!;

    protected override void OnInitialized() => LabelContext.OnStateChanged += StateHasChanged;

    private async Task ShowPluSelectDialog() => await ModalService.Show<PluSelect>();

    public void Dispose() => LabelContext.OnStateChanged -= StateHasChanged;
}