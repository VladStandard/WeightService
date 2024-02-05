using System.Numerics;
using Microsoft.AspNetCore.Components;
using ScalesHybrid.Utils;

namespace ScalesHybrid.Features.Shared;

public sealed partial class SharedInputNumber<TValue> : ComponentBase where TValue: IMinMaxValue<TValue>, IConvertible, IComparable<TValue>, IEquatable<TValue>
{
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> Attributes { get; set; } = new();
    [Parameter] public TValue Value { get; set; } = default!;
    [Parameter] public EventCallback<TValue> ValueChanged { get; set; }
    
    private string AdditionalClass => DictionaryUtils.TryGetValue(Attributes, "class");

    private async Task OnValueChanged() => await ValueChanged.InvokeAsync(Value);

    private string InputClass => $"flex h-10 w-full rounded-md border border-neutral-200 bg-white px-3 py-2 text-sm" +
                                 $" ring-offset-white placeholder:text-neutral-500 focus-visible:outline-none" +
                                 $" focus-visible:ring-2 focus-visible:ring-neutral-400 focus-visible:ring-offset-2" +
                                 $" disabled:cursor-not-allowed focus-visible:border-neutral-200 disabled:opacity-50" +
                                 $" {AdditionalClass}";
}