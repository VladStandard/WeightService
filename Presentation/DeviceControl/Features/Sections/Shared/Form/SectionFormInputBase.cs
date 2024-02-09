using Microsoft.AspNetCore.Components;

namespace DeviceControl.Features.Sections.Shared.Form;

public class SectionFormInputBase : ComponentBase
{
    [Parameter] public string Label { get; set; } = string.Empty;
    [Parameter] public string Link { get; set; } = string.Empty;
}