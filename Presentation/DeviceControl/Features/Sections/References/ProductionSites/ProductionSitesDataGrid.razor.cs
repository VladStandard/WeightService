using Blazorise.DataGrid;
using DeviceControl.Features.Shared.DataGrid;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Helpers;

namespace DeviceControl.Features.Sections.References.ProductionSites;

public sealed partial class ProductionSitesDataGrid: SectionDataGridBase<SqlProductionSiteEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private SqlProductionSiteRepository PlatformsRepository { get; } = new();
    
    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<ProductionSitesCreateDialog>(new());
    
    protected override async Task OpenDataGridEntityModal(SqlProductionSiteEntity item)
        => await OpenSectionModal<ProductionSitesUpdateDialog>(item);

    protected override void SetSqlSectionCast() =>
        SectionItems = PlatformsRepository.GetEnumerable(SqlCrudConfigSection).ToList();
    
    protected override void SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        SectionItems = new() { SqlCoreHelper.Instance.GetItemByUid<SqlProductionSiteEntity>(itemUid) };
    }
}