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
    
    protected override Func<SqlPrinterEntity, bool> SearchCondition =>
        item => item.IdentityValueUid.ToString() == SearchingSectionItemId;
    
    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<PrintersCreateDialog>(new());
    
    protected override async Task OpenDataGridEntityModal(SqlPrinterEntity item)
        => await OpenSectionModal<PrintersUpdateDialog>(item);

    protected override void SetSqlSectionCast() =>
        SectionItems = SqlPrinterRepository.GetEnumerable(SqlCrudConfigSection).ToList();
}