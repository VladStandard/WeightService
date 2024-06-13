using Microsoft.AspNetCore.Components;
using Ws.Components.Source.Utils;

namespace Ws.Components.Source.UI;

public sealed partial class Badge : ComponentBase
{
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> Attributes { get; set; } = new();
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string Class { get; set; } = string.Empty;
    [Parameter] public BadgeVariant Variant { get; set; } = BadgeVariant.Default;

    private string BadgeClasses => Css.Class("inline-flex items-center rounded-full border px-2.5 py-0.5",
                                             "text-xs font-semibold transition-colors focus:outline-none focus:ring-2",
                                             "focus:ring-ring focus:ring-offset-2", VariantClasses, Class);

    private string VariantClasses => Variant switch
    {
        BadgeVariant.Default => "border-transparent bg-primary text-primary-foreground hover:bg-primary/80",
        BadgeVariant.Secondary => "border-transparent bg-secondary text-secondary-foreground hover:bg-secondary/80",
        BadgeVariant.Destructive => "border-transparent bg-destructive text-destructive-foreground hover:bg-destructive/80",
        BadgeVariant.Outline => "text-foreground",
        _ => string.Empty
    };
}

public enum BadgeVariant
{
    Default,
    Destructive,
    Outline,
    Secondary
}