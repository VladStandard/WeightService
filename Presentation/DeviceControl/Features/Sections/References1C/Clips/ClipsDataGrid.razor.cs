using Blazorise.DataGrid;
using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaRef1c.Clips;
using Ws.StorageCore.Helpers;

namespace DeviceControl.Features.Sections.References1C.Clips;


public sealed partial class ClipsDataGrid: SectionDataGridBase<SqlClipEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private SqlClipRepository ClipRepository { get; } = new();

    protected override async Task OpenDataGridEntityModal(SqlClipEntity item)
        => await OpenSectionModal<ClipsUpdateDialog>(item);
    
    protected override async Task OpenItemInNewTab(SqlClipEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionClips}/{item.IdentityValueUid.ToString()}");

    protected override void SetSqlSectionCast() =>
        SectionItems = ClipRepository.GetEnumerable(SqlCrudConfigSection).ToList();
    
    protected override void SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        SectionItems = new() { SqlCoreHelper.Instance.GetItemByUid<SqlClipEntity>(itemUid) };
    }
}
