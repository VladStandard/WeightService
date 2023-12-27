using Blazorise.DataGrid;
using DeviceControl.Features.Shared.DataGrid;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Helpers;

namespace DeviceControl.Features.Sections.References1C.Clips;


public sealed partial class ClipsDataGrid: SectionDataGridBase<SqlClipEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private SqlClipRepository ClipRepository { get; } = new();

    protected override async Task OpenDataGridEntityModal(SqlClipEntity item)
        => await OpenSectionModal<ClipsUpdateDialog>(item);

    protected override void SetSqlSectionCast() =>
        SectionItems = ClipRepository.GetEnumerable(SqlCrudConfigSection).ToList();
    
    protected override void SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        SectionItems = new() { SqlCoreHelper.Instance.GetItemByUid<SqlClipEntity>(itemUid) };
    }
}
