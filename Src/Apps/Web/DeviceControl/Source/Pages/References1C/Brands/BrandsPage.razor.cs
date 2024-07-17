using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Features.Brands;

namespace DeviceControl.Source.Pages.References1C.Brands;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class BrandsPage : SectionDataGridPageBase<Brand>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IBrandService BrandService { get; set; } = default!;

    #endregion

    protected override async Task OpenDataGridEntityModal(Brand item)
        => await OpenSectionModal<BrandsUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(Brand item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionBrands}/{item.Uid.ToString()}");

    protected override IEnumerable<Brand> SetSqlSectionCast() => BrandService.GetAll();

    protected override IEnumerable<Brand> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [BrandService.GetItemByUid(itemUid)];
    }
}