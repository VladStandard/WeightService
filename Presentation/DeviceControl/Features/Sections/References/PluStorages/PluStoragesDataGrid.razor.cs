using Blazorise.DataGrid;
using DeviceControl.Features.Shared.DataGrid;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace DeviceControl.Features.Sections.References.PluStorages;

public sealed partial class PluStoragesDataGrid : SectionDataGridBase<SqlPluStorageMethodEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private SqlPluStorageMethodRepository PluStorageRepository { get; } = new();
    
    protected override async Task OpenSearchingEntityModal()
    {
        if (string.IsNullOrEmpty(SearchingSectionItemId)) return;
        Guid.TryParse(SearchingSectionItemId, out Guid newGuid);
        SqlPluStorageMethodEntity? selectedEntity = SectionItems.FirstOrDefault(x => 
            x?.IdentityValueUid == newGuid, null);
        if (selectedEntity != null) await OpenSectionModal<PluStoragesUpdateDialog>(selectedEntity);
    }
    
    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<PluStoragesCreateDialog>(new());
    
    protected override async Task OpenDataGridEntityModal(DataGridRowMouseEventArgs<SqlPluStorageMethodEntity> e)
        => await OpenSectionModal<PluStoragesUpdateDialog>(e.Item);

    protected override void SetSqlSectionCast() =>
        SectionItems = PluStorageRepository.GetList(SqlCrudConfigSection);
}