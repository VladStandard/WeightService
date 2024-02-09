using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.ProductionSite;

namespace DeviceControl.Features.Sections.References.ProductionSites;

public sealed partial class ProductionSitesDataGrid : SectionDataGridBase<ProductionSiteEntity>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IProductionSiteService ProductionSiteService { get; set; } = null!;

    #endregion

    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<ProductionSitesCreateDialog>(new());

    protected override async Task OpenDataGridEntityModal(ProductionSiteEntity item)
        => await OpenSectionModal<ProductionSitesUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(ProductionSiteEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionProductionSites}/{item.IdentityValueUid.ToString()}");

    protected override IEnumerable<ProductionSiteEntity> SetSqlSectionCast() => ProductionSiteService.GetAll();

    protected override IEnumerable<ProductionSiteEntity> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [ProductionSiteService.GetItemByUid(itemUid)];
    }
}