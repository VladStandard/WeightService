using Microsoft.AspNetCore.Components;

namespace ScalesHybrid.Components.PluDisplay;

public sealed partial class PluDisplayItem: ComponentBase
{
    [Parameter] public RenderFragment ChildContent { get; set; }
    [Parameter] public string Label { get; set; } = string.Empty;
}