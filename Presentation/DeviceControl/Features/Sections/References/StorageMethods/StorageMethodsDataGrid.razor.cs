using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaRef.StorageMethods;
using Ws.StorageCore.Helpers;

namespace DeviceControl.Features.Sections.References.StorageMethods;

public sealed partial class StorageMethodsDataGrid : SectionDataGridBase<SqlStorageMethodEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private SqlStorageMethodRepository StorageRepository { get; } = new();
    
    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<StorageMethodsCreateDialog>(new());
    
    protected override async Task OpenDataGridEntityModal(SqlStorageMethodEntity item)
        => await OpenSectionModal<StorageMethodsUpdateDialog>(item);
    
    protected override async Task OpenItemInNewTab(SqlStorageMethodEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionStorageMethods}/{item.IdentityValueUid.ToString()}");

    protected override void SetSqlSectionCast() =>
        SectionItems = StorageRepository.GetList();
    
    protected override void SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        SectionItems = [SqlCoreHelper.Instance.GetItemByUid<SqlStorageMethodEntity>(itemUid)];
    }
}