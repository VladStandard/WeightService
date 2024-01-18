using Blazorise;
using Microsoft.AspNetCore.Components;
using ScalesHybrid.Features.Labels.Dialogs;
using ScalesHybrid.Services;

namespace ScalesHybrid.Features.Labels.Modules;

public sealed partial class LabelConfig : ComponentBase, IDisposable
{
    [Inject] private LineContext LineContext { get; set; } = null!;
    [Inject] private IModalService ModalService { get; set; } = null!;
    
    protected override void OnInitialized() => LineContext.OnStateChanged += StateHasChanged;

    private async Task ShowLineSelectDialog() => await ModalService.Show<LineSelect>();
    
    private async Task ShowPluSelectDialog() => await ModalService.Show<PluSelect>();
    
    private async Task ShowPluNestingSelectDialog() => await ModalService.Show<PluNestingSelect>();
    
    public void Dispose() => LineContext.OnStateChanged -= StateHasChanged;
}