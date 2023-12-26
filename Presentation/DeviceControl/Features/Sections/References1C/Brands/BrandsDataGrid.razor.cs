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
    
    protected override Func<SqlBrandEntity, bool> SearchCondition =>
        item => item.IdentityValueUid.ToString() == SearchingSectionItemId;

    protected override async Task OpenDataGridEntityModal(SqlBrandEntity item)
        => await OpenSectionModal<BrandsUpdateDialog>(item);

    protected override void SetSqlSectionCast() =>
        SectionItems = BrandsRepository.GetEnumerable(SqlCrudConfigSection).ToList();
}
