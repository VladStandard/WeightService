using Blazorise.DataGrid;
using DeviceControl.Features.Shared.DataGrid;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Helpers;

namespace DeviceControl.Features.Sections.Devices.Printers;

public sealed partial class PrintersDataGrid: SectionDataGridBase<SqlPrinterEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;

    private SqlPrinterRepository SqlPrinterRepository { get; } = new();
    
    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<PrintersCreateDialog>(new());
    
    protected override async Task OpenDataGridEntityModal(SqlPrinterEntity item)
        => await OpenSectionModal<PrintersUpdateDialog>(item);

    protected override void SetSqlSectionCast() =>
        SectionItems = SqlPrinterRepository.GetEnumerable(SqlCrudConfigSection).ToList();
    
    protected override void SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        SectionItems = new() { SqlCoreHelper.Instance.GetItemByUid<SqlPrinterEntity>(itemUid) };
    }
}