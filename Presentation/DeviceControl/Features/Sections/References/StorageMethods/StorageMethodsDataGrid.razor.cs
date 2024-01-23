using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.StorageMethod;

namespace DeviceControl.Features.Sections.References.StorageMethods;

public sealed partial class StorageMethodsDataGrid : SectionDataGridBase<StorageMethodEntity>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IStorageMethodService StorageMethodService { get; set; } = null!;

    #endregion
    
    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<StorageMethodsCreateDialog>(new());
    
    protected override async Task OpenDataGridEntityModal(StorageMethodEntity item)
        => await OpenSectionModal<StorageMethodsUpdateDialog>(item);
    
    protected override async Task OpenItemInNewTab(StorageMethodEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionStorageMethods}/{item.IdentityValueUid.ToString()}");

    protected override void SetSqlSectionCast() => 
        SectionItems = StorageMethodService.GetAll();
    
    protected override void SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        SectionItems = [StorageMethodService.GetByUid(itemUid)];
    }
}