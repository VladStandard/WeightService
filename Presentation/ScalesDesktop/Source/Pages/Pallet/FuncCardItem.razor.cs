using Microsoft.AspNetCore.Components;

namespace ScalesDesktop.Source.Pages.Pallet;

public sealed partial class FuncCardItem: ComponentBase
{
    [Parameter] public string IconName { get; set; } = string.Empty;
    [Parameter] public RenderFragment? CardTitle { get; set; }
    [Parameter] public RenderFragment? CardDescription { get; set; }
}