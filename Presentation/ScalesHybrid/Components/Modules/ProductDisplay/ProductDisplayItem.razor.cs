using Microsoft.AspNetCore.Components;

namespace ScalesHybrid.Components.Modules.ProductDisplay;

public sealed partial class ProductDisplayItem: ComponentBase
{
    [Parameter] public RenderFragment ChildContent { get; set; }
    [Parameter] public string Label { get; set; } = string.Empty;
    [Parameter] public bool IsLongLabel { get; set; } = false;
}