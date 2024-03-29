using DeviceControl2.Source.Pages.References1C.Plus;
using DeviceControl2.Source.Shared.Localization;
using DeviceControl2.Source.Shared.Utils;
using DeviceControl2.Source.Widgets.Section;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Features.Box;
using Ws.Shared.Resources;

namespace DeviceControl2.Source.Pages.References1C.Boxes;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class BoxesPage: SectionDataGridPageBase<BoxEntity>
{
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = null!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IBoxService BoxService { get; set; } = null!;


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