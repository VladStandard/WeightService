using DeviceControl.Features.Shared;
using DeviceControl.Features.Shared.DataGrid;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaRef.Hosts;

namespace DeviceControl.Features.Sections.Devices.Hosts;

public sealed partial class HostsPage: SectionBase<SqlHostEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    private SqlHostRepository HostRepository { get; } = new();

    protected override void SetSqlSectionCast() =>
        SectionItems = HostRepository.GetEnumerable(SqlCrudConfigSection).ToList();
}