using Blazorise.DataGrid;
using DeviceControl.Features.Shared.DataGrid;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace DeviceControl.Features.Sections.References1C.Clips;


public sealed partial class ClipsDataGrid: SectionDataGridBase<SqlClipEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private SqlClipRepository ClipRepository { get; } = new();
    
    protected override Func<SqlClipEntity, bool> SearchCondition =>
        item => item.IdentityValueUid.ToString() == SearchingSectionItemId;

    protected override async Task OpenDataGridEntityModal(SqlClipEntity item)
        => await OpenSectionModal<ClipsUpdateDialog>(item);

    protected override void SetSqlSectionCast() =>
        SectionItems = ClipRepository.GetEnumerable(SqlCrudConfigSection).ToList();
}
