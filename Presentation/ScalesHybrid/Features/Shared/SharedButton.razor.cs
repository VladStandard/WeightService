using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace ScalesHybrid.Features.Shared;

public sealed partial class SharedButton : ComponentBase
{
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> AdditionalAttributes { get; set; } = new();

    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public ButtonVariant Variant { get; set; } = ButtonVariant.Default;
    [Parameter] public ButtonSize Size { get; set; } = ButtonSize.Default;

    private string TryGetAdditionalClass() => AdditionalAttributes.TryGetValue("class", out object? classObj) ?
        classObj.ToString() ?? string.Empty : string.Empty;

    
    private string ButtonClasses => $"inline-flex items-center justify-center whitespace-nowrap rounded-md text-sm" +
                                    $" font-medium transition-colors focus-visible:outline-none focus-visible:ring-1" +
                                    $" focus-visible:ring-ring disabled:pointer-events-none disabled:opacity-50" +
                                    $" {VariantClasses} {SizeClasses} {TryGetAdditionalClass()}";

    private string VariantClasses => Variant switch
    {
        ButtonVariant.Default => "bg-neutral-900 text-neutral-100 shadow hover:bg-neutral-800",
        ButtonVariant.Destructive => "bg-destructive text-destructive-foreground shadow-sm hover:bg-destructive/90",
        ButtonVariant.Outline => "border border-neutral-200 bg-white shadow-sm hover:bg-neutral-100 hover:text-neutral-700",
        ButtonVariant.Secondary => "bg-neutral-100 text-neutral-700 shadow-sm hover:bg-neutral-50",
        ButtonVariant.Ghost => "hover:bg-accent hover:text-accent-foreground",
        ButtonVariant.Link => "text-primary underline-offset-4 hover:underline",
        _ => string.Empty
    };

    private string SizeClasses => Size switch
    {
        ButtonSize.Default => "h-9 px-4 py-2",
        ButtonSize.Small => "h-8 rounded-md px-3 text-xs",
        ButtonSize.Large => "h-10 rounded-md px-8",
        ButtonSize.Icon => "h-9 w-9",
        _ => string.Empty
    };
}

public enum ButtonVariant
{
    Default,
    Destructive,
    Outline,
    Secondary,
    Ghost,
    Link
}

public enum ButtonSize
{
    Default,
    Small,
    Large,
    Icon
}