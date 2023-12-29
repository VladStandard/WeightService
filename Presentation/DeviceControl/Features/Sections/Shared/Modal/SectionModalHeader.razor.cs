using Blazorise;
using Microsoft.AspNetCore.Components;

namespace DeviceControl.Features.Sections.Shared.Modal;

public sealed partial class SectionModalHeader: ComponentBase
{
    [Inject] private IModalService ModalService { get; set; } = null!;
    
    [Parameter] public RenderFragment? ChildContent { get; set; }

    private async Task CloseModal() => await ModalService.Hide();
}