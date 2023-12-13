using Microsoft.AspNetCore.Components;
using Ws.Shared.Enums;
using Ws.StorageCore.Entities.SchemaRef.Hosts;

namespace DeviceControl.Features.Sections.Devices.Hosts;

public sealed partial class HostsDialog: ComponentBase
{
    [Parameter] public SqlHostEntity SectionEntity { get; set; } = new();
    private List<EnumTypeModel<string>> TabsList { get; set; } = new();

    protected override void OnInitialized()
    {
        TabsList = new()
        {
            new("Hosts", "main"),
            new("Tests", "test")
        };
    }
}