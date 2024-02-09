using Microsoft.AspNetCore.Components;

namespace ScalesDesktop.Features.Labels.Modules;

public sealed partial class LabelDisplayItem : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string Label { get; set; } = string.Empty;
    [Parameter] public bool IsLongLabel { get; set; }
}