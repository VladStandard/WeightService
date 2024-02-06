using Microsoft.AspNetCore.Components;

namespace ScalesHybrid.Features.Pallet.Viewer;

public sealed partial class OverviewInputWrapper
{
    [Parameter, EditorRequired] public string Label { get; set; } = string.Empty;
    [Parameter, EditorRequired] public string HtmlFor { get; set; } = string.Empty;
    [Parameter] public RenderFragment? ChildContent { get; set; }
}