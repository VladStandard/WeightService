using Blazor.Heroicons;
using Microsoft.AspNetCore.Components;

namespace ScalesHybrid.Features.Labels.Layouts;

public sealed partial class StatusIconTemplate: ComponentBase
{
    [Parameter] public string IconName { get; set; } = string.Empty;
    [Parameter] public HeroiconType IconType { get; set; } = HeroiconType.Solid;
    [Parameter] public string CustomClass { get; set; } = string.Empty;
    [Parameter] public RenderFragment? ChildContent { get; set; }
}