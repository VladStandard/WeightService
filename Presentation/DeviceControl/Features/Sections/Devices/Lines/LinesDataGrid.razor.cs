using Blazorise;
using Blazorise.DataGrid;
using DeviceControl.Features.Sections.Devices.Hosts;
using DeviceControl.Features.Shared.DataGrid;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaRef.Hosts;

namespace DeviceControl.Features.Sections.Devices.Lines;

public sealed partial class LinesDataGrid: SectionDataGridBase<SqlLineEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private SqlLineRepository LineRepository { get; } = new();

    protected override Func<SqlLineEntity, bool> SearchCondition =>
        item => item.IdentityValueId.ToString() == SearchingSectionItemId;
    
    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<LinesCreateDialog>(new());
    
    protected override async Task OpenDataGridEntityModal(SqlLineEntity item)
        => await OpenSectionModal<LinesUpdateDialog>(item);

    protected override void SetSqlSectionCast() =>
        SectionItems = LineRepository.GetEnumerable(SqlCrudConfigSection).ToList();
}