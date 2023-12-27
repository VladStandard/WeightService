using Blazorise.DataGrid;
using DeviceControl.Features.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Helpers;

namespace DeviceControl.Features.Sections.References1C.Brands;


public sealed partial class BrandsDataGrid: SectionDataGridBase<SqlBrandEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private SqlBrandRepository BrandsRepository { get; } = new();

    protected override async Task OpenDataGridEntityModal(SqlBrandEntity item)
        => await OpenSectionModal<BrandsUpdateDialog>(item);
    
    protected override async Task OpenItemInNewTab(SqlBrandEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionBrands}/{item.IdentityValueUid.ToString()}");

    protected override void SetSqlSectionCast() =>
        SectionItems = BrandsRepository.GetEnumerable(SqlCrudConfigSection).ToList();
    
    protected override void SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        SectionItems = new() { SqlCoreHelper.Instance.GetItemByUid<SqlBrandEntity>(itemUid) };
    }
}
