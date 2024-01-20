using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.StorageCore.Entities.Ref1c.Bundles;
using Ws.StorageCore.Helpers;

namespace DeviceControl.Features.Sections.References1C.Bundles;


public sealed partial class BundlesDataGrid: SectionDataGridBase<BundleEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private SqlBundleRepository BundleRepository { get; } = new();

    protected override async Task OpenDataGridEntityModal(BundleEntity item)
        => await OpenSectionModal<BundlesUpdateDialog>(item);
    
    protected override async Task OpenItemInNewTab(BundleEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionBundles}/{item.IdentityValueUid.ToString()}");

    protected override void SetSqlSectionCast() =>
        SectionItems = BundleRepository.GetEnumerable().ToList();
    
    protected override void SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        SectionItems = [SqlCoreHelper.Instance.GetItemByUid<BundleEntity>(itemUid)];
    }
}
