using DeviceControl2.Source.Shared.Localization;
using DeviceControl2.Source.Shared.Utils;
using DeviceControl2.Source.Widgets.Section;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Features.Bundle;
using Ws.Shared.Resources;

namespace DeviceControl2.Source.Pages.References1C.Bundles;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class BundlesPage: SectionDataGridPageBase<BundleEntity>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = null!;
    [Inject] private IBundleService BundleService { get; set; } = null!;

    #endregion

    protected override async Task OpenDataGridEntityModal(BundleEntity item)
        => await OpenSectionModal<BundlesUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(BundleEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionBundles}/{item.Uid.ToString()}");

    protected override IEnumerable<BundleEntity> SetSqlSectionCast() => BundleService.GetAll();

    protected override IEnumerable<BundleEntity> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [BundleService.GetItemByUid(itemUid)];
    }
}