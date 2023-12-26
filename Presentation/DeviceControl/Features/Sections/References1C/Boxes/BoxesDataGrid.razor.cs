using Blazorise.DataGrid;
using DeviceControl.Features.Shared.DataGrid;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace DeviceControl.Features.Sections.References1C.Boxes;


public sealed partial class BoxesDataGrid: SectionDataGridBase<SqlBoxEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private SqlBoxRepository BoxRepository { get; } = new();
    
    protected override Func<SqlBoxEntity, bool> SearchCondition =>
        item => item.IdentityValueUid.ToString() == SearchingSectionItemId;

    protected override async Task OpenDataGridEntityModal(SqlBoxEntity item)
        => await OpenSectionModal<BoxesUpdateDialog>(item);

    protected override void SetSqlSectionCast() =>
        SectionItems = BoxRepository.GetEnumerable(SqlCrudConfigSection).ToList();
}
