using Blazorise;
using Microsoft.AspNetCore.Components;

namespace DeviceControl.Features.Sections.Devices.Hosts;

public sealed partial class HostsDialog: ComponentBase
{
    [Inject] private IModalService ModalService { get; set; } = null!;

    private async Task CloseModal() => await ModalService.Hide();
}