using Blazorise;
using Microsoft.AspNetCore.Components;
using ScalesHybrid.Components.Dialogs;
using ScalesHybrid.Services;
using Ws.Services.Services.Line;
using Ws.StorageCore.Entities.SchemaRef1c.Plus;
using Ws.StorageCore.Entities.SchemaScale.Scales;

namespace ScalesHybrid.Components;

public sealed partial class PluConfigDisplay : ComponentBase, IDisposable
{
    [Inject] private LineContext LineContext { get; set; }
    [Inject] private IModalService ModalService { get; set; }
    
    protected override void OnInitialized() => LineContext.OnStateChanged += StateHasChanged;

    private void ShowLineSelectDialog() => InvokeAsync(() => ModalService.Show<DialogLineSelect>());
    
    private void ShowPluSelectDialog() => InvokeAsync(() => ModalService.Show<DialogPluSelect>());
    
    private void ShowPluNestingSelectDialog() => InvokeAsync(() => ModalService.Show<DialogPluNestingSelect>());
    
    public void Dispose() => LineContext.OnStateChanged -= StateHasChanged;
}