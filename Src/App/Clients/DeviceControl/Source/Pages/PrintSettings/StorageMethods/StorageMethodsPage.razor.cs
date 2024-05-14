using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Features.StorageMethod;

namespace DeviceControl.Source.Pages.PrintSettings.StorageMethods;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class StorageMethodsPage : SectionDataGridPageBase<StorageMethod>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IStorageMethodService StorageMethodService { get; set; } = default!;

    #endregion

    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<StorageMethodsCreateDialog>(new());

    protected override async Task OpenDataGridEntityModal(StorageMethod item)
        => await OpenSectionModal<StorageMethodsUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(StorageMethod item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionStorageMethods}/{item.Uid.ToString()}");

    protected override IEnumerable<StorageMethod> SetSqlSectionCast() => StorageMethodService.GetAll();

    protected override IEnumerable<StorageMethod> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [StorageMethodService.GetItemByUid(itemUid)];
    }

    protected override Task DeleteItemAction(StorageMethod item)
    {
        StorageMethodService.Delete(item);
        return Task.CompletedTask;
    }
}