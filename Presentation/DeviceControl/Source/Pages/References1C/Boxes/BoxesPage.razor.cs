using DeviceControl.Source.Shared.Localization;
using DeviceControl.Source.Shared.Utils;
using DeviceControl.Source.Widgets.Section;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Features.Box;
using Ws.Shared.Resources;

namespace DeviceControl.Source.Pages.References1C.Boxes;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class BoxesPage : SectionDataGridPageBase<BoxEntity>
{
    # region Injects

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IBoxService BoxService { get; set; } = default!;

    # endregion

    protected override async Task OpenDataGridEntityModal(BoxEntity item)
        => await OpenSectionModal<BoxesUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(BoxEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionBoxes}/{item.Uid.ToString()}");

    protected override IEnumerable<BoxEntity> SetSqlSectionCast() => BoxService.GetAll();

    protected override IEnumerable<BoxEntity> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [BoxService.GetItemByUid(itemUid)];
    }
}