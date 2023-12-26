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
    
    protected override Func<SqlProductionSiteEntity, bool> SearchCondition =>
        item => item.IdentityValueUid.ToString() == SearchingSectionItemId;
    
    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<ProductionSitesCreateDialog>(new());
    
    protected override async Task OpenDataGridEntityModal(SqlProductionSiteEntity item)
        => await OpenSectionModal<ProductionSitesUpdateDialog>(item);

    protected override void SetSqlSectionCast() =>
        SectionItems = PlatformsRepository.GetEnumerable(SqlCrudConfigSection).ToList();
}