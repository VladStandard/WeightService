using Microsoft.AspNetCore.Components;

namespace ScalesHybrid.Features.Pallet;

public sealed partial class PalletFormInputWrapper
{
    [Parameter, EditorRequired] public string Label { get; set; } = string.Empty;
    [Parameter, EditorRequired] public string HtmlFor { get; set; } = string.Empty;
    [Parameter] public RenderFragment? ChildContent { get; set; }
}