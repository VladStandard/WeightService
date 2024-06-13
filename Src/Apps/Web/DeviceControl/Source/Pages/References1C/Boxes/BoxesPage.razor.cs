using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Features.Boxes;

namespace DeviceControl.Source.Pages.References1C.Boxes;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class BoxesPage : SectionDataGridPageBase<Box>
{
    # region Injects

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IBoxService BoxService { get; set; } = default!;

    # endregion

    protected override async Task OpenDataGridEntityModal(Box item)
        => await OpenSectionModal<BoxesUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(Box item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionBoxes}/{item.Uid.ToString()}");

    protected override IEnumerable<Box> SetSqlSectionCast() => BoxService.GetAll();

    protected override IEnumerable<Box> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [BoxService.GetItemByUid(itemUid)];
    }
}