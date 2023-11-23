using Microsoft.AspNetCore.Components;

namespace ScalesHybrid.Components;

public sealed partial class PluConfigDisplay : ComponentBase
{
    private List<PluConfigItem> ConfigItems { get; set; } = new();

    protected override void OnInitialized()
    {
        ConfigItems = new()
        {
            new() { Title = "Линия", SelectedItemName = "Линия не выбрана" },
            new() { Title = "Плу", SelectedItemName = "ПЛУ не выбрана" },
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