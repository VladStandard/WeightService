using Blazorise.DataGrid;
using DeviceControl.Features.Shared.DataGrid;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace DeviceControl.Features.Sections.References1C.Brands;


public sealed partial class BrandsDataGrid: SectionDataGridBase<SqlBrandEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private SqlBrandRepository BrandsRepository { get; } = new();
    
    protected override async Task OpenSearchingEntityModal()
    {
        if (string.IsNullOrEmpty(SearchingSectionItemId)) return;
        Guid.TryParse(SearchingSectionItemId, out Guid newGuid);
        SqlBrandEntity? selectedEntity = SectionItems.FirstOrDefault(x => 
            x?.IdentityValueUid == newGuid, null);
        if (selectedEntity != null) await OpenSectionModal<BrandsUpdateDialog>(selectedEntity);
    }

    protected override async Task OpenDataGridEntityModal(DataGridRowMouseEventArgs<SqlBrandEntity> e)
        => await OpenSectionModal<BrandsUpdateDialog>(e.Item);

    protected override void SetSqlSectionCast() =>
        SectionItems = BrandsRepository.GetEnumerable(SqlCrudConfigSection).ToList();
}
