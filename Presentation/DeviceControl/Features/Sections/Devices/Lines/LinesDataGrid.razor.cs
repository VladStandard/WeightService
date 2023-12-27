using Blazorise;
using Blazorise.DataGrid;
using DeviceControl.Features.Sections.Devices.Hosts;
using DeviceControl.Features.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaRef.Hosts;
using Ws.StorageCore.Entities.SchemaRef.Lines;
using Ws.StorageCore.Helpers;

namespace DeviceControl.Features.Sections.Devices.Lines;

public sealed partial class LinesDataGrid: SectionDataGridBase<SqlLineEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private SqlLineRepository LineRepository { get; } = new();
    
    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<LinesCreateDialog>(new());
    
    protected override async Task OpenDataGridEntityModal(SqlLineEntity item)
        => await OpenSectionModal<LinesUpdateDialog>(item);
    
    protected override async Task OpenItemInNewTab(SqlLineEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionLines}/{item.IdentityValueUid.ToString()}");

    protected override void SetSqlSectionCast() =>
        SectionItems = LineRepository.GetEnumerable(SqlCrudConfigSection).ToList();

    protected override void SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        SectionItems = new() { SqlCoreHelper.Instance.GetItemByUid<SqlLineEntity>(itemUid) };
    }
}