using Microsoft.AspNetCore.Components;

namespace ScalesHybrid.Components;

public sealed partial class PluConfigDisplayItem: ComponentBase
{
    [Parameter] public string Label { get; set; } = string.Empty;
    [Parameter] public string Value { get; set; } = string.Empty;
    [Parameter] public bool IsDisabled { get; set; }
    [Parameter] public Action OnClickAction { get; set; }
}