using Blazorise;
using Microsoft.AspNetCore.Components;
using ScalesDesktop.Features.Shared;
using ScalesDesktop.Services;

namespace ScalesDesktop.Features.Labels.Modules;

public sealed partial class LabelConfig : ComponentBase, IDisposable
{
    [Inject] private IModalService ModalService { get; set; } = null!;
    [Inject] private LabelContext LabelContext { get; set; } = null!;

    protected override void OnInitialized() => LabelContext.OnStateChanged += StateHasChanged;

    private async Task ShowPluSelectDialog() => await ModalService.Show<PluSelect>();

    public void Dispose() => LabelContext.OnStateChanged -= StateHasChanged;
}