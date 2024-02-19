using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesDesktop.Features.Labels.Modals;
using ScalesDesktop.Features.Labels.Resources;
using ScalesDesktop.Features.Shared;
using ScalesDesktop.Services;
using Ws.SharedUI.Resources;

namespace ScalesDesktop.Features.Labels.Modules;

public sealed partial class LabelConfig : ComponentBase, IDisposable
{
    [Inject] private IModalService ModalService { get; set; } = null!;
    [Inject] private LabelContext LabelContext { get; set; } = null!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = null!;
    [Inject] private IStringLocalizer<LabelsResources> LabelsLocalizer { get; set; } = null!;

    protected override void OnInitialized() => LabelContext.OnStateChanged += StateHasChanged;

    private async Task ShowPluSelectDialog() => await ModalService.Show<PluSelect>();

    public void Dispose() => LabelContext.OnStateChanged -= StateHasChanged;
}