using Blazorise.DataGrid;
using DeviceControl.Features.Sections.Devices.Lines;
using DeviceControl.Features.Shared.DataGrid;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace DeviceControl.Features.Sections.References.Workshops;

public sealed partial class WorkshopsDataGrid: SectionDataGridBase<SqlWorkShopEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private SqlWorkShopRepository WorkshopRepository { get; } = new();
    
    protected override Func<SqlWorkShopEntity, bool> SearchCondition =>
        item => item.IdentityValueUid.ToString() == SearchingSectionItemId;
    
    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<WorkshopsCreateDialog>(new());
    
    protected override async Task OpenDataGridEntityModal(SqlWorkShopEntity item)
        => await OpenSectionModal<WorkshopsUpdateDialog>(item);

    protected override void SetSqlSectionCast() =>
        SectionItems = WorkshopRepository.GetEnumerable(SqlCrudConfigSection).ToList();
}