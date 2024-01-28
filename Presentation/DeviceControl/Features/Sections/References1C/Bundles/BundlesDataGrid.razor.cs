using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Features.Bundle;

namespace DeviceControl.Features.Sections.References1C.Bundles;


public sealed partial class BundlesDataGrid: SectionDataGridBase<BundleEntity>
{
    #region Inject
    
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IBundleService BundleService { get; set; } = null!;
    
    #endregion

    protected override async Task OpenDataGridEntityModal(BundleEntity item)
        => await OpenSectionModal<BundlesUpdateDialog>(item);
    
    protected override async Task OpenItemInNewTab(BundleEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionBundles}/{item.IdentityValueUid.ToString()}");

    protected override IEnumerable<BundleEntity> SetSqlSectionCast() => BundleService.GetAll();
    
    protected override IEnumerable<BundleEntity> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [BundleService.GetItemByUid(itemUid)];
    }
}
