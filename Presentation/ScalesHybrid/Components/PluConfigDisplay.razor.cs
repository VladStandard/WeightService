using Microsoft.AspNetCore.Components;
using Radzen;
using ScalesHybrid.Components.Dialogs;
using ScalesHybrid.Services;
using Ws.Services.Services.Line;
using Ws.StorageCore.Entities.SchemaRef1c.Plus;
using Ws.StorageCore.Entities.SchemaScale.Scales;

namespace ScalesHybrid.Components;

public sealed partial class PluConfigDisplay : ComponentBase
{
    [Inject] private LineContext LineContext { get; set; }
    [Inject] private ILineService LineService { get; set; } 
    [Inject] private DialogService DialogService { get; set; }
    private IEnumerable<SqlLineEntity> LineEntities { get; set; }
    private IEnumerable<SqlPluEntity> PluEntities { get; set; }

    protected override void OnInitialized()
    {
        LineEntities = LineService.GetLinesByWorkshop(LineContext.Line.WorkShop);
        PluEntities = LineService.GetLinePlus(LineContext.Line);
    }

    private void ChangeLine(SqlLineEntity sqlLineEntity) => LineContext.Line = sqlLineEntity;

    private void ChangePlu(SqlPluEntity sqlPluEntity) => LineContext.Plu = sqlPluEntity;
    
    private async Task ShowPluSelectDialog() => 
        await DialogService.OpenAsync<DialogPluSelect>("Выбор ПЛУ",
            new() { { "PluEntities", PluEntities }, { "CallbackFunction", (Action<SqlPluEntity>)ChangePlu } },
            new() { Style = "min-height:auto;min-width:auto;width:auto;max-width:70%;" });
    
    
    private async Task ShowLineSelectDialog() => 
        await DialogService.OpenAsync<DialogLineSelect>("Выбор Линий",
            new() { { "LineEntities", LineEntities }, { "CallbackFunction", (Action<SqlLineEntity>)ChangeLine } },
            new() { Style = "min-height:auto;min-width:auto;width:auto;max-width:70%;" });
}