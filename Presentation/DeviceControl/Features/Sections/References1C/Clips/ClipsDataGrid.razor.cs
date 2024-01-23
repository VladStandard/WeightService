using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Features.Clip;

namespace DeviceControl.Features.Sections.References1C.Clips;


public sealed partial class ClipsDataGrid: SectionDataGridBase<ClipEntity>
{
    #region Inject
    
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IClipService ClipService { get; set; } = null!;
    
    #endregion

    protected override async Task OpenDataGridEntityModal(ClipEntity item)
        => await OpenSectionModal<ClipsUpdateDialog>(item);
    
    protected override async Task OpenItemInNewTab(ClipEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionClips}/{item.IdentityValueUid.ToString()}");

    protected override void SetSqlSectionCast() =>
        SectionItems = ClipService.GetAll();
    
    protected override void SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        SectionItems = [ClipService.GetByUid(itemUid)];
    }
}
