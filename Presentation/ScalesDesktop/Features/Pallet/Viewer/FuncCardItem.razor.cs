using Microsoft.AspNetCore.Components;

namespace ScalesDesktop.Features.Pallet.Viewer;

public sealed partial class FuncCardItem: ComponentBase
{
    [Parameter] public string IconName { get; set; } = string.Empty;
    [Parameter] public RenderFragment? CardTitle { get; set; }
    [Parameter] public RenderFragment? CardDescription { get; set; }
}