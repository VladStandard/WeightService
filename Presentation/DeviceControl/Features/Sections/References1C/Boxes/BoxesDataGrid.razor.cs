using Blazorise.DataGrid;
using DeviceControl.Features.Shared.DataGrid;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace DeviceControl.Features.Sections.References1C.Boxes;


public sealed partial class BoxesDataGrid: SectionDataGridBase<SqlBoxEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private SqlBoxRepository BoxRepository { get; } = new();
    
    protected override async Task OpenSearchingEntityModal()
    {
        if (string.IsNullOrEmpty(SearchingSectionItemId)) return;
        Guid.TryParse(SearchingSectionItemId, out Guid newGuid);
        SqlBoxEntity? selectedEntity = SectionItems.FirstOrDefault(x => 
            x?.IdentityValueUid == newGuid, null);
        if (selectedEntity != null) await OpenSectionModal<BoxesUpdateDialog>(selectedEntity);
    }

    protected override async Task OpenDataGridEntityModal(DataGridRowMouseEventArgs<SqlBoxEntity> e)
        => await OpenSectionModal<BoxesUpdateDialog>(e.Item);

    protected override void SetSqlSectionCast() =>
        SectionItems = BoxRepository.GetEnumerable(SqlCrudConfigSection).ToList();
}
