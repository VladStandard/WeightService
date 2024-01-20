using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.StorageCore.Entities.Ref1c.Clips;
using Ws.StorageCore.Helpers;

namespace DeviceControl.Features.Sections.References1C.Clips;


public sealed partial class ClipsDataGrid: SectionDataGridBase<ClipEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private SqlClipRepository ClipRepository { get; } = new();

    protected override async Task OpenDataGridEntityModal(ClipEntity item)
        => await OpenSectionModal<ClipsUpdateDialog>(item);
    
    protected override async Task OpenItemInNewTab(ClipEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionClips}/{item.IdentityValueUid.ToString()}");

    protected override void SetSqlSectionCast() =>
        SectionItems = ClipRepository.GetEnumerable().ToList();
    
    protected override void SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        SectionItems = [SqlCoreHelper.Instance.GetItemByUid<ClipEntity>(itemUid)];
    }
}
