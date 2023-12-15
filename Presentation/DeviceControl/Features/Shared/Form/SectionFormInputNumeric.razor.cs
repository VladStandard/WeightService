
using Microsoft.AspNetCore.Components;

namespace DeviceControl.Features.Shared.Form;

public sealed partial class SectionFormInputNumeric : SectionFormInputBase
{
    [Parameter] public int Value { get; set; }
    [Parameter] public int MaxValue { get; set; } = int.MaxValue;
    [Parameter] public int MinValue { get; set; } = int.MinValue;
    [Parameter] public bool IsDisabled { get; set; }

    protected override void OnInitialized()
    {
        LimitValue();
    }
    
    private void LimitValue()
    {
        Value = Math.Min(Value, MaxValue);
        Value = Math.Max(Value, MinValue);
    }

    private void OnInputLimitCheck(ChangeEventArgs e)
    {
        if (!(Value < MinValue || Value > MaxValue)) return;
        LimitValue();
    }
    
    private void IncreaseValue() => Value++;

    private void DecreaseValue() => Value--;
}