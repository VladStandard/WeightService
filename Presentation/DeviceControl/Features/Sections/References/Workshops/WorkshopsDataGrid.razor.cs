using Blazorise.DataGrid;
using DeviceControl.Features.Sections.Devices.Lines;
using DeviceControl.Features.Shared.DataGrid;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Helpers;

namespace DeviceControl.Features.Sections.References.Workshops;

public sealed partial class WorkshopsDataGrid: SectionDataGridBase<SqlWorkShopEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private SqlWorkShopRepository WorkshopRepository { get; } = new();
    
    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<WorkshopsCreateDialog>(new());
    
    protected override async Task OpenDataGridEntityModal(SqlWorkShopEntity item)
        => await OpenSectionModal<WorkshopsUpdateDialog>(item);

    protected override void SetSqlSectionCast() =>
        SectionItems = WorkshopRepository.GetEnumerable(SqlCrudConfigSection).ToList();
    
    protected override void SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        SectionItems = new() { SqlCoreHelper.Instance.GetItemByUid<SqlWorkShopEntity>(itemUid) };
    }
}