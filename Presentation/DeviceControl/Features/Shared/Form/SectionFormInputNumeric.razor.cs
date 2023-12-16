
using Microsoft.AspNetCore.Components;

namespace DeviceControl.Features.Shared.Form;

public sealed partial class SectionFormInputNumeric : SectionFormInputBase
{
    [Parameter] public int Value { get; set; }
    [Parameter] public EventCallback<int> ValueChanged { get; set; }
    [Parameter] public int MaxValue { get; set; } = int.MaxValue;
    [Parameter] public int MinValue { get; set; } = int.MinValue;
    [Parameter] public bool IsDisabled { get; set; }
    
    private string BindingValue { get; set; } = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        await HandleInputChange(Value.ToString());
    }

    private async Task HandleInputChange(string arg)
    {
        int.TryParse(arg, out int newValue);
        Value = GetLimitedValue(newValue);
        BindingValue = Value.ToString();
        await ValueChanged.InvokeAsync(Value);
    }
    
    private int GetLimitedValue(int value) => Math.Min(Math.Max(value, MinValue), MaxValue);
}