using Microsoft.AspNetCore.Components;

namespace ScalesDesktop.Features.Pallet.Create;

public sealed partial class CreateFormInputWrapper
{
    [Parameter, EditorRequired] public string Label { get; set; } = string.Empty;
    [Parameter, EditorRequired] public string HtmlFor { get; set; } = string.Empty;
    [Parameter] public RenderFragment? ChildContent { get; set; }
}