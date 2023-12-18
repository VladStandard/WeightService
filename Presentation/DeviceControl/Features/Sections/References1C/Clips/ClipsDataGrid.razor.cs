using Blazorise.DataGrid;
using DeviceControl.Features.Shared.DataGrid;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace DeviceControl.Features.Sections.References1C.Clips;


public sealed partial class ClipsDataGrid: SectionDataGridBase<SqlClipEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private SqlClipRepository ClipRepository { get; } = new();
    
    protected override async Task OpenSearchingEntityModal()
    {
        if (string.IsNullOrEmpty(SearchingSectionItemId)) return;
        Guid.TryParse(SearchingSectionItemId, out Guid newGuid);
        SqlClipEntity? selectedEntity = SectionItems.FirstOrDefault(x => 
            x?.IdentityValueUid == newGuid, null);
        if (selectedEntity != null) await OpenSectionModal<ClipsUpdateDialog>(selectedEntity);
    }

    protected override async Task OpenDataGridEntityModal(DataGridRowMouseEventArgs<SqlClipEntity> e)
        => await OpenSectionModal<ClipsUpdateDialog>(e.Item);

    protected override void SetSqlSectionCast() =>
        SectionItems = ClipRepository.GetEnumerable(SqlCrudConfigSection).ToList();
}
