using Microsoft.AspNetCore.Components;
using Radzen;
using ScalesHybrid.Components.Dialogs;
using ScalesHybrid.Services;
using Ws.Services.Services.Line;
using Ws.StorageCore.Entities.SchemaRef1c.Plus;
using Ws.StorageCore.Entities.SchemaScale.Scales;

namespace ScalesHybrid.Components;

public sealed partial class PluConfigDisplay : ComponentBase, IDisposable
{
    [Inject] private LineContext LineContext { get; set; }
    [Inject] private DialogService DialogService { get; set; }
    
    protected override void OnInitialized() => LineContext.OnStateChanged += StateHasChanged;
    
    
    private async Task ShowLineSelectDialog() => 
        await DialogService.OpenAsync<DialogLineSelect>("Выбор Линий", new(),
            new() { Style = "min-height:auto;min-width:auto;width:auto;max-width:70%;" });
    
    private async Task ShowPluSelectDialog() => 
        await DialogService.OpenAsync<DialogPluSelect>("Выбор ПЛУ", new(),
            new() { Style = "min-height:auto;min-width:auto;width:auto;max-width:70%;" });
    
    private async Task ShowPluNestingSelectDialog() => 
        await DialogService.OpenAsync<DialogPluNestingSelect>("Выбор вложенности", new(),
            new() { Style = "min-height:auto;min-width:auto;width:auto;max-width:70%;" });
    
    public void Dispose() => LineContext.OnStateChanged -= StateHasChanged;
}