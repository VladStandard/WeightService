using Blazorise;
using Blazorise.DataGrid;
using DeviceControl.Features.Shared.DataGrid;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaRef.Hosts;

namespace DeviceControl.Features.Sections.Devices.Hosts;

public sealed partial class HostsDataGrid: SectionDataGridBase<SqlHostEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IModalService ModalService { get; set; } = null!;
    private SqlHostRepository HostRepository { get; } = new();

    private async Task OpenModal(DataGridRowMouseEventArgs<SqlHostEntity> e) => 
        await ModalService.Show<HostsDialog>(p =>
        {
            p.Add(x => x.DialogSectionEntity, e.Item);
            p.Add(x => x.OnDataChangedAction, new(this, ReloadGrid));
        });

    protected override void SetSqlSectionCast() =>
        SectionItems = HostRepository.GetEnumerable(SqlCrudConfigSection).ToList();
}