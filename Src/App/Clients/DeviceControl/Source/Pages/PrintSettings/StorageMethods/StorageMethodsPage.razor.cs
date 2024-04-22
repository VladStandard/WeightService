using DeviceControl.Source.Shared.Localization;
using DeviceControl.Source.Shared.Utils;
using DeviceControl.Source.Widgets.Section;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.StorageMethod;
using Ws.Shared.Resources;

namespace DeviceControl.Source.Pages.PrintSettings.StorageMethods;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class StorageMethodsPage : SectionDataGridPageBase<StorageMethodEntity>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IStorageMethodService StorageMethodService { get; set; } = default!;

    #endregion

    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<StorageMethodsCreateDialog>(new());

    protected override async Task OpenDataGridEntityModal(StorageMethodEntity item)
        => await OpenSectionModal<StorageMethodsUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(StorageMethodEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionStorageMethods}/{item.Uid.ToString()}");

    protected override IEnumerable<StorageMethodEntity> SetSqlSectionCast() => StorageMethodService.GetAll();

    protected override IEnumerable<StorageMethodEntity> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [StorageMethodService.GetItemByUid(itemUid)];
    }

    protected override Task DeleteItemAction(StorageMethodEntity item)
    {
        StorageMethodService.Delete(item);
        return Task.CompletedTask;
    }
}