using System.Numerics;
using Microsoft.AspNetCore.Components;

namespace DeviceControl.Features.Sections.Shared.Form;

public sealed partial class SectionFormInputNumeric<TValue> : SectionFormInputBase
    where TValue : IMinMaxValue<TValue>, IConvertible, IComparable<TValue>, IEquatable<TValue>
{
    [Parameter] public TValue Value { get; set; } = default!;
    [Parameter] public EventCallback<TValue> ValueChanged { get; set; }
    [Parameter] public TValue MaxValue { get; set; } = TValue.MaxValue;
    [Parameter] public TValue MinValue { get; set; } = TValue.MinValue;
    [Parameter] public bool IsDisabled { get; set; }

    private string BindingValue { get; set; } = string.Empty;

    protected override async Task OnParametersSetAsync()
    {
        await HandleInputChange(Value.ToString()!);
    }

    private async Task HandleInputChange(string arg)
    {
        if (!TryParse(arg, out TValue newValue)) return;
        newValue = GetLimitedValue(newValue);
        if (BindingValue.Equals(newValue.ToString())) return;
        await SetValue(newValue);
    }

    private async Task SetValue(TValue newValue)
    {
        BindingValue = newValue.ToString()!;
        Value = newValue;
        await ValueChanged.InvokeAsync(newValue);
    }

    private static bool TryParse(string value, out TValue result)
    {
        try
        {
            result = (TValue)Convert.ChangeType(value, typeof(TValue));
            return true;
        }
        catch
        {
            result = default!;
            return false;
        }
    }

    private static bool IsEqual(TValue oldValue, TValue newValue) =>
        EqualityComparer<TValue>.Default.Equals(oldValue, newValue);

    private TValue GetLimitedValue(TValue value) =>
        Comparer<TValue>.Default.Compare(value, MinValue) < 0 ? MinValue :
        Comparer<TValue>.Default.Compare(value, MaxValue) > 0 ? MaxValue :
        value;
}