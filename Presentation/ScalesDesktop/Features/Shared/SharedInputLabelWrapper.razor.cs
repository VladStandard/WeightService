using Microsoft.AspNetCore.Components;

namespace ScalesDesktop.Features.Shared;

public sealed partial class SharedInputLabelWrapper: ComponentBase
{
    [Parameter, EditorRequired] public string Label { get; set; } = string.Empty;
    [Parameter, EditorRequired] public string HtmlFor { get; set; } = string.Empty;
    [Parameter] public RenderFragment? ChildContent { get; set; }
}