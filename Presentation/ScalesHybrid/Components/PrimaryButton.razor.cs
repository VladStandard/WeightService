using Microsoft.AspNetCore.Components;
using ScalesHybrid.Resources;

namespace ScalesHybrid.Components;

public sealed partial class PrimaryButton : ComponentBase
{
    [Parameter] public Action OnClickAction { get; set; }
    [Parameter] public string Title { get; set; } = string.Empty;
    [Parameter] public bool IsDisabled { get; set; }
}