using Microsoft.AspNetCore.Components;
using ScalesDesktop.Source.Shared.Utils;

namespace ScalesDesktop.Source.Shared.UI;

public sealed partial class Label : ComponentBase
{
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> Attributes { get; set; } = new();

    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string Href { get; set; } = string.Empty;

    private string AdditionalClass => DictionaryUtils.TryGetValue(Attributes, "class");

    private string LabelClass => $"text-sm font-medium leading-none peer-disabled:cursor-not-allowed " +
                                 $"peer-disabled:opacity-70 {AdditionalClass}";
}