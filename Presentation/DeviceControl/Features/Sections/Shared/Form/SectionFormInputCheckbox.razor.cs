using Microsoft.AspNetCore.Components;

namespace DeviceControl.Features.Sections.Shared.Form;

public sealed partial class SectionFormInputCheckbox: SectionFormInputBase
{
    [Parameter] public bool Value { get; set; }
    [Parameter] public bool IsDisabled { get; set; }
    [Parameter] public EventCallback<bool> ValueChanged { get; set; }
    
    private async Task HandleValueChange(ChangeEventArgs e) => await ValueChanged.InvokeAsync(Value);
}