using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.Warehouse;

namespace DeviceControl.Source.Pages.References.Warehouses;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class WarehousesPage : SectionDataGridPageBase<Warehouse>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IWarehouseService WarehouseService { get; set; } = default!;

    #endregion

    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<WarehousesCreateDialog>(new());

    protected override async Task OpenDataGridEntityModal(Warehouse item)
        => await OpenSectionModal<WarehousesUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(Warehouse item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionWarehouses}/{item.Uid.ToString()}");

    protected override IEnumerable<Warehouse> SetSqlSectionCast() => WarehouseService.GetAll();

    protected override IEnumerable<Warehouse> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [WarehouseService.GetItemByUid(itemUid)];
    }

    protected override Task DeleteItemAction(Warehouse item)
    {
        WarehouseService.Delete(item);
        return Task.CompletedTask;
    }
}