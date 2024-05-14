using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Features.Clip;

namespace DeviceControl.Source.Pages.References1C.Clips;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class ClipsPage : SectionDataGridPageBase<Clip>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IClipService ClipService { get; set; } = default!;

    #endregion

    protected override async Task OpenDataGridEntityModal(Clip item)
        => await OpenSectionModal<ClipsUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(Clip item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionClips}/{item.Uid.ToString()}");

    protected override IEnumerable<Clip> SetSqlSectionCast() => ClipService.GetAll();

    protected override IEnumerable<Clip> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [ClipService.GetItemByUid(itemUid)];
    }
}