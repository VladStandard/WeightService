using DeviceControl.Source.Shared.Localization;
using DeviceControl.Source.Shared.Utils;
using DeviceControl.Source.Widgets.Section;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Features.Plu;
using Ws.Shared.Resources;

namespace DeviceControl.Source.Pages.References1C.Plus;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class PlusPage : SectionDataGridPageBase<PluEntity>
{
    # region Injects

    [Inject] private IPluService PluService { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;

    # endregion

    private PluValidValidator PluValidator { get; set; } = new();

    protected override async Task OpenDataGridEntityModal(PluEntity item)
        => await OpenSectionModal<PlusUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(PluEntity item) =>
        await OpenLinkInNewTab($"{RouteUtils.SectionPlus}/{item.Uid.ToString()}");

    protected override IEnumerable<PluEntity> SetSqlSectionCast() => PluService.GetAll();

    protected override IEnumerable<PluEntity> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [PluService.GetItemByUid(itemUid)];
    }
}