using Blazorise;
using Microsoft.AspNetCore.Components;
using Ws.StorageCore.Entities.SchemaRef.Hosts;

namespace DeviceControl.Features.Sections.Devices.Hosts;

public sealed partial class HostsDialog: ComponentBase
{
    [Parameter] public SqlHostEntity SectionEntity { get; set; } = new();
    [Inject] private IModalService ModalService { get; set; } = null!;

    private async Task CloseModal() => await ModalService.Hide();
}