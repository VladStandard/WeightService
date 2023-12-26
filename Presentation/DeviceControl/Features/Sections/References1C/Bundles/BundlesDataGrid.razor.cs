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
    
    protected override Func<SqlBundleEntity, bool> SearchCondition =>
        item => item.IdentityValueUid.ToString() == SearchingSectionItemId;

    protected override async Task OpenDataGridEntityModal(SqlBundleEntity item)
        => await OpenSectionModal<BundlesUpdateDialog>(item);

    protected override void SetSqlSectionCast() =>
        SectionItems = BundleRepository.GetEnumerable(SqlCrudConfigSection).ToList();
}
