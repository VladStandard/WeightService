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
    
    private SqlLineRepository LineRepository { get; } = new();
    
    protected override async Task OpenSearchingEntityModal()
    {
        if (string.IsNullOrEmpty(SearchingSectionItemId)) return;
        long.TryParse(SearchingSectionItemId, out long newLong);
        SqlLineEntity? selectedEntity = SectionItems.FirstOrDefault(x => 
            x?.IdentityValueId == newLong, null);
        if (selectedEntity != null) await OpenSectionModal<LinesUpdateDialog>(selectedEntity);
    }
    
    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<LinesCreateDialog>(new());
    
    protected override async Task OpenDataGridEntityModal(DataGridRowMouseEventArgs<SqlLineEntity> e)
        => await OpenSectionModal<LinesUpdateDialog>(e.Item);

    protected override void SetSqlSectionCast() =>
        SectionItems = LineRepository.GetEnumerable(SqlCrudConfigSection).ToList();
}