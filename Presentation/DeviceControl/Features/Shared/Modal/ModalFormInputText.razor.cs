using Microsoft.AspNetCore.Components;

namespace DeviceControl.Features.Shared.Modal;

public sealed partial class ModalFormInputText: ModalFormInputBase
{
    [Parameter] public bool IsDisabled { get; set; }
    [Parameter] public string Value { get; set; } = string.Empty;
}