using Microsoft.AspNetCore.Components;
using ScalesHybrid.Utils;

namespace ScalesHybrid.Features.Shared;


public sealed partial class SharedLabel : ComponentBase
{
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> Attributes { get; set; } = new();

    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string Href { get; set; } = string.Empty;
    
    private string AdditionalClass => DictionaryUtils.TryGetValue(Attributes, "class");

    private string LabelClass => $"text-sm font-medium leading-none peer-disabled:cursor-not-allowed " +
                                 $"peer-disabled:opacity-70 {AdditionalClass}";
}