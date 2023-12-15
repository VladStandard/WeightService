using Microsoft.AspNetCore.Components;

namespace DeviceControl.Features.Shared.Form;

public class SectionFormInputBase: ComponentBase
{
    [Parameter] public string Title { get; set; } = string.Empty;
    [Parameter] public bool IsFullWidth { get; set; }
    [Parameter] public bool IsTall { get; set; }
}