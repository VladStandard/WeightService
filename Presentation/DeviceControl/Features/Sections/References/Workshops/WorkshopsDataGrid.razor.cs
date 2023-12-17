using Blazorise.DataGrid;
using DeviceControl.Features.Sections.Devices.Lines;
using DeviceControl.Features.Shared.DataGrid;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace DeviceControl.Features.Sections.References.Workshops;

public sealed partial class WorkshopsDataGrid: SectionDataGridBase<SqlWorkShopEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private SqlWorkShopRepository WorkshopRepository { get; } = new();
    
    protected override async Task OpenSearchingEntityModal()
    {
        if (string.IsNullOrEmpty(SearchingSectionItemId)) return;
        Guid.TryParse(SearchingSectionItemId, out Guid newGuid);
        SqlWorkShopEntity? selectedEntity = SectionItems.FirstOrDefault(x => 
            x?.IdentityValueUid == newGuid, null);
        if (selectedEntity != null) await OpenSectionModal<WorkshopsUpdateDialog>(selectedEntity);
    }
    
    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<WorkshopsCreateDialog>(new());
    
    protected override async Task OpenDataGridEntityModal(DataGridRowMouseEventArgs<SqlWorkShopEntity> e)
        => await OpenSectionModal<WorkshopsUpdateDialog>(e.Item);

    protected override void SetSqlSectionCast() =>
        SectionItems = WorkshopRepository.GetEnumerable(SqlCrudConfigSection).ToList();
}