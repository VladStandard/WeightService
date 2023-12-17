using Blazorise.DataGrid;
using DeviceControl.Features.Shared.DataGrid;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace DeviceControl.Features.Sections.Devices.Printers;

public sealed partial class PrintersDataGrid: SectionDataGridBase<SqlPrinterEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;

    private SqlPrinterRepository SqlPrinterRepository { get; } = new();
    
    protected override async Task OpenSearchingEntityModal()
    {
        if (string.IsNullOrEmpty(SearchingSectionItemId)) return;
        Guid.TryParse(SearchingSectionItemId, out Guid newGuid);
        SqlPrinterEntity? selectedEntity = SectionItems.FirstOrDefault(x => 
            x?.IdentityValueUid == newGuid, null);
        if (selectedEntity != null) await OpenSectionModal<PrintersUpdateDialog>(selectedEntity);
    }
    
    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<PrintersCreateDialog>(new());
    
    protected override async Task OpenDataGridEntityModal(DataGridRowMouseEventArgs<SqlPrinterEntity> e)
        => await OpenSectionModal<PrintersUpdateDialog>(e.Item);

    protected override void SetSqlSectionCast() =>
        SectionItems = SqlPrinterRepository.GetEnumerable(SqlCrudConfigSection).ToList();
}