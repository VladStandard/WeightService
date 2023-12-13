using Microsoft.AspNetCore.Components;

namespace DeviceControl.Features.Shared.Modal;

public sealed partial class SectionFormInputWrapper: SectionFormInputBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}