using Blazorise.DataGrid;
using DeviceControl.Features.Shared.DataGrid;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace DeviceControl.Features.Sections.References1C.Bundles;


public sealed partial class BundlesDataGrid: SectionDataGridBase<SqlBundleEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private SqlBundleRepository BundleRepository { get; } = new();
    
    protected override async Task OpenSearchingEntityModal()
    {
        if (string.IsNullOrEmpty(SearchingSectionItemId)) return;
        Guid.TryParse(SearchingSectionItemId, out Guid newGuid);
        SqlBundleEntity? selectedEntity = SectionItems.FirstOrDefault(x => 
            x?.IdentityValueUid == newGuid, null);
        if (selectedEntity != null) await OpenSectionModal<BundlesUpdateDialog>(selectedEntity);
    }

    protected override async Task OpenDataGridEntityModal(DataGridRowMouseEventArgs<SqlBundleEntity> e)
        => await OpenSectionModal<BundlesUpdateDialog>(e.Item);

    protected override void SetSqlSectionCast() =>
        SectionItems = BundleRepository.GetEnumerable(SqlCrudConfigSection).ToList();
}
