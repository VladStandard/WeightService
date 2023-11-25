using Microsoft.AspNetCore.Components;
using ScalesHybrid.Services;
using Ws.StorageCore.Entities.SchemaScale.Scales;

namespace ScalesHybrid.Components;

public sealed partial class PluConfigDisplay : ComponentBase
{
    [Inject] private PluService PluService { get; set; }
    private List<PluConfigItem> ConfigItems { get; set; } = new();

    protected override void OnInitialized()
    {
        ConfigItems = new()
        {
            new() { Title = "Линия", SelectedItemName = PluService.Line.DisplayName },
            new() { Title = "Плу", SelectedItemName = PluService.KneadingModel.PluName },
            new() { Title = "Вложенность", SelectedItemName = "Вложенность не выбрана" },
        };
    }
}

internal class PluConfigItem
{
    public string Title { get; set; } = string.Empty;
    public string SelectedItemName { get; set; } = string.Empty;
    public Action SelectItemAction { get; set; }
}