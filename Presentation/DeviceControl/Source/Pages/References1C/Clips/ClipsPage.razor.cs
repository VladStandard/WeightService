using DeviceControl.Source.Shared.Localization;
using DeviceControl.Source.Shared.Utils;
using DeviceControl.Source.Widgets.Section;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Features.Clip;
using Ws.Shared.Resources;

namespace DeviceControl.Source.Pages.References1C.Clips;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class ClipsPage : SectionDataGridPageBase<ClipEntity>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IClipService ClipService { get; set; } = default!;

    #endregion

    protected override async Task OpenDataGridEntityModal(ClipEntity item)
        => await OpenSectionModal<ClipsUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(ClipEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionClips}/{item.Uid.ToString()}");

    protected override IEnumerable<ClipEntity> SetSqlSectionCast() => ClipService.GetAll();

    protected override IEnumerable<ClipEntity> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [ClipService.GetItemByUid(itemUid)];
    }
}