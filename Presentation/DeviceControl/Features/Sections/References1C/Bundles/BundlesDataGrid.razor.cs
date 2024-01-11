using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaRef1c.Bundles;
using Ws.StorageCore.Helpers;

namespace DeviceControl.Features.Sections.References1C.Bundles;


public sealed partial class BundlesDataGrid: SectionDataGridBase<SqlBundleEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private SqlBundleRepository BundleRepository { get; } = new();

    protected override async Task OpenDataGridEntityModal(SqlBundleEntity item)
        => await OpenSectionModal<BundlesUpdateDialog>(item);
    
    protected override async Task OpenItemInNewTab(SqlBundleEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionBundles}/{item.IdentityValueUid.ToString()}");

    protected override void SetSqlSectionCast() =>
        SectionItems = BundleRepository.GetEnumerable(SqlCrudConfigSection).ToList();
    
    protected override void SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        SectionItems = [SqlCoreHelper.Instance.GetItemByUid<SqlBundleEntity>(itemUid)];
    }
}
