using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.StorageCore.Entities.Ref1c.Brands;
using Ws.StorageCore.Helpers;

namespace DeviceControl.Features.Sections.References1C.Brands;


public sealed partial class BrandsDataGrid: SectionDataGridBase<BrandEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private SqlBrandRepository BrandsRepository { get; } = new();

    protected override async Task OpenDataGridEntityModal(BrandEntity item)
        => await OpenSectionModal<BrandsUpdateDialog>(item);
    
    protected override async Task OpenItemInNewTab(BrandEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionBrands}/{item.IdentityValueUid.ToString()}");

    protected override void SetSqlSectionCast() =>
        SectionItems = BrandsRepository.GetEnumerable().ToList();
    
    protected override void SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        SectionItems = [SqlCoreHelper.Instance.GetItemByUid<BrandEntity>(itemUid)];
    }
}
