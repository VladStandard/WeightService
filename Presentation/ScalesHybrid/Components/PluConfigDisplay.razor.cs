using Microsoft.AspNetCore.Components;
using Radzen;
using ScalesHybrid.Components.Dialogs;
using ScalesHybrid.Services;
using Ws.Services.Services.Line;
using Ws.StorageCore.Entities.SchemaScale.Scales;

namespace ScalesHybrid.Components;

public sealed partial class PluConfigDisplay : ComponentBase
{
    [Inject] private LineContext LineContext { get; set; }
    [Inject] private ILineService LineService { get; set; } 
    [Inject] private DialogService DialogService { get; set; }
    private List<PluConfigItem> ConfigItems { get; set; } = new();
    private IEnumerable<SqlLineEntity> LineEntities { get; set; }

    protected override void OnInitialized()
    {
        CreateButtons();
        LineEntities = LineService.GetLinesByWorkshop(LineContext.Line.WorkShop);
    }

    private void CreateButtons()
    {
        ConfigItems = new()
        {
            new() { Title = "Линия", SelectedItemName = LineContext.Line.DisplayName, SelectItemAction = ShowLineSelectDialog },
            new() { Title = "Плу", SelectedItemName = LineContext.KneadingModel.PluName },
            new() { Title = "Вложенность", SelectedItemName = "Вложенность не выбрана" },
        };
        StateHasChanged();
    }

    private void ChangeLine(SqlLineEntity sqlLineEntity)
    {
        LineContext.Line = sqlLineEntity;
        CreateButtons();
    }
    
    private async Task ShowLineSelectDialog() => 
        await DialogService.OpenAsync<DialogLineSelect>("Line Select",
            new() { { "LineEntities", LineEntities }, { "CallbackFunction", (Action<SqlLineEntity>)ChangeLine } },
            new() { Style = "min-height:auto;min-width:auto;width:auto" });
}

internal class PluConfigItem
{
    public string Title { get; set; } = string.Empty;
    public string SelectedItemName { get; set; } = string.Empty;
    public Func<Task> SelectItemAction { get; set; }
}