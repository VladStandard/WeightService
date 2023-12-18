using Blazorise.DataGrid;
using DeviceControl.Features.Shared.DataGrid;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace DeviceControl.Features.Sections.References.ProductionSites;

public sealed partial class ProductionSitesDataGrid: SectionDataGridBase<SqlProductionSiteEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private SqlProductionSiteRepository PlatformsRepository { get; } = new();
    
    protected override async Task OpenSearchingEntityModal()
    {
        if (string.IsNullOrEmpty(SearchingSectionItemId)) return;
        Guid.TryParse(SearchingSectionItemId, out Guid newGuid);
        SqlProductionSiteEntity? selectedEntity = SectionItems.FirstOrDefault(x => 
            x?.IdentityValueUid == newGuid, null);
        if (selectedEntity != null) await OpenSectionModal<ProductionSitesUpdateDialog>(selectedEntity);
    }
    
    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<ProductionSitesCreateDialog>(new());
    
    protected override async Task OpenDataGridEntityModal(DataGridRowMouseEventArgs<SqlProductionSiteEntity> e)
        => await OpenSectionModal<ProductionSitesUpdateDialog>(e.Item);

    protected override void SetSqlSectionCast() =>
        SectionItems = PlatformsRepository.GetEnumerable(SqlCrudConfigSection).ToList();
}