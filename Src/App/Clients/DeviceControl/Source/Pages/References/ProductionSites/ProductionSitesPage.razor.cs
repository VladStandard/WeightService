using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.ProductionSite;

namespace DeviceControl.Source.Pages.References.ProductionSites;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class ProductionSitesPage : SectionDataGridPageBase<ProductionSite>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IProductionSiteService ProductionSiteService { get; set; } = default!;

    #endregion

    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<ProductionSitesCreateDialog>(new());

    protected override async Task OpenDataGridEntityModal(ProductionSite item)
        => await OpenSectionModal<ProductionSitesUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(ProductionSite item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionProductionSites}/{item.Uid.ToString()}");

    protected override IEnumerable<ProductionSite> SetSqlSectionCast() => ProductionSiteService.GetAll();

    protected override IEnumerable<ProductionSite> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [ProductionSiteService.GetItemByUid(itemUid)];
    }

    protected override Task DeleteItemAction(ProductionSite item)
    {
        ProductionSiteService.Delete(item);
        return Task.CompletedTask;
    }
}