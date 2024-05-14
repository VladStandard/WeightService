using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Features.Bundle;

namespace DeviceControl.Source.Pages.References1C.Bundles;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class BundlesPage : SectionDataGridPageBase<Bundle>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IBundleService BundleService { get; set; } = default!;

    #endregion

    protected override async Task OpenDataGridEntityModal(Bundle item)
        => await OpenSectionModal<BundlesUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(Bundle item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionBundles}/{item.Uid.ToString()}");

    protected override IEnumerable<Bundle> SetSqlSectionCast() => BundleService.GetAll();

    protected override IEnumerable<Bundle> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [BundleService.GetItemByUid(itemUid)];
    }
}