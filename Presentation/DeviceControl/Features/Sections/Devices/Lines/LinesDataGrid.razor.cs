using Blazorise;
using Blazorise.DataGrid;
using DeviceControl.Features.Sections.Devices.Hosts;
using DeviceControl.Features.Shared.DataGrid;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaRef.Hosts;

namespace DeviceControl.Features.Sections.Devices.Lines;

public sealed partial class LinesDataGrid: SectionDataGridBase<SqlLineEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IModalService ModalService { get; set; } = null!;
    
    private SqlLineRepository LineRepository { get; } = new();
    
    private async Task OpenModal(DataGridRowMouseEventArgs<SqlLineEntity> e) => 
        await ModalService.Show<LinesDialog>(p =>
        {
            p.Add(x => x.OnDataChangedAction, new(this, ReloadGrid));
            p.Add(x => x.DialogSectionEntity, e.Item);
        });

    protected override void SetSqlSectionCast() =>
        SectionItems = LineRepository.GetEnumerable(SqlCrudConfigSection).ToList();
}