using Microsoft.AspNetCore.Components;

namespace DeviceControl.Features.Shared.Form;

public class SectionFormBase<TItem>: ComponentBase where TItem: new()
{
    [Parameter] public TItem SectionEntity { get; set; } = new();
    [Parameter] public EventCallback OnSaveAction { get; set; }
}