using Microsoft.AspNetCore.Components;

namespace DeviceControl2.Source.Shared.UI.Form;

public abstract class BasicInput<TValue>: ComponentBase
{
    [Parameter] public TValue? Value { get; set; }
    [Parameter] public EventCallback<TValue> ValueChanged { get; set; }
    [Parameter] public string Class { get; set; } = string.Empty;
    [Parameter] public bool Disabled { get; set; }
    [Parameter] public bool AutoFocus { get; set; }
    [Parameter] public string Placeholder { get; set; } = string.Empty;
    [Parameter] public bool ReadOnly { get; set; }
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object> AdditionalAttributes { get; set; } = new();
    
    protected async Task OnValueChanged() => await ValueChanged.InvokeAsync(Value);
}