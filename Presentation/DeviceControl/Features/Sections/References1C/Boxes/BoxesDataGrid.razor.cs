using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaRef1c.Boxes;
using Ws.StorageCore.Helpers;

namespace DeviceControl.Features.Sections.References1C.Boxes;


public sealed partial class BoxesDataGrid: SectionDataGridBase<SqlBoxEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private SqlBoxRepository BoxRepository { get; } = new();

    protected override async Task OpenDataGridEntityModal(SqlBoxEntity item)
        => await OpenSectionModal<BoxesUpdateDialog>(item);
    
    protected override async Task OpenItemInNewTab(SqlBoxEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionBoxes}/{item.IdentityValueUid.ToString()}");

    protected override void SetSqlSectionCast() =>
        SectionItems = BoxRepository.GetEnumerable().ToList();
    
    protected override void SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        SectionItems = [SqlCoreHelper.Instance.GetItemByUid<SqlBoxEntity>(itemUid)];
    }
}
