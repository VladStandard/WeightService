
using System.Numerics;
using Microsoft.AspNetCore.Components;

namespace DeviceControl.Features.Shared.Form;

public sealed partial class SectionFormInputNumeric<TValue> : SectionFormInputBase 
    where TValue: IMinMaxValue<TValue>, IConvertible, IComparable<TValue>, IEquatable<TValue>
{
    [Parameter] public TValue Value { get; set; } = default!;
    [Parameter] public EventCallback<TValue> ValueChanged { get; set; }
    [Parameter] public TValue MaxValue { get; set; } = TValue.MinValue;
    [Parameter] public TValue MinValue { get; set; } = TValue.MaxValue;
    [Parameter] public bool IsDisabled { get; set; }
    
    private string BindingValue { get; set; } = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        if (MaxValue.CompareTo(TValue.MaxValue) > 0) MaxValue = TValue.MaxValue;
        if (MinValue.CompareTo(TValue.MinValue) < 0) MinValue = TValue.MinValue;
        await HandleInputChange(Value.ToString()!);
    }

    private async Task HandleInputChange(string arg)
    {
        if (TryParse(arg, out TValue newValue))
        {
            Value = GetLimitedValue(newValue);
            BindingValue = Value.ToString()!;
            await ValueChanged.InvokeAsync(Value);
        }
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