using Microsoft.AspNetCore.Components;

namespace ScalesDesktop.Source.Shared.UI;

public sealed partial class InputLabelWrapper: ComponentBase
{
    [Parameter, EditorRequired] public string Label { get; set; } = string.Empty;
    [Parameter, EditorRequired] public string HtmlFor { get; set; } = string.Empty;
    [Parameter] public RenderFragment? ChildContent { get; set; }
}