using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ScalesDesktop.Source.Shared.Utils;

namespace ScalesDesktop.Source.Shared.UI;

public sealed partial class VsInputDate<TValue> : ComponentBase
{
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> Attributes { get; set; } = new();
    [Parameter] public TValue Value { get; set; } = default!;
    [Parameter] public EventCallback<TValue> ValueChanged { get; set; }
    [Parameter] public InputDateType Type { get; set; } = InputDateType.Date;

    private string AdditionalClass => DictionaryUtils.TryGetValue(Attributes, "class");

    private async Task OnValueChanged() => await ValueChanged.InvokeAsync(Value);

    private string InputClass => $"flex h-10 w-full rounded-md border border-input bg-background px-3 py-2 text-sm" +
                                 $" ring-offset-background file:border-0 file:bg-transparent file:text-sm" +
                                 $" file:font-medium placeholder:text-muted-foreground focus-visible:outline-none" +
                                 $" focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2" +
                                 $" disabled:cursor-not-allowed disabled:opacity-50 {AdditionalClass}";
}