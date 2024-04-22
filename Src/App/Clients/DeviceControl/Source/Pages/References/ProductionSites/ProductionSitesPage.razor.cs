using DeviceControl.Source.Shared.Localization;
using DeviceControl.Source.Shared.Utils;
using DeviceControl.Source.Widgets.Section;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.ProductionSite;
using Ws.Shared.Resources;

namespace DeviceControl.Source.Pages.References.ProductionSites;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class ProductionSitesPage : SectionDataGridPageBase<ProductionSiteEntity>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IProductionSiteService ProductionSiteService { get; set; } = default!;

    #endregion

    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<ProductionSitesCreateDialog>(new());

    protected override async Task OpenDataGridEntityModal(ProductionSiteEntity item)
        => await OpenSectionModal<ProductionSitesUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(ProductionSiteEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionProductionSites}/{item.Uid.ToString()}");

    protected override IEnumerable<ProductionSiteEntity> SetSqlSectionCast() => ProductionSiteService.GetAll();

    protected override IEnumerable<ProductionSiteEntity> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [ProductionSiteService.GetItemByUid(itemUid)];
    }

    protected override Task DeleteItemAction(ProductionSiteEntity item)
    {
        ProductionSiteService.Delete(item);
        return Task.CompletedTask;
    }
}