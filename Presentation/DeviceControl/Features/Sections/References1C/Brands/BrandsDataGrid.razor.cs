using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Features.Brand;

namespace DeviceControl.Features.Sections.References1C.Brands;


public sealed partial class BrandsDataGrid: SectionDataGridBase<BrandEntity>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IBrandService BrandService { get; set; } = null!;

    #endregion

    protected override async Task OpenDataGridEntityModal(BrandEntity item)
        => await OpenSectionModal<BrandsUpdateDialog>(item);
    
    protected override async Task OpenItemInNewTab(BrandEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionBrands}/{item.IdentityValueUid.ToString()}");

    protected override IEnumerable<BrandEntity> SetSqlSectionCast() => BrandService.GetAll();
    
    protected override IEnumerable<BrandEntity> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [BrandService.GetByUid(itemUid)];
    }
}
