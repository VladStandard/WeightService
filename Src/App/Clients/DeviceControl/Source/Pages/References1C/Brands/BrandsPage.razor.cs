using DeviceControl.Source.Shared.Localization;
using DeviceControl.Source.Shared.Utils;
using DeviceControl.Source.Widgets.Section;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Features.Brand;
using Ws.Shared.Resources;

namespace DeviceControl.Source.Pages.References1C.Brands;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class BrandsPage : SectionDataGridPageBase<BrandEntity>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IBrandService BrandService { get; set; } = default!;

    #endregion

    protected override async Task OpenDataGridEntityModal(BrandEntity item)
        => await OpenSectionModal<BrandsUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(BrandEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionBrands}/{item.Uid.ToString()}");

    protected override IEnumerable<BrandEntity> SetSqlSectionCast() => BrandService.GetAll();

    protected override IEnumerable<BrandEntity> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [BrandService.GetItemByUid(itemUid)];
    }
}