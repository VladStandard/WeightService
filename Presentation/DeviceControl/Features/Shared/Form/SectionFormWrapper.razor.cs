using Microsoft.AspNetCore.Components;

namespace DeviceControl.Features.Shared.Form;

public sealed partial class SectionFormWrapper: ComponentBase
{
    [Parameter] public DateTime? CreateDate { get; set; }
    [Parameter] public DateTime? ChangeDate { get; set; }
    [Parameter] public EventCallback OnSaveAction { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
}