using Microsoft.AspNetCore.Components;

namespace DeviceControl.Features.Shared.Modal;

public sealed partial class ModalFormInputWrapper: ModalFormInputBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}