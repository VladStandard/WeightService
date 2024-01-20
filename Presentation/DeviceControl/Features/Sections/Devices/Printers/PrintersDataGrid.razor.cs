using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.StorageCore.Entities.Ref.Printers;
using Ws.StorageCore.Helpers;

namespace DeviceControl.Features.Sections.Devices.Printers;

public sealed partial class PrintersDataGrid: SectionDataGridBase<PrinterEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;

    private SqlPrinterRepository SqlPrinterRepository { get; } = new();
    
    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<PrintersCreateDialog>(new());
    
    protected override async Task OpenDataGridEntityModal(PrinterEntity item)
        => await OpenSectionModal<PrintersUpdateDialog>(item);
    
    protected override async Task OpenItemInNewTab(PrinterEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionPrinters}/{item.IdentityValueUid.ToString()}");

    protected override void SetSqlSectionCast() => SectionItems = SqlPrinterRepository.GetEnumerable().ToList();
    
    protected override void SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        SectionItems = [SqlCoreHelper.Instance.GetItemByUid<PrinterEntity>(itemUid)];
    }
}