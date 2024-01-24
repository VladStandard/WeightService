using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.Warehouse;

namespace DeviceControl.Features.Sections.References.Warehouses;

public sealed partial class WarehousesDataGrid: SectionDataGridBase<WarehouseEntity>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IWarehouseService WarehouseService { get; set; } = null!;

    #endregion
    
    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<WarehousesCreateDialog>(new());
    
    protected override async Task OpenDataGridEntityModal(WarehouseEntity item)
        => await OpenSectionModal<WarehousesUpdateDialog>(item);
    
    protected override async Task OpenItemInNewTab(WarehouseEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionWarehouses}/{item.IdentityValueUid.ToString()}");

    protected override IEnumerable<WarehouseEntity> SetSqlSectionCast() => WarehouseService.GetAll();
    
    protected override IEnumerable<WarehouseEntity> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [WarehouseService.GetByUid(itemUid)];
    }
}