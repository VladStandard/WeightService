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


    private void ShowLineSelectDialog() => InvokeAsync(() => ModalService.Show<DialogLineSelect>("Выбор Линий"));
    
    private void ShowPluSelectDialog() => InvokeAsync(() => ModalService.Show<DialogPluSelect>("Выбор ПЛУ"));
    
    private void ShowPluNestingSelectDialog() => InvokeAsync(() => ModalService.Show<DialogPluNestingSelect>("Выбор вложенности"));
    
    public void Dispose() => LineContext.OnStateChanged -= StateHasChanged;
}