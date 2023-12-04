using Microsoft.AspNetCore.Components;

namespace ScalesHybrid.Components;

public sealed partial class KneadingDisplayWeightItem: ComponentBase
{
    [Parameter] public RenderFragment ChildContent { get; set; }
    [Parameter] public string Label { get; set; } = string.Empty;
}