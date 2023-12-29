using Blazorise.DataGrid;
using DeviceControl.Features.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaScale.PlusStorageMethods;
using Ws.StorageCore.Helpers;

namespace DeviceControl.Features.Sections.References.PluStorages;

public sealed partial class PluStoragesDataGrid : SectionDataGridBase<SqlPluStorageMethodEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private SqlPluStorageMethodRepository PluStorageRepository { get; } = new();
    
    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<PluStoragesCreateDialog>(new());
    
    protected override async Task OpenDataGridEntityModal(SqlPluStorageMethodEntity item)
        => await OpenSectionModal<PluStoragesUpdateDialog>(item);
    
    protected override async Task OpenItemInNewTab(SqlPluStorageMethodEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionPlusStorage}/{item.IdentityValueUid.ToString()}");

    protected override void SetSqlSectionCast() =>
        SectionItems = PluStorageRepository.GetList(SqlCrudConfigSection);
    
    protected override void SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        SectionItems = new() { SqlCoreHelper.Instance.GetItemByUid<SqlPluStorageMethodEntity>(itemUid) };
    }
}