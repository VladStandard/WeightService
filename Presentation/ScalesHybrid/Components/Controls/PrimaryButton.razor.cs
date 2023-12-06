using Microsoft.AspNetCore.Components;

namespace ScalesHybrid.Components.Controls;

public sealed partial class PrimaryButton : ComponentBase
{
    [Parameter] public EventCallback OnClickAction { get; set; }
    [Parameter] public string Title { get; set; } = string.Empty;
    [Parameter] public bool IsDisabled { get; set; }
}