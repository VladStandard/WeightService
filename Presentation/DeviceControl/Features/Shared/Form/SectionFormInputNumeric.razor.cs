
using System.Numerics;
using Microsoft.AspNetCore.Components;

namespace DeviceControl.Features.Shared.Form;

public sealed partial class SectionFormInputNumeric<TValue> : SectionFormInputBase 
    where TValue: IMinMaxValue<TValue>, IConvertible, IComparable<TValue>, IEquatable<TValue>
{
    [Parameter] public TValue Value { get; set; } = default!;
    [Parameter] public EventCallback<TValue> ValueChanged { get; set; }
    [Parameter] public TValue MaxValue { get; set; } = TValue.MaxValue;
    [Parameter] public TValue MinValue { get; set; } = TValue.MinValue;
    [Parameter] public bool IsDisabled { get; set; }
    
    protected override Task OnInitializedAsync()
    {
        InitializeMinMaxValue();
        return Task.CompletedTask;
    }

    private void InitializeMinMaxValue()
    {
        if (Comparer<TValue>.Default.Compare(MaxValue, TValue.MaxValue) > 0) MaxValue = TValue.MaxValue;
        if (Comparer<TValue>.Default.Compare(MinValue, TValue.MinValue) < 0) MinValue = TValue.MinValue;
    }

    private void HandleInputChange(string arg)
    {
        if (!TryParse(arg, out TValue newValue)) return;
        if (EqualityComparer<TValue>.Default.Equals(Value, newValue)) return;
        Value = newValue;
    }
    
    private bool TryParse(string value, out TValue result)
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
    
    private TValue GetLimitedValue(TValue value) => 
        Comparer<TValue>.Default.Compare(value, MinValue) < 0 ? MinValue :
        Comparer<TValue>.Default.Compare(value, MaxValue) > 0 ? MaxValue :
        value;
}