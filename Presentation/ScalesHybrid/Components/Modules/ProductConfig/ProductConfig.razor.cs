using Blazorise;
using Microsoft.AspNetCore.Components;
using ScalesHybrid.Components.Dialogs;
using ScalesHybrid.Services;

namespace ScalesHybrid.Components.Modules.ProductConfig;

public sealed partial class ProductConfig : ComponentBase, IDisposable
{
    [Inject] private LineContext LineContext { get; set; }
    [Inject] private IModalService ModalService { get; set; }
    
    protected override void OnInitialized() => LineContext.OnStateChanged += StateHasChanged;

    private async Task ShowLineSelectDialog() => await ModalService.Show<DialogLineSelect>();
    
    private async Task ShowPluSelectDialog() => await ModalService.Show<DialogPluSelect>();
    
    private async Task ShowPluNestingSelectDialog() => await ModalService.Show<DialogNestingSelect>();
    
    public void Dispose() => LineContext.OnStateChanged -= StateHasChanged;
}