using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaRef.Warehouses;
using Ws.StorageCore.Helpers;

namespace DeviceControl.Features.Sections.References.Warehouses;

public sealed partial class WarehousesDataGrid: SectionDataGridBase<SqlWarehouseEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private SqlWarehouseRepository WarehouseRepository { get; } = new();
    
    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<WarehousesCreateDialog>(new());
    
    protected override async Task OpenDataGridEntityModal(SqlWarehouseEntity item)
        => await OpenSectionModal<WarehousesUpdateDialog>(item);
    
    protected override async Task OpenItemInNewTab(SqlWarehouseEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionWarehouses}/{item.IdentityValueUid.ToString()}");

    protected override void SetSqlSectionCast() =>
        SectionItems = WarehouseRepository.GetEnumerable().ToList();
    
    protected override void SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        SectionItems = [SqlCoreHelper.Instance.GetItemByUid<SqlWarehouseEntity>(itemUid)];
    }
}