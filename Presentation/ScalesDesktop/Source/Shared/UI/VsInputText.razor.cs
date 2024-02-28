using Microsoft.AspNetCore.Components;
using ScalesDesktop.Source.Shared.Utils;

namespace ScalesDesktop.Source.Shared.UI;

public sealed partial class VsInputText : ComponentBase
{
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> Attributes { get; set; } = new();

    [Parameter] public string Value { get; set; } = string.Empty;
    [Parameter] public EventCallback<string> ValueChanged { get; set; }

    private async Task OnValueChanged() => await ValueChanged.InvokeAsync(Value);

    private string AdditionalClass => DictionaryUtils.TryGetValue(Attributes, "class");

    private string InputClass => $"flex h-10 w-full rounded-md border border-input bg-background px-3 py-2 text-sm" +
                                 $" ring-offset-background file:border-0 file:bg-transparent file:text-sm" +
                                 $" file:font-medium placeholder:text-muted-foreground focus-visible:outline-none" +
                                 $" focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2" +
                                 $" disabled:cursor-not-allowed disabled:opacity-50 {AdditionalClass}";
}