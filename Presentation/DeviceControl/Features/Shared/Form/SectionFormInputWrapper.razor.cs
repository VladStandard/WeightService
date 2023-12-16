using Microsoft.AspNetCore.Components;

namespace DeviceControl.Features.Shared.Form;

public sealed partial class SectionFormInputWrapper: SectionFormInputBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public bool IsFullWidth { get; set; }
    [Parameter] public bool IsTall { get; set; }
}