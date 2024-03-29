using DeviceControl2.Source.Shared.Localization;
using DeviceControl2.Source.Shared.Utils;
using DeviceControl2.Source.Widgets.Section;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.Warehouse;
using Ws.Shared.Resources;

namespace DeviceControl2.Source.Pages.References.Warehouses;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class WarehousesPage : SectionDataGridPageBase<WarehouseEntity>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IWarehouseService WarehouseService { get; set; } = default!;

    #endregion

    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<WarehousesCreateDialog>(new());

    protected override async Task OpenDataGridEntityModal(WarehouseEntity item)
        => await OpenSectionModal<WarehousesUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(WarehouseEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionWarehouses}/{item.Uid.ToString()}");

    protected override IEnumerable<WarehouseEntity> SetSqlSectionCast() => WarehouseService.GetAll();

    protected override IEnumerable<WarehouseEntity> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [WarehouseService.GetItemByUid(itemUid)];
    }

    protected override Task DeleteItemAction(WarehouseEntity item) {
        WarehouseService.Delete(item);
        return Task.CompletedTask;
    }
}
